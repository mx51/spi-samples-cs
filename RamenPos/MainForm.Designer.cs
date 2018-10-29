namespace RamenPos
{
    partial class RamenPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RamenPos));
            this.pbRamenPos = new System.Windows.Forms.PictureBox();
            this.lblPosId = new System.Windows.Forms.Label();
            this.txtPosId = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblEftposAddress = new System.Windows.Forms.Label();
            this.chkAutoAddress = new System.Windows.Forms.CheckBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnPair = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.grpAutoAddressResolution = new System.Windows.Forms.GroupBox();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.lblPairingStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpAutoAddressResolution.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbRamenPos
            // 
            this.pbRamenPos.Image = global::RamenPos.Properties.Resources._450191;
            this.pbRamenPos.Location = new System.Drawing.Point(30, 62);
            this.pbRamenPos.Name = "pbRamenPos";
            this.pbRamenPos.Size = new System.Drawing.Size(410, 393);
            this.pbRamenPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRamenPos.TabIndex = 0;
            this.pbRamenPos.TabStop = false;
            // 
            // lblPosId
            // 
            this.lblPosId.AutoSize = true;
            this.lblPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosId.Location = new System.Drawing.Point(459, 62);
            this.lblPosId.Name = "lblPosId";
            this.lblPosId.Size = new System.Drawing.Size(52, 16);
            this.lblPosId.TabIndex = 1;
            this.lblPosId.Text = "POS ID";
            // 
            // txtPosId
            // 
            this.txtPosId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosId.Location = new System.Drawing.Point(462, 84);
            this.txtPosId.MaxLength = 10;
            this.txtPosId.Name = "txtPosId";
            this.txtPosId.Size = new System.Drawing.Size(250, 22);
            this.txtPosId.TabIndex = 2;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.Location = new System.Drawing.Point(462, 145);
            this.txtSerialNumber.MaxLength = 20;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(250, 22);
            this.txtSerialNumber.TabIndex = 3;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(459, 126);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(114, 16);
            this.lblSerialNumber.TabIndex = 4;
            this.lblSerialNumber.Text = "SERIAL NUMBER";
            // 
            // lblEftposAddress
            // 
            this.lblEftposAddress.AutoSize = true;
            this.lblEftposAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEftposAddress.Location = new System.Drawing.Point(459, 194);
            this.lblEftposAddress.Name = "lblEftposAddress";
            this.lblEftposAddress.Size = new System.Drawing.Size(122, 16);
            this.lblEftposAddress.TabIndex = 5;
            this.lblEftposAddress.Text = "DEVICE ADDRESS";
            // 
            // chkAutoAddress
            // 
            this.chkAutoAddress.AutoSize = true;
            this.chkAutoAddress.Checked = true;
            this.chkAutoAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoAddress.Location = new System.Drawing.Point(109, 24);
            this.chkAutoAddress.Name = "chkAutoAddress";
            this.chkAutoAddress.Size = new System.Drawing.Size(54, 20);
            this.chkAutoAddress.TabIndex = 6;
            this.chkAutoAddress.Text = "Auto";
            this.chkAutoAddress.UseVisualStyleBackColor = true;
            this.chkAutoAddress.CheckedChanged += new System.EventHandler(this.chkAutoIpAddress_CheckedChanged);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(462, 213);
            this.txtAddress.MaxLength = 20;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(250, 22);
            this.txtAddress.TabIndex = 7;
            // 
            // btnPair
            // 
            this.btnPair.BackColor = System.Drawing.Color.LightCyan;
            this.btnPair.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;
            this.btnPair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPair.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPair.Location = new System.Drawing.Point(614, 413);
            this.btnPair.Name = "btnPair";
            this.btnPair.Size = new System.Drawing.Size(98, 42);
            this.btnPair.TabIndex = 8;
            this.btnPair.Text = "PAIR";
            this.btnPair.UseVisualStyleBackColor = false;
            this.btnPair.Click += new System.EventHandler(this.btnPair_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDescription.Location = new System.Drawing.Point(27, 24);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(416, 16);
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
            this.btnSave.Location = new System.Drawing.Point(180, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 31);
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
            this.grpAutoAddressResolution.Location = new System.Drawing.Point(462, 260);
            this.grpAutoAddressResolution.Name = "grpAutoAddressResolution";
            this.grpAutoAddressResolution.Size = new System.Drawing.Size(250, 65);
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
            this.chkTestMode.Location = new System.Drawing.Point(16, 24);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(87, 20);
            this.chkTestMode.TabIndex = 12;
            this.chkTestMode.Text = "Test Mode";
            this.chkTestMode.UseVisualStyleBackColor = true;
            // 
            // lblPairingStatus
            // 
            this.lblPairingStatus.AutoSize = true;
            this.lblPairingStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPairingStatus.Location = new System.Drawing.Point(459, 426);
            this.lblPairingStatus.Name = "lblPairingStatus";
            this.lblPairingStatus.Size = new System.Drawing.Size(59, 16);
            this.lblPairingStatus.TabIndex = 12;
            this.lblPairingStatus.Text = "Unpaired";
            // 
            // RamenPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 467);
            this.Controls.Add(this.lblPairingStatus);
            this.Controls.Add(this.grpAutoAddressResolution);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnPair);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblEftposAddress);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.txtPosId);
            this.Controls.Add(this.lblPosId);
            this.Controls.Add(this.pbRamenPos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RamenPos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly Payment - RamenPos";
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpAutoAddressResolution.ResumeLayout(false);
            this.grpAutoAddressResolution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRamenPos;
        private System.Windows.Forms.Label lblPosId;
        private System.Windows.Forms.TextBox txtPosId;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblEftposAddress;
        private System.Windows.Forms.CheckBox chkAutoAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnPair;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpAutoAddressResolution;
        private System.Windows.Forms.Label lblPairingStatus;
        private System.Windows.Forms.CheckBox chkTestMode;
    }
}

