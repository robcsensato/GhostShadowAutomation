using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.ManagedSpy;

namespace Automation
{
    public class ReflectionClass
    {
        List<ControlPropertiesClass> controlprop = new List<ControlPropertiesClass>();
        
        public event RelayControlTextInformationBackToMainDelegate ControlText; // Get ready to take this out..

        // public event ReplayPropertyControlValuesToGhostSpyWindowDelegate LogWriteToGhostSpyMainWindowEvent;

        public event RelayControlPropertiesInfoBackToMain RelayControlInfoBackToMainEvent;

        public event CheckIfCurrentParentWindowIstheAutomationApplicationDelegate CheckIfCurrentParentWindowIsAutomationApplication_Event; 
        
        public void UseReflectionOnControl(IntPtr ControlHandleID) 
        {
            if (CheckIfCurrentParentWindowIsAutomationApplication_Event())
             {
                controlprop.Clear();
                Control controller = Control.FromHandle(ControlHandleID);
                Type controlType = controller.GetType();
                PropertyInfo[] properties = controlType.GetProperties();

                ProcessInformation(properties, controller);
             }
             else
             {
                controlprop.Clear();
                ControlProxy controller = ControlProxy.FromHandle(ControlHandleID);
                Type controlType = controller.GetType();
                PropertyInfo[] properties = controlType.GetProperties();

                ProcessInformation(properties, controller);
             }
        }

        private void ProcessInformation(PropertyInfo[] properties, ControlProxy control)
        {
            foreach (PropertyInfo controlProperty in properties)
            {
                object value = controlProperty.GetValue(control, null);
                //string message = "Control Property Name: " + controlProperty.Name + " Value: " + tester;
                string name = controlProperty.Name;

                controlprop.Add(new ControlPropertiesClass(name, value));

                //ControlText(message);
                //ControlText("");
            }

            RelayControlInfoBackToMainEvent(controlprop);
        }

        private void ProcessInformation(PropertyInfo[] properties, Control control)
        {
            foreach (PropertyInfo controlProperty in properties)
            {
                object value = controlProperty.GetValue(control, null);
                //string message = "Control Property Name: " + controlProperty.Name + " Value: " + tester;
                string name = controlProperty.Name;
                
                controlprop.Add(new ControlPropertiesClass(name,value));
                
                //ControlText(message);
                //ControlText("");
            }

            RelayControlInfoBackToMainEvent(controlprop);
        }
    }
}
