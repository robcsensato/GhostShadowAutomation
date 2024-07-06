using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace Automation
{
    public class PlayBackEnterControl
    {
        public event GetReplayFromPlayActionsDelegate GetReplayPlayAction;
        public event ReplayLogWriteToPlayActionFromPlayControl LogWrite;
        
        //public event GetXMLSource GetXMLSourceEvent; //--> No longer Used
        //public event GetObjectiveName GetObjectiveNameEvent; //--> No longer Used
        
        public event GetObjectiveBoolValue GetObjectiveBoolEvent;
        public event RetrieveXMLSourceData RetrieveXMLSourceDataEvent;
        public event RetrieveGlobalIndexer GetCurrentIndexNumberEvent;

        EnumChildren LocateChildren = new EnumChildren();
        ReadDefinitionFile ReadDefinition = new ReadDefinitionFile();

        XmlDocument doc = new XmlDocument();
        XmlNodeList nodeList;
        XmlNode node;

        public event ReplayChildrenPropertiesInfoBackToMain RelayChildcontrolInfo; //Take this out??

        public void SetUpEvents()
        {
            ReadDefinition.GetGlobalDefinitionReaderEvent += new GetGlobalDefinitionReaderDelegate(ReadDefinition_GetGlobalDefinitionReaderEvent);
            ReadDefinition.LogWrite += new ReplayLogWriteForReadDefinitionFileDelegate(ReadDefinition_LogWrite);
            LocateChildren.LogWrite += new ReplayLogWriteFromEnumChildrenToPlayControlDelegate(LogWriterFromEnumChild);   
        }

        public void EnterIntoControlUsingXMLSource(string webtablexml, string controlname, string row, string column, int current_window_handle_id, string activewindow, int ParentWindowTop, int ParentWindowBottom, int ParentWindowRight, int ParentWindowLeft)
        {
            LogWrite("HANDLE ID: " + current_window_handle_id.ToString());
            LogWrite("");

            LogWrite("Opening XML File: " + webtablexml);
            doc.Load(webtablexml);

            LogWrite("Retrieving Info from XML File. Row: " + row + "Column: " + column);

            string newexpression = ("//MyWebTableValues//TD[@column ='" + column + "']//TR[@row ='" + row + "']");
            nodeList = doc.SelectNodes(newexpression);
            LogWrite("NodeList Count: " + nodeList.Count.ToString());

            node = nodeList.Item(0);
            string VALUE = node.InnerText;

            LogWrite("Value Found: " + VALUE);

            LogWrite("Control Name: " + controlname);
            LogWrite("");

            if (ReadDefinition.CheckForControls(activewindow, controlname))
            {
                int DeltaParentTop;
                int DeltaParentBottom;
                int DeltaParentLeft;
                int DeltaParentRight;

                int ControlRelativePositionTop;
                int ControlRelativePositionBottom;
                int ControlRelativePositionLeft;
                int ControlRelativePositionRight;

                string top = ReadDefinition.top; //Control Top
                string bottom = ReadDefinition.bottom; //Control Bottom
                string left = ReadDefinition.left; //Control Left
                string right = ReadDefinition.right; //Control Right

                string ParentTop = ReadDefinition.ParentTop;
                string ParentBottom = ReadDefinition.ParentBottom;
                string ParentLeft = ReadDefinition.ParentLeft;
                string ParentRight = ReadDefinition.ParentRight;

                int RealTop = Convert.ToInt16(top); //Control Top
                int RealBottom = Convert.ToInt16(bottom); //Control Bottom
                int RealLeft = Convert.ToInt16(left); //Control Left
                int RealRight = Convert.ToInt16(right); //Control Right

                int ConvertedParentTop = Convert.ToInt16(ParentTop); //Parent Info from XML
                int ConvertedParentBottom = Convert.ToInt16(ParentBottom); //Parent Info from XML
                int ConvertedParentLeft = Convert.ToInt16(ParentLeft); //Parent Info from XML
                int ConvertedParentRight = Convert.ToInt16(ParentRight); //Parent Info from XML

                // This is the Current Location of the Parent Window

                //ParentWindowTop 
                //ParentWindowBottom,
                //ParentWindowRight,
                //ParentWindowLeft

                DeltaParentTop = ParentWindowTop - ConvertedParentTop;
                DeltaParentBottom = ParentWindowBottom - ConvertedParentBottom;
                DeltaParentRight = ParentWindowRight - ConvertedParentRight;
                DeltaParentLeft = ParentWindowLeft - ConvertedParentLeft;

                ControlRelativePositionTop = RealTop + DeltaParentTop;
                ControlRelativePositionBottom = RealBottom + DeltaParentBottom;
                ControlRelativePositionLeft = RealLeft + DeltaParentLeft;
                ControlRelativePositionRight = RealRight + DeltaParentRight;

                LocateChildren.Number_of_Control_Children = 0;
                LocateChildren.Start_Looking_For_Controls(current_window_handle_id, ParentWindowTop, ParentWindowBottom, ParentWindowLeft, ParentWindowRight);

                if (LocateChildren.Number_of_Control_Children > 0)
                {
                    // TO DO!!!!
                    //Check for controls relative to Parent Window in case window has moved.
                    //Get active window positions: windowtop, windowbotton, windowleft, windowright

                    //int MatchedHandleID = RunMatchAgainstControlInfo(RealTop.ToString(), RealBottom.ToString(), RealLeft.ToString(), RealRight.ToString());

                    int MatchedHandleID = RunMatchAgainstControlInfo(ControlRelativePositionTop.ToString(), ControlRelativePositionBottom.ToString(), ControlRelativePositionLeft.ToString(), ControlRelativePositionRight.ToString());

                    // If there's a MatchID on record.

                    if (MatchedHandleID > 0)
                    {
                        LogWrite("HandleID of Matched Control: " + MatchedHandleID.ToString());

                        SendMessageText(MatchedHandleID, VALUE);
                        //LogWrite("!!SENT Message: '" + TEXT_VALUE + "' to control: " + controlname + " ...MESSAGE SENT!!");
                        LogWrite("");
                        LogWrite("Message SENT: " + VALUE);
                        LogWrite("Parent Window: " + activewindow);
                        LogWrite("Control Name: " + controlname);
                        LogWrite("");
                    }
                }
                else
                {
                    LogWrite("No Children Found..");
                }
            }
            else
            {
                LogWrite("Control Not Found in Definition File");
                LogWrite("");
            }
        }

        void ReadDefinition_LogWrite(string message)
        {
            LogWrite(message);    
        }

        void LogWriterFromEnumChild(string message)
        {
            LogWrite(message);
        }

        XmlTextReader ReadDefinition_GetGlobalDefinitionReaderEvent()
        {
            return GetReplayPlayAction();
        }

        public int GetHandleIDOfControl(XmlNode n, string activewindow, int DeltaTop, int DeltaBottom, int DeltaLeft, int DeltaRight)
        {
            // Will be used later.. Usefull when adjusting for current control location..
            //::::: DeltaTop, DeltaBottom, DeltaRight, DeltaLeft 
            
            string controlname = n.ChildNodes.Item(0).InnerText.ToString();
            LogWrite("Control Name: " + controlname);
            LogWrite("");

            if (ReadDefinition.CheckForControls(activewindow, controlname))
            {
                string top = ReadDefinition.top;
                string bottom = ReadDefinition.bottom;
                string left = ReadDefinition.left;
                string right = ReadDefinition.right;

                // TO DO::Use Delta's 
                return RunMatchAgainstControlInfo(top, bottom, left, right);
            }

            return 999999;
        }

        public void EnterIntoControl(string TO_SEND,XmlNode n, int current_window_handle_id, string activewindow, int ParentWindowTop, int ParentWindowBottom, int ParentWindowRight, int ParentWindowLeft)
        {
            string controlname = "";
            int CurrentIndexNumber = 0;

            if (TO_SEND == "TEXT") 
            {
            controlname = n.Attributes.Item(1).Value;
            //string TEXT_VALUE = n.ChildNodes.Item(0).InnerText.ToString();
            LogWrite("Control Name: " + controlname);
            LogWrite("");
            }
            
             if (TO_SEND == "CLICK")
             {
             controlname = n.ChildNodes.Item(0).InnerText.ToString();
             LogWrite("Control Name: " + controlname);
             LogWrite("");
             }

            if (ReadDefinition.CheckForControls(activewindow, controlname))
            {
                string TEXT_VALUE = n.ChildNodes.Item(0).InnerText.ToString(); // coming out soon

                int DeltaParentTop;
                int DeltaParentBottom;
                int DeltaParentLeft;
                int DeltaParentRight;

                int ControlRelativePositionTop;
                int ControlRelativePositionBottom;
                int ControlRelativePositionLeft;
                int ControlRelativePositionRight;

                string top = ReadDefinition.top; //Control Top
                string bottom = ReadDefinition.bottom; //Control Bottom
                string left = ReadDefinition.left; //Control Left
                string right = ReadDefinition.right; //Control Right

                string ParentTop = ReadDefinition.ParentTop; 
                string ParentBottom = ReadDefinition.ParentBottom;
                string ParentLeft = ReadDefinition.ParentLeft;
                string ParentRight = ReadDefinition.ParentRight;

                int RealTop = Convert.ToInt16(top); //Control Top
                int RealBottom = Convert.ToInt16(bottom); //Control Bottom
                int RealLeft = Convert.ToInt16(left); //Control Left
                int RealRight = Convert.ToInt16(right); //Control Right

                int ConvertedParentTop = Convert.ToInt16(ParentTop); //Parent Info from XML
                int ConvertedParentBottom = Convert.ToInt16(ParentBottom); //Parent Info from XML
                int ConvertedParentLeft = Convert.ToInt16(ParentLeft); //Parent Info from XML
                int ConvertedParentRight = Convert.ToInt16(ParentRight); //Parent Info from XML

                // This is the Current Location of the Parent Window

                //ParentWindowTop 
                //ParentWindowBottom,
                //ParentWindowRight,
                //ParentWindowLeft

                DeltaParentTop = ParentWindowTop - ConvertedParentTop;
                DeltaParentBottom = ParentWindowBottom - ConvertedParentBottom;
                DeltaParentRight = ParentWindowRight - ConvertedParentRight;
                DeltaParentLeft = ParentWindowLeft - ConvertedParentLeft;

                ControlRelativePositionTop = RealTop + DeltaParentTop;
                ControlRelativePositionBottom = RealBottom + DeltaParentBottom;
                ControlRelativePositionLeft = RealLeft + DeltaParentLeft;
                ControlRelativePositionRight = RealRight + DeltaParentRight;
                
     // GET PARENT WINDOW POSITION FROM XML.....

                // IF USING OBJECTIVE
                // CHECK TO SEE IF TEXT_VALUE CONTAINS $
                // IF SO GET DATA FROM SOURCE
                // GET INDEX
                // CONVERT TEXT_VALUE TO STRIP $
                // TEXT_VALUE = RETRIEVE_DATA_FROM_XMLSOURCE_EVENT(index, CONVERTED_TEXT);

                 if (TO_SEND == "TEXT")
                 { 
                  bool UsingObjectiveAndRemoteSource = CheckIfUsingRemoteSourceData(TEXT_VALUE);

                  if (UsingObjectiveAndRemoteSource)
                  {
                    int index = GetCurrentIndexNumberEvent(); //--> index = GetCurrentIndexNumberEvent();

                    // Remove $ from TextData. Ex: $name becomes name

                    TEXT_VALUE = TEXT_VALUE.Remove(0, 1);

                    LogWrite("Converted Value: " + TEXT_VALUE);

                    TEXT_VALUE = RetrieveXMLSourceDataEvent(index, TEXT_VALUE);
                  }

                  LogWrite("Object Definition File contains: " + controlname);
                  LogWrite("Message to Send: " + TEXT_VALUE);
                  LogWrite("");

                  bool UsingAObjectiveBoolValue = GetObjectiveBoolEvent();
                  CurrentIndexNumber = GetCurrentIndexNumberEvent();                
                }

                if (CurrentIndexNumber == 0)
                {
                    LogWrite("Starting Search of all controls on Parent Window: " + activewindow);
                    LogWrite("");
                    // Compare these values against EnumChildren
                    // Check to see if a brand new window is open 

                    LocateChildren.Number_of_Control_Children = 0;
                    LocateChildren.Start_Looking_For_Controls(current_window_handle_id, ParentWindowTop, ParentWindowBottom, ParentWindowLeft, ParentWindowRight);
                }

                if (LocateChildren.Number_of_Control_Children > 0)
                {
                    // TO DO!!!!
                    //Check for controls relative to Parent Window in case window has moved.
                    //Get active window positions: windowtop, windowbotton, windowleft, windowright
                 
                 //int MatchedHandleID = RunMatchAgainstControlInfo(RealTop.ToString(), RealBottom.ToString(), RealLeft.ToString(), RealRight.ToString());

                 int MatchedHandleID = RunMatchAgainstControlInfo(ControlRelativePositionTop.ToString(), ControlRelativePositionBottom.ToString(), ControlRelativePositionLeft.ToString(), ControlRelativePositionRight.ToString());

                 // If there's a MatchID on record.

                 if (MatchedHandleID > 0)
                 {
                     LogWrite("HandleID of Matched Control: " + MatchedHandleID.ToString());

                      if (TO_SEND == "TEXT")
                      {
                        SendMessageText(MatchedHandleID, TEXT_VALUE);
                        //LogWrite("!!SENT Message: '" + TEXT_VALUE + "' to control: " + controlname + " ...MESSAGE SENT!!");
                        LogWrite("");
                        LogWrite("Message SENT: " + TEXT_VALUE);
                        LogWrite("Parent Window: " + activewindow);
                        LogWrite("Control Name: " + controlname);
                        LogWrite("");
                      }

                      if (TO_SEND == "CLICK")
                      {
                        SENDCLICK(MatchedHandleID);
                        LogWrite("CLICK SENT");
                        LogWrite("Parent Window: " + activewindow);
                        LogWrite("Control Name: " + controlname);
                        LogWrite("");
                      }
                 }
                 else
                 {
                    LogWrite("Could NOT Controls located within the Parent Window ...");
                    LogWrite("");
                 }
                   
               }
            }
            else
            {
                  LogWrite("Control Not Defined in Definition File: " + controlname);
            }
        }

        public void SENDCLICK(int MatchedHandleID)
        {
            IntPtr convert = new IntPtr(MatchedHandleID);

            uint WindowMessage = (uint)Win32.WindowMessages.WM_JUST_A_BUTTON_CLICK;
            //uint WindowMessage = (uint)Win32.WindowMessages.WM_ACTIVATE;
            Win32.SendMessage(convert, WindowMessage, 0, 0);
            WindowHighlighter.Flash(convert);
        }

        private bool CheckIfUsingRemoteSourceData(string UserTextData)
        {
            bool UsingObjective = GetObjectiveBoolEvent();

            if (UsingObjective)
            {
                if (CheckifUserTextDataRequiresSourceData(UserTextData))
                {
                    //string XMLSource = GetXMLSourceEvent();
                    //string ObjectiveName = GetObjectiveNameEvent();
                    // Report (LogWrite) Using 'XMLSource' and Objective 'ObjectName'
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckifUserTextDataRequiresSourceData(string TextData)
        {
            // IF REGULAR EXPRESSION. Check if TextData Contains $

            string value = "\\$";

            Regex ContainsTriggerToUseSourceData = new Regex(value);

            if (ContainsTriggerToUseSourceData.IsMatch(TextData))
            {
                LogWrite("DATA CONTAINS $. Reading Data From Source");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SendMessageText(int handleid, string text)
        {
            IntPtr convert = new IntPtr(handleid);
            uint WindowMessage = (uint)Win32.WindowMessages.WM_SETTEXT;
            Win32.SendMessage(convert, WindowMessage, 0, text);
            WindowHighlighter.Flash(convert);
        }

        private int RunMatchAgainstControlInfo(string position_top, string position_bottom, string position_left, string position_right)
        {
            string Right;
            string Left;
            string Top;
            string Bottom;

            int MatchedHandleID = 0;
            int TotalNumberofChildren = LocateChildren.Number_of_Control_Children;

            for (int I = 0; I < TotalNumberofChildren; I++)
            {
                Right = LocateChildren.Children_Control_Info[I].Rect_Position_Right.ToString();
                Left = LocateChildren.Children_Control_Info[I].Rect_Position_Left.ToString();
                Top = LocateChildren.Children_Control_Info[I].Rect_Position_Top.ToString();
                Bottom = LocateChildren.Children_Control_Info[I].Rect_Position_Bottom.ToString();

                LogWrite("Right: " + Right);
                LogWrite("Left : " + Left);

                LogWrite("Position Left:  " + position_left);
                LogWrite("Position Right: " + position_right);

                LogWrite("---------------------------------");
                LogWrite("");

                if (Right == position_right && Left == position_left)
                {
                   // if (Top == position_top && Bottom == position_bottom)
                   // {
                        LogWrite("Found Match!");
                        MatchedHandleID = LocateChildren.Children_Control_Info[I].Handle_ID;
                        return MatchedHandleID;
                   // }
                }
            }
            return MatchedHandleID;
        }
    }
}
