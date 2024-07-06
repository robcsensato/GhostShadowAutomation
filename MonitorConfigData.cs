using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class MonitorConfigData
    {
        public int GlobalTotalNumberOfMonitors;

        public struct RecorderedMonitorConfig
        {
            public bool Monitor_Found;

            //Only need Top and Left???

            public int Position_Top;
            public int Position_Bottom;
            public int Position_Left;
            public int Position_Right;
        };

        public struct CurrentMonitorConfig
        {
              //Only need Top and Left
              //Maybe only Height and Width
              //This can save on memory

            public int Position_Top;
            public int Position_Bottom;
            public int Position_Left;
            public int Position_Right;
            public int Height;
            public int Width;

            public int Delta_Height;
            public int Delta_Width;
        };

          public RecorderedMonitorConfig[] RecoderedMonitorSettings = new RecorderedMonitorConfig[3];
          public CurrentMonitorConfig[] CurrentMonitorSettings = new CurrentMonitorConfig[3];
    }
}
