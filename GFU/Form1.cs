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
//using EnterpriseDT.Net.Ftp;

namespace GFU
{
    public partial class GFUForm : Form
    {
        private string CombinedFile = "";
        private int totalFiles = 0;
        private string downloadFile = "http://www.gemini-2.com/firmware1/current/combined.zip";
        private bool bCancel = false;
        private Timer tmTimer = new Timer();
        private bool bError = false;

        private int connections = 1;

        private string previousDateTime = "(unknown)";

        private string[] http_links = new string[]
        {

            "http://www.gemini-2.com/firmware1/current/combined.zip",
            "http://gemini-2.com/firmware1/Older/NewGem-Dec-18-2012.zip",

        };

        private string old_firmware_name = "Cur_Gem2.bin";
            // pre-2012 firmware cannot be flashed if this file is in the root directory -- Tom Hilton

        private string copy_firmware_from = "NewGem.bin";
            // for some subtle upgrade issues, need to make a copy of NewGem.bin and name it HGM_Gem2.bin -- Tom Hilton

        private string copy_firmware_to = "HGM_Gem2.bin";

        private System.Threading.Semaphore semConn = null;

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
                MessageBox.Show(this, "Exception Caught:\n" + XXX.Message + "\n\n" + XXX.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                cbZip.SelectedIndex = cbZip.FindString("http://www.gemini-2.com/firmware1/current/combined.zip", 0);

            }
            catch (Exception YYY)
            {
                MessageBox.Show(this, "Exception Caught:\n" + YYY.Message + "\n\n" + YYY.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblVer.Text = "v" + Application.ProductVersion;

            tmTimer.Tick += new EventHandler(tmTimer_Tick);
            tmTimer.Interval = 500;
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

                if (!CheckVersion())
                {
                    button1.Enabled = true;
                    bError = true;
                    previousDateTime = "(unknown)";
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
                        Application.DoEvents();

                        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU";
                        var file = "download " + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
                        var fileName = Path.Combine(path, file);
                        System.IO.Directory.CreateDirectory(path);
                        using (MyWebClient client = new MyWebClient())
                        {
                            client.DownloadProgressChanged +=
                                new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                            client.DownloadFileAsync(new System.Uri(downloadFile), fileName);
                        }

                        CombinedFile = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this,
                            "Unable to download combined.zip:\n" + ex.Message + "\n\nDetails:\n" + ex.ToString(),
                            "Download " + downloadFile, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                }

            }
            button1.Text = "Start";
            button1.Enabled = true;

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
                button1.Text = "Start";
                button1.Enabled = true;
                bCancel = true;

                MessageBox.Show(this,
                    "Download failed to complete:\n\n" + e.Error.Message + "\n\nDetails:\n" + e.Error.ToString(),
                    "Cannot download Combined.zip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bError = true;

                return;
            }
            progDownload.Value = 1000;
            lbDownloadPercent.Text = (progDownload.Value/10).ToString() + "%";
            statDownload.Text = "Done!";
            statDownload.BackColor = Color.Green;
            lbDownload.BackColor = Color.Green;
            Application.DoEvents();
            Decompress(CombinedFile);

            button1.Text = "Start";
            button1.Enabled = true;
            bCancel = true;

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
            Application.DoEvents();
        }

        private bool Decompress(string file)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GFU\\Upload";
            try
            {
                System.IO.Directory.Delete(path, true); //delete old contents
            }
            catch
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
                using (ZipFile zip1 = ZipFile.Read(file))
                {
                    zip1.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(zip1_ExtractProgress);
                    zip1.ExtractAll(path);
                }



                progDownload.Value = 1000;
                lbDownloadPercent.Text = "100%";
                statDecompress.Text = "Done!";
                statDecompress.BackColor = Color.Green;
                lbDecompress.BackColor = Color.Green;

                Application.DoEvents();

 

                if (!ckHC.Checked && !ckCat.Checked)
                {
                    Directory.Delete(path + @"\HCFirmware", true); // user didn't want HC files
                } 
                else if (!ckHC.Checked) // delete firmware only, keep the other files (catalogs)
                {
                    Directory.Delete(Path.Combine(path, "HCFirmware"), true);
                }


                if (!chkVideos.Checked)
                {
                    try
                    {
                        Directory.Delete(Path.Combine(path, "Video"), true);
                    }
                    catch
                    {
                    }
                }


