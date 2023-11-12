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
            catch
            {
                MessageBox.Show("RI executable not found!");
            }
        }

        private void btnReviver_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines("data2.dat");

                if (System.Environment.OSVersion.Version.Major >= 10)
                {
                    File.Copy(".\\libcurl.dll", ".\\libcurl.dll.bak", true);
                    File.Delete(".\\libcurl.dll");

                    try
                    {
                        progressBar1.Visible = true;

                        using (var client = new WebClient())
                        {
                            client.DownloadProgressChanged += wc_DownloadProgressChanged;
                            client.DownloadFileAsync(new System.Uri("https://www.ricochetuniverse.com/misc/libcurl.dll"), "libcurl.dll");
                        }

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Length >= 11)
                                if (lines[i].Substring(0, 11) == "Catalog URL")
                                    lines[i] = "Catalog URL=https://www.ricochetuniverse.com/gateway/catalog.php";
                        }

                        File.WriteAllLines("data2.dat", lines);
                        progressBar1.Visible = false;
                    }
                    catch
                    {
                        MessageBox.Show("Unable to download new version of libcurl.dll! Data2 will be patched to connect to the http version of Ricochet Universe instead of https.");
                        File.Move(".\\libcurl.dll.bak", ".\\libcurl.dll");

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Length >= 11)
                                if (lines[i].Substring(0, 11) == "Catalog URL")
                                    lines[i] = "Catalog URL=http://www.ricochetuniverse.com/gateway/catalog.php";
                        }

                        File.WriteAllLines("data2.dat", lines);

                        progressBar1.Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Length >= 11)
                            if (lines[i].Substring(0, 11) == "Catalog URL")
                                lines[i] = "Catalog URL=http://www.ricochetuniverse.com/gateway/catalog.php";
                    }

                    File.WriteAllLines("data2.dat", lines);
                }

                MessageBox.Show("Patching done!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            /* foreach (string file in files)
            {
                File.Copy(file, $".\\{file}", true);
            } */
        }
    }
}
