using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Automation
{
    class TriggerCaptureWindow
    {
        CaptureKeyBoardEvents VirtualKeyBoardCommandTrigger = new CaptureKeyBoardEvents();
        
        public event DisplayImageBackToMainDelegate DisplayImageBackToMainEvent;
        
        public void SetUpDelegate()
        {
            VirtualKeyBoardCommandTrigger.DisplayImageEvent += new DisplayImageDelegate(VirtualKeyBoardCommandTrigger_DisplayImageEvent);
        }

        void VirtualKeyBoardCommandTrigger_DisplayImageEvent(System.Drawing.Image image, bool value, string mode)
        {
            DisplayImageBackToMainEvent(image,value,mode);
        }

        public void CapturePicture(XmlNode n)
        {
            string Command = n.ChildNodes.Item(0).InnerText.ToString();

            if (Command == "activewindow")
            {
                VirtualKeyBoardCommandTrigger.MappedPressedCommands("F1", 1,IntPtr.Zero);

                // Change to the following!!!
                //CaptureWindows.MappedPressedCommands("F1", filename);
            }

            if (Command == "desktop")
            {
                // This captures The Desktop Window
                VirtualKeyBoardCommandTrigger.MappedPressedCommands("F2", 1,IntPtr.Zero);
            }
        }

        public void TakePictureOfActiveWindow(IntPtr HandleID)
        {
            VirtualKeyBoardCommandTrigger.MappedPressedCommands("F1", 3, HandleID);
        }

        public void TakePictureOfDeskTop()
        {
            VirtualKeyBoardCommandTrigger.MappedPressedCommands("F2", 3,IntPtr.Zero);
        }

        public void StartCapturingOfActiveWindow(XmlNode n)
        {
            VirtualKeyBoardCommandTrigger.MappedPressedCommands("F1", 1,IntPtr.Zero);
        }

        public void StartCapturingOfDesktop(XmlNode n)
        {
            VirtualKeyBoardCommandTrigger.MappedPressedCommands("F2", 1,IntPtr.Zero);
        }
    }
}
