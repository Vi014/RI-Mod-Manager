using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RI_Mod_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] enabled  = Directory.GetFiles(".", "*.red",      SearchOption.AllDirectories);
            string[] disabled = Directory.GetFiles(".", "*.disabled", SearchOption.AllDirectories);

            foreach(var modName in disabled)
            {
                lbDisabled.Items.Add(modName.Remove(0, 2).Remove(modName.Length-11));
            }
            foreach (var modName in enabled)
            {
                lbEnabled.Items.Add(modName.Remove(0, 2).Remove(modName.Length-6));
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();

            foreach(string mod in lbEnabled.SelectedItems)
            {
                selection.Add(mod);
            }

            foreach(string modName in selection)
            {
                File.Move(".\\" + modName + ".red", ".\\" + modName + ".disabled");
                lbDisabled.Items.Add(modName);
                lbEnabled.Items.Remove(modName);
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();

            foreach (string mod in lbDisabled.SelectedItems)
            {
                selection.Add(mod);
            }

            foreach (string modName in selection)
            {
                File.Move(".\\" + modName + ".disabled", ".\\" + modName + ".red");
                lbEnabled.Items.Add(modName);
                lbDisabled.Items.Remove(modName);
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("RicochetInfinity.exe");
                if (cbClose.Checked) Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RI executable not found!");
            }
        }
    }
}
