using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
//using System.Runtime.InteropServices;
using System.Diagnostics;
//using System.IO;



namespace Automation
{
    public partial class WebCapture : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public WebCapture()
        {
            InitializeComponent();

            this.capturesize.Items.AddRange(new object[] {
            "100",
            "90",
            "80",
            "70",
            "60",
            "50",
            "40"});

            this.qualityselect.Items.AddRange(new object[] {
            "100",
            "90",
            "80",
            "70",
            "50",
            "30",
            "10"
            });

            this.capturesize.Text = "100";
            this.qualityselect.Text = "100";
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "C:\\IECapture_TEST");
        }

    }
}