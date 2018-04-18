using System;
using SPIClient;

namespace AcmePosCs
{
    /// <summary>
    /// NOTE: THIS PROJECT USES THE 2.0.x of the SPI Client Library
    ///  
    /// This is your POS. To integrate with SPI, you need to instantiate a Spi object
    /// and interact with it.
    /// 
    /// Primarily you need to implement 3 things.
    /// 1. Settings Screen
    /// 2. Pairing Flow Screen
    /// 3. Transaction Flow screen
    /// 
    /// </summary>
    internal class AcmePos
    {
        private static void Main(string[] args)
        {
            log.Info("Starting AcmePos...");
            var myPos = new AcmePos();
            myPos.Start();
        }

        private void Start()
        {
            // This is where you load your state - like the pos_id, eftpos address and secrets - from your file system or database
            LoadPersistedState();

            #region Spi Setup
            // This is how you instantiate Spi.
            _spi = new Spi(_posId, _eftposAddress, _spiSecrets); // It is ok to not have the secrets yet to start with.
            _spi.StatusChanged += OnStatusChanged; // Called when Status changes between Unpaired, PairedConnected and PairedConnecting
            _spi.SecretsChanged += OnSecretsChanged; // Called when Secrets are set or changed or voided.
            _spi.PairingFlowStateChanged += OnPairingFlowStateChanged; // Called throughout to pairing process to update us with progress
            _spi.TxFlowStateChanged += OnTxFlowStateChanged; // Called throughout to transaction process to update us with progress
            _spi.Start();
            #endregion
            
            // And Now we just accept user input and display to the user what is happening.
            
            Console.Clear();
            Console.WriteLine("# ");
            Console.WriteLine("# Howdy and welcome to ACME-POS! My name is {0}.", _posId);
            Console.WriteLine("# I integrate with SPI.");
            Console.WriteLine("# ");
            
            PrintStatusAndActions();
            AcceptUserInput();
        }

        /// <summary>
        /// Called when we received a Status Update i.e. Unpaired/PairedConnecting/PairedConnected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="spiStatus"></param>
        private void OnStatusChanged(object sender, SpiStatusEventArgs spiStatus)
        {
            if (_spi.CurrentFlow == SpiFlow.Idle) Console.Clear();
            
//            if (spiStatus.SpiStatus == SpiStatus.PairedConnecting && _spi.CurrentFlow == SpiFlow.Idle) _spi.InitiateGetLastTx();
            
            Console.WriteLine("# ------- STATUS UPDATE -----------");
            PrintStatusAndActions();
            
        }
        
        /// <summary>
        /// Called during the pairing process to let us know how it's going.
        /// We just update our screen with the information, and provide relevant Actions to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pairingFlowState"></param>
        private void OnPairingFlowStateChanged(object sender, PairingFlowState pairingFlowState)
        {
            Console.Clear();
            Console.WriteLine("# --------- PAIRING FLOW UPDATE -----------");
            Console.WriteLine("# Message: {0}", pairingFlowState.Message);
            if (!string.IsNullOrEmpty(pairingFlowState.ConfirmationCode))
            {
                Console.WriteLine("# Confirmation Code: {0}", pairingFlowState.ConfirmationCode);    
            }
            PrintStatusAndActions();
        }

