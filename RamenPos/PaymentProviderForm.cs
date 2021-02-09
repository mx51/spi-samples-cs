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
            var tenants = await Spi.GetAvailableTenants("123", "123", "AU");
            lbPaymentProviders.DataSource = tenants.Data;
            lbPaymentProviders.DisplayMember = "Name";
            lbPaymentProviders.ValueMember = "Code";
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
            else
            {
                TenantCode = lbPaymentProviders.SelectedValue.ToString();
                TenantName = lbPaymentProviders.Text;
            }

            this.Close();
        }
    }
}
