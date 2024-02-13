using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GFU
{
    public partial class frmFirmware : Form
    {
        public string[] Files { get; set; }

        public string SelectedFile;

        public frmFirmware()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbFirmware.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a firmware file to flash", "GFU", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SelectedFile = lbFirmware.SelectedItem as string;
            DialogResult = DialogResult.OK;
        }

        private void lbFirmware_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFile = lbFirmware.SelectedItem as string;
        }

        private void frmFirmware_Load(object sender, EventArgs e)
        {
            lbFirmware.Items.AddRange(Files);

        }

        internal static Dictionary<string, string> populateVersions(string url, bool beta)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (MyWebClient client = new MyWebClient())
                {
                    var a = client.DownloadData(url);
                    var html = System.Text.Encoding.Default.GetString(a);

                    var find = $"id=\"versions{(beta?"-beta":"")}\" hidden>";

                    int idx = html.IndexOf(find);
                    if (idx < 0) return res;

                    idx += find.Length;

                    int idx2 = html.IndexOf("</div>", idx);

                    var list = html.Substring(idx, idx2 - idx);

                 
                    var  json = JObject.Parse(list.Trim());


                    //var result = json.SelectToken("versions").Children().OfType<JProperty>()
                    //    .ToDictionary(p => p.Name, p => new
                    //    {
                    //        Created = (DateTime)p.First()["create date"],
                    //        Name = (string)p.First()["name"]
                    //    });

                    foreach (var n in json["versions"])
                    {
                        var level = n.Children().OfType<JProperty>().First().Name;
                        var path = n.Children().OfType<JProperty>().First().Value.ToString();

                        if (path.StartsWith("https://gemini-2.com"))
                            path = path.Replace("https://gemini-2.com", "https://github.com/PKAstro/Gemini-2/raw/master");

                        res.Add(level, path);
                    }


                }
            }
            catch (Exception ex) { }

            return res;
        }
    }


    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 2000;   // timeout after 2 seconds
            return w;
        }
    }

}
