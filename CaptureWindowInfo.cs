using System;
using System.Text;
//using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Automation
{
   public class CaptureWindowInfo
    {
       //These 'DLLs' are needed here. Can't place in Win32. Conflicts with other definitions

       [DllImport("user32.dll")]
       static extern int GetForegroundWindow();

       [DllImport("user32.dll")]
       static extern int GetWindowText(int hWnd, StringBuilder text, int count);

       string AutomationMainForm = "MainForm";

       public event SendWhatTypeOfWindowIsActiveDelegate SendWhatTypeOfWindowThisIsEvent;

       public event DisplayWindowEventMessage SendWindowMessage;

       public event SendMessageToPlayBackDelegate SendMessageToPlayBack;
       public event WriteNewWindowInfoToXMLDelegate WriteNewWindowInfoToXMLEvent;
       public event WriteIEWindowInforToXMLDelegate WriteIEBrowserTitleToXMLEvent;

        //private bool findwindow_exists; //--> Used with findwindowActivity method
        //private int findwindow_handle_count;
        
       private string LastActiveWindow;
       private IntPtr LastActiveWindowID;

       public struct OpenApplications
        {
            public string WindowTitle;
            public string ProcessName;
            public int Top;
            public int Bottom;
            public int Left;
            public int Right;
        }

       public OpenApplications[] OpenApplicationArray = new OpenApplications[10];

       public int GetWindowHandleID(string WindowName)
        {
           int current_window_handle = Win32.FindWindow(null, WindowName);
           return current_window_handle;
        }

       public bool CheckIfWindowExists(string WindowName)
       {
           int HandleID = GetWindowHandleID(WindowName);

           if (HandleID > 0)
           {
               SendMessageToPlayBack(WindowName + " Found.");
               SendMessageToPlayBack("");
                return true;
           }
           else
           {
               SendMessageToPlayBack("Warning! " + WindowName + "Does not exists.");
               SendMessageToPlayBack("I will not send any Messages to that window.");
               SendMessageToPlayBack("");
                return false;
           }
       }

       public bool CheckIfIEBrowserIsRunning()
       {
           bool IEBrowserFound = false;

           int NumberOfAppsOpen = this.OpenApplicationArray.Length;

           for (int I = 0; I < NumberOfAppsOpen; I++)
           {
               if (this.OpenApplicationArray[I].ProcessName == "iexplore")
               {
                   IEBrowserFound = true;
                   break;
               }
           }

           return IEBrowserFound;
       }

       public void findwindowActivity(string WindowName)
        {
            // Coming Soon...
            // This tracks specific windows..
            
            //findwindow_exists = false;
            //findwindow_handle_count = 0;
        }

       public string ReturnInternalName(IntPtr hWnd)
       { 
           StringBuilder formDetails = new StringBuilder(256);
           int txtValue = Win32.GetWindowText(hWnd, formDetails, 256);

           return formDetails.ToString().Trim();
       }

       public string ReturnActiveWindow()
       {
            StringBuilder Buff = new StringBuilder(256);
            IntPtr handle = Win32.GetForegroundWindow();
            Win32.GetWindowText(handle, Buff, 256);

            return Buff.ToString();
       }

       public string Return_True_Parent_Window(int x_mouse, int y_mouse)
       {
          string blank_title = "";
          int ArrayLength = OpenApplicationArray.Length;

            for (int I = 0; I < ArrayLength; I++)
            {
                 int Left = OpenApplicationArray[I].Left;
                 int Right = OpenApplicationArray[I].Right;
                 int Top = OpenApplicationArray[I].Top;
                 int Bottom = OpenApplicationArray[I].Bottom;

                 if (x_mouse > Left && x_mouse < Right && y_mouse > Top && y_mouse < Bottom)
                   return OpenApplicationArray[I].WindowTitle;
            }

           return blank_title;
       }

       public Win32.Rect ReturnWindowPositionInfo(int WindowID)
       {
           IntPtr IDconverted = new IntPtr(WindowID);

           Win32.Rect rc = new Win32.Rect();
           Win32.GetWindowRect(IDconverted, ref rc);

           return rc;
       }

       public void Discover_Open_Applications()
       { 
           System.Diagnostics.Process[] pros = System.Diagnostics.Process.GetProcesses(".");
           int index = 0;

           foreach (System.Diagnostics.Process p in pros)
               if (p.MainWindowTitle.Length > 0)
               {
                   OpenApplicationArray[index].WindowTitle = p.MainWindowTitle;
                   OpenApplicationArray[index].ProcessName = p.ProcessName;
                   
                   int WindowID = GetWindowHandleID(p.MainWindowTitle);
                   IntPtr IDconverted = new IntPtr(WindowID);

                   Win32.Rect rc = new Win32.Rect();
                   Win32.GetWindowRect(IDconverted, ref rc);
                   
                   OpenApplicationArray[index].Top =  rc.top;
                   OpenApplicationArray[index].Bottom = rc.bottom;
                   OpenApplicationArray[index].Left = rc.left;
                   OpenApplicationArray[index].Right = rc.right;
              
                   index++;
               }
       }

       public string ReturnWindowText(IntPtr handle)
       {
           StringBuilder Buff = new StringBuilder(256);
           Win32.GetWindowText(handle, Buff, 256);

           return Buff.ToString();
       }

       public IntPtr ReturnActiveWindowHandleID()
       {
           return Win32.GetForegroundWindow();     
       }

       private bool CheckIfWindowIsAnIEBrowser(IntPtr hWnd)
       {
           StringBuilder Buffer = new StringBuilder(256);
           Win32.GetClassName(hWnd, Buffer, 256);

           string CurrentClassName = Buffer.ToString();

           Regex WindowIEMatch = new Regex("IEFrame");
           Regex IETabMatch = new Regex("Internet");

           if (WindowIEMatch.IsMatch(CurrentClassName) || IETabMatch.IsMatch(CurrentClassName))
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       public void ActiveWindowBaedOnHandleID(int Handle)
       {
           IntPtr ConvertedHandle = new IntPtr(Handle);
           Win32.ShowWindow(ConvertedHandle, 6);
       }

       public void CheckForNewWindow()
       {
           string CurrentActiveWindow = ReturnActiveWindow();
           IntPtr CurrentActiveWindowHandleID = ReturnActiveWindowHandleID();

           if ((CurrentActiveWindow != LastActiveWindow) || (CurrentActiveWindowHandleID != LastActiveWindowID))
           {
                SendWindowMessage("New Window Found: " + CurrentActiveWindow);

                // Get Current RC 'Top and Left' positions based on CurrentActiveWindowID
                // int Top = RC.top
                // int Right = RC.right

                Win32.Rect rc = new Win32.Rect();
                Win32.GetWindowRect(CurrentActiveWindowHandleID, ref rc);
                
                int Top = rc.top;
                int Left = rc.left;

                bool IEFound = CheckIfWindowIsAnIEBrowser(CurrentActiveWindowHandleID);

                //bool IEFound = false; //--> Temp value since IEHandler class isn't implemented

                if (IEFound)
                {
                    SendWindowMessage("New Active IE Browser Found!!!");
                    SendWindowMessage("WindowID: " + CurrentActiveWindowHandleID.ToString());
                    SendWindowMessage("Title: " + CurrentActiveWindow);

                    SendWhatTypeOfWindowThisIsEvent("IE_BROWSER");
                    // --> Write to XML that this is a IEBROWER.. Create new event to do the following.
                    WriteIEBrowserTitleToXMLEvent(CurrentActiveWindow, Top, Left);
                    // --> XMLWrite.writeIEBrowserTitle_To_XML(CurrentActiveWindow, Top, Left);
                }
                else
                {
                   if (CurrentActiveWindow != AutomationMainForm)
                   {
                     SendWhatTypeOfWindowThisIsEvent("NON_BROWSER");
                     WriteNewWindowInfoToXMLEvent(CurrentActiveWindow,Top,Left);
                   }
                }
            }

            LastActiveWindow = CurrentActiveWindow;
            LastActiveWindowID = CurrentActiveWindowHandleID;
        }

    } // End of CaptureWindowInfo
}
