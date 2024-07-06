using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Automation
{
    public class CaptureMouseEvents
    {
        public event ReplayMouseClickEventDelagate SendClickEventToMain;

        public event TurnDoubleClickTimerOn TurnTimerOnEvent;
        public event TurnDoubleClickTimerOff TurnTimerOffEvent;
        public event DisplayUserMouseEventMessage SendUserMessage;
        
        public event WriteMouseUpEventDelegate WriteMouseUPToXML;
        public event WriteMouseDownEventDelegate WriteMouseDOWNToXML;

        public event CheckIfControlIsAButtonDelegate CheckifControlIsAButton;

        CaptureWindowInfo WindowUtil = new CaptureWindowInfo();

        public bool DisplayOnlyOnce = true;
        public bool VerboseMode = false;

        int Global_Click_Count;
        int currentposition_x;
        int currentposition_y;

        string DownButtonPressedValue;

        bool mouse_clicked = false;
        bool mouse_pressed_down = false;
        
        string Global_ButtonX;
        string Global_ButtonY;
        string Global_ButtonUsed;

        public void MouseDown(object sender, MouseEventArgs e)
        {
            DisplayOnlyOnce = false;

            if (e.Clicks > 0)
            {
                mouse_pressed_down = true;
                DownButtonPressedValue = e.Button.ToString();
            }
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            string ButtonUsed = e.Button.ToString();
            string ButtonX = e.X.ToString();
            string ButtonY = e.Y.ToString();

            DisplayOnlyOnce = true;

            CheckIfThisIsAClick(ButtonUsed, ButtonX, ButtonY);
          
            if (e.Clicks > 0)
            {
                if (!mouse_clicked)
                {
                    string MouseButtonUsed = e.Button.ToString();
                    
                    ///// BUG FIX...Shouldn't perform this section unless Recording as Started/////
                    ///// Since I started at load time the MouseMove and MouseUp Events. This was 
                    //    for GhostSpy++

                    /// CHECK WITH MAINFORM.. IF RECORDING IS TAKING PLACE THEN OK TO GO AHEAD//
                    /// CREATE EVENT --> ASK_MAINFORM_CHECK_IF_RECORDING();
                    // Return a Boolean Value
                    
                    /// bool CurrentlyRecording = ASK_MAINFORM_CHECK_IF_RECORDING();
                    /// if (CurrentlyRecording)
                    /// {
                    String SpecialUserMessage = "Mouse Moved";
                    string Message = "MouseUp " + MouseButtonUsed;

                    SendUserMessage(SpecialUserMessage);
                    SendUserMessage(Message);

                    WriteMouseUPToXML(ButtonUsed, ButtonX, ButtonY);
                    //   }
                    ///////////////////////////////////////////////////
                }
                else
                {
                    SendClickEventToMain();
                }
            }
            else
            {
                string Message = "Mouse X: " + ButtonX + " , " + "Y: " + ButtonY;
                SendUserMessage(Message);
            }
        }

        private void CheckIfThisIsAClick(string ButtonUsed, string ButtonX, string ButtonY)
        {
            if (mouse_pressed_down)
            {
                mouse_pressed_down = false;

                mouse_clicked = true;
                
                if (ButtonUsed == "None")
                {
                    SendUserMessage("MouseDown X: " + ButtonX + " , " + "Y: " + ButtonY);

                    WriteMouseDOWNToXML(DownButtonPressedValue, ButtonX, ButtonY);
                }
                else
                {
                    Global_ButtonX = ButtonX;
                    Global_ButtonY = ButtonY;
                    Global_ButtonUsed = ButtonUsed;
                    
                    Global_Click_Count = Global_Click_Count + 1;

                    TurnTimerOnEvent();
                }
            }
        }

        public void MouseMoved(object sender, MouseEventArgs e)
        {
            mouse_clicked = false;

            currentposition_x = e.X;
            currentposition_y = e.Y;

            string ButtonX = e.X.ToString();
            string ButtonY = e.Y.ToString();

            if (mouse_pressed_down)
            {
                mouse_pressed_down = false;
                SendUserMessage("MouseDown " + DownButtonPressedValue);

                WriteMouseDOWNToXML(DownButtonPressedValue, ButtonX, ButtonY);
            }

            ////////
            if (DisplayOnlyOnce)
            {
               DisplayOnlyOnce = false;

               // Only Display this if Recording.
               /// CHECK WITH MAINFORM.. IF RECORDING IS TAKING PLACE THEN OK TO GO AHEAD//
               /// CREATE EVENT --> ASK_MAINFORM_CHECK_IF_RECORDING();
               // Return a Boolean Value

               /// bool CurrentlyRecording = ASK_MAINFORM_CHECK_IF_RECORDING();
               /// if (CurrentlyRecording)
               /// {
               string Message = "MouseMoved X: " + ButtonX + " , " + "Y: " + ButtonY;
               SendUserMessage(Message);
               /// }

            }

            // Send ALL MouseMoved Messages
            //
            if (VerboseMode)
            {
               string Message = "MouseMoved X: " + ButtonX + " , " + "Y: " + ButtonY;
               SendUserMessage(Message);
            }
   
            //WriteMousePosition(currentposition_x, currentposition_y); --> May Keep!!This writes to a label.
        }

        public void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            TurnTimerOffEvent();

            IntPtr hWnd = Win32.WindowFromPoint(Cursor.Position);

            StringBuilder Buffer = new StringBuilder(256);
            Win32.GetClassName(hWnd, Buffer, 256);
            
            string ButtonX = Global_ButtonX;
            string ButtonY = Global_ButtonY;
            string ButtonUsed = Global_ButtonUsed;
            string CurrentClassName = Buffer.ToString();
            string ActiveWindow =  WindowUtil.ReturnActiveWindow();
          
            string ClickCountType;

            if (ActiveWindow != "MainForm")
            {
                if (Global_Click_Count == 2)
                {
                    //Thread.Sleep(1200);
                    SendUserMessage("Double Click " + ButtonUsed + " X:" + ButtonX + " Y:" + ButtonY);
                    ClickCountType = "Double";
                }
                else
                {
                    IntPtr test = Win32.GetFocus();
                    Win32.Rect rc = new Win32.Rect();
                    Win32.GetWindowRect(test, ref rc);

                    int left = rc.left;
                    //Thread.Sleep(1200);
                    SendUserMessage("Single Click " + ButtonUsed + " X:" + ButtonX + " Y:" + ButtonY);
                    ClickCountType = "Single";
                }

                Global_Click_Count = 0;

                // This is the Active Window Rectangle Reference
                // To be used for Relative Corridante Play back..

                Win32.Rect win_rc = new Win32.Rect();
                
                IntPtr activeWindowID = WindowUtil.ReturnActiveWindowHandleID();
                Win32.GetWindowRect(activeWindowID, ref win_rc);

                int WindowPositionLeft = win_rc.left;
                int WindowPositionTop = win_rc.top;

                CheckifControlIsAButton(hWnd, CurrentClassName, ButtonUsed, ButtonX, ButtonY, ActiveWindow, ClickCountType, WindowPositionLeft, WindowPositionTop);
            }
        }
    }
}
