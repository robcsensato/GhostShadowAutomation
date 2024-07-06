using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Automation
{
    class WindowSearchFunctionsDuringPlayBack
    {
         public WriteToPlayBackFromWindowSearchDelegate WriteToPlayBack;

        // public bool WindowEventBasedOnRegularExpression(string rex_title)
        // {
        //     string current_window = GetCurrentActiveWindowTitle();
        //     bool DoesTitleContainTheValue(rex_title,current_window;
        // }

        public bool SearchForWindowTitle(string title)
        {
            int current_window_handle = Win32.FindWindow(null, title);

            if (current_window_handle > 0)
            {
                // Window is Found
                return true;
            }
            else
            {
                // Window was NOT Found
                return false;
            }
        }

        public string SearchForWindowTitleUsingRegularExpression(string expression)
        {
            string WindowTitle = "";

            // Start search of all Processes
         System.Diagnostics.Process[] pros = System.Diagnostics.Process.GetProcesses(".");

           foreach (System.Diagnostics.Process p in pros)
               if (p.MainWindowTitle.Length > 0)
               {
                   WriteToPlayBack("Process Found: " + p.MainWindowTitle);
                   bool MatchesExpression = DoesTitleMatchTheFollowing(expression, p.MainWindowTitle);

                   if (MatchesExpression)
                   {
                       WindowTitle = p.MainWindowTitle;
                     break;
                   }
                }

            return WindowTitle;
        }
        

        private bool DoesTitleMatchTheFollowing(string expression, string window_title)
        {
            Regex Pattern = new Regex(expression);

            if (Pattern.IsMatch(window_title))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SearchForStrictWindowTitleWithinMonitor(string title)
        {
            int current_window_handle = Win32.FindWindow(null, title);

            if (current_window_handle > 0)
            {
                // Window is Found Check It's within Current Monitor Configution
                // ... Create New Method within New Class for IsWindowWithinCurrentMonitorConfigEvent
                // ... Logic is as follows
                // ...   Get Position of Window
                // ...   Get Current Window Positon From TargetWindowInfoData --> returns MousePositionHolder. windowpositionx = MousePositionHolder.x & windowpositiony = MousePositionHolder.y
                // ...   RecordedMonitorNumber = GetMonitorNumberFromRecordedMonitorConfigData(windowpositionx, windowpositiony);
                // ...   CurrentMonitorNumber = GetMonitorNumberFromCurrentMonitorConfigData(windowpositionx, windowpositiony);
                // ...   if (RecordedMonitorNumber == CurrentMonitorNumber)
                // ...   return true
                // ...   else
                // ...   return false
                // ... END OF NEW METHOD FOR IsWindowWithinCurrentMonitorConfigEvent

                //-> Following Becomes within Current Statement. Waiting for IsWindowWithinCurrentMonitorConfigEvent to be created

                // bool WindowFoundWithinMonitor = IsWindowWithinCurrentMonitorConfigEvent(current_window_handle);
                // return WindowFoundWithinMonitor;

                return true;
            }
            else
            {
                // Window was NOT Found
                return false;
            }
        }
    }
}
