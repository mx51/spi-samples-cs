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
            this.lblEftposAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblPairingStatus = new System.Windows.Forms.Label();
            this.txtSecrets = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.grpSecrets = new System.Windows.Forms.GroupBox();
            this.chkSecrets = new System.Windows.Forms.CheckBox();
            this.grpPaymentProvider = new System.Windows.Forms.GroupBox();
            this.lblPaymentProviderSelected = new System.Windows.Forms.Label();
            this.btnPaymentProvider = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.grpSecrets.SuspendLayout();
            this.grpPaymentProvider.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbRamenPos
            // 
            this.pbRamenPos.Image = global::RamenPos.Properties.Resources._450191;
            this.pbRamenPos.Location = new System.Drawing.Point(40, 76);
            this.pbRamenPos.Margin = new System.Windows.Forms.Padding(4);
            this.pbRamenPos.Name = "pbRamenPos";
            this.pbRamenPos.Size = new System.Drawing.Size(547, 484);
            this.pbRamenPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRamenPos.TabIndex = 0;
            this.pbRamenPos.TabStop = false;
            // 
            // lblPosId
            // 
            this.lblPosId.AutoSize = true;
            this.lblPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosId.Location = new System.Drawing.Point(8, 20);
            this.lblPosId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPosId.Name = "lblPosId";
            this.lblPosId.Size = new System.Drawing.Size(65, 19);
            this.lblPosId.TabIndex = 1;
            this.lblPosId.Text = "POS ID";
            // 
            // txtPosId
            // 
            this.txtPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosId.Location = new System.Drawing.Point(15, 47);
            this.txtPosId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPosId.MaxLength = 20;
            this.txtPosId.Name = "txtPosId";
            this.txtPosId.Size = new System.Drawing.Size(325, 26);
            this.txtPosId.TabIndex = 2;
            // 
            // lblEftposAddress
            // 
            this.lblEftposAddress.AutoSize = true;
            this.lblEftposAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEftposAddress.Location = new System.Drawing.Point(8, 78);
            this.lblEftposAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEftposAddress.Name = "lblEftposAddress";
            this.lblEftposAddress.Size = new System.Drawing.Size(154, 19);
            this.lblEftposAddress.TabIndex = 5;
            this.lblEftposAddress.Text = "DEVICE ADDRESS";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(15, 105);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.MaxLength = 20;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(325, 26);
            this.txtAddress.TabIndex = 7;
            // 
            // btnMain
            // 
            this.btnMain.BackColor = System.Drawing.Color.LightCyan;
            this.btnMain.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMain.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMain.Location = new System.Drawing.Point(835, 525);
            this.btnMain.Margin = new System.Windows.Forms.Padding(4);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(144, 52);
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
            this.lblDescription.Location = new System.Drawing.Point(36, 41);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(455, 20);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Please select Payment Provider and then Pair your terminal";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // lblPairingStatus
            // 
            this.lblPairingStatus.AutoSize = true;
            this.lblPairingStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPairingStatus.Location = new System.Drawing.Point(616, 541);
            this.lblPairingStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPairingStatus.Name = "lblPairingStatus";
            this.lblPairingStatus.Size = new System.Drawing.Size(76, 20);
            this.lblPairingStatus.TabIndex = 12;
            this.lblPairingStatus.Text = "Unpaired";
            // 
            // txtSecrets
            // 
            this.txtSecrets.BackColor = System.Drawing.SystemColors.Window;
            this.txtSecrets.Enabled = false;
            this.txtSecrets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecrets.Location = new System.Drawing.Point(15, 55);
            this.txtSecrets.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecrets.Name = "txtSecrets";
            this.txtSecrets.Size = new System.Drawing.Size(325, 26);
            this.txtSecrets.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1015, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // transactionsToolStripMenuItem
            // 
            this.transactionsToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.transactionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            this.transactionsToolStripMenuItem.Size = new System.Drawing.Size(144, 32);
            this.transactionsToolStripMenuItem.Text = "Transactions";
            this.transactionsToolStripMenuItem.Visible = false;
            this.transactionsToolStripMenuItem.Click += new System.EventHandler(this.transactionsToolStripMenuItem_Click);
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.chkTestMode);
            this.grpSettings.Controls.Add(this.lblPosId);
            this.grpSettings.Controls.Add(this.txtPosId);
            this.grpSettings.Controls.Add(this.lblSerialNumber);
            this.grpSettings.Controls.Add(this.txtSerialNumber);
            this.grpSettings.Controls.Add(this.lblEftposAddress);
            this.grpSettings.Controls.Add(this.txtAddress);
            this.grpSettings.Location = new System.Drawing.Point(605, 171);
            this.grpSettings.Margin = new System.Windows.Forms.Padding(4);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Padding = new System.Windows.Forms.Padding(4);
            this.grpSettings.Size = new System.Drawing.Size(373, 245);
            this.grpSettings.TabIndex = 18;
            this.grpSettings.TabStop = false;
            // 
            // chkTestMode
            // 
            this.chkTestMode.AutoSize = true;
            this.chkTestMode.Checked = true;
            this.chkTestMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTestMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTestMode.Location = new System.Drawing.Point(15, 213);
            this.chkTestMode.Margin = new System.Windows.Forms.Padding(4);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(108, 24);
            this.chkTestMode.TabIndex = 20;
            this.chkTestMode.Text = "Test Mode";
            this.chkTestMode.UseVisualStyleBackColor = true;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(8, 145);
            this.lblSerialNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(139, 19);
            this.lblSerialNumber.TabIndex = 5;
            this.lblSerialNumber.Text = "SERIAL NUMBER";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.Location = new System.Drawing.Point(15, 172);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtSerialNumber.MaxLength = 20;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(325, 26);
            this.txtSerialNumber.TabIndex = 8;
            // 
            // grpSecrets
            // 
            this.grpSecrets.Controls.Add(this.chkSecrets);
            this.grpSecrets.Controls.Add(this.txtSecrets);
            this.grpSecrets.Location = new System.Drawing.Point(605, 415);
            this.grpSecrets.Margin = new System.Windows.Forms.Padding(4);
            this.grpSecrets.Name = "grpSecrets";
            this.grpSecrets.Padding = new System.Windows.Forms.Padding(4);
            this.grpSecrets.Size = new System.Drawing.Size(373, 102);
            this.grpSecrets.TabIndex = 19;
            this.grpSecrets.TabStop = false;
            // 
            // chkSecrets
            // 
            this.chkSecrets.AutoSize = true;
            this.chkSecrets.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecrets.Location = new System.Drawing.Point(12, 23);
            this.chkSecrets.Margin = new System.Windows.Forms.Padding(4);
            this.chkSecrets.Name = "chkSecrets";
            this.chkSecrets.Size = new System.Drawing.Size(89, 24);
            this.chkSecrets.TabIndex = 15;
            this.chkSecrets.Text = "Secrets";
            this.chkSecrets.UseVisualStyleBackColor = true;
            this.chkSecrets.CheckedChanged += new System.EventHandler(this.chkSecrets_CheckedChanged);
            // 
            // grpPaymentProvider
            // 
            this.grpPaymentProvider.Controls.Add(this.lblPaymentProviderSelected);
            this.grpPaymentProvider.Controls.Add(this.btnPaymentProvider);
            this.grpPaymentProvider.Location = new System.Drawing.Point(605, 41);
            this.grpPaymentProvider.Margin = new System.Windows.Forms.Padding(4);
            this.grpPaymentProvider.Name = "grpPaymentProvider";
            this.grpPaymentProvider.Padding = new System.Windows.Forms.Padding(4);
            this.grpPaymentProvider.Size = new System.Drawing.Size(371, 134);
            this.grpPaymentProvider.TabIndex = 20;
            this.grpPaymentProvider.TabStop = false;
            // 
            // lblPaymentProviderSelected
            // 
            this.lblPaymentProviderSelected.AutoSize = true;
            this.lblPaymentProviderSelected.Location = new System.Drawing.Point(12, 96);
            this.lblPaymentProviderSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentProviderSelected.Name = "lblPaymentProviderSelected";
            this.lblPaymentProviderSelected.Size = new System.Drawing.Size(0, 17);
            this.lblPaymentProviderSelected.TabIndex = 1;
            // 
            // btnPaymentProvider
            // 
            this.btnPaymentProvider.BackColor = System.Drawing.Color.LightCyan;
            this.btnPaymentProvider.Location = new System.Drawing.Point(9, 14);
            this.btnPaymentProvider.Margin = new System.Windows.Forms.Padding(4);
            this.btnPaymentProvider.Name = "btnPaymentProvider";
            this.btnPaymentProvider.Size = new System.Drawing.Size(353, 74);
            this.btnPaymentProvider.TabIndex = 0;
            this.btnPaymentProvider.Text = "Payment Provider";
            this.btnPaymentProvider.UseVisualStyleBackColor = false;
            this.btnPaymentProvider.Click += new System.EventHandler(this.btnPaymentProvider_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1015, 590);
            this.Controls.Add(this.grpPaymentProvider);
            this.Controls.Add(this.grpSecrets);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.lblPairingStatus);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.pbRamenPos);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "`";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.RamenPos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.grpSecrets.ResumeLayout(false);
            this.grpSecrets.PerformLayout();
            this.grpPaymentProvider.ResumeLayout(false);
            this.grpPaymentProvider.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pbRamenPos;
        internal System.Windows.Forms.Label lblPosId;
        internal System.Windows.Forms.TextBox txtPosId;
        internal System.Windows.Forms.Label lblEftposAddress;
        internal System.Windows.Forms.TextBox txtAddress;
        internal System.Windows.Forms.Button btnMain;
        internal System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.ErrorProvider errorProvider;
        internal System.Windows.Forms.Label lblPairingStatus;
        internal System.Windows.Forms.TextBox txtSecrets;
        internal System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
        internal System.Windows.Forms.GroupBox grpSettings;
        internal System.Windows.Forms.GroupBox grpSecrets;
        internal System.Windows.Forms.CheckBox chkSecrets;
        internal System.Windows.Forms.CheckBox chkTestMode;
        internal System.Windows.Forms.GroupBox grpPaymentProvider;
        internal System.Windows.Forms.Button btnPaymentProvider;
        private System.Windows.Forms.Label lblPaymentProviderSelected;
        internal System.Windows.Forms.Label lblSerialNumber;
        internal System.Windows.Forms.TextBox txtSerialNumber;
    }
}

