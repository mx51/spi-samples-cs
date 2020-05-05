namespace RamenPos
{
    partial class TransactionForm
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
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.cboPrintMerchantCopy = new System.Windows.Forms.CheckBox();
            this.cboxSignFromEftpos = new System.Windows.Forms.CheckBox();
            this.cboxReceiptFrom = new System.Windows.Forms.CheckBox();
            this.lblSettings = new System.Windows.Forms.Label();
            this.pnlTransactions = new System.Windows.Forms.Panel();
            this.btnRecovery = new System.Windows.Forms.Button();
            this.btnSettleEnq = new System.Windows.Forms.Button();
            this.btnCashout = new System.Windows.Forms.Button();
            this.btnMoto = new System.Windows.Forms.Button();
            this.btnGetTransaction = new System.Windows.Forms.Button();
            this.btnSettle = new System.Windows.Forms.Button();
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.lbTransactions = new System.Windows.Forms.Label();
            this.richtextReceipt = new System.Windows.Forms.RichTextBox();
            this.pnlReceipt = new System.Windows.Forms.Panel();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.lblStatusHead = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secretsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOtherTransactions = new System.Windows.Forms.Panel();
            this.btnTerminalSettings = new System.Windows.Forms.Button();
            this.btnHeaderFooter = new System.Windows.Forms.Button();
            this.btnTerminalStatus = new System.Windows.Forms.Button();
            this.btnFreeformReceipt = new System.Windows.Forms.Button();
            this.lblOtherTransactions = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            this.pnlTransactions.SuspendLayout();
            this.pnlReceipt.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlOtherTransactions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSettings.Controls.Add(this.cboPrintMerchantCopy);
            this.pnlSettings.Controls.Add(this.cboxSignFromEftpos);
            this.pnlSettings.Controls.Add(this.cboxReceiptFrom);
            this.pnlSettings.Controls.Add(this.lblSettings);
            this.pnlSettings.Location = new System.Drawing.Point(3, 105);
            this.pnlSettings.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(226, 133);
            this.pnlSettings.TabIndex = 4;
            // 
            // cboPrintMerchantCopy
            // 
            this.cboPrintMerchantCopy.AutoSize = true;
            this.cboPrintMerchantCopy.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPrintMerchantCopy.Location = new System.Drawing.Point(16, 97);
            this.cboPrintMerchantCopy.Name = "cboPrintMerchantCopy";
            this.cboPrintMerchantCopy.Size = new System.Drawing.Size(160, 23);
            this.cboPrintMerchantCopy.TabIndex = 7;
            this.cboPrintMerchantCopy.Text = "Print Merchant Copy";
            this.cboPrintMerchantCopy.UseVisualStyleBackColor = true;
            this.cboPrintMerchantCopy.CheckedChanged += new System.EventHandler(this.cboPrintMerchantCopy_CheckedChanged);
            // 
            // cboxSignFromEftpos
            // 
            this.cboxSignFromEftpos.AutoSize = true;
            this.cboxSignFromEftpos.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxSignFromEftpos.Location = new System.Drawing.Point(16, 74);
            this.cboxSignFromEftpos.Name = "cboxSignFromEftpos";
            this.cboxSignFromEftpos.Size = new System.Drawing.Size(144, 23);
            this.cboxSignFromEftpos.TabIndex = 6;
            this.cboxSignFromEftpos.Text = "Sign From EFTPOS";
            this.cboxSignFromEftpos.UseVisualStyleBackColor = true;
            this.cboxSignFromEftpos.CheckedChanged += new System.EventHandler(this.cboxSignFromEftpos_CheckedChanged);
            // 
            // cboxReceiptFrom
            // 
            this.cboxReceiptFrom.AutoSize = true;
            this.cboxReceiptFrom.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxReceiptFrom.Location = new System.Drawing.Point(16, 51);
            this.cboxReceiptFrom.Name = "cboxReceiptFrom";
            this.cboxReceiptFrom.Size = new System.Drawing.Size(166, 23);
            this.cboxReceiptFrom.TabIndex = 5;
            this.cboxReceiptFrom.Text = "Receipt From EFTPOS";
            this.cboxReceiptFrom.UseVisualStyleBackColor = true;
            this.cboxReceiptFrom.CheckedChanged += new System.EventHandler(this.cboxReceiptFrom_CheckedChanged);
            // 
            // lblSettings
            // 
            this.lblSettings.BackColor = System.Drawing.Color.LightGray;
            this.lblSettings.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(0, 0);
            this.lblSettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(225, 38);
            this.lblSettings.TabIndex = 4;
            this.lblSettings.Text = "EFTPOS Settings";
            this.lblSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTransactions
            // 
            this.pnlTransactions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTransactions.Controls.Add(this.btnRecovery);
            this.pnlTransactions.Controls.Add(this.btnSettleEnq);
            this.pnlTransactions.Controls.Add(this.btnCashout);
            this.pnlTransactions.Controls.Add(this.btnMoto);
            this.pnlTransactions.Controls.Add(this.btnGetTransaction);
            this.pnlTransactions.Controls.Add(this.btnSettle);
            this.pnlTransactions.Controls.Add(this.btnRefund);
            this.pnlTransactions.Controls.Add(this.btnPurchase);
            this.pnlTransactions.Controls.Add(this.lbTransactions);
            this.pnlTransactions.Location = new System.Drawing.Point(3, 240);
            this.pnlTransactions.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlTransactions.Name = "pnlTransactions";
            this.pnlTransactions.Size = new System.Drawing.Size(226, 208);
            this.pnlTransactions.TabIndex = 0;
            // 
            // btnRecovery
            // 
            this.btnRecovery.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecovery.Location = new System.Drawing.Point(115, 164);
            this.btnRecovery.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnRecovery.Name = "btnRecovery";
            this.btnRecovery.Size = new System.Drawing.Size(95, 35);
            this.btnRecovery.TabIndex = 14;
            this.btnRecovery.Text = "Recovery";
            this.btnRecovery.UseVisualStyleBackColor = true;
            this.btnRecovery.Click += new System.EventHandler(this.btnRecovery_Click);
            // 
            // btnSettleEnq
            // 
            this.btnSettleEnq.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettleEnq.Location = new System.Drawing.Point(115, 126);
            this.btnSettleEnq.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSettleEnq.Name = "btnSettleEnq";
            this.btnSettleEnq.Size = new System.Drawing.Size(95, 35);
            this.btnSettleEnq.TabIndex = 13;
            this.btnSettleEnq.Text = "Settle Enq";
            this.btnSettleEnq.UseVisualStyleBackColor = true;
            this.btnSettleEnq.Click += new System.EventHandler(this.btnSettleEnq_Click);
            // 
            // btnCashout
            // 
            this.btnCashout.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashout.Location = new System.Drawing.Point(115, 88);
            this.btnCashout.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnCashout.Name = "btnCashout";
            this.btnCashout.Size = new System.Drawing.Size(95, 35);
            this.btnCashout.TabIndex = 12;
            this.btnCashout.Text = "Cash Out";
            this.btnCashout.UseVisualStyleBackColor = true;
            this.btnCashout.Click += new System.EventHandler(this.btnCashout_Click);
            // 
            // btnMoto
            // 
            this.btnMoto.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoto.Location = new System.Drawing.Point(115, 50);
            this.btnMoto.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnMoto.Name = "btnMoto";
            this.btnMoto.Size = new System.Drawing.Size(95, 35);
            this.btnMoto.TabIndex = 11;
            this.btnMoto.Text = "MOTO";
            this.btnMoto.UseVisualStyleBackColor = true;
            this.btnMoto.Click += new System.EventHandler(this.btnMoto_Click);
            // 
            // btnGetTransaction
            // 
            this.btnGetTransaction.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetTransaction.Location = new System.Drawing.Point(16, 164);
            this.btnGetTransaction.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetTransaction.Name = "btnGetTransaction";
            this.btnGetTransaction.Size = new System.Drawing.Size(95, 35);
            this.btnGetTransaction.TabIndex = 10;
            this.btnGetTransaction.Text = "Get Tx";
            this.btnGetTransaction.UseVisualStyleBackColor = true;
            this.btnGetTransaction.Click += new System.EventHandler(this.btnGetTransaction_Click);
            // 
            // btnSettle
            // 
            this.btnSettle.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettle.Location = new System.Drawing.Point(16, 126);
            this.btnSettle.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(95, 35);
            this.btnSettle.TabIndex = 9;
            this.btnSettle.Text = "Settle";
            this.btnSettle.UseVisualStyleBackColor = true;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefund.Location = new System.Drawing.Point(16, 88);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(95, 35);
            this.btnRefund.TabIndex = 8;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = true;
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnPurchase
            // 
            this.btnPurchase.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchase.Location = new System.Drawing.Point(16, 50);
            this.btnPurchase.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(95, 35);
            this.btnPurchase.TabIndex = 7;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // lbTransactions
            // 
            this.lbTransactions.BackColor = System.Drawing.Color.LightGray;
            this.lbTransactions.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTransactions.Location = new System.Drawing.Point(0, 0);
            this.lbTransactions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTransactions.Name = "lbTransactions";
            this.lbTransactions.Size = new System.Drawing.Size(225, 38);
            this.lbTransactions.TabIndex = 5;
            this.lbTransactions.Text = "Transactions";
            this.lbTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richtextReceipt
            // 
            this.richtextReceipt.Location = new System.Drawing.Point(2, 40);
            this.richtextReceipt.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.richtextReceipt.Name = "richtextReceipt";
            this.richtextReceipt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richtextReceipt.Size = new System.Drawing.Size(295, 527);
            this.richtextReceipt.TabIndex = 6;
            this.richtextReceipt.Text = "";
            // 
            // pnlReceipt
            // 
            this.pnlReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReceipt.Controls.Add(this.lblReceipt);
            this.pnlReceipt.Controls.Add(this.richtextReceipt);
            this.pnlReceipt.Location = new System.Drawing.Point(233, 38);
            this.pnlReceipt.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlReceipt.Name = "pnlReceipt";
            this.pnlReceipt.Size = new System.Drawing.Size(298, 571);
            this.pnlReceipt.TabIndex = 7;
            // 
            // lblReceipt
            // 
            this.lblReceipt.BackColor = System.Drawing.Color.LightGray;
            this.lblReceipt.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceipt.Location = new System.Drawing.Point(0, 0);
            this.lblReceipt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(297, 39);
            this.lblReceipt.TabIndex = 5;
            this.lblReceipt.Text = "Receipt";
            this.lblReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusHead
            // 
            this.lblStatusHead.BackColor = System.Drawing.Color.LightGray;
            this.lblStatusHead.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusHead.Location = new System.Drawing.Point(0, -1);
            this.lblStatusHead.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatusHead.Name = "lblStatusHead";
            this.lblStatusHead.Size = new System.Drawing.Size(225, 38);
            this.lblStatusHead.TabIndex = 0;
            this.lblStatusHead.Text = "Status";
            this.lblStatusHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(1, 37);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(224, 26);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Controls.Add(this.lblStatusHead);
            this.pnlStatus.Location = new System.Drawing.Point(3, 38);
            this.pnlStatus.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(226, 65);
            this.pnlStatus.TabIndex = 5;
            this.pnlStatus.Tag = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.secretsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(535, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.settingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.settingsToolStripMenuItem.Text = "Pairing Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // secretsToolStripMenuItem
            // 
            this.secretsToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.secretsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.secretsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.secretsToolStripMenuItem.Name = "secretsToolStripMenuItem";
            this.secretsToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.secretsToolStripMenuItem.Text = "Secrets";
            this.secretsToolStripMenuItem.Click += new System.EventHandler(this.secretsToolStripMenuItem_Click);
            // 
            // pnlOtherTransactions
            // 
            this.pnlOtherTransactions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOtherTransactions.Controls.Add(this.btnTerminalSettings);
            this.pnlOtherTransactions.Controls.Add(this.btnHeaderFooter);
            this.pnlOtherTransactions.Controls.Add(this.btnTerminalStatus);
            this.pnlOtherTransactions.Controls.Add(this.btnFreeformReceipt);
            this.pnlOtherTransactions.Controls.Add(this.lblOtherTransactions);
            this.pnlOtherTransactions.Location = new System.Drawing.Point(3, 450);
            this.pnlOtherTransactions.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pnlOtherTransactions.Name = "pnlOtherTransactions";
            this.pnlOtherTransactions.Size = new System.Drawing.Size(226, 156);
            this.pnlOtherTransactions.TabIndex = 9;
            // 
            // btnTerminalSettings
            // 
            this.btnTerminalSettings.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminalSettings.Location = new System.Drawing.Point(115, 104);
            this.btnTerminalSettings.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnTerminalSettings.Name = "btnTerminalSettings";
            this.btnTerminalSettings.Size = new System.Drawing.Size(95, 46);
            this.btnTerminalSettings.TabIndex = 12;
            this.btnTerminalSettings.Text = "Terminal Settings";
            this.btnTerminalSettings.UseVisualStyleBackColor = true;
            this.btnTerminalSettings.Click += new System.EventHandler(this.btnTerminalSettings_Click);
            // 
            // btnHeaderFooter
            // 
            this.btnHeaderFooter.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaderFooter.Location = new System.Drawing.Point(115, 50);
            this.btnHeaderFooter.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnHeaderFooter.Name = "btnHeaderFooter";
            this.btnHeaderFooter.Size = new System.Drawing.Size(95, 52);
            this.btnHeaderFooter.TabIndex = 11;
            this.btnHeaderFooter.Text = "Header Footer";
            this.btnHeaderFooter.UseVisualStyleBackColor = true;
            this.btnHeaderFooter.Click += new System.EventHandler(this.btnHeaderFooter_Click);
            // 
            // btnTerminalStatus
            // 
            this.btnTerminalStatus.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminalStatus.Location = new System.Drawing.Point(16, 104);
            this.btnTerminalStatus.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnTerminalStatus.Name = "btnTerminalStatus";
            this.btnTerminalStatus.Size = new System.Drawing.Size(95, 46);
            this.btnTerminalStatus.TabIndex = 8;
            this.btnTerminalStatus.Text = "Terminal Status";
            this.btnTerminalStatus.UseVisualStyleBackColor = true;
            this.btnTerminalStatus.Click += new System.EventHandler(this.btnTerminalStatus_Click);
            // 
            // btnFreeformReceipt
            // 
            this.btnFreeformReceipt.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFreeformReceipt.Location = new System.Drawing.Point(16, 50);
            this.btnFreeformReceipt.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnFreeformReceipt.Name = "btnFreeformReceipt";
            this.btnFreeformReceipt.Size = new System.Drawing.Size(95, 52);
            this.btnFreeformReceipt.TabIndex = 7;
            this.btnFreeformReceipt.Text = "Freeform Receipt";
            this.btnFreeformReceipt.UseVisualStyleBackColor = true;
            this.btnFreeformReceipt.Click += new System.EventHandler(this.btnFreeformReceipt_Click);
            // 
            // lblOtherTransactions
            // 
            this.lblOtherTransactions.BackColor = System.Drawing.Color.LightGray;
            this.lblOtherTransactions.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherTransactions.Location = new System.Drawing.Point(0, 0);
            this.lblOtherTransactions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOtherTransactions.Name = "lblOtherTransactions";
            this.lblOtherTransactions.Size = new System.Drawing.Size(225, 38);
            this.lblOtherTransactions.TabIndex = 5;
            this.lblOtherTransactions.Text = "Other Transactions";
            this.lblOtherTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransactionForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 611);
            this.Controls.Add(this.pnlOtherTransactions);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlReceipt);
            this.Controls.Add(this.pnlTransactions);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlSettings);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransactionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly Payment - RamenPos - Transactions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.TransactionForm_Load);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlTransactions.ResumeLayout(false);
            this.pnlReceipt.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlOtherTransactions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Panel pnlSettings;
        internal System.Windows.Forms.Panel pnlTransactions;
        internal System.Windows.Forms.Label lblSettings;
        internal System.Windows.Forms.Label lbTransactions;
        internal System.Windows.Forms.Button btnSettle;
        internal System.Windows.Forms.Button btnRefund;
        internal System.Windows.Forms.Button btnPurchase;
        internal System.Windows.Forms.RichTextBox richtextReceipt;
        internal System.Windows.Forms.Panel pnlReceipt;
        internal System.Windows.Forms.Label lblReceipt;
        internal System.Windows.Forms.Button btnGetTransaction;
        internal System.Windows.Forms.Button btnRecovery;
        internal System.Windows.Forms.Button btnSettleEnq;
        internal System.Windows.Forms.Button btnCashout;
        internal System.Windows.Forms.Button btnMoto;
        internal System.Windows.Forms.Label lblStatusHead;
        internal System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.Panel pnlStatus;
        internal System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        internal System.Windows.Forms.Panel pnlOtherTransactions;
        internal System.Windows.Forms.Button btnTerminalSettings;
        internal System.Windows.Forms.Button btnHeaderFooter;
        internal System.Windows.Forms.Button btnTerminalStatus;
        internal System.Windows.Forms.Button btnFreeformReceipt;
        internal System.Windows.Forms.Label lblOtherTransactions;
        internal System.Windows.Forms.CheckBox cboPrintMerchantCopy;
        internal System.Windows.Forms.CheckBox cboxSignFromEftpos;
        internal System.Windows.Forms.CheckBox cboxReceiptFrom;
        internal System.Windows.Forms.ToolStripMenuItem secretsToolStripMenuItem;
    }
}