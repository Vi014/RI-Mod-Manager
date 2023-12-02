using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace RI_Mod_Manager
{
    static class Program
    {
        private static string configPath = AppDomain.CurrentDomain.BaseDirectory + "modManagerCfg.json";
        private static JsonSettings programSettings;

        public static string ConfigPath { get => configPath; set => configPath = value; }
        public static JsonSettings ProgramSettings { get => programSettings; set => programSettings = value; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!File.Exists(ConfigPath))
            {
                if (MessageBox.Show("Would you like to select your Ricochet Infinity install directory? If not, the program will assume the directory it's located in is also RI's install directory.", "First launch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                            createNewConfig(fbd.SelectedPath);
                        else
                            createNewConfig(AppDomain.CurrentDomain.BaseDirectory);
                    }
                }
                else
                    createNewConfig(AppDomain.CurrentDomain.BaseDirectory);
            }

            programSettings = JsonConvert.DeserializeObject<JsonSettings>(File.ReadAllText(ConfigPath));

            Environment.CurrentDirectory = programSettings.InstallPath;

            foreach (var file in args)
            {
                string[] split = file.Split('\\');
                string fileName = split[split.Length - 1];

                try
                {
                    if (fileName.Substring(fileName.Length - 4).ToLower() == ".red")
                        File.Copy(file, ".\\" + fileName, true);
                    else if (fileName.Substring(fileName.Length - 9).ToLower() == ".disabled")
                        File.Copy(file, ".\\" + fileName, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred while installing your mods. \r\nIf you have the game installed in the Program Files folder, try running the mod manager as administrator. \r\n \r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Application.Run(new Form1());
        }

        static void createNewConfig (string path)
        {
            var data = new JsonSettings(false, path);

            try
            {
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has ocurred while saving the settings file. \r\nIf you're running the mod manager inside the Program Files folder, try running it as administrator. \r\n\r\nDetails: \r\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