                if (ckCat.Checked)
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(path, "HCFirmware"));
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
                            // to remove name collusion
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
                        MessageBox.Show(this, "Cannot copy GFU.CGI file!\n\n" + ex.ToString(), "Copy failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bError = true;
                        bCancel = true;
                        return false;
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

                if (ckFlash.Checked)
                {
                    string[] files = Directory.GetFiles(path, "*.bin");

                    if (files.Count() > 0)
                    {
                        Gemini_Reboot(); //reboot just in case

                        string ff = files[0];
                        DateTime dt = File.GetCreationTime(ff);
                        string fn = Path.GetFileName(ff);

                        DialogResult res = MessageBox.Show(this,
                            "Upload Completed!\n\nDo you want to flash firmware file " + fn + " dated " +
                            dt.ToShortDateString() + "?", "File update completed", MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);


                        if (res == DialogResult.Yes)
                        {
                            DELETE(old_firmware_name, "Delete " + old_firmware_name);
                                // remove old firmware file (pre-2012) that causes flash to fail

                            // make a copy of firmware file named HGM_Gem2.bin to make sure this is flashed in some rare upgrade cases, as per Tom Hilton
                            // don't overwrite if 'HGM_Gem2.bin' already exists in the zip:

                            bool bFrom = File.Exists(Path.Combine(path, copy_firmware_from));
                            bool bTo = File.Exists(Path.Combine(path, copy_firmware_to));
//                            if (!bTo && bFrom)
//                                File.Copy(Path.Combine(path, copy_firmware_from), Path.Combine(path, copy_firmware_to));


                            string file_idx = "";
                            string idx;

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
                                return false;
                            }

                            lbFlash.BackColor = Color.Yellow;
                            lbFlash.Update();

                            Application.DoEvents();


                            try
                            {
                                Gemini_Save_Settings();
                            }
                            catch (Exception ex1)
                            {
                            }

                            try
                            {
                                Flash(file_idx);
                            }
                            catch (Exception ex1)
                            {

                                MessageBox.Show(this, "Flash didn't complete." + ex1.Message, "Failed to flash",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                return false;
                            }
                            lbFlash.BackColor = Color.Green;
                            lbFlash.Update();

                            lbSRAM.BackColor = Color.Yellow;
                            lbSRAM.Update();
                            Application.DoEvents();

                            try
                            {
                                Gemini_SRAM_Reset();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show(this, "SRAM Reset didn't complete." + ex2.Message,
                                    "Failed to reset SRAM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                return false;
                            }


                            try
                            {
                                Gemini_Load_Settings();

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
                                Gemini_Reboot();
                            }
                            catch (Exception ex3)
                            {
                                MessageBox.Show(this, "Reboot didn't complete." + ex3.Message, "Failed to reboot",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bError = true;
                                return false;
                            }
                            lbReboot.BackColor = Color.Green;
                            lbReboot.Text = "Done!";
                            lbReboot.Update();
                            Application.DoEvents();
                            MessageBox.Show(this,
                                "Flashing completed!\n\nPlease turn off and on your Gemini to complete the process.",
                                "All Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error" + "\n\n" + ex.Message + "\n\nDetails:\n" + ex.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

        }

        private bool Gemini_Save_Settings()
        {
            try
            {
                GET("firmware.cgi", "CS=Store+SRAM");
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
            if (e.EntriesExtracted > 0)
            {
                if (bCancel) e.Cancel = true;
                try
                {
                    progDecompress.Value = (int) ((1000*e.EntriesExtracted)/e.EntriesTotal);
                    totalFiles = e.EntriesTotal;
                    lbDecompressPercent.Text = (progDecompress.Value/10).ToString() + "%";
                }
                catch
                {
                }
                Application.DoEvents();
            }
            if (e.EventType == ZipProgressEventType.Error_Saving)
            {
                bError = true;
                progDecompress.Value = 0;
                statDecompress.BackColor = Color.Red;
                statDecompress.Text = "Failed!";
                MessageBox.Show("Failed to decompress", "Failed to decompress archive\n\n" + e.ArchiveName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private int UploadCount = 0;

        private bool ftpAll(string fromPath, string to, string uname, string pwd)
        {
            if (bError) return false;

            string[] dirs = Directory.GetDirectories(fromPath);

            foreach (string d in dirs)
            {
                if (bError) return false;

                string p = Path.GetFileName(d);

                try
                {
                    WebRequest request = WebRequest.Create("ftp://" + to + "/" + p);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(uname, pwd);
                    request.Timeout = 5000;
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
                            throw new Exception("Couldn't upload files to Gemini");
                        }

                    }

                }

                string dirPath = Path.Combine(fromPath, p);
                string toPath = to + "/" + p;

                ftpAll(dirPath, toPath, uname, pwd);

                //string[] files = Directory.GetFiles(dirPath);
                //foreach (string f in files)
                //{
                //    string fname = Path.GetFileName(f);
                //    using (WebClient webClient = new WebClient())
                //    {
                //        webClient.Credentials = new NetworkCredential(uname, pwd);
                //        webClient.UploadFile("ftp://" + toPath + "/" + fname, f);
                //    }
                //}
            }



#if false
            {
                try
                {
                    string[] files2 = Directory.GetFiles(fromPath);

                    EnterpriseDT.Net.Ftp.FTPConnection ftpConnection = new FTPConnection();
                    ftpConnection.ServerAddress =  txtIP.Text;
                    ftpConnection.UserName = uname;
                    ftpConnection.Password = "aa";
                    ftpConnection.AccountInfo = "";

                    ftpConnection.Connect();

                    string x = to.Replace(txtIP.Text, "");
                    if (x.StartsWith("/")) x = x.Substring(1);

                    ftpConnection.ChangeWorkingDirectory(x);

                    foreach (string f in files2)
                    {

                        if (bError) return false;

                        string fname = Path.GetFileName(f);

                        try
                        {
                            ftpConnection.UploadFile(f, fname);
                        }
                        catch (Exception ex)
                        {
                            if (!bError)
                            {
                                bError = true;
                                bCancel = true;
                                MessageBox.Show(this, ex.Message + "\n" + fname + "\n\nDetails:\n" + ex.ToString(), "Failed to upload", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        progUpload.Value = (int)((1000 * UploadCount) / totalFiles);
                        if (!bError)
                        {
                            UploadCount++;
                            lbUploadPercent.Text = (progUpload.Value / 10).ToString() + "%" + " (" + UploadCount.ToString() + "/" + totalFiles.ToString() + ")";
                            Application.DoEvents();
                        }
                    }

                    ftpConnection.Close();

                }
                catch (Exception ex)
                {
                  
                }
            }
//#else

            {
                try
                {
                    string[] files2 = Directory.GetFiles(fromPath);

                    foreach (string f in files2)
                    {

                        if (bError) return false;

                        string fname = Path.GetFileName(f);

                        System.Threading.ThreadPool.QueueUserWorkItem(arg =>
                        {
                            semConn.WaitOne();


                            using (MyWebClient webClient = new MyWebClient())
                            {
                                webClient.Credentials = new NetworkCredential(uname, pwd);
              
                                try
                                {
                                    webClient.UploadFile("ftp://" + to + "/" + fname, f);
                                    while (webClient.IsBusy)
                                        System.Threading.Thread.Sleep(100);
                                }
                                catch (Exception ex)
                                {
                                    lock (this)
                                        this.Invoke(new Action(() =>
                                        {
                                            if (!bError)
                                            {
                                                bError = true;
                                                bCancel = true;
                                                MessageBox.Show(this,
                                                    ex.Message + "\n" + fname + "\n\nDetails:\n" + ex.ToString(),
                                                    "Failed to upload", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }));
                                }

                                this.Invoke(new Action(() =>
                                {
                                    progUpload.Value = (int) ((1000*UploadCount)/totalFiles);
                                    if (!bError)
                                    {
                                        UploadCount++;
                                        lbUploadPercent.Text = (progUpload.Value/10).ToString() + "%" + " (" +
                                                               UploadCount.ToString() + "/" + totalFiles.ToString() +
                                                               ")";
                                        Application.DoEvents();

                                    }
                                }));

                            }
                            semConn.Release();
                        });


                    }
                }
                catch (Exception ex)
                {

                }
            }
            return true;
#endif


            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = new NetworkCredential(uname, pwd);

                string[] files2 = Directory.GetFiles(fromPath);
                foreach (string f in files2)
                {
                    if (bError) return false;


                    string fname = Path.GetFileName(f);
                    try
                    {
                        webClient.UploadFile("ftp://" + to + "/" + fname, f);
                        while (webClient.IsBusy)
                            System.Threading.Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message + "\n\n" + to + "/" + fname + "\n\nDetails:\n" + ex.ToString(),
                            "Failed to FTP file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bError = true;
                        break;
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
            SaveSettings();
            bError = true;
            bCancel = true;
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

        private bool Submit(string f, string s)
        {
            WebRequest req = WebRequest.Create("http://" + txtIP.Text + "/en/" + f + "?" + s);
            req.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
            req.Timeout = 1000; // don't worry about the timeout here

            req.Method = "GET";
            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();
            return true;
        }

        private string GET(string f, string s)
        {
            WebRequest req = WebRequest.Create("http://" + txtIP.Text + "/en/" + f + "?" + s);
            req.Credentials = new NetworkCredential(txtUser.Text, txtPwd.Text);
            req.Timeout = 40000;

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
                Submit("firmware.cgi", "bC=Cold Reboot");
            }
            catch
            {
            }
            return WaitForGemini("Reboot");

        }

        private bool Gemini_SRAM_Reset()
        {
            try
            {
                Submit("firmware.cgi", "CL=RESET SRAM");
            }
            catch
            {
            }
            return WaitForGemini("SRAM Reset");
        }

        private bool Flash(string fname)
        {
            try
            {
                Submit("index.cgi", "ff=" + fname);
            }
            catch
            {
            }
            return WaitForGemini("Flash Firmware");
        }

        private bool WaitForGemini(string msg)
        {
            try
            {
                string res = GET("firmware.cgi", "");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Timeout occurred while waiting for Gemini to " + msg + "\n\nDetails:\n" + ex.ToString(),
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
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout =  5 * 60 * 1000;
                return w;
            }
        }
    }

}
