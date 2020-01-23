using System;
using System.Reflection;
using SPIClient;

namespace KebabPos
{
    /// <summary>
    /// NOTE: THIS PROJECT USES THE 2.7.x of the SPI Client Library
    ///  
    /// This is your POS. To integrate with SPI, you need to instantiate a Spi object
    /// and interact with it.
    /// 
    /// Primarily you need to implement 3 things.
    /// 1. Settings Screen
    /// 2. Pairing Flow Screen
    /// 3. Transaction Flow screen
    /// 
    /// To see logs from spi, you need to create a SPIClient.dll.config file next to your binary, 
    /// that contains log4net configuration, similar to what is inside app.config in this project.
    /// </summary>
    internal class KebabPos
    {
        private static void Main(string[] args)
        {
            var myPos = new KebabPos();
            myPos.Start();
        }

        private Spi _spi;
        private string _posId = "KEBABPOS1";
        private string _eftposAddress = "192.168.1.1";
        private Secrets _spiSecrets = null;

        private string _version = Assembly.GetEntryAssembly().GetName().Version.ToString();

        private void Start()
        {
            log.Info("Starting KebabPos...");
            LoadPersistedState();

            _spi = new Spi(_posId, _eftposAddress, _spiSecrets); // It is ok to not have the secrets yet to start with.
            _spi.StatusChanged += OnSpiStatusChanged;
            _spi.PairingFlowStateChanged += OnPairingFlowStateChanged;
            _spi.SecretsChanged += OnSecretsChanged;
            _spi.TxFlowStateChanged += OnTxFlowStateChanged;
            _spi.SetPosInfo("KebabPoS", "2.7");
            _spi.SetAcquirerCode("wbc");
            _spi.SetTestMode(true);
            _spi.Start();
            _spi.TransactionUpdateMessage = HandleTransactionUpdate;

            Console.Clear();
            Console.WriteLine("# Welcome to KebabPos !");
            PrintStatusAndActions();
            Console.Write("> ");
            AcceptUserInput();
        }

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

        private void PrintStatusAndActions()
        {
            PrintFlowInfo();

            PrintActions();

            PrintPairingStatus();
        }

        private void PrintFlowInfo()
        {
            switch (_spi.CurrentFlow)
            {
                case SpiFlow.Pairing:
                    var pairingState = _spi.CurrentPairingFlowState;
                    Console.WriteLine("### PAIRING PROCESS UPDATE ###");
                    Console.WriteLine($"# {pairingState.Message}");
                    Console.WriteLine($"# Finished? {pairingState.Finished}");
                    Console.WriteLine($"# Successful? {pairingState.Successful}");
                    Console.WriteLine($"# Confirmation Code: {pairingState.ConfirmationCode}");
                    Console.WriteLine($"# Waiting Confirm from Eftpos? {pairingState.AwaitingCheckFromEftpos}");
                    Console.WriteLine($"# Waiting Confirm from POS? {pairingState.AwaitingCheckFromPos}");
                    break;

                case SpiFlow.Transaction:
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

                    if (txState.AwaitingSignatureCheck)
                    {
                        // We need to print the receipt for the customer to sign.
                        Console.WriteLine($"# RECEIPT TO PRINT FOR SIGNATURE");
                        Console.WriteLine(txState.SignatureRequiredMessage.GetMerchantReceipt().TrimEnd());
                    }

                    if (txState.AwaitingPhoneForAuth)
                    {
                        Console.WriteLine($"# PHONE FOR AUTH DETAILS:");
                        Console.WriteLine($"# CALL: {txState.PhoneForAuthRequiredMessage.GetPhoneNumber()}");
                        Console.WriteLine($"# QUOTE: Merchant Id: {txState.PhoneForAuthRequiredMessage.GetMerchantId()}");
                    }

                    if (txState.Finished)
                    {
                        Console.WriteLine($"");
                        switch (txState.Type)
                        {
                            case TransactionType.Purchase:
                                HandleFinishedPurchase(txState);
                                break;
                            case TransactionType.Refund:
                                HandleFinishedRefund(txState);
                                break;
                            case TransactionType.CashoutOnly:
                                HandleFinishedCashout(txState);
                                break;
                            case TransactionType.MOTO:
                                HandleFinishedMoto(txState);
                                break;
                            case TransactionType.Settle:
                                HandleFinishedSettle(txState);
                                break;
                            case TransactionType.SettlementEnquiry:
                                HandleFinishedSettlementEnquiry(txState);
                                break;

                            case TransactionType.GetLastTransaction:
                                HandleFinishedGetLastTransaction(txState);
                                break;
                            default:
                                Console.WriteLine($"# CAN'T HANDLE TX TYPE: {txState.Type}");
                                break;
                        }
                    }
                    break;
                case SpiFlow.Idle:
                    break;
            }

            Console.WriteLine("");
        }