        /// <summary>
        /// Called during a transaction to let us know how it's going.
        /// We just update our screen with the information, and provide relevant Actions to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="txFlowState"></param>
        private void OnTxFlowStateChanged(object sender, TransactionFlowState txFlowState)
        {
            Console.Clear();
            Console.WriteLine("# --------- TRANSACTION FLOW UPDATE -----------");
            Console.WriteLine("# Id: {0}", txFlowState.Id);
            Console.WriteLine("# Type: {0}", txFlowState.Type);
            Console.WriteLine("# RequestSent: {0}", txFlowState.RequestSent);
            Console.WriteLine("# WaitingForSignature: {0}", txFlowState.AwaitingSignatureCheck);
            Console.WriteLine("# Attempting to Cancel : {0}", txFlowState.AttemptingToCancel);
            Console.WriteLine("# Finished: {0}", txFlowState.Finished);
            Console.WriteLine("# Success: {0}", txFlowState.Success);
            Console.WriteLine("# Display Message: {0}", txFlowState.DisplayMessage);

            if (txFlowState.AwaitingSignatureCheck)
            {
                // We need to print the receipt for the customer to sign.
                Console.WriteLine(txFlowState.SignatureRequiredMessage.GetMerchantReceipt().TrimEnd());
            }
            
            // If the transaction is finished, we take some extra steps.
            if (txFlowState.Finished)
            {
                if (txFlowState.Success == Message.SuccessState.Unknown)
                {
                    // TH-4T, TH-4N, TH-2T - This is the dge case when we can't be sure what happened to the transaction.
                    // Invite the merchant to look at the last transaction on the EFTPOS using the dicumented shortcuts.
                    // Now offer your merchant user the options to:
                    // A. Retry the transaction from scrtatch or pay using a different method - If Merchant is confident that tx didn't go through.
                    // B. Override Order as Paid in you POS - If Merchant is confident that payment went through.
                    // C. Cancel out of the order all together - If the customer has left / given up without paying 
                    Console.WriteLine("# NOT SURE IF WE GOT PAID OR NOT. CHECK LAST TRANSACTION MANUALLY ON EFTPOS!");
                }
                else{
                    // We have a result...
                    switch (txFlowState.Type)
                    {
                        // Depending on what type of transaction it was, we might act diffeently or use different data.
                        case TransactionType.Purchase:
                            if (txFlowState.Response != null)
                            {
                                var purchaseResponse = new PurchaseResponse(txFlowState.Response);
                                Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                                Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                                Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                                Console.WriteLine("# Error: {0}", txFlowState.Response.GetError());
                                Console.WriteLine("# Customer Receipt:");
                                Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());
                            }
                            else
                            {
                                // We did not even get a response, like in the case of a time-out.
                            }
                            if (txFlowState.Success == Message.SuccessState.Success)
                            {
                                // TH-6A
                                Console.WriteLine("# HOORAY WE GOT PAID (TH-7A). CLOSE THE ORDER!");
                            }
                            else
                            {
                                // TH-6E
                                Console.WriteLine("# WE DIDN'T GET PAID. RETRY PAYMENT (TH-5R) OR GIVE UP (TH-5C)!");
                            }
                            break;
                        case TransactionType.Refund:
                            if (txFlowState.Response != null)
                            {
                                var refundResponse = new RefundResponse(txFlowState.Response);
                                Console.WriteLine("# Scheme: {0}", refundResponse.SchemeName);
                                Console.WriteLine("# Response: {0}", refundResponse.GetResponseText());
                                Console.WriteLine("# RRN: {0}", refundResponse.GetRRN());
                                Console.WriteLine("# Error: {0}", txFlowState.Response.GetError());
                                Console.WriteLine("# Customer Receipt:");
                                Console.WriteLine(refundResponse.GetCustomerReceipt().TrimEnd());
                            }
                            else
                            {
                                // We did not even get a response, like in the case of a time-out.
                            }
                            break;
                        case TransactionType.Settle:
                            if (txFlowState.Response != null)
                            {
                                var settleResponse = new Settlement(txFlowState.Response);
                                Console.WriteLine("# Response: {0}", settleResponse.GetResponseText());
                                Console.WriteLine("# Error: {0}", txFlowState.Response.GetError());
                                Console.WriteLine("# Merchant Receipt:");
                                Console.WriteLine(settleResponse.GetReceipt().TrimEnd());
                            }
                            else
                            {
                                // We did not even get a response, like in the case of a time-out.
                            }
                            break;
                        case TransactionType.GetLastTransaction:
                            if (txFlowState.Response != null)
                            {
                                var gltResponse = new GetLastTransactionResponse(txFlowState.Response);
                                var success = _spi.GltMatch(gltResponse, TransactionType.Purchase, 10000, DateTime.Now.Subtract(TimeSpan.FromMinutes(1)), "MYORDER123");
                                
                                if (success == Message.SuccessState.Unknown)
                                {
                                    Console.WriteLine("# Did not retrieve Expected Transaction.");
                                }
                                else
                                {
                                    Console.WriteLine("# Tx Matched Expected Purchase Request.");
                                    Console.WriteLine("# Result: {0}", success);
                                    var purchaseResponse = new PurchaseResponse(txFlowState.Response);
                                    Console.WriteLine("# Scheme: {0}", purchaseResponse.SchemeName);
                                    Console.WriteLine("# Response: {0}", purchaseResponse.GetResponseText());
                                    Console.WriteLine("# RRN: {0}", purchaseResponse.GetRRN());
                                    Console.WriteLine("# Error: {0}", txFlowState.Response.GetError());
                                    Console.WriteLine("# Customer Receipt:");
                                    Console.WriteLine(purchaseResponse.GetCustomerReceipt().TrimEnd());                                    
                                }
                            }
                            else
                            {
                                // We did not even get a response, like in the case of a time-out.
                                Console.WriteLine("# Could Not Retrieve Last Transaction.");
                            }
                            break;
                            
                        default:
                            break;
                    }
                }
            }
            
