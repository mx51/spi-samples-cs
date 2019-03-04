using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using SPIClient;

namespace TablePos
{
    /// <summary>
    /// NOTE: THIS PROJECT USES THE 2.1.x of the SPI Client Library
    ///  
    /// This specific POS shows you how to integrate the pay-at-table features of the SPI protocol.
    /// If you're just starting, we recommend you start with KebabPos. It goes through the basics.
    /// 
    /// </summary> 
    internal class TablePos
    {
        private static void Main(string[] args)
        {
            var myPos = new TablePos();
            myPos.Start();
        }

        /// <summary>
        /// My Bills Store.
        /// Key = BillId
        /// Value = Bill 
        /// </summary>
        private Dictionary<string, Bill> billsStore = new Dictionary<string, Bill>();

        /// <summary>
        /// Lookup dictionary of table -> current order
        /// Key = TableId
        /// Value = BillId
        /// </summary>
        private Dictionary<string, string> tableToBillMapping = new Dictionary<string, string>();


        /// <summary>
        /// Assembly Payments Integration asks us to persist some data on their behalf
        /// So that the eftpos terminal can recover state.
        /// Key = BillId
        /// Value = Assembly Payments Bill Data
        /// </summary>
        private Dictionary<string, string> assemblyBillDataStore = new Dictionary<string, string>();

        private Spi _spi;
        private SpiPayAtTable _pat;
        private string _posId = "TABLEPOS1";
        private string _eftposAddress = "192.168.1.9";
        private Secrets _spiSecrets = null;
        private string _serialNumber = "";

        private void Start()
        {
            log.Info("Starting TablePos...");
            LoadPersistedState();

            _spi = new Spi(_posId, _serialNumber, _eftposAddress, _spiSecrets);
            _spi.SetPosInfo("assembly", "2.5.0");
            _spi.StatusChanged += OnSpiStatusChanged;
            _spi.PairingFlowStateChanged += OnPairingFlowStateChanged;
            _spi.SecretsChanged += OnSecretsChanged;
            _spi.TxFlowStateChanged += OnTxFlowStateChanged;

            _pat = _spi.EnablePayAtTable();
            _pat.Config.LabelTableId = "Table Number";
            _pat.GetBillStatus = PayAtTableGetBillDetails;
            _pat.BillPaymentReceived = PayAtTableBillPaymentReceived;
            _pat.BillPaymentFlowEnded = PayAtTableBillPaymentFlowEnded;
            _pat.GetOpenTables = PayAtTableGetOpenTables;

            try
            {
                _spi.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($@"SPI check failed: {e.Message}", @"Please ensure you followed all the configuration steps on your machine");
            }

            Console.Clear();
            Console.WriteLine("# Welcome to TablePos !");
            PrintStatusAndActions();
            Console.Write("> ");
            AcceptUserInput();
        }

        #region Main Spi Callbacks

        private void OnTxFlowStateChanged(object sender, TransactionFlowState txState)
        {
            Console.Clear();
            PrintStatusAndActions();
            Console.Write("> ");
        }

        private void OnPairingFlowStateChanged(object sender, PairingFlowState pairingFlowState)
        {
            Console.Clear();
            PrintStatusAndActions();
            Console.Write("> ");
        }

        private void OnSecretsChanged(object sender, Secrets secrets)
        {
            _spiSecrets = secrets;
            if (secrets != null)
            {
                Console.WriteLine($"# I Have Secrets: {secrets.EncKey}{secrets.HmacKey}. Persist them Securely.");
            }
            else
            {
                Console.WriteLine($"# I Have Lost the Secrets, i.e. Unpaired. Destroy the persisted secrets.");
            }
        }

        private void OnSpiStatusChanged(object sender, SpiStatusEventArgs status)
        {
            Console.Clear();
            Console.WriteLine($"# --> SPI Status Changed: {status.SpiStatus}");
            PrintStatusAndActions();
            Console.Write("> ");
        }

        #endregion

