using System;
using System.Drawing;
using System.Windows.Forms;

namespace Automation
{
    public class ResolutionAdjustment
    {
        MousePositionHolder MousePosition = new MousePositionHolder();

        public event GetRecorderedMonitorDataDelegate GetRecorderedMonitorDataEvent;
        //public event SetRecorderedMonitorDataDelegate SetRecorderedMonitorDataEvent;

        //public event SetRecorderedMonitorFoundDelegate SetRecorderedMonitorFoundEvent;
        public event GetRecorderedMonitorFoundDelegate GetRecorderedMonitorFoundEvent;

        public event GetCurrentMonitorDataDelegate GetCurrentMonitorDataEvent;
        //public event SetCurrentMonitorDataDelegate SetCurrentMonitorDataEvent;

        public event GetGlobalTotalNumberMonitors GetTotalNumberMonitors;

        //MonitorConfigData PlayController = new MonitorConfigData();

        Point pt = Cursor.Position;

        int GlobalTotalNumberofMonitors; 
        int GlobalWindowMangerPositionRight; 
        int GlobalWindowMangerPositionBottom; 
        int GlobalTotalAccmulatedCurrentDeltaX; 
        int GlobalTotalAccmulatedCurrentDeltaY;

        public MousePositionHolder CompensateForAnyResolutionChange(int convertedX,int convertedY)
        {   
            //GlobalTotalNumberofMonitors = ResolutionReader.GlobalTotalNumberofMonitors;
            GlobalTotalNumberofMonitors = GetTotalNumberMonitors();

            //GlobalWindowMangerPositionRight = ResolutionReader.GlobalWindowMangerPositionRight;
            //GlobalWindowMangerPositionBottom = ResolutionReader.GlobalWindowMangerPositionBottom;

            //GlobalWindowMangerPositionRight = PlayController.RecoderedMonitorSettings[0].Position_Right;
            //GlobalWindowMangerPositionBottom = PlayController.RecoderedMonitorSettings[0].Position_Bottom;

            GlobalWindowMangerPositionRight = GetRecorderedMonitorDataEvent(0, "right");
            GlobalWindowMangerPositionBottom = GetRecorderedMonitorDataEvent(0, "bottom");

            if (GlobalWindowMangerPositionRight > 0)
            {
                if (GlobalTotalNumberofMonitors == 0)
                {
                    if (CheckScreenResolution(GlobalWindowMangerPositionRight, GlobalWindowMangerPositionBottom))
                    {
                        //  LogWrite("Auto Adjusting for Current Resolution");
                        //  LogWrite("Orignal X: " + x_coordinate);
                        //  LogWrite("Orignal Y: " + y_coordinate);

                        MousePosition.RESOLUTION_CHANGE_OCCURED_FLAG = true;
                        
                        convertedX = ScreenResolutionAdjustedX(convertedX);
                        convertedY = ScreenResolutionAdjustedY(convertedY);
                        
                        //  LogWrite("Switching to X: " + x_position.ToString());
                        //  LogWrite("Switching to Y: " + y_position.ToString());
                        //  LogWrite("");
                    }
                      MousePosition.RESOLUTION_CHANGE_OCCURED_FLAG = false;
                }
                else
                {
                    //Using Multi-Monitor
                    if (CheckScreenResolutionMultiMonitor(convertedX, convertedY))
                    {
                        //  LogWrite("Auto Adjusting for Current Resolution Scheme");
                        //  LogWrite("Orignal X: " + x_coordinate);
                        //  LogWrite("Orignal Y: " + y_coordinate);
                        MousePosition.RESOLUTION_CHANGE_OCCURED_FLAG = true;
                        
                        convertedX = convertedX + GlobalTotalAccmulatedCurrentDeltaX;
                        convertedY = convertedY + GlobalTotalAccmulatedCurrentDeltaY;

                        //  LogWrite("Switching to X: " + x_position.ToString());
                        //  LogWrite("Switching to Y: " + y_position.ToString());
                        //  LogWrite("");
                    }
                      MousePosition.RESOLUTION_CHANGE_OCCURED_FLAG = false;
                }
            }

                MousePosition.xposition = convertedX;
                MousePosition.yposition = convertedY;
           
        return MousePosition;
        }

