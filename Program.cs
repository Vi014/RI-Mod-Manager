using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RI_Mod_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var file in args)
            {
                string[] split = file.Split('\\');
                string fileName = split[split.Length - 1];

                try
                {
                    if (fileName.Substring(fileName.Length - 4).ToLower() == ".red")
                    {
                        File.Copy(file, ".\\" + fileName, true);
                    }
                    else if (fileName.Substring(fileName.Length - 9).ToLower() == ".disabled")
                    {
                        File.Copy(file, ".\\" + fileName, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred while installing your mods: {ex}");
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