        #region PayAtTable Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="tableId"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        private BillStatusResponse PayAtTableGetBillDetails(string billId, string tableId, string operatorId, bool paymentFlowStarted)
        {

            if (string.IsNullOrWhiteSpace(billId))
            {
                // We were not given a billId, just a tableId.
                // This means that we are being asked for the bill by its table number.

                // Let's see if we have it.
                if (!tableToBillMapping.ContainsKey(tableId))
                {
                    // We didn't find a bill for this table.
                    // We just tell the Eftpos that.
                    return new BillStatusResponse { Result = BillRetrievalResult.INVALID_TABLE_ID };
                }

                // We have a billId for this Table.
                // Let's set it so we can retrieve it.
                billId = tableToBillMapping[tableId];
            }

            if (!billsStore.ContainsKey(billId))
            {
                // We could not find the billId that was asked for.
                // We just tell the Eftpos that.
                return new BillStatusResponse { Result = BillRetrievalResult.INVALID_BILL_ID };
            }

            var myBill = billsStore[billId];

            if (billsStore[billId].Locked && paymentFlowStarted)
            {
                Console.WriteLine($"Table is Locked.");
                return new BillStatusResponse { Result = BillRetrievalResult.INVALID_TABLE_ID };
            }

            billsStore[billId].Locked = paymentFlowStarted;

            var response = new BillStatusResponse
            {
                Result = BillRetrievalResult.SUCCESS,
                BillId = billId,
                TableId = tableId,
                OperatorId = operatorId,
                TotalAmount = myBill.TotalAmount,
                OutstandingAmount = myBill.OutstandingAmount
            };
            assemblyBillDataStore.TryGetValue(billId, out var billData);
            response.BillData = billData;
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billPayment"></param>
        /// <param name="updatedBillData"></param>
        /// <returns></returns>
        private BillStatusResponse PayAtTableBillPaymentReceived(BillPayment billPayment, string updatedBillData)
        {
            if (!billsStore.ContainsKey(billPayment.BillId))
            {
                // We cannot find this bill.
                return new BillStatusResponse { Result = BillRetrievalResult.INVALID_BILL_ID };
            }

            Console.WriteLine($"# Got a {billPayment.PaymentType} Payment against bill {billPayment.BillId} for table {billPayment.TableId}");
            var bill = billsStore[billPayment.BillId];
            bill.OutstandingAmount -= billPayment.PurchaseAmount;
            bill.TippedAmount += billPayment.TipAmount;
            bill.Locked = bill.OutstandingAmount == 0 ? false : true;

            Console.WriteLine($"Updated Bill: {bill}");
            Console.Write($"> ");

            // Here you can access other data that you might want to store from this payment, for example the merchant receipt.
            // billPayment.PurchaseResponse.GetMerchantReceipt();

            // It is important that we persist this data on behalf of assembly.
            assemblyBillDataStore[billPayment.BillId] = updatedBillData;

            return new BillStatusResponse
            {
                Result = BillRetrievalResult.SUCCESS,
                OutstandingAmount = bill.OutstandingAmount,
                TotalAmount = bill.TotalAmount
            };
        }

        private void PayAtTableBillPaymentFlowEnded(Message message)
        {
            var billPaymentFlowEndedResponse = new BillPaymentFlowEndedResponse(message);

            if (!billsStore.ContainsKey(billPaymentFlowEndedResponse.BillId))
            {
                // We cannot find this bill.
                Console.WriteLine("Incorrect Bill Id!");
            }

            var myBill = billsStore[billPaymentFlowEndedResponse.BillId];
            myBill.Locked = false;

            Console.WriteLine(
                $"Bill Id                : {billPaymentFlowEndedResponse.BillId}" + Environment.NewLine +
                $"Table                  : {billPaymentFlowEndedResponse.TableId}" + Environment.NewLine +
                $"Operator Id            : {billPaymentFlowEndedResponse.OperatorId}" + Environment.NewLine +
                $"Bill OutStanding Amount: ${billPaymentFlowEndedResponse.BillOutstandingAmount / 100.0:0.00}" + Environment.NewLine +
                $"Bill Total Amount      : ${billPaymentFlowEndedResponse.BillTotalAmount / 100.0:0.00}" + Environment.NewLine +
                $"Card Total Count       : {billPaymentFlowEndedResponse.CardTotalCount}" + Environment.NewLine +
                $"Card Total Amount      : {billPaymentFlowEndedResponse.CardTotalAmount}" + Environment.NewLine +
                $"Cash Total Count       : {billPaymentFlowEndedResponse.CashTotalCount}" + Environment.NewLine +
                $"Cash Total Amount      : {billPaymentFlowEndedResponse.CashTotalAmount}" + Environment.NewLine +
                $"Locked                 : {myBill.Locked}");
            Console.Write("> ");
        }

        private GetOpenTablesResponse PayAtTableGetOpenTables(string operatorId)
        {
            var openTableList = new List<OpenTablesEntry>();
            bool isOpenTables = false;

            if (tableToBillMapping.Count > 0)
            {
                foreach (var item in tableToBillMapping)
                {
                    if (billsStore[item.Value].OperatorId == operatorId && billsStore[item.Value].OutstandingAmount > 0)
                    {
                        if (!isOpenTables)
                        {
                            Console.WriteLine("#    Open Tables: ");
                            isOpenTables = true;
                        }

                        var openTablesItem = new OpenTablesEntry
                        {
                            TableId = item.Key,
                            Label = billsStore[item.Value].Label,
                            BillOutstandingAmount = billsStore[item.Value].OutstandingAmount
                        };

                        Console.WriteLine("Table Id : " + item.Key + ", Bill Id: " + billsStore[item.Value].BillId + ", Outstanding Amount: $" + billsStore[item.Value].OutstandingAmount / 100);
                        openTableList.Add(openTablesItem);
                    }
                }
            }


            if (!isOpenTables)
            {
                Console.WriteLine("# No Open Tables.");
            }

            var openTableListJson = JsonConvert.SerializeObject(openTableList);

            return new GetOpenTablesResponse
            {
                TableData = Convert.ToBase64String(Encoding.UTF8.GetBytes(openTableListJson))
            };
        }

        #endregion

        #region User Interface

        private void PrintStatusAndActions()
        {
            PrintFlowInfo();

            PrintActions();

            PrintPairingStatus();
        }

        private void PrintFlowInfo()
        {
            if (_spi.CurrentFlow == SpiFlow.Pairing)
            {
                var pairingState = _spi.CurrentPairingFlowState;
                Console.WriteLine("### PAIRING PROCESS UPDATE ###");
                Console.WriteLine($"# {pairingState.Message}");
                Console.WriteLine($"# Finished? {pairingState.Finished}");
                Console.WriteLine($"# Successful? {pairingState.Successful}");
                Console.WriteLine($"# Confirmation Code: {pairingState.ConfirmationCode}");
                Console.WriteLine($"# Waiting Confirm from Eftpos? {pairingState.AwaitingCheckFromEftpos}");
                Console.WriteLine($"# Waiting Confirm from POS? {pairingState.AwaitingCheckFromPos}");
            }

            if (_spi.CurrentFlow == SpiFlow.Transaction)
            {
                var txState = _spi.CurrentTxFlowState;
                Console.WriteLine("### TX PROCESS UPDATE ###");
                Console.WriteLine($"# {txState.DisplayMessage}");
                Console.WriteLine($"# Id: {txState.PosRefId}");
                Console.WriteLine($"# Type: {txState.Type}");
                Console.WriteLine($"# Amount: ${txState.AmountCents / 100.0}");
                Console.WriteLine($"# Waiting For Signature: {txState.AwaitingSignatureCheck}");
                Console.WriteLine($"# Attempting to Cancel : {txState.AttemptingToCancel}");
                Console.WriteLine($"# Finished: {txState.Finished}");
                Console.WriteLine($"# Success: {txState.Success}");

                if (txState.Finished)
                {
                    Console.WriteLine($"");
                    switch (txState.Success)
                    {
                        case Message.SuccessState.Success:
                            if (txState.Type == TransactionType.Purchase)
                            {
                                Console.WriteLine($"# WOOHOO - WE GOT PAID!");
                                var purchaseResponse = new PurchaseResponse(txState.Response);
                                Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                                Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                                Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                                Console.WriteLine("# Customer Receipt:");
                                Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
                                Console.WriteLine("# PURCHASE: {0}", purchaseResponse.GetPurchaseAmount());
                                Console.WriteLine("# TIP: {0}", purchaseResponse.GetTipAmount());
                                Console.WriteLine("# CASHOUT: {0}", purchaseResponse.GetCashoutAmount());
                                Console.WriteLine("# BANKED NON-CASH AMOUNT: {0}", purchaseResponse.GetBankNonCashAmount());
                                Console.WriteLine("# BANKED CASH AMOUNT: {0}", purchaseResponse.GetBankCashAmount());
                            }
                            else if (txState.Type == TransactionType.Refund)
                            {
                                Console.WriteLine($"# REFUND GIVEN - OH WELL!");
                                var refundResponse = new RefundResponse(txState.Response);
                                Console.WriteLine("# Response: {0}", refundResponse.GetResponseText());
                                Console.WriteLine("# RRN: {0}", refundResponse.GetRRN());
                                Console.WriteLine("# Scheme: {0}", refundResponse.SchemeName);
                                Console.WriteLine("# Customer Receipt:");
                                Console.WriteLine(refundResponse.GetCustomerReceipt().TrimEnd());
                            }
                            else if (txState.Type == TransactionType.Settle)
                            {
                                Console.WriteLine($"# SETTLEMENT SUCCESSFUL!");
                                if (txState.Response != null)
                                {
                                    var settleResponse = new Settlement(txState.Response);
                                    Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                                    Console.WriteLine("# Merchant Receipt:");
                                    Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                                }
                            }
                            break;
                        case Message.SuccessState.Failed:
                            if (txState.Type == TransactionType.Purchase)
                            {
                                Console.WriteLine($"# WE DID NOT GET PAID :(");
                                if (txState.Response != null)
                                {
                                    var purchaseResponse = new PurchaseResponse(txState.Response);
                                    Console.WriteLine("# Error: {0}", txState.Response.GetError());
                                    Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                                    Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                                    Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                                    Console.WriteLine("# Customer Receipt:");
                                    Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
                                }
                            }
                            else if (txState.Type == TransactionType.Refund)
                            {
                                Console.WriteLine($"# REFUND FAILED!");
                                if (txState.Response != null)
                                {
                                    var refundResponse = new RefundResponse(txState.Response);
                                    Console.WriteLine("# Response: {0}", refundResponse.GetResponseText());
                                    Console.WriteLine("# RRN: {0}", refundResponse.GetRRN());
                                    Console.WriteLine("# Scheme: {0}", refundResponse.SchemeName);
                                    Console.WriteLine("# Customer Receipt:");
                                    Console.WriteLine(refundResponse.GetCustomerReceipt().TrimEnd());
                                }
                            }
                            else if (txState.Type == TransactionType.Settle)
                            {
                                Console.WriteLine($"# SETTLEMENT FAILED!");
                                if (txState.Response != null)
                                {
                                    var settleResponse = new Settlement(txState.Response);
                                    Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                                    Console.WriteLine("# Error: {0}", txState.Response.GetError());
                                    Console.WriteLine("# Merchant Receipt:");
                                    Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                                }
                            }

                            break;
                        case Message.SuccessState.Unknown:
                            if (txState.Type == TransactionType.Purchase)
                            {
                                Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER WE GOT PAID OR NOT :/");
                                Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                                Console.WriteLine($"# IF YOU CONFIRM THAT THE CUSTOMER PAID, CLOSE THE ORDER.");
                                Console.WriteLine($"# OTHERWISE, RETRY THE PAYMENT FROM SCRATCH.");
                            }
                            else if (txState.Type == TransactionType.Refund)
                            {
                                Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER THE REFUND WENT THROUGH OR NOT :/");
                                Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                                Console.WriteLine($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                            }
                            break;
                    }
                }
            }
            Console.WriteLine("");
        }

        private void PrintActions()
        {
            Console.WriteLine("# ----------- TABLE ACTIONS ------------");
            Console.WriteLine("# [open:12:3:vip]   - Start a new bill for table 12, operator Id 3, Label is vip");
            Console.WriteLine("# [add:12:1000]     - Add $10.00 to the bill of table 12");
            Console.WriteLine("# [close:12]        - Close table 12");
            Console.WriteLine("# [lock:12:true]    - Lock/Unlock table 12");
            Console.WriteLine("# [tables]          - List open tables");
            Console.WriteLine("# [table:12]        - Print current bill for table 12");
            Console.WriteLine("# [bill:9876789876] - Print bill with id 9876789876");
            Console.WriteLine("#");

            if (_spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# ----------- OTHER ACTIONS ------------");
                Console.WriteLine("# [purchase:1200] - Quick Purchase Tx");
                Console.WriteLine("# [yuck] - hand out a refund!");
                Console.WriteLine("# [settle] - Initiate Settlement");
                Console.WriteLine("#");
                Console.WriteLine("# [print_merchant_copy:true] - Offer Merchant Receipt From Eftpos");
                Console.WriteLine("# [rcpt_from_eftpos:true] - Offer Customer Receipt From Eftpos");
                Console.WriteLine("# [sig_flow_from_eftpos:true] - Signature Flow to be handled by Eftpos");
                Console.WriteLine("#");
            }
            Console.WriteLine("# ----------- SETTINGS ACTIONS ------------");
            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# [pos_id:TABLEPOS1] - Set the POS ID");
            }
            if (_spi.CurrentStatus == SpiStatus.Unpaired || _spi.CurrentStatus == SpiStatus.PairedConnecting)
            {
                Console.WriteLine("# [eftpos_address:10.161.104.104] - Set the EFTPOS ADDRESS");
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [pair] - Pair with Eftpos");

            if (_spi.CurrentStatus != SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [unpair] - Unpair and Disconnect");
            Console.WriteLine("#");

            Console.WriteLine("# ----------- FLOW ACTIONS ------------");
            if (_spi.CurrentFlow == SpiFlow.Pairing)
            {
                Console.WriteLine("# [pair_cancel] - Cancel Pairing");

                if (_spi.CurrentPairingFlowState.AwaitingCheckFromPos)
                    Console.WriteLine("# [pair_confirm] - Confirm Pairing Code");

                if (_spi.CurrentPairingFlowState.Finished)
                    Console.WriteLine("# [ok] - acknowledge final");
            }

            if (_spi.CurrentFlow == SpiFlow.Transaction)
            {
                var txState = _spi.CurrentTxFlowState;

                if (txState.AwaitingSignatureCheck)
                {
                    Console.WriteLine("# [tx_sign_accept] - Accept Signature");
                    Console.WriteLine("# [tx_sign_decline] - Decline Signature");
                }

                if (!txState.Finished && !txState.AttemptingToCancel)
                    Console.WriteLine("# [tx_cancel] - Attempt to Cancel Tx");

                if (txState.Finished)
                    Console.WriteLine("# [ok] - acknowledge final");
            }

            Console.WriteLine("# [status] - reprint buttons/status");
            Console.WriteLine("# [bye] - exit");
            Console.WriteLine();
        }

        private void PrintPairingStatus()
        {
            Console.WriteLine("# --------------- STATUS ------------------");
            Console.WriteLine($"# {_posId} <-> Eftpos: {_eftposAddress} #");
            Console.WriteLine($"# SPI STATUS: {_spi.CurrentStatus}     FLOW: {_spi.CurrentFlow} #");
            Console.WriteLine($"# SPI CONFIG: {_spi.Config}");
            Console.WriteLine("# ----------------TABLES-------------------");
            Console.WriteLine($"# Open Tables   : {tableToBillMapping.Count}");
            Console.WriteLine($"# Bills in Store: {billsStore.Count}");
            Console.WriteLine($"# Assembly Bills: {assemblyBillDataStore.Count}");
            Console.WriteLine($"# -----------------------------------------");
            Console.WriteLine($"# POS: v{_version} Spi: v{Spi.GetVersion()}");
        }

        private void AcceptUserInput()
        {
            var bye = false;
            while (!bye)
            {
                var input = Console.ReadLine();
                var spInput = input.Split(':');
                switch (spInput[0].ToLower())
                {
                    case "open":
                        if (!CheckInput(spInput, 2)) { break; };
                        var label = "";
                        if (spInput.Length > 3)
                        {
                            label = spInput[3];
                        }
                        OpenTable(spInput[1], spInput[2], label); Console.Write("> ");
                        break;
                    case "close":
                        if (!CheckInput(spInput, 1)) { break; };
                        CloseTable(spInput[1]); Console.Write("> ");
                        break;
                    case "lock":
                        if (!CheckInput(spInput, 2)) { break; };
                        var isLock = false;
                        bool.TryParse(spInput[2], out isLock);
                        LockTable(spInput[1], isLock); Console.Write("> ");
                        break;
                    case "add":
                        if (!CheckInput(spInput, 1)) { break; };
                        AddToTable(spInput[1], int.Parse(spInput[2])); Console.Write("> ");
                        break;
                    case "table":
                        if (!CheckInput(spInput, 1)) { break; };
                        PrintTable(spInput[1]); Console.Write("> ");
                        break;
                    case "bill":
                        if (!CheckInput(spInput, 1)) { break; };
                        PrintBill(spInput[1]); Console.Write("> ");
                        break;
                    case "tables":
                        PrintTables(); Console.Write("> ");
                        break;

                    case "purchase":
                        if (!CheckInput(spInput, 1)) { break; };
                        var pres = _spi.InitiatePurchaseTxV2("purchase-" + DateTime.Now.ToString("o"), int.Parse(spInput[1]), 0, 0, false);
                        if (!pres.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate purchase: {pres.Message}. Please Retry.");
                        }
                        break;
                    case "refund":
                    case "yuck":
                        var yuckres = _spi.InitiateRefundTx("yuck-" + DateTime.Now.ToString("o"), 1000);
                        if (!yuckres.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate refund: {yuckres.Message}. Please Retry.");
                        }
                        break;

                    case "pos_id":
                        Console.Clear();
                        if (!CheckInput(spInput, 1)) { break; };
                        if (_spi.SetPosId(spInput[1]))
                        {
                            _posId = spInput[1];
                            Console.WriteLine($"## -> POS ID now set to {_posId}");
                        }
                        else
                        {
                            Console.WriteLine($"## -> Could not set POS ID");
                        };
                        PrintStatusAndActions();
                        Console.Write("> ");
                        break;
                    case "eftpos_address":
                        Console.Clear();
                        if (!CheckInput(spInput, 1)) { break; };
                        if (_spi.SetEftposAddress(spInput[1]))
                        {
                            _eftposAddress = spInput[1];
                            Console.WriteLine($"## -> Eftpos Address now set to {_eftposAddress}");
                        }
                        else
                        {
                            Console.WriteLine($"## -> Could not set Eftpos Address");
                        };
                        PrintStatusAndActions();
                        Console.Write("> ");
                        break;

                    case "pair":
                        var pairingInited = _spi.Pair();
                        if (!pairingInited) Console.WriteLine($"## -> Could not Start Pairing. Check Settings.");
                        break;
                    case "pair_cancel":
                        _spi.PairingCancel();
                        break;
                    case "pair_confirm":
                        _spi.PairingConfirmCode();
                        break;
                    case "unpair":
                        _spi.Unpair();
                        break;

                    case "tx_sign_accept":
                        _spi.AcceptSignature(true);
                        break;
                    case "tx_sign_decline":
                        _spi.AcceptSignature(false);
                        break;
                    case "tx_cancel":
                        _spi.CancelTransaction();
                        break;

                    case "settle":
                        var settleres = _spi.InitiateSettleTx(RequestIdHelper.Id("settle"));
                        if (!settleres.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate settlement: {settleres.Message}. Please Retry.");
                        }
                        break;

                    case "print_merchant_copy":
                        _spi.Config.PrintMerchantCopy = spInput[1].ToLower() == "true";
                        Console.Write("> ");
                        break;
                    case "rcpt_from_eftpos":
                        _spi.Config.PromptForCustomerCopyOnEftpos = spInput[1].ToLower() == "true";
                        Console.Write("> ");
                        break;
                    case "sig_flow_from_eftpos":
                        _spi.Config.SignatureFlowOnEftpos = spInput[1].ToLower() == "true";
                        Console.Write("> ");
                        break;

                    case "ok":
                        Console.Clear();
                        _spi.AckFlowEndedAndBackToIdle();
                        PrintStatusAndActions();
                        Console.Write("> ");
                        break;

                    case "status":
                        Console.Clear();
                        PrintStatusAndActions();
                        break;
                    case "bye":
                        bye = true;
                        break;
                    case "":
                        Console.Write("> ");
                        break;

                    default:
                        Console.WriteLine("# I don't understand. Sorry.");
                        Console.Write("> ");
                        break;
                }
            }
            Console.WriteLine("# BaBye!");
            PersistState();
            if (_spiSecrets != null)
                Console.WriteLine($"{_posId}:{_eftposAddress}:{_spiSecrets.EncKey}:{_spiSecrets.HmacKey}");
        }

        private bool CheckInput(string[] input, int paramCount)
        {
            if (input.Length <= paramCount)
            {
                Console.WriteLine("# I don't understand. Sorry.");
                Console.Write("> ");
                return false;
            }

            return true;
        }

        #endregion

        #region My Pos Functions

        private void OpenTable(string tableId, string operatorId, string label)
        {
            int tableIdInt;
            int.TryParse(tableId, out tableIdInt);
            if (tableIdInt <= 0)
            {
                Console.WriteLine("Incorrect Table Id!");
                return;
            }

            tableId = tableId.Trim();

            if (tableToBillMapping.ContainsKey(tableId))
            {
                Console.WriteLine($"Table Already Open: {billsStore[tableToBillMapping[tableId]]}");
                return;
            }

            var newBill = new Bill() { BillId = NewBillId(), TableId = tableId, OperatorId = operatorId, Label = label };
            billsStore.Add(newBill.BillId, newBill);
            tableToBillMapping.Add(newBill.TableId, newBill.BillId);
            Console.WriteLine($"Opened: {newBill}");
        }

        private void AddToTable(string tableId, int amountCents)
        {
            tableId = tableId.Trim();
            if (!tableToBillMapping.ContainsKey(tableId))
            {
                Console.WriteLine($"Table not Open.");
                return;
            }

            var bill = billsStore[tableToBillMapping[tableId]];
            if (bill.Locked)
            {
                Console.WriteLine($"Table is Locked.");
                return;
            }

            bill.TotalAmount += amountCents;
            bill.OutstandingAmount += amountCents;
            Console.WriteLine($"Updated: {bill}");
        }

        private void CloseTable(string tableId)
        {
            tableId = tableId.Trim();
            if (!tableToBillMapping.ContainsKey(tableId))
            {
                Console.WriteLine($"Table not Open.");
                return;
            }
            var bill = billsStore[tableToBillMapping[tableId]];
            if (bill.Locked)
            {
                Console.WriteLine($"Table is Locked.");
                return;
            }

            if (bill.OutstandingAmount > 0)
            {
                Console.WriteLine($"Bill not Paid Yet: {bill}");
                return;
            }
            tableToBillMapping.Remove(tableId);
            assemblyBillDataStore.Remove(bill.BillId);
            Console.WriteLine($"Closed: {bill}");
        }

        private void LockTable(string tableId, bool locked)
        {
            tableId = tableId.Trim();
            if (!tableToBillMapping.ContainsKey(tableId))
            {
                Console.WriteLine($"Table not Open.");
                return;
            }
            var bill = billsStore[tableToBillMapping[tableId]];
            bill.Locked = locked;
            if (locked)
            {
                Console.WriteLine($"Locked: {bill}");
            }
            else
            {
                Console.WriteLine($"UnLocked: {bill}");
            }
        }

        private void PrintTable(string tableId)
        {
            tableId = tableId.Trim();
            if (!tableToBillMapping.ContainsKey(tableId))
            {
                Console.WriteLine($"Table not Open.");
                return;
            }
            PrintBill(tableToBillMapping[tableId]);
        }

        private void PrintTables()
        {
            if (tableToBillMapping.Count > 0)
            {
                Console.WriteLine("#    Open Tables: " + tableToBillMapping.Keys.Aggregate((i, j) => i + "," + j));
            }
            else
            {
                Console.WriteLine("# No Open Tables.");
            }

            if (billsStore.Count > 0)
            {
                Console.WriteLine("# My Bills Store: " + billsStore.Keys.Aggregate((i, j) => i + "," + j));
            }

            if (assemblyBillDataStore.Count > 0)
            {
                Console.WriteLine("# Assembly Bills Data: " + assemblyBillDataStore.Keys.Aggregate((i, j) => i + "," + j));
            }
        }

        private void PrintBill(string billId)
        {
            billId = billId.Trim();
            if (!billsStore.ContainsKey(billId))
            {
                Console.WriteLine($"Bill Not Found.");
                return;
            }
            Console.WriteLine($"Bill: {billsStore[billId]}");
        }

        private string NewBillId()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds.ToString();
        }

        #endregion

        #region Persistence
        private void LoadPersistedState()
        {
            // Let's read cmd line arguments.
            var cmdArgs = Environment.GetCommandLineArgs();
            if (cmdArgs.Length <= 1) return; // nothing passed in

            if (string.IsNullOrWhiteSpace(cmdArgs[1])) return;

            var argSplit = cmdArgs[1].Split(':');
            _posId = argSplit[0];
            _eftposAddress = argSplit[1];
            _spiSecrets = new Secrets(argSplit[2], argSplit[3]);

            if (File.Exists("tableToBillMapping.bin"))
            {
                tableToBillMapping = ReadFromBinaryFile<Dictionary<string, string>>("tableToBillMapping.bin");
                billsStore = ReadFromBinaryFile<Dictionary<string, Bill>>("billsStore.bin");
                assemblyBillDataStore = ReadFromBinaryFile<Dictionary<string, string>>("assemblyBillDataStore.bin");
            }
        }

        private void PersistState()
        {
            WriteToBinaryFile("tableToBillMapping.bin", tableToBillMapping);
            WriteToBinaryFile("billsStore.bin", billsStore);
            WriteToBinaryFile("assemblyBillDataStore.bin", assemblyBillDataStore);
        }

        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        #endregion

        #region My Model

        [Serializable]
        class Bill
        {
            public string BillId;
            public string TableId;
            public string OperatorId;
            public string Label;
            public int TotalAmount = 0;
            public int OutstandingAmount = 0;
            public int TippedAmount = 0;
            public bool Locked = false;



            public override string ToString()
            {
                return $"{BillId} - Table:{TableId} Operator Id:{OperatorId} Label:{Label} Total:${TotalAmount / 100.0:0.00} " +
                    $"Outstanding:${OutstandingAmount / 100.0:0.00} Tips:${TippedAmount / 100.0:0.00} Locked:{Locked}";
            }
        }

        #endregion

        private readonly string _version = Assembly.GetEntryAssembly().GetName().Version.ToString();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("spi");
    }
}