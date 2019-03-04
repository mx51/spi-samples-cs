namespace RamenPos
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pbRamenPos = new System.Windows.Forms.PictureBox();
            this.lblPosId = new System.Windows.Forms.Label();
            this.txtPosId = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblEftposAddress = new System.Windows.Forms.Label();
            this.chkAutoAddress = new System.Windows.Forms.CheckBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.grpAutoAddressResolution = new System.Windows.Forms.GroupBox();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.lblPairingStatus = new System.Windows.Forms.Label();
            this.txtSecrets = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.grpSecrets = new System.Windows.Forms.GroupBox();
            this.cboxSecrets = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpAutoAddressResolution.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.grpSecrets.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbRamenPos
            // 
            this.pbRamenPos.Image = global::RamenPos.Properties.Resources._450191;
            this.pbRamenPos.Location = new System.Drawing.Point(60, 119);
            this.pbRamenPos.Margin = new System.Windows.Forms.Padding(6);
            this.pbRamenPos.Name = "pbRamenPos";
            this.pbRamenPos.Size = new System.Drawing.Size(820, 756);
            this.pbRamenPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRamenPos.TabIndex = 0;
            this.pbRamenPos.TabStop = false;
            // 
            // lblPosId
            // 
            this.lblPosId.AutoSize = true;
            this.lblPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosId.Location = new System.Drawing.Point(12, 31);
            this.lblPosId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPosId.Name = "lblPosId";
            this.lblPosId.Size = new System.Drawing.Size(100, 31);
            this.lblPosId.TabIndex = 1;
            this.lblPosId.Text = "POS ID";
            // 
            // txtPosId
            // 
            this.txtPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosId.Location = new System.Drawing.Point(12, 73);
            this.txtPosId.Margin = new System.Windows.Forms.Padding(6);
            this.txtPosId.MaxLength = 10;
            this.txtPosId.Name = "txtPosId";
            this.txtPosId.Size = new System.Drawing.Size(496, 37);
            this.txtPosId.TabIndex = 2;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.Location = new System.Drawing.Point(12, 163);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(6);
            this.txtSerialNumber.MaxLength = 20;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(496, 37);
            this.txtSerialNumber.TabIndex = 3;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(12, 121);
            this.lblSerialNumber.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(222, 31);
            this.lblSerialNumber.TabIndex = 4;
            this.lblSerialNumber.Text = "SERIAL NUMBER";
            // 
            // lblEftposAddress
            // 
            this.lblEftposAddress.AutoSize = true;
            this.lblEftposAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEftposAddress.Location = new System.Drawing.Point(12, 210);
            this.lblEftposAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEftposAddress.Name = "lblEftposAddress";
            this.lblEftposAddress.Size = new System.Drawing.Size(240, 31);
            this.lblEftposAddress.TabIndex = 5;
            this.lblEftposAddress.Text = "DEVICE ADDRESS";
            // 
            // chkAutoAddress
            // 
            this.chkAutoAddress.AutoSize = true;
            this.chkAutoAddress.Checked = true;
            this.chkAutoAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoAddress.Location = new System.Drawing.Point(218, 46);
            this.chkAutoAddress.Margin = new System.Windows.Forms.Padding(6);
            this.chkAutoAddress.Name = "chkAutoAddress";
            this.chkAutoAddress.Size = new System.Drawing.Size(97, 34);
            this.chkAutoAddress.TabIndex = 6;
            this.chkAutoAddress.Text = "Auto";
            this.chkAutoAddress.UseVisualStyleBackColor = true;
            this.chkAutoAddress.CheckedChanged += new System.EventHandler(this.chkAutoIpAddress_CheckedChanged);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(12, 252);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txtAddress.MaxLength = 20;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(496, 37);
            this.txtAddress.TabIndex = 7;
            // 
            // btnMain
            // 
            this.btnMain.BackColor = System.Drawing.Color.LightCyan;
            this.btnMain.Enabled = false;
            this.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMain.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMain.Location = new System.Drawing.Point(1220, 794);
            this.btnMain.Margin = new System.Windows.Forms.Padding(6);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(216, 81);
            this.btnMain.TabIndex = 8;
            this.btnMain.Text = "btnMain";
            this.btnMain.UseVisualStyleBackColor = false;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDescription.Location = new System.Drawing.Point(54, 63);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(803, 30);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "To begin, press PAIR on your terminal and then the PAIR button below";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Font = new System.Drawing.Font("Arial", 8F);
            this.btnSave.Location = new System.Drawing.Point(360, 37);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 60);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpAutoAddressResolution
            // 
            this.grpAutoAddressResolution.Controls.Add(this.chkTestMode);
            this.grpAutoAddressResolution.Controls.Add(this.chkAutoAddress);
            this.grpAutoAddressResolution.Controls.Add(this.btnSave);
            this.grpAutoAddressResolution.Location = new System.Drawing.Point(920, 390);
            this.grpAutoAddressResolution.Margin = new System.Windows.Forms.Padding(6);
            this.grpAutoAddressResolution.Name = "grpAutoAddressResolution";
            this.grpAutoAddressResolution.Padding = new System.Windows.Forms.Padding(6);
            this.grpAutoAddressResolution.Size = new System.Drawing.Size(548, 125);
            this.grpAutoAddressResolution.TabIndex = 11;
            this.grpAutoAddressResolution.TabStop = false;
            this.grpAutoAddressResolution.Text = "Auto Address Resolution";
            // 
            // chkTestMode
            // 
            this.chkTestMode.AutoSize = true;
            this.chkTestMode.Checked = true;
            this.chkTestMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTestMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTestMode.Location = new System.Drawing.Point(12, 46);
            this.chkTestMode.Margin = new System.Windows.Forms.Padding(6);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(163, 34);
            this.chkTestMode.TabIndex = 12;
            this.chkTestMode.Text = "Test Mode";
            this.chkTestMode.UseVisualStyleBackColor = true;
            // 
            // lblPairingStatus
            // 
            this.lblPairingStatus.AutoSize = true;
            this.lblPairingStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPairingStatus.Location = new System.Drawing.Point(924, 819);
            this.lblPairingStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPairingStatus.Name = "lblPairingStatus";
            this.lblPairingStatus.Size = new System.Drawing.Size(117, 30);
            this.lblPairingStatus.TabIndex = 12;
            this.lblPairingStatus.Text = "Unpaired";
            // 
            // txtSecrets
            // 
            this.txtSecrets.BackColor = System.Drawing.SystemColors.Window;
            this.txtSecrets.Enabled = false;
            this.txtSecrets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecrets.Location = new System.Drawing.Point(12, 87);
            this.txtSecrets.Margin = new System.Windows.Forms.Padding(6);
            this.txtSecrets.Name = "txtSecrets";
            this.txtSecrets.Size = new System.Drawing.Size(496, 37);
            this.txtSecrets.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1476, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // transactionsToolStripMenuItem
            // 
            this.transactionsToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.transactionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            this.transactionsToolStripMenuItem.Size = new System.Drawing.Size(218, 49);
            this.transactionsToolStripMenuItem.Text = "Transactions";
            this.transactionsToolStripMenuItem.Visible = false;
            this.transactionsToolStripMenuItem.Click += new System.EventHandler(this.transactionsToolStripMenuItem_Click);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.lblPosId);
            this.grpSettings.Controls.Add(this.txtPosId);
            this.grpSettings.Controls.Add(this.txtSerialNumber);
            this.grpSettings.Controls.Add(this.lblSerialNumber);
            this.grpSettings.Controls.Add(this.lblEftposAddress);
            this.grpSettings.Controls.Add(this.txtAddress);
            this.grpSettings.Location = new System.Drawing.Point(920, 63);
            this.grpSettings.Margin = new System.Windows.Forms.Padding(6);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Padding = new System.Windows.Forms.Padding(6);
            this.grpSettings.Size = new System.Drawing.Size(548, 315);
            this.grpSettings.TabIndex = 18;
            this.grpSettings.TabStop = false;
            // 
            // grpSecrets
            // 
            this.grpSecrets.Controls.Add(this.cboxSecrets);
            this.grpSecrets.Controls.Add(this.txtSecrets);
            this.grpSecrets.Location = new System.Drawing.Point(920, 527);
            this.grpSecrets.Margin = new System.Windows.Forms.Padding(6);
            this.grpSecrets.Name = "grpSecrets";
            this.grpSecrets.Padding = new System.Windows.Forms.Padding(6);
            this.grpSecrets.Size = new System.Drawing.Size(548, 160);
            this.grpSecrets.TabIndex = 19;
            this.grpSecrets.TabStop = false;
            // 
            // cboxSecrets
            // 
            this.cboxSecrets.AutoSize = true;
            this.cboxSecrets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxSecrets.Location = new System.Drawing.Point(12, 37);
            this.cboxSecrets.Margin = new System.Windows.Forms.Padding(6);
            this.cboxSecrets.Name = "cboxSecrets";
            this.cboxSecrets.Size = new System.Drawing.Size(132, 34);
            this.cboxSecrets.TabIndex = 15;
            this.cboxSecrets.Text = "Secrets";
            this.cboxSecrets.UseVisualStyleBackColor = true;
            this.cboxSecrets.CheckedChanged += new System.EventHandler(this.cboxSecrets_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1476, 898);
            this.Controls.Add(this.grpSecrets);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.lblPairingStatus);
            this.Controls.Add(this.grpAutoAddressResolution);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.pbRamenPos);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.Load += new System.EventHandler(this.RamenPos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpAutoAddressResolution.ResumeLayout(false);
            this.grpAutoAddressResolution.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.grpSecrets.ResumeLayout(false);
            this.grpSecrets.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pbRamenPos;
        internal System.Windows.Forms.Label lblPosId;
        internal System.Windows.Forms.TextBox txtPosId;
        internal System.Windows.Forms.TextBox txtSerialNumber;
        internal System.Windows.Forms.Label lblSerialNumber;
        internal System.Windows.Forms.Label lblEftposAddress;
        internal System.Windows.Forms.CheckBox chkAutoAddress;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.Button btnMain;
        internal System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.ErrorProvider errorProvider;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.GroupBox grpAutoAddressResolution;
        internal System.Windows.Forms.Label lblPairingStatus;
        internal System.Windows.Forms.CheckBox chkTestMode;
        internal System.Windows.Forms.TextBox txtSecrets;
        internal System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
        internal System.Windows.Forms.GroupBox grpSettings;
        internal System.Windows.Forms.GroupBox grpSecrets;
        internal System.Windows.Forms.CheckBox cboxSecrets;
    }
}

