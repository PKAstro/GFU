namespace GFU
{
    partial class frmFirmware
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
            this.pbOK = new System.Windows.Forms.Button();
            this.pbCancel = new System.Windows.Forms.Button();
            this.lbFirmware = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOK.Location = new System.Drawing.Point(260, 278);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(75, 23);
            this.pbOK.TabIndex = 0;
            this.pbOK.Text = "OK";
            this.pbOK.UseVisualStyleBackColor = true;
            this.pbOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pbCancel.Location = new System.Drawing.Point(341, 278);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(75, 23);
            this.pbCancel.TabIndex = 1;
            this.pbCancel.Text = "Cancel";
            this.pbCancel.UseVisualStyleBackColor = true;
            // 
            // lbFirmware
            // 
            this.lbFirmware.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFirmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirmware.FormattingEnabled = true;
            this.lbFirmware.ItemHeight = 16;
            this.lbFirmware.Location = new System.Drawing.Point(0, 0);
            this.lbFirmware.Name = "lbFirmware";
            this.lbFirmware.Size = new System.Drawing.Size(429, 260);
            this.lbFirmware.TabIndex = 2;
            this.lbFirmware.SelectedIndexChanged += new System.EventHandler(this.lbFirmware_SelectedIndexChanged);
            // 
            // frmFirmware
            // 
            this.AcceptButton = this.pbOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.pbCancel;
            this.ClientSize = new System.Drawing.Size(428, 308);
            this.Controls.Add(this.lbFirmware);
            this.Controls.Add(this.pbCancel);
            this.Controls.Add(this.pbOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFirmware";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Firmware to flash";
            this.Load += new System.EventHandler(this.frmFirmware_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pbOK;
        private System.Windows.Forms.Button pbCancel;
        private System.Windows.Forms.ListBox lbFirmware;
    }
}