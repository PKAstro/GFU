using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using Ionic.Zip;
using System.Threading;
//using EnterpriseDT.Net.Ftp;

namespace GFU
{
    public partial class GFUForm : Form
    {
        private string CombinedFile = "";
        private int totalFiles = 0;
        private string downloadFile = "http://losmandy.com/files/gemini/firmware/combined.zip";
        private bool bCancel = false;
        private System.Windows.Forms.Timer tmTimer = new System.Windows.Forms.Timer();
        private bool bError = false;

        private int connections = 1;

        private string previousDateTime = "(unknown)";

        private string[] http_links = new string[]
        {

            "http://losmandy.com/files/gemini/firmware/combined.zip",

        };

        private string old_firmware_name = "Cur_Gem2.bin";
            // pre-2012 firmware cannot be flashed if this file is in the root directory -- Tom Hilton

        private string copy_firmware_to = "NewGem.bin";

        private string copy_firmware_from = "HGM_Gem2.bin";

        private System.Threading.Semaphore semConn = null;

        private ManualResetEvent eCancel = new ManualResetEvent(false);
        MyWebClient client = null;

        public GFUForm()
        {

            System.Threading.ThreadPool.SetMaxThreads(200, 200);

            semConn = new System.Threading.Semaphore(2, 2);

            string p = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU";

            try
            {
                Directory.CreateDirectory(p);
            }
            catch
            {
            }


            if (!Directory.Exists(p))
            {
                MessageBox.Show(this, "Unable to create Application Data folder: \n\n" + p, "Cannot start",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            try
            {
                InitializeComponent();
            }
            catch (Exception XXX)
            {
                MessageBox.Show(this, "Exception Caught:\n" + XXX.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Text = ProductName + " " + ProductVersion;


            if (Properties.Settings.Default.IPAddress != "")
            {
                txtIP.Text = Properties.Settings.Default.IPAddress;
                txtPwd.Text = Properties.Settings.Default.Password;
                txtUser.Text = Properties.Settings.Default.UserName;
            }


            try
            {
                ckFlash.Checked = true;
                ckGemini.Checked = true;
                ckHC.Checked = true;
                ckCat.Checked = false;
                chkVideos.Checked = false;

                cbZip.Sorted = true;
                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU";
                string[] Zips = Directory.GetFiles(path, "*.zip");
                cbZip.Items.AddRange(Zips);

                cbZip.Items.AddRange(http_links);
                cbZip.SelectedIndex = cbZip.FindString("http://losmandy.com/files/gemini/firmware/combined.zip", 0);

            }
            catch (Exception YYY)
            {
                MessageBox.Show(this, "Exception Caught:\n" + YYY.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblVer.Text = "v" + Application.ProductVersion;

            tmTimer.Tick += new EventHandler(tmTimer_Tick);
            tmTimer.Interval = 500;
            cbVersion.SelectedIndex = 0;
        }

        internal void Stop()
        {
            ResetButton();
        }


        void Status(string s)
        {
            if (InvokeRequired)
                BeginInvoke(new Action(() => { lbStat.Text = s; lbStat.Update(); }));
            else
            { lbStat.Text = s; lbStat.Update(); }
        }

        private void tmTimer_Tick(object sender, EventArgs e)
        {
            tmTimer.Stop();
            button1_Click(this, null);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.IPAddress = txtIP.Text;
            Properties.Settings.Default.Password = txtPwd.Text;
            Properties.Settings.Default.UserName = txtUser.Text;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();

            if (button1.Text == "Start")
            {
                eCancel.Reset();

                //                DELETE(old_firmware_name, "Delete " + old_firmware_name);   // remove old firmware file (pre-2012) that causes flash to fail

                //                return;

                bCancel = false;
                button1.Enabled = false;
                bError = false;
                previousDateTime = "(unknown)";


                statDecompress.BackColor = SystemColors.InactiveCaption;
                statDownload.BackColor = SystemColors.InactiveCaption;
                statUpload.BackColor = SystemColors.InactiveCaption;
                lbReboot.BackColor = SystemColors.InactiveCaption;
                lbFlash.BackColor = SystemColors.InactiveCaption;
                lbSRAM.BackColor = SystemColors.InactiveCaption;

                statDecompress.Text = "";
                statDownload.Text = "";
                statUpload.Text = "";
                lbReboot.Text = "Reboot";


                button1.Text = "Gemini";
                button1.BackColor = Color.Yellow;
                button1.Update();

                Status("Connecting to Gemini...");

                if (!CheckVersion())
                {
                    ResetButton();
                    bError = true;
                    previousDateTime = "(unknown)";
                    ResetButton();
                    Status("Gemini not connected");
                    return;
                }

                Status("Gemini Connected!");


                button1.Text = "Stop";
                button1.BackColor = Color.Red;
                button1.Enabled = true;

                if (tabControl1.SelectedIndex == 1)
                {
                    Decompress("", true);
                    ResetButton();
                    return;

                }

                if (cbZip.Text == "" || cbZip.Text.StartsWith("http://")
                    || !File.Exists(cbZip.Text))
                {
                    if (cbZip.Text != "") downloadFile = cbZip.Text;

                    try
                    {
                        lbDownload.BackColor = Color.Yellow;
                        progDownload.Minimum = 0;
                        progDownload.Maximum = 1000;
                        progDownload.Value = 0;

                        Status("Downloading firmware...");

                        Application.DoEvents();

                        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU";
                        var file = "download " + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
                        var fileName = Path.Combine(path, file);
                        System.IO.Directory.CreateDirectory(path);
                        using (client = new MyWebClient())
                        {
                            client.Timeout = 5000;
                            client.DownloadProgressChanged +=
                                new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                            client.Headers.Add("user-agent", $"GFU/{Application.ProductVersion} (${Environment.OSVersion.VersionString}; Trident/7.0; rv:11.0) like Gecko");
                            client.DownloadFileAsync(new System.Uri(downloadFile), fileName);
                        }

                        CombinedFile = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this,
                            "Unable to download combined.zip:\n" + ex.Message,
                            "Download " + downloadFile, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        ResetButton();
                    }
                }
                else //an older/saved version of firmware was already picked in the cb
                {
                    DialogResult res = MessageBox.Show(this,
                        "Do you want to upload a prevoiusly downloaded firmware file?\n" + cbZip.Text,
                        "Upload existing file?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);
                    if (res == DialogResult.Yes)
                    {
                        CombinedFile = cbZip.Text;
                        Application.DoEvents();
                        Decompress(CombinedFile);

                    }
                    else
                    {
                        ResetButton();
                        return;
                    }
                }

            }
            else
            {
                var r = MessageBox.Show("Firmware update in progress. Are you sure you want to abort?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    eCancel.Set();  //cause a stop event
                    bCancel = true;
                    Status("Update Aborted");
                }
            }

        }

        private void DoAdvancedUpdate()
        {

        }

        void CheckCancel()
        {
            if (eCancel.WaitOne(0)) {
                throw new Exception("Stopped");
            }
        }


        private void _reset()
        {
            if (client != null && client.IsBusy)
            {
                try { client.CancelAsync(); client = null; } catch { };
            }

            button1.Enabled = true;
            button1.Text = "Start";
            button1.BackColor = Color.Green;

        }
        internal void ResetButton()
        {
            if (InvokeRequired) BeginInvoke(new Action(() => _reset()));
            else _reset();
        }

        private bool CheckVersion()
        {

            try
            {
                string s = "";
                try
                {
                    s = GET("firmware.cgi", "");
                }
                catch (WebException EX)
                {
                    // if file not found, perhaps it's just a clean SD card
                    if (!EX.Message.Contains("404"))
                        throw EX;
                }

                int idx = s.IndexOf("Build date:");
                if (idx < 0)
                {
                    //DialogResult res = MessageBox.Show(this, "Please note that if your current Gemini firmware version is earlier than Dec 18, 2012, you should first upgrade to Dec 18, 2012 firmware before proceeding!\n\nDo you want to continue anyway (not recommended)?", "Version check", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //if (res != DialogResult.Yes)
                    //{
                    //    return false;
                    //}
                    //else
                    return true;
                }

                try
                {
                    string[] sa = (s.Substring(idx)).Split(new string[] {"<BR>"}, StringSplitOptions.RemoveEmptyEntries);
                    string v = "Build date:";
                    s = sa[0].Trim().Substring(v.Length);
                    s = s.Trim();
                    DateTime dt = new DateTime();

                    if (DateTime.TryParse(s, out dt))
                    {
                        previousDateTime = dt.ToLongDateString();

                        //if (dt < new DateTime(2012, 12, 17))
                        //{

                        //    DialogResult res = MessageBox.Show(this, "Your firmware version (" + previousDateTime + ") is earlier than\nDecember 18, 2012.\n\nYou should first upgrade to December 18, 2012 firmware before proceeding!\n\nDo you want to continue anyway (not recommended)?", "Version check", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                        //    if (res != DialogResult.Yes)
                        //    {
                        //        return false;
                        //    }
                        //}
                    }

                }
                catch (Exception ex)
                {
                    //DialogResult res = MessageBox.Show(this, "Please note that if your firmware version is earlier than December 18, 2012, you should first upgrade to Dec 18, 2012 firmware before proceeding!\n\nDo you want to continue anyway (not recommended)?", "Version check", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //if (res != DialogResult.Yes)
                    //{
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {

                statUpload.BackColor = Color.Red;
                statUpload.Text = "Failed!";
                progUpload.Value = 0;
                MessageBox.Show(this,
                    "Failed to connect to Gemini. Please check that it's connected, turned ON, and at the correct IP address.\n\nError: " +
                    (ex as WebException).Message, "Cannot connect to Gemini: " + txtIP.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                bError = true;
                ResetButton();
                return false;
            }


        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                progDownload.Value = 0;
                statDownload.BackColor = Color.Red;
                statDownload.Text = "Failed!";


                MessageBox.Show(this,
                    "Download failed to complete:\n\n" + e.Error.Message,
                    "Cannot download Combined.zip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bError = true;
                ResetButton();
                return;
            }
            progDownload.Value = 1000;
            lbDownloadPercent.Text = (progDownload.Value/10).ToString() + "%";
            statDownload.Text = "Done!";
            statDownload.BackColor = Color.Green;
            lbDownload.BackColor = Color.Green;
            Application.DoEvents();
            Decompress(CombinedFile);
            ResetButton();
        }

        private string MyDir
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                progDownload.Value = (int) ((1000*e.BytesReceived)/e.TotalBytesToReceive);
                lbDownloadPercent.Text = (progDownload.Value/10).ToString() + "%";
            }
            catch
            {
            }
            progDownload.Update();
            CheckCancel();
        }

        private bool Decompress(string file, bool advanced = false)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU\\Upload";
            try
            {
                FileSystemInfo inf = new DirectoryInfo(path);
                inf.DeleteReadOnly();
            }
            catch (Exception ex)
            {
            }


            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch
            {
            }

            lbDecompress.BackColor = Color.Yellow;
            progDecompress.Minimum = 0;
            progDecompress.Maximum = 1000;
            progDecompress.Value = 0;

            try
            {

                Status("Extracting files...");

                if (!advanced)
                {
                    using (ZipFile zip1 = ZipFile.Read(file))
                    {
                        zip1.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(zip1_ExtractProgress);
                        zip1.ExtractAll(path);
                    }
                }
                else
                {
                    ExtractAdvancedFiles(path);
                }


                progDownload.Value = 1000;
                lbDownloadPercent.Text = "100%";
                statDecompress.Text = "Done!";
                statDecompress.BackColor = Color.Green;
                lbDecompress.BackColor = Color.Green;

                Application.DoEvents();

                if (Directory.Exists(Path.Combine(path, "combined")))
                    path = Path.Combine(path, "combined");

                if (!ckHC.Checked && !ckCat.Checked)
                {
                    try
                    {
                        FileSystemInfo inf = new DirectoryInfo(path + @"\HCFirmware");
                        inf.DeleteReadOnly();
                    }
                    catch { }
                }
                else if (!ckHC.Checked) // delete firmware only, keep the other files (catalogs)
                {
                    try
                    {
                        FileSystemInfo inf = new DirectoryInfo(path + @"\HCFirmware");
                        inf.DeleteReadOnly();
                    }
                    catch { }
                }
               
                if (!ckCat.Checked)
                {
                    try
                    {
                        //Directory.Delete(path + @"\HCFirmware\Catalogs", true); // user didn't want Catalog files
                        var dir = new DirectoryInfo(path + @"\HCFirmware");

                        foreach (var c in dir.EnumerateFiles("*.guc"))
                        {
                            c.DeleteReadOnly();
                        }
                    }
                    catch { }

                    try { 
                        FileSystemInfo inf = new DirectoryInfo(path + @"\Catalogs");
                        inf.DeleteReadOnly();
                    }
                    catch { }
                }

                if (!chkVideos.Checked)
                {
                    try
                    {
                        FileSystemInfo inf = new DirectoryInfo(Path.Combine(path, "Video"));
                        inf.DeleteReadOnly();                       
                    }
                    catch
                    {
                    }
                }
                 
                if (ckCat.Checked)
                {
                    try
                    {
                        FileSystemInfo inf = new DirectoryInfo(Path.Combine(path, "HCFirmware"));
                        inf.DeleteReadOnly();
                      
                    }
                    catch
                    {
                    }

                    try
                    {
                        List<String> Catalogs =
                            Directory.GetFiles(path + "\\Catalogs", "*.*", SearchOption.AllDirectories).ToList();

                        foreach (string f in Catalogs)
                        {
                            FileInfo mFile = new FileInfo(f);
                            // to remove name collision
                            if (new FileInfo(path + "\\HCFirmware\\" + mFile.Name).Exists == false)
                                mFile.CopyTo(path + "\\HCFirmware\\" + mFile.Name);
                        }
                    }
                    catch
                    {
                    }
                }


                if (!ckGemini.Checked)
                {


                    string[] Dirs = Directory.GetDirectories(path);
                    foreach (string s in Dirs)
                    {
                        if (s.EndsWith("HCFirmware", StringComparison.CurrentCultureIgnoreCase)) continue;
                        if (s.EndsWith("Catalogs", StringComparison.CurrentCultureIgnoreCase) && ckCat.Checked)
                            continue;

                        if (s.EndsWith("Video", StringComparison.CurrentCultureIgnoreCase) && chkVideos.Checked)
                            continue;

                        Directory.Delete(s, true);
                    }

                    string[] fs = Directory.GetFiles(path);
                    foreach (string s in fs)
                    {
                        if (s.ToLower().EndsWith(".bin") && ckFlash.Checked)
                            // don't delete .bin files if we want to do flash
                            ;
                        else
                            File.Delete(s);
                    }
                }


                
                if (ckFlash.Checked)
                {
                    try
                    {
                        Directory.CreateDirectory(path + "\\EN");
                    }
                    catch
                    {
                    }
                    try
                    {
                        File.Copy(Path.Combine(MyDir, "gfu.cgi"), path + "\\EN\\" + "gfu.cgi");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Cannot copy GFU.CGI file!" + ex.ToString(), "Copy failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bError = true;
                        bCancel = true;
                        ResetButton();
                        return false;
                    }

                    // if HGM2_Gem2.bin file exists, rename it to NewGem.bin
                    // so that it's not auto-flashed on reboot (6-2023):

                    bool bFrom = File.Exists(Path.Combine(path, copy_firmware_from));
                    bool bTo = File.Exists(Path.Combine(path, copy_firmware_to));
                    if (bFrom && !bTo)
                    {
                        System.IO.File.Move(Path.Combine(path, copy_firmware_from), Path.Combine(path, copy_firmware_to));
                    }



                }
                else // delete firmware files, since flash wasn't requested
                {
                    string[] fs = Directory.GetFiles(path);
                    foreach (string s in fs)
                    {
                        if (s.ToLower().EndsWith(".bin") && ckFlash.Checked)
                            // don't delete .bin files if we want to do flash
                            ;
                        else
                            File.Delete(s);
                    }
                }

                if (!ckFlash.Checked && !ckGemini.Checked && !ckHC.Checked && !ckCat.Checked && !chkVideos.Checked)
                {
                    MessageBox.Show(this, "No upload/flash option is selected", "Nothing to do", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                    return true;
                }

                UploadCount = 0;
                progUpload.Minimum = 0;
                progUpload.Maximum = 1000;
                progUpload.Value = 0;
                lbUpload.BackColor = Color.Yellow;
                totalFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Count();

                Status("Uploading files...");
                ftpAll(path, txtIP.Text, txtUser.Text, txtPwd.Text);

                if (bError || bCancel)
                {
                    return false;
                }
                progUpload.Value = 1000;
                lbUploadPercent.Text = "100%";
                statUpload.BackColor = Color.Green;
                lbUpload.BackColor = Color.Green;

                statUpload.Text = "Done!";
                bool flashed = false;

                if (ckFlash.Checked)
                {
                    string[] files = Directory.GetFiles(path, "*.bin");

                    if (files.Count() > 0)
                    {
                        string ff = files[0];

                        if (files.Count() > 1)
                        {
                            frmFirmware frm = new frmFirmware();
                            frm.Files = files;
                            DialogResult dlg = frm.ShowDialog(this);
                            if (dlg == DialogResult.OK)
                            {
                                ff = frm.SelectedFile;
                            }
                            else
                                return false;
                        }
//                        Status("Restarting Gemini...");

//                        Gemini_Reboot(); //reboot just in case

                     
                        DateTime dt = File.GetCreationTime(ff);
                        string fn = Path.GetFileName(ff);

                        Status("Ready to flash");

                        DialogResult res = MessageBox.Show(this,
                            "Upload Completed!\n\nDo you want to flash firmware file " + fn + " dated " +
                            dt.ToShortDateString() + "?", "File update completed", MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);


                        if (res == DialogResult.Yes)
                        {
                            flashed = true;
                            Status("Removing old firmware file...");

                            DELETE(old_firmware_name, "Delete " + old_firmware_name);
                                // remove old firmware file (pre-2012) that causes flash to fail


//                            if (!bTo && bFrom)
//                                File.Copy(Path.Combine(path, copy_firmware_from), Path.Combine(path, copy_firmware_to));


                            string file_idx = "";
                            string idx;

                            Status("Getting firmware version...");

                            try
                            {
                                idx = GET("gfu.cgi", "");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this,
                                    "Failed to get firmware listing from Gemini!\n\nGET(gfu.cgi)\n" + ex.ToString(),
                                    "Flash Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                ResetButton();
                                return false;

                            }

                            string[] firm = idx.Split(new string[] {"</option>"}, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in firm)
                            {
                                if (s.EndsWith(fn, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    string[] v = s.Split(new string[] {"=", "<", ">"},
                                        StringSplitOptions.RemoveEmptyEntries);
                                    file_idx = v[1];
                                    break;
                                }
                            }

                            if (file_idx == "")
                            {
                                MessageBox.Show(this,
                                    "Did not find firmware on SD card! Something didn't go right, nothing was flashed.",
                                    "Failed to find firmware bin file", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                bError = false;
                                ResetButton();
                                return false;
                            }

                            lbFlash.BackColor = Color.Yellow;
                            lbFlash.Update();

                            Application.DoEvents();


                            try
                            {
                                Status("Saving settings...");

                                Gemini_Save_Settings();
                            }
                            catch (Exception ex1)
                            {
                            }

                            try
                            {
                                Status("Flashing new firmware...");

                                Flash(file_idx);
                            }
                            catch (Exception ex1)
                            {

                                MessageBox.Show(this, "Flash didn't complete." + ex1.Message, "Failed to flash",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                ResetButton();
                                return false;
                            }
                            lbFlash.BackColor = Color.Green;
                            lbFlash.Update();

                            lbSRAM.BackColor = Color.Yellow;
                            lbSRAM.Update();
                            Application.DoEvents();

                            try
                            {
                                //Status("SRAM Reset...");

                                //Gemini_SRAM_Reset();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show(this, "SRAM Reset didn't complete." + ex2.Message,
                                    "Failed to reset SRAM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                ResetButton();
                                return false;
                            }


                            try
                            {
                                Status("Reloading settings...");

                                //Gemini_Load_Settings();

                            }
                            catch (Exception ex3)
                            {

                            }

                            lbSRAM.BackColor = Color.Green;
                            lbFlash.Update();
                            lbReboot.BackColor = Color.Yellow;
                            lbReboot.Update();
                            Application.DoEvents();

                            try
                            {
                                Status("Rebooting Gemini...");

                                Gemini_Reboot();
                            }
                            catch (Exception ex3)
                            {
                                MessageBox.Show(this, "Reboot didn't complete." + ex3.Message, "Failed to reboot",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                ResetButton();
                                return false;
                            }
                            lbReboot.BackColor = Color.Green;
                            lbReboot.Text = "Done!";
                            lbReboot.Update();
                            Status("DONE! Please turn Gemini Off, then On.");

                            Application.DoEvents();

                            MessageBox.Show(this,
                                "Flashing completed!\n\nPlease turn off and on your Gemini to complete the process.",
                                "All Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ResetButton();
                        }
                    }
                    else
                    {
                        Status("DONE! No firmware .bin file found to flash");
                        MessageBox.Show(this,
                              "Files uploaded. No bin file found to flash!",
                              "All Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetButton();
                    }
                }


                if (!flashed && ckHC.Checked && Directory.Exists(path + @"\HCFirmware"))
                {
                    Status("Ready");

                    DialogResult res = MessageBox.Show(this,
                        "Upload Completed!\n\nPlease restart Gemini or disconnect/reconnect hand controller to flash new HC firmware", "File update completed", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                 
                }
                ResetButton();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message=="Stopped")
                    MessageBox.Show(this, "Stopped!", Application.ProductName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else 
                    MessageBox.Show(this, "Error" + "\n\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ResetButton();
            return false;

        }

        private void ExtractAdvancedFiles(string path)
        {
            var hc_files = lbHCFiles.Items.Cast<String>().ToList();
            var gemini_files = lbGeminiFiles.Items.Cast<String>().ToList();

            // all the Gemini files
            foreach (var f in gemini_files)
            {
                var new_path = FindSubFolder(f, subsGemini);
                new_path = Path.Combine(path, new_path);

                string dir = "";

                if (Directory.Exists(f))
                    dir = new_path; // the whole thing is a directory
                else
                    dir = Path.GetDirectoryName(new_path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // if it's a folder, copy all the files under it
                if (Directory.Exists(f))
                    CopyDirectory(f, new_path, true);
                else
                if (f.EndsWith(".zip", StringComparison.InvariantCulture))
                {
                    using (ZipFile zip1 = ZipFile.Read(f))
                    {
                        zip1.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(zip1_ExtractProgress);
                        zip1.ExtractAll(dir);
                    }

                }
                else
                    File.Copy(f, new_path);
            }
            
            foreach (var f in hc_files)
            {
                var new_path = FindSubFolder(f,  null);
                new_path = Path.Combine(path, new_path);
                string dir = "";


                var isDir = Directory.Exists(f);
                if (isDir)
                    dir = new_path; // the whole thing is a directory
                else
                    dir = Path.GetDirectoryName(new_path);


                if (!dir.ToLower().Contains("\\hcfirmware"))
                {
                    if (isDir)
                    {
                        dir = Path.Combine(Path.Combine(Path.GetDirectoryName(dir), "HCFirmware"), Path.GetFileName(dir));
                        new_path = dir;
                    }
                    else
                    {
                        dir = Path.Combine(dir, "HCFirmware");
                        new_path = Path.Combine(dir, Path.GetFileName(new_path));
                    }
                }

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);


                // if it's a folder, copy all the files under it
                if (Directory.Exists(f))
                    CopyDirectory(f, new_path, true);
                else
                if (f.EndsWith(".zip", StringComparison.InvariantCulture))
                {
                    using (ZipFile zip1 = ZipFile.Read(f))
                    {
                        zip1.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(zip1_ExtractProgress);
                        zip1.ExtractAll(dir);
                    }

                }
                else
                    File.Copy(f, new_path);
            }

        }


        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }


        string[] subsGemini = new string[]
        {
            "AltAz",
            "Catalogs",
            "DE",
            "EN",
            "ES",
            "FR",
            "HC",
            "HCFirmware",
            "MHC",
            "Video"
        };


        private string FindSubFolder(string f, string[] subs)
        {
            if (subs!=null)
                foreach(var d in subs)
                {
                    if (f.ToLower().Contains("\\" + d.ToLower() + "\\"))
                        return f.Substring(f.ToLower().LastIndexOf(d.ToLower()) + 1);
                }

            return Path.GetFileName(f);
        }

        private bool Gemini_Save_Settings()
        {
            try
            {
                Submit("ser.cgx", "SE%3D>43610%3At%23"); // >43610: stores all user settings to be restored after flash
                //GET("firmware.cgi", "CS=Store+SRAM");
            }
            catch
            {
            }

            return WaitForGemini("Save Settings");
        }

        private bool Gemini_Load_Settings()
        {
            try
            {
                GET("firmware.cgi", "CR=Load+SRAM");
            }
            catch
            {
            }

            return WaitForGemini("Save Settings");
        }



        private void zip1_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            CheckCancel();

            if (e.EntriesExtracted > 0)
            {
//                if (bCancel || eCancel.WaitOne(0)) e.Cancel = true;

                try
                {
                    progDecompress.Value = (int) ((1000*e.EntriesExtracted)/e.EntriesTotal);
                    totalFiles = e.EntriesTotal;
                    lbDecompressPercent.Text = (progDecompress.Value/10).ToString() + "%";
                    Application.DoEvents();
                }
                catch
                {
                }

//                Application.DoEvents();
            }
            if (e.EventType == ZipProgressEventType.Error_Saving)
            {
                bError = true;
                progDecompress.Value = 0;
                statDecompress.BackColor = Color.Red;
                statDecompress.Text = "Failed!";
                MessageBox.Show("Failed to decompress", "Failed to decompress archive\n\n" + e.ArchiveName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetButton();
                throw new Exception("Failed to decompress");

            }
        }

        private int UploadCount = 0;

        private bool ftpAll(string fromPath, string to, string uname, string pwd)
        {
            CheckCancel();
            if (bError) return false;

            string[] dirs = Directory.GetDirectories(fromPath);

            foreach (string d in dirs)
            {
                if (bError) return false;

                CheckCancel();
                string p = Path.GetFileName(d);

                try
                {
                    WebRequest request = WebRequest.Create("ftp://" + to + "/" + p);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(uname, pwd);
                    request.Timeout = 15000;
                    using (var resp = (FtpWebResponse) request.GetResponse())
                    {
                        Console.WriteLine(resp.StatusCode);
                    }

                }
                catch (Exception ex)
                {

                    if (ex is WebException)
                    {
                        if ((ex as WebException).Status == WebExceptionStatus.ConnectFailure)
                        {
                            statUpload.BackColor = Color.Red;
                            statUpload.Text = "Failed!";
                            progUpload.Value = 0;
                            MessageBox.Show(this,
                                "Failed to connect to Gemini:\n\nError: " + (ex as WebException).Message,
                                "Cannot connect to Gemini: " + txtIP.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bError = true;
                            ResetButton();
                            throw new Exception("Couldn't upload files to Gemini");
                        }

                    }

                }

                string dirPath = Path.Combine(fromPath, p);
                string toPath = to + "/" + p;

                ftpAll(dirPath, toPath, uname, pwd);

            }

            using (MyWebClient webClient = new MyWebClient())
            {
                webClient.Credentials = new NetworkCredential(uname, pwd);

                webClient.Timeout = 90000;

                webClient.Encoding = System.Text.Encoding.UTF8;

                string[] files2 = Directory.GetFiles(fromPath);
                foreach (string f in files2)
                {
                    CheckCancel();

                    if (bError) return false;


                    string fname = Path.GetFileName(f);
                    try
                    {                        
                        webClient.UploadFile("ftp://" + to + "/" + fname, f);                     
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message + "\n\n" + to + "/" + fname + "\n\n" + ex.ToString(),
                            "Failed to FTP file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bError = true;
                        ResetButton();
                        return false;
                    }
                    try
                    {
                        progUpload.Value = (int) ((1000*UploadCount)/totalFiles);
                        lbUploadPercent.Text = (progUpload.Value/10).ToString() + "%" + " (" + UploadCount.ToString() +
                                               "/" + totalFiles.ToString() + ")";
                    }
                    catch
                    {
                    }
                    Application.DoEvents();
                    UploadCount++;
                }
            }

            return true;
        }

        private void GFUForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void GFUForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bError = true;
            bCancel = true;
            eCancel.Set();

            try
            {
                SaveSettings();
            }
            catch { }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU\\Upload";
            try
            {
                System.IO.Directory.Delete(path, true); //delete old contents
            }
            catch
            {
            }

        }


        private bool POST(string f, string s)
        {
            WebRequest req = WebRequest.Create("http://" + txtIP.Text + "/en/" + f);
            req.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
            req.Timeout = 10000; // don't worry about the timeout here
            req.Method = "POST";

            string postData = s;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byteArray.Length;
            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();
            return true;
        }

        private bool Submit(string f, string s, int timeout = 5000)
        {
            WebRequest req = WebRequest.Create("http://" + txtIP.Text + "/en/" + f + "?" + s);
            req.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
            req.Timeout = timeout; // don't worry about the timeout here

            req.Method = "GET";
            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();
            return true;
        }

        private string GET(string f, string s, int timeout = 5000)
        {
            WebRequest req = WebRequest.Create("http://" + txtIP.Text + "/en/" + f + "?" + s);
            req.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
            req.Timeout = timeout;

            req.Method = "GET";
            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();
            return returnvalue;
        }


        private bool DELETE(string f, string s)
        {
            // turns out delete doesn't work the first time on 2012 firmware. Need to try it again
            // and then it works
            for (int i = 0; i < 3; ++i)
                try
                {
                    WebRequest request = WebRequest.Create("ftp://" + txtIP.Text + "/" + f);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    request.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
                    request.Timeout = 5000;
                    using (var resp = (FtpWebResponse) request.GetResponse())
                    {
                        Console.WriteLine(resp.StatusCode);
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    if (i < 2)
                    {
                        System.Threading.Thread.Sleep(500); //retry
                        continue;
                    }
                    if (ex is WebException)
                    {
                        if ((ex as WebException).Status == WebExceptionStatus.ConnectFailure)
                        {
                            statUpload.BackColor = Color.Red;
                            statUpload.Text = "Failed!";
                            progUpload.Value = 0;
                            MessageBox.Show(this,
                                "Failed to connect to Gemini:\n\nError: " + (ex as WebException).Message,
                                "Cannot connect to Gemini: " + txtIP.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            bError = true;
                            ResetButton();
                            throw new Exception("Couldn't delete a file on Gemini SD card: " + f);
                        }

                    }
                    return false;
                }

            return true;
        }

        private bool Gemini_Reboot()
        {
            try
            {

                Submit("ser.cgx", "SE%3D>65534%3Ar%23");
                //Submit("firmware.cgi", "bC=Cold Reboot", 15000);
            }
            catch
            {
            }
            return WaitForGemini("Reboot", 15000);

        }

        private bool Gemini_SRAM_Reset()
        {
            try
            {
                Submit("firmware.cgi", "CL=RESET SRAM", 15000);
            }
            catch
            {
            }
            return WaitForGemini("SRAM Reset", 15000);
        }

        private bool Flash(string fname)
        {
            try
            {
                Submit("index.cgi", "ff=" + fname, 30000);
            }
            catch (Exception ex)
            {

            }
            return WaitForGemini("Flash Firmware", 30000);
        }

        private bool WaitForGemini(string msg, int timeout = 5000)
        {
            try
            {
                string res = GET("firmware.cgi", "", timeout);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Timeout occurred while waiting for Gemini to " + msg ,
                    "Failed to " + msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(this,
                "ARE YOU SURE YOU WANT TO FORMAT GEMINI SD CARD?\r\n\r\nAll data on the card will be erased and will need to be re-uploaded",
                "Format Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                try
                {
                    POST("firmware.cgi", "format=yes&label=GeminiSD");
                }
                catch
                {
                }
                res = MessageBox.Show(this, "SD Card Format Completed", "Format Done", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private class MyWebClient : WebClient
        {
            public int Timeout { get; set; }
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);

                w.Timeout = Timeout;
                //((HttpWebRequest)w).ReadWriteTimeout = Timeout;
                return w;
            }
        }

        private void lbGeminiDD_DragDrop(object sender, DragEventArgs e)
        {
            lbGeminiFiles_DragDrop(sender, e);
        }

        private void lblHCDD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lbHCFiles.Items.AddRange(dialog.FileNames);
                }
            }

        }

        private void lbGeminiDD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lbGeminiFiles.Items.AddRange(dialog.FileNames);
                }
            }

        }

        private void lbGeminiFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
                lbGeminiFiles.Items.AddRange(files); 
        }

        private void lbHCFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
                lbHCFiles.Items.AddRange(files);
        }


        private void lbGeminiFiles_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lbHCFiles_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lbGeminiDD_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lblHCDD_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lbGeminiFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblHCDD_DragDrop(object sender, DragEventArgs e)
        {
            lbHCFiles_DragDrop(sender, e);
        }

        private void pbClearGemini_Click(object sender, EventArgs e)
        {
            lbGeminiFiles.Items.Clear();
        }

        private void pbClearHC_Click(object sender, EventArgs e)
        {
            lbHCFiles.Items.Clear();

        }

        private void lbGeminiFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbGeminiFiles.SelectedIndex >= 0)
                lbGeminiFiles.Items.RemoveAt(lbGeminiFiles.SelectedIndex);
        }

        private void lbHCFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbHCFiles.SelectedIndex >= 0)
                lbHCFiles.Items.RemoveAt(lbHCFiles.SelectedIndex);

        }


        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVersion.Text.Contains("L6"))
                cbZip.Text = "http://losmandy.com/files/gemini/firmware/combined.zip";
            else if (cbVersion.Text.Contains("L5"))
                cbZip.Text = "http://losmandy.com/files/gemini/firmware/combined5.zip";

        }
    }
    static class ExtensionMethods
    {
        public static void DeleteReadOnly(this FileSystemInfo fileSystemInfo)
        {
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            if (directoryInfo != null)
            {
                foreach (FileSystemInfo childInfo in directoryInfo.GetFileSystemInfos())
                {
                    childInfo.DeleteReadOnly();
                }
            }

            fileSystemInfo.Attributes = FileAttributes.Normal;
            fileSystemInfo.Delete();
        }
    }

}
