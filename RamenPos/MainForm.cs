using System;
using System.Drawing;
using System.Windows.Forms;
using SPIClient;
using SPIClient.Service;

namespace RamenPos
{
    public partial class RamenPos : RamenForm
    {
        private const string ApiKey = "RamenPosDeviceIpApiKey"; // this key needs to be requested from Assembly Payments

        public RamenPos()
        {
            InitializeComponent();
            InitialiseSpi();
        }

        private DeviceIpAddressRequest DeviceIpAddressRequest()
        {
            return new DeviceIpAddressRequest
            {
                ApiKey = ApiKey,
                SerialNumber = SerialNumber
            };
        }

        #region User Controls
        private void btnPair_Click(object sender, EventArgs e)
        {
            if (!ValidateControls())
                return;

            // spi already initiated, set values
            SpiClient.SetEftposAddress(EftposAddress);
            SpiClient.SetPosId(PosId);

            SpiClient.Pair();
        }

        private void btnResolveIpAddress_Click(object sender, EventArgs e)
        {
            if (!ValidateControls())
                return;

            if (!chkAutoIpAddress.Checked)
                return;

            GetDeviceIpAddress(DeviceIpAddressRequest());
        }

        private bool ValidateControls()
        {
            var valid = true;

            PosId = txtPosId.Text;
            EftposAddress = txtIpAddress.Text;
            SerialNumber = txtSerialNumber.Text;

            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(PosId))
            {
                errorProvider.SetError(txtPosId, "Please provide a Pos Id");
                valid = false;
            }

            if (chkAutoIpAddress.Checked && string.IsNullOrWhiteSpace(SerialNumber))
            {
                errorProvider.SetError(txtSerialNumber, "Please provide a Serial Number");
                valid = false;
            }

            if (!chkAutoIpAddress.Checked && string.IsNullOrWhiteSpace(EftposAddress))
            {
                errorProvider.SetError(chkAutoIpAddress, "Please provide a IP Address or enable Auto IP detection");
                valid = false;
            }

            return valid;
        }
        #endregion

        private void chkAutoIpAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoIpAddress.Checked)
            {
                txtIpAddress.Enabled = false;
                txtIpAddress.BackColor = Color.LightGray;
            }
            else
            {
                txtIpAddress.ResetText();
                txtIpAddress.Enabled = true;
                txtIpAddress.BackColor = Color.White;
            }
        }

        #region SPI Client
        private void InitialiseSpi()
        {
            SpiClient = new Spi(PosId, EftposAddress, Secrets, DeviceIpAddressRequest());

            SpiClient.DeviceIpAddressChanged += OnDeviceIpAddressChanged;
            SpiClient.StatusChanged += OnSpiStatusChanged;
            SpiClient.PairingFlowStateChanged += OnPairingFlowStateChanged;
            SpiClient.SecretsChanged += OnSecretsChanged;
            SpiClient.TxFlowStateChanged += OnTxFlowStateChanged;
            SpiClient.Start();
        }

        private void GetDeviceIpAddress(DeviceIpAddressRequest deviceIpAddressRequest)
        {
            if (!chkAutoIpAddress.Checked)
                return;

            SpiClient.AutoIpResolutionEnable = true;
            SpiClient.GetDeviceIpAddress(deviceIpAddressRequest);
            SpiClient.SetEftposAddress(EftposAddress);
        }

        private void OnTxFlowStateChanged(object sender, TransactionFlowState e)
        {
        }

        private void OnSecretsChanged(object sender, Secrets e)
        {
        }

        private void OnPairingFlowStateChanged(object sender, PairingFlowState e)
        {
            if (SpiClient.CurrentFlow != SpiFlow.Pairing || !SpiClient.CurrentPairingFlowState.AwaitingCheckFromPos)
                return;

            var result = MessageBox.Show(SpiClient.CurrentPairingFlowState.ConfirmationCode, @"Confirmation Code",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                SpiClient.PairingConfirmCode();
            else
                SpiClient.PairingCancel();
        }

        private void OnSpiStatusChanged(object sender, SpiStatusEventArgs e)
        {
        }

        private void OnDeviceIpAddressChanged(object sender, DeviceIpAddressStatus e)
        {
            txtIpAddress.Text = e?.Ip;
            EftposAddress = e?.Ip;
        }

        #endregion

    }
}
