using SPIClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcmePosWF
{
    public partial class FrmActions : Form
    {
        public Spi Spi;
        private FrmMain _frmMain;

        public FrmActions(FrmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        private void DoPurchase()
        {
            var amount = Convert.ToInt32(textAmount.Text);
            var purchase = Spi.InitiatePurchaseTx(RequestIdHelper.Id("prchs"), amount);

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
            var amount = Convert.ToInt32(textAmount.Text);
            var refund = Spi.InitiatePurchaseTx(RequestIdHelper.Id("rfnd"), amount);

            if (refund.Initiated)
            {
                listBoxFlow.Items.Add("# Refund Initiated. Will be updated with Progress.");
            }
            else
            {
                listBoxFlow.Items.Add("# Could not initiate refund: " + refund.Message + ". Please Retry.");
            }
        }

        private void btnAction1_Click(object sender, EventArgs e)
        {
            if (btnAction1.Text == "Confirm Code")
            {
                Spi.PairingConfirmCode();
            }
            else if (btnAction1.Text == "Cancel Pairing")
            {
                Spi.PairingCancel();
                _frmMain.lblStatus.BackColor = Color.Red;
            }
            else if (btnAction1.Text == "Cancel")
            {
                Spi.CancelTransaction();
            }
            else if (btnAction1.Text == "OK")
            {
                Spi.AckFlowEndedAndBackToIdle();
                listBoxFlow.Items.Clear();
                _frmMain.PrintStatusAndActions();
                _frmMain.Enabled = true;
                _frmMain.btnPair.Enabled = true;
                _frmMain.textPosId.Enabled = true;
                _frmMain.textEftposAddress.Enabled = true;
                Hide();
            }
            else if (btnAction1.Text == "Accept Signature")
            {
                Spi.AcceptSignature(true);
            }
            else if (btnAction1.Text == "Retry")
            {
                Spi.AckFlowEndedAndBackToIdle();
                listBoxFlow.Items.Clear();
                if (Spi.CurrentTxFlowState.Type == TransactionType.Purchase)
                {
                    DoPurchase();
                }
                else
                {
                    lblFlowStatus.Text = "Retry by selecting from the options below";
                    _frmMain.PrintStatusAndActions();
                }
            }
            else if (btnAction1.Text == "Purchase")
            {
                DoPurchase();
            }
            else if (btnAction1.Text == "Refund")
            {
                DoRefund();
            }
        }

        private void btnAction2_Click(object sender, EventArgs e)
        {
            if (btnAction2.Text == "Cancel Pairing")
            {
                Spi.PairingCancel();
                _frmMain.lblStatus.BackColor = Color.Red;
            }
            else if (btnAction2.Text == "Decline Signature")
            {
                Spi.AcceptSignature(false);
            }
            else if (btnAction2.Text == "Cancel")
            {
                Spi.AckFlowEndedAndBackToIdle();
                listBoxFlow.Items.Clear();
                _frmMain.PrintStatusAndActions();
                _frmMain.Enabled = true;
                Hide();
            }
        }

        private void btnAction3_Click(object sender, EventArgs e)
        {
            if (btnAction3.Text == "Cancel")
            {
                Spi.CancelTransaction();
            }
        }

        private void FrmActions_Leave(object sender, EventArgs e)
        {
            _frmMain.Enabled = true;
        }

        private void FrmActions_Load(object sender, EventArgs e)
        {
            lblFlowStatus.Text = Spi.CurrentFlow.ToString();
        }
    }
}