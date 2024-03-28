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
using System.Reflection;
using Newtonsoft.Json;

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
            cbClose.Checked = Program.ProgramSettings.CloseOnLaunch;

            checkDirAndPatched();
            generateEnabled();
            generateDisabled();
        }

        private void checkDirAndPatched()
        {
            bool readSuccess = false, patched = false, fullscreen = false;

            while (!readSuccess)
            {
                try
                {
                    foreach (var line in File.ReadAllLines("data2.dat"))
                        if (line == "Catalog URL=https://www.ricochetuniverse.com/gateway/catalog.php" || line == "Catalog URL=http://www.ricochetuniverse.com/gateway/catalog.php")
                            patched = true;

                    foreach (var line in File.ReadAllLines("Ricochet Infinity.CFG"))
                        if (line == "Full Screen=1")
                            fullscreen = true;

                    readSuccess = true;
                }
                catch
                {
                    MessageBox.Show("An error has ocurred while reading the RI game files. You most likely chose the wrong directory as the game install directory. Please try selecting a different directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    using (var fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            Program.ProgramSettings.InstallPath = Environment.CurrentDirectory = fbd.SelectedPath;
                            File.WriteAllText(Program.ConfigPath, JsonConvert.SerializeObject(Program.ProgramSettings, Formatting.Indented));
                        }
                    }
                }
            }

            if (patched)
            {
                btnReviver.Enabled = false;
                label4.Visible = true;
            }

            cbFullscreen.Checked = fullscreen;
        }

        private void generateEnabled()
        {
            lbEnabled.Items.Clear();

            string[] enabled = Directory.GetFiles(".", "*.red", SearchOption.TopDirectoryOnly);

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while loading installed mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void generateDisabled()
        {
            lbDisabled.Items.Clear();

            string[] disabled = Directory.GetFiles(".", "*.disabled", SearchOption.TopDirectoryOnly);

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while loading installed mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region enable and disable
        private void btnDisable_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();

            foreach(string mod in lbEnabled.SelectedItems)
                selection.Add(mod);

            try
            {
                foreach (string modName in selection)
                {
                    int index = lbEnabled.Items.IndexOf(modName) + 10;

                    File.Move(".\\" + index.ToString() + "_" + modName + ".red", ".\\" + modName + ".disabled");
                    lbDisabled.Items.Add(modName);
                    generateEnabled();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while disabling the selected mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            int newIndex = lbEnabled.Items.Count + 10;
            
            List<string> selection = new List<string>();

            foreach (string mod in lbDisabled.SelectedItems)
                selection.Add(mod);

            try
            {
                foreach (string modName in selection)
                {
                    File.Move(".\\" + modName + ".disabled", ".\\" + newIndex.ToString() + "_" + modName + ".red");
                    lbEnabled.Items.Add(modName);
                    lbDisabled.Items.Remove(modName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while enabling the selected mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("RI executable not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    File.WriteAllBytes("./libcurl.dll", RI_Mod_Manager.Properties.Resources.libcurl);

                    for (int i = 0; i < lines.Length; i++)
                        if (lines[i].Length >= 11)
                            if (lines[i].Substring(0, 11) == "Catalog URL")
                                lines[i] = "Catalog URL=https://www.ricochetuniverse.com/gateway/catalog.php";

                    File.WriteAllLines("data2.dat", lines);
                }
                else
                {
                    for (int i = 0; i < lines.Length; i++)
                        if (lines[i].Length >= 11)
                            if (lines[i].Substring(0, 11) == "Catalog URL")
                                lines[i] = "Catalog URL=http://www.ricochetuniverse.com/gateway/catalog.php";

                    File.WriteAllLines("data2.dat", lines);
                }

                MessageBox.Show("Patching done!", "RI Mod Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReviver.Enabled = false;
                label4.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while updating the game files. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region drag and drop
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            try
            {
                foreach (string file in files)
                {
                    string[] split = file.Split('\\');
                    string fileName = split[split.Length - 1];

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while installing your mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                foreach (var selecEle in selection)
                {
                    var selIndex = lbEnabled.Items.IndexOf(selecEle);

                    if (selIndex != 0)
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

                foreach (var selecEle in selection)
                    lbEnabled.SelectedItems.Add(selecEle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while changing mod loading priority. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            List<string> selection = new List<string>();
            foreach (string mod in lbEnabled.SelectedItems)
                selection.Add(mod);

            lbEnabled.ClearSelected();

            try
            {
                for (int i = selection.Count - 1; i >= 0; i--)
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while changing mod loading priority. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region delete and install
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (var modName in lbDisabled.SelectedItems)
                    {
                        File.Delete(".\\" + modName + ".disabled");
                    }

                    generateDisabled();

                    foreach (var modName in lbEnabled.SelectedItems)
                    {
                        var index = lbEnabled.Items.IndexOf(modName) + 10;
                        File.Delete(".\\" + index.ToString() + "_" + modName + ".red");
                    }

                    generateEnabled();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred while deleting your mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                        MessageBox.Show($"An error has occurred while installing your mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            Program.ProgramSettings.CloseOnLaunch = cbClose.Checked;
            File.WriteAllText(Program.ConfigPath, JsonConvert.SerializeObject(Program.ProgramSettings, Formatting.Indented));
        }

        private void cbFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines("Ricochet Infinity.CFG");

                for (int i = 0; i < lines.Length; i++)
                    if (lines[i].Length >= 11)
                        if (lines[i].Substring(0, 11) == "Full Screen")
                            lines[i] = "Full Screen=" + (cbFullscreen.Checked ? "1" : "0");

                File.WriteAllLines("Ricochet Infinity.CFG", lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred while updating the game files. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCd_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Program.ProgramSettings.InstallPath = Environment.CurrentDirectory = fbd.SelectedPath;
                    File.WriteAllText(Program.ConfigPath, JsonConvert.SerializeObject(Program.ProgramSettings, Formatting.Indented));
                }
            }

            checkDirAndPatched();
            generateEnabled();
            generateDisabled();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start($"{Program.ProgramSettings.InstallPath}");
            }
            catch
            {
                MessageBox.Show("Unable to open RI folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
