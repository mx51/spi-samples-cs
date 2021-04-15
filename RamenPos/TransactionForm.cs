using SPIClient;
using System;
using System.Windows.Forms;

namespace RamenPos
{
    public partial class TransactionForm : RamenForm
    {
        public TransactionForm()
        {
            InitializeComponent();
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            TransactionForm = this;
            MainForm.grpSecrets.Enabled = false;
            MainForm.grpSettings.Enabled = false;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActionsForm.Dispose();
            MainForm.Dispose();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the amount you would like to purchase for in cents";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Purchase;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.Amount;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "0";
            ActionsForm.lblAction2.Visible = true;
            ActionsForm.lblAction2.Text = LabelCaption.TipAmount;
            ActionsForm.txtAction2.Visible = true;
            ActionsForm.txtAction2.Text = "0";
            ActionsForm.lblAction3.Visible = true;
            ActionsForm.lblAction3.Text = LabelCaption.CashoutAmount;
            ActionsForm.txtAction3.Visible = true;
            ActionsForm.txtAction3.Text = "0";
            ActionsForm.lblAction4.Visible = true;
            ActionsForm.lblAction4.Text = LabelCaption.SurchargeAmount;
            ActionsForm.txtAction4.Visible = true;
            ActionsForm.txtAction4.Text = "0";
            ActionsForm.cboxAction1.Visible = true;
            ActionsForm.cboxAction1.Text = CheckboxCaption.PromptForCashout;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the amount you would like to refund for in cents";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Refund;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.Amount;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "0";
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = true;
            ActionsForm.cboxAction1.Text = CheckboxCaption.SuppressMerhcantPassword;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please click the Settlement button to initiate settlement";
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Settlement;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = false;
            ActionsForm.txtAction1.Visible = false;
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnGetTransaction_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the PosRefId for the transaction you wish to retrieve";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.GetTx;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.PosRefId;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "";
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.transactionsToolStripMenuItem.Visible = true;
            MainForm.grpSecrets.Enabled = false;
            MainForm.grpSettings.Enabled = false;
            MainForm.btnMain.Enabled = true;
            MainForm.btnPaymentProvider.Enabled = false;
            Hide();
            MainForm.Show();
        }

        private void btnMoto_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the amount you would like to do moto for in cents";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.MOTO;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.Amount;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "0";
            ActionsForm.lblAction2.Visible = true;
            ActionsForm.lblAction2.Text = LabelCaption.SurchargeAmount;
            ActionsForm.txtAction2.Visible = true;
            ActionsForm.txtAction2.Text = "0";
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = true;
            ActionsForm.cboxAction1.Text = CheckboxCaption.SuppressMerhcantPassword;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnCashout_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the amount you would like to cash out for in cents";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.CashOut;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.Amount;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "0";
            ActionsForm.lblAction2.Visible = true;
            ActionsForm.lblAction2.Text = LabelCaption.SurchargeAmount;
            ActionsForm.txtAction2.Visible = true;
            ActionsForm.txtAction2.Text = "0";
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnSettleEnq_Click(object sender, EventArgs e)
        {
            ActionsForm.btnAction1.Visible = false;
            ActionsForm.btnAction2.Visible = false;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = false;
            ActionsForm.txtAction1.Visible = false;
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;

            var senqRes = SpiClient.InitiateSettlementEnquiry(RequestIdHelper.Id("stlenq"), Options);
            ActionsForm.listBoxFlow.Items.Add(senqRes.Initiated ? "# Settle Initiated. Will be updated with Progress." : "# Could not initiate settlement: " + senqRes.Message + ". Please Retry.");
            ActionsForm.Show();
        }

        private void btnRecovery_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the reference id you would like to recovery";
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Recovery;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.PosRefId;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "";
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void cboxReceiptFrom_CheckedChanged(object sender, EventArgs e)
        {
            SpiClient.Config.PromptForCustomerCopyOnEftpos = cboxReceiptFrom.Checked;
        }

        private void cboxSignFromEftpos_CheckedChanged(object sender, EventArgs e)
        {
            SpiClient.Config.SignatureFlowOnEftpos = cboxSignFromEftpos.Checked;
        }

        private void cboPrintMerchantCopy_CheckedChanged(object sender, EventArgs e)
        {
            SpiClient.Config.PrintMerchantCopy = cboPrintMerchantCopy.Checked;
        }

        private void btnHeaderFooter_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the receipt header and footer you would like to print";
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Set;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.ReceiptHeader;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "";
            ActionsForm.lblAction2.Visible = true;
            ActionsForm.lblAction2.Text = LabelCaption.ReceiptFooter;
            ActionsForm.txtAction2.Visible = true;
            ActionsForm.txtAction2.Text = "";
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnFreeformReceipt_Click(object sender, EventArgs e)
        {
            ActionsForm.lblFlowMessage.Text = "Please enter the print text and key you would like to print receipt";
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.Print;
            ActionsForm.btnAction2.Visible = true;
            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
            ActionsForm.btnAction3.Visible = false;
            ActionsForm.lblAction1.Visible = true;
            ActionsForm.lblAction1.Text = LabelCaption.Key;
            ActionsForm.txtAction1.Visible = true;
            ActionsForm.txtAction1.Text = "";
            ActionsForm.lblAction2.Visible = true;
            ActionsForm.lblAction2.Text = LabelCaption.PrintText;
            ActionsForm.txtAction2.Visible = true;
            ActionsForm.txtAction2.Text = "";
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
            TransactionForm.Enabled = false;
            ActionsForm.Show();
        }

        private void btnTerminalStatus_Click(object sender, EventArgs e)
        {
            SpiClient.GetTerminalStatus();
        }

        private void btnTerminalSettings_Click(object sender, EventArgs e)
        {
            SpiClient.GetTerminalConfiguration();
        }

        public void secretsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionsForm.listBoxFlow.Items.Clear();

            if (Secrets != null)
            {
                ActionsForm.listBoxFlow.Items.Add("Pos Id:");
                ActionsForm.listBoxFlow.Items.Add(PosId);
                ActionsForm.listBoxFlow.Items.Add("Eftpos Address:");
                ActionsForm.listBoxFlow.Items.Add(EftposAddress);
                ActionsForm.listBoxFlow.Items.Add("Secrets:");
                ActionsForm.listBoxFlow.Items.Add(Secrets.EncKey + ":" + Secrets.HmacKey);
            }
            else
            {
                ActionsForm.listBoxFlow.Items.Add("I have no secrets!");
            }

            MainForm.GetOKActionComponents();
            Enabled = false;
            ActionsForm.Show();
        }
    }
}
