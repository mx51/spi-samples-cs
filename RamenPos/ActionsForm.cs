using SPIClient;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RamenPos
{
    public partial class ActionsForm : RamenForm
    {
        public ActionsForm()
        {
            InitializeComponent();
        }

        #region Form Controls

        private void FrmActions_Load(object sender, EventArgs e)
        {
            ActionsForm = this;
        }

        private void btnAction1_Click(object sender, EventArgs e)
        {
            switch (btnAction1.Text)
            {
                case ButtonCaption.CancelPairing:
                    btnAction1.Enabled = false;
                    SpiClient.PairingCancel();
                    TransactionForm.lblStatus.BackColor = Color.Red;
                    break;
                case ButtonCaption.Cancel:
                    btnAction1.Enabled = false;
                    SpiClient.CancelTransaction();
                    break;
                case ButtonCaption.OK:
                    SpiClient.AckFlowEndedAndBackToIdle();
                    MainForm.SpiStatusAndActions();
                    MainForm.Enabled = true;
                    TransactionForm.Enabled = true;
                    Hide();
                    break;
                case ButtonCaption.OKUnpaired:
                    SpiClient.Unpair();
                    SpiClient.AckFlowEndedAndBackToIdle();
                    MainForm.btnMain.Text = ButtonCaption.Pair;
                    TransactionForm.lblStatus.BackColor = Color.Red;
                    MainForm.grpSecrets.Enabled = false; // secrets have been destroyed
                    MainForm.btnPaymentProvider.Enabled = true;
                    MainForm.chkSecrets.Checked = false;
                    MainForm.grpSettings.Enabled = true;
                    MainForm.Enabled = true;
                    MainForm.txtPosId.Text = "";
                    MainForm.txtAddress.Text = "";
                    TransactionForm.Hide();
                    MainForm.Show();
                    Hide();
                    break;
                case ButtonCaption.AcceptSignature:
                    SpiClient.AcceptSignature(true);
                    break;
                case ButtonCaption.Retry:
                    SpiClient.AckFlowEndedAndBackToIdle();
                    listBoxFlow.Items.Clear();
                    switch (SpiClient.CurrentTxFlowState.Type)
                    {
                        case TransactionType.Purchase:
                            DoPurchase();
                            break;
                        case TransactionType.Refund:
                            DoRefund();
                            break;
                        case TransactionType.MOTO:
                            DoMoto();
                            break;
                        case TransactionType.CashoutOnly:
                            DoCashout();
                            break;
                        case TransactionType.Settle:
                            DoSettlement();
                            break;
                        default:
                            lblFlowMessage.Text = "Retry by selecting from the options below";
                            MainForm.SpiStatusAndActions();
                            break;
                    }
                    break;
                case ButtonCaption.Purchase:
                    DoPurchase();
                    break;
                case ButtonCaption.Refund:
                    DoRefund();
                    break;
                case ButtonCaption.MOTO:
                    DoMoto();
                    break;
                case ButtonCaption.CashOut:
                    DoCashout();
                    break;
                case ButtonCaption.Recovery:
                    DoRecovery();
                    break;
                case ButtonCaption.Settlement:
                    DoSettlement();
                    break;
                case ButtonCaption.GetTx:
                    DoGetTransaction();
                    break;
                case ButtonCaption.Set:
                    DoHeaderFooter();
                    break;
                case ButtonCaption.Print:
                    SpiClient.PrintReport(txtAction1.Text.Trim(), SanitizePrintText(txtAction2.Text));
                    break;
                default:
                    break;
            }
        }

        private void btnAction2_Click(object sender, EventArgs e)
        {
            switch (btnAction2.Text)
            {
                case ButtonCaption.CancelPairing:
                    SpiClient.PairingCancel();
                    break;
                case ButtonCaption.DeclineSignature:
                    SpiClient.AcceptSignature(false);
                    break;
                case ButtonCaption.UnknownOverrideAsPaid:
                    SpiClient.AckFlowEndedAndBackToIdle();
                    listBoxFlow.Items.Clear();
                    MainForm.SpiStatusAndActions();
                    TransactionForm.Enabled = true;
                    Hide();
                    break;
                case ButtonCaption.Cancel:
                    SpiClient.AckFlowEndedAndBackToIdle();
                    listBoxFlow.Items.Clear();
                    MainForm.SpiStatusAndActions();
                    TransactionForm.Enabled = true;
                    Hide();
                    break;
                default:
                    break;
            }
        }

        private void btnAction3_Click(object sender, EventArgs e)
        {
            switch (btnAction3.Text)
            {
                case ButtonCaption.Cancel:
                    SpiClient.CancelTransaction();
                    break;
                case ButtonCaption.UnknownCancel:
                    SpiClient.AckFlowEndedAndBackToIdle();
                    listBoxFlow.Items.Clear();
                    MainForm.SpiStatusAndActions();
                    TransactionForm.Enabled = true;
                    Hide();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Transactions

        private void DoPurchase()
        {
            int.TryParse(txtAction1.Text, out int amount);
            int.TryParse(txtAction2.Text, out int tipAmount);
            int.TryParse(txtAction3.Text, out int cashoutAmount);
            int.TryParse(txtAction4.Text, out int surchargeAmount);

            var purchase = SpiClient.InitiatePurchaseTxV2("prchs-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), amount, tipAmount, cashoutAmount, cboxAction1.Checked, Options, surchargeAmount);

            if (purchase.Initiated)
            {
                listBoxFlow.Items.Add("# Purchase Initiated. Will be updated with Progress.");
            }
            else
            {
                listBoxFlow.Items.Add("# Could not initiate purchase: " + purchase.Message + ". Please Retry.");
            }
        }

        private void DoRefund()
        {
            int.TryParse(txtAction1.Text, out int amount);
            var refund = SpiClient.InitiateRefundTx("rfnd-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), amount, cboxAction1.Checked, Options);

            if (refund.Initiated)
            {
                listBoxFlow.Items.Add("# Refund Initiated. Will be updated with Progress.");
            }
            else
            {
                listBoxFlow.Items.Add("# Could not initiate refund: " + refund.Message + ". Please Retry.");
            }
        }

        private void DoMoto()
        {
            int.TryParse(txtAction1.Text, out int amount);
            int.TryParse(txtAction2.Text, out int surchargeAmount);
            var motoRes = SpiClient.InitiateMotoPurchaseTx("moto-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), amount, surchargeAmount, cboxAction1.Checked, Options);

            if (motoRes.Initiated)
            {
                listBoxFlow.Items.Add("# Moto Initiated. Will be updated with Progress.");
            }
            else
            {
                Console.WriteLine($"# Could not initiate moto: {motoRes.Message}. Please Retry.");
            }
        }

        private void DoCashout()
        {
            int.TryParse(txtAction1.Text, out int amount);
            int.TryParse(txtAction2.Text, out int surchargeAmount);
            var coRes = SpiClient.InitiateCashoutOnlyTx("cshout-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"), amount, surchargeAmount, Options);

            if (coRes.Initiated)
            {
                listBoxFlow.Items.Add("# Cashout Initiated. Will be updated with Progress.");
            }
            else
            {
                Console.WriteLine($"# Could not initiate cashout: {coRes.Message}. Please Retry.");
            }
        }

        private void DoRecovery()
        {
            if (txtAction1.Text == "")
            {
                MessageBox.Show("Recovery", "Reference Id is missing", MessageBoxButtons.OK);
                return;
            }

            var coRes = SpiClient.InitiateRecovery(txtAction1.Text.Trim(), TransactionType.Purchase);

            if (coRes.Initiated)
            {
                listBoxFlow.Items.Add("# Recovery Initiated. Will be updated with Progress.");
            }
            else
            {
                Console.WriteLine($"# Could not initiate recovery: {coRes.Message}. Please Retry.");
            }
        }

        private void DoGetTransaction()
        {
            var coRes = SpiClient.InitiateGetTx(txtAction1.Text);

            if (coRes.Initiated)
            {
                listBoxFlow.Items.Add("# Last Transaction Initiated. Will be updated with Progress.");
            }
            else
            {
                Console.WriteLine($"# Could not initiate last transaction: {coRes.Message}. Please Retry.");
            }
        }

        private void DoSettlement()
        {
            var settleRes = SpiClient.InitiateSettleTx(RequestIdHelper.Id("settle"), Options);
            listBoxFlow.Items.Add(settleRes.Initiated ? "# Settle Initiated. Will be updated with Progress." : "# Could not initiate settlement: " + settleRes.Message + ". Please Retry."); 
        }

        private void DoHeaderFooter()
        {
            Options.SetCustomerReceiptHeader(SanitizePrintText(txtAction1.Text));
            Options.SetMerchantReceiptHeader(SanitizePrintText(txtAction1.Text));
            Options.SetCustomerReceiptFooter(SanitizePrintText(txtAction2.Text));
            Options.SetMerchantReceiptFooter(SanitizePrintText(txtAction2.Text));

            ActionsForm.lblFlowMessage.Text = "# --> Receipt Header and Footer is entered";
            MainForm.GetOKActionComponents();
        }

        private string SanitizePrintText(string printText)
        {
            printText = printText.Replace(@"\\emphasis", @"\emphasis");
            printText = printText.Replace(@"\\clear", @"\clear");
            printText = printText.Replace(@"\r\n", Environment.NewLine);
            return printText.Replace(@"\n", Environment.NewLine);
        }

        #endregion

        private void listBoxFlow_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, listBoxFlow.SelectedItem);
        }

        private void ActionsForm_Activated(object sender, EventArgs e)
        {
            MainForm.Enabled = false;
        }
    }
}
