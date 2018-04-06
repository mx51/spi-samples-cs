namespace AcmePosWF
{
    partial class FrmMain
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
            this.lblPosId = new System.Windows.Forms.Label();
            this.lblEftposAddress = new System.Windows.Forms.Label();
            this.textPosId = new System.Windows.Forms.TextBox();
            this.textEftposAddress = new System.Windows.Forms.TextBox();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.lblSettings = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPair = new System.Windows.Forms.Button();
            this.lblStatusHead = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnGetLast = new System.Windows.Forms.Button();
            this.btnSettle = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.lblActions = new System.Windows.Forms.Label();
            this.richtextReceipt = new System.Windows.Forms.RichTextBox();
            this.pnlReceipt = new System.Windows.Forms.Panel();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.pnlReceipt.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPosId
            // 
            this.lblPosId.AutoSize = true;
            this.lblPosId.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosId.Location = new System.Drawing.Point(15, 49);
            this.lblPosId.Name = "lblPosId";
            this.lblPosId.Size = new System.Drawing.Size(66, 24);
            this.lblPosId.TabIndex = 0;
            this.lblPosId.Text = "Pos ID:";
            // 
            // lblEftposAddress
            // 
            this.lblEftposAddress.AutoSize = true;
            this.lblEftposAddress.Location = new System.Drawing.Point(15, 87);
            this.lblEftposAddress.Name = "lblEftposAddress";
            this.lblEftposAddress.Size = new System.Drawing.Size(148, 24);
            this.lblEftposAddress.TabIndex = 1;
            this.lblEftposAddress.Text = "EFTPOS Address:";
            // 
            // textPosId
            // 
            this.textPosId.Location = new System.Drawing.Point(159, 46);
            this.textPosId.Name = "textPosId";
            this.textPosId.Size = new System.Drawing.Size(217, 32);
            this.textPosId.TabIndex = 2;
            // 
            // textEftposAddress
            // 
            this.textEftposAddress.Location = new System.Drawing.Point(159, 84);
            this.textEftposAddress.Name = "textEftposAddress";
            this.textEftposAddress.Size = new System.Drawing.Size(217, 32);
            this.textEftposAddress.TabIndex = 3;
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSettings.Controls.Add(this.lblSettings);
            this.pnlSettings.Controls.Add(this.lblPosId);
            this.pnlSettings.Controls.Add(this.textEftposAddress);
            this.pnlSettings.Controls.Add(this.lblEftposAddress);
            this.pnlSettings.Controls.Add(this.textPosId);
            this.pnlSettings.Location = new System.Drawing.Point(12, 12);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(381, 126);
            this.pnlSettings.TabIndex = 4;
            // 
            // lblSettings
            // 
            this.lblSettings.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblSettings.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(0, 0);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(380, 43);
            this.lblSettings.TabIndex = 4;
            this.lblSettings.Text = "Settings";
            this.lblSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Controls.Add(this.btnPair);
            this.pnlStatus.Controls.Add(this.lblStatusHead);
            this.pnlStatus.Location = new System.Drawing.Point(12, 144);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(381, 138);
            this.pnlStatus.TabIndex = 5;
            this.pnlStatus.Tag = "";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(0, 44);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(380, 43);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPair
            // 
            this.btnPair.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPair.Location = new System.Drawing.Point(97, 90);
            this.btnPair.Name = "btnPair";
            this.btnPair.Size = new System.Drawing.Size(179, 33);
            this.btnPair.TabIndex = 6;
            this.btnPair.Text = "Pair";
            this.btnPair.UseVisualStyleBackColor = true;
            this.btnPair.Click += new System.EventHandler(this.btnPair_Click);
            // 
            // lblStatusHead
            // 
            this.lblStatusHead.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblStatusHead.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusHead.Location = new System.Drawing.Point(0, 0);
            this.lblStatusHead.Name = "lblStatusHead";
            this.lblStatusHead.Size = new System.Drawing.Size(380, 43);
            this.lblStatusHead.TabIndex = 0;
            this.lblStatusHead.Text = "Status";
            this.lblStatusHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.btnGetLast);
            this.pnlActions.Controls.Add(this.btnSettle);
            this.pnlActions.Controls.Add(this.btnRefund);
            this.pnlActions.Controls.Add(this.btnPurchase);
            this.pnlActions.Controls.Add(this.lblActions);
            this.pnlActions.Location = new System.Drawing.Point(12, 288);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(381, 209);
            this.pnlActions.TabIndex = 0;
            this.pnlActions.Visible = false;
            // 
            // btnGetLast
            // 
            this.btnGetLast.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetLast.Location = new System.Drawing.Point(60, 166);
            this.btnGetLast.Name = "btnGetLast";
            this.btnGetLast.Size = new System.Drawing.Size(254, 33);
            this.btnGetLast.TabIndex = 10;
            this.btnGetLast.Text = "Get Last Transaction";
            this.btnGetLast.UseVisualStyleBackColor = true;
            this.btnGetLast.Click += new System.EventHandler(this.btnGetLast_Click);
            // 
            // btnSettle
            // 
            this.btnSettle.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettle.Location = new System.Drawing.Point(60, 126);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(254, 33);
            this.btnSettle.TabIndex = 9;
            this.btnSettle.Text = "Settle";
            this.btnSettle.UseVisualStyleBackColor = true;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefund.Location = new System.Drawing.Point(60, 86);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(254, 33);
            this.btnRefund.TabIndex = 8;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnPurchase
            // 
            this.btnPurchase.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchase.Location = new System.Drawing.Point(60, 46);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(254, 33);
            this.btnPurchase.TabIndex = 7;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // lblActions
            // 
            this.lblActions.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblActions.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActions.Location = new System.Drawing.Point(0, 0);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(380, 43);
            this.lblActions.TabIndex = 5;
            this.lblActions.Text = "Actions";
            this.lblActions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richtextReceipt
            // 
            this.richtextReceipt.Location = new System.Drawing.Point(3, 46);
            this.richtextReceipt.Name = "richtextReceipt";
            this.richtextReceipt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richtextReceipt.Size = new System.Drawing.Size(340, 438);
            this.richtextReceipt.TabIndex = 6;
            this.richtextReceipt.Text = "";
            // 
            // pnlReceipt
            // 
            this.pnlReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReceipt.Controls.Add(this.lblReceipt);
            this.pnlReceipt.Controls.Add(this.richtextReceipt);
            this.pnlReceipt.Location = new System.Drawing.Point(399, 12);
            this.pnlReceipt.Name = "pnlReceipt";
            this.pnlReceipt.Size = new System.Drawing.Size(350, 485);
            this.pnlReceipt.TabIndex = 7;
            // 
            // lblReceipt
            // 
            this.lblReceipt.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblReceipt.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceipt.Location = new System.Drawing.Point(0, 0);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(343, 43);
            this.lblReceipt.TabIndex = 5;
            this.lblReceipt.Text = "Receipt";
            this.lblReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 501);
            this.Controls.Add(this.pnlReceipt);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlSettings);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acme POS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlReceipt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPosId;
        private System.Windows.Forms.Label lblEftposAddress;
        public System.Windows.Forms.TextBox textPosId;
        public System.Windows.Forms.TextBox textEftposAddress;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusHead;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label lblActions;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Button btnPair;
        private System.Windows.Forms.Button btnSettle;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.RichTextBox richtextReceipt;
        private System.Windows.Forms.Panel pnlReceipt;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.Button btnGetLast;
    }
}

