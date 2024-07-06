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
    public partial class TimeFormatWindow : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public TimeFormatWindow()
        {
            InitializeComponent();
        }

        public event SetTimeFormatDelegate SendUpdateTimeFormatEvent;

        private void Set_Button_Click(object sender, EventArgs e)
        {
            // allowed format values:
            // ----------------------
            // US     --> "en-US";
            // Japan  --> "ja-JP";
            // FRENCH --> "fr-FR"

            string Time = "en-US";

            if (US_Time.Checked)
                Time = "en-US";

            if (EuropeanTime.Checked)
                Time = "fr-FR";

            if (AsianTime.Checked)
                Time = "ja-JP";

            SendUpdateTimeFormatEvent(Time);
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            
        }
    }
}