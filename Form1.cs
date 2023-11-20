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

        #region startup
        private void Form1_Load(object sender, EventArgs e)
        {
            generateEnabled();
            generateDisabled();

            if (File.Exists("modmanager.cfg"))
            {
                if (File.ReadAllText("modmanager.cfg") == "y")
                {
                    cbClose.Checked = true;
                }
            }
            else
            {
                File.AppendAllText("modmanager.cfg", "n");
            }
        }

        private void generateEnabled()
        {
            lbEnabled.Items.Clear();

            string[] enabled = Directory.GetFiles(".", "*.red", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < enabled.Length; i++)
            {
                var modName = enabled[i];
                var counter = i + 10;

                if (Int32.TryParse(modName.Substring(2, 2), out int temp) && modName[4] == '_')
                {
                    if (temp != counter)
                        File.Move(modName, ".\\" + counter.ToString() + "_" + modName.Remove(0, 5));
                    lbEnabled.Items.Add(modName.Remove(0, 5).Remove(modName.Length - 9));
                }
                else
                {
                    File.Move(modName, ".\\" + counter.ToString() + "_" + modName.Remove(0, 2));
                    lbEnabled.Items.Add(modName.Remove(0, 2).Remove(modName.Length - 6));
                }
            }
        }

        private void generateDisabled()
        {
            lbDisabled.Items.Clear();

            string[] disabled = Directory.GetFiles(".", "*.disabled", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < disabled.Length; i++)
            {
                var modName = disabled[i];

                if (Int32.TryParse(modName.Substring(2, 2), out int temp) && modName[4] == '_')
                {
                    File.Move(modName, ".\\" + modName.Substring(5));
                    lbDisabled.Items.Add(modName.Remove(0, 5).Remove(modName.Length - 14));
                }
                else
                {
                    lbDisabled.Items.Add(modName.Remove(0, 2).Remove(modName.Length - 11));
                }
            }
        }
        #endregion

        #region enable and disable
        private void btnDisable_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();

            foreach(string mod in lbEnabled.SelectedItems)
                selection.Add(mod);

            foreach(string modName in selection)
            {
                int index = lbEnabled.Items.IndexOf(modName) + 10;

                File.Move(".\\" + index.ToString() + "_" + modName + ".red", ".\\" + modName + ".disabled");
                lbDisabled.Items.Add(modName);
                generateEnabled();
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            int newIndex = lbEnabled.Items.Count + 10;
            
            List<string> selection = new List<string>();

            foreach (string mod in lbDisabled.SelectedItems)
                selection.Add(mod);

            foreach (string modName in selection)
            {
                File.Move(".\\" + modName + ".disabled", ".\\" + newIndex.ToString() + "_" + modName + ".red");
                lbEnabled.Items.Add(modName);
                lbDisabled.Items.Remove(modName);
            }
        }
        #endregion

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

        #region reviver
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
        #endregion

        #region drag and drop
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                string[] split = file.Split('\\');
                string fileName = split[split.Length - 1];

                try
                {
                    if (fileName.Substring(fileName.Length - 4).ToLower() == ".red")
                    {
                        File.Copy(file, ".\\" + fileName);
                        generateEnabled();
                    }
                    else if (fileName.Substring(fileName.Length - 9).ToLower() == ".disabled")
                    {
                        File.Copy(file, ".\\" + fileName);
                        generateDisabled();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred while installing your mods: {ex}");
                }
            }
        }
        #endregion

        #region up and down
        private void btnUp_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (string mod in lbEnabled.SelectedItems)
                selection.Add(mod);

            lbEnabled.ClearSelected();

            foreach (var selecEle in selection)
            {
                var selIndex = lbEnabled.Items.IndexOf(selecEle);

                if(selIndex != 0)
                {
                    var uppIndex = selIndex - 1;
                    var upperEle = lbEnabled.Items[uppIndex];

                    lbEnabled.Items[selIndex] = upperEle;
                    lbEnabled.Items[uppIndex] = selecEle;

                    var upOffset = uppIndex + 10;
                    var slOffset = selIndex + 10;

                    File.Move(".\\" + upOffset.ToString() + "_" + upperEle + ".red", ".\\" + slOffset.ToString() + "_" + upperEle + ".red");
                    File.Move(".\\" + slOffset.ToString() + "_" + selecEle + ".red", ".\\" + upOffset.ToString() + "_" + selecEle + ".red");
                }
            }

            foreach(var selecEle in selection)
                lbEnabled.SelectedItems.Add(selecEle);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (string mod in lbEnabled.SelectedItems)
                selection.Add(mod);

            lbEnabled.ClearSelected();

            for(int i = selection.Count - 1; i >= 0; i--)
            {
                var selecEle = selection[i];
                var selIndex = lbEnabled.Items.IndexOf(selecEle);

                if (selIndex != lbEnabled.Items.Count - 1)
                {
                    var lowIndex = selIndex + 1;
                    var lowerEle = lbEnabled.Items[lowIndex];

                    lbEnabled.Items[selIndex] = lowerEle;
                    lbEnabled.Items[lowIndex] = selecEle;

                    var lwOffset = lowIndex + 10;
                    var slOffset = selIndex + 10;

                    File.Move(".\\" + lwOffset.ToString() + "_" + lowerEle + ".red", ".\\" + slOffset.ToString() + "_" + lowerEle + ".red");
                    File.Move(".\\" + slOffset.ToString() + "_" + selecEle + ".red", ".\\" + lwOffset.ToString() + "_" + selecEle + ".red");
                }
            }

            foreach (var selecEle in selection)
                lbEnabled.SelectedItems.Add(selecEle);
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach(var modName in lbDisabled.SelectedItems)
                {
                    try
                    {
                        File.Delete(".\\" + modName + ".disabled");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error has occurred: {ex}");
                    }
                }

                generateDisabled();

                foreach (var modName in lbEnabled.SelectedItems)
                {
                    try
                    {
                        var index = lbEnabled.Items.IndexOf(modName) + 10;
                        File.Delete(".\\" + index.ToString() + "_" + modName + ".red");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error has occurred: {ex}");
                    }
                }

                generateEnabled();
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ricochet mods (*.red;*.disabled)|*.red;*.disabled";

                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    var file = ofd.FileName;
                    string[] split = file.Split('\\');
                    string fileName = split[split.Length - 1];

                    try
                    {
                        if (fileName.Substring(fileName.Length - 4).ToLower() == ".red")
                        {
                            File.Copy(file, ".\\" + fileName, true);
                            generateEnabled();
                        }
                        else if (fileName.Substring(fileName.Length - 9).ToLower() == ".disabled")
                        {
                            File.Copy(file, ".\\" + fileName, true);
                            generateDisabled();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error has occurred while installing your mods: {ex}");
                    }
                }
            }
        }

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            if (cbClose.Checked) File.WriteAllText("modmanager.cfg", "y");
            else File.WriteAllText("modmanager.cfg", "n");
        }
    }
}