            // Let's show the user what options he has at this stage.
            PrintStatusAndActions();
        }
        
        /// <summary>
        /// Called when Secrets are set or changed or voided.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newSecrets"></param>
        private void OnSecretsChanged(object sender, Secrets newSecrets)
        {
            _spiSecrets = newSecrets;
            if (_spiSecrets != null)
            {
                Console.WriteLine("\n\n\n# --------- I GOT NEW SECRETS -----------");
                Console.WriteLine("# ---------- PERSIST THEM SAFELY ----------");
                Console.WriteLine("# {0}:{1}", _spiSecrets.EncKey, _spiSecrets.HmacKey);
                Console.WriteLine("# -----------------------------------------");
            }
            else
            {
                Console.WriteLine("\n\n\n# --------- THE SECRETS HAVE BEEN VOIDED -----------");
                Console.WriteLine("# ---------- CONSIDER ME UNPAIRED ----------");
                Console.WriteLine("# -----------------------------------------");
            }
        }
        
        /// <summary>
        /// This method prints the current Spi Status and Flow
        /// and lists available actions to the user.
        /// </summary>
        private void PrintStatusAndActions()
        {
            Console.WriteLine("# ----------- AVAILABLE ACTIONS ------------");
            
            // Available Actions depend on the current status (Unpaired/PairedConnecting/PairedConnected)
            switch (_spi.CurrentStatus)
            {
                case SpiStatus.Unpaired: //Unpaired...
                    switch (_spi.CurrentFlow)
                    {
                        case SpiFlow.Idle: // Unpaired, Idle
                            Console.WriteLine("# [pos_id:MYPOSNAME] - sets your POS instance ID");
                            Console.WriteLine("# [eftpos_address:10.10.10.10] - sets IP address of target EFTPOS");
                            Console.WriteLine("# [pair] - start pairing");
                            break;
                            
                        case SpiFlow.Pairing: // Unpaired, PairingFlow
                            var pairingState = _spi.CurrentPairingFlowState;
                            if (pairingState.AwaitingCheckFromPos)
                            {
                                Console.WriteLine("# [pair_confirm] - confirm the code matches");
                            }
                            if (!pairingState.Finished)
                            {
                                Console.WriteLine("# [pair_cancel] - cancel pairing process");
                            }
                            else
                            {
                                Console.WriteLine("# [ok] - acknowledge");
                            }
                            break;
                            
                        case SpiFlow.Transaction: // Unpaired, TransactionFlow - Should never be the case!
                        default:
                            Console.WriteLine("# .. Unexpected Flow .. {0}", _spi.CurrentFlow);
                            break;
                    }
                    break;
                  
                case SpiStatus.PairedConnected:
                    switch (_spi.CurrentFlow)
                    {
                        case SpiFlow.Idle: // Paired, Idle
                            Console.WriteLine("# [purchase:1981] - initiate a payment of $19.81");
                            Console.WriteLine("# [refund:1891] - initiate a refund of $18.91");
                            Console.WriteLine("# [settle] - Initiate Settlement");
                            Console.WriteLine("# [unpair] - unpair and disconnect");
                            break;
                        case SpiFlow.Transaction: // Paired, Transaction
                            if (_spi.CurrentTxFlowState.AwaitingSignatureCheck)
                            {
                                Console.WriteLine("# [tx_sign_accept] - Accept Signature");
                                Console.WriteLine("# [tx_sign_decline] - Decline Signature");
                            }
                            if (!_spi.CurrentTxFlowState.Finished)
                            {
                                Console.WriteLine("# [tx_cancel] - Attempt to Cancel Transaction");
                            }
                            else
                            {
                                Console.WriteLine("# [ok] - acknowledge");
                            }
                            break;
                        case SpiFlow.Pairing: // Paired, Pairing - we have just finished the pairing flow. OK to ack.
                            Console.WriteLine("# [ok] - acknowledge");
                            break;
                        default:
                            Console.WriteLine("# .. Unexpected Flow .. {0}", _spi.CurrentFlow);
                            break;
                    }
                    break;

                case SpiStatus.PairedConnecting: // This is still considered as a Paired kind of state, but...
                    // .. we give user the option of changing IP address, just in case the EFTPOS got a new one in the meanwhile
                    Console.WriteLine("# [eftpos_address:10.161.110.247] - change IP address of target EFTPOS");
                    // .. but otherwise we give the same options as PairedConnected
                    goto case SpiStatus.PairedConnected;

                default:
                    Console.WriteLine("# .. Unexpected State .. {0}", _spi.CurrentStatus);
                    break;
            }
            Console.WriteLine("# [status] - reprint buttons/status");
            Console.WriteLine("# [bye] - exit");
            Console.WriteLine();
            Console.WriteLine("# --------------- STATUS ------------------");            
            Console.WriteLine("# {0} <--> {1}", _posId, _eftposAddress);
            Console.WriteLine("# {0}:{1} ",  _spi.CurrentStatus,  _spi.CurrentFlow);
            Console.WriteLine("# -----------------------------------------");
            Console.Write("> ");
        }

