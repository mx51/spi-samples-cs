using System;
using System.Reflection;
using SPIClient;

namespace MotelPos
{
    /// <summary>
    /// NOTE: THIS PROJECT USES THE 2.1.x of the SPI Client Library
    ///  
    /// This specific POS shows you how to integrate the preauth features of the SPI protocol.
    /// If you're just starting, we recommend you start with KebabPos. It goes through the basics.
    /// 
    /// </summary> 
    internal class MotelPos
    {
        private static void Main(string[] args)
        {
            var myPos = new MotelPos();
            myPos.Start();
        }

        private Spi _spi;
        private SpiPreauth _spiPreauth;
        private string _posId = "MOTELPOS1";
        private string _eftposAddress = "192.168.1.6";
        private Secrets _spiSecrets = null;
        private string _version = Assembly.GetEntryAssembly().GetName().Version.ToString();

        private void Start()
        {
            log.Info("Starting MotelPos...");

            LoadPersistedState();

            _spi = new Spi(_posId, _eftposAddress, _spiSecrets); // It is ok to not have the secrets yet to start with.
            _spi.StatusChanged += OnSpiStatusChanged;
            _spi.PairingFlowStateChanged += OnPairingFlowStateChanged;
            _spi.SecretsChanged += OnSecretsChanged;
            _spi.TxFlowStateChanged += OnTxFlowStateChanged;
            _spiPreauth = _spi.EnablePreauth();
            _spi.Start();

            Console.Clear();
            Console.WriteLine("# Welcome to MotelPos!");
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
                Console.WriteLine($"# Request Amount: ${txState.AmountCents / 100.0}");
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

                if (txState.Finished)
                {
                    Console.WriteLine($"");
                    switch (txState.Success)
                    {
                        case Message.SuccessState.Success:
                            switch (txState.Type)
                            {
                                case TransactionType.Preauth:
                                    {
                                        Console.WriteLine("# PREAUTH RESULT - SUCCESS");
                                        var preauthResponse = new PreauthResponse(txState.Response);
                                        Console.WriteLine("# PREAUTH-ID: {0}", preauthResponse.PreauthId);
                                        Console.WriteLine("# NEW BALANCE AMOUNT: {0}", preauthResponse.GetBalanceAmount());
                                        Console.WriteLine("# PREV BALANCE AMOUNT: {0}", preauthResponse.GetPreviousBalanceAmount());
                                        Console.WriteLine("# COMPLETION AMOUNT: {0}", preauthResponse.GetCompletionAmount());

                                        var details = preauthResponse.Details;
                                        Console.WriteLine("# Response: {0}", details.GetResponseText());
                                        Console.WriteLine("# RRN: {0}", details.GetRRN());
                                        Console.WriteLine("# Scheme: {0}", details.SchemeName);
                                        Console.WriteLine("# Customer Receipt:");
                                        Console.WriteLine(details.GetCustomerReceipt().TrimEnd());
                                        break;
                                    }
                                case TransactionType.AccountVerify:
                                    {
                                        Console.WriteLine($"# ACCOUNT VERIFICATION SUCCESS");
                                        var acctVerifyResponse = new AccountVerifyResponse(txState.Response);
                                        var details = acctVerifyResponse.Details;
                                        Console.WriteLine("# Response: {0}", details.GetResponseText());
                                        Console.WriteLine("# RRN: {0}", details.GetRRN());
                                        Console.WriteLine("# Scheme: {0}", details.SchemeName);
                                        Console.WriteLine("# Merchant Receipt:");
                                        Console.WriteLine(details.GetMerchantReceipt().TrimEnd());
                                        break;
                                    }
                                default:
                                    Console.WriteLine($"# MOTEL POS DOESN'T KNOW WHAT TO DO WITH THIS TX TYPE WHEN IT SUCCEEDS");
                                    break;
                            }
                            break;
                        case Message.SuccessState.Failed:
                            switch (txState.Type)
                            {
                                case TransactionType.Preauth:
                                    Console.WriteLine($"# PREAUTH TRANSACTION FAILED :(");
                                    Console.WriteLine("# Error: {0}", txState.Response.GetError());
                                    Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());
                                    if (txState.Response != null)
                                    {
                                        var purchaseResponse = new PurchaseResponse(txState.Response);
                                        Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                                        Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                                        Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                                        Console.WriteLine("# Customer Receipt:");
                                        Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
                                    }
                                    break;
                                case TransactionType.AccountVerify:
                                    Console.WriteLine($"# ACCOUNT VERIFICATION FAILED :(");
                                    Console.WriteLine("# Error: {0}", txState.Response.GetError());
                                    Console.WriteLine("# Error Detail: {0}", txState.Response.GetErrorDetail());
                                    if (txState.Response != null)
                                    {
                                        var acctVerifyResponse = new AccountVerifyResponse(txState.Response);
                                        var details = acctVerifyResponse.Details;
                                        Console.WriteLine(details.GetCustomerReceipt().TrimEnd());
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"# MOTEL POS DOESN'T KNOW WHAT TO DO WITH THIS TX TYPE WHEN IT FAILS");
                                    break;
                            }
                            break;
                        case Message.SuccessState.Unknown:
                            switch (txState.Type)
                            {
                                case TransactionType.Preauth:
                                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER PREAUTH TRANSACTION WENT THROUGH OR NOT:/");
                                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                                    Console.WriteLine($"# IF YOU CONFIRM THAT THE CUSTOMER PAID, CLOSE THE ORDER.");
                                    Console.WriteLine($"# OTHERWISE, RETRY THE PAYMENT FROM SCRATCH.");
                                    break;
                                case TransactionType.AccountVerify:
                                    Console.WriteLine($"# WE'RE NOT QUITE SURE WHETHER ACCOUNT VERIFICATION WENT THROUGH OR NOT:/");
                                    Console.WriteLine($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                                    Console.WriteLine($"# IF YOU CONFIRM THAT THE CUSTOMER PAID, CLOSE THE ORDER.");
                                    Console.WriteLine($"# OTHERWISE, RETRY THE PAYMENT FROM SCRATCH.");
                                    break;
                                default:
                                    Console.WriteLine($"# MOTEL POS DOESN'T KNOW WHAT TO DO WITH THIS TX TYPE WHEN IT's UNKNOWN");
                                    break;
                            }
                            break;
                    }
                }
            }
            Console.WriteLine("");
        }

