using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.Windows.Forms;

namespace Automation
{
    public class PlayBackResolutionReader
    {
        public event SendMessageToMainPlayBackControl LogWritePlayBack;

        public event GetRecorderedMonitorDataDelegate GetRecorderedMonitorDataEvent;
        public event SetRecorderedMonitorDataDelegate SetRecorderedMonitorDataEvent;

        public event SetRecorderedMonitorFoundDelegate SetRecorderedMoitorFoundEvent;

        public event GetCurrentMonitorDataDelegate GetCurrentMonitorDataEvent;
        public event SetCurrentMonitorDataDelegate SetCurrentMonitorDataEvent;

        public event SetGlobalTotalNumberMonitors SetTotalGlobalNumberMonitors;

        public event AdjustRecordedMonitorDataDelegate AdjustRecordMonitorDataEvent;
                    
        Point pt = Cursor.Position;

        //MonitorConfigData PlayController = new MonitorConfigData(); // This is no longer needed

        int GlobalTotalWorkingAreaHeight;
        int GlobalTotalWorkingAreaLength;

        int GlobalTotalNumberofMonitors;
        int GlobalWindowMangerPositionBottom;
        int GlobalWindowMangerPositionRight;

        public void GetOrignalResolutionWindowManger(XmlNode n)
        {
            XmlNodeList xmlnodepath = null;
            int NumberofNodes = n.ChildNodes.Count;

            //LogWrite("Reading Recorded Resolution...");

            pt.X = 10;
            pt.Y = 10;

            IntPtr handleID = Win32.WindowFromPoint(pt);
            Win32.Rect rc = new Win32.Rect();
            Win32.GetWindowRect(handleID, ref rc);

            GlobalTotalWorkingAreaHeight = rc.Height;
            GlobalTotalWorkingAreaLength = rc.Width;

            xmlnodepath = n.SelectNodes("//monitor");

            if (xmlnodepath.Count > 0)
            {
               LogWritePlayBack("Found Monitor Resolutions Records");

                MonitorInstructionsFound(xmlnodepath);

                if (GlobalTotalNumberofMonitors == 1)
                {
                    LogWritePlayBack("Only 1 Monitor Configured");
                    LogWritePlayBack("Playing back in Sinlge Monitor Mode.");
                 //   LogWrite("In Future, ask user if this is ok.");
                    LogWritePlayBack("");

                    GlobalTotalNumberofMonitors = 0;
                    SetTotalGlobalNumberMonitors(0);

                    //GlobalWindowMangerPositionBottom = PlayController.RecoderedMonitorSettings[0].Position_Bottom;
                    //GlobalWindowMangerPositionRight = PlayController.RecoderedMonitorSettings[0].Position_Right;

                    GlobalWindowMangerPositionBottom = GetRecorderedMonitorDataEvent(0, "bottom");

                    //LogWritePlayBack("Recorded: " + GlobalWindowMangerPositionBottom);
                 
                    GlobalWindowMangerPositionRight = GetRecorderedMonitorDataEvent(0, "right");
                }

                if (GlobalTotalNumberofMonitors > 1)
                    CheckCurrentMonitorConfiguration();
            }
            else
            {
                MonitorNotFoundInstructions(NumberofNodes, n);
            }
        }

