using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class WebTableDataClass
    {
        private string value;
        private int tr;
        private int td;
       
        public string Value
        {
            get
            {
                return value;
            }
        }

        public int ROW
        {
            get
            {
                return tr;
            }
        }

        public int COLUMN
        {
            get
            {
                return td;
            }
        }

        public WebTableDataClass(string value, int tr, int td)
        {
            this.value = value;
            this.tr = tr;
            this.td = td;
        }
    }
}
