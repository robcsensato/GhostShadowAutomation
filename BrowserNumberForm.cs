using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Automation
{
    public partial class Browser_NumberForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public event BrowserNumberDelegate BrowserNumber;

        public Browser_NumberForm()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            // Close the Browser_NumberForm Window
            this.Close();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            // Fire Event back to Main Form. Launch the Attach to IE browser method.

            string BrowserNumberText = Browser_NumberTextBox.Text;
            int ConvertedBrowserNumber = Convert.ToInt16(BrowserNumberText);
            BrowserNumber(ConvertedBrowserNumber);
        }
    }
}