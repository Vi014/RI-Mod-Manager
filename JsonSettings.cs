using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RI_Mod_Manager
{
    public class JsonSettings
    {
        private bool closeOnLaunch;
        private string installPath;

        public bool CloseOnLaunch { get => closeOnLaunch; set => closeOnLaunch = value; }
        public string InstallPath { get => installPath; set => installPath = value; }

        public JsonSettings(bool COL, string IP)
        {
            closeOnLaunch = COL;
            installPath = IP;
        }
    }
}
