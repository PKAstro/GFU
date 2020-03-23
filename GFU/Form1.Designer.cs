namespace GFU
{
    partial class GFUForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GFUForm));
            this.button1 = new System.Windows.Forms.Button();
            this.lbDownload = new System.Windows.Forms.Label();
            this.progDownload = new System.Windows.Forms.ProgressBar();
            this.statDownload = new System.Windows.Forms.Label();
            this.statDecompress = new System.Windows.Forms.Label();
            this.progDecompress = new System.Windows.Forms.ProgressBar();
            this.lbDecompress = new System.Windows.Forms.Label();
            this.lbDownloadPercent = new System.Windows.Forms.Label();
            this.lbDecompressPercent = new System.Windows.Forms.Label();
            this.lbUploadPercent = new System.Windows.Forms.Label();
            this.statUpload = new System.Windows.Forms.Label();
            this.progUpload = new System.Windows.Forms.ProgressBar();
            this.lbUpload = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkVideos = new System.Windows.Forms.CheckBox();
            this.ckCat = new System.Windows.Forms.CheckBox();
            this.ckFlash = new System.Windows.Forms.CheckBox();
            this.ckHC = new System.Windows.Forms.CheckBox();
            this.ckGemini = new System.Windows.Forms.CheckBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbZip = new System.Windows.Forms.ComboBox();
            this.lbReboot = new System.Windows.Forms.Label();
            this.lbFlash = new System.Windows.Forms.Label();
            this.lbSRAM = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lbStat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(20, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 84);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbDownload
            // 
            this.lbDownload.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownload.Location = new System.Drawing.Point(20, 152);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(145, 88);
            this.lbDownload.TabIndex = 2;
            this.lbDownload.Text = "Download";
            this.lbDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progDownload
            // 
            this.progDownload.Location = new System.Drawing.Point(171, 183);
            this.progDownload.Maximum = 1000;
            this.progDownload.Name = "progDownload";
            this.progDownload.Size = new System.Drawing.Size(303, 23);
            this.progDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progDownload.TabIndex = 3;
            // 
            // statDownload
            // 
            this.statDownload.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statDownload.Location = new System.Drawing.Point(480, 152);
            this.statDownload.Name = "statDownload";
            this.statDownload.Size = new System.Drawing.Size(145, 88);
            this.statDownload.TabIndex = 4;
            this.statDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statDecompress
            // 
            this.statDecompress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statDecompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statDecompress.Location = new System.Drawing.Point(480, 257);
            this.statDecompress.Name = "statDecompress";
            this.statDecompress.Size = new System.Drawing.Size(145, 88);
            this.statDecompress.TabIndex = 7;
            this.statDecompress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progDecompress
            // 
            this.progDecompress.Location = new System.Drawing.Point(171, 288);
            this.progDecompress.Maximum = 1000;
            this.progDecompress.Name = "progDecompress";
            this.progDecompress.Size = new System.Drawing.Size(303, 23);
            this.progDecompress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progDecompress.TabIndex = 6;
            // 
            // lbDecompress
            // 
            this.lbDecompress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbDecompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDecompress.Location = new System.Drawing.Point(20, 257);
            this.lbDecompress.Name = "lbDecompress";
            this.lbDecompress.Size = new System.Drawing.Size(145, 88);
            this.lbDecompress.TabIndex = 5;
            this.lbDecompress.Text = "Extract";
            this.lbDecompress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDownloadPercent
            // 
            this.lbDownloadPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownloadPercent.Location = new System.Drawing.Point(267, 157);
            this.lbDownloadPercent.Name = "lbDownloadPercent";
            this.lbDownloadPercent.Size = new System.Drawing.Size(100, 23);
            this.lbDownloadPercent.TabIndex = 8;
            this.lbDownloadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDecompressPercent
            // 
            this.lbDecompressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDecompressPercent.Location = new System.Drawing.Point(267, 262);
            this.lbDecompressPercent.Name = "lbDecompressPercent";
            this.lbDecompressPercent.Size = new System.Drawing.Size(100, 23);
            this.lbDecompressPercent.TabIndex = 9;
            this.lbDecompressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUploadPercent
            // 
            this.lbUploadPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUploadPercent.Location = new System.Drawing.Point(221, 365);
            this.lbUploadPercent.Name = "lbUploadPercent";
            this.lbUploadPercent.Size = new System.Drawing.Size(210, 23);
            this.lbUploadPercent.TabIndex = 13;
            this.lbUploadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statUpload
            // 
            this.statUpload.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statUpload.Location = new System.Drawing.Point(480, 360);
            this.statUpload.Name = "statUpload";
            this.statUpload.Size = new System.Drawing.Size(145, 88);
            this.statUpload.TabIndex = 12;
            this.statUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progUpload
            // 
            this.progUpload.Location = new System.Drawing.Point(171, 391);
            this.progUpload.Maximum = 1000;
            this.progUpload.Name = "progUpload";
            this.progUpload.Size = new System.Drawing.Size(303, 23);
            this.progUpload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progUpload.TabIndex = 11;
            // 
            // lbUpload
            // 
            this.lbUpload.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUpload.Location = new System.Drawing.Point(20, 360);
            this.lbUpload.Name = "lbUpload";
            this.lbUpload.Size = new System.Drawing.Size(145, 88);
            this.lbUpload.TabIndex = 10;
            this.lbUpload.Text = "Upload";
            this.lbUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(477, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 99);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkVideos);
            this.panel1.Controls.Add(this.ckCat);
            this.panel1.Controls.Add(this.ckFlash);
            this.panel1.Controls.Add(this.ckHC);
            this.panel1.Controls.Add(this.ckGemini);
            this.panel1.Controls.Add(this.txtPwd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtIP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(187, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 132);
            this.panel1.TabIndex = 15;
            // 
            // chkVideos
            // 
            this.chkVideos.AutoSize = true;
            this.chkVideos.Location = new System.Drawing.Point(78, 85);
            this.chkVideos.Name = "chkVideos";
            this.chkVideos.Size = new System.Drawing.Size(58, 17);
            this.chkVideos.TabIndex = 10;
            this.chkVideos.Text = "Videos";
            this.chkVideos.UseVisualStyleBackColor = true;
            // 
            // ckCat
            // 
            this.ckCat.AutoSize = true;
            this.ckCat.Location = new System.Drawing.Point(78, 108);
            this.ckCat.Name = "ckCat";
            this.ckCat.Size = new System.Drawing.Size(67, 17);
            this.ckCat.TabIndex = 9;
            this.ckCat.Text = "Catalogs";
            this.ckCat.UseVisualStyleBackColor = true;
            // 
            // ckFlash
            // 
            this.ckFlash.AutoSize = true;
            this.ckFlash.Location = new System.Drawing.Point(148, 85);
            this.ckFlash.Name = "ckFlash";
            this.ckFlash.Size = new System.Drawing.Size(96, 17);
            this.ckFlash.TabIndex = 8;
            this.ckFlash.Text = "Flash Firmware";
            this.ckFlash.UseVisualStyleBackColor = true;
            // 
            // ckHC
            // 
            this.ckHC.AutoSize = true;
            this.ckHC.Location = new System.Drawing.Point(7, 108);
            this.ckHC.Name = "ckHC";
            this.ckHC.Size = new System.Drawing.Size(41, 17);
            this.ckHC.TabIndex = 7;
            this.ckHC.Text = "HC";
            this.ckHC.UseVisualStyleBackColor = true;
            // 
            // ckGemini
            // 
            this.ckGemini.AutoSize = true;
            this.ckGemini.Location = new System.Drawing.Point(7, 85);
            this.ckGemini.Name = "ckGemini";
            this.ckGemini.Size = new System.Drawing.Size(58, 17);
            this.ckGemini.TabIndex = 6;
            this.ckGemini.Text = "Gemini";
            this.ckGemini.UseVisualStyleBackColor = true;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(72, 55);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(165, 20);
            this.txtPwd.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(72, 29);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(165, 20);
            this.txtUser.TabIndex = 3;
            this.txtUser.Text = "admin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User:";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(72, 3);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(165, 20);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "192.168.0.111";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 631);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Copyright (c) 2014-20 by Paul Kanevsky";
            // 
            // cbZip
            // 
            this.cbZip.FormattingEnabled = true;
            this.cbZip.Location = new System.Drawing.Point(175, 219);
            this.cbZip.Name = "cbZip";
            this.cbZip.Size = new System.Drawing.Size(299, 21);
            this.cbZip.TabIndex = 17;
            // 
            // lbReboot
            // 
            this.lbReboot.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReboot.Location = new System.Drawing.Point(480, 463);
            this.lbReboot.Name = "lbReboot";
            this.lbReboot.Size = new System.Drawing.Size(145, 88);
            this.lbReboot.TabIndex = 20;
            this.lbReboot.Text = "Reboot";
            this.lbReboot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFlash
            // 
            this.lbFlash.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbFlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFlash.Location = new System.Drawing.Point(20, 463);
            this.lbFlash.Name = "lbFlash";
            this.lbFlash.Size = new System.Drawing.Size(145, 88);
            this.lbFlash.TabIndex = 18;
            this.lbFlash.Text = "Flash";
            this.lbFlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSRAM
            // 
            this.lbSRAM.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbSRAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSRAM.Location = new System.Drawing.Point(251, 463);
            this.lbSRAM.Name = "lbSRAM";
            this.lbSRAM.Size = new System.Drawing.Size(145, 88);
            this.lbSRAM.TabIndex = 21;
            this.lbSRAM.Text = "SRAM Reset";
            this.lbSRAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVer
            // 
            this.lblVer.Location = new System.Drawing.Point(482, 626);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(140, 23);
            this.lblVer.TabIndex = 22;
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 626);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Format SD Card!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbStat
            // 
            this.lbStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStat.ForeColor = System.Drawing.Color.Blue;
            this.lbStat.Location = new System.Drawing.Point(22, 561);
            this.lbStat.Name = "lbStat";
            this.lbStat.Size = new System.Drawing.Size(603, 57);
            this.lbStat.TabIndex = 24;
            this.lbStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GFUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 653);
            this.Controls.Add(this.lbStat);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lbSRAM);
            this.Controls.Add(this.lbReboot);
            this.Controls.Add(this.lbFlash);
            this.Controls.Add(this.cbZip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbUploadPercent);
            this.Controls.Add(this.statUpload);
            this.Controls.Add(this.progUpload);
            this.Controls.Add(this.lbUpload);
            this.Controls.Add(this.lbDecompressPercent);
            this.Controls.Add(this.lbDownloadPercent);
            this.Controls.Add(this.statDecompress);
            this.Controls.Add(this.progDecompress);
            this.Controls.Add(this.lbDecompress);
            this.Controls.Add(this.statDownload);
            this.Controls.Add(this.progDownload);
            this.Controls.Add(this.lbDownload);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GFUForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gemini Firmware Updater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GFUForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GFUForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbDownload;
        private System.Windows.Forms.ProgressBar progDownload;
        private System.Windows.Forms.Label statDownload;
        private System.Windows.Forms.Label statDecompress;
        private System.Windows.Forms.ProgressBar progDecompress;
        private System.Windows.Forms.Label lbDecompress;
        private System.Windows.Forms.Label lbDownloadPercent;
        private System.Windows.Forms.Label lbDecompressPercent;
        private System.Windows.Forms.Label lbUploadPercent;
        private System.Windows.Forms.Label statUpload;
        private System.Windows.Forms.ProgressBar progUpload;
        private System.Windows.Forms.Label lbUpload;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbZip;
        private System.Windows.Forms.CheckBox ckFlash;
        private System.Windows.Forms.CheckBox ckHC;
        private System.Windows.Forms.CheckBox ckGemini;
        private System.Windows.Forms.Label lbReboot;
        private System.Windows.Forms.Label lbFlash;
        private System.Windows.Forms.Label lbSRAM;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.CheckBox ckCat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkVideos;
        private System.Windows.Forms.Label lbStat;
    }
}

