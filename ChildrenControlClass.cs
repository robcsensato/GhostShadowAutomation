using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class ChildrenControlClass
    {
        private string controlnumber;
        private string edittext;
        private string classinfo;
        private string handleid;

        public string ControlNumber
        {
            get
            {
                return controlnumber;
            }
        }

        public string EditText
        {
            get
            {
                return edittext;
            }
        }

        public string ClassInfo
        {
            get
            {
                return classinfo;
            }
        }

        public string HandleID
        {
            get
            {
                return handleid;
            }
        }

        public ChildrenControlClass(string controlnumber, string edittext, string classinfo, string handleid)
        { 
            this.controlnumber = controlnumber;
            this.classinfo = classinfo;
            this.edittext = edittext;
            this.handleid = handleid;
        }
    }
}
