using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fk_lib
{
    class setupIni
    {
        #region /*** Ini DLL ***/
        public string path;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public void IniWriteValue(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);
            //WritePrivateProfileString(Section, Key, Value, Application.StartupPath + "\\" + inipath);
        }
        public string IniReadValue(string Section, string Key, string iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            //int i = GetPrivateProfileString(Section, Key, "", temp, 255, Application.StartupPath + "\\" + iniPath);
            return temp.ToString();
        }
        public setupIni()
        {

        }
        public bool CkeckFileExists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
                
        }
        #endregion


    }
}