        private void AcceptUserInput()
        {
            var bye = false;
            while (!bye)
            {
                var input = Console.ReadLine();
                var spInput = input.Split(':');
                switch (spInput[0])
                {
                    case "pos_id":
                        _posId = spInput[1];
                        _spi.SetPosId(_posId);
                        Console.Clear();
                        PrintStatusAndActions();
                        break;
                    case "eftpos_address":
                        _eftposAddress = spInput[1];
                        _spi.SetEftposAddress(_eftposAddress);
                        Console.Clear();
                        PrintStatusAndActions();
                        break;
                    case "pair":
                        _spi.Pair();
                        break;
                    case "pair_confirm":
                        _spi.PairingConfirmCode();
                        break;
                    case "pair_cancel":
                        _spi.PairingCancel();
                        break;
                    case "unpair":
                        _spi.Unpair();
                        break;
                    case "purchase":
                        var pres = _spi.InitiatePurchaseTx(RequestIdHelper.Id("prchs"), int.Parse(spInput[1]));
                        Console.WriteLine(pres.Initiated ? "# Purchase Initiated. Will be updated with Progress." : $"# Could not initiate purchase: {pres.Message}. Please Retry.");
                        break;
                    case "refund":
                        var rres = _spi.InitiateRefundTx(RequestIdHelper.Id("rfnd"), int.Parse(spInput[1]));
                        Console.WriteLine(rres.Initiated ? "# Refund Initiated. Will be updated with Progress." : $"# Could not initiate refund: {rres.Message}. Please Retry.");
                        break;
                    case "settle":
                        var sres = _spi.InitiateSettleTx(RequestIdHelper.Id("settle"));
                        Console.WriteLine(sres.Initiated ? "# Settle Initiated. Will be updated with Progress." : $"# Could not initiate settle: {sres.Message}. Please Retry.");
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
                    case "ok":
                        _spi.AckFlowEndedAndBackToIdle();
                        Console.Clear();
                        PrintStatusAndActions();
                        break;
                    case "status":
                        Console.Clear();
                        PrintStatusAndActions();
                        break;
                    case "bye":
                        bye = true;
                        break;
                    case "":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("# I don't understand. Sorry.");
                        PrintStatusAndActions();
                        break;
                }
            }
            _spi.Dispose();
            Console.WriteLine("# BaBye!");
        }        
        
        /// <summary>
        /// Just a little function to Load State from command line arguments that looks like
        /// PosId:PinPadAddress:EncKey:HmacKey
        /// You will need to persist/load these yourself in your own database/filesystem.
        /// </summary>
        private void LoadPersistedState()
        {
            // Let's read cmd line arguments.
            var cmdArgs = Environment.GetCommandLineArgs();
            if (cmdArgs.Length <= 1) return; // nothing passed in

            // we were given something, at least PosId and PinPad address...
            var argSplit = cmdArgs[1].Split(':');
            _posId = argSplit[0];
            if (argSplit.Length > 1) _eftposAddress = argSplit[1];

            // Let's see if we were given existing secrets as well.
            if (argSplit.Length > 2)
            {
                _spiSecrets = new Secrets(argSplit[2], argSplit[3]);
            }
        }

        private string _posId = "ACMEPOS3";
        private Secrets _spiSecrets;
        private string _eftposAddress = "emulator-prod.herokuapp.com";

        private Spi _spi;
        
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("spi");

        
    }
}