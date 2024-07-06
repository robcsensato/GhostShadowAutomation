using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Automation
{
    public class ClickButtonHandler
    {
        public event ReturnIFClickedOnIEDelegate CHECK_IF_CLICKED_ON_IE_BROWSER_EVENT;

        public event GetUserDataString GetDataString;
        public event UpdateDataString UpdateData;

        CaptureWindowInfo WindowUtil = new CaptureWindowInfo();

        public event DisplayUserMouseEventMessage WriteMessageToUser;
        public event WriteMouseClickEventDelegate WriteMouseClickToXML;
        public event WriteButtonClickEventDelegate WriteButtonClickToXML;

        public event WriteIEClickToXMLDelegate WriteIEClickToXMLEvent;

        private string cleardata = "";

        public void CheckIfControlIsAButtonThenWriteXML(IntPtr hWnd, string CurrentClassName, string ButtonUsed, string ButtonX, string ButtonY, string ParentWindowTitle, string ClickCount, int WindowPositionLeft, int WindowPositionTop)
        {
            string UserData = GetDataString().ToString();

            if (!CheckForButton_WriteXML(ButtonUsed, CurrentClassName, hWnd, ClickCount, ButtonX, ButtonY, ParentWindowTitle, WindowPositionLeft, WindowPositionTop))
            {
                // This is a Normal Mouse Click. Not Clicking on a Button
                // ClickCount is either Single or Double
                ////////////////////////////////////////

                // CHECK IF CLICKED ON IE BROWSER
               if (CHECK_IF_CLICKED_ON_IE_BROWSER_EVENT()) // Check value of IEClickEventDetected
                   WriteIEClickToXMLEvent(); // After this is written set // Check value of IEClickEventDetected to false
               else
                {
                    UpdateData(cleardata);
                    WriteMouseClickToXML(ButtonUsed, ClickCount, ButtonX, ButtonY, ParentWindowTitle, WindowPositionTop, WindowPositionLeft);
                }
            }
        }

        private bool CheckForButton_WriteXML(string ButtonUsed, string CurrentClassName, IntPtr hWnd, string ClickCount, string ButtonX, string ButtonY, string ActiveWindow, int WindowPositionLeft, int WindowPositionTop)
        {
            // Checking if this is a Button
            // ClickCount equals either "Single" or "Double"
            ////////////////////////////////////////////////

            // Send Event to Retrieve UserDataString --> string UserData = GetUserDataString();
            string UserData = GetDataString().ToString();

            if (ReturnIfMatchOnClassNameButton(CurrentClassName))
            {
                //string CurrentCaption = WindowUtil.ReturnActiveWindow(hWnd);
                string CurrentCaption = WindowUtil.ReturnWindowText(hWnd);
                IntPtr ParentWindowID = Win32.GetParent(hWnd);
                string ParentWindowText = WindowUtil.ReturnWindowText(ParentWindowID);

                WriteMessageToUser("You Clicked on a Button!!!");
                WriteMessageToUser("Parent Window: " + ParentWindowText); // GET PARENT WINDOW INSTEAD OF CURRENT ACTIVE WINDOW. A new window may appear.
                WriteMessageToUser("Button Caption: " + CurrentCaption);

                if (UserData.Length > 0)
                {
                    WriteMessageToUser("Writing: " + UserData + " To XML");   
                    // WriteDataStringToXML(UserData);
                }

                UpdateData(cleardata);
                WriteButtonClickToXML(CurrentCaption, ClickCount, ButtonUsed, ButtonX, ButtonY, ParentWindowText, WindowPositionLeft, WindowPositionTop);
                
                return true;  // This is a Button and XML has been updated.
            }
            else
            {
                UpdateData(cleardata);
                return false; // This is NOT a Button
            }
        }

        private bool ReturnIfMatchOnClassNameButton(string classinfo)
        {
            Regex ButtonMatch = new Regex(".BUTTON.");

            if (classinfo == "Button" || ButtonMatch.IsMatch(classinfo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
