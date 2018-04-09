using SPIClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcmePosWF
{
    public partial class FrmMain : Form
    {
        FrmActions frmActions;

        private Spi _spi;
        private string _posId;
        private Secrets _spiSecrets;
        private string _eftposAddress;

        private readonly log4net.ILog log = log4net.LogManager.GetLogger("spi");

        public FrmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmActions = new FrmActions(this);
            lblStatus.BackColor = Color.Red;
            Start();
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

            PrintStatusAndActions();
        }

        private void OnStatusChanged(object sender, SpiStatusEventArgs spiStatus)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                if (_spi.CurrentFlow == SpiFlow.Idle)
                {
                    frmActions.listBoxFlow.Items.Clear();
                }

                PrintStatusAndActions();
                frmActions.Spi = _spi;
                frmActions.Show();
            }));
        }

        private void OnPairingFlowStateChanged(object sender, PairingFlowState pairingFlowState)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                frmActions.listBoxFlow.Items.Clear();
                frmActions.lblFlowMessage.Text = pairingFlowState.Message;

                if (pairingFlowState.ConfirmationCode != "")
                {
                    frmActions.listBoxFlow.Items.Add("# Confirmation Code: " + pairingFlowState.ConfirmationCode);
                }

                PrintStatusAndActions();
                frmActions.Spi = _spi;
                frmActions.Show();
            }));
        }

        private void OnTxFlowStateChanged(object sender, TransactionFlowState txFlowState)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                frmActions.listBoxFlow.Items.Clear();
                frmActions.lblFlowMessage.Text = txFlowState.DisplayMessage;
                frmActions.listBoxFlow.Items.Add("# Id: " + txFlowState.Id);
                frmActions.listBoxFlow.Items.Add("# Type: " + txFlowState.Type);
                frmActions.listBoxFlow.Items.Add("# RequestSent: " + txFlowState.RequestSent);
                frmActions.listBoxFlow.Items.Add("# WaitingForSignature: " + txFlowState.AwaitingSignatureCheck);
                frmActions.listBoxFlow.Items.Add("# Attempting to Cancel : " + txFlowState.AttemptingToCancel);
                frmActions.listBoxFlow.Items.Add("# Finished: " + txFlowState.Finished);
                frmActions.listBoxFlow.Items.Add("# Outcome: " + txFlowState.Success);
                frmActions.listBoxFlow.Items.Add("# Display Message: " + txFlowState.DisplayMessage);

                if (txFlowState.AwaitingSignatureCheck)
                {
                    //We need to print the receipt for the customer to sign.
                    richtextReceipt.Text = richtextReceipt.Text + System.Environment.NewLine + txFlowState.SignatureRequiredMessage.GetMerchantReceipt().TrimEnd();
                }

                //If the transaction is finished, we take some extra steps.
                if (txFlowState.Finished)
                {
                    if (txFlowState.Success == SPIClient.Message.SuccessState.Unknown)
                    {
                        //TH-4T, TH-4N, TH-2T - This is the dge case when we can't be sure what happened to the transaction.
                        //Invite the merchant to look at the last transaction on the EFTPOS using the dicumented shortcuts.
                        //Now offer your merchant user the options to:
                        //A. Retry the transaction from scratch or pay using a different method - If Merchant is confident that tx didn't go through.
                        //B. Override Order as Paid in you POS - If Merchant is confident that payment went through.
                        //C. Cancel out of the order all together - If the customer has left / given up without paying
                        frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                        frmActions.listBoxFlow.Items.Add("# NOT SURE IF WE GOT PAID OR NOT. CHECK LAST TRANSACTION MANUALLY ON EFTPOS!");
                        frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                    }
                    else
                    {
                        //We have a result...
                        switch (txFlowState.Type)
                        {
                            //Depending on what type of transaction it was, we might act diffeently or use different data.
                            case TransactionType.Purchase:
                                if (txFlowState.Success == SPIClient.Message.SuccessState.Success)
                                {
                                    //TH-6A
                                    frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                                    frmActions.listBoxFlow.Items.Add("# HOORAY WE GOT PAID (TH-7A). CLOSE THE ORDER!");
                                    frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                                }
                                else
                                {
                                    //TH-6E
                                    frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                                    frmActions.listBoxFlow.Items.Add("# WE DIDN''T GET PAID. RETRY PAYMENT (TH-5R) OR GIVE UP (TH-5C)!");
                                    frmActions.listBoxFlow.Items.Add("# ##########################################################################");
                                }

                                if (txFlowState.Response != null)
                                {
                                    var purchaseResponse = new PurchaseResponse(txFlowState.Response);
                                    frmActions.listBoxFlow.Items.Add("# Scheme: " + purchaseResponse.SchemeName);
                                    frmActions.listBoxFlow.Items.Add("# Response: " + purchaseResponse.GetResponseText());
                                    frmActions.listBoxFlow.Items.Add("# RRN: " + purchaseResponse.GetRRN());
                                    frmActions.listBoxFlow.Items.Add("# Error: " + txFlowState.Response.GetError());
                                    frmActions.listBoxFlow.Items.Add("# Customer Receipt:");
                                    richtextReceipt.Text = richtextReceipt.Text + System.Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd();
                                }
                                else
                                {
                                    // We did not even get a response, like in the case of a time-out.
                                }
                                break;

                            case TransactionType.Refund:
                                if (txFlowState.Response != null)
                                {
                                    var refundResponse = new RefundResponse(txFlowState.Response);
                                    frmActions.listBoxFlow.Items.Add("# Scheme: " + refundResponse.SchemeName);
                                    frmActions.listBoxFlow.Items.Add("# Response: " + refundResponse.GetResponseText());
                                    frmActions.listBoxFlow.Items.Add("# RRN: " + refundResponse.GetRRN());
                                    frmActions.listBoxFlow.Items.Add("# Error: " + txFlowState.Response.GetError());
                                    frmActions.listBoxFlow.Items.Add("# Customer Receipt:");
                                    richtextReceipt.Text = richtextReceipt.Text + System.Environment.NewLine + refundResponse.GetCustomerReceipt().TrimEnd();
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
                                    frmActions.listBoxFlow.Items.Add("# Response: " + settleResponse.GetResponseText());
                                    frmActions.listBoxFlow.Items.Add("# Error: " + txFlowState.Response.GetError());
                                    frmActions.listBoxFlow.Items.Add("# Merchant Receipt:");
                                    richtextReceipt.Text = richtextReceipt.Text + System.Environment.NewLine + settleResponse.GetReceipt().TrimEnd();
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
                                    frmActions.listBoxFlow.Items.Add("# Checking to see if it matches the $100.00 purchase we did 1 minute ago :)");
                                    var success = this._spi.GltMatch(gltResponse, TransactionType.Purchase, 10000, DateTime.Now.Subtract(TimeSpan.FromMinutes(1)), "MYORDER123");

                                    if (success == SPIClient.Message.SuccessState.Unknown)
                                    {
                                        frmActions.listBoxFlow.Items.Add("# Did not retrieve Expected Transaction.");
                                    }
                                    else
                                    {
                                        frmActions.listBoxFlow.Items.Add("# Tx Matched Expected Purchase Request.");
                                        frmActions.listBoxFlow.Items.Add("# Result: " + success);

                                        var purchaseResponse = new PurchaseResponse(txFlowState.Response);
                                        frmActions.listBoxFlow.Items.Add("# Scheme: " + purchaseResponse.SchemeName);
                                        frmActions.listBoxFlow.Items.Add("# Response: " + purchaseResponse.GetResponseText());
                                        frmActions.listBoxFlow.Items.Add("# RRN: " + purchaseResponse.GetRRN());
                                        frmActions.listBoxFlow.Items.Add("# Error: " + txFlowState.Response.GetError());
                                        frmActions.listBoxFlow.Items.Add("# Customer Receipt:");
                                        richtextReceipt.Text = richtextReceipt.Text + System.Environment.NewLine + purchaseResponse.GetMerchantReceipt().TrimEnd();
                                    }
                                }
                                else
                                {
                                    // We did not even get a response, like in the case of a time-out.
                                }
                                break;
                        }
                    }
                }

                PrintStatusAndActions();
                frmActions.Spi = _spi;
                frmActions.Show();
            }));
        }

        private void OnSecretsChanged(object sender, Secrets newSecrets)
        {
            _spiSecrets = newSecrets;
        }

        public void PrintStatusAndActions()
        {
            lblStatus.Text = _spi.CurrentStatus + ":" + _spi.CurrentFlow;

            switch (_spi.CurrentStatus)
            {
                case SpiStatus.Unpaired:
                    switch (_spi.CurrentFlow)
                    {
                        case SpiFlow.Idle:
                            frmActions.lblFlowMessage.Text = "Unpaired";
                            break;

                        case SpiFlow.Pairing:
                            if (_spi.CurrentPairingFlowState.AwaitingCheckFromPos)
                            {
                                frmActions.btnAction1.Visible = true;
                                frmActions.btnAction1.Text = "Confirm Code";
                                frmActions.btnAction2.Visible = true;
                                frmActions.btnAction2.Text = "Cancel Pairing";
                                frmActions.btnAction3.Visible = false;
                                frmActions.lblAmount.Visible = false;
                                frmActions.textAmount.Visible = false;
                            }
                            else if (!_spi.CurrentPairingFlowState.Finished)
                            {
                                frmActions.btnAction1.Visible = true;
                                frmActions.btnAction1.Text = "Cancel Pairing";
                                frmActions.btnAction2.Visible = false;
                                frmActions.btnAction3.Visible = false;
                                frmActions.lblAmount.Visible = false;
                                frmActions.textAmount.Visible = false;
                            }
                            else
                            {
                                frmActions.btnAction1.Visible = true;
                                frmActions.btnAction1.Text = "OK";
                                frmActions.btnAction2.Visible = false;
                                frmActions.btnAction3.Visible = false;
                                frmActions.lblAmount.Visible = false;
                                frmActions.textAmount.Visible = false;
                            }
                            break;

                        case SpiFlow.Transaction:
                            break;

                        default:
                            frmActions.btnAction1.Visible = true;
                            frmActions.btnAction1.Text = "OK";
                            frmActions.btnAction2.Visible = false;
                            frmActions.btnAction3.Visible = false;
                            frmActions.lblAmount.Visible = false;
                            frmActions.textAmount.Visible = false;
                            frmActions.listBoxFlow.Items.Clear();
                            frmActions.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + _spi.CurrentFlow);
                            break;
                    }
                    break;

                case SpiStatus.PairedConnecting:
                    break;

                case SpiStatus.PairedConnected:
                    switch (_spi.CurrentFlow)
                    {
                        case SpiFlow.Idle:
                            btnPair.Text = "UnPair";
                            pnlActions.Visible = true;
                            lblStatus.BackColor = Color.Green;
                            break;

                        case SpiFlow.Transaction:
                            if (_spi.CurrentTxFlowState.AwaitingSignatureCheck)
                            {
                                frmActions.btnAction1.Visible = true;
                                frmActions.btnAction1.Text = "Accept Signature";
                                frmActions.btnAction2.Visible = true;
                                frmActions.btnAction2.Text = "Decline Signature";
                                frmActions.btnAction3.Visible = true;
                                frmActions.btnAction3.Text = "Cancel";
                                frmActions.lblAmount.Visible = false;
                                frmActions.textAmount.Visible = false;
                            }
                            else if (!_spi.CurrentTxFlowState.Finished)
                            {
                                frmActions.btnAction1.Visible = true;
                                frmActions.btnAction1.Text = "Cancel";
                                frmActions.btnAction2.Visible = false;
                                frmActions.btnAction3.Visible = false;
                                frmActions.lblAmount.Visible = false;
                                frmActions.textAmount.Visible = false;
                            }
                            else
                            {
                                switch (_spi.CurrentTxFlowState.Success)
                                {
                                    case SPIClient.Message.SuccessState.Success:
                                        frmActions.btnAction1.Visible = true;
                                        frmActions.btnAction1.Text = "OK";
                                        frmActions.btnAction2.Visible = false;
                                        frmActions.btnAction3.Visible = false;
                                        frmActions.lblAmount.Visible = false;
                                        frmActions.textAmount.Visible = false;
                                        break;

                                    case SPIClient.Message.SuccessState.Failed:
                                        frmActions.btnAction1.Visible = true;
                                        frmActions.btnAction1.Text = "Retry";
                                        frmActions.btnAction2.Visible = true;
                                        frmActions.btnAction2.Text = "Cancel";
                                        frmActions.btnAction3.Visible = false;
                                        frmActions.lblAmount.Visible = false;
                                        frmActions.textAmount.Visible = false;
                                        break;

                                    default:
                                        frmActions.btnAction1.Visible = true;
                                        frmActions.btnAction1.Text = "OK";
                                        frmActions.btnAction2.Visible = false;
                                        frmActions.btnAction3.Visible = false;
                                        frmActions.lblAmount.Visible = false;
                                        frmActions.textAmount.Visible = false;
                                        break;
                                }
                            }
                            break;

                        case SpiFlow.Pairing:
                            frmActions.btnAction1.Visible = true;
                            frmActions.btnAction1.Text = "OK";
                            frmActions.btnAction2.Visible = false;
                            frmActions.btnAction3.Visible = false;
                            frmActions.lblAmount.Visible = false;
                            frmActions.textAmount.Visible = false;
                            break;

                        default:
                            frmActions.btnAction1.Visible = true;
                            frmActions.btnAction1.Text = "OK";
                            frmActions.btnAction2.Visible = false;
                            frmActions.btnAction3.Visible = false;
                            frmActions.lblAmount.Visible = false;
                            frmActions.textAmount.Visible = false;
                            frmActions.listBoxFlow.Items.Clear();
                            frmActions.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + _spi.CurrentFlow);
                            break;
                    }
                    break;

                default:
                    frmActions.btnAction1.Visible = true;
                    frmActions.btnAction1.Text = "OK";
                    frmActions.btnAction2.Visible = false;
                    frmActions.btnAction3.Visible = false;
                    frmActions.lblAmount.Visible = false;
                    frmActions.textAmount.Visible = false;
                    frmActions.listBoxFlow.Items.Clear();
                    frmActions.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + _spi.CurrentFlow);
                    break;
            }
        }

        private void LoadPersistedState()
        {
            _posId = "CSUIPOS";
            _eftposAddress = "emulator-prod.herokuapp.com";
            // _eftposAddress = "10.161.106.54";

            textPosId.Text = _posId;
            textEftposAddress.Text = _eftposAddress;
        }

        private void btnPair_Click(object sender, EventArgs e)
        {
            if (btnPair.Text == "Pair")
            {
                _posId = textPosId.Text;
                _eftposAddress = textEftposAddress.Text;
                _spi.SetPosId(_posId);
                _spi.SetEftposAddress(_eftposAddress);
                textPosId.Enabled = false;
                textEftposAddress.Enabled = false;
                lblStatus.BackColor = Color.Yellow;
                btnPair.Enabled = false;
                Enabled = false;
                _spi.Pair();
            }
            else if (btnPair.Text == "UnPair")
            {
                _spi.Unpair();
                btnPair.Text = "Pair";
                pnlActions.Visible = false;
                lblStatus.BackColor = Color.Red;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmActions.Dispose();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            frmActions.lblFlowMessage.Text = "Please enter the amount you would like to purchase for in cents";
            frmActions.btnAction1.Visible = true;
            frmActions.btnAction1.Text = "Purchase";
            frmActions.btnAction2.Visible = true;
            frmActions.btnAction2.Text = "Cancel";
            frmActions.btnAction3.Visible = false;
            frmActions.lblAmount.Visible = true;
            frmActions.textAmount.Visible = true;
            Enabled = false;
            frmActions.Spi = _spi;
            frmActions.Show();
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            frmActions.lblFlowMessage.Text = "Please enter the amount you would like to refund for in cents";
            frmActions.btnAction1.Visible = true;
            frmActions.btnAction1.Text = "Refund";
            frmActions.btnAction2.Visible = true;
            frmActions.btnAction2.Text = "Cancel";
            frmActions.btnAction3.Visible = false;
            frmActions.lblAmount.Visible = true;
            frmActions.textAmount.Visible = true;
            Enabled = false;
            frmActions.Spi = _spi;
            frmActions.Show();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmActions.btnAction1.Visible = true;
            frmActions.btnAction1.Text = "Cancel";
            frmActions.btnAction2.Visible = false;
            frmActions.btnAction3.Visible = false;
            frmActions.lblAmount.Visible = false;
            frmActions.textAmount.Visible = false;
            Enabled = false;

            var settleres = _spi.InitiateSettleTx(RequestIdHelper.Id("settle"));
            frmActions.listBoxFlow.Items.Add(settleres.Initiated ? "# Settle Initiated. Will be updated with Progress." : "# Could not initiate settlement: " + settleres.Message + ". Please Retry.");
            frmActions.Spi = _spi;
            frmActions.Show();
        }

        private void btnGetLast_Click(object sender, EventArgs e)
        {
            var sres = _spi.InitiateGetLastTx();
            frmActions.listBoxFlow.Items.Add(sres.Initiated ? "# GLT Initiated. Will be updated with Progress." : "# Could not initiate GLT: " + sres.Message + ". Please Retry.");
            frmActions.Spi = _spi;
            frmActions.Show();
        }
    }
}