        private bool CheckScreenResolution(int GlobalWindowMangerPositionRight, int GlobalWindowMangerPositionBottom)
        {
          //  LogWrite("Checking for Current Resolution");

            Win32.Rect rc = ReturnWindowManagerScreenRefer();
            int Right = rc.right;
            int Bottom = rc.bottom;

            if (GlobalWindowMangerPositionRight != Right || GlobalWindowMangerPositionBottom != Bottom)
            {
          //      LogWrite("");
          //      LogWrite("Detected Change in Resolution");

          //      LogWrite("Orignal Resolution: " + GlobalWindowMangerPositionRight.ToString() + "X" + GlobalWindowMangerPositionBottom.ToString());
          //      LogWrite("Current Resolution: " + Right.ToString() + "X" + Bottom.ToString());
          //      LogWrite("");
                return true;
            }
            else
            {
                return false;
            }
        }

        private int ScreenResolutionAdjustedX(int x_position)
        {
            Win32.Rect rc = ReturnWindowManagerScreenRefer();
            int Right = rc.right;
            int deltaRightScreen = 0;

            if (GlobalWindowMangerPositionRight == Right)
            {
                return x_position;
            }

            if (GlobalWindowMangerPositionRight > Right)
            {
                deltaRightScreen = GlobalWindowMangerPositionRight - Right;
               // LogWrite("Adding to X: " + deltaRightScreen.ToString());
                return x_position = x_position + deltaRightScreen;
            }
            else
            {
                deltaRightScreen = Right - GlobalWindowMangerPositionRight;
               // LogWrite("Subtracting from X: " + deltaRightScreen.ToString());
                //return x_position = x_position - deltaRightScreen;
                return x_position = x_position - deltaRightScreen;
            }
        }

        private int ScreenResolutionAdjustedY(int y_position)
        {
            Win32.Rect rc = ReturnWindowManagerScreenRefer();
            int Bottom = rc.bottom;
            int deltaBottomScreen = 0;

            if (GlobalWindowMangerPositionBottom > Bottom)
            {
                deltaBottomScreen = GlobalWindowMangerPositionBottom - Bottom;
            //    LogWrite("Adding to Y: " + deltaBottomScreen.ToString());
                return y_position = y_position + deltaBottomScreen;
            }
            else
            {
                deltaBottomScreen = Bottom - GlobalWindowMangerPositionBottom;
            //    LogWrite("Subtracting from Y: " + deltaBottomScreen.ToString());
                return y_position = y_position - deltaBottomScreen;
            }
        }

        private Win32.Rect ReturnWindowManagerScreenRefer()
        {
            IntPtr WindowDesktopID = Win32.GetDesktopWindow();
            Win32.Rect rc = new Win32.Rect();
            Win32.GetWindowRect(WindowDesktopID, ref rc);

            return rc;
        }

