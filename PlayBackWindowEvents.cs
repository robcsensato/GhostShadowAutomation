using System;
using System.Xml;

namespace Automation
{
    class PlayBackWindowEvents
    {
        //TargetWindowInfoData TargetWindowInfo = new TargetWindowInfoData();

        public event SetTargetWindowInfoDelegate SetTargetWindowInfoEvent;

        bool UsingRegressionExpression = false;

        int GlobalWindowTopPosition = 0;
        int GlobalWindowLeftPosition = 0;

        public string WindowEvent(XmlNode n)
        {
            string eventID;
            string WindowTitle;
            string Top;
            string Left;

          // This is to make reading the xml robust... Come Back
                            
          //      XmlNodeList childnodes = n.ChildNodes;

          //      foreach (XmlNode child in childnodes)
          //      {
          //          switch (child.Name)
          //          {
          //              case "Right":
          //                  {
          //                      string RightString = child.InnerText;
          //                      Right = Convert.ToInt16(RightString);
          //                      break;
          //                  }
          //              case "Left":
          //                  {
          //                      string LeftString = child.InnerText;
          //                      Left = Convert.ToInt16(LeftString);
          //                      break;
          //                  }
          //              case "Top":
          //                  {
          //                      string TopString = child.InnerText;
          //                      Top = Convert.ToInt16(TopString);
          //                     break;
          //                  }
          //              case "Bottom":
          //                  {
          //                      string BottomString = child.InnerText;
          //                      Bottom = Convert.ToInt16(BottomString);
          //                      break;
          //                  }
          //          }
          //      }
                
          // Coming Soon!!!!
          // ---------------
          // <WindowEvent eventid = "1" regex="true">
          // <title>admin*</title>      ---> Searches for admin* within current window title
          // <top>10</top>
          // <bottom>20</bottom>
          //  </WindowEvent>

          // For Now using this::
          // <WindowEvent regex="true"> 
          // <eventID>1</eventID>
          // <title>admin*</title>
          // <top>10</top>
          // <bottom>20</bottom>
          // </WindowEvent>

            if (n.Attributes.Count > 0)
            {
                if (n.Attributes.Item(0).Name == "regex")
                {
                    string regex_value = n.Attributes.Item(0).Value;
                    if (regex_value == "true")
                    {
                        UsingRegressionExpression = true;
                    }
                    else
                    {
                        UsingRegressionExpression = false;
                    }
                }
            }
            else
            {
                UsingRegressionExpression = false;
            }

            eventID = n.ChildNodes.Item(0).InnerText.ToString();
            WindowTitle = n.ChildNodes.Item(1).InnerText.ToString();

            Top = n.ChildNodes.Item(2).InnerText.ToString();
            Left = n.ChildNodes.Item(3).InnerText.ToString();

            int TopConverted = Convert.ToInt16(Top);
            int LeftConverted = Convert.ToInt16(Left);

            GlobalWindowTopPosition = TopConverted;
            GlobalWindowLeftPosition = LeftConverted;

            //TargetWindowInfo.Top = TopConverted;
            //TargetWindowInfo.Left = LeftConverted;

            //SetTargetWindowInfoEvent(WindowTitle, TopConverted, LeftConverted);

            SetTargetWindowInfoEvent(WindowTitle, TopConverted, LeftConverted, UsingRegressionExpression);

            return WindowTitle;
        }

        public int ReturnRecordedTopPosition()
        {
            return GlobalWindowTopPosition;
        }

        public int ReturnRecordedLeftPosition()
        {
            return GlobalWindowLeftPosition;
        }


        // Moved to WindowSearchFunctionsDuringPlayBack
        // ---------------------------------------------

        // public bool WindowEventBasedOnRegularExpression(string rex_title)
        // {
        //     string current_window = GetCurrentActiveWindowTitle();
        //     bool DoesTitleContainTheValue(rex_title,current_window;
        // }

        //public bool SearchForWindowTitle(string title)
        //{
        //    int current_window_handle = Win32.FindWindow(null, title);

        //    if (current_window_handle > 0)
        //    {
        //       // Window is Found
        //        return true;
        //    }
        //    else
        //    {
        //       // Window was NOT Found
        //        return false;
        //    }
        //}

       // public bool SearchForWindowTitleUsingRegularExpression(string expression)
       // {
       //     //     string current_window = GetCurrentActiveWindowTitle();
       //     //     return DoesTitleMatchTheFollowig(expression,current_window);
       //     return false;
       // }

       // public bool SearchForStrictWindowTitleWithinMonitor(string title)
       // {
       //     int current_window_handle = Win32.FindWindow(null, title);
       //
       //     if (current_window_handle > 0)
       //     {
       //         // Window is Found Check It's within Current Monitor Configution
       //         // ... Create New Method within New Class for IsWindowWithinCurrentMonitorConfigEvent
       //         // ... Logic is as follows
       //         // ...   Get Position of Window
       //         // ...   Get Current Window Positon From TargetWindowInfoData --> returns MousePositionHolder. windowpositionx = MousePositionHolder.x & windowpositiony = MousePositionHolder.y
       //         // ...   RecordedMonitorNumber = GetMonitorNumberFromRecordedMonitorConfigData(windowpositionx, windowpositiony);
       //         // ...   CurrentMonitorNumber = GetMonitorNumberFromCurrentMonitorConfigData(windowpositionx, windowpositiony);
       //         // ...   if (RecordedMonitorNumber == CurrentMonitorNumber)
       //         // ...   return true
       //         // ...   else
       //         // ...   return false
       //         // ... END OF NEW METHOD FOR IsWindowWithinCurrentMonitorConfigEvent
       //
       //         //-> Following Becomes within Current Statement. Waiting for IsWindowWithinCurrentMonitorConfigEvent to be created

       //         // bool WindowFoundWithinMonitor = IsWindowWithinCurrentMonitorConfigEvent(current_window_handle);
       //         // return WindowFoundWithinMonitor;

       //         return true;
       //     }
       //     else
       //     {
       //         // Window was NOT Found
       //        return false;
       //     }
       // }

    }
}
