using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Automation
{
    public class CaptureKeyBoardEvents
    {
        public event UpdateDataString UpdateData;
        public event RemoveDataString RemoveData;

        public event ToggleReadingXMLValueDelegate ToggleReadXMLValueEvent;

        private System.Drawing.Imaging.ImageFormat format;
        //private System.Drawing.Imaging.ImageFormat format_icon;

        public event DisplayImageDelegate DisplayImageEvent;
        public event DisplayUserKeyBoardEventMessage SendKeyBoardMessage;
        public event WriteGeneralKeyBoardToXMLEventDelegate WriteGeneralKeyBoardEvent;
        public event WriteSpecialKeyBoardToXMLEventDelegate WriteSpecialKeyBoardEvent;
        public event WriteComboPressKeyBoardToXMLEventDelegate WriteComboPressedEvent;
       // public event CaptureIEWebPageDelegate CaputureIEAsPictureEvent;

        CaptureWindowInfo NewWindowInfo = new CaptureWindowInfo();
     
        public CaptureKeyBoardEvents()
        {
          String_Started = false;
          LoadSpecialKeyList();         
        }

        public void ResetUserData()
        {
            data.Remove(0, data.Length);
        }

        private enum CaptureActions
        {
            None,
            Desktop,
            Window,
            SmallIcon,
            LargeIcon,
            Clear
        }

        private Hashtable Special_Key_List = new Hashtable();

        private const int nChars = 256;
        private bool Member_of_Special_Key_List;

        private StringBuilder data = new StringBuilder(nChars);
        private StringBuilder combolist = new StringBuilder(nChars);

        private bool String_Started;

        private string ActionKeyPressedDown;

        private bool SpecialKeyPressedDown;

        private int global_Menu_Down_Count;
        private int global_Control_Down_Count;

        private string Previous_KeyData;

        private bool ControlPressed = false;

        public void LoadSpecialKeyList()
        {
            Special_Key_List.Add("Return", null);
            Special_Key_List.Add("RMenu", null);
            Special_Key_List.Add("LMenu", null);
            Special_Key_List.Add("LControlKey", null);
            Special_Key_List.Add("RControlKey", null);
            Special_Key_List.Add("LShiftKey", null);
            Special_Key_List.Add("RShiftKey", null);
            Special_Key_List.Add("Tab", null);
        }

        public void HotKeyPressedDown(object sender, KeyEventArgs e)
        {
            string KeyData = e.KeyData.ToString();
            MappedPressedCommands(KeyData,0,IntPtr.Zero); // --> Check for Mapped KEYS

            // CHANGED TO THE FOLLOWING:
            // MappedPressedCommands(KeyData,0); --> This means just save to Viewer
        }

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            string KeyData = e.KeyData.ToString();

            //LogWrite("KeyDown 	- " + KeyData);

            if (KeyData == "Delete" || KeyData == "Back")
            {
                int current_list_length = data.Length;
                int current_index = current_list_length - 1;
                ActionKeyPressedDown = "DELETER";

                if (data.Length > 0)
                {
                    //textBox2.Text = "Delete from String: Length of String __ " + current_list_length.ToString() + "__Current index__" + current_index.ToString();
                    data.Remove(current_index, 1);
                    RemoveData(current_index, 1);
                }

                Member_of_Special_Key_List = false;
            }

            foreach (string Character in Special_Key_List.Keys)
            {
                if (KeyData == Character)
                {
                    Member_of_Special_Key_List = true;
                    break;
                }
            }

            if (Member_of_Special_Key_List)
            {
                switch (KeyData)
                {
                    case "Return":
                        ActionKeyPressedDown = "Return";
                        Member_of_Special_Key_List = false;
                        break;

                    case "Tab":
                        //WriteTabXML(e.keydata)--> Check to see if Sting was built --> WriteNameKeyBoard then WriteTabtoXML
                        ActionKeyPressedDown = "Tab";
                        ControlPressed = false;
                        Member_of_Special_Key_List = false;
                        String_Started = false;
                        break;

                    case "LMenu":
                        //BuildKeyWithALT()--> Build LeftALT+key1+key2+.. Then write this to XML from KeyUpEvent                        
                        ActionKeyPressedDown = "COMBOKEY";
                        
                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("LMenu");
                            else
                                combolist.Append("+LMenu");
                        }

                        Member_of_Special_Key_List = false;
                        global_Menu_Down_Count = global_Menu_Down_Count + 1;

                        break;

                    case "RMenu":
                        //BuildKeyWithALT()--> Build RightALT+key1+key2+.. Then write this to XML from KeyUpEvent
                        ActionKeyPressedDown = "COMBOKEY";

                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("RMenu");
                            else
                                combolist.Append("+RMenu");
                        }

                        Member_of_Special_Key_List = false;
                        global_Menu_Down_Count = global_Menu_Down_Count + 1;
                        break;

                    case "LControlKey":
                        //BuildKeyWithControlKey()--> Build LeftControl+key1+key2.. Then write to XML from KeyUpEvent
                        ActionKeyPressedDown = "COMBOKEY";
                        ControlPressed = true;
                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("LControl");
                            else
                                combolist.Append("+LControl");
                        }

                        Member_of_Special_Key_List = false;
                        global_Control_Down_Count = global_Control_Down_Count + 1;
                        break;

                    case "RControlKey":
                        //BuildKeyWithControlKey()--> Build RightControl+key1+key2.. Then write to XML from KeyUpEvent
                        ActionKeyPressedDown = "COMBOKEY";
                        ControlPressed = true;
                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("RControl");
                            else
                                combolist.Append("+RControl");
                        }

                        Member_of_Special_Key_List = false;
                        global_Control_Down_Count = global_Control_Down_Count + 1;
                        break;

                    case "LShiftKey":
                        //BuildKeyWithControlKey()--> Build RightControl+key1+key2.. Then write to XML from KeyUpEvent
                        ActionKeyPressedDown = "COMBOKEY";
                        ControlPressed = false;
                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("LShift");
                            else
                                combolist.Append("+LShift");
                        }

                        Member_of_Special_Key_List = false;
                        global_Control_Down_Count = global_Control_Down_Count + 1;
                        break;
                        
                    case "RShiftKey":
                        //BuildKeyWithControlKey()--> Build RightControl+key1+key2.. Then write to XML from KeyUpEvent
                        ActionKeyPressedDown = "COMBOKEY";
                        ControlPressed = false;
                        SpecialKeyPressedDown = true;

                        if (global_Menu_Down_Count == 0)
                        {
                            if (combolist.Length == 0)
                                combolist.Append("RShift");
                            else
                                combolist.Append("+RShift");
                        }

                        Member_of_Special_Key_List = false;
                        global_Control_Down_Count = global_Control_Down_Count + 1;
                        break;
                }
            }
            else // NOT A MEMBER OF THE SPECIAL KEY LIST --> NOT A RETURN, TAB OR CONTROL KEY...
            {
                String_Started = true;

                //Took out if (KeyData != Previous_KeyData) condition
                if (SpecialKeyPressedDown) //&& KeyData != Previous_KeyData)
                {
                    combolist.Append("+" + KeyData);
                    Previous_KeyData = KeyData;
                }
            }
        }

        public void MappedPressedCommands(string KeyData, int parameter, IntPtr HandleID)
        {
            // UPDATE TO: MappedPressedCommands(string KeyData, int parameter)

            ////MAP OF SPECIAL COMMANDS
            // Can be Defined by User
            // Hardwired for now

           CaptureActions action = CaptureActions.None;
           format = System.Drawing.Imaging.ImageFormat.Jpeg;
           int CurrentHandleID = 0;

           switch (KeyData)
           {
                    //==> These Could be User Defined Mapped Key Board Values
                case "F1":
                    {
                        // ControlPressed = true;
                        action = CaptureActions.Window;

                        if (HandleID.Equals(IntPtr.Zero))
                        {
                            string activewindowtitle = NewWindowInfo.ReturnActiveWindow();
                            CurrentHandleID = NewWindowInfo.GetWindowHandleID(activewindowtitle);
                        }
                        else
                        {
                            CurrentHandleID = HandleID.ToInt32();
                        }
                        
                        PerformAction(action, CurrentHandleID,parameter); // Update to PerformAction(action, CurrentHandID, parameter);

                   //   CaptureWindowAsJPGEvent(activewindowtitle, format);
                        
                   // ***** Remaining GOES INTO CaptureWindowAsJPGEvent Method ******

                   //     active_image_count = active_image_count + 1;

                   //     activewindow_filename = "active" + active_image_count.ToString() + ".jpg";


                   //     this.pictureBox1.Image.Save(activewindow_filename, format);
                   //     LogWrite("Saved Active Window: " + activewindow_filename);

                        //Write to XML either to show current window 'activewindow_filename' during Test Plan mode 
                        //or Capture new JPEG during Automatic Mode
                        break;
                    }

                case "F2":
                    {
                        // ControlPressed = true;
                          action = CaptureActions.Desktop;
                          PerformAction(action,0, parameter); // Update to PerformAction(action,0,parameter);
                   //     desktop_image_count = desktop_image_count + 1;

                   //     desktop_filename = "desktop" + desktop_image_count.ToString() + ".jpg";

                   //     this.pictureBox1.Image.Save(desktop_filename, format);
                   //     LogWrite("Saved Active Window: " + desktop_filename);
                        break;
                    }

                case "F3":
                    {
                        // ControlPressed = true;
                          action = CaptureActions.Clear;
                          PerformAction(action,0, 0);
                        break;
                    }

                case "F4":
                    {
                     //   int wait_time = 3;
                     //   XMLWrite.writeMouseWaitPosition(currentposition_x, currentposition_y, wait_time);
                        
                        break;
                    }

                case "F5":
                    {
                     //   XMLWrite.setup_XML("ROB_XML_TEST.xml");
                     //   WriteMonitorConfigurationToXML();
                        break;
                    }

                case "F6":
                    {
                       // first_start_of_ghost = true;
                        //XMLREAD("ROB_XML_TEST.xml");
                        break;
                    }

                case "F7":
                    {
                       // actHook.Stop();
                        //actHook2.Stop();
                      //  timer1.Enabled = false;
                      //  timer2.Enabled = false;
                      //  this.Opacity = 1;
                        //HockEventThread.Abort();
                        break;
                    }

                case "F8":
                    {
                        //LocateChildren.Start_Looking(global_current_window_title, current_handleID);
                        //LogWrite_Notes("Refreshing Find Children..");
                        break;
                    }

                case "F9":
                    {
                        ToggleReadXMLValueEvent();
                        break;
                    }
                case "F10":
                    {
                        break;
                    }
            }
        }

        private void PerformAction(CaptureActions action, int current_handleID,int parameter)
        {
            // Update to PerformAction(CaptureActions action, int current_handleID, parameter);
            Image image = null;
            string TypeOfFile = "";
            string Name = "";

            switch (action)
            {
                case CaptureActions.Desktop:
                    TypeOfFile = "DeskTop";
                    image = (Image)ScreenCapturing.GetDesktopWindowCaptureAsBitmap();
                    break;
                case CaptureActions.Window:
                    TypeOfFile = "ActiveWindow";
                    // Use Current HandleID. Replace this.Handle with currentHandleID
                    image = (Image)ScreenCapturing.GetWindowCaptureAsBitmap(current_handleID);
                    break;
                case CaptureActions.SmallIcon:
                    TypeOfFile = "SmallIcon";
                    image = (Image)ScreenCapturing.GetWindowSmallIconAsBitmap((int)current_handleID);
                    break;
                case CaptureActions.LargeIcon:
                    TypeOfFile = "LargeIcon";
                    image = (Image)ScreenCapturing.GetWindowLargeIconAsBitmap((int)current_handleID);
                    break;
                default:
                    break;
            };

            if (parameter == 0)
                DisplayImageEvent(image, true, "normal");
            else
            {
                // JUST SAVE TO DISK!!! DON'T SEND TO VIEWER
                DateTime myTime = DateTime.Now;
                String format = "MM.dd.hh.mm.ss";

                if (TypeOfFile == "DeskTop")
                {
                    Name = ".\\Desktop_";

                    Name = Name + myTime.ToString(format)+".bmp";
                    //Name = ".\\Desktop.bmp";
                }

                if (TypeOfFile == "ActiveWindow")
                 {
                        // Get the Name of Window
                        IntPtr WindowID = new IntPtr(current_handleID);
                        Name = NewWindowInfo.ReturnWindowText(WindowID) + "_";
                        Name = Name + myTime.ToString(format) + ".bmp";
                  }

               // image.Save("c:\\test.bmp");
               //Name = "c:\\" + Name;
               image.Save(Name);

               if (parameter == 3)
               {
                   DisplayImageEvent(image, true, "normal");
               }
            }

            
        }

        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            //This is to guard against saving certain keydown events
            //If user holds down 'RETURN' only one gets saved once until KeyUp Event is fired

            //LogWrite("KeyUp 		- " + e.KeyData.ToString());

            //Add New SwitchCase

            //case (TabPressed)
            //case (LControlPressed)
            //case (LMenuPressed)
            //case (RMenuPressed)

            // ... Add All Special KeyBoard Keys

            switch (ActionKeyPressedDown)
            {
                case "Return":
                    SendKeyBoardMessage("Pressed Enter..Writing {RETURN} to XML");
                    WriteTextToXML("RETURN"); 
                    ActionKeyPressedDown = "None";
                    break;

                case "TAB":
                    //   Coming Soon. Works the same as Return. Writes TAB instead of Return 
                   //    WriteTextToXML("{TAB}");
                   //    ActionKeyPressedDown = "None";
                    break;
                
                // Write More Special Key List HERE
                // ....

                // Specail Case Where Combo Keys are used
                case "COMBOKEY":

                    Regex StringMatch = new Regex("Shift");

                    if (!StringMatch.IsMatch(combolist.ToString()))
                    {
                    SendKeyBoardMessage("Created ComboString: " + combolist.ToString());
                    WriteSpecialCommandToXML(combolist.ToString());
                    }

                    SpecialKeyPressedDown = false;
                    ActionKeyPressedDown = "None";
                    global_Menu_Down_Count = 0;
                    combolist.Remove(0, combolist.Length);
                    break;
            }
        }

        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            Char KeyChar = e.KeyChar;
            int HashCode = e.KeyChar.GetHashCode();

            string PressedCharString = KeyChar.ToString();
            string HashCodeString = HashCode.ToString();

            // The following is needed since keys like Return and Delete returns a MyKeyPress character value.  
            // This character should get placed within the string 'action'
            // The Hash Code for Return (Enter) is 851981.
            // I don't want to record Enter into the String
            // If I come acrosss any other key's I don't want placed into the string
            // -- such as Back Space, Control, TAB or Delete --  this is the area to come too.

            // Added Check for Delete Key Hash Code 524295.
            
            // TO DO: Add CONTROL to This. Find Hash Code for 'Control' Keys..
            // Building... DATA String

            //if (ActionKeyPressedDown != "Return" && ActionPressDown !="DELETER" && !ControlPressed)
            if (HashCode != 851981 && HashCode != 524296)
            {
                //-> Get Current Value From Main Form
                //data = GetData();
                data.Append(KeyChar);

                ///--> SendEvent to Update DataTextString -->UpdateDataString(data.ToString());
                UpdateData(KeyChar.ToString());

               //if (data.ToString() != null)
               // {
               //   SendKeyBoardMessage(data.ToString());
               // }
               //   textBox2.Text = list.ToString();
            }
        }

        public void WriteTextToXML(string KeyData)
        {
            if (String_Started)
            {
                Member_of_Special_Key_List = false;

                string StringEnteredFromUser = data.ToString();

                if (!ControlPressed)
                {
                    SendKeyBoardMessage("String Recorded: " + StringEnteredFromUser);
                    //-->XMLWrite.writeNormalKeyboardEvent_To_XML(StringEnteredFromUser);
                    WriteGeneralKeyBoardEvent(StringEnteredFromUser);
                }
                //Re-set Flag for Building Strings
                String_Started = false;

                //Clean up string. Re-set length to zero
                data.Remove(0, data.Length);
                
                //Send Event to Update DataString --> UpdateUserDataString("");
                //--> Orignal UpdateData(data.ToString());
                string cleardata = "";
                UpdateData(cleardata);
            }

            //Write XML as KeyPressed
            //-->XMLWrite.writeSpecialKeyboardEvent_To_XML(KeyData);
            WriteSpecialKeyBoardEvent(KeyData);
        }

        public void WriteSpecialCommandToXML(string KeyData)
        {
            WriteComboPressedEvent(KeyData);
        }        
    }
}
