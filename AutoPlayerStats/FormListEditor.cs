using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPlayerStats
{
    public partial class FormListEditor : Form
    {
        public FormListEditor()
        {
            InitializeComponent();
        }

        private void FormListEditor_Load(object sender, EventArgs e)
        {
            txtWhitelist.Text = string.Join(", ", Properties.Settings.Default.fList.Cast<string>().ToArray());
            txtBlacklist.Text = string.Join(", ", Properties.Settings.Default.eList.Cast<string>().ToArray());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // Maybe function-ify this? fixLists() already exists.

            string[] txtWl = txtWhitelist.Text.Replace(" ", "").Split(','); // Remove whitespace and split
            List<string> Wl = txtWl.ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            StringCollection wlCollec = new StringCollection();
            wlCollec.AddRange(Wl.ToArray());
            Properties.Settings.Default.fList = wlCollec;

            string[] txtBl = txtBlacklist.Text.Replace(" ", "").Split(','); // Remove whitespace and split
            List<string> Bl = txtBl.ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            StringCollection blCollec = new StringCollection();
            blCollec.AddRange(Bl.ToArray());
            Properties.Settings.Default.eList = blCollec;

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
