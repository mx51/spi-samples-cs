using System;
using SPIClient;
using System.Windows.Forms;

namespace RamenPos
{
    public partial class PaymentProviderForm : RamenForm
    {
        public PaymentProviderForm()
        {
            InitializeComponent();
        }
        
        private void PaymentProviderForm_Load(object sender, EventArgs e)
        {
            LoadPaymentProviders();
        }

        private async void LoadPaymentProviders()
        {
            var tenants = await Spi.GetAvailableTenants(PosVendorId, ApiKey, "AU");
           
            if (tenants != null)
            {
                lbPaymentProviders.DataSource = tenants.Data;
                lbPaymentProviders.DisplayMember = "Name";
                lbPaymentProviders.ValueMember = "Code";
                lbPaymentProviders.ClearSelected();
            }
        }

        private void rbOtherPaymentProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOtherPaymentProvider.Checked)
            {
                txtOtherPaymentProvider.Enabled = true;
                txtOtherPaymentProvider.Text = "";
                lbPaymentProviders.SelectedIndex = -1;
                rbOtherPaymentProvider.Checked = true;
            }
        }

        private void lbPaymentProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbOtherPaymentProvider.Checked = false;
            txtOtherPaymentProvider.Enabled = false;
            txtOtherPaymentProvider.Text = "Other";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (rbOtherPaymentProvider.Checked)
            {
                TenantCode = txtOtherPaymentProvider.Text;
                TenantName = "Other";
            }
            else if (lbPaymentProviders.SelectedIndex > 0)
            {
                TenantCode = lbPaymentProviders.SelectedValue.ToString();
                TenantName = lbPaymentProviders.Text;
            }
            else
            {
                MessageBox.Show("Please select a payment provider or enter in other value", "Payment Provider Missing");
                return;
            }

            this.Close();
        }
    }
}