        private void PrintActions()
        {
            Console.WriteLine("# ----------- AVAILABLE ACTIONS ------------");

            if (_spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# [acct_verify] - Verify a Customer's account");
                Console.WriteLine("# [preauth_open:10000] - Open a new Preauth for $100.00");
                Console.WriteLine("# [preauth_topup:12345678:5000] - Top-up existing Preauth 12345678 with $50.00");
                Console.WriteLine("# [preauth_topdown:12345678:5000] - Partially cancel existing Preauth 12345678 by $50.00");
                Console.WriteLine("# [preauth_extend:12345678] - Extend existing Preauth 12345678");
                Console.WriteLine("# [preauth_complete:12345678:8000] - Complete Preauth with id 12345678 for $80.00");
                Console.WriteLine("# [preauth_cancel:12345678] - Cancel Preauth with id 12345678");
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
            {
                Console.WriteLine("# [pos_id:MOTEPOS1] - Set the POS ID");
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired || _spi.CurrentStatus == SpiStatus.PairedConnecting)
            {
                Console.WriteLine("# [eftpos_address:10.161.104.104] - Set the EFTPOS ADDRESS");
            }

            if (_spi.CurrentStatus == SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [pair] - Pair with Eftpos");

            if (_spi.CurrentStatus != SpiStatus.Unpaired && _spi.CurrentFlow == SpiFlow.Idle)
                Console.WriteLine("# [unpair] - Unpair and Disconnect");

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
            Console.WriteLine("# CASH ONLY! #");
            Console.WriteLine("# -----------------------------------------");
            Console.WriteLine($"# POS: v{_version} Spi: v{Spi.GetVersion()}");
        }

        private void AcceptUserInput()
        {
            var bye = false;
            while (!bye)
            {
                var input = Console.ReadLine();
                var spInput = input.Split(':');
                string preauthId;
                InitiateTxResult initRes;
                switch (spInput[0].ToLower())
                {
                    case "acct_verify":
                        { }
                        initRes = _spiPreauth.InitiateAccountVerifyTx("actvfy-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"));
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate account verify request: {initRes.Message}. Please Retry.");
                        }
                        break;

                    case "preauth_open":
                        { }
                        initRes = _spiPreauth.InitiateOpenTx("propen-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), int.Parse(spInput[1]));
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
                        break;

                    case "preauth_topup":
                        preauthId = spInput[1];
                        initRes = _spiPreauth.InitiateTopupTx("prtopup-" + preauthId + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), preauthId, int.Parse(spInput[2]));
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
                        break;

                    case "preauth_topdown":
                        preauthId = spInput[1];
                        initRes = _spiPreauth.InitiatePartialCancellationTx("prtopd-" + preauthId + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), preauthId, int.Parse(spInput[2]));
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
                        break;
                    case "preauth_extend":
                        preauthId = spInput[1];
                        initRes = _spiPreauth.InitiateExtendTx("prtopd-" + preauthId + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), preauthId);
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
                        break;
                    case "preauth_cancel":
                        preauthId = spInput[1];
                        initRes = _spiPreauth.InitiateCancelTx("prtopd-" + preauthId + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), preauthId);
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
                        break;

                    case "preauth_complete":
                        preauthId = spInput[1];
                        initRes = _spiPreauth.InitiateCompletionTx("prcomp-" + preauthId + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), preauthId, int.Parse(spInput[2]));
                        if (!initRes.Initiated)
                        {
                            Console.WriteLine($"# Could not initiate preauth request: {initRes.Message}. Please Retry.");
                        }
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
                        };
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
                        };
                        PrintStatusAndActions();
                        Console.Write("> ");
                        break;

                    case "pair":
                        _spi.Pair();
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
                    case "glt":
                        var gltres = _spi.InitiateGetLastTx();
                        Console.WriteLine(gltres.Initiated ? "# GLT Initiated. Will be updated with Progress." : $"# Could not initiate GLT: {gltres.Message}. Please Retry.");
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
                        break;
                }
            }
            Console.WriteLine("# BaBye!");
            if (_spiSecrets != null)
                Console.WriteLine($"{_posId}:{_eftposAddress}:{_spiSecrets.EncKey}:{_spiSecrets.HmacKey}");
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

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("spi");
    }
}