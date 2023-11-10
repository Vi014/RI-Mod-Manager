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
using System.Net;

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

        private void btnReviver_Click(object sender, EventArgs e)
        {
            if(System.Environment.OSVersion.Version.Major >= 10) // swap this so the order is try-filestream-if os
            {
                try
                {
                    //File.Copy(".\\libcurl.dll", ".\\libcurl.dll.bak", true);
                    //File.Delete(".\\libcurl.dll");
                    FileStream data2 = new FileStream("data2.dat", FileMode.Open, FileAccess.ReadWrite);

                    try
                    {
                        progressBar1.Visible = true;
                        /* using (var client = new WebClient())
                        {
                            client.DownloadProgressChanged += wc_DownloadProgressChanged;
                            client.DownloadFileAsync(new System.Uri("https://www.ricochetuniverse.com/misc/libcurl.dll"), "libcurl.dll");
                        } */

                        StreamReader reader = new StreamReader(data2);
                        StreamWriter writer = new StreamWriter(data2);

                        while(!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line.Length >= 11)
                            {
                                if (line.Substring(0, 11) == "Catalog URL")
                                {
                                    writer.Write("Catalog URL=https://www.ricochetuniverse.com/gateway/catalog.php"); // doesn't seem to work
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                        MessageBox.Show("Unable to download new version of libcurl.dll! Data2 will be patched to connect to the http version of Ricochet Universe instead of https.");
                        File.Move(".\\libcurl.dll.bak", ".\\libcurl.dll");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                    throw;
                }
            }
            else
            {

            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
