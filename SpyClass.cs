using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class SpyClass
    {
        public IntPtr CurrentWindowHandleID;

        IntPtr hPreviousWindow;

        public event SetClassNameDelegate SetClassNameEvent;
        public event SetWindowTextDelegate SetWindowTextEvent;
        public event SetParentWindowDelegate SetParentWindowTextEvent;
 
        public void SetUpSpyInformation(IntPtr hWnd)
        {
            CurrentWindowHandleID = hWnd;

            try
            {
                // if the window we're over, is not the same as the one before, and we had one before, refresh it
                if (hPreviousWindow != IntPtr.Zero && hPreviousWindow != hWnd)
                    WindowHighlighter.Refresh(hPreviousWindow);

                // if didn't find a window
                if (hWnd == IntPtr.Zero)
                {
                    //_textBoxHandle.Text = null;
                    //_textBoxClass.Text = null;
                    //_textBoxText.Text = null;
                    //_textBoxStyle.Text = null;
                    //_textBoxRect.Text = null;
                }
                else
                {
                    // save the window we're over
                    hPreviousWindow = hWnd;

                    // class
                    //ClassLabel.Text = this.GetClassName(hWnd);
                    SetClassNameEvent(this.GetClassName(hWnd));

                    // caption
                    //CaptionLabel.Text = this.GetWindowText(hWnd);
                    SetWindowTextEvent(this.GetWindowText(hWnd));

                    // parent 
                    //ParentWindow.Text = "Parent: " + this.GetParentWindow(hWnd).ToString();
                    SetParentWindowTextEvent(this.GetParentWindowText(hWnd));

                    //Win32.Rect rc = new Win32.Rect();
                    //Win32.GetWindowRect(hWnd, ref rc);

                    // rect
                    //RectLabel.Text = "Rect: " + string.Format("[{0} x {1}]", rc.right - rc.left, rc.bottom - rc.top);
                   //({2},{3})-({4},{5}) left , top , right , bottom ", rc.right - rc.left, rc.bottom - rc.top, rc.left, rc.top, rc.right, rc.bottom);

                   //int top = rc.top;
                   //int bottom = rc.bottom;
                   //int left = rc.left;
                   //int right = rc.right;


                   // highlight the window
                   //WindowHighlighter.Highlight(hWnd);
                }
            }

            catch (Exception ex)
            {
               // Debug.WriteLine(ex);
            }
        }

        private string GetClassName(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(256);
            Win32.GetClassName(hWnd, sb, 256);
            return sb.ToString();
        }

        private string GetWindowText(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(256);
            Win32.GetWindowText(hWnd, sb, 256);
            return sb.ToString();
        }

        private string GetParentWindowText(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(256);
            IntPtr parentWindowID;
            parentWindowID = Win32.GetParent(hWnd);

            Win32.GetWindowText(parentWindowID, sb, 256);
            return sb.ToString();
        }
    } // End of SpyClass
}
