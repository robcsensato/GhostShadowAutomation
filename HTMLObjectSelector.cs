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
    public partial class HTMLObjectSelector : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public HTMLObjectSelector()
        {
            InitializeComponent();
        }

        public event SendHTMLObjectDelegate SendHTMLObjectEvent;

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (InnerTextButton.Checked == true)
            {    
                SendHTMLObjectEvent("InnerText");
            }

            if (OuterHtmlButton.Checked == true)
            {
                SendHTMLObjectEvent("OuterHtml");
            }

            if (HREFButton.Checked == true)
            {
                SendHTMLObjectEvent("HREF");
            }

            if (NameButton.Checked == true)
            {
                SendHTMLObjectEvent("Name");
            }

            if (OuterHtmlButton.Checked == true)
            {
                SendHTMLObjectEvent("OuterHtml");
            }

            if (AltButton.Checked == true)
            {
                SendHTMLObjectEvent("ALT");
            }

            if (tagNameButton.Checked == true)
            {
                SendHTMLObjectEvent("tagName");
            }

            if (OuterTextButton.Checked == true)
            {
                SendHTMLObjectEvent("OuterText");
            }

            if (UserDefinedButton.Checked == true)
            {
                string UserDefinedValue = UserDefinedTextBox.Text;
                // Send UserDefinedEvent(UserDefinedValue);
            }
        }
    }
}