namespace Windows_Update_Error
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label3 = new Label();
            label1 = new Label();
            tabPage2 = new TabPage();
            button1 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            button3 = new Button();
            groupBox1 = new GroupBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Font = new Font("微软雅黑", 22.125F, FontStyle.Bold, GraphicsUnit.Point, 134);
            tabControl1.Location = new Point(35, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.RightToLeft = RightToLeft.Yes;
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1047, 611);
            tabControl1.TabIndex = 0;
            tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label1);
            tabPage1.Font = new Font("微软雅黑", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 134);
            tabPage1.ForeColor = SystemColors.ControlText;
            tabPage1.Location = new Point(8, 92);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1031, 511);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "首页";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("微软雅黑", 96F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label3.Location = new Point(229, 181);
            label3.Name = "label3";
            label3.Size = new Size(645, 338);
            label3.TabIndex = 1;
            label3.Text = "TVT";
            label3.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微软雅黑", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(15, 6);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(965, 171);
            label1.TabIndex = 0;
            label1.Text = "更新失败，如果你现在关闭计算机，那么计算机\r\n将无法启动，如果你想恢复你的计算机，就得想\r\n办法获取关闭Windows_Update_Error相关的Key\r\n";
            label1.Click += label1_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.HotTrack;
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(textBox1);
            tabPage2.ForeColor = SystemColors.ControlText;
            tabPage2.Location = new Point(8, 92);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1031, 511);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Decrypt";
            // 
            // button1
            // 
            button1.Font = new Font("微软雅黑", 22.125F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button1.Location = new Point(686, 290);
            button1.Name = "button1";
            button1.Size = new Size(297, 91);
            button1.TabIndex = 2;
            button1.Text = "decrypt";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = SystemColors.HotTrack;
            label2.Font = new Font("微软雅黑", 18F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(6, 199);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(343, 64);
            label2.TabIndex = 1;
            label2.Text = "Decrypt_Key:";
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(355, 185);
            textBox1.Name = "textBox1";
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.Size = new Size(628, 85);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resource1.锁;
            pictureBox1.Location = new Point(1118, 126);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(203, 199);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button2.Location = new Point(37, 47);
            button2.Name = "button2";
            button2.Size = new Size(170, 51);
            button2.TabIndex = 3;
            button2.Text = "Get the Key";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button3.Location = new Point(37, 121);
            button3.Name = "button3";
            button3.Size = new Size(170, 93);
            button3.TabIndex = 4;
            button3.Text = "Destroy\r\ncomputer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.Location = new Point(1097, 385);
            groupBox1.Name = "groupBox1";
            groupBox1.RightToLeft = RightToLeft.No;
            groupBox1.Size = new Size(241, 241);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "操作";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Maroon;
            ClientSize = new Size(1357, 705);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(tabControl1);
            Cursor = Cursors.Default;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Windows_Update_Error";
            Load += Form2_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage2;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private TabPage tabPage1;
        private Label label1;
        private Label label3;
        private PictureBox pictureBox1;
        private Button button2;
        private Button button3;
        private GroupBox groupBox1;
    }
}