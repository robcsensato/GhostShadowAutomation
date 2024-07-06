using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class ControlPropertiesClass
    {
        private string propertyname;
        private object propertyvalue;

        public string PropertyName
        {
            get
            {
                return propertyname;
            }
        }

        public object PropertyValue
        {
            get
            {
                return propertyvalue;
            }
        }

        public ControlPropertiesClass(string propertyname, object propertyvalue)
        {
            this.propertyname = propertyname;
            this.propertyvalue = propertyvalue; 
        }

       // public override string ToString()
       // {
       //     return propertyname + ", " + propertyvalue;
       // }
    }
}
