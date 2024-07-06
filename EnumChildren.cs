using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Automation
{
    class EnumChildren
    {
        [DllImport("User32.dll")]
        public static extern Boolean EnumChildWindows(int hWndParent, Delegate lpEnumFunc, int lParam);

        public delegate int Callback(int hWnd, int lParam);

        public event ReplayLogWriteFromEnumChildrenToPlayControlDelegate LogWrite; // Get Ready to take out.. Not needed

        public event ReplayChildrenPropertiesInfoBackToMain SendParentWindowChildrenControlInfoToMain_Event;

        List<ChildrenControlClass> childrencontrolclass = new List<ChildrenControlClass>();

        bool UsingControlFindToSendInformationBackToMain = false;

        private const long CLR_NONE = 0xFFFFFFFF;
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETBKIMAGE = (LVM_FIRST + 68);
        private const int LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);

        //IntPtr CLR_NONE_NEW = new IntPtr(CLR_NONE);

        //private bool Interesting_Control_Found = false;  
     
        private string class_type;

        private string HostWindow;

        public int Number_of_Children;
        public int Number_of_Control_Children;
        public int Number_of_Buttons;

        private int txtValue;
        private int ClassValue;

        private int GlobalTopControlPosition;
        private int GlobalBottomControlPosition;
        private int GlobalLeftControlPosition;
        private int GlobalRightControlPosition;

        //private bool ButtonFound = false;

        //private string[] name;

        //private string classname;
        private string editText = "";
        private string classInfo = "";
        //private string WindowName = "";

        private StringBuilder formDetails = new StringBuilder(256);
        private StringBuilder classDetails = new StringBuilder(256);
        private StringBuilder windowInfo = new StringBuilder(256);

        //public ArrayList windowChildList = new ArrayList();

        public struct windowChildStructure
        {
            public String Child_Caption;
            public String Class_Name;
            public String Type;
            public String AttachedToWindow;
            public int Handle_ID;
            public int Rect_Position_Top;
            public int Rect_Position_Bottom;
            public int Rect_Position_Left;
            public int Rect_Position_Right;
        };

        //windowChildStructure windowChildInfo = new windowChildStructure();

        List<windowChildStructure> Children_button_info_structure = new List<windowChildStructure>();

        // UPDATE NEEDED!!!
        // Change the following into List of windowChildStructures
        public windowChildStructure[] Children_Button_Info = new windowChildStructure[300];
        public windowChildStructure[] Children_Control_Info = new windowChildStructure[300];
        public windowChildStructure[] Just_Return_Simple_Button_Info = new windowChildStructure[300];

        // Want to Return an Public Object (or Structure)
  
        // Structure contains the following
        // Object(N)
        //   --> String Child_Caption
        //   --> String Class_Name
        //   --> Int Handle_ID
        //   --> Int Rect_Position_Top
        //   --> Int Rect_Position_Bottom
        //   --> Int Rect_Position_Left
        //   --> Int Rect_Position_Right
        //
        // Where N = 1 to Unknown

        public void Start_Looks_For_Buttons(int hWnd)
        {
            Callback myCallBack = new Callback(EnumButtons);
            EnumChildWindows(hWnd, myCallBack, 0);
        }

        public void Start_Looking(string ParentWindow, int hWnd)
        {
            Callback myCallBack = new Callback(EnumChildButtonGetValue);
            HostWindow = ParentWindow;
            EnumChildWindows(hWnd, myCallBack, 0);            
        }

        public void Start_Looking_For_Controls(int hWnd, int Top, int Bottom, int Left, int Right)
        {
            UsingControlFindToSendInformationBackToMain = false;

            GlobalTopControlPosition = Top;
            GlobalBottomControlPosition = Bottom;
            GlobalLeftControlPosition = Left;
            GlobalRightControlPosition = Right;

            Number_of_Control_Children = 0;

            Callback myCallBack = new Callback(EnumChildControlGetValue);
            EnumChildWindows(hWnd, myCallBack, 0);
        }

        public void Start_Looking_For_Controls(int hWnd)
        {
            childrencontrolclass.Clear();
            
            Number_of_Control_Children = 0;

            UsingControlFindToSendInformationBackToMain = true;

            Callback myCallBack = new Callback(EnumChildControlGetValue);
            EnumChildWindows(hWnd, myCallBack, 0);

            try
            {
                SendParentWindowChildrenControlInfoToMain_Event(childrencontrolclass);
            }
            catch(Exception ex)
            { 
               // Error Handling...
            }
        }

        private bool ReturnIfMatch(string classinfo)
        {
            if (classinfo == "Edit")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ReturnIfMatchOnClassNameButton(string classinfo)
        {
            Regex ButtonMatch = new Regex(".BUTTON.");

            if (classInfo == "Button" || ButtonMatch.IsMatch(classInfo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int EnumButtons(int hWnd, int lParam)
        { 
            IntPtr convert = new IntPtr(hWnd);
           
            txtValue = Win32.GetWindowText(convert, formDetails, 256);
            ClassValue = Win32.GetClassName(convert, classDetails, 256);
            
            editText = formDetails.ToString().Trim();
            classInfo = classDetails.ToString();

            // IF REGULAR EXPRESSION CONTAINS .BUTTON.

            //Regex ButtonMatch = new Regex(".BUTTON.");
            //Regex AnotherButtonMatch = new Regex("Button");
            
            //if (ButtonMatch.IsMatch(classInfo) || ReturnIfMatchOnClassNameButton(classInfo))
            if (ReturnIfMatchOnClassNameButton(classInfo))
            {
                Just_Return_Simple_Button_Info[Number_of_Buttons].Handle_ID = hWnd;
                Just_Return_Simple_Button_Info[Number_of_Buttons].Child_Caption = editText;
                Number_of_Buttons++;
            }
            return 1;
        }

        private int EnumChildControlGetValue(int hWnd, int lParam)
        {
            IntPtr convert = new IntPtr(hWnd);

            txtValue = Win32.GetWindowText(convert, formDetails, 256);
            ClassValue = Win32.GetClassName(convert, classDetails, 256);

            editText = formDetails.ToString().Trim();
            classInfo = classDetails.ToString();

            // This can be used to idenify what type of Controls these are
            // Can be used to Show ALL Children and then give Type

            //       Regex ControlMatch = new Regex(".EDIT.");

            //if (ControlMatch.IsMatch(classInfo) || ControlMatch.IsMatch(classInfo))
            //       if (ControlMatch.IsMatch(classInfo) || ReturnIfMatch(classInfo))
            //       {
            Win32.Rect rc = new Win32.Rect();
            Win32.GetWindowRect(convert, ref rc);


            //   int RelativeBottom = rc.bottom - GlobalBottomControlPosition;
            //   int RelativeTop = rc.top - GlobalTopControlPosition;
            //   int RelativeLeft = rc.left - GlobalLeftControlPosition;
            //   int RelativeRight = rc.right - GlobalRightControlPosition;

            int RelativeBottom = rc.bottom;
            int RelativeTop = rc.top;
            int RelativeLeft = rc.left;
            int RelativeRight = rc.right;

            Children_Control_Info[Number_of_Control_Children].Handle_ID = hWnd;
            Children_Control_Info[Number_of_Control_Children].Rect_Position_Bottom = RelativeBottom;
            Children_Control_Info[Number_of_Control_Children].Rect_Position_Top = RelativeTop;
            Children_Control_Info[Number_of_Control_Children].Rect_Position_Left = RelativeLeft;
            Children_Control_Info[Number_of_Control_Children].Rect_Position_Right = RelativeRight;

            if (UsingControlFindToSendInformationBackToMain)
            {
                //UsingControlFindToSendInformationBackToMain = false;
                childrencontrolclass.Add(new ChildrenControlClass(Number_of_Control_Children.ToString(), editText, classInfo, hWnd.ToString()));
            }
            
            //LogWrite("# of Controls Found: " + TotalNumberOfControlsFound.ToString());
            //LogWrite("Value: " + editText);
            //LogWrite("ClassInFo: " + classInfo);
            //LogWrite("Children HandleID: " + hWnd);
            //LogWrite("Position of Control");
            //LogWrite("Top: " + RelativeTop.ToString());
            //LogWrite("Bottom: " + RelativeBottom.ToString());
            //LogWrite("Left: " + RelativeLeft.ToString());
            //LogWrite("Right: " + RelativeRight.ToString());
            //LogWrite("");

            //if (GlobalTopControlPosition == rc.top.ToString() && GlobalBottomControlPosition == rc.bottom.ToString() && GlobalLeftControlPosition == rc.left.ToString() && GlobalRightControlPosition == rc.right.ToString())
            //    return 0;

            Number_of_Control_Children++;
            //      }

            return 1;
        }

        private int EnumChildButtonGetValue(int hWnd, int lParam)
        {
            IntPtr convert = new IntPtr(hWnd);
           
            txtValue = Win32.GetWindowText(convert, formDetails, 256);
            ClassValue = Win32.GetClassName(convert, classDetails, 256);
            
            editText = formDetails.ToString().Trim();
            classInfo = classDetails.ToString();

            // IF REGULAR EXPRESSION CONTAINS .BUTTON.

            Regex ButtonMatch = new Regex(".BUTTON.");
            Regex AnotherButtonMatch = new Regex("Button");

            if (ButtonMatch.IsMatch(classInfo) || AnotherButtonMatch.IsMatch(classInfo))
            {
                //Class_Tpye may not be necessary. Check this out....May take out completely
                class_type = "Button";

             //Taking this out. Since this affects other classes, leaving it in for now
                classInfo = "Button";
                
                Win32.Rect rc = new Win32.Rect();
                Win32.GetWindowRect(convert, ref rc);

                Children_Button_Info[Number_of_Children].AttachedToWindow = HostWindow;
                Children_Button_Info[Number_of_Children].Child_Caption = editText;
                Children_Button_Info[Number_of_Children].Class_Name = classInfo;
                Children_Button_Info[Number_of_Children].Handle_ID = hWnd;
                Children_Button_Info[Number_of_Children].Rect_Position_Bottom = rc.bottom;
                Children_Button_Info[Number_of_Children].Rect_Position_Top = rc.top;
                Children_Button_Info[Number_of_Children].Rect_Position_Left = rc.left;
                Children_Button_Info[Number_of_Children].Rect_Position_Right = rc.right;
                Children_Button_Info[Number_of_Children].Type = class_type;

                Number_of_Children++;
            }
                      
            return 1;
        }

    }
}
