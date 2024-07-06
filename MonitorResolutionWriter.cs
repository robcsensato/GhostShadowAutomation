using System;
using System.Xml;
using System.Text;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Automation
{
    public class MonitorResolutionWriter
    {
        XMLWriter XMLWrite = new XMLWriter();
        Point pt = Cursor.Position;

        public event LogWriteToRecordWindowFromMonitorResolutionDelegate LogWrite;

        public void WriteScreenResolutionToXML(string XMLFILE)
        {
            XMLWrite.WriteInitialMonitorConfigurationNode(XMLFILE);

            int CurrentMonitorPosition_Right = 0;
            int NumberofMonitors = 0;

            NumberofMonitors = Screen.AllScreens.Length;
            string NumberofMonitorsString = NumberofMonitors.ToString();

            //LogWrite("Multi Monitor Support");
            LogWrite("Checking for Current Monitor Configuration");
            //LogWrite("Total Number of Monitors Defined From XML: " + GlobalTotalNumberofMonitors.ToString());
            //LogWrite("");
            //LogWrite("Gathering STATS...");

            LogWrite("Number of Monitors Currently Detected: " + NumberofMonitorsString);
            LogWrite("");

            LogWrite("Writing Following Monitor Information to XML");
            LogWrite("");
            CurrentMonitorPosition_Right = GetMonitorResolution(0, XMLFILE);
            //MonitorSettings[0].Monitor_Found = true;
            //GatherDeltaForCurrentMonitor(0);

            for (int I = 1; I < NumberofMonitors; I++)
            {
                //MonitorSettings[I].Monitor_Found = true;
                CurrentMonitorPosition_Right = GetMonitorResolution(I,XMLFILE);
                CurrentMonitorPosition_Right = CurrentMonitorPosition_Right + CurrentMonitorPosition_Right;
                //GatherDeltaForCurrentMonitor(I);
            }
        }

        private int GetMonitorResolution(int monitornumber, string XMLFILE)
        {
            //pt.X = x;
            //pt.Y = y;
            //Cursor.Position = pt;
            pt = Cursor.Position;

            Screen scrn = Screen.FromPoint(pt);

            string test1 = scrn.DeviceName;

            string test2 = scrn.WorkingArea.ToString();
            string test3 = scrn.Bounds.ToString();

            int X = scrn.WorkingArea.Location.X;
            int Y = scrn.WorkingArea.Location.Y;

            int Height = scrn.WorkingArea.Height;
            int Width = scrn.WorkingArea.Width;
            //int Left = scrn.WorkingArea.Left;
            int Right = scrn.WorkingArea.Right;
            //int Top = scrn.WorkingArea.Top;
            //int Bottom = scrn.WorkingArea.Bottom;

            //CurrentMonitorSettingsToXML[monitornumber].Height = Height;
            //CurrentMonitorSettingsToXML[monitornumber].Width = Width;

            //CurrentMonitorSettingsToXML[monitornumber].Position_Right = Right;
            //CurrentMonitorSettingsToXML[monitornumber].Position_Left = Left;
            //CurrentMonitorSettingsToXML[monitornumber].Position_Top = Top;
            //CurrentMonitorSettingsToXML[monitornumber].Position_Bottom = Bottom;

            ///LogWrite("DeviceName: " + test1);
            ///LogWrite("");
            ///LogWrite("Working Area: " + test2);
            ///LogWrite("");
            ///LogWrite("Bounds: " + test3);
            ///LogWrite("");
            ///LogWrite("Here's Height: " + Height.ToString());
            ///LogWrite("Here's Width: " + Width.ToString());
            ///LogWrite("Here's Left: " + Left.ToString());
            ///LogWrite("Here's Right: " + Right.ToString());
            ///LogWrite("Here's Top: " + Top.ToString());
            ///LogWrite("Here's Bottom: " + Bottom.ToString());
            ///LogWrite("");
            ///
            int UserFriendlyMonitorNumber = monitornumber + 1;

            LogWrite("Current Resolution for Monitor: " + UserFriendlyMonitorNumber.ToString());
            LogWrite("Width: " + Width.ToString());
            LogWrite("Height: " + Height.ToString());
            LogWrite("");

            XMLWrite.updateMonitorConfig(Height.ToString(), Width.ToString(),XMLFILE);

            return Right;
        }
    }
}
