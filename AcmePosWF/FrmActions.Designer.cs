namespace AcmePosWF
{
    partial class FrmActions
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
            this.pnlFlow = new System.Windows.Forms.Panel();
            this.listBoxFlow = new System.Windows.Forms.ListBox();
            this.lblFlowStatus = new System.Windows.Forms.Label();
            this.lblFlowMessage = new System.Windows.Forms.Label();
            this.lbFlow = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblAmount = new System.Windows.Forms.Label();
            this.textAmount = new System.Windows.Forms.TextBox();
            this.btnAction3 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.pnlFlow.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFlow
            // 
            this.pnlFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFlow.Controls.Add(this.listBoxFlow);
            this.pnlFlow.Controls.Add(this.lblFlowStatus);
            this.pnlFlow.Controls.Add(this.lblFlowMessage);
            this.pnlFlow.Controls.Add(this.lbFlow);
            this.pnlFlow.Location = new System.Drawing.Point(13, 14);
            this.pnlFlow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlFlow.Name = "pnlFlow";
            this.pnlFlow.Size = new System.Drawing.Size(437, 357);
            this.pnlFlow.TabIndex = 8;
            // 
            // listBoxFlow
            // 
            this.listBoxFlow.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFlow.FormattingEnabled = true;
            this.listBoxFlow.ItemHeight = 22;
            this.listBoxFlow.Location = new System.Drawing.Point(3, 107);
            this.listBoxFlow.Name = "listBoxFlow";
            this.listBoxFlow.Size = new System.Drawing.Size(429, 246);
            this.listBoxFlow.TabIndex = 9;
            // 
            // lblFlowStatus
            // 
            this.lblFlowStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblFlowStatus.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlowStatus.ForeColor = System.Drawing.Color.Red;
            this.lblFlowStatus.Location = new System.Drawing.Point(216, 0);
            this.lblFlowStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblFlowStatus.Name = "lblFlowStatus";
            this.lblFlowStatus.Size = new System.Drawing.Size(220, 52);
            this.lblFlowStatus.TabIndex = 8;
            this.lblFlowStatus.Text = "Idle";
            this.lblFlowStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFlowMessage
            // 
            this.lblFlowMessage.BackColor = System.Drawing.SystemColors.Control;
            this.lblFlowMessage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlowMessage.Location = new System.Drawing.Point(4, 52);
            this.lblFlowMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblFlowMessage.Name = "lblFlowMessage";
            this.lblFlowMessage.Size = new System.Drawing.Size(432, 52);
            this.lblFlowMessage.TabIndex = 7;
            // 
            // lbFlow
            // 
            this.lbFlow.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbFlow.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFlow.Location = new System.Drawing.Point(0, 0);
            this.lbFlow.Margin = new System.Windows.Forms.Padding(0);
            this.lbFlow.Name = "lbFlow";
            this.lbFlow.Size = new System.Drawing.Size(216, 52);
            this.lbFlow.TabIndex = 5;
            this.lbFlow.Text = "Flow :";
            this.lbFlow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.lblAmount);
            this.pnlActions.Controls.Add(this.textAmount);
            this.pnlActions.Controls.Add(this.btnAction3);
            this.pnlActions.Controls.Add(this.btnAction2);
            this.pnlActions.Controls.Add(this.btnAction1);
            this.pnlActions.Location = new System.Drawing.Point(13, 375);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(437, 197);
            this.pnlActions.TabIndex = 9;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(31, 13);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(136, 24);
            this.lblAmount.TabIndex = 10;
            this.lblAmount.Text = "Amout (Cents):";
            this.lblAmount.Visible = false;
            // 
            // textAmount
            // 
            this.textAmount.Location = new System.Drawing.Point(214, 10);
            this.textAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textAmount.Name = "textAmount";
            this.textAmount.Size = new System.Drawing.Size(188, 32);
            this.textAmount.TabIndex = 11;
            this.textAmount.Visible = false;
            // 
            // btnAction3
            // 
            this.btnAction3.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction3.Location = new System.Drawing.Point(35, 146);
            this.btnAction3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Size = new System.Drawing.Size(367, 38);
            this.btnAction3.TabIndex = 9;
            this.btnAction3.UseVisualStyleBackColor = true;
            this.btnAction3.Visible = false;
            this.btnAction3.Click += new System.EventHandler(this.btnAction3_Click);
            // 
            // btnAction2
            // 
            this.btnAction2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAction2.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction2.Location = new System.Drawing.Point(35, 100);
            this.btnAction2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new System.Drawing.Size(367, 38);
            this.btnAction2.TabIndex = 8;
            this.btnAction2.UseVisualStyleBackColor = true;
            this.btnAction2.Visible = false;
            this.btnAction2.Click += new System.EventHandler(this.btnAction2_Click);
            // 
            // btnAction1
            // 
            this.btnAction1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAction1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction1.Location = new System.Drawing.Point(35, 50);
            this.btnAction1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(367, 38);
            this.btnAction1.TabIndex = 7;
            this.btnAction1.UseVisualStyleBackColor = true;
            this.btnAction1.Visible = false;
            this.btnAction1.Click += new System.EventHandler(this.btnAction1_Click);
            // 
            // FrmActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 580);
            this.ControlBox = false;
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlFlow);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmActions";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actions";
            this.Load += new System.EventHandler(this.FrmActions_Load);
            this.Leave += new System.EventHandler(this.FrmActions_Leave);
            this.pnlFlow.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFlow;
        public System.Windows.Forms.Label lbFlow;
        public System.Windows.Forms.Label lblFlowMessage;
        private System.Windows.Forms.Panel pnlActions;
        public System.Windows.Forms.Button btnAction3;
        public System.Windows.Forms.Button btnAction2;
        public System.Windows.Forms.Button btnAction1;
        public System.Windows.Forms.Label lblAmount;
        public System.Windows.Forms.TextBox textAmount;
        public System.Windows.Forms.Label lblFlowStatus;
        public System.Windows.Forms.ListBox listBoxFlow;
    }
}