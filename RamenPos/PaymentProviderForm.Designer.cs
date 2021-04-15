
namespace RamenPos
{
    partial class PaymentProviderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentProviderForm));
            this.lbPaymentProviders = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.rbOtherPaymentProvider = new System.Windows.Forms.RadioButton();
            this.txtOtherPaymentProvider = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbPaymentProviders
            // 
            this.lbPaymentProviders.FormattingEnabled = true;
            this.lbPaymentProviders.Location = new System.Drawing.Point(12, 12);
            this.lbPaymentProviders.Name = "lbPaymentProviders";
            this.lbPaymentProviders.Size = new System.Drawing.Size(259, 134);
            this.lbPaymentProviders.TabIndex = 0;
            this.lbPaymentProviders.SelectedIndexChanged += new System.EventHandler(this.lbPaymentProviders_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(12, 196);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(264, 36);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // rbOtherPaymentProvider
            // 
            this.rbOtherPaymentProvider.AutoSize = true;
            this.rbOtherPaymentProvider.Location = new System.Drawing.Point(13, 167);
            this.rbOtherPaymentProvider.Name = "rbOtherPaymentProvider";
            this.rbOtherPaymentProvider.Size = new System.Drawing.Size(14, 13);
            this.rbOtherPaymentProvider.TabIndex = 2;
            this.rbOtherPaymentProvider.TabStop = true;
            this.rbOtherPaymentProvider.UseVisualStyleBackColor = true;
            this.rbOtherPaymentProvider.CheckedChanged += new System.EventHandler(this.rbOtherPaymentProvider_CheckedChanged);
            // 
            // txtOtherPaymentProvider
            // 
            this.txtOtherPaymentProvider.Enabled = false;
            this.txtOtherPaymentProvider.Location = new System.Drawing.Point(33, 164);
            this.txtOtherPaymentProvider.Name = "txtOtherPaymentProvider";
            this.txtOtherPaymentProvider.Size = new System.Drawing.Size(238, 20);
            this.txtOtherPaymentProvider.TabIndex = 3;
            this.txtOtherPaymentProvider.Text = "Other";
            // 
            // PaymentProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 244);
            this.Controls.Add(this.txtOtherPaymentProvider);
            this.Controls.Add(this.rbOtherPaymentProvider);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbPaymentProviders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentProviderForm";
            this.Text = "Select a Payment Provider";
            this.Load += new System.EventHandler(this.PaymentProviderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbPaymentProviders;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.RadioButton rbOtherPaymentProvider;
        private System.Windows.Forms.TextBox txtOtherPaymentProvider;
    }
}