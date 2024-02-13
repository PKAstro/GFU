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
            this.components = new System.ComponentModel.Container();
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
            this.ckShowBeta = new System.Windows.Forms.CheckBox();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbClearHC = new System.Windows.Forms.Button();
            this.pbClearGemini = new System.Windows.Forms.Button();
            this.lbHCFiles = new System.Windows.Forms.ListBox();
            this.lbGeminiFiles = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHCDD = new System.Windows.Forms.Label();
            this.lbGeminiDD = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.lbDownload.Location = new System.Drawing.Point(1, 13);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(145, 88);
            this.lbDownload.TabIndex = 2;
            this.lbDownload.Text = "Download";
            this.lbDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progDownload
            // 
            this.progDownload.Location = new System.Drawing.Point(152, 44);
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
            this.statDownload.Location = new System.Drawing.Point(461, 13);
            this.statDownload.Name = "statDownload";
            this.statDownload.Size = new System.Drawing.Size(145, 88);
            this.statDownload.TabIndex = 4;
            this.statDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statDecompress
            // 
            this.statDecompress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statDecompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statDecompress.Location = new System.Drawing.Point(461, 118);
            this.statDecompress.Name = "statDecompress";
            this.statDecompress.Size = new System.Drawing.Size(145, 88);
            this.statDecompress.TabIndex = 7;
            this.statDecompress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progDecompress
            // 
            this.progDecompress.Location = new System.Drawing.Point(152, 149);
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
            this.lbDecompress.Location = new System.Drawing.Point(1, 118);
            this.lbDecompress.Name = "lbDecompress";
            this.lbDecompress.Size = new System.Drawing.Size(145, 88);
            this.lbDecompress.TabIndex = 5;
            this.lbDecompress.Text = "Extract";
            this.lbDecompress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDownloadPercent
            // 
            this.lbDownloadPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDownloadPercent.Location = new System.Drawing.Point(248, 18);
            this.lbDownloadPercent.Name = "lbDownloadPercent";
            this.lbDownloadPercent.Size = new System.Drawing.Size(100, 23);
            this.lbDownloadPercent.TabIndex = 8;
            this.lbDownloadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDecompressPercent
            // 
            this.lbDecompressPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDecompressPercent.Location = new System.Drawing.Point(248, 123);
            this.lbDecompressPercent.Name = "lbDecompressPercent";
            this.lbDecompressPercent.Size = new System.Drawing.Size(100, 23);
            this.lbDecompressPercent.TabIndex = 9;
            this.lbDecompressPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUploadPercent
            // 
            this.lbUploadPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUploadPercent.Location = new System.Drawing.Point(221, 400);
            this.lbUploadPercent.Name = "lbUploadPercent";
            this.lbUploadPercent.Size = new System.Drawing.Size(210, 23);
            this.lbUploadPercent.TabIndex = 13;
            this.lbUploadPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statUpload
            // 
            this.statUpload.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.statUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statUpload.Location = new System.Drawing.Point(480, 395);
            this.statUpload.Name = "statUpload";
            this.statUpload.Size = new System.Drawing.Size(145, 88);
            this.statUpload.TabIndex = 12;
            this.statUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progUpload
            // 
            this.progUpload.Location = new System.Drawing.Point(171, 426);
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
            this.lbUpload.Location = new System.Drawing.Point(20, 395);
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
            this.panel1.Controls.Add(this.ckShowBeta);
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
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ckShowBeta
            // 
            this.ckShowBeta.AutoSize = true;
            this.ckShowBeta.Location = new System.Drawing.Point(148, 108);
            this.ckShowBeta.Name = "ckShowBeta";
            this.ckShowBeta.Size = new System.Drawing.Size(120, 17);
            this.ckShowBeta.TabIndex = 11;
            this.ckShowBeta.Text = "Show Beta versions";
            this.toolTip1.SetToolTip(this.ckShowBeta, "Show beta versions of firmware in Firmware selector");
            this.ckShowBeta.UseVisualStyleBackColor = true;
            this.ckShowBeta.CheckedChanged += new System.EventHandler(this.ckShowBeta_CheckedChanged);
            // 
            // chkVideos
            // 
            this.chkVideos.AutoSize = true;
            this.chkVideos.Location = new System.Drawing.Point(78, 85);
            this.chkVideos.Name = "chkVideos";
            this.chkVideos.Size = new System.Drawing.Size(58, 17);
            this.chkVideos.TabIndex = 10;
            this.chkVideos.Text = "Videos";
            this.toolTip1.SetToolTip(this.chkVideos, "Upload Videos to the Gemini");
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
            this.toolTip1.SetToolTip(this.ckCat, "Update Catalogs");
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
            this.toolTip1.SetToolTip(this.ckFlash, "Flash Gemini Firmware");
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
            this.toolTip1.SetToolTip(this.ckHC, "Update Gemini Hand Controller");
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
            this.toolTip1.SetToolTip(this.ckGemini, "Uplate Gemini HTML files");
            this.ckGemini.UseVisualStyleBackColor = true;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(72, 55);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(165, 20);
            this.txtPwd.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtPwd, "Gemini login password");
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
            this.toolTip1.SetToolTip(this.txtUser, "Gemini login user ID");
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
            this.toolTip1.SetToolTip(this.txtIP, "Gemini IP address");
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
            this.label4.Location = new System.Drawing.Point(228, 666);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Copyright (c) 2014-24 by Paul Kanevsky";
            // 
            // cbZip
            // 
            this.cbZip.FormattingEnabled = true;
            this.cbZip.Location = new System.Drawing.Point(151, 80);
            this.cbZip.Name = "cbZip";
            this.cbZip.Size = new System.Drawing.Size(304, 24);
            this.cbZip.TabIndex = 17;
            // 
            // lbReboot
            // 
            this.lbReboot.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReboot.Location = new System.Drawing.Point(480, 498);
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
            this.lbFlash.Location = new System.Drawing.Point(20, 498);
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
            this.lbSRAM.Location = new System.Drawing.Point(251, 498);
            this.lbSRAM.Name = "lbSRAM";
            this.lbSRAM.Size = new System.Drawing.Size(145, 88);
            this.lbSRAM.TabIndex = 21;
            this.lbSRAM.Text = "SRAM Reset";
            this.lbSRAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVer
            // 
            this.lblVer.Location = new System.Drawing.Point(482, 661);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(140, 23);
            this.lblVer.TabIndex = 22;
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 661);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Format SD Card!";
            this.toolTip1.SetToolTip(this.button2, "Format Gemini SD card -- THIS WILL ERASE ALL FILES!");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbStat
            // 
            this.lbStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStat.ForeColor = System.Drawing.Color.Blue;
            this.lbStat.Location = new System.Drawing.Point(22, 596);
            this.lbStat.Name = "lbStat";
            this.lbStat.Size = new System.Drawing.Size(603, 57);
            this.lbStat.TabIndex = 24;
            this.lbStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(16, 151);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(614, 241);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.cbVersion);
            this.tabPage1.Controls.Add(this.statDownload);
            this.tabPage1.Controls.Add(this.lbDownload);
            this.tabPage1.Controls.Add(this.progDownload);
            this.tabPage1.Controls.Add(this.lbDecompress);
            this.tabPage1.Controls.Add(this.progDecompress);
            this.tabPage1.Controls.Add(this.statDecompress);
            this.tabPage1.Controls.Add(this.lbDownloadPercent);
            this.tabPage1.Controls.Add(this.cbZip);
            this.tabPage1.Controls.Add(this.lbDecompressPercent);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(606, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Automatic";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Firmware Version:";
            // 
            // cbVersion
            // 
            this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Items.AddRange(new object[] {
            "Gemini L6",
            "Gemini L5"});
            this.cbVersion.Location = new System.Drawing.Point(274, 14);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(181, 24);
            this.cbVersion.TabIndex = 20;
            this.toolTip1.SetToolTip(this.cbVersion, "Select firmware to update");
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.pbClearHC);
            this.tabPage2.Controls.Add(this.pbClearGemini);
            this.tabPage2.Controls.Add(this.lbHCFiles);
            this.tabPage2.Controls.Add(this.lbGeminiFiles);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.lblHCDD);
            this.tabPage2.Controls.Add(this.lbGeminiDD);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(606, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            // 
            // pbClearHC
            // 
            this.pbClearHC.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbClearHC.BackgroundImage")));
            this.pbClearHC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbClearHC.Location = new System.Drawing.Point(574, 151);
            this.pbClearHC.Name = "pbClearHC";
            this.pbClearHC.Size = new System.Drawing.Size(32, 32);
            this.pbClearHC.TabIndex = 17;
            this.pbClearHC.UseVisualStyleBackColor = true;
            this.pbClearHC.Click += new System.EventHandler(this.pbClearHC_Click);
            // 
            // pbClearGemini
            // 
            this.pbClearGemini.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbClearGemini.BackgroundImage")));
            this.pbClearGemini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbClearGemini.Location = new System.Drawing.Point(574, 70);
            this.pbClearGemini.Margin = new System.Windows.Forms.Padding(0);
            this.pbClearGemini.Name = "pbClearGemini";
            this.pbClearGemini.Size = new System.Drawing.Size(32, 32);
            this.pbClearGemini.TabIndex = 16;
            this.pbClearGemini.UseVisualStyleBackColor = true;
            this.pbClearGemini.Click += new System.EventHandler(this.pbClearGemini_Click);
            // 
            // lbHCFiles
            // 
            this.lbHCFiles.AllowDrop = true;
            this.lbHCFiles.FormattingEnabled = true;
            this.lbHCFiles.ItemHeight = 16;
            this.lbHCFiles.Location = new System.Drawing.Point(123, 129);
            this.lbHCFiles.Name = "lbHCFiles";
            this.lbHCFiles.Size = new System.Drawing.Size(448, 68);
            this.lbHCFiles.TabIndex = 15;
            this.lbHCFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbHCFiles_DragDrop);
            this.lbHCFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.lbHCFiles_DragOver);
            this.lbHCFiles.DoubleClick += new System.EventHandler(this.lbHCFiles_DoubleClick);
            // 
            // lbGeminiFiles
            // 
            this.lbGeminiFiles.AllowDrop = true;
            this.lbGeminiFiles.FormattingEnabled = true;
            this.lbGeminiFiles.ItemHeight = 16;
            this.lbGeminiFiles.Location = new System.Drawing.Point(123, 51);
            this.lbGeminiFiles.Name = "lbGeminiFiles";
            this.lbGeminiFiles.Size = new System.Drawing.Size(448, 68);
            this.lbGeminiFiles.TabIndex = 14;
            this.lbGeminiFiles.SelectedIndexChanged += new System.EventHandler(this.lbGeminiFiles_SelectedIndexChanged);
            this.lbGeminiFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbGeminiFiles_DragDrop);
            this.lbGeminiFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.lbGeminiFiles_DragOver);
            this.lbGeminiFiles.DoubleClick += new System.EventHandler(this.lbGeminiFiles_DoubleClick);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkRed;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(597, 41);
            this.label7.TabIndex = 13;
            this.label7.Text = "Drag and drop files you want to update to the appropriate list below or click the" +
    " button to select.\r\nThen press Start.";
            // 
            // lblHCDD
            // 
            this.lblHCDD.AllowDrop = true;
            this.lblHCDD.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblHCDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHCDD.Location = new System.Drawing.Point(6, 132);
            this.lblHCDD.Name = "lblHCDD";
            this.lblHCDD.Size = new System.Drawing.Size(111, 66);
            this.lblHCDD.TabIndex = 12;
            this.lblHCDD.Text = "Hand Controller";
            this.lblHCDD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHCDD.Click += new System.EventHandler(this.lblHCDD_Click);
            this.lblHCDD.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblHCDD_DragDrop);
            this.lblHCDD.DragOver += new System.Windows.Forms.DragEventHandler(this.lblHCDD_DragOver);
            // 
            // lbGeminiDD
            // 
            this.lbGeminiDD.AllowDrop = true;
            this.lbGeminiDD.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbGeminiDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGeminiDD.Location = new System.Drawing.Point(6, 51);
            this.lbGeminiDD.Name = "lbGeminiDD";
            this.lbGeminiDD.Size = new System.Drawing.Size(111, 66);
            this.lbGeminiDD.TabIndex = 11;
            this.lbGeminiDD.Text = "Gemini CPU";
            this.lbGeminiDD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbGeminiDD.Click += new System.EventHandler(this.lbGeminiDD_Click);
            this.lbGeminiDD.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbGeminiDD_DragDrop);
            this.lbGeminiDD.DragOver += new System.Windows.Forms.DragEventHandler(this.lbGeminiDD_DragOver);
            // 
            // GFUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 701);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbStat);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lbSRAM);
            this.Controls.Add(this.lbReboot);
            this.Controls.Add(this.lbFlash);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbUploadPercent);
            this.Controls.Add(this.statUpload);
            this.Controls.Add(this.progUpload);
            this.Controls.Add(this.lbUpload);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblHCDD;
        private System.Windows.Forms.Label lbGeminiDD;
        private System.Windows.Forms.ListBox lbHCFiles;
        private System.Windows.Forms.ListBox lbGeminiFiles;
        private System.Windows.Forms.Button pbClearHC;
        private System.Windows.Forms.Button pbClearGemini;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckShowBeta;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