        private void CheckCurrentMonitorConfiguration()
        {
            int CurrentMonitorPosition_Right = 0;
            int NumberofMonitors = 0;

            NumberofMonitors = Screen.AllScreens.Length;
            string NumberofMonitorsString = NumberofMonitors.ToString();

           // LogWrite("Multi Monitor Support");
           // LogWrite("Checking for Current Monitor Configuration");
           // LogWrite("Total Number of Monitors Defined From XML: " + GlobalTotalNumberofMonitors.ToString());
           // LogWrite("");
           // LogWrite("Gathering STATS...");

           // LogWrite("Number of Monitors Currently Detected: " + NumberofMonitorsString);
           // LogWrite("");

            CurrentMonitorPosition_Right = GetMonitorResolution(10, 10, 0);

            //PlayController.RecoderedMonitorSettings[0].Monitor_Found = true;
            SetRecorderedMoitorFoundEvent(0, true);

            GatherDeltaForCurrentMonitor(0);

            for (int I = 1; I < NumberofMonitors; I++)
            {
                //PlayController.RecoderedMonitorSettings[I].Monitor_Found = true;
                SetRecorderedMoitorFoundEvent(I, true);

                CurrentMonitorPosition_Right = GetMonitorResolution(CurrentMonitorPosition_Right + 10, 10, I);
                CurrentMonitorPosition_Right = CurrentMonitorPosition_Right + CurrentMonitorPosition_Right;
                GatherDeltaForCurrentMonitor(I);
            }

            if (GlobalTotalNumberofMonitors > NumberofMonitors)
            {
                for (int A = NumberofMonitors + 1; A < GlobalTotalNumberofMonitors + 1; A++)
                {
                  //  LogWrite("Monitor: " + A.ToString() + " NOT Availble..");
                }
                  //  LogWrite("");
            }

           // LogWrite("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
           // LogWrite("Finished Monitor Configuration");
           // LogWrite("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
           // LogWrite("");
        }

        private void GatherDeltaForCurrentMonitor(int MonitorNumber)
        {
            int CurrentMonitorNumber = MonitorNumber + 1;

          //  LogWrite("Checking for Change in Resolution");
          //  LogWrite("Checking Monitor: " + CurrentMonitorNumber.ToString());

            //int RightValueFromXML = PlayController.RecoderedMonitorSettings[MonitorNumber].Position_Right;
            //int LeftValueFromXML = PlayController.RecoderedMonitorSettings[MonitorNumber].Position_Left;
            //int TopValueFromXML = PlayController.RecoderedMonitorSettings[MonitorNumber].Position_Top;
            //int BottomValueFromXML = PlayController.RecoderedMonitorSettings[MonitorNumber].Position_Bottom;

            int RightValueFromXML = GetRecorderedMonitorDataEvent(MonitorNumber, "right");
            int LeftValueFromXML = GetRecorderedMonitorDataEvent(MonitorNumber, "left");
            int TopValueFromXML = GetRecorderedMonitorDataEvent(MonitorNumber, "top");
            int BottomValueFromXML = GetRecorderedMonitorDataEvent(MonitorNumber, "bottom");

            int WidthFromXML = RightValueFromXML - LeftValueFromXML;
            int HeightFromXML = BottomValueFromXML - TopValueFromXML;

            //int CurrentHeight = PlayController.CurrentMonitorSettings[MonitorNumber].Height;
            //int CurrentWidth = PlayController.CurrentMonitorSettings[MonitorNumber].Width;

            int CurrentHeight = GetCurrentMonitorDataEvent(MonitorNumber, "height");
            int CurrentWidth = GetCurrentMonitorDataEvent(MonitorNumber, "width");

          //  LogWrite("CurrentWidth: " + CurrentWidth.ToString());
          //  LogWrite("CurrentHeight: " + CurrentHeight.ToString());

          //  LogWrite("");

            int DifferenceHeight = CurrentHeight - HeightFromXML;
            int DifferenceWidth = CurrentWidth - WidthFromXML;

            //PlayController.CurrentMonitorSettings[MonitorNumber].Delta_Height = DifferenceHeight;
            //PlayController.CurrentMonitorSettings[MonitorNumber].Delta_Width = DifferenceWidth;

            SetCurrentMonitorDataEvent(MonitorNumber, "delta_height", DifferenceHeight);
            SetCurrentMonitorDataEvent(MonitorNumber, "delta_width", DifferenceWidth);

            if (DifferenceHeight == 0) // TODO:Display in Verbose Mode --> if (DifferenceHeight == 0 && verbosemode_event())
            {               
              //  LogWritePlayBack("No Change in Dimension Height");
                // verbosemode_event --> Make this an event that returns true or false. 
            }
            else
            {
             //   LogWrite("Difference in Height: " + DifferenceHeight.ToString
                LogWritePlayBack("Difference in Height: " + DifferenceHeight.ToString());
            }

            if (DifferenceWidth == 0) // TODO: include Verbose Mode
            {
             //   LogWrite("No Change in Dimension Width");
            }
            else
            {
               LogWritePlayBack("Difference in Width: " + DifferenceWidth.ToString());
            }
             //   LogWrite("");
        }

