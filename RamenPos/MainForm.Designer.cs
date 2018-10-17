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
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.chkAutoIpAddress = new System.Windows.Forms.CheckBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.btnPair = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnResolveIpAddress = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbRamenPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.txtSerialNumber.Location = new System.Drawing.Point(462, 156);
            this.txtSerialNumber.MaxLength = 20;
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(250, 22);
            this.txtSerialNumber.TabIndex = 3;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(459, 135);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(114, 16);
            this.lblSerialNumber.TabIndex = 4;
            this.lblSerialNumber.Text = "SERIAL NUMBER";
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.Location = new System.Drawing.Point(459, 205);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(86, 16);
            this.lblIpAddress.TabIndex = 5;
            this.lblIpAddress.Text = "IP ADDRESS";
            // 
            // chkAutoIpAddress
            // 
            this.chkAutoIpAddress.AutoSize = true;
            this.chkAutoIpAddress.Checked = true;
            this.chkAutoIpAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoIpAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoIpAddress.Location = new System.Drawing.Point(658, 228);
            this.chkAutoIpAddress.Name = "chkAutoIpAddress";
            this.chkAutoIpAddress.Size = new System.Drawing.Size(54, 20);
            this.chkAutoIpAddress.TabIndex = 6;
            this.chkAutoIpAddress.Text = "Auto";
            this.chkAutoIpAddress.UseVisualStyleBackColor = true;
            this.chkAutoIpAddress.CheckedChanged += new System.EventHandler(this.chkAutoIpAddress_CheckedChanged);
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.BackColor = System.Drawing.Color.LightGray;
            this.txtIpAddress.Enabled = false;
            this.txtIpAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpAddress.Location = new System.Drawing.Point(462, 226);
            this.txtIpAddress.MaxLength = 20;
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(190, 22);
            this.txtIpAddress.TabIndex = 7;
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
            // btnResolveIpAddress
            // 
            this.btnResolveIpAddress.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnResolveIpAddress.Font = new System.Drawing.Font("Arial", 8F);
            this.btnResolveIpAddress.Location = new System.Drawing.Point(614, 254);
            this.btnResolveIpAddress.Name = "btnResolveIpAddress";
            this.btnResolveIpAddress.Size = new System.Drawing.Size(98, 42);
            this.btnResolveIpAddress.TabIndex = 10;
            this.btnResolveIpAddress.Text = "Resolve IP";
            this.btnResolveIpAddress.UseVisualStyleBackColor = false;
            this.btnResolveIpAddress.Click += new System.EventHandler(this.btnResolveIpAddress_Click);
            // 
            // RamenPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 467);
            this.Controls.Add(this.btnResolveIpAddress);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnPair);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.chkAutoIpAddress);
            this.Controls.Add(this.lblIpAddress);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRamenPos;
        private System.Windows.Forms.Label lblPosId;
        private System.Windows.Forms.TextBox txtPosId;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.CheckBox chkAutoIpAddress;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnPair;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnResolveIpAddress;
    }
}

