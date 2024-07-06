using System;
//using System.Collections.Generic;
//using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace Automation
{
    public class PlayBackActions
    {
        static TargetWindowInfoData RecordedWindowData = new TargetWindowInfoData();

        public event ReplayWindowTitleToPlayBackMainDelegate ReturnWindowTitleToPlayBackMainEvent;

        public event RelayPlayBackEventMessage LogWritePlayBack;

        public event ReplayGlobalTotalNumberMonitors ReplayMonitorsTotal;
        public event ReplayRecorderedMonitorFound ReplayRecordedMonitorFound;
        public event ReplayRecorderedMonitorData ReplayRecordedMonitorData;
        public event ReplayCurrentMonitorData ReplayCurrentMonitorData;

        public event ReplayProgressBar UpdateProgressBar;

        public event GetReplayFromPlayBackMainDelegate GetPlayBlackDefinitionFromPlayMain;

        public event GetXMLSource GetXMLSourceEvent;  //--> NO Longer Used
        public event GetObjectiveName GetObjectiveNameEvent; //--> No Longer Used

        public event GetObjectiveBoolValue GetObjectiveBoolEvent;
        public event RetrieveXMLSourceData RetrieveXMLSourceDataEvent;
        public event RetrieveGlobalIndexer GetCurrentIndexNumberEvent;

        public bool CorrectWindowActiveState = true;
        string CurrentDefinedGlobalWindow = "";
        
        string eventID;
        string Button_Used;
        string WindowTitle;

        // Raw XML Values 
        string x_coordinate;
        string y_coordinate;

        // Converted RAW XML data
        int x_position;
        int y_position;
        ///////////////////////////

        int GlobalDeltaTop = 0;
        int GlobalDeltaLeft = 0;

        PlayBackEnterControl PlayBackObjectController = new PlayBackEnterControl();
        //SendClickToControlHandler SendClickToObjectController = new SendClickToControlHandler(); // This is coming out in the Future!!

        PlayBackMouseEvents PlayMouseEvents = new PlayBackMouseEvents();
        PlayBackWindowEvents PlayWindowEvents = new PlayBackWindowEvents();
        PlayBackKeyBoardEvents PlayKeyBoardEvent = new PlayBackKeyBoardEvents();

        MousePositionHolder AdjustForWindowMove = new MousePositionHolder();
        MousePositionHolder AdjustForResolutionChange = new MousePositionHolder();

        CaptureWindowInfo NewWindowInfo = new CaptureWindowInfo();

        ResolutionAdjustment ResolutionAdjust = new ResolutionAdjustment();

        //TargetWindowMoveCheck TargetWindowCheck = new TargetWindowMoveCheck();

        EnumChildren LocateChildren = new EnumChildren();

        WindowSearchFunctionsDuringPlayBack WindowSearch = new WindowSearchFunctionsDuringPlayBack();

        IEFunctionalCommand IEFunctional = new IEFunctionalCommand();

        public void SetUpDelegateEvents()
        {
            NewWindowInfo.SendMessageToPlayBack += new SendMessageToPlayBackDelegate(NewWindowInfo_SendMessageToPlayBack);
            PlayWindowEvents.SetTargetWindowInfoEvent += new SetTargetWindowInfoDelegate(PlayWindowEvents_SetTargetWindowInfoEvent);
            
            PlayBackObjectController.SetUpEvents();
            PlayBackObjectController.GetReplayPlayAction += new GetReplayFromPlayActionsDelegate(PlayBackObjectController_GetReplayPlayAction);
            PlayBackObjectController.LogWrite += new ReplayLogWriteToPlayActionFromPlayControl(PlayBackObjectController_LogWrite);
            PlayBackObjectController.GetObjectiveBoolEvent += new GetObjectiveBoolValue(PlayBackObjectController_GetObjectiveBoolEvent);
            PlayBackObjectController.RetrieveXMLSourceDataEvent += new RetrieveXMLSourceData(PlayBackObjectController_RetrieveXMLSourceDataEvent);
            PlayBackObjectController.GetCurrentIndexNumberEvent += new RetrieveGlobalIndexer(PlayBackObjectController_GetCurrentIndexNumberEvent);

           // SendClickToObjectController.SetUpEvents();
           // SendClickToObjectController.LogWrite += new ReplayLogWriteToPlayActionFromPlayControl(SendClickToObjectController_LogWrite);
           // SendClickToObjectController.GetCurrentIndexNumberEvent += new RetrieveGlobalIndexer(SendClickToObjectController_GetCurrentIndexNumberEvent);
           // SendClickToObjectController.GetReplayPlayAction += new GetReplayFromPlayActionsDelegate(SendClickToObjectController_GetReplayPlayAction);
            
           // TargetWindowCheck.GetRecordedWindowDataEvent += new GetRecordedWindowDataDelegate(TargetWindowCheck_GetRecordedWindowDataEvent);
           // TargetWindowCheck.TargetWindowLogWriteEvent += new TargetWindowLogWriteDelegate(TargetWindowCheck_TargetWindowLogWriteEvent);
            
            ResolutionAdjust.GetCurrentMonitorDataEvent += new GetCurrentMonitorDataDelegate(ResolutionAdjust_GetCurrentMonitorDataEvent);
            ResolutionAdjust.GetRecorderedMonitorDataEvent += new GetRecorderedMonitorDataDelegate(ResolutionAdjust_GetRecorderedMonitorDataEvent);
            ResolutionAdjust.GetRecorderedMonitorFoundEvent += new GetRecorderedMonitorFoundDelegate(ResolutionAdjust_GetRecorderedMonitorFoundEvent);
            ResolutionAdjust.GetTotalNumberMonitors += new GetGlobalTotalNumberMonitors(ResolutionAdjust_GetTotalNumberMonitors);

            WindowSearch.WriteToPlayBack += new WriteToPlayBackFromWindowSearchDelegate(LogWriteBackFromWindowSearch);

           //WindowSearch.WriteToPlayBack += WriteToPlayBackFromWindowSearchDelegate(...);
        }

        XmlTextReader SendClickToObjectController_GetReplayPlayAction()
        {
            return GetPlayBlackDefinitionFromPlayMain();
        }

        int SendClickToObjectController_GetCurrentIndexNumberEvent()
        {
            return GetCurrentIndexNumberEvent(); 
        }

        void SendClickToObjectController_LogWrite(string message)
        {
            LogWritePlayBack(message);
        }

        void LogWriteBackFromWindowSearch(string data)
        {
            LogWritePlayBack(data);
        }

        int PlayBackObjectController_GetCurrentIndexNumberEvent()
        {
            return GetCurrentIndexNumberEvent(); 
        }

        string PlayBackObjectController_RetrieveXMLSourceDataEvent(int index, string data)
        {
            return RetrieveXMLSourceDataEvent(index, data);
        }

        bool PlayBackObjectController_GetObjectiveBoolEvent()
        {
            return GetObjectiveBoolEvent();
        }

        void NewWindowInfo_SendMessageToPlayBack(string message)
        {
            LogWritePlayBack(message);
        }

        void PlayBackObjectController_LogWrite(string message)
        {
            LogWritePlayBack(message);
        }

        XmlTextReader PlayBackObjectController_GetReplayPlayAction()
        {
            return GetPlayBlackDefinitionFromPlayMain(); 
        }

        void TargetWindowCheck_TargetWindowLogWriteEvent(string message)
        {
            LogWritePlayBack(message);    
        }

        int TargetWindowCheck_GetRecordedWindowDataEvent(string position)
        {
            switch (position)
            { 
                case "left":
                    return RecordedWindowData.Left;
                case "top":
                    return RecordedWindowData.Top;
                default:
                    return 9999; // Error Occured..
            }
        }

       void PlayWindowEvents_SetTargetWindowInfoEvent(string WindowTitle, int Top, int Left, bool UsingRegularExpression)
        //void PlayWindowEvents_SetTargetWindowInfoEvent(string WindowTitle, int Top, int Left)
        {
            RecordedWindowData.WindowTitle = WindowTitle;
            RecordedWindowData.Top = Top;
            RecordedWindowData.Left = Left;
            RecordedWindowData.UsingRegularExpression = UsingRegularExpression;
        }

        int ResolutionAdjust_GetTotalNumberMonitors()
        {
            return ReplayMonitorsTotal();    
        }

        bool ResolutionAdjust_GetRecorderedMonitorFoundEvent(int index)
        {
            return ReplayRecordedMonitorFound(index);
        }

        int ResolutionAdjust_GetRecorderedMonitorDataEvent(int index, string position)
        {
            return ReplayRecordedMonitorData(index, position);
        }

        int ResolutionAdjust_GetCurrentMonitorDataEvent(int index, string position)
        {
            return ReplayCurrentMonitorData(index, position);
        }

        public void ParseWebPage(XmlNode n)
        { 
            string outputxml = n.InnerText;
            IEFunctional.ParseWebPage(outputxml);
        }


        public void EnterText(XmlNode n)
        {
            LogWritePlayBack("Enter Text Tag Found");
            LogWritePlayBack("");

            string parentWindow="";
            int current_window_handle_id;
            //  EnterIntoControl(n, current_window_handle_id, activewindow);
            // PlayBackObjectController.EnterIntoControl(n, current_window_handle_id, activewindow);

            int HandleID;

            IntPtr convertedID;

            //Win32.Rect rc;
            //Win32.GetWindowRect(convertedID, ref rc);

            int Top = 0;
            int Bottom = 0;
            int Left = 0;
            int Right = 0;

            if (n.Attributes.Count > 0)
            {
                if (n.Attributes.Item(0).Name == "parent")
                {
                    parentWindow = n.Attributes.Item(0).Value;
                    LogWritePlayBack("Parent Window: " + parentWindow);
                    LogWritePlayBack("");

                    current_window_handle_id = NewWindowInfo.GetWindowHandleID(parentWindow);
                    //  EnterIntoControl(n, current_window_handle_id, activewindow);
                    // PlayBackObjectController.EnterIntoControl(n, current_window_handle_id, activewindow);

                    HandleID = NewWindowInfo.GetWindowHandleID(parentWindow);

                    convertedID = new IntPtr(HandleID);

                    Win32.Rect rc = new Win32.Rect();
                    Win32.GetWindowRect(convertedID, ref rc);

                    Top = rc.top;
                    Bottom = rc.bottom;
                    Left = rc.left;
                    Right = rc.right;
                }
                  
                // I need to change this. This is not Robust!!! Needs re-work.

                if (NewWindowInfo.CheckIfWindowExists(parentWindow))
                    switch (n.Attributes.Item(1).Name)
                    {
                        case "control": //XMLDATA <EnterText parent="MainForm" control="controlname1">Text</EnterText>
                            {
                                //  string activewindow = this.ReturnActiveWindow();
                                //  string activewindow = NewWindowInfo.ReturnActiveWindow();

                                //  int current_window_handle_id = this.getActiveWindowHandleID(activewindow);
                                //  int current_window_handle_id = NewWindowInfo.GetWindowHandleID(activewindow);
                                current_window_handle_id = NewWindowInfo.GetWindowHandleID(parentWindow);
                                //  EnterIntoControl(n, current_window_handle_id, activewindow);
                                // PlayBackObjectController.EnterIntoControl(n, current_window_handle_id, activewindow);
                                
                               // int HandleID = NewWindowInfo.GetWindowHandleID(parentWindow);

                               // IntPtr convertedID = new IntPtr(HandleID);

                               // Win32.Rect rc = new Win32.Rect();
                               // Win32.GetWindowRect(convertedID, ref rc);

                               // int Top = rc.top;
                               // int Bottom = rc.bottom;
                               // int Left = rc.left;
                               // int Right = rc.right;

                                PlayBackObjectController.EnterIntoControl("TEXT",n, current_window_handle_id, parentWindow, Top, Bottom, Right, Left);
                                
                                break;
                            }

                        case "webtablexml": //XMLDATA <EnterText parent="MainForm" webtablexml="ROB2.xml" control="controlname">1,2</EnterText>
                            {
                               // Just Refined a new SourceXML. Wait for this... Need to restructure how the XML attributes are searched.
                               // Make the new source private. Don't over write any source that is already being used by an objective.
                               // It will only be used at one time only!
                                
                                // LoadSourceXML
                                // if row==NULL then use index.....this comes from looping through the xml
                                // ProcessSpecificXMLNodePath(sourcexml,td,row);
                                current_window_handle_id = NewWindowInfo.GetWindowHandleID(parentWindow);

                                string webtablexml = n.Attributes.Item(1).Value;
                                string controlname = n.Attributes.Item(2).Value;
                                string value = n.InnerXml;
                                string column = "1"; // This comes from splitting value
                                string row = "1"; // This coms from splitting value

                                PlayBackObjectController.EnterIntoControlUsingXMLSource(webtablexml, controlname, row, column, current_window_handle_id, parentWindow, Top, Bottom, Right, Left);


                               
                                break;
                            }
                    }
              }
              else
              {                 
                  Playback_KeyBoardEvent(n); // No Attributes. Just a normal <EnterText>Hello..</EnterText>
              }
        }

        public void SendClickToControl(XmlNode n)
        { 
         //<SendClickToControl parent="test">CONTROL_NAME_FROM_OBJECT_FILE</SendClickToControl>

            LogWritePlayBack("Send Click To Control Tag Detected..");

            string parentWindow="";

            Win32.Rect rc;

            int ParentWindowTop=0;
            int ParentWindowBottom=0;
            int ParentWindowRight=0;
            int ParentWindowLeft=0;
            int WindowID = 0;

            if (n.Attributes.Count > 0)
            {
                if (n.Attributes.Item(0).Name == "parent")
                {
                    parentWindow = n.Attributes.Item(0).Value;

                    LogWritePlayBack("Parent Window: " + parentWindow);
                    LogWritePlayBack("");
                }

            }
            else
            {
            // If no attributes then get Current Active Window or Use the Window Defined within the Window Event Tag
                if (CurrentDefinedGlobalWindow != "")
                    parentWindow = CurrentDefinedGlobalWindow;
                else
                    parentWindow = NewWindowInfo.ReturnActiveWindow(); //In case no Window Definition Tag is defined.
            }

             if (NewWindowInfo.CheckIfWindowExists(parentWindow))
             {
                 WindowID = NewWindowInfo.GetWindowHandleID(parentWindow);
                 rc = NewWindowInfo.ReturnWindowPositionInfo(WindowID);

                 ParentWindowLeft = rc.left;
                 ParentWindowRight = rc.right;

                 ParentWindowTop = rc.top;
                 ParentWindowBottom = rc.bottom;

                 // TO DO: Check to See Which one works...
                 PlayBackObjectController.EnterIntoControl("CLICK", n, WindowID, parentWindow, ParentWindowTop, ParentWindowBottom, ParentWindowRight, ParentWindowLeft);
                 //SendClickToObjectController.SendClickToControlAction(parentWindow, WindowID, ControlName,ParentWindowTop, ParentWindowBottom, ParentWindowRight, ParentWindowLeft);                        
             }
             else
             {
                 LogWritePlayBack("Could not locate Parent Window: " + parentWindow);
                 LogWritePlayBack("Did not Send Click to Control");
             }
        }

        private void SendClick(int ButtonHandleID)
        {
            IntPtr convertedButtonHandleID = new IntPtr(ButtonHandleID);

            uint WindowMessage = (uint)Win32.WindowMessages.WM_JUST_A_BUTTON_CLICK;

            Win32.SendMessage(convertedButtonHandleID, WindowMessage, 0, 0);

            WindowHighlighter.Flash(convertedButtonHandleID);
        }

      //  private void MouseOverClick(int x_position, int y_position, int TargetWindowHandleID)
      //  {
      //      AdjustForResolutionChange = ResolutionAdjust.CompensateForAnyResolutionChange(x_position, y_position);

      //      int NewXPosition = AdjustForResolutionChange.xposition;
      //      int NewYPosition = AdjustForResolutionChange.yposition;

      //      AdjustForWindowMove = TargetWindowCheck.CompensateForAnyWindowMove(NewXPosition, NewYPosition, TargetWindowHandleID);

      //      int AdjustedX = AdjustForWindowMove.xposition;
      //      int AdjustedY = AdjustForWindowMove.yposition;

      //      string Button_Used = "LEFT";

      //      //PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "clicked");

      //     PlayMouseEvents.MouseActions(x_position, y_position, Button_Used, "doubleclicked");
      //  }

        public void PressButton(XmlNode n)
        {
            int current_window_handle_id = 0;
            string parentwindow = "";

            IntPtr window_handle_id;

            string ButtonName = n.ChildNodes.Item(0).InnerText.ToString();

            LogWritePlayBack("Found Press Button Event. Button Name: " + ButtonName);

            if (n.Attributes.Count > 0)
            {
                if (n.Attributes.Item(0).Name == "parentwindow")
                {
                    parentwindow = n.Attributes.Item(0).Value;
                    LogWritePlayBack("Sending to Parent Window: " + parentwindow);
                }
            }

            if (parentwindow == "")
            {
                if (WindowTitle == "") // No Active window set... 
                {
                    string active_window_name = NewWindowInfo.ReturnActiveWindow();
                    window_handle_id = NewWindowInfo.ReturnActiveWindowHandleID();
                    
                    current_window_handle_id = Convert.ToInt16(window_handle_id);

                    LogWritePlayBack("Sending to Current Active Window: " + active_window_name);
                }
                else //Send to defined window set in WindowEvent Tag
                {
                    LogWritePlayBack("Sending to Defined Window: " + WindowTitle);
                    current_window_handle_id = NewWindowInfo.GetWindowHandleID(WindowTitle);
                }
            }
            else //This will send to Parent Window defined in the current Attribute...
            {
                LogWritePlayBack("Sending to Defined window from Press Button Tag: " + parentwindow);
                current_window_handle_id = NewWindowInfo.GetWindowHandleID(parentwindow);
            }

            // TO DO!!!!!!!
            ///////////////////////////////////////////
            // Place this within Playback_WindowEvent!!
            ///////////////////////////////////////////

            LocateChildren.Number_of_Buttons = 0;

            // Place this within Playback_WindowEvent!!
            
            LogWritePlayBack("Starting Look for Buttons...");
            LocateChildren.Start_Looks_For_Buttons(current_window_handle_id);
            
            // Place above in Playback_WindowEvents
            // Place below in Playback_WindowEvent!!

            if (LocateChildren.Number_of_Buttons > 0)
            {
                int MatchedButtonHandleID = RunMatchAgainstButtonInfo(ButtonName);

                if (MatchedButtonHandleID > 0)
                {
                    LogWritePlayBack("Button Match Found.. Sending click to Button HandleID: " + MatchedButtonHandleID.ToString());
                    SendClick(MatchedButtonHandleID);
                }
                else
                {
                    LogWritePlayBack("Button NOT FOUND within Parent Window...Click was NOT sent..");
                }
            }

            ///////////////////////
        }

        private int RunMatchAgainstButtonInfo(string ButtonName)
        {
            int MatchedHandleID = 0;

            // To DO:
            // Have to Take into Account that the Caption Could have '&'
            // Example: OK could be &OK or Save -> &Save

            for (int i = 0; i < LocateChildren.Number_of_Buttons; i++)
            {
                if (ButtonName == LocateChildren.Just_Return_Simple_Button_Info[i].Child_Caption)
                {
                    MatchedHandleID = LocateChildren.Just_Return_Simple_Button_Info[i].Handle_ID;
                    return MatchedHandleID;
                }
            }
            return MatchedHandleID;
        }

        public void Playback_ComboKeyBoardEvent(XmlNode n)
        {
            if (CorrectWindowActiveState)
            {
                string UserAction;
                string UserTextData;

                LogWritePlayBack("Found Combo KeyBoard Event");

                eventID = n.ChildNodes.Item(0).InnerText;
                UserTextData = n.ChildNodes.Item(1).InnerText;

                LogWritePlayBack("KeyBoard Combo Event ID: " + eventID);

                LogWritePlayBack("Sending KeyBoard Event: " + UserTextData);
                LogWritePlayBack("");

                UserAction = "combo";

                string NewUserText = UserTextData.ToLower();
                NewUserText = NewUserText.Replace("lcontrol+", "^");
                NewUserText = NewUserText.Replace("rcontrol+", "^");
                NewUserText = NewUserText.Replace("lshift+", "+");
                NewUserText = NewUserText.Replace("rshift+", "+");

                PlayKeyBoardEvent.KeyBoardEvent(UserAction, NewUserText);

            }
            else
            {
                // Required Window Not Active. Skipping KeyBoard Event
            }
        }
    
        public void Playback_KeyBoardEvent(XmlNode n)
        {
            if (CorrectWindowActiveState)
            {
                string UserAction;
                string UserTextData;
                bool UsingObjectiveAndRemoteSource = false;

                LogWritePlayBack("KeyBoard Event Found");

                eventID = n.ChildNodes.Item(0).InnerText;
                LogWritePlayBack("KeyBoard Event ID: " + eventID);

                UserAction = n.ChildNodes.Item(1).Name.ToString();

                UserTextData = n.ChildNodes.Item(1).InnerText;

                UsingObjectiveAndRemoteSource = CheckIfUsingRemoteSourceData(UserTextData);

                if (UsingObjectiveAndRemoteSource)
                {
                    int index = GetCurrentIndexNumberEvent(); //--> index = GetCurrentIndexNumberEvent();
                
                // Remove $ from UserTextData. Ex: $name becomes name

                   UserTextData = UserTextData.Remove(0, 1);

                   LogWritePlayBack("Converted Value: " + UserTextData);
 
                   UserTextData =  RetrieveXMLSourceDataEvent(index, UserTextData);
                }
                
                LogWritePlayBack("Sending KeyBoard Event: " + UserTextData);

                PlayKeyBoardEvent.KeyBoardEvent(UserAction, UserTextData);
                LogWritePlayBack("");
            }
            else
            {
                // Required Window Not Active. Skipping KeyBoard Event
            }
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
                LogWritePlayBack("DATA CONTAINS $. Reading Data From Source");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Playback_WindowEvent(XmlNode n)
        {
           WindowTitle = PlayWindowEvents.WindowEvent(n);
           ReturnWindowTitleToPlayBackMainEvent(WindowTitle);

           CurrentDefinedGlobalWindow = WindowTitle;

           LogWritePlayBack("Found Window Event. Window Title: " + WindowTitle);
           LogWritePlayBack("");

           // Get Recorded Position for This Window
              
           int RecordedWindowLeftPosition = PlayWindowEvents.ReturnRecordedLeftPosition();
           int RecordedWindowTopPosition = PlayWindowEvents.ReturnRecordedTopPosition();
           
           // TO DO: COME BACK!!!

           // if (UseStrictMonitorConfigurationEvent) // This get's set by the User from the MainForm
           // {
           // CorrectWindowActiveState = PlayWindowEvents.SearchForStrictWindowTitleWithinMonitor(WindowTitle);
           // }
           // else
           // {
                 if (RecordedWindowData.UsingRegularExpression)
                 {
                     LogWritePlayBack("Using RegularExpression..");
                     LogWritePlayBack("Searching ALL running processes..");
                     LogWritePlayBack("");

                     string WindowTitleMatched = WindowSearch.SearchForWindowTitleUsingRegularExpression(WindowTitle);
                    
                     if (WindowTitleMatched != "")
                     {
                         CorrectWindowActiveState = true;
                         WindowTitle = WindowTitleMatched; 
                         LogWritePlayBack("Found Match: " + WindowTitle);

                         if (IsWindowVisible(WindowTitle))
                         {
                             BringWindowToTop(WindowTitle);
                         }
                         else
                         {
                             LogWritePlayBack("Window is not on Desktop: " + WindowTitle);
                             LogWritePlayBack("Can't start test until window is ready..Bring window to Desktop!!");
                             CorrectWindowActiveState = false;
                         }

                         DetectForWindowMove(WindowTitle, RecordedWindowTopPosition, RecordedWindowLeftPosition);

                         // Get Current Position of Window
                         //int WindowHandleID = NewWindowInfo.GetWindowHandleID(WindowTitle);

                         //LogWritePlayBack("Current Window HandleID: " + WindowHandleID.ToString());

                         //Win32.Rect rc = NewWindowInfo.ReturnWindowPositionInfo(WindowHandleID);

                         //int CurrentWindowTopPosition = rc.top;
                         //int CurrentWindowLeftPosition = rc.left;

                         //LogWritePlayBack("Comparing Current and Recorded Position");

                         //if (CurrentWindowTopPosition == RecordedWindowTopPosition && CurrentWindowLeftPosition == RecordedWindowLeftPosition)
                         //{
                         //    LogWritePlayBack("Positions Match");
                         //}
                         //else
                         //{
                         //    LogWritePlayBack("Detected Current Window Has Moved");

                         //    int DeltaTop = CurrentWindowTopPosition - RecordedWindowTopPosition;
                         //    int DeltaLeft = CurrentWindowLeftPosition - RecordedWindowLeftPosition;

                         //    LogWritePlayBack("Moved from Top: " + DeltaTop.ToString());
                         //    LogWritePlayBack("Moved from Left: " + DeltaLeft.ToString());
                         //    LogWritePlayBack("");
                         //}
                             

                         // Win32.BringWindowToTop(
                         // int WindowID = GetWindowHandleID(WindowTitle);
                         // IntPtr IDconverted = new IntPtr(WindowID);

                         // Win32.Rect rc = new Win32.Rect();
                         // Win32.GetWindowRect(IDconverted, ref rc);
                         // RecordedWindowData.Left = rc.left;
                         // RecordedWindowData.Top = rc.top;
                     }
                     else
                     {
                         LogWritePlayBack("Window Not Found: " + WindowTitle);
                         CorrectWindowActiveState = false;
                     }

                     // CorrectWindowActiveState = PlayWindowEvents.SearchForWindowTitleUsingRegularExpression(WindowTitle);
                 }
                 else
                 {
                     CorrectWindowActiveState = WindowSearch.SearchForWindowTitle(WindowTitle);
                     if (CorrectWindowActiveState)
                     {
                         BringWindowToTop(WindowTitle);
                         DetectForWindowMove(WindowTitle, RecordedWindowTopPosition, RecordedWindowLeftPosition);
                         //CorrectWindowActiveState = PlayWindowEvents.SearchForWindowTitle(WindowTitle);
                     }
                     else
                     {
                         LogWritePlayBack("Window Not Found: " + WindowTitle);
                         LogWritePlayBack("Stopping test");
                         CorrectWindowActiveState = false;
                     }
                 }
           // }       
        }

        private void DetectForWindowMove(string WindowTitle, int RecordedWindowTopPosition, int RecordedWindowLeftPosition)
        {
            // Get Current Position of Window

            GlobalDeltaTop = 0;
            GlobalDeltaLeft = 0;

            int WindowHandleID = NewWindowInfo.GetWindowHandleID(WindowTitle);

            LogWritePlayBack("The Window HandleID: " + WindowTitle + " :" + WindowHandleID.ToString());

            Win32.Rect rc = NewWindowInfo.ReturnWindowPositionInfo(WindowHandleID);

            int CurrentWindowTopPosition = rc.top;
            int CurrentWindowLeftPosition = rc.left;

            LogWritePlayBack("Comparing Current and Recorded Positions");

            if (CurrentWindowTopPosition == RecordedWindowTopPosition && CurrentWindowLeftPosition == RecordedWindowLeftPosition)
            {
                LogWritePlayBack("Positions Match");
                LogWritePlayBack("");
            }
            else
            {
                LogWritePlayBack("Detected Current Window Has Moved");

                GlobalDeltaTop = CurrentWindowTopPosition - RecordedWindowTopPosition;
                GlobalDeltaLeft = CurrentWindowLeftPosition - RecordedWindowLeftPosition;

                //GlobalDeltaTop = RecordedWindowTopPosition - CurrentWindowTopPosition;
                //GlobalDeltaLeft = RecordedWindowLeftPosition - CurrentWindowLeftPosition;

                LogWritePlayBack("Moved from Top: " + GlobalDeltaTop.ToString());
                LogWritePlayBack("Moved from Left: " + GlobalDeltaLeft.ToString());
                LogWritePlayBack("");
            }
        }

        private bool IsWindowVisible(string WindowTitle)
        { 
            int current_window_handle = Win32.FindWindow(null, WindowTitle);
            IntPtr convertedID = new IntPtr(current_window_handle);

            return Win32.IsWindowVisible(convertedID); 
        }

        private void BringWindowToTop(string WindowTitle)
        {
            int current_window_handle = Win32.FindWindow(null, WindowTitle);
            LogWritePlayBack("Bringing Window to Top: " + WindowTitle);

            IntPtr convertedID = new IntPtr(current_window_handle);

            Win32.ShowWindow(convertedID, 9);

            Win32.SetForegroundWindow(convertedID);
            uint WindowMessage = (uint)Win32.WindowMessages.WA_ACTIVE;
            Win32.SendMessage(convertedID, WindowMessage, 0, 0);
            LogWritePlayBack("");

            //Win32.SwitchToThisWindow(convertedID, 0);
            //Win32.BringWindowToTop(convertedID);
        }

        public void Playback_MouseUpEvent(XmlNode n)
        {
            if (CorrectWindowActiveState)
            {
                int NewXPosition = 0;
                int NewYPosition = 0;

                eventID = n.ChildNodes.Item(0).InnerText;
                LogWritePlayBack("Mouse Event ID: " + eventID);

                Button_Used = n.ChildNodes.Item(1).InnerText;
                LogWritePlayBack("Button Used: " + Button_Used);

                x_coordinate = n.ChildNodes.Item(2).InnerText;
                y_coordinate = n.ChildNodes.Item(3).InnerText;
                LogWritePlayBack("Sending UP: x: " + x_coordinate + " y: " + y_coordinate);
                LogWritePlayBack("");

                x_position = Convert.ToInt16(x_coordinate);
                y_position = Convert.ToInt16(y_coordinate);

                //LogWritePlayBack("Sending Mouse " + Button_Used + " Up Event");
                //LogWritePlayBack("");

                AdjustForResolutionChange = ResolutionAdjust.CompensateForAnyResolutionChange(x_position, y_position);

                if (AdjustForResolutionChange.RESOLUTION_CHANGE_OCCURED_FLAG)
                {
                    NewXPosition = AdjustForResolutionChange.xposition;
                    NewYPosition = AdjustForResolutionChange.yposition;

                    LogWritePlayBack("Resolution Change Detected: Adjusted X & Y: " + NewXPosition + " , " + NewYPosition);
                    LogWritePlayBack("");

                    // TO DO: COME BACK!!!

                    // COMING SOON!!
                    // -------------
                    // int DELTAX = AdjustForResolutionChange.deltax;
                    // int DELTAY = AdjustForResolutionChange.deltay;
                    // string OLD_RESOLUTION = AdjustForResolutionChange.old_resolution;
                    // string NEW_RESOLUTION = AdjustForResolutionChange.new_resolution;

                    // LogWritePlayBack(...Report Changes...);
                }
                else
                {
                    NewXPosition = x_position;
                    NewYPosition = y_position;
                }
                 
               // string TargetWindowName = RecordedWindowData.WindowTitle;
                
               // int TargetWindowHandleID = NewWindowInfo.GetWindowHandleID(TargetWindowName);

               // AdjustForWindowMove = TargetWindowCheck.CompensateForAnyWindowMove(NewXPosition, NewYPosition, TargetWindowHandleID);

               // int AdjustedX = AdjustForWindowMove.xposition;
               // int AdjustedY = AdjustForWindowMove.yposition;

               // if ((AdjustedX >0 )|| (AdjustedY > 0))
               // {
               //     LogWritePlayBack("Adjusting for Window Move: " + TargetWindowName);
               //     LogWritePlayBack("New X: " + AdjustedX + " New Y: " + AdjustedY);
               //     LogWritePlayBack("");
               // }
                
               // PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "UP");

                if (GlobalDeltaLeft != 0 && GlobalDeltaTop != 0)
                {
                    int AdjustedX = NewXPosition + GlobalDeltaLeft;
                    int AdjustedY = NewYPosition + GlobalDeltaTop;


                   // int AdjustedX = NewXPosition + GlobalDeltaTop;
                   // int AdjustedY = NewYPosition + GlobalDeltaLeft;

                    LogWritePlayBack("Adjusting for Window Move. New X & Y: " + AdjustedX + " , " + AdjustedY);
                    LogWritePlayBack("");

                    PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "UP");
                }
                else
                {
                    PlayMouseEvents.MouseActions(NewXPosition, NewYPosition, Button_Used, "UP");
                }
                
                UpdateProgressBar(); // TO DO: May Take This out!! PlayBackMainParseXML is doing this as well...
            }
            else
            {
                // Required Window Not Active. Skipping MouseEvent
                LogWritePlayBack("Window: " + WindowTitle + " does not exists. Skipping Mouse UP");
            }
        }

        public void Playback_MouseDownEvent(XmlNode n)
        {
            if (CorrectWindowActiveState)
            {
                eventID = n.ChildNodes.Item(0).InnerText;
                LogWritePlayBack("Mouse Event ID: " + eventID);

                Button_Used = n.ChildNodes.Item(1).InnerText;
                LogWritePlayBack("Button Used: " + Button_Used);

                x_coordinate = n.ChildNodes.Item(2).InnerText;
                y_coordinate = n.ChildNodes.Item(3).InnerText;

                LogWritePlayBack("Sending DOWN: x: " + x_coordinate + " y: " + y_coordinate);
                LogWritePlayBack("");

                x_position = Convert.ToInt16(x_coordinate);
                y_position = Convert.ToInt16(y_coordinate);

                //LogWritePlayBack("Sending Mouse " + Button_Used + " Down Event");

                AdjustForResolutionChange = ResolutionAdjust.CompensateForAnyResolutionChange(x_position, y_position);

                int NewXPosition = AdjustForResolutionChange.xposition;
                int NewYPosition = AdjustForResolutionChange.yposition;

                if (AdjustForResolutionChange.RESOLUTION_CHANGE_OCCURED_FLAG)
                {
                    LogWritePlayBack("Resolution Adjusted X & Y: " + NewXPosition + " , " + NewYPosition);
                    LogWritePlayBack("");
                }

               // string TargetWindowName = RecordedWindowData.WindowTitle;
               // int TargetWindowHandleID = NewWindowInfo.GetWindowHandleID(TargetWindowName);

               // AdjustForWindowMove = TargetWindowCheck.CompensateForAnyWindowMove(NewXPosition, NewYPosition, TargetWindowHandleID);

               // int AdjustedX = AdjustForWindowMove.xposition;
               // int AdjustedY = AdjustForWindowMove.yposition;

               // PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "DOWN");

                if (GlobalDeltaLeft != 0 && GlobalDeltaTop != 0)
                {
                    int AdjustedX = NewXPosition + GlobalDeltaLeft;
                    int AdjustedY = NewYPosition + GlobalDeltaTop;

                   // int AdjustedX = NewXPosition + GlobalDeltaTop;
                   // int AdjustedY = NewYPosition + GlobalDeltaLeft;

                    LogWritePlayBack("Adjusting for Window Move. New X & Y: " + AdjustedX + " , " + AdjustedY);
                    LogWritePlayBack("");

                    PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "DOWN");
                }
                else
                {
                    PlayMouseEvents.MouseActions(NewXPosition, NewYPosition, Button_Used, "DOWN");
                }
            }
            else
            {
                // Required Window Not Active. Skipping MouseEvent
            }
        }

            public void Playback_MouseDoubleClickEvent(XmlNode n)
            {
                if (CorrectWindowActiveState)
                {
                    string TargetWindowName = "";
                    int TargetWindowHandleID = 0;

                    LogWritePlayBack("Found Mouse Double Click Event");

                    eventID = n.ChildNodes.Item(0).InnerText;
                    LogWritePlayBack("Mouse Event ID: " + eventID);

                    Button_Used = n.ChildNodes.Item(1).InnerText;
                    LogWritePlayBack("Button Used: " + Button_Used);

                    LogWritePlayBack("");

                    x_coordinate = n.ChildNodes.Item(2).InnerText;
                    y_coordinate = n.ChildNodes.Item(3).InnerText;

                    TargetWindowName = RecordedWindowData.WindowTitle;

                    //LogWritePlayBack("Sending Mouse " + Button_Used + " Click Event");

                    TargetWindowHandleID = NewWindowInfo.GetWindowHandleID(TargetWindowName);

                    x_position = Convert.ToInt16(x_coordinate);
                    y_position = Convert.ToInt16(y_coordinate);

                    AdjustForResolutionChange = ResolutionAdjust.CompensateForAnyResolutionChange(x_position, y_position);

                    int NewXPosition = AdjustForResolutionChange.xposition;
                    int NewYPosition = AdjustForResolutionChange.yposition;

                    if (AdjustForResolutionChange.RESOLUTION_CHANGE_OCCURED_FLAG)
                    {
                        LogWritePlayBack("Resolution Adjusted X & Y: " + NewXPosition + " , " + NewYPosition);
                        LogWritePlayBack("");
                    }

                    //AdjustForWindowMove = TargetWindowCheck.CompensateForAnyWindowMove(NewXPosition, NewYPosition, TargetWindowHandleID);

                    //int AdjustedX = AdjustForWindowMove.xposition;
                    //int AdjustedY = AdjustForWindowMove.yposition;

                    if (GlobalDeltaLeft != 0 && GlobalDeltaTop != 0)
                    {
                        int AdjustedX = NewXPosition + GlobalDeltaLeft;
                        int AdjustedY = NewYPosition + GlobalDeltaTop;
                        
                        LogWritePlayBack("Adjusting for Window Move. New X & Y: " + AdjustedX + " , " + AdjustedY);
                        LogWritePlayBack("");

                        PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "doubleclicked");
                    }
                    else
                    {
                        PlayMouseEvents.MouseActions(NewXPosition, NewYPosition, Button_Used, "doubleclicked");
                    }
            }
            else
            {
                // Required Window Not Active. Skipping MouseEvent
            }
        }

        public void Playback_MouseClickEvent(XmlNode n)
        {
            if (CorrectWindowActiveState)
            {
                string TargetWindowName = "";
                int TargetWindowHandleID = 0;

                LogWritePlayBack("Found Mouse Click Event");

                eventID = n.ChildNodes.Item(0).InnerText;
                LogWritePlayBack("Mouse Event ID: " + eventID);

                Button_Used = n.ChildNodes.Item(1).InnerText;
                LogWritePlayBack("Button Used: " + Button_Used);

                LogWritePlayBack("");

                x_coordinate = n.ChildNodes.Item(2).InnerText;
                y_coordinate = n.ChildNodes.Item(3).InnerText;

                TargetWindowName = RecordedWindowData.WindowTitle;

                //LogWritePlayBack("Sending Mouse " + Button_Used + " Click Event");

                TargetWindowHandleID = NewWindowInfo.GetWindowHandleID(TargetWindowName);

                x_position = Convert.ToInt16(x_coordinate);
                y_position = Convert.ToInt16(y_coordinate);

                AdjustForResolutionChange = ResolutionAdjust.CompensateForAnyResolutionChange(x_position, y_position);

                int NewXPosition = AdjustForResolutionChange.xposition;
                int NewYPosition = AdjustForResolutionChange.yposition;

                if (AdjustForResolutionChange.RESOLUTION_CHANGE_OCCURED_FLAG)
                {
                    LogWritePlayBack("Resolution Adjusted X & Y: " + NewXPosition + " , " + NewYPosition);
                    LogWritePlayBack("");
                }

               // AdjustForWindowMove = TargetWindowCheck.CompensateForAnyWindowMove(NewXPosition, NewYPosition, TargetWindowHandleID);

               // int AdjustedX = AdjustForWindowMove.xposition;
               // int AdjustedY = AdjustForWindowMove.yposition;

              //  if ((AdjustForWindowMove.deltax || AdjustForWindowMove.deltay ) > 0)
              //  {
              //      LogWritePlayBack("TargetWindowMoved:");
              //      LogWritePlayBack("Adjust Mouse X for Move:" + AdjustForWindowMove.deltax.ToString());
              //      LogWritePlayBack("Adjust Mouse Y for Move:" + AdjustForWindowMove.deltay.ToString());
              //      LogWritePlayBack("");
              //      LogWritePlayBack("Previouse X: " +  AdjustForWindowMove.previosevaluex); // This will be returned as a String
              //     LogWritePlayBack("Previouse Y: " + AdjustForWindowMove.previousevaluey); // AdjustForWindowMove.previousevaluey is x value are returned as string
              //      LogWritePlayBack("");
              //}
     
              // PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "clicked");

                if (GlobalDeltaLeft != 0 && GlobalDeltaTop != 0)
                {
                    int AdjustedX = NewXPosition + GlobalDeltaLeft;
                    int AdjustedY = NewYPosition + GlobalDeltaTop;

                   // int AdjustedX = NewXPosition + GlobalDeltaTop;
                   // int AdjustedY = NewYPosition + GlobalDeltaLeft;

                    LogWritePlayBack("Adjusting for Window Move. New X & Y: " + AdjustedX + " , " + AdjustedY);
                    LogWritePlayBack("");

                    PlayMouseEvents.MouseActions(AdjustedX, AdjustedY, Button_Used, "clicked");
                }
                else
                {
                    PlayMouseEvents.MouseActions(NewXPosition, NewYPosition, Button_Used, "clicked");
                }

            }
            else
            {
                // Required Window Not Active. Skipping MouseEvent
            }
        }
    }
}