        private bool CheckScreenResolutionMultiMonitor(int xposition, int yposition)
        {
           // LogWrite("Checking for any Resolution change");
           // LogWrite("");

            int TotalDeltaX = 0;
            int TotalDeltaY = 0;
            int CurrentMousePositionX = pt.X;
            int CurrentMousePositionY = pt.Y;
            //int xpositionValue = Convert.ToInt16(xposition);
            //int ypositionValue = Convert.ToInt16(yposition);
            int TargetMonitor = NewGetMonitorNumber(xposition, yposition);
            int CurrentMonitor = NewGetMonitorNumber(CurrentMousePositionX, CurrentMousePositionY);

            int tempTargetMonitor = TargetMonitor + 1;
            int tempCurrentMonitor = CurrentMonitor + 1;

           // LogWrite("Targer Monitor: " + tempTargetMonitor.ToString());
           // LogWrite("Current Monitor: " + tempCurrentMonitor.ToString());
           // LogWrite("");

            if (TargetMonitor > CurrentMonitor)
            {
                for (int I = CurrentMonitor; I < TargetMonitor; I++)
                {
                    //if (PlayController.RecoderedMonitorSettings[I].Monitor_Found)
                    if (GetRecorderedMonitorFoundEvent(I))
                    {
                        //TotalDeltaX = PlayController.CurrentMonitorSettings[I].Delta_Width;
                        //TotalDeltaY = PlayController.CurrentMonitorSettings[I].Delta_Height;

                        TotalDeltaX = GetCurrentMonitorDataEvent(I, "delta_width");
                        TotalDeltaY = GetCurrentMonitorDataEvent(I, "delta_height");

                        TotalDeltaX = TotalDeltaX + TotalDeltaX;
                        TotalDeltaY = TotalDeltaY + TotalDeltaY;
                    }
                }
            }

            if (CurrentMonitor > TargetMonitor)
            {
                for (int I = TargetMonitor; I < CurrentMonitor; I++)
                {
                    //if (PlayController.RecoderedMonitorSettings[I].Monitor_Found)
                    if (GetRecorderedMonitorFoundEvent(I))
                    {
                        //TotalDeltaX = PlayController.CurrentMonitorSettings[I].Delta_Width;
                        //TotalDeltaY = PlayController.CurrentMonitorSettings[I].Delta_Height;

                        TotalDeltaX = GetCurrentMonitorDataEvent(I, "delta_width");
                        TotalDeltaY = GetCurrentMonitorDataEvent(I, "delta_height");

                        TotalDeltaX = TotalDeltaX + TotalDeltaX;
                        TotalDeltaY = TotalDeltaY + TotalDeltaY;
                    }
                }
            }

            if (CurrentMonitor == TargetMonitor)
            {
                //if (PlayController.RecoderedMonitorSettings[CurrentMonitor].Monitor_Found)
                if (GetRecorderedMonitorFoundEvent(CurrentMonitor))
                {
                    //TotalDeltaX = PlayController.CurrentMonitorSettings[CurrentMonitor].Delta_Width;
                    //TotalDeltaY = PlayController.CurrentMonitorSettings[CurrentMonitor].Delta_Height;

                    TotalDeltaX = GetCurrentMonitorDataEvent(CurrentMonitor, "delta_width");
                    TotalDeltaY = GetCurrentMonitorDataEvent(CurrentMonitor, "delta_height");
                }
            }

            GlobalTotalAccmulatedCurrentDeltaX = TotalDeltaX;
            GlobalTotalAccmulatedCurrentDeltaY = TotalDeltaY;

            CurrentMonitor = CurrentMonitor + 1;

            if (TotalDeltaY == 0 && TotalDeltaX == 0)
            {
              //  LogWrite("No Change in Resolution Detected");
              //  LogWrite("");
                return false;
            }
            else
            {
              //  LogWrite("Change in Resolution Detected");
              //  LogWrite("");
                return true;
            }
        }

        private int NewGetMonitorNumber(int x, int y)
        {
            //LogWrite("Checking for Monitor Resolution");
            //LogWrite("Moving Mouse position");
            //LogWrite("");

            int MonitorNumber = 0;

            string DeviceNameString = "";
            string TheMonitorNumberString = "";
            string DeviceName = "";

            char[] delimiterChars = { 'Y' };
            char[] delimiterChar2 = { ' ' };

            pt.X = x;
            pt.Y = y;
            Cursor.Position = pt;

            Screen scrn = Screen.FromPoint(pt);
            DeviceName = scrn.DeviceName;

            // Split "DISPLAY1"
            string[] DeviceNameSplit = DeviceName.Split(delimiterChars);
            DeviceNameString = DeviceNameSplit[1].ToString();

            string[] MonitorNumberChar = DeviceNameString.Split(delimiterChar2);
            TheMonitorNumberString = MonitorNumberChar[0].ToString();

            switch (TheMonitorNumberString)
            {
                case "1":
                    {
                        MonitorNumber = 0;
                        break;
                    }
                case "2":
                    {
                        MonitorNumber = 1;
                        break;
                    }
                case "3":
                    {
                        MonitorNumber = 2;
                        break;
                    }
                case "4":
                    {
                        MonitorNumber = 3;
                        break;
                    }
                case "5":
                    {
                        MonitorNumber = 4;
                        break;
                    }
                case "6":
                    {
                        MonitorNumber = 5;
                        break;
                    }
                default:
                    {
                        // No Return for Monitor Numnber. Position is outside current Range
                        //MonitorNumber = 9999; 
                        break;
                    }
            }
            return MonitorNumber;
        }
    }
}
