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
        private const string AcquirerCode = "wbc";

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
            if (!AreControlsValid(false))
                return;

            SpiClient.SetTestMode(chkTestMode.Checked);
            SpiClient.SetAutoAddressResolution(AutoAddressEnabled); // trigger auto address 
            SpiClient.SetSerialNumber(SerialNumber); // trigger auto address
        }

        private void chkAutoIpAddress_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = chkAutoAddress.Checked;
            chkTestMode.Checked = chkAutoAddress.Checked;
            chkTestMode.Enabled = chkAutoAddress.Checked;
        }

        private void btnPair_Click(object sender, EventArgs e)
        {
            if (!AreControlsValid(true))
                return;

            SpiClient.SetPosId(PosId);
            SpiClient.SetSerialNumber(SerialNumber);
            SpiClient.SetEftposAddress(EftposAddress);

            SpiClient.Pair();
            this.Enabled = false;
        }

        private bool AreControlsValid(bool isPairing)
        {
            var valid = true;

            AutoAddressEnabled = chkAutoAddress.Checked;
            PosId = txtPosId.Text;
            EftposAddress = txtAddress.Text;
            SerialNumber = txtSerialNumber.Text;

            errorProvider.Clear();

            if (isPairing && string.IsNullOrWhiteSpace(EftposAddress))
            {
                errorProvider.SetError(txtAddress, "Please enable auto address resolution or enter a device address");
                valid = false;
            }

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
            
            // initialise auto ip
            SpiClient.SetAcquirerCode(AcquirerCode);
            SpiClient.SetDeviceApiKey(ApiKey);

            SpiClient.Start();
        }

        private void OnDeviceAddressChanged(object sender, DeviceAddressStatus e)
        {
            if (!string.IsNullOrWhiteSpace(e?.Address))
            {
                txtAddress.Text = e.Address;
                MessageBox.Show($@"Device Address has been updated to {e.Address}", @"Device Address Updated");
            }
        }

        private void OnTxFlowStateChanged(object sender, TransactionFlowState e)
        {
            SpiFlowInfo();
            SpiPairingStatus();
        }

        private void OnSecretsChanged(object sender, Secrets e)
        {
            SpiFlowInfo();
            SpiPairingStatus();
        }

        private void OnPairingFlowStateChanged(object sender, PairingFlowState e)
        {
            SpiActions();
            SpiPairingStatus();
        }

        private void OnSpiStatusChanged(object sender, SpiStatusEventArgs e)
        {
            SpiFlowInfo();
            SpiActions();
            SpiPairingStatus();
        }

        private void SpiFlowInfo()
        {
            switch (SpiClient.CurrentFlow)
            {
                case SpiFlow.Pairing:
                    var pairingState = SpiClient.CurrentPairingFlowState;
                    System.Diagnostics.Debug.WriteLine("### PAIRING PROCESS UPDATE ###");
                    System.Diagnostics.Debug.WriteLine($"# {pairingState.Message}");
                    System.Diagnostics.Debug.WriteLine($"# Finished? {pairingState.Finished}");
                    System.Diagnostics.Debug.WriteLine($"# Successful? {pairingState.Successful}");
                    System.Diagnostics.Debug.WriteLine($"# Confirmation Code: {pairingState.ConfirmationCode}");
                    System.Diagnostics.Debug.WriteLine(
                        $"# Waiting Confirm from Eftpos? {pairingState.AwaitingCheckFromEftpos}");
                    System.Diagnostics.Debug.WriteLine(
                        $"# Waiting Confirm from POS? {pairingState.AwaitingCheckFromPos}");
                    break;
                case SpiFlow.Transaction:
                    break;
                case SpiFlow.Idle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SpiActions()
        {
            if (SpiClient.CurrentFlow != SpiFlow.Pairing)
            {
                Invoke(new Action(() => this.Enabled = true));
                return;
            }

            // checking for confirmation code
            if (SpiClient.CurrentPairingFlowState.AwaitingCheckFromPos)
            {
                var result = MessageBox.Show($@"Confirm Pairing Code : {SpiClient.CurrentPairingFlowState.ConfirmationCode}", @"Please confirm pairing",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                    SpiClient.PairingConfirmCode();
                else
                    SpiClient.PairingCancel();
            }

            // paired
            if (SpiClient.CurrentStatus == SpiStatus.PairedConnecting && SpiClient.CurrentFlow == SpiFlow.Idle)
            {
                Invoke(new Action(() => this.Enabled = true));
                btnPair.Text = "Unpair";
            }
        }

        private void SpiPairingStatus()
        {
            Invoke(new Action(() => lblPairingStatus.Text = SpiClient.CurrentStatus.ToString()));
        }
        #endregion
    }
}