        private int GetMonitorResolution(int x, int y, int monitornumber)
        {
            pt.X = x;
            pt.Y = y;
            Cursor.Position = pt;

            Screen scrn = Screen.FromPoint(pt);

            string test1 = scrn.DeviceName;

            string test2 = scrn.WorkingArea.ToString();
            string test3 = scrn.Bounds.ToString();

            int X = scrn.WorkingArea.Location.X;
            int Y = scrn.WorkingArea.Location.Y;

            int Height = scrn.WorkingArea.Height;
            int Width = scrn.WorkingArea.Width;
            int Left = scrn.WorkingArea.Left;
            int Right = scrn.WorkingArea.Right;
            int Top = scrn.WorkingArea.Top;
            int Bottom = scrn.WorkingArea.Bottom;

            //PlayController.CurrentMonitorSettings[monitornumber].Height = Height;
            //PlayController.CurrentMonitorSettings[monitornumber].Width = Width;
            //PlayController.CurrentMonitorSettings[monitornumber].Position_Right = Right;
            //PlayController.CurrentMonitorSettings[monitornumber].Position_Left = Left;
            //PlayController.CurrentMonitorSettings[monitornumber].Position_Top = Top;
            //PlayController.CurrentMonitorSettings[monitornumber].Position_Bottom = Bottom;

            SetCurrentMonitorDataEvent(monitornumber, "height", Height);
            SetCurrentMonitorDataEvent(monitornumber, "width", Width);
            SetCurrentMonitorDataEvent(monitornumber, "right", Right);
            SetCurrentMonitorDataEvent(monitornumber, "left", Left);
            SetCurrentMonitorDataEvent(monitornumber, "top", Top);
            SetCurrentMonitorDataEvent(monitornumber, "bottom", Bottom);

         //   LogWrite("DeviceName: " + test1);
         //   LogWrite("");
         //   LogWrite("Working Area: " + test2);
         //   LogWrite("");
         //   LogWrite("Bounds: " + test3);
         //   LogWrite("");
         //   LogWrite("Here's Height: " + Height.ToString());
         //   LogWrite("Here's Width: " + Width.ToString());
         //   LogWrite("Here's Left: " + Left.ToString());
         //   LogWrite("Here's Right: " + Right.ToString());
         //   LogWrite("Here's Top: " + Top.ToString());
         //   LogWrite("Here's Bottom: " + Bottom.ToString());
         //   LogWrite("");

            return Right;
        }

        private void MonitorInstructionsFound(XmlNodeList xmlnodepath)
        {
            int Top = 0;
            int Bottom = 0;
            int Left = 0;
            int Right = 0;
            int Count = 0;
            int Height = 0;
            int Length = 0;

            foreach (XmlNode node in xmlnodepath)
            {
                int MonitorNumber = Count + 1;
               // LogWrite("---> MonitorNumber: " + MonitorNumber.ToString());

                XmlNodeList childnodes = node.ChildNodes;

                foreach (XmlNode child in childnodes)
                {
                    switch (child.Name)
                    {
                        case "Right":
                            {
                                string RightString = child.InnerText;
                                Right = Convert.ToInt16(RightString);
                                break;
                            }
                        case "Left":
                            {
                                string LeftString = child.InnerText;
                                Left = Convert.ToInt16(LeftString);
                                break;
                            }
                        case "Top":
                            {
                                string TopString = child.InnerText;
                                Top = Convert.ToInt16(TopString);
                                break;
                            }
                        case "Bottom":
                            {
                                string BottomString = child.InnerText;
                                Bottom = Convert.ToInt16(BottomString);
                                break;
                            }
                    }
                }

                if (Count == 0)
                {
                    //PlayController.RecoderedMonitorSettings[Count].Position_Bottom = Bottom;
                    //PlayController.RecoderedMonitorSettings[Count].Position_Left = Left;
                    //PlayController.RecoderedMonitorSettings[Count].Position_Right = Right;
                    //PlayController.RecoderedMonitorSettings[Count].Position_Top = Top;
                    //PlayController.RecoderedMonitorSettings[Count].Monitor_Found = false;

                    SetRecorderedMonitorDataEvent(Count, "bottom", Bottom);
                    SetRecorderedMonitorDataEvent(Count, "left", Left);
                    SetRecorderedMonitorDataEvent(Count, "right", Right);
                    SetRecorderedMonitorDataEvent(Count, "top", Top);
                    SetRecorderedMoitorFoundEvent(Count, false);
                }
                else
                {
                    AdjustRecordMonitorDataEvent(Count, "left", Length);
                    AdjustRecordMonitorDataEvent(Count, "right", Length);
                    SetRecorderedMoitorFoundEvent(Count, false);
                }

                Height = Bottom - Top;
                Length = Right - Left;

              //  LogWrite("-- Recorded Resolution");
              //  LogWrite("Length: " + Length.ToString());
              //  LogWrite("Heigth: " + Height.ToString());
              //  LogWrite("");

                Count = Count + 1;
                GlobalTotalNumberofMonitors = Count;
                SetTotalGlobalNumberMonitors(GlobalTotalNumberofMonitors);
            }
        }

