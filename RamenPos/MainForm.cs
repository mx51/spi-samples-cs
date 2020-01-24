using SPIClient;
using SPIClient.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RamenPos
{
    public partial class MainForm : RamenForm
    {
        private const string ApiKey = "RamenPosDeviceAddressApiKey"; // this key needs to be requested from Assembly Payments
        private const string AcquirerCode = "wbc";
        public bool IsStarted;
        private Dictionary<string, string> secretsDict = new Dictionary<string, string>();
        private bool IsReconnect;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Form Controls

        private void RamenPos_Load(object sender, EventArgs e)
        {
            MainForm = this;
            TransactionForm = new TransactionForm();
            ActionsForm = new ActionsForm();
            btnMain.Text = ButtonCaption.Pair;

            if (File.Exists("Secrets.bin"))
            {
                secretsDict = ReadFromBinaryFile<Dictionary<string, string>>("Secrets.bin");

                if (secretsDict?.Count > 0)
                {
                    txtSecrets.Text = secretsDict["Secrets"];
                    txtAddress.Text = secretsDict["EftposAddress"];
                    txtPosId.Text = secretsDict["PosId"];
                    btnMain.Enabled = true;
                    cboxSecrets.Checked = true;
                    IsReconnect = true;
                }
            }

            IsStarted = true;
        }

        private bool AreControlsValid(bool isPairing)
        {
            var valid = true;

            PosId = txtPosId.Text;
            EftposAddress = txtAddress.Text;
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

            return valid;
        }

        private bool AreControlsValidForSecrets()
        {
            PosId = txtPosId.Text;
            EftposAddress = txtAddress.Text;

            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(EftposAddress))
            {
                errorProvider.SetError(txtAddress, "Please provide a Eftpos address");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PosId))
            {
                errorProvider.SetError(txtPosId, "Please provide a Pos Id");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSecrets.Text))
            {
                errorProvider.SetError(txtSecrets, "Please provide Secrets");
                return false;
            }

            if (txtSecrets.Text.Split(':').Length < 2)
            {
                errorProvider.SetError(txtSecrets, "Please provide a valid Secrets");
                return false;
            }

            return true;
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm.grpSecrets.Enabled = false;
            MainForm.grpSettings.Enabled = false;
            TransactionForm.Show();
        }

        private void cboxSecrets_CheckedChanged(object sender, EventArgs e)
        {
            txtSecrets.Enabled = cboxSecrets.Checked;

            if (cboxSecrets.Checked)
            {
                btnMain.Text = ButtonCaption.Start;
            }
            else
            {
                btnMain.Text = ButtonCaption.Pair;
                txtSecrets.Text = "";
                errorProvider.Clear();
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            switch (btnMain.Text)
            {
                case ButtonCaption.Start:
                    if (!AreControlsValidForSecrets())
                        return;

                    IsStarted = false;
                    Secrets = new Secrets(txtSecrets.Text.Split(':')[0], txtSecrets.Text.Split(':')[1]);
                    Start();
                    break;
                case ButtonCaption.Pair:
                    if (!AreControlsValid(true))
                        return;

                    PosId = txtPosId.Text;
                    EftposAddress = txtAddress.Text;
                    Start();

                    if (!SpiClient.Pair())
                        errorProvider.SetError(btnMain, "Pairing failed, please check Spi logs");
                    break;
                case ButtonCaption.UnPair:
                    txtPosId.Text = "";
                    txtAddress.Text = "";

                    SpiClient.Unpair();
                    break;
                default:
                    break;
            }
        }

        private static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public void SaveSecrets()
        {
            if (secretsDict.ContainsKey("PosId"))
            {
                secretsDict["PosId"] = PosId;
            }
            else
            {
                secretsDict.Add("PosId", PosId);
            }

            if (secretsDict.ContainsKey("EftposAddress"))
            {
                secretsDict["EftposAddress"] = EftposAddress;
            }
            else
            {
                secretsDict.Add("EftposAddress", EftposAddress);
            }

            if (secretsDict.ContainsKey("SerialNumber"))
            {
                secretsDict["SerialNumber"] = SerialNumber;
            }
            else
            {
                secretsDict.Add("SerialNumber", SerialNumber);
            }

            if (secretsDict.ContainsKey("AutoAddressEnabled"))
            {
                secretsDict["AutoAddressEnabled"] = AutoAddressEnabled.ToString();
            }
            else
            {
                secretsDict.Add("AutoAddressEnabled", AutoAddressEnabled.ToString());
            }

            if (secretsDict.ContainsKey("Secrets"))
            {
                secretsDict["Secrets"] = Secrets.EncKey + ":" + Secrets.HmacKey;
            }
            else
            {
                secretsDict.Add("Secrets", Secrets.EncKey + ":" + Secrets.HmacKey);
            }

            WriteToBinaryFile("Secrets.bin", secretsDict, false);
        }

        #endregion

        #region SPI Client
        internal void Start()
        {

            SpiClient = new Spi(PosId, EftposAddress, Secrets);
            SpiClient.SetPosInfo("Sample_PoS", "2.7");
            Options = new TransactionOptions();

            SpiClient.DeviceAddressChanged += OnDeviceAddressChanged;
            SpiClient.StatusChanged += OnSpiStatusChanged;
            SpiClient.PairingFlowStateChanged += OnPairingFlowStateChanged;
            SpiClient.SecretsChanged += OnSecretsChanged;
            SpiClient.TxFlowStateChanged += OnTxFlowStateChanged;

            SpiClient.PrintingResponse = HandlePrintingResponse;
            SpiClient.TerminalStatusResponse = HandleTerminalStatusResponse;
            SpiClient.TerminalConfigurationResponse = HandleTerminalConfigurationResponse;
            SpiClient.BatteryLevelChanged = HandleBatteryLevelChanged;
            SpiClient.TransactionUpdateMessage = HandleTransactionUpdate;

            SpiClient.SetAcquirerCode(AcquirerCode);
            SpiClient.SetDeviceApiKey(ApiKey);
            SpiClient.SetTestMode(true);

            try
            {
                SpiClient.Start();

            }
            catch (Exception e)
            {
                MessageBox.Show($@"SPI check failed: {e.Message}", @"Please ensure you followed all the configuration steps on your machine",
    MessageBoxButtons.OK);
            }

            if (!IsStarted)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    SpiStatusAndActions();
                }));
            }
        }

        private void OnDeviceAddressChanged(object sender, DeviceAddressStatus e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                btnMain.Enabled = false;
                if (e != null)
                {
                    switch (e.DeviceAddressResponseCode)
                    {
                        case DeviceAddressResponseCode.SUCCESS:
                            txtAddress.Text = e.Address;
                            btnMain.Enabled = true;
                            MessageBox.Show($@"Address has been updated to {e.Address}", "Address updated");
                            break;
                        case DeviceAddressResponseCode.INVALID_SERIAL_NUMBER:
                            txtAddress.Text = "";
                            MessageBox.Show("The serial number is invalid", "Serial Number Invalid");
                            break;
                        case DeviceAddressResponseCode.DEVICE_SERVICE_ERROR:
                            txtAddress.Text = "";
                            MessageBox.Show("Cannot retrieve address for serial number, please try again", "Device address error");
                            break;
                        case DeviceAddressResponseCode.ADDRESS_NOT_CHANGED:
                            btnMain.Enabled = true;
                            MessageBox.Show("The address have not changed!", "Address Has Not Changed");
                            break;
                        case DeviceAddressResponseCode.SERIAL_NUMBER_NOT_CHANGED:
                            btnMain.Enabled = true;
                            MessageBox.Show("The serial number have not changed!", "Serial Number not Changed");
                            break;
                        default:
                            break;
                    }
                }
            }));
        }

        private void OnTxFlowStateChanged(object sender, TransactionFlowState e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                SpiStatusAndActions();
            }));
        }

        private void OnSecretsChanged(object sender, Secrets e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Secrets = e;
                TransactionForm.secretsToolStripMenuItem_Click(sender, e);
                SpiStatusAndActions();
            }));
        }

        private void OnPairingFlowStateChanged(object sender, PairingFlowState pairingFlowState)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                ActionsForm.lblFlowMessage.Text = pairingFlowState.Message;

                if (pairingFlowState.ConfirmationCode != "")
                {
                    ActionsForm.listBoxFlow.Items.Add("# Confirmation Code: " + pairingFlowState.ConfirmationCode);
                }

                SpiStatusAndActions();
            }));
        }

        private void OnSpiStatusChanged(object sender, SpiStatusEventArgs spiStatus)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                ActionsForm.lblFlowMessage.Text = "It's trying to connect";
                SpiStatusAndActions();
            }));
        }

        private void HandlePrintingResponse(SPIClient.Message message)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                ActionsForm.listBoxFlow.Items.Clear();
                var printingResponse = new PrintingResponse(message);

                if (printingResponse.IsSuccess())
                {
                    ActionsForm.lblFlowMessage.Text = "# --> Printing Response: Printing Receipt successful";
                }
                else
                {
                    ActionsForm.lblFlowMessage.Text = "# --> Printing Response:  Printing Receipt failed: reason = " + printingResponse.GetErrorReason() + ", detail = " + printingResponse.GetErrorDetail();
                }

                SpiClient.AckFlowEndedAndBackToIdle();
                GetOKActionComponents();
                ActionsForm.Show();
            }));
        }

        private void HandleTerminalStatusResponse(SPIClient.Message message)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                ActionsForm.listBoxFlow.Items.Clear();
                ActionsForm.lblFlowMessage.Text = "# --> Terminal Status Response Successful";
                var terminalStatusResponse = new TerminalStatusResponse(message);
                ActionsForm.listBoxFlow.Items.Add("# Terminal Status Response #");
                ActionsForm.listBoxFlow.Items.Add("# Status: " + terminalStatusResponse.GetStatus());
                ActionsForm.listBoxFlow.Items.Add("# Battery Level: " + terminalStatusResponse.GetBatteryLevel().Replace("d", "") + "%");
                ActionsForm.listBoxFlow.Items.Add("# Charging: " + terminalStatusResponse.IsCharging());
                SpiClient.AckFlowEndedAndBackToIdle();
                GetOKActionComponents();
                ActionsForm.Show();
            }));
        }

        private void HandleTerminalConfigurationResponse(SPIClient.Message message)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                ActionsForm.listBoxFlow.Items.Clear();
                ActionsForm.lblFlowMessage.Text = "# --> Terminal Configuration Response Successful";
                var terminalConfigurationResponse = new TerminalConfigurationResponse(message);
                ActionsForm.listBoxFlow.Items.Add("# Terminal Configuration Response #");
                ActionsForm.listBoxFlow.Items.Add("# Comms Selected: " + terminalConfigurationResponse.GetCommsSelected());
                ActionsForm.listBoxFlow.Items.Add("# Merchant Id: " + terminalConfigurationResponse.GetMerchantId());
                ActionsForm.listBoxFlow.Items.Add("# PA Version: " + terminalConfigurationResponse.GetPAVersion());
                ActionsForm.listBoxFlow.Items.Add("# Payment Inbterface Version: " + terminalConfigurationResponse.GetPaymentInterfaceVersion());
                ActionsForm.listBoxFlow.Items.Add("# Plugin Version: " + terminalConfigurationResponse.GetPluginVersion());
                ActionsForm.listBoxFlow.Items.Add("# Serial Number: " + terminalConfigurationResponse.GetSerialNumber());
                ActionsForm.listBoxFlow.Items.Add("# Terminal Id: " + terminalConfigurationResponse.GetTerminalId());
                ActionsForm.listBoxFlow.Items.Add("# Terminal Model: " + terminalConfigurationResponse.GetTerminalModel());
                SpiClient.AckFlowEndedAndBackToIdle();
                GetOKActionComponents();
                ActionsForm.Show();
            }));
        }

        private void HandleBatteryLevelChanged(SPIClient.Message message)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                if (!ActionsForm.Visible)
                {
                    ActionsForm.listBoxFlow.Items.Clear();
                    ActionsForm.lblFlowMessage.Text = "# --> Battery Level Changed Successful";
                    var terminalBattery = new TerminalBattery(message);
                    ActionsForm.listBoxFlow.Items.Add("# Battery Level Changed #");
                    ActionsForm.listBoxFlow.Items.Add("# Battery Level: " + terminalBattery.BatteryLevel.Replace("d", "") + "%");
                    SpiClient.AckFlowEndedAndBackToIdle();
                    ActionsForm.Show();
                }
            }));
        }

        private void HandleTransactionUpdate(SPIClient.Message message)
        {
            const string txUpdateText = "# Transaction Update:";

            this.Invoke(new MethodInvoker(delegate ()
            {
                if (ActionsForm.Visible)
                {
                    var txUpdate = new TransactionUpdate(message);

                    ActionsForm.listBoxFlow.Items.Add(txUpdateText + txUpdate.DisplayMessageText);
                }
            }));
        }

        internal void SpiStatusAndActions()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                SpiFlowInfo();
                SpiActions();
                SpiPairingStatus();
                ActionsForm.Show();
                ActionsForm.BringToFront();
            }));
        }

        private void SpiActions()
        {
            ActionsForm.lblFlowStatus.Text = SpiClient.CurrentFlow.ToString();
            TransactionForm.lblStatus.Text = SpiClient.CurrentStatus + ":" + SpiClient.CurrentFlow;

            switch (SpiClient.CurrentStatus)
            {
                case SpiStatus.Unpaired:
                    switch (SpiClient.CurrentFlow)
                    {
                        case SpiFlow.Idle:
                            ActionsForm.lblFlowMessage.Text = "Unpaired";
                            ActionsForm.btnAction1.Enabled = true;
                            ActionsForm.btnAction1.Visible = true;
                            ActionsForm.btnAction1.Text = ButtonCaption.OKUnpaired;
                            ActionsForm.btnAction2.Visible = false;
                            ActionsForm.btnAction3.Visible = false;
                            transactionsToolStripMenuItem.Visible = false;
                            GetUnvisibleActionComponents();
                            TransactionForm.lblStatus.BackColor = Color.Red;
                            if (File.Exists("Secrets.bin"))
                            {
                                var secretsFile = new FileInfo("Secrets.bin");
                                File.Delete(secretsFile.FullName);
                            }
                            break;

                        case SpiFlow.Pairing:
                            if (SpiClient.CurrentPairingFlowState.AwaitingCheckFromPos)
                            {
                                ActionsForm.btnAction1.Enabled = true;
                                ActionsForm.btnAction1.Visible = true;
                                ActionsForm.btnAction1.Text = ButtonCaption.ConfirmCode;
                                ActionsForm.btnAction2.Visible = true;
                                ActionsForm.btnAction2.Text = ButtonCaption.CancelPairing;
                                ActionsForm.btnAction3.Visible = false;
                                GetUnvisibleActionComponents();
                            }
                            else if (!SpiClient.CurrentPairingFlowState.Finished)
                            {
                                ActionsForm.btnAction1.Visible = true;
                                ActionsForm.btnAction1.Text = ButtonCaption.CancelPairing;
                                ActionsForm.btnAction2.Visible = false;
                                ActionsForm.btnAction3.Visible = false;
                                GetUnvisibleActionComponents();
                            }
                            else
                            {
                                GetOKActionComponents();
                            }
                            break;

                        case SpiFlow.Transaction:
                            ActionsForm.lblFlowMessage.Text = "Unpaired";
                            ActionsForm.btnAction1.Enabled = true;
                            ActionsForm.btnAction1.Visible = true;
                            ActionsForm.btnAction1.Text = ButtonCaption.OKUnpaired;
                            ActionsForm.btnAction2.Visible = false;
                            ActionsForm.btnAction3.Visible = false;
                            transactionsToolStripMenuItem.Visible = false;
                            GetUnvisibleActionComponents();
                            TransactionForm.lblStatus.BackColor = Color.Red;
                            break;

                        default:
                            GetOKActionComponents();
                            ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                            break;
                    }
                    break;

                case SpiStatus.PairedConnecting:
                    switch (SpiClient.CurrentFlow)
                    {
                        case SpiFlow.Idle:
                            btnMain.Text = ButtonCaption.UnPair;
                            TransactionForm.lblStatus.BackColor = Color.Yellow;
                            ActionsForm.lblFlowMessage.Text = "# --> SPI Status Changed: " + SpiClient.CurrentStatus;
                            GetOKActionComponents();
                            break;

                        case SpiFlow.Transaction:
                            if (SpiClient.CurrentTxFlowState.AwaitingSignatureCheck)
                            {
                                ActionsForm.btnAction1.Enabled = true;
                                ActionsForm.btnAction1.Visible = true;
                                ActionsForm.btnAction1.Text = ButtonCaption.AcceptSignature;
                                ActionsForm.btnAction2.Visible = true;
                                ActionsForm.btnAction2.Text = ButtonCaption.DeclineSignature;
                                ActionsForm.btnAction3.Visible = true;
                                ActionsForm.btnAction3.Text = ButtonCaption.Cancel;
                                GetUnvisibleActionComponents();
                            }
                            else if (!SpiClient.CurrentTxFlowState.Finished)
                            {
                                if (SpiClient.CurrentTxFlowState.Type != TransactionType.SettlementEnquiry)
                                {
                                    ActionsForm.btnAction1.Visible = true;
                                    ActionsForm.btnAction1.Text = ButtonCaption.Cancel;
                                }
                                else
                                {
                                    ActionsForm.btnAction1.Visible = false;
                                }

                                ActionsForm.btnAction2.Visible = false;
                                ActionsForm.btnAction3.Visible = false;
                                GetUnvisibleActionComponents();
                            }
                            else
                            {
                                switch (SpiClient.CurrentTxFlowState.Success)
                                {
                                    case SPIClient.Message.SuccessState.Success:
                                        GetOKActionComponents();
                                        break;

                                    case SPIClient.Message.SuccessState.Failed:
                                        if (SpiClient.CurrentTxFlowState.Type != TransactionType.SettlementEnquiry)
                                        {
                                            ActionsForm.btnAction1.Text = ButtonCaption.Retry;
                                            ActionsForm.btnAction2.Visible = true;
                                            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
                                        }
                                        else
                                        {
                                            ActionsForm.btnAction1.Text = ButtonCaption.OK;
                                            ActionsForm.btnAction2.Visible = false;
                                        }

                                        ActionsForm.btnAction1.Enabled = true;
                                        ActionsForm.btnAction1.Visible = true;
                                        ActionsForm.btnAction3.Visible = false;
                                        GetUnvisibleActionComponents();
                                        break;

                                    case SPIClient.Message.SuccessState.Unknown:
                                        //GetOKActionComponents();
                                        ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                                        ActionsForm.btnAction1.Enabled = true;
                                        ActionsForm.btnAction1.Visible = true;
                                        ActionsForm.btnAction1.Text = ButtonCaption.Retry;
                                        ActionsForm.btnAction2.Visible = true;
                                        ActionsForm.btnAction2.Text = ButtonCaption.UnknownOverrideAsPaid;
                                        ActionsForm.btnAction3.Visible = true;
                                        ActionsForm.btnAction3.Text = ButtonCaption.UnknownCancel;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        case SpiFlow.Pairing:
                            GetOKActionComponents();
                            break;

                        default:
                            GetOKActionComponents();
                            ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                            break;

                    }
                    break;

                case SpiStatus.PairedConnected:
                    switch (SpiClient.CurrentFlow)
                    {
                        case SpiFlow.Idle:
                            SaveSecrets();
                            btnMain.Text = ButtonCaption.UnPair;
                            TransactionForm.lblStatus.BackColor = Color.Green;
                            ActionsForm.lblFlowMessage.Text = "# --> SPI Status Changed: " + SpiClient.CurrentStatus;
                            Hide();
                            TransactionForm.Show();

                            if (ActionsForm.btnAction1.Text == ButtonCaption.Retry)
                            {
                                GetOKActionComponents();
                            }
                            break;

                        case SpiFlow.Pairing:
                            GetOKActionComponents();
                            break;

                        case SpiFlow.Transaction:
                            if (SpiClient.CurrentTxFlowState.AwaitingSignatureCheck)
                            {
                                ActionsForm.btnAction1.Enabled = true;
                                ActionsForm.btnAction1.Visible = true;
                                ActionsForm.btnAction1.Text = ButtonCaption.AcceptSignature;
                                ActionsForm.btnAction2.Visible = true;
                                ActionsForm.btnAction2.Text = ButtonCaption.DeclineSignature;
                                ActionsForm.btnAction3.Visible = true;
                                ActionsForm.btnAction3.Text = ButtonCaption.Cancel;
                                GetUnvisibleActionComponents();
                            }
                            else if (!SpiClient.CurrentTxFlowState.Finished)
                            {
                                if (SpiClient.CurrentTxFlowState.Type != TransactionType.SettlementEnquiry)
                                {
                                    ActionsForm.btnAction1.Visible = true;
                                    ActionsForm.btnAction1.Text = ButtonCaption.Cancel;
                                }
                                else
                                {
                                    ActionsForm.btnAction1.Visible = false;
                                }

                                ActionsForm.btnAction2.Visible = false;
                                ActionsForm.btnAction3.Visible = false;
                                GetUnvisibleActionComponents();
                            }
                            else
                            {
                                switch (SpiClient.CurrentTxFlowState.Success)
                                {
                                    case SPIClient.Message.SuccessState.Success:
                                        GetOKActionComponents();
                                        break;

                                    case SPIClient.Message.SuccessState.Failed:
                                        if (SpiClient.CurrentTxFlowState.Type != TransactionType.SettlementEnquiry)
                                        {
                                            ActionsForm.btnAction1.Text = ButtonCaption.Retry;
                                            ActionsForm.btnAction2.Visible = true;
                                            ActionsForm.btnAction2.Text = ButtonCaption.Cancel;
                                        }
                                        else
                                        {
                                            ActionsForm.btnAction1.Text = ButtonCaption.OK;
                                            ActionsForm.btnAction2.Visible = false;
                                        }

                                        ActionsForm.btnAction1.Enabled = true;
                                        ActionsForm.btnAction1.Visible = true;
                                        ActionsForm.btnAction3.Visible = false;
                                        GetUnvisibleActionComponents();
                                        break;

                                    case SPIClient.Message.SuccessState.Unknown:
                                        ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                                        ActionsForm.btnAction1.Enabled = true;
                                        ActionsForm.btnAction1.Visible = true;
                                        ActionsForm.btnAction1.Text = ButtonCaption.Retry;
                                        ActionsForm.btnAction2.Visible = true;
                                        ActionsForm.btnAction2.Text = ButtonCaption.UnknownOverrideAsPaid;
                                        ActionsForm.btnAction3.Visible = true;
                                        ActionsForm.btnAction3.Text = ButtonCaption.UnknownCancel;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        default:
                            GetOKActionComponents();
                            ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                            break;
                    }
                    break;

                default:
                    GetOKActionComponents();
                    ActionsForm.listBoxFlow.Items.Add("# .. Unexpected Flow .. " + SpiClient.CurrentFlow);
                    break;
            }
        }

        private void SpiFlowInfo()
        {
            ActionsForm.listBoxFlow.Items.Clear();


            switch (SpiClient.CurrentFlow)
            {
                case SpiFlow.Pairing:
                    var pairingState = SpiClient.CurrentPairingFlowState;
                    ActionsForm.lblFlowMessage.Text = pairingState.Message;
                    ActionsForm.listBoxFlow.Items.Add("### PAIRING PROCESS UPDATE ###");
                    ActionsForm.listBoxFlow.Items.Add($"# {pairingState.Message}");
                    ActionsForm.listBoxFlow.Items.Add($"# Finished? {pairingState.Finished}");
                    ActionsForm.listBoxFlow.Items.Add($"# Successful? {pairingState.Successful}");
                    ActionsForm.listBoxFlow.Items.Add($"# Confirmation Code: {pairingState.ConfirmationCode}");
                    ActionsForm.listBoxFlow.Items.Add($"# Waiting Confirm from Eftpos? {pairingState.AwaitingCheckFromEftpos}");
                    ActionsForm.listBoxFlow.Items.Add($"# Waiting Confirm from POS? {pairingState.AwaitingCheckFromPos}");
                    break;

                case SpiFlow.Transaction:
                    var txState = SpiClient.CurrentTxFlowState;
                    ActionsForm.lblFlowMessage.Text = txState.DisplayMessage;
                    ActionsForm.listBoxFlow.Items.Add("### TX PROCESS UPDATE ###");
                    ActionsForm.listBoxFlow.Items.Add($"# {txState.DisplayMessage}");
                    ActionsForm.listBoxFlow.Items.Add($"# Id: {txState.PosRefId}");
                    ActionsForm.listBoxFlow.Items.Add($"# Type: {txState.Type}");
                    ActionsForm.listBoxFlow.Items.Add($"# Amount: ${txState.AmountCents / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# Waiting For Signature: {txState.AwaitingSignatureCheck}");
                    ActionsForm.listBoxFlow.Items.Add($"# Attempting to Cancel : {txState.AttemptingToCancel}");
                    ActionsForm.listBoxFlow.Items.Add($"# Finished: {txState.Finished}");
                    ActionsForm.listBoxFlow.Items.Add($"# Success: {txState.Success}");
                    ActionsForm.listBoxFlow.Items.Add($"# Last GLT Request Id: {txState.LastGltRequestId}");

                    if (txState.AwaitingSignatureCheck)
                    {
                        // We need to print the receipt for the customer to sign.
                        ActionsForm.listBoxFlow.Items.Add($"# RECEIPT TO PRINT FOR SIGNATURE");
                        TransactionForm.richtextReceipt.Text = TransactionForm.richtextReceipt.Text + Environment.NewLine + txState.SignatureRequiredMessage.GetMerchantReceipt().TrimEnd();
                    }

                    if (txState.AwaitingPhoneForAuth)
                    {
                        ActionsForm.listBoxFlow.Items.Add($"# PHONE FOR AUTH DETAILS:");
                        ActionsForm.listBoxFlow.Items.Add($"# CALL: {txState.PhoneForAuthRequiredMessage.GetPhoneNumber()}");
                        ActionsForm.listBoxFlow.Items.Add($"# QUOTE: Merchant Id: {txState.PhoneForAuthRequiredMessage.GetMerchantId()}");
                    }

                    if (txState.Finished)
                    {
                        ActionsForm.listBoxFlow.Items.Add($"");
                        switch (txState.Type)
                        {
                            case TransactionType.Purchase:
                                HandleFinishedPurchase(txState);
                                break;
                            case TransactionType.Refund:
                                HandleFinishedRefund(txState);
                                break;
                            case TransactionType.CashoutOnly:
                                HandleFinishedCashout(txState);
                                break;
                            case TransactionType.MOTO:
                                HandleFinishedMoto(txState);
                                break;
                            case TransactionType.Settle:
                                HandleFinishedSettle(txState);
                                break;
                            case TransactionType.SettlementEnquiry:
                                HandleFinishedSettlementEnquiry(txState);
                                break;

                            case TransactionType.GetLastTransaction:
                                HandleFinishedGetLastTransaction(txState);
                                break;
                            default:
                                ActionsForm.listBoxFlow.Items.Add($"# CAN'T HANDLE TX TYPE: {txState.Type}");
                                break;
                        }
                    }
                    break;
                case SpiFlow.Idle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedPurchase(TransactionFlowState txState)
        {
            PurchaseResponse purchaseResponse;
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# WOOHOO - WE GOT PAID!");
                    purchaseResponse = new PurchaseResponse(txState.Response);
                    ActionsForm.listBoxFlow.Items.Add($"# Response: {purchaseResponse.GetResponseText()}");
                    ActionsForm.listBoxFlow.Items.Add($"# RRN: { purchaseResponse.GetRRN()}");
                    ActionsForm.listBoxFlow.Items.Add($"# Scheme: {purchaseResponse.SchemeName}");
                    ActionsForm.listBoxFlow.Items.Add($"# PURCHASE: ${purchaseResponse.GetPurchaseAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# TIP: ${purchaseResponse.GetTipAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# SURCHARGE: ${purchaseResponse.GetSurchargeAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# CASHOUT: ${purchaseResponse.GetCashoutAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED NON-CASH AMOUNT: ${purchaseResponse.GetBankNonCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED CASH AMOUNT: ${purchaseResponse.GetBankCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                    TransactionForm.richtextReceipt.Text = !purchaseResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    break;

                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# WE DID NOT GET PAID :(");
                    if (txState.Response != null)
                    {
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error Detail: {txState.Response.GetErrorDetail()}");
                        purchaseResponse = new PurchaseResponse(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {purchaseResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# RRN: {purchaseResponse.GetRRN()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Scheme: {purchaseResponse.SchemeName}");
                        ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                        TransactionForm.richtextReceipt.Text = !purchaseResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# WE'RE NOT QUITE SURE WHETHER WE GOT PAID OR NOT :/");
                    ActionsForm.listBoxFlow.Items.Add($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    ActionsForm.listBoxFlow.Items.Add($"# IF YOU CONFIRM THAT THE CUSTOMER PAID, CLOSE THE ORDER.");
                    ActionsForm.listBoxFlow.Items.Add($"# OTHERWISE, RETRY THE PAYMENT FROM SCRATCH.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedRefund(TransactionFlowState txState)
        {
            RefundResponse refundResponse;
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# REFUND GIVEN- OH WELL!");
                    refundResponse = new RefundResponse(txState.Response);
                    ActionsForm.listBoxFlow.Items.Add($"# Response: {refundResponse.GetResponseText()}");
                    ActionsForm.listBoxFlow.Items.Add($"# RRN: {refundResponse.GetRRN()}");
                    ActionsForm.listBoxFlow.Items.Add($"# Scheme: {refundResponse.SchemeName}");
                    ActionsForm.listBoxFlow.Items.Add($"# REFUNDED AMOUNT: ${refundResponse.GetRefundAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                    TransactionForm.richtextReceipt.Text = !refundResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + refundResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    break;
                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# REFUND FAILED!");
                    if (txState.Response != null)
                    {
                        refundResponse = new RefundResponse(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error Detail: {txState.Response.GetErrorDetail()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Response: { refundResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# RRN: {refundResponse.GetRRN()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Scheme: {refundResponse.SchemeName}");
                        ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                        TransactionForm.richtextReceipt.Text = !refundResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + refundResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# WE'RE NOT QUITE SURE WHETHER THE REFUND WENT THROUGH OR NOT :/");
                    ActionsForm.listBoxFlow.Items.Add($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    ActionsForm.listBoxFlow.Items.Add($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedCashout(TransactionFlowState txState)
        {
            CashoutOnlyResponse cashoutResponse;
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# CASH-OUT SUCCESSFUL - HAND THEM THE CASH!");
                    cashoutResponse = new CashoutOnlyResponse(txState.Response);
                    ActionsForm.listBoxFlow.Items.Add($"# Response: {cashoutResponse.GetResponseText()}");
                    ActionsForm.listBoxFlow.Items.Add($"# RRN: {cashoutResponse.GetRRN()}");
                    ActionsForm.listBoxFlow.Items.Add($"# Scheme: {cashoutResponse.SchemeName}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED NON-CASH AMOUNT: ${cashoutResponse.GetBankNonCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED CASH AMOUNT: ${cashoutResponse.GetBankCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# SURCHARGE: ${cashoutResponse.GetSurchargeAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# CASHOUT: {cashoutResponse.GetCashoutAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                    TransactionForm.richtextReceipt.Text = !cashoutResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + cashoutResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    break;
                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# CASHOUT FAILED!");
                    if (txState.Response != null)
                    {
                        cashoutResponse = new CashoutOnlyResponse(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error Detail: {txState.Response.GetErrorDetail()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {cashoutResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# RRN: {cashoutResponse.GetRRN()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Scheme: {cashoutResponse.SchemeName}");
                        ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                        TransactionForm.richtextReceipt.Text = !cashoutResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + cashoutResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# WE'RE NOT QUITE SURE WHETHER THE CASHOUT WENT THROUGH OR NOT :/");
                    ActionsForm.listBoxFlow.Items.Add($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    ActionsForm.listBoxFlow.Items.Add($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedMoto(TransactionFlowState txState)
        {
            MotoPurchaseResponse motoResponse;
            PurchaseResponse purchaseResponse;
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# WOOHOO - WE GOT MOTO-PAID!");
                    motoResponse = new MotoPurchaseResponse(txState.Response);
                    purchaseResponse = motoResponse.PurchaseResponse;
                    ActionsForm.listBoxFlow.Items.Add($"# Response: {purchaseResponse.GetResponseText()}");
                    ActionsForm.listBoxFlow.Items.Add($"# RRN: {purchaseResponse.GetRRN()}");
                    ActionsForm.listBoxFlow.Items.Add($"# Scheme: {purchaseResponse.SchemeName}");
                    ActionsForm.listBoxFlow.Items.Add($"# Card Entry: {purchaseResponse.GetCardEntry()}");
                    ActionsForm.listBoxFlow.Items.Add($"# PURCHASE: {purchaseResponse.GetPurchaseAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED NON-CASH AMOUNT: {purchaseResponse.GetBankNonCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# BANKED CASH AMOUNT: {purchaseResponse.GetBankCashAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add($"# SURCHARGE: ${purchaseResponse.GetSurchargeAmount() / 100.0}");
                    ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                    TransactionForm.richtextReceipt.Text = !purchaseResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    break;
                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# WE DID NOT GET MOTO-PAID :(");
                    if (txState.Response != null)
                    {
                        motoResponse = new MotoPurchaseResponse(txState.Response);
                        purchaseResponse = motoResponse.PurchaseResponse;
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error Detail: {txState.Response.GetErrorDetail()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {purchaseResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# RRN: {purchaseResponse.GetRRN()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Scheme: {purchaseResponse.SchemeName}");
                        ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                        TransactionForm.richtextReceipt.Text = !purchaseResponse.WasCustomerReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# WE'RE NOT QUITE SURE WHETHER THE MOTO WENT THROUGH OR NOT :/");
                    ActionsForm.listBoxFlow.Items.Add($"# CHECK THE LAST TRANSACTION ON THE EFTPOS ITSELF FROM THE APPROPRIATE MENU ITEM.");
                    ActionsForm.listBoxFlow.Items.Add($"# YOU CAN THE TAKE THE APPROPRIATE ACTION.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedGetLastTransaction(TransactionFlowState txState)
        {
            if (txState.Response != null)
            {
                var gltResponse = new GetLastTransactionResponse(txState.Response);

                // User specified that he intended to retrieve a specific tx by pos_ref_id
                // This is how you can use a handy function to match it.
                var success = SpiClient.GltMatch(gltResponse, ActionsForm.txtAction1.Text.Trim());
                if (success == SPIClient.Message.SuccessState.Unknown)
                {
                    ActionsForm.listBoxFlow.Items.Add("# Did not retrieve Expected Transaction. Here is what we got:");
                }
                else
                {
                    ActionsForm.listBoxFlow.Items.Add("# Tx Matched Expected Purchase Request.");
                }

                var purchaseResponse = new PurchaseResponse(txState.Response);
                ActionsForm.listBoxFlow.Items.Add($"# Scheme: {purchaseResponse.SchemeName}");
                ActionsForm.listBoxFlow.Items.Add($"# Response: {purchaseResponse.GetResponseText()}");
                ActionsForm.listBoxFlow.Items.Add($"# RRN: {purchaseResponse.GetRRN()}");
                ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                ActionsForm.listBoxFlow.Items.Add("# Customer Receipt:");

                TransactionForm.richtextReceipt.Text = TransactionForm.richtextReceipt.Text + Environment.NewLine + purchaseResponse.GetCustomerReceipt().TrimEnd();
            }
            else
            {
                // We did not even get a response, like in the case of a time-out.
                ActionsForm.listBoxFlow.Items.Add("# Could Not Retrieve Last Transaction.");
            }
        }

        private void HandleFinishedSettle(TransactionFlowState txState)
        {
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT SUCCESSFUL!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {settleResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add("# Period Start: " + settleResponse.GetPeriodStartTime());
                        ActionsForm.listBoxFlow.Items.Add("# Period End: " + settleResponse.GetPeriodEndTime());
                        ActionsForm.listBoxFlow.Items.Add("# Settlement Time: " + settleResponse.GetTriggeredTime());
                        ActionsForm.listBoxFlow.Items.Add("# Transaction Range: " + settleResponse.GetTransactionRange());
                        ActionsForm.listBoxFlow.Items.Add("# Terminal Id: " + settleResponse.GetTerminalId());
                        ActionsForm.listBoxFlow.Items.Add("# Total TX Count: " + settleResponse.GetTotalCount());
                        ActionsForm.listBoxFlow.Items.Add($"# Total TX Value: {settleResponse.GetTotalValue() / 100.0}");
                        ActionsForm.listBoxFlow.Items.Add("# By Aquirer TX Count: " + settleResponse.GetSettleByAcquirerCount());
                        ActionsForm.listBoxFlow.Items.Add($"# By Aquirer TX Value: {settleResponse.GetSettleByAcquirerValue() / 100.0}");
                        ActionsForm.listBoxFlow.Items.Add("# SCHEME SETTLEMENTS:");
                        var schemes = settleResponse.GetSchemeSettlementEntries();
                        foreach (var s in schemes)
                        {
                            ActionsForm.listBoxFlow.Items.Add("# " + s);
                        }

                        TransactionForm.richtextReceipt.Text = TransactionForm.richtextReceipt.Text + Environment.NewLine + "# Merchant Receipt:";
                        TransactionForm.richtextReceipt.Text = !settleResponse.WasMerchantReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + settleResponse.GetReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT FAILED!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {settleResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add("# Merchant Receipt:");

                        TransactionForm.richtextReceipt.Text = !settleResponse.WasMerchantReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + settleResponse.GetReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT ENQUIRY RESULT UNKNOWN!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFinishedSettlementEnquiry(TransactionFlowState txState)
        {
            switch (txState.Success)
            {
                case SPIClient.Message.SuccessState.Success:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT ENQUIRY SUCCESSFUL!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {settleResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add("# Period Start: " + settleResponse.GetPeriodStartTime());
                        ActionsForm.listBoxFlow.Items.Add("# Period End: " + settleResponse.GetPeriodEndTime());
                        ActionsForm.listBoxFlow.Items.Add("# Settlement Time: " + settleResponse.GetTriggeredTime());
                        ActionsForm.listBoxFlow.Items.Add("# Transaction Range: " + settleResponse.GetTransactionRange());
                        ActionsForm.listBoxFlow.Items.Add("# Terminal Id: " + settleResponse.GetTerminalId());
                        ActionsForm.listBoxFlow.Items.Add("# Total TX Count: " + settleResponse.GetTotalCount());
                        ActionsForm.listBoxFlow.Items.Add($"# Total TX Value: {settleResponse.GetTotalValue() / 100.0}");
                        ActionsForm.listBoxFlow.Items.Add("# By Aquirer TX Count: " + settleResponse.GetSettleByAcquirerCount());
                        ActionsForm.listBoxFlow.Items.Add($"# By Aquirere TX Value: {settleResponse.GetSettleByAcquirerValue() / 100.0}");
                        ActionsForm.listBoxFlow.Items.Add("# SCHEME SETTLEMENTS:");
                        var schemes = settleResponse.GetSchemeSettlementEntries();
                        foreach (var s in schemes)
                        {
                            ActionsForm.listBoxFlow.Items.Add("# " + s);
                        }

                        TransactionForm.richtextReceipt.Text = TransactionForm.richtextReceipt.Text + Environment.NewLine + "# Merchant Receipt:";
                        TransactionForm.richtextReceipt.Text = !settleResponse.WasMerchantReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + settleResponse.GetReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Failed:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT ENQUIRY FAILED!");
                    if (txState.Response != null)
                    {
                        var settleResponse = new Settlement(txState.Response);
                        ActionsForm.listBoxFlow.Items.Add($"# Response: {settleResponse.GetResponseText()}");
                        ActionsForm.listBoxFlow.Items.Add($"# Error: {txState.Response.GetError()}");
                        ActionsForm.listBoxFlow.Items.Add("# Merchant Receipt:");

                        TransactionForm.richtextReceipt.Text = !settleResponse.WasMerchantReceiptPrinted() ? TransactionForm.richtextReceipt.Text + Environment.NewLine + settleResponse.GetReceipt().TrimEnd() : TransactionForm.richtextReceipt.Text + Environment.NewLine + "# PRINTED FROM EFTPOS";
                    }
                    break;
                case SPIClient.Message.SuccessState.Unknown:
                    ActionsForm.listBoxFlow.Items.Add($"# SETTLEMENT ENQUIRY RESULT UNKNOWN!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SpiPairingStatus()
        {
            Invoke(new Action(() => lblPairingStatus.Text = SpiClient.CurrentStatus.ToString()));
        }

        public void GetOKActionComponents()
        {
            ActionsForm.btnAction1.Enabled = true;
            ActionsForm.btnAction1.Visible = true;
            ActionsForm.btnAction1.Text = ButtonCaption.OK;
            ActionsForm.btnAction2.Visible = false;
            ActionsForm.btnAction3.Visible = false;
            GetUnvisibleActionComponents();
        }

        private void GetUnvisibleActionComponents()
        {
            ActionsForm.lblAction1.Visible = false;
            ActionsForm.lblAction2.Visible = false;
            ActionsForm.lblAction3.Visible = false;
            ActionsForm.lblAction4.Visible = false;
            ActionsForm.txtAction1.Visible = false;
            ActionsForm.txtAction2.Visible = false;
            ActionsForm.txtAction3.Visible = false;
            ActionsForm.txtAction4.Visible = false;
            ActionsForm.cboxAction1.Visible = false;
        }

        public bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == form.Name)
                    return true;
            return false;
        }

        #endregion

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.Enabled = true;
        }
    }
}
