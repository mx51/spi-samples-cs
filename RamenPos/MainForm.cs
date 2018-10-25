using System;
using System.Drawing;
using System.Windows.Forms;
using SPIClient;
using SPIClient.Service;
using Message = SPIClient.Message;

namespace RamenPos
{
    public partial class RamenPos : RamenForm
    {
        private const string ApiKey = "RamenPosDeviceAddressApiKey"; // this key needs to be requested from Assembly Payments

        public RamenPos()
        {
            InitializeComponent();
            Start();
        }

        #region Form Controls
        /// <summary>
        /// This will trigger auto address resolution
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SpiClient.SetAutoAddressResolution(AutoAddressEnabled); // trigger auto address 
            SpiClient.SetSerialNumber(SerialNumber); // trigger auto address
        }

        private void chkAutoIpAddress_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = chkAutoAddress.Checked;
            txtAddress.Enabled = !chkAutoAddress.Checked;
            txtAddress.BackColor = chkAutoAddress.Checked ? Color.LightGray : Color.White;
        }

        private void btnPair_Click(object sender, EventArgs e)
        {
            if (!AreControlsValid())
                return;

            SpiClient.SetPosId(PosId);
            SpiClient.SetSerialNumber(SerialNumber);
            SpiClient.SetEftposAddress(EftposAddress);

            SpiClient.Pair();
        }

        private bool AreControlsValid()
        {
            var valid = true;

            AutoAddressEnabled = chkAutoAddress.Checked;
            PosId = txtPosId.Text;
            EftposAddress = txtAddress.Text;
            SerialNumber = txtSerialNumber.Text;

            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(PosId))
            {
                errorProvider.SetError(txtPosId, "Please provide a Pos Id");
                valid = false;
            }

            if (chkAutoAddress.Checked && string.IsNullOrWhiteSpace(SerialNumber))
            {
                errorProvider.SetError(txtSerialNumber, "Please provide a Serial Number");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(EftposAddress))
            {
                errorProvider.SetError(chkAutoAddress, "Please provide an address or enable auto address detection");
                valid = false;
            }

            return valid;
        }
        #endregion

        #region SPI Client
        private void Start()
        {
            SpiClient = new Spi(PosId, SerialNumber, EftposAddress, Secrets);

            SpiClient.DeviceAddressChanged += OnDeviceAddressChanged;
            SpiClient.StatusChanged += OnSpiStatusChanged;
            SpiClient.PairingFlowStateChanged += OnPairingFlowStateChanged;
            SpiClient.SecretsChanged += OnSecretsChanged;
            SpiClient.TxFlowStateChanged += OnTxFlowStateChanged;
            SpiClient.Start();
        }

        private void OnDeviceAddressChanged(object sender, DeviceAddressStatus e)
        {
            if (!string.IsNullOrWhiteSpace(e?.Address))
                txtAddress.Text = e.Address;
        }

        private void OnTxFlowStateChanged(object sender, TransactionFlowState e)
        {
            MessageBox.Show("1");
        }

        private void OnSecretsChanged(object sender, Secrets e)
        {
            MessageBox.Show("2");
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
            MessageBox.Show("3");
        }


        #endregion
    }
}
