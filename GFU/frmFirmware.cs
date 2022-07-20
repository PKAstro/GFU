using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GFU
{
    public partial class frmFirmware : Form
    {
        public string [] Files { get; set; }

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
    }
}