        private void MonitorNotFoundInstructions(int NumberofNodes, XmlNode n)
        {
            //Monitor Tag is not found. However, Right and Bottom tags exists

            int Right = 0;
            int Bottom = 0;
            GlobalTotalNumberofMonitors = 0; //This means using Single Monitor
            SetTotalGlobalNumberMonitors(0);

            if (NumberofNodes > 0)
            {
              //  WindowScreenResolution Tag is defined but without <Monitor> child tag..
              
              //  LogWrite("Recorded in Single Monitor Mode");
              //  LogWrite("Ask user if this is correct in the future.");

                //Get Right and Bottom Positions
                ///////////////////////////

                XmlNodeList childnodes = n.ChildNodes;
                foreach (XmlNode child in childnodes)
                {
                    switch (child.Name)
                    {
                        case "Right":
                            {
                                string RightString = child.InnerText;
                                Right = Convert.ToInt16(RightString);
                                break;
                            }

                        case "Bottom":
                            {
                                string BottomString = child.InnerText;
                                Bottom = Convert.ToInt16(BottomString);
                                break;
                            }
                    }
                }

              ////////////////////////////
              //  LogWrite("Recorded Length: " + Right.ToString());
              //  LogWrite("Recorded Height: " + Bottom.ToString());
              //  LogWrite("");

                GlobalWindowMangerPositionRight = Right;
                GlobalWindowMangerPositionBottom = Bottom;
            }
            else
            {
                // Nothing defined at ALL
                LogWritePlayBack("Incomplete Definition..No Resolution Information at ALL.");
                LogWritePlayBack("Setting Current Monitor Resolution..");
                LogWritePlayBack("Ask user if this is OK in the future.");

                Win32.Rect rc = ReturnWindowManagerScreenRefer();
                GlobalWindowMangerPositionBottom = rc.bottom;
                GlobalWindowMangerPositionRight = rc.right;
                GlobalTotalNumberofMonitors = 0;
                SetTotalGlobalNumberMonitors(0);

                //PlayController.RecoderedMonitorSettings[0].Position_Right = GlobalWindowMangerPositionRight;
                //PlayController.RecoderedMonitorSettings[0].Position_Bottom = GlobalWindowMangerPositionBottom;
                //PlayController.RecoderedMonitorSettings[0].Monitor_Found = true;

                SetRecorderedMonitorDataEvent(0, "right", GlobalWindowMangerPositionRight);
                SetRecorderedMonitorDataEvent(0, "bottom", GlobalWindowMangerPositionBottom);
                SetRecorderedMoitorFoundEvent(0, true);

                LogWritePlayBack("Current Monitor Setting: " + GlobalWindowMangerPositionRight.ToString() + " X " + GlobalWindowMangerPositionBottom.ToString());
                LogWritePlayBack("");
            }// End of IF Number of Nodes > 0
        }

        private Win32.Rect ReturnWindowManagerScreenRefer()
        {
            IntPtr WindowDesktopID = Win32.GetDesktopWindow();
            Win32.Rect rc = new Win32.Rect();
            Win32.GetWindowRect(WindowDesktopID, ref rc);
            return rc;
        }
    }
}