        private void HandleFinishedPurchase(TransactionFlowState txState)
        {
            PurchaseResponse purchaseResponse;
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# WOOHOO - WE GOT PAID!");
                    purchaseResponse = new PurchaseResponse(txState.Response);
                    Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                    Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                    Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                    Console.WriteLine("# Customer Receipt:");
                    Console.WriteLine(!purchaseResponse.WasCustomerReceiptPrinted() ? purchaseResponse.GetCustomerReceipt().TrimEnd() : "# PRINTED FROM EFTPOS");
                    Console.WriteLine("# PURCHASE: {0}", purchaseResponse.GetPurchaseAmount());
                    Console.WriteLine("# TIP: {0}", purchaseResponse.GetTipAmount());
                    Console.WriteLine("# SURCHARGE: {0}", purchaseResponse.GetSurchargeAmount());
                    Console.WriteLine("# CASHOUT: {0}", purchaseResponse.GetCashoutAmount());
                    Console.WriteLine("# BANKED NON-CASH AMOUNT: {0}", purchaseResponse.GetBankNonCashAmount());
                    Console.WriteLine("# BANKED CASH AMOUNT: {0}", purchaseResponse.GetBankCashAmount());

                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# WE DID NOT GET PAID :(");                    
                    if (txState.Response != null)
                    {
                        purchaseResponse = new PurchaseResponse(txState.Response);
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());
                        Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                        Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                        Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                        Console.WriteLine("# Customer Receipt:");
                        Console.WriteLine(!purchaseResponse.WasCustomerReceiptPrinted()
                            ? purchaseResponse.GetCustomerReceipt().TrimEnd()
                            : "# PRINTED FROM EFTPOS");
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER WE GOT PAID OR NOT :/");
                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    Console.WriteLine($"# IF YOU CONFIRM THAT THE CUSTOMER PAID, CLOSE THE ORDER.");
                    Console.WriteLine($"# OTHERWISE, RETRY THE PAYMENT FROM SCRATCH.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedRefund(TransactionFlowState txState)
        {
            RefundResponse refundResponse;
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# REFUND GIVEN- OH WELL!");
                    refundResponse = new RefundResponse(txState.Response);
                    Console.WriteLine("# Response: {0}", refundResponse.GetResponseText());
                    Console.WriteLine("# RRN: {0}", refundResponse.GetRRN());
                    Console.WriteLine("# Scheme: {0}", refundResponse.SchemeName);
                    Console.WriteLine("# Customer Receipt:");
                    Console.WriteLine(!refundResponse.WasCustomerReceiptPrinted() ? refundResponse.GetCustomerReceipt().TrimEnd() : "# PRINTED FROM EFTPOS");
                    Console.WriteLine("# REFUNDED AMOUNT: {0}", refundResponse.GetRefundAmount());
                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# REFUND FAILED!");                    
                    if (txState.Response != null)
                    {
                        refundResponse = new RefundResponse(txState.Response);
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());
                        Console.WriteLine("# Response: {0}", refundResponse.GetResponseText());
                        Console.WriteLine("# RRN: {0}", refundResponse.GetRRN());
                        Console.WriteLine("# Scheme: {0}", refundResponse.SchemeName);
                        Console.WriteLine("# Customer Receipt:");
                        Console.WriteLine(!refundResponse.WasCustomerReceiptPrinted() ? refundResponse.GetCustomerReceipt().TrimEnd() : "# PRINTED FROM EFTPOS");
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER THE REFUND WENT THROUGH OR NOT :/");
                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    Console.WriteLine($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedCashout(TransactionFlowState txState)
        {
            CashoutOnlyResponse cashoutResponse;
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# CASH-OUT SUCCESSFUL - HAND THEM THE CASH!");
                    cashoutResponse = new CashoutOnlyResponse(txState.Response);
                    Console.WriteLine("# Response: {0}", cashoutResponse.GetResponseText());
                    Console.WriteLine("# RRN: {0}", cashoutResponse.GetRRN());
                    Console.WriteLine("# Scheme: {0}", cashoutResponse.SchemeName);
                    Console.WriteLine("# Customer Receipt:");
                    Console.WriteLine(!cashoutResponse.WasCustomerReceiptPrinted() ? cashoutResponse.GetCustomerReceipt().TrimEnd() : "# PRINTED FROM EFTPOS");
                    Console.WriteLine("# CASHOUT: {0}", cashoutResponse.GetCashoutAmount());
                    Console.WriteLine("# BANKED NON-CASH AMOUNT: {0}", cashoutResponse.GetBankNonCashAmount());
                    Console.WriteLine("# BANKED CASH AMOUNT: {0}", cashoutResponse.GetBankCashAmount());
                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# CASHOUT FAILED!");                    
                    if (txState.Response != null)
                    {
                        cashoutResponse = new CashoutOnlyResponse(txState.Response);
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());                        
                        Console.WriteLine("# Response: {0}", cashoutResponse.GetResponseText());
                        Console.WriteLine("# RRN: {0}", cashoutResponse.GetRRN());
                        Console.WriteLine("# Scheme: {0}", cashoutResponse.SchemeName);
                        Console.WriteLine("# Customer Receipt:");
                        Console.WriteLine(cashoutResponse.GetCustomerReceipt().TrimEnd());
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER THE CASHOUT WENT THROUGH OR NOT :/");
                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    Console.WriteLine($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedMoto(TransactionFlowState txState)
        {
            MotoPurchaseResponse motoResponse;
            PurchaseResponse purchaseResponse;
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# WOOHOO - WE GOT MOTO-PAID!");
                    motoResponse = new MotoPurchaseResponse(txState.Response);
                    purchaseResponse = motoResponse.PurchaseResponse;
                    Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                    Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                    Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                    Console.WriteLine("# Card Entry: {0}", purchaseResponse.GetCardEntry());
                    Console.WriteLine("# Customer Receipt:");
                    Console.WriteLine(!purchaseResponse.WasCustomerReceiptPrinted() ? purchaseResponse.GetCustomerReceipt().TrimEnd() : "# PRINTED FROM EFTPOS");
                    Console.WriteLine("# PURCHASE: {0}", purchaseResponse.GetPurchaseAmount());
                    Console.WriteLine("# BANKED NON-CASH AMOUNT: {0}", purchaseResponse.GetBankNonCashAmount());
                    Console.WriteLine("# BANKED CASH AMOUNT: {0}", purchaseResponse.GetBankCashAmount());
                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# WE DID NOT GET MOTO-PAID :(");
                    if (txState.Response != null)
                    {
                        motoResponse = new MotoPurchaseResponse(txState.Response);
                        purchaseResponse = motoResponse.PurchaseResponse;
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());                        
                        Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                        Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                        Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                        Console.WriteLine("# Customer Receipt:");
                        Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER THE MOTO WENT THROUGH OR NOT :/");
                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    Console.WriteLine($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedGetLastTransaction(TransactionFlowState txState)
        {
            if (txState.Response != null)
            {
                var gltResponse = new GetLastTransactionResponse(txState.Response);

                if (_lastCmd.Length > 1) {
                    // User specified that he intended to retrieve a specific tx by pos_ref_id
                    // This is how you can use a handy function to match it.
                    var success = _spi.GltMatch(gltResponse, _lastCmd[1]);
                    if (success == Message.SuccessState.Unknown)
                    {
                        Console.WriteLine("# Did not retrieve Expected Transaction. Here is what we got:");
                    } else {
                        Console.WriteLine("# Tx Matched Expected Purchase Request.");
                    }
                }

                var purchaseResponse = new PurchaseResponse(txState.Response);
                Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                Console.WriteLine("# Error: {0}", txState.Response.GetError());
                Console.WriteLine("# Customer Receipt:");
                Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
            }
            else
            {
                // We did not even get a response, like in the case of a time-out.
                Console.WriteLine("# Could Not Retrieve Last Transaction.");
            }
        }

        private static void HandleFinishedSettle(TransactionFlowState txState)
        {
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# SETTLEMENT SUCCESSFUL!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                        Console.WriteLine("# Merchant Receipt:");
                        Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                        Console.WriteLine("# Period Start: " + settleResponse.GetPeriodStartTime());
                        Console.WriteLine("# Period End: " + settleResponse.GetPeriodEndTime());
                        Console.WriteLine("# Settlement Time: " + settleResponse.GetTriggeredTime());
                        Console.WriteLine("# Transaction Range: " + settleResponse.GetTransactionRange());
                        Console.WriteLine("# Terminal Id: " + settleResponse.GetTerminalId());
                        Console.WriteLine("# Total TX Count: " + settleResponse.GetTotalCount());
                        Console.WriteLine($"# Total TX Value: {settleResponse.GetTotalValue() / 100.0}");
                        Console.WriteLine("# By Aquirer TX Count: " + settleResponse.GetSettleByAcquirerCount());
                        Console.WriteLine($"# By Aquirer TX Value: {settleResponse.GetSettleByAcquirerValue() / 100.0}");
                        Console.WriteLine("# SCHEME SETTLEMENTS:");
                        var schemes = settleResponse.GetSchemeSettlementEntries();
                        foreach (var s in schemes)
                        {
                            Console.WriteLine("# " + s);
                        }

                    }
                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# SETTLEMENT FAILED!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Merchant Receipt:");
                        Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# SETTLEMENT ENQUIRY RESULT UNKNOWN!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void HandleFinishedSettlementEnquiry(TransactionFlowState txState)
        {
            switch (txState.Success)
            {
                case Message.SuccessState.Success:
                    Console.WriteLine($"# SETTLEMENT ENQUIRY SUCCESSFUL!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                        Console.WriteLine("# Merchant Receipt:");
                        Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                        Console.WriteLine("# Period Start: " + settleResponse.GetPeriodStartTime());
                        Console.WriteLine("# Period End: " + settleResponse.GetPeriodEndTime());
                        Console.WriteLine("# Settlement Time: " + settleResponse.GetTriggeredTime());
                        Console.WriteLine("# Transaction Range: " + settleResponse.GetTransactionRange());
                        Console.WriteLine("# Terminal Id: " + settleResponse.GetTerminalId());
                        Console.WriteLine("# Total TX Count: " + settleResponse.GetTotalCount());
                        Console.WriteLine($"# Total TX Value: {settleResponse.GetTotalValue() / 100.0}");
                        Console.WriteLine("# By Aquirer TX Count: " + settleResponse.GetSettleByAcquirerCount());
                        Console.WriteLine($"# By Aquirere TX Value: {settleResponse.GetSettleByAcquirerValue() / 100.0}");
                        Console.WriteLine("# SCHEME SETTLEMENTS:");
                        var schemes = settleResponse.GetSchemeSettlementEntries();
                        foreach (var s in schemes)
                        {
                            Console.WriteLine("# " + s);
                        }
                    }
                    break;
                case Message.SuccessState.Failed:
                    Console.WriteLine($"# SETTLEMENT ENQUIRY FAILED!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                        Console.WriteLine("# Error: {0}", txState.Response.GetError());
                        Console.WriteLine("# Merchant Receipt:");
                        Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                    }
                    break;
                case Message.SuccessState.Unknown:
                    Console.WriteLine($"# SETTLEMENT ENQUIRY RESULT UNKNOWN!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PrintActions()
        {
            Console.WriteLine("# ----------- AVAILABLE ACTIONS ------------");

            if (_spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# [kebab:1200:100:500:false] - [kebab:price:tip:cashout:promptForCash] Charge for kebab with extras!");
                Console.WriteLine("# [13kebab:1300] - MOTO - Accept Payment Over the phone");
                Console.WriteLine("# [yuck:500] - hand out a refund of $5.00!");
                Console.WriteLine("# [cashout:5000] - do a cashout only transaction");
                Console.WriteLine("# [settle] - Initiate Settlement");
                Console.WriteLine("# [settle_enq] - Settlment Enquiry");
                Console.WriteLine("#");
                Console.WriteLine("# [recover:prchs1] - Attempt State Recovery for pos_ref_id 'prchs1'");
                Console.WriteLine("# [glt:prchs1] - Get Last Transaction - Expect it to be posRefId 'prchs1'");
                Console.WriteLine("#");
                Console.WriteLine("# [rcpt_from_eftpos:true] - Offer Customer Receipt From Eftpos");
                Console.WriteLine("# [sig_flow_from_eftpos:true] - Signature Flow to be handled by Eftpos");
                Console.WriteLine("#");
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# [pos_id:CITYKEBAB1] - Set the POS ID");
            }            

            if (_spi.CurrentStatus == SpiStatus.Unpaired || _spi.CurrentStatus == SpiStatus.PairedConnecting)
            {
                if (!IsUnknownStatus())
                    Console.WriteLine("# [eftpos_address:10.161.104.104] - Set the EFTPOS ADDRESS");                
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [pair] - Pair with Eftpos");

            if (_spi.CurrentStatus != SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [unpair] - Unpair and Disconnect");

            if (_spi.CurrentFlow == SpiFlow.Pairing)
            {
                if (_spi.CurrentPairingFlowState.AwaitingCheckFromPos)
                    Console.WriteLine("# [pair_confirm] - Confirm Pairing Code");

                if (!_spi.CurrentPairingFlowState.Finished)
                    Console.WriteLine("# [pair_cancel] - Cancel Pairing");

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

                if (txState.AwaitingPhoneForAuth)
                {
                    Console.WriteLine("# [tx_auth_code:123456] - Submit Phone For Auth Code");
                }

                if (IsUnknownStatus())
                {
                    Console.WriteLine("# [ok_retry] - Attempt to Retry Tx");
                    Console.WriteLine("# [ok_override_paid] - Override As Paid Tx");
                    Console.WriteLine("# [ok_cancel] - Order As Cancelled Tx");
                }

                if (!txState.Finished && !txState.AttemptingToCancel)
                    Console.WriteLine("# [tx_cancel] - Attempt to Cancel Tx");                
                
                if (txState.Finished && txState.Success != Message.SuccessState.Unknown)
                    Console.WriteLine("# [ok] - acknowledge final");
            }

            if (!IsUnknownStatus())
            {
                Console.WriteLine("# [status] - reprint buttons/status");
                Console.WriteLine("# [bye] - exit");
            }

            Console.WriteLine();
        }

        private void HandleTransactionUpdate(Message message)
        {
            var txUpdate = new TransactionUpdate(message);
            Console.WriteLine("# Transaction Update:" + txUpdate.DisplayMessageText);
        }

        private bool IsUnknownStatus()
        {
            if (_spi.CurrentFlow == SpiFlow.Transaction)
                if (_spi.CurrentTxFlowState.Finished && _spi.CurrentTxFlowState.Success == Message.SuccessState.Unknown)
                    return true;
            return false;
        }

        private void PrintPairingStatus()
        {
            Console.WriteLine("# --------------- STATUS ------------------");
            Console.WriteLine($"# {_posId} <-> Eftpos: {_eftposAddress} #");
            Console.WriteLine($"# SPI STATUS: {_spi.CurrentStatus}     FLOW: {_spi.CurrentFlow} #");
            Console.WriteLine($"# SPI CONFIG: {_spi.Config}");
            Console.WriteLine("# -----------------------------------------");
            Console.WriteLine($"# POS: v{_version} Spi: v{Spi.GetVersion()}");

        }

        private void AcceptUserInput()
        {
            var bye = false;
            while (!bye)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) { Console.Write("> "); continue; }
                var spInput = input.Split(':');
                _lastCmd = spInput;
                try
                {
                    bye = ProcessInput(spInput);
                }
                catch (SystemException e)
                {
                    Console.WriteLine("Could Not Process Input. " + e.Message);
                    Console.WriteLine("Try Again.");
                    Console.Write("> ");
                }
            }
            Console.WriteLine("# BaBye!");
            if (_spiSecrets != null)
                Console.WriteLine($"{_posId}:{_eftposAddress}:{_spiSecrets.EncKey}:{_spiSecrets.HmacKey}");
        }

        private bool ProcessInput(string[] spInput)
        {
            switch (spInput[0].ToLower())
            {
                case "purchase":
                case "kebab":
                    _retryCmd = spInput;
                    DoPurchase();
                    break;
                case "refund":
                case "yuck":
                    _retryCmd = spInput;
                    DoRefund();
                    break;
                case "cashout":
                    _retryCmd = spInput;
                    DoCashout();                    
                    break;
                case "13kebab":
                    _retryCmd = spInput;
                    DoMoto();                    
                    break;

                case "pos_id":
                    Console.Clear();
                    if (_spi.SetPosId(spInput[1]))
                    {
                        _posId = spInput[1];
                        Console.WriteLine($"## -> POS ID now set to {_posId}");
                    }
                    else
                    {
                        Console.WriteLine($"## -> Could not set POS ID");
                    }
                    ;
                    PrintStatusAndActions();
                    Console.Write("> ");
                    break;
                case "eftpos_address":
                    Console.Clear();
                    if (_spi.SetEftposAddress(spInput[1]))
                    {
                        _eftposAddress = spInput[1];
                        Console.WriteLine($"## -> Eftpos Address now set to {_eftposAddress}");
                    }
                    else
                    {
                        Console.WriteLine($"## -> Could not set Eftpos Address");
                    }
                    ;
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

                case "tx_auth_code":
                    var sacRes = _spi.SubmitAuthCode(spInput[1]);
                    if (!sacRes.ValidFormat)
                    {
                        Console.WriteLine($"Ivalid Code Format. {sacRes.Message}. Try Again.");
                    }
                    break;

                case "settle":
                    var settleres = _spi.InitiateSettleTx(RequestIdHelper.Id("settle"));
                    if (!settleres.Initiated)
                    {
                        Console.WriteLine($"# Could not initiate settlement: {settleres.Message}. Please Retry.");
                    }
                    break;
                case "settle_enq":
                    var senqres = _spi.InitiateSettlementEnquiry(RequestIdHelper.Id("stlenq"));
                    if (!senqres.Initiated)
                    {
                        Console.WriteLine($"# Could not initiate settlement enquiry: {senqres.Message}. Please Retry.");
                    }
                    break;


                case "rcpt_from_eftpos":
                    _spi.Config.PromptForCustomerCopyOnEftpos = spInput[1].ToLower() == "true";
                    break;
                case "sig_flow_from_eftpos":
                    _spi.Config.SignatureFlowOnEftpos = spInput[1].ToLower() == "true";
                    break;

                case "ok":
                    Console.Clear();
                    _spi.AckFlowEndedAndBackToIdle();
                    PrintStatusAndActions();
                    Console.Write("> ");
                    break;

                case "recover":
                    Console.Clear();
                    var rres = _spi.InitiateRecovery(spInput[1], TransactionType.Purchase);
                    if (!rres.Initiated)
                    {
                        Console.WriteLine($"# Could not initiate recovery. {rres.Message}. Please Retry.");
                    }
                    break;

                case "glt":
                    var gltres = _spi.InitiateGetLastTx();
                    Console.WriteLine(gltres.Initiated ? "# GLT Initiated. Will be updated with Progress." : $"# Could not initiate GLT: {gltres.Message}. Please Retry.");
                    break;

                case "status":
                    Console.Clear();
                    PrintStatusAndActions();
                    break;
                case "ok_retry":
                    Console.Clear();
                    _spi.AckFlowEndedAndBackToIdle();
                    Console.WriteLine($"# Order Cancelled");
                    if (_spi.CurrentTxFlowState.Type == TransactionType.Purchase)
                    {
                        DoPurchase();
                    }
                    else if (_spi.CurrentTxFlowState.Type == TransactionType.Refund)
                    {
                        DoRefund();
                    }
                    else if (_spi.CurrentTxFlowState.Type == TransactionType.CashoutOnly)
                    {
                        DoCashout();
                    }
                    else if(_spi.CurrentTxFlowState.Type == TransactionType.MOTO)
                    {
                        DoMoto();
                    }
                    PrintStatusAndActions();
                    break;
                case "ok_override_paid":
                    Console.Clear();
                    _spi.AckFlowEndedAndBackToIdle();
                    Console.WriteLine($"# Order Paid");
                    PrintStatusAndActions();
                    break;
                case "ok_cancel":
                    Console.Clear();
                    _spi.AckFlowEndedAndBackToIdle();
                    Console.WriteLine($"# Order Cancelled");
                    PrintStatusAndActions();
                    break;
                case "bye":
                    return true;
                case "":
                    Console.Write("> ");
                    break;

                default:
                    Console.WriteLine("# I don't understand. Sorry.");
                    Console.Write("> ");
                    break;
            }
            return false;
        }

        private void DoMoto()
        {
            var motoRed = _spi.InitiateMotoPurchaseTx("kebab-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), int.Parse(_retryCmd[1]));
            if (!motoRed.Initiated)
            {
                Console.WriteLine($"# Could not initiate moto purchase: {motoRed.Message}. Please Retry.");
            }
        }

        private void DoCashout()
        {
            var coRes = _spi.InitiateCashoutOnlyTx("launder-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), int.Parse(_retryCmd[1]));
            if (!coRes.Initiated)
            {
                Console.WriteLine($"# Could not initiate cashout: {coRes.Message}. Please Retry.");
            }
        }

        private void DoRefund()
        {
            var yuckres = _spi.InitiateRefundTx("yuck-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), int.Parse(_retryCmd[1]));
            if (!yuckres.Initiated)
            {
                Console.WriteLine($"# Could not initiate refund: {yuckres.Message}. Please Retry.");
            }
        }

        private void DoPurchase()
        {
            var tipAmount = 0;
            if (_retryCmd.Length > 2) int.TryParse(_retryCmd[2], out tipAmount);
            var cashoutAmount = 0;
            if (_retryCmd.Length > 3) int.TryParse(_retryCmd[3], out cashoutAmount);
            var promptForCashout = false;
            if (_retryCmd.Length > 4) bool.TryParse(_retryCmd[4], out promptForCashout);
            // posRefId is what you would usually use to identify the order in your own system.
            var posRefId = "kebab-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");

            var pres = _spi.InitiatePurchaseTxV2(posRefId, int.Parse(_retryCmd[1]), tipAmount, cashoutAmount, promptForCashout);
            if (!pres.Initiated)
            {
                Console.WriteLine($"# Could not initiate purchase: {pres.Message}. Please Retry.");
            }            
        }

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
        }

        private string[] _retryCmd = { };        
        private string[] _lastCmd = { };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("spi");
    }
}