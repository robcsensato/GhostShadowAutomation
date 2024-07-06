using System;
using System.IO;
using System.Collections;

using System.Collections.Generic;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Ribbon;
using System.Globalization;
using System.Threading;
using System.Xml;

namespace Automation
{
    public delegate string ReturnDLLDirectory();

    //--> Special Delegate to Move XML File from HTMLSearch to IEHandle --> IEFunction to Main
    public delegate void MoveXMLFileFromHTMLSearchResultDelegate(string xmlsource);

    //--> Delegate to Replay LogWrite from IEHandler To IEFunctionalCommand Class
    public delegate void RelayLogWriteToFunctionalCommandDelegate(string message);

    //--> Delegate to Display Image By Pressing Capture Image Button
    public delegate void DisplayImageBackToMainDelegate(System.Drawing.Image image, bool value, string mode);

    //--> Delegate To CheckIfCurrentParentWindowIs The Ghost Shadow Application
    public delegate bool CheckIfCurrentParentWindowIstheAutomationApplicationDelegate();
  
    //--> Delegate to Replay Control Text Information back From Reflection to MainForm /// TAKE THIS OUT
    public delegate void RelayControlTextInformationBackToMainDelegate(string controltext); /// TAKE THIS OUT

    //--> NEW delegate to GET CONTROL PROPERTIES from ReflectionClass
    public delegate void RelayControlPropertiesInfoBackToMain(List<ControlPropertiesClass> properties_control);

    //--> NEW DELEGATE TO REPLAY LIST<ControlProperties> webcontrol from IEHandler Via IE FunctionalCommand Class
    public delegate void ReplayWebControlPropertiesToMainViaIEFunctionalDelegate(List<ControlPropertiesClass> specialWebControlProperties);
    //--> New delegate to Get Children PROPERTIES from EnumChildren Class
    public delegate void ReplayChildrenPropertiesInfoBackToMain(List<ChildrenControlClass> child_properties);

    //--> New Delegate to Get WebTable values from HTMLSearchResults Class
    public delegate void RelayWebTableDataFromHTMLSearhResults(List<WebTableDataClass> webtableresults);

    //--> Delegate Used to Trigger READING Seperate XML Files.. Used in ReadSeperateXMLFiles 
    public delegate void ReadingNewXMLWithObjectivesDelegate(string XmlFile, string ObjectiveControlFile, string objectfile);
    public delegate void ReadingNewXMLWithOUTObjectives(string XmlFile);

    //--> Delegate Used to Get File Name Selected From OpenFileDialogBoxClass
    public delegate void ReturnFileNameFromDialogBoxDelegate(string FileName, string TypeOfFileBeingStored);

    //--> Delegate Used to Get File Name Selected From SaveFileDialogBoxClass
    public delegate void ReturnFileNameFromSaveDialogBoxDelegate(string FileName, string TypeofFileBeingStored);

    //--> Delegate Used to Replay ReadingNewXMLWithObjectives
    public delegate void Relay_ReadingNewXMLWithObjectivesDelegate(string XmlFile, string ObjectiveControlFile, string objectfile);

    //--> Delegate To Halt ReadingXML --> Used in PlayBackMainParseXML --> loogthroughchildren
    public delegate bool HaltXMLReadingDelegate();

    //--> Delegate to Toggle the HaltReadingXMlValue
    public delegate void ToggleReadingXMLValueDelegate();

    //--> Display Mouse and KeyBoard Events to GUI 
    public delegate void DisplayUserMouseEventMessage (string Message);
    public delegate void DisplayUserKeyBoardEventMessage (string Message);

    //--> Display Window Event Messages to GUI
    public delegate void DisplayWindowEventMessage(string WindowTitle);

    //--> Display Playback Event Messages to GUI
    public delegate void DisplayPlayBackEventMessage(string Message);
    public delegate void SendMessageToMainPlayBackControl (string Message);
    public delegate void RelayPlayBackEventMessage(string Message);
  
    //--> Turn Double Click Timer On/Off
    public delegate void TurnDoubleClickTimerOn();
    public delegate void TurnDoubleClickTimerOff();

    //--> TO DO:: TURN MonitorWindowActivityTimerOFF();

    //--> WriteMouseEventToXML (This is for Mouse UP and DOWN Events). 
    public delegate void WriteMouseUpEventDelegate(string buttontype, string x, string y);
    public delegate void WriteMouseDownEventDelegate(string buttontype, string x, string y);
                             
    //--> WriteMouseClickEventToXML (This is stricly for Mouse Click Events). ClickCount is either Single or Double
    public delegate void WriteMouseClickEventDelegate(string ButtonUsed, string ClickCount, string ButtonX, string ButtonY, string ParentWindowTitle, int window_position_top, int window_position_left);

    //--> Write BUTTON Click Button. Takes ClickCountType as a variable (Single, Double)
    public delegate void WriteButtonClickEventDelegate(string CurrentCaptionstring,string ClickCountType, string ButtonUsed, string ButtonX, string ButtonY, string ActiveWindow, int WindowPositionLeft, int WindowPositionTop);

    //--> WriteKeyBoardEventToXML (KeyBoard Writer)
    public delegate void WriteGeneralKeyBoardToXMLEventDelegate(string data);
    public delegate void WriteComboPressKeyBoardToXMLEventDelegate(string data);
    public delegate void WriteSpecialKeyBoardToXMLEventDelegate(string data);

    //--> WriteWindowEventToXML (NewWindow Writer)
    public delegate void WriteWindowEventToXMLDelegate(string windowTitle);

    //--> Check if the Mouse Clicked Control is a Button. 
    public delegate void CheckIfControlIsAButtonDelegate(IntPtr hWnd, string CurrentClassName, string ButtonUsed, string ButtonX, string ButtonY, string ParentWindowTitle, string ClickCount, int WindowPositionLeft, int WindowPositionTop);

    //--> TREEVIEWER Controler Delagate
    public delegate void TreeViewControlerDelegate(string control, ImageList TreeImage);

    //--> TREEVIEW Return Root Node
    public delegate TreeNode TreeViewRootNodeDelegate(string readerName);

    //--> TREEVIEW Populuate Tree
    public delegate void PopulateTreeDelegate();

    //--> XML VIEWER LISTBOX Controller Delegate
    public delegate void ListBoxControlerDelegate(string control);

    //--> XML VIEWER LISTBOX Add to List
    public delegate void ListBoxAddToList(string addition);

    //--> XML VIEWER LIST Set Selected Value
    public delegate void ListBoxSetSelected();

    //--> XML VIEWER Special Delegate to handle populating TreeView
    delegate void MyDelegate();

    //--> Special Delegate to send XML File Name to XMLViewHandler
    public delegate string SendRECORDFileInfo();

    //-> XML VIEWER MoveToLine in ListBox Delegate
    public delegate void MoveToLineDelegate(int ln);

    //-> Updates the UserDataString
    public delegate void UpdateDataString(string data);

    //-> Sends the UserDataString
    public delegate StringBuilder GetUserDataString();

    //-> Removes Current Index from UserDataString
    public delegate void RemoveDataString (int current_index, int place);

    //-> Replays Mouse Click Event from PlackBackAction and PlayBackParse
    public delegate void ReplayMouseClickEventDelagate();
    
    //-> Initializes Progress Bar
    public delegate void InitializeTotalNodesProgressBar(int max_value);

    //-> Update Progress Bar
    public delegate void UpdateTotalNodeProgressBar();

    //-> Send Complete Message to Progress Bar
    public delegate void SendCompleteMessageProgressBar();

    //-> Send STARTUP Message to Progress Bar
    public delegate void SendStartMessageProgressBar();

    //-> Retrieve Monitor Data from MonitorConfigData Class file. Used in PlayBackResolutionReader.
    public delegate int GetRecorderedMonitorDataDelegate(int index, string position);
    public delegate void SetRecorderedMonitorDataDelegate(int index, string position, int value);

    public delegate void SetRecorderedMonitorFoundDelegate (int index, bool result); 
    public delegate bool GetRecorderedMonitorFoundDelegate (int index);

    public delegate int GetCurrentMonitorDataDelegate(int index, string position);
    public delegate void SetCurrentMonitorDataDelegate(int index, string position, int value);
   
    public delegate void AdjustRecordedMonitorDataDelegate (int Count, string position, int Length);

    public delegate void SetGlobalTotalNumberMonitors (int Count);
    public delegate int GetGlobalTotalNumberMonitors ();

    //-> Delegates for the Replay of information from PlayBackActions to PlayBackParseXML
    public delegate int ReplayGlobalTotalNumberMonitors ();
    public delegate bool ReplayRecorderedMonitorFound(int index);
    public delegate int ReplayRecorderedMonitorData(int index, string position);
    public delegate int ReplayCurrentMonitorData(int index, string position);
    public delegate string ReplaySourceXMLData(int index, string data);

    //-> Delegates for PlackWindowEvents to PlayBackAction
    public delegate void SetTargetWindowInfoDelegate(string WindowTitle, int Top, int Left, bool UsingRegularExpression);

    //-> Delegates for TargetWindowMoveCheck to PlayBackAction
    public delegate int GetRecordedWindowDataDelegate(string position);

    public delegate void WriteNewWindowInfoToXMLDelegate(string CurrentActiveWindow,int Top,int Left);

    public delegate void TargetWindowLogWriteDelegate (string message);

    //-> Delegate to replay UpdateProgressBar from PlayBackAction to PlayBackParseXML
    public delegate void ReplayProgressBar();

    //-> Used in SpyClass to Set the Spy++ Label Text fields
    public delegate void SetClassNameDelegate(string ClassName);
    public delegate void SetWindowTextDelegate(string CurrentWindowText);
    public delegate void SetParentWindowDelegate(string ParentWindowText);

    //-> Used in ReadDefinitionFile to Retrieve ObjectDefinition File
    public delegate XmlTextReader GetGlobalDefinitionReaderDelegate();

    public delegate XmlTextReader GetReplayFromPlayActionsDelegate();
    
    public delegate XmlTextReader GetReplayFromPlayBackMainDelegate();

    public delegate XmlTextReader GetPlayBackObjectDefinitionReaderDelegate();

    //-> LogWrite for PlayBackEnterControl which replays to PlayBackActions
    public delegate void ReplayLogWriteToPlayActionFromPlayControl(string message);

    //-> LogWrite for ReadDefinitionFile which replays to PlayBackEnterControl
    public delegate void ReplayLogWriteForReadDefinitionFileDelegate(string message);

    //-> LogWrite for EnumClildren To PlayBackEnterControl
    public delegate void ReplayLogWriteFromEnumChildrenToPlayControlDelegate(string message);

    //-> Relays XmlTextReader from MainForm to XmlWrite Class
    public delegate XmlTextReader GetXmlTextReaderDelegate();

    //-> Sends Logs Write from CaptureWindowInfo. This message is intented for the PlayBack Window.
    // Needs a replay to PlayBackActions then PlayBackMainParseXML which then goes to MainForm (PlayBackWindow). 
    public delegate void SendMessageToPlayBackDelegate(string message);

    //-> IE Delegates
    public delegate void DisplayIE_Event_ClickHandler(string data, string clientx, string clienty);
    public delegate void DisplayIEInfo(string LocationURL, int handleID, int browserNumber, string LocationName);
    public delegate void DisplayIESelectedHook(string LocationURL, int handleID, int browserNumber, string LocationName);
    public delegate void DisplayWebCaptureImage(string url);
    public delegate void DisplayIEClickEventInfo(string data, string type);
 
    public delegate void IE_FrameWindowInfoReadyForDisplay(string FrameName, string data, IELinksClass links, int FrameNumber, string FrameURL);
    public delegate void IE_NonFrameWindowInfoReadyForDisplay(string data, IELinksClass links);

    public delegate void IE_FRAME_DETECTED(int FrameLength);
    public delegate void IE_NON_FRAME_DETECTED();

    public delegate void IE_ShowBasicInfo(int handleid);

    public delegate void LogWriteIEInfoDelegate(string message);
    public delegate void LogWriteIEWebParseDelegate(string message);

    public delegate void DisplayImageDelegate(Image image, bool value, string mode /* doesn't matter as audo decide sizing is enabled */);

    //--> BrowserNumberForm Delegate
    public delegate void BrowserNumberDelegate (int NumberofBrowsers);

    //-->HTMLObjectSelector for the SendHTMLObjectEvent
    public delegate void SendHTMLObjectDelegate(string htmlobjecttype);

    //-->Trigger using Objective used in PlayBackMainParseXML
    public delegate void TriggerObjectiveResourceDelegate(string name, string xmlsource);

    //-->RetrieveXMLSource used in PlayBackActions
    public delegate string GetXMLSource();

    //-->RetrieveObjectiveNAME used in PlayBackActions
    public delegate string GetObjectiveName();

    //-->RetrieveIfUsingObjectiveBoolean used in PlayBackActions
    public delegate bool GetObjectiveBoolValue();

    //-->Replay Using Objective Bool Value from PlayBackActions to PlayBackMainParseXML
    public delegate bool ReplayUsingObjectiveBoolValueDelegate();

    //-->Populate List of XMLNodes from the XMLSourceData
    public delegate void PopulateListOfNodesDelegate(string rootnode, string data);

    //-->LoadSourceXML Data
    public delegate void LoadSourceDataXML(string xmlsource);

    //--> Return XMLSourceData
    public delegate string RetrieveXMLSourceData(int index, string data);

    public delegate void LogWriteFromXMLSourceDataControllerDelegate (string data);

    //-> Used in PlayBackMainParseXML to find out what the Current Counter is
    public delegate int ReturnGlobalCounterDelegate();

    //-> Used in PlayBackActions to find out what the Current Counter (index) is..
    public delegate int RetrieveGlobalIndexer();
   
    //-> Used in WindowSearchFunctionDuringPlayBack to PlayBackActions. Does a LogWritePlayBack..
    public delegate void WriteToPlayBackFromWindowSearchDelegate(string data);

    //-> LogWrite from MonitorResolutionWrite Back to Main Form Record User Events Window
    public delegate void LogWriteToRecordWindowFromMonitorResolutionDelegate(string message);

    //--> Replay Window Title From PlayActions To PlayBackMainParseXML
    public delegate void ReplayWindowTitleToPlayBackMainDelegate(string windowtitle);

    //--> Get's the Window Title that orignated from PlayActions --> WindowEvent
    public delegate void PushWindowTitleFromPlayBackMainDelegate(string windowtitle);

    //--> Set Time Format
    public delegate void SetTimeFormatDelegate(string Time);

    //--> Returns if Clicked On IE Boolean
    public delegate bool ReturnIFClickedOnIEDelegate();

    //--> Write IE Click Event to XML. Called from ClickButtonHandler
    public delegate void WriteIEClickToXMLDelegate();

    //--> This sets what type of Window Is Active.. Used in CaptureWindowInfo
    public delegate void SendWhatTypeOfWindowIsActiveDelegate(string type);
    
    //--> Write the IE Browser Title information to XML
    public delegate void WriteIEWindowInforToXMLDelegate(string IEWindowTitle, int Top, int Left);
   
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        List<ControlPropertiesClass> wecontrolProperties = new List<ControlPropertiesClass>();
        List<ChildrenControlClass> childrenclass = new List<ChildrenControlClass>();

        string DLL_Directory = Application.StartupPath + @"\Plugins";

        bool GlobalAreObjectivesBeingUsedValue = false;
        
        string GlobalFileNameSetByUser = "";

        string Global_HTMLOBJECT_TYPE = "HREF";

        int Global_Current_Browser_Number = 0;

        bool HaltReadingXMLValue = false;

        bool IEWindowActive = false;

        XmlDocument objDom = new XmlDocument();
        EnumChildren ChildrenControlClass = new EnumChildren();

        StringBuilder data = new StringBuilder(256);

        string CurrentObjectiveName = "";
        bool FirstTimeInLoop = false;

        //int IENumberWindowEventCount = 0;
        int IE_eventcounter = 0;
        bool IE_NumberValueChanged = false;
        int Last_IE_Number = 0;

        bool CorrectWindowActiveState = false;
       
        string GlobalPlayBackFileName = "";
        string GlobalObjectiveControllFile = "";
        string GlobalObjectDefinitionFile = "";

        string GlobalObjectiveName = "";
        string GlobalXMLSource = "";
        bool GlobalBoolUsingObjective = false;
        int GlobalIndexLoop = 0;

        string GlobalTrueParentWindow;
        int GlobalTopPosition;
        int GlobalBottomPosition;
        int GlobalLeftPosition;
        int GlobalRightPosition;
        IntPtr GlobalHandleID;

        PluginServices PluginServer;

        ReflectionClass Reflection;

        OpenFileDialogBoxClass OpenFileDialogControl;
        SaveFileDialogBoxClass SaveFileDialogControl;

        ReadWriteObjectControlFile ObjectiveControl;

        ObjectiveDataContainer ObjectDataInfo;

        XmlTextReader GlobalDefinitionXmlTextReader;

        Browser_NumberForm IENumberWindow;

        //EmbeddedResources Embedded;

        int GlobalNodePositionIndex = 0;
        int GlobalTotalInitialNodeCount = 0;

        bool IsLoopingSafe = true;

        ValidateXML ValidateXML;

        TimeFormatWindow TimeFormat;

        XmlSourceDataController XmlSourceController;

        SpyClass SpyWindowClass;
        TreeViewSerializer serializer;

        UserActivityHook acthook; // Defines and Hooks Mouse plus KeyBoard Events

        XMLWriter XmlWrite;
        XMLViewerHandler XMLViewer;

        CaptureMouseEvents MouseEvents;
        CaptureWindowInfo NewWindowInfo;
        CaptureKeyBoardEvents KeyBoardEvents;

        ClickButtonHandler ClickButtonHandle;

        MonitorResolutionWriter WriteMonitorResolutions;

        PlayBackMainParseXML Playback;
        PlayBackWindowEvents PlayWindowEvents;
        PlayBackMouseEvents PlayMouseEvents;

        TriggerCaptureWindow TriggerPictureCapture;

        IEHandler IE_EventHook;
        IEFunctionalCommand IEFunctional;
        
        string GlobalClassNameForSpyClass;
        string GlobalParentWindowTextForSpyClass;
        string GlobalWindowTextForSpyClass;

        string LastReadTitleFromXML = "";

        // This goes into the new SpyWindowClass
        private IntPtr _hPreviousWindow;

        public MainForm()
        {
            InitializeComponent();
        }

        private int _cacheWidth;

        //Flags to control how Window Events are displayed to User
        //Sends new Line in Text box before message is displayed
        private bool FirstTimeStartingApplication1 = true;

        //Flags to control how Moused and Keyboard Events are displayed to User
        //Sends new Line in Text box before message is displayed
        private bool FirstTimeStartingApplication2 = true;

        //Flag to control how Play BackEvent are displayed to the User
        //Same as above..
        private bool FirstTimeStartingApplication3 = true;

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            splitContainer1.SuspendLayout();

            if (splitContainer1.FixedPanel == FixedPanel.None)
            {
                splitContainer1.FixedPanel = FixedPanel.Panel1;
                splitContainer1.IsSplitterFixed = true;

                _cacheWidth = kryptonHeaderGroup1.Width;
                int newWidth = kryptonHeaderGroup1.PreferredSize.Height;

                splitContainer1.Panel1MinSize = newWidth;
                splitContainer1.SplitterDistance = newWidth;

                kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Right;
                kryptonHeaderGroup1.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
            }
            else
            {
                splitContainer1.FixedPanel = FixedPanel.None;
                splitContainer1.IsSplitterFixed = false;
                splitContainer1.Panel1MinSize = 25;
                splitContainer1.SplitterDistance = _cacheWidth;

                kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Top;
                kryptonHeaderGroup1.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;
            }
            splitContainer1.ResumeLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Setting default page to kyptonPage1 --> Record User Events Window
            // To Do Note: Change kyptonPage1 to --> GhostRecordUserEventsPage

            // Set's the Record Window as Default during startup
            kryptonNavigator1.SelectedPage = kryptonPage1; //kryptopnPage1 --> Record Window

            // US     DATA TIME FORMAT "en-US" 
            // JAPAN  DATA TIME FORMAT "ja-JP";
            // FRENCH DATE TIME FORMAT "fr-FR" 
            
            SetDateFormat("en-US"); //--> Setting 'US' Format by Default

           // IENumberWindow = new Browser_NumberForm();

            string GlobalObjectiveName = "";
            string GlobalXMLSource = "";
            bool GlobalBoolUsingObjective = false;

            PluginServer = new PluginServices();

            Reflection = new ReflectionClass();

            ValidateXML = new ValidateXML();

            SpyWindowClass = new SpyClass();
            Name_textBox.Visible = false;
            serializer = new TreeViewSerializer();
            
            SpyWindowClass.SetClassNameEvent += new SetClassNameDelegate(SpyWindowClass_SetClassNameEvent);
            SpyWindowClass.SetParentWindowTextEvent += new SetParentWindowDelegate(SpyWindowClass_SetParentWindowTextEvent);
            SpyWindowClass.SetWindowTextEvent += new SetWindowTextDelegate(SpyWindowClass_SetWindowTextEvent);

            TriggerPictureCapture = new TriggerCaptureWindow();
            
            OpenFileDialogControl = new OpenFileDialogBoxClass();

            SaveFileDialogControl = new SaveFileDialogBoxClass();
            
            ObjectiveControl = new ReadWriteObjectControlFile();

            ObjectDataInfo = new ObjectiveDataContainer();

            XmlSourceController = new XmlSourceDataController();

            Browser_NumberForm IENumberWindow = new Browser_NumberForm();

            //Attach Recording and Writing Class Files
            XmlWrite = new XMLWriter();
            WriteMonitorResolutions = new MonitorResolutionWriter();
            NewWindowInfo = new CaptureWindowInfo();

            //IE Handler Classes
            IEFunctional = new IEFunctionalCommand();
            IE_EventHook = new IEHandler();

            //Attach XMLViewer Handler Class
            XMLViewer = new XMLViewerHandler();

            //Attach Event Class Files
            MouseEvents = new CaptureMouseEvents();
            KeyBoardEvents = new CaptureKeyBoardEvents();
            ClickButtonHandle = new ClickButtonHandler();

            //Attach PlayBack XML Reader
            Playback = new PlayBackMainParseXML();
            PlayWindowEvents = new PlayBackWindowEvents();
            PlayMouseEvents = new PlayBackMouseEvents();

            TimeFormat = new TimeFormatWindow();

            //Attach PlayBack Actions. This contains the real PlayBack Nuts and Bolts
          
            // Attaches Hooks to KeyBoard and Mouse
            acthook = new UserActivityHook();
            acthook.Stop(); //--> Monitoring Hooks Turned Off

            acthook.LoadHotKeyDown();
            acthook.HotKeyDown += new KeyEventHandler(KeyBoardEvents.HotKeyPressedDown);
            //-> This turns off the HotKey Option. acthook.UnloadHotKeyDown();

            //Hooking Mouse Events
            acthook.MouseDown += new MouseEventHandler(MouseEvents.MouseDown);
            acthook.MouseUp += new MouseEventHandler(MouseEvents.MouseUp);
            acthook.MouseMoved += new MouseEventHandler(MouseEvents.MouseMoved);

            //Hooking KeyBoard Events
            acthook.KeyDown += new KeyEventHandler(KeyBoardEvents.MyKeyDown);
            acthook.KeyUp += new KeyEventHandler(KeyBoardEvents.MyKeyUp);
            acthook.KeyPress += new KeyPressEventHandler(KeyBoardEvents.MyKeyPress);

            //Determine Click or Double Click
            theDoubleClickTimer.Interval = Win32.GetDoubleClickTime();
            theDoubleClickTimer.Enabled = false;
            theDoubleClickTimer.Tick += new EventHandler(MouseEvents.DoubleClickTimeElapsed);

            //Write to MainForm Events
            //-> General Mouse Message Event Handler. Writes Mouse UserEvents to Text Box
            MouseEvents.SendUserMessage += new DisplayUserMouseEventMessage(MouseEvents_SendUserMessage);

            //-> General KeyBoard Message Event Handler. Write KeyBoard UserEvent to Text Box
            KeyBoardEvents.SendKeyBoardMessage += new DisplayUserKeyBoardEventMessage(KeyBoardEvents_SendKeyBoardMessage);
            KeyBoardEvents.DisplayImageEvent += new DisplayImageDelegate(KeyBoardEvents_DisplayImage);
            KeyBoardEvents.WriteComboPressedEvent += new WriteComboPressKeyBoardToXMLEventDelegate(KeyBoardEvents_WriteComboPressedEvent);
            KeyBoardEvents.WriteGeneralKeyBoardEvent += new WriteGeneralKeyBoardToXMLEventDelegate(KeyBoardEvents_WriteGeneralKeyBoardEvent);
            KeyBoardEvents.WriteSpecialKeyBoardEvent += new WriteSpecialKeyBoardToXMLEventDelegate(KeyBoardEvents_WriteSpecialKeyBoardEvent);
           // KeyBoardEvents.CaputureIEAsPictureEvent += new CaptureIEWebPageDelegate(CaptureIEAsPictureMethod);
            KeyBoardEvents.ToggleReadXMLValueEvent += new ToggleReadingXMLValueDelegate(KeyBoardEvents_ToggleReadXMLValueEvent);

            //-> General Message Event Handle. Writes New WindowEvent message to Text Box
            NewWindowInfo.SendWindowMessage += new DisplayWindowEventMessage(NewWindowInfo_SendWindowMessage);
            NewWindowInfo.WriteNewWindowInfoToXMLEvent += new WriteNewWindowInfoToXMLDelegate(NewWindowInfo_WriteNewWindowInfoToXMLEvent);
            NewWindowInfo.SendWhatTypeOfWindowThisIsEvent += new SendWhatTypeOfWindowIsActiveDelegate(NewWindowInfo_SendWhatTypeOfWindowThisIsEvent);
            NewWindowInfo.WriteIEBrowserTitleToXMLEvent += new WriteIEWindowInforToXMLDelegate(NewWindowInfo_WriteIEBrowserTitleToXMLEvent);
            
            //-> Events for Turning the Timer On or Off
            MouseEvents.TurnTimerOnEvent += new TurnDoubleClickTimerOn(MouseEvents_TurnTimerOnEvent);
            MouseEvents.TurnTimerOffEvent += new TurnDoubleClickTimerOff(MouseEvents_TurnTimerOffEvent);

            //--> Replays MouseEvent From CaptureMouseEvent to CaptureKeyBoardEvent
            MouseEvents.SendClickEventToMain += new ReplayMouseClickEventDelagate(MouseEvents_SendClickEventToMain);
            
            //-> Idenitify any NEW windows that appears during PlayBack. Based on Timer
            NewWindowMonitorTimer.Interval = 1000;
            NewWindowMonitorTimer.Enabled = false;
            NewWindowMonitorTimer.Tick +=new EventHandler(NewWindowMonitorTimer_Tick);

            //-> Write Mouse DOWN and UP Events to XML
            MouseEvents.WriteMouseDOWNToXML += new WriteMouseDownEventDelegate(MouseEvents_WriteMouseDOWNToXML);
            MouseEvents.WriteMouseUPToXML += new WriteMouseUpEventDelegate(MouseEvents_WriteMouseUPToXML);

            //-> MouseEvent. Check if Clicked Control is a Button
            MouseEvents.CheckifControlIsAButton += new CheckIfControlIsAButtonDelegate(MouseEvents_Send_CheckifControlMessage);
            
            //-> Normal Click Handler ... Not clicking on a Windows Form Button - Write to XML
            ClickButtonHandle.WriteMouseClickToXML += new WriteMouseClickEventDelegate(ClickButtonHandle_WriteMouseClickToXML);
            
            //-> Button Click Handler. User Clicked on a Windows Form Button - Write To XML
            ClickButtonHandle.WriteButtonClickToXML += new WriteButtonClickEventDelegate(ClickButtonHandle_WriteButtonClickToXML);
         
            //-> Write User Clicked Button Message back to Mainform
            ClickButtonHandle.WriteMessageToUser += new DisplayUserMouseEventMessage(MouseEvents_SendUserMessage);

            //-> Sends UserDataString to ClickButtonHandler
            ClickButtonHandle.GetDataString += new GetUserDataString(ClickButtonHandle_GetDataString);

            ClickButtonHandle.UpdateData += new UpdateDataString(ClickButtonHandle_UpdateData);
            
            //--> Returns boolean from MainForm to Click Button Handler
            ClickButtonHandle.CHECK_IF_CLICKED_ON_IE_BROWSER_EVENT += new ReturnIFClickedOnIEDelegate(ClickButtonHandle_CHECK_IF_CLICKED_ON_IE_BROWSER_EVENT);
            
            //--> Write the IE Click Event to XML.. Called from ClickButtonHandler
            ClickButtonHandle.WriteIEClickToXMLEvent += new WriteIEClickToXMLDelegate(ClickButtonHandle_WriteIEClickToXMLEvent);
            //-> TreeView Controller Event
            XMLViewer.TreeviewControl += new TreeViewControlerDelegate(XMLViewer_TreeviewControl);
            
            //-> TreeView Return RootNode
            XMLViewer.ReturnTreeViewRootNode += new TreeViewRootNodeDelegate(XMLViewer_ReturnTreeViewRootNode);
            
            //-> Populate TreeView Delegate
            XMLViewer.PopulateTreeView += new PopulateTreeDelegate(XMLViewer_PopulateTreeView);

            //-> ListBox Controller Event
            XMLViewer.ListBoxControl += new ListBoxControlerDelegate(XMLViewer_ListBoxControl);

            //-> ListBox Add to ListBox
            XMLViewer.AddtoListBox += new ListBoxAddToList(XMLViewer_AddtoListBox);

            //-> Move to Line in ListBox after TreeView Select
            XMLViewer.MoveToListinBoxLine += new MoveToLineDelegate(XMLViewer_MoveToListinBoxLine);

            //-> ListBox Set Selected
            XMLViewer.SetSelectedListBox += new ListBoxSetSelected(XMLViewer_SetSelectedListBox);
            
            //-> SendLatestXMLFile to XMLViewHandler
            XMLViewer.GetLatestXMLFile += new SendRECORDFileInfo(XMLViewer_GetLatestXMLFile);

            XmlWrite.GetXmlTextReaderEvent += new GetXmlTextReaderDelegate(XmlWrite_GetXmlTextReaderEvent);
            
            //-> PlayBack Event to write to PlayBack ListBox
            Playback.LogWritePlayBack += new SendMessageToMainPlayBackControl(PlayBackEvents_SendUserMessage);
            Playback.InitializeProgressBar += new InitializeTotalNodesProgressBar(Playback_InitializeProgressBar);
            Playback.UpdateProgressBar += new UpdateTotalNodeProgressBar(Playback_UpdateProgressBar);
            Playback.CompleteProcessBar += new SendCompleteMessageProgressBar(Playback_CompleteProcessBar);
            Playback.GetPlayBackObjectDefinitionReaderEvent += new GetPlayBackObjectDefinitionReaderDelegate(Playback_GetPlayBackObjectDefinitionReaderEvent);
            Playback.TriggerObjectiveResourceEvent += new TriggerObjectiveResourceDelegate(Playback_TriggerObjectiveResourceEvent);
            Playback.SetUpDelegateEvent();
            Playback.ReplaySourceXMLData += new ReplaySourceXMLData(Playback_ReplaySourceXMLData);
            Playback.GetUsingObjectiveBoolValueEvent += new ReplayUsingObjectiveBoolValueDelegate(Playback_GetUsingObjectiveBoolValueEvent);
            Playback.GlobalCounterEvent += new ReturnGlobalCounterDelegate(ReturnGlobalCounter);
            Playback.HaltReadingXMLEvent += new HaltXMLReadingDelegate(Playback_HaltReadingXMLEvent);
            Playback.PushWindowTitleToMainFormFromPlayBackMainViaPlayBackActionsEvent += new PushWindowTitleFromPlayBackMainDelegate(Playback_PushWindowTitleToMainFormFromPlayBackMainViaPlayBackActionsEvent);
            Playback.RelayStartingPlayToMainForm += new Relay_ReadingNewXMLWithObjectivesDelegate(Playback_RelayStartingPlayToMainForm);

            //-> Updates the User Data String. The variable that's written when user types
            KeyBoardEvents.UpdateData += new UpdateDataString(KeyBoardEvents_UpdateData);
            KeyBoardEvents.RemoveData += new RemoveDataString(KeyBoardEvents_RemoveData);
          
            //-> IE Handler Subscribitions
            IEFunctional.LogWriteIEInfo += new LogWriteIEInfoDelegate(IEFunctional_LogWriteIEInfo);
            IEFunctional.LogWriteIEParse += new LogWriteIEWebParseDelegate(IEFunctional_LogWriteIEParse);
            IEFunctional.TriggerDisplayOfWebPicture += new DisplayWebCaptureImage(IEFunctional_TriggerDisplayOfWebPicture);
            IEFunctional.SetUpDelegates();
            IEFunctional.ReplayWebControlPropertiesEvent += new ReplayWebControlPropertiesToMainViaIEFunctionalDelegate(IEFunctional_ReplayWebControlPropertiesEvent);
            IEFunctional.MoveXMLSourceToMainEvent += new MoveXMLFileFromHTMLSearchResultDelegate(IEFunctional_MoveXMLSourceToMainEvent);
            IEFunctional.webtable_results_event += new RelayWebTableDataFromHTMLSearhResults(IEFunctional_webtable_results_event);

            //-> Setting Browser Number. This subscribes to that event
            //IENumberWindow.BrowserNumber += new BrowserNumberDelegate(SetBrowserNumberMethod);

            //-> Subscribing to the IEHandler Events

            /// TEMP TAKE THIS OUT.. ONLY NEED FOR DE-BUGGIN
            //IE_EventHook.LogWrite += new LogWriteIEInfoDelegate(IEFunctional_LogWriteIEInfo);

            IE_EventHook.IE_BrowserSpecificHookEvent += new DisplayIESelectedHook(IE_EventHook_IE_BrowserSpecificHookEvent);
            IE_EventHook.IE_Window_NO_FrameEvent += new IE_NonFrameWindowInfoReadyForDisplay(IE_EventHook_IE_Window_NO_FrameEvent);
            IE_EventHook.IE_Window_FRAMEEVEMT += new IE_FrameWindowInfoReadyForDisplay(IE_EventHook_IE_Window_FRAMEEVEMT);
            IE_EventHook.IE_DISCOVERED_FRAMES += new IE_FRAME_DETECTED(IE_EventHook_IE_DISCOVERED_FRAMES);
            IE_EventHook.IE_DISCOVERED_NONFRAME += new IE_NON_FRAME_DETECTED(IE_EventHook_IE_DISCOVERED_NONFRAME);
            IE_EventHook.WebCaptureReadForDiplay += new DisplayWebCaptureImage(IE_EventHook_WebCaptureReadForDiplay);
         //   IE_EventHook.RelayControlInfoBackToMainEvent += new RelayControlPropertiesInfoBackToMain(JustDefineTheWebcontrolProperties);
            WriteMonitorResolutions.LogWrite += new LogWriteToRecordWindowFromMonitorResolutionDelegate(WriteMonitorResolutions_LogWrite);

            XmlSourceController.LogWrite += new LogWriteFromXMLSourceDataControllerDelegate(XmlSourceController_LogWrite);

            TimeFormat.SendUpdateTimeFormatEvent += new SetTimeFormatDelegate(TimeFormat_SendUpdateTimeFormatEvent);

            //--> Subscribing to OpenFileDialogBox Class to GET File Name SET by user
            OpenFileDialogControl.ReturnFileNameEvent += new ReturnFileNameFromDialogBoxDelegate(FileDialogControl_ReturnFileNameEvent);

            //--> Subscribing to SaveFileDialogBox Class to Get File Name SET by user
            SaveFileDialogControl.SendFileNameToBeSaved += new ReturnFileNameFromSaveDialogBoxDelegate(FileDialogControl_ReturnFileNameEvent);
            //IE_EventHook.IEdataselector = "HREF";
            //WebCapture Handler
            //WebCapture.WebCaptureReadForDiplay += new DisplayWebCaptureImage(WebCapture_WebCaptureReadForDiplay);

            Reflection.ControlText += new RelayControlTextInformationBackToMainDelegate(Reflection_ControlText);
            Reflection.CheckIfCurrentParentWindowIsAutomationApplication_Event += new CheckIfCurrentParentWindowIstheAutomationApplicationDelegate(Reflection_CheckIfCurrentParentWindowIsAutomationApplication_Event);
            Reflection.RelayControlInfoBackToMainEvent += new RelayControlPropertiesInfoBackToMain(Reflection_RelayControlInfoBackToMainEvent);
       
            TriggerPictureCapture.SetUpDelegate();
            TriggerPictureCapture.DisplayImageBackToMainEvent += new DisplayImageBackToMainDelegate(KeyBoardEvents_DisplayImage);
            _pictureBox.Image = Automation.Properties.Resources.FinderHome_new;
            //_pictureBox.MouseDown += new MouseEventHandler(OnFinderToolMouseDown);
            //_pictureBox.Click += new EventHandler(_pictureBox_Click);
            _pictureBox.MouseClick +=new MouseEventHandler(OnFinderToolMouseDown);

            ChildrenControlClass.SendParentWindowChildrenControlInfoToMain_Event += new ReplayChildrenPropertiesInfoBackToMain(ChildrenControlClass_SendParentWindowChildrenControlInfoToMain_Event);
            Playback.ThePluginDirectory += new ReturnDLLDirectory(Playback_ThePluginDirectory);

            PluginServer.SendMessage += new DisplayPlayBackEventMessage(PluginServer_SendMessage);
            
        }

        void PluginServer_SendMessage(string Message)
        {
            LogWritePlayBackEvents(Message);
        }

        string Playback_ThePluginDirectory()
        {
            return DLL_Directory;
        }

        void IEFunctional_webtable_results_event(List<WebTableDataClass> webtableresults)
        {
            //
            dataGridView2.DataSource = null;
            dataGridView2.DataError +=new DataGridViewDataErrorEventHandler(dataGridView2_DataError);

            bindingSource2.DataSource = webtableresults;
            dataGridView2.DataSource = bindingSource2;
        }

        void IEFunctional_MoveXMLSourceToMainEvent(string xmlsource)
        {
            // Send XMLSource to a DataViewer
         //   dataGridView2.DataSource = null;
        //    dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dataGridView2_DataError);

           // bindingSource2.DataSource = child_properties;
           // dataGridView2.DataSource = bindingSource2;
        //    bindingSource2.DataSource = xmlsource;
        //    dataGridView2.DataSource = bindingSource2;

           LogWritePlayBackEvents("Parsed to Main: " + xmlsource);
           LogWritePlayBackEvents("");
        }

        void SetBrowserNumberMethod(int TheBrowserNumber)
        {
            IEFunctional.DefineNewIEBrowser(TheBrowserNumber);
        }

        void IEFunctional_ReplayWebControlPropertiesEvent(List<ControlPropertiesClass> specialWebControlProperties)
        {
            wecontrolProperties = specialWebControlProperties;
        }

        void ChildrenControlClass_SendParentWindowChildrenControlInfoToMain_Event(List<ChildrenControlClass> child_properties)
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dataGridView2_DataError);

            bindingSource2.DataSource = child_properties;
            dataGridView2.DataSource = bindingSource2;
        }

        void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        bool Reflection_CheckIfCurrentParentWindowIsAutomationApplication_Event()
        {
            if (ParentWindowTextBox.Text == "Automation")
            {
                return true;
            }
            else
            {
                return false;
            }
         }

        void JustDefineTheWebcontrolProperties(List<ControlPropertiesClass> properties_control)
        {
            wecontrolProperties = properties_control;
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("GOT A PROPERTY!!");
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("THIS IS THE COUNT: " + wecontrolProperties.Count.ToString());
            LogWritePlayBackEvents("");
        }

        void Reflection_RelayControlInfoBackToMainEvent(List<ControlPropertiesClass> properties_control)
        {
            bindingSource1.DataSource = properties_control;
            string count = properties_control.Count.ToString();

            dataGridView1.DataSource = null;
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
          //  dataGridView1.DataSource = properties_control;
            dataGridView1.DataSource = bindingSource1;
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            LogWritePlayBackEvents("******************************");
            LogWritePlayBackEvents("Data Error: " + e.ToString());
            LogWritePlayBackEvents("");
        }

        void Reflection_ControlText(string controltext) // TAKE THIS OUT!!
        {
            LogWritePlayBackEvents(controltext);
        }


        void FileDialogControl_ReturnFileNameEvent(string FileName, string TypeOfFileBeingStored)
        {
            if (TypeOfFileBeingStored == "PlayBack")
            {
                GlobalPlayBackFileName = FileName;
            }

            if (TypeOfFileBeingStored == "ObjectFile")
            {
                GlobalObjectDefinitionFile = FileName;
            }

            if (TypeOfFileBeingStored == "ObjectiveController")
            {
                GlobalObjectiveControllFile = FileName;
            }
        }

        string Get_GlobalFileNameSetByUser()
        {
            return GlobalFileNameSetByUser;
        }

        int ObjectiveReader_GetTotalNumberOfElementsEvent()
        {
           return ObjectDataInfo.ReturnNumberOfObjectivesInArray();
        }

        string ObjectiveReader_GetCurrentObjectiveNameEvent(int index)
        {
           return ObjectDataInfo.ReturnObjectiveBasedOnIndex(index);
        }

        int ObjectiveReader_GetCountIndexForObjectiveEvent(string CurrentObjectiveName)
        {
            return ObjectDataInfo.ReturnCountForObjective(CurrentObjectiveName);
        }

        void ObjectiveReader_LogWritePlayBackEvents(string Message)
        {
            LogWritePlayBackEvents(Message);
        }

        void Playback_RelayStartingPlayToMainForm(string XmlFile, string ObjectiveControlFile, string objectfile)
        {
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("Found XMLFILE Tag...Preparing XML");
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("Defining Object File:" + objectfile);
            GlobalObjectDefinitionFile = objectfile;
            //DefineObjectFile(objectfile);
            
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("Defining XML File:" + XmlFile);
            DefineNewXML_For_SeperateXMLPlayBack(XmlFile);
   
            SetUpObjectiveControlList(ObjectiveControlFile);
            StartingPlayBack(true);
        }

        void NewWindowInfo_WriteIEBrowserTitleToXMLEvent(string IEWindowTitle, int Top, int Left)
        {
            //To Do: Think about this some more...IE Window Title is currently based on Browser Number

            IEWindowTitle = "IEWindow:# " + Global_Current_Browser_Number.ToString(); // This is only a temp fix..

            XmlWrite.writeIECaption_To_XML(IEWindowTitle, Top, Left, GlobalPlayBackFileName);
        }

        void NewWindowInfo_SendWhatTypeOfWindowThisIsEvent(string type)
        {
            if (type == "IE_BROWSER")
            {
                IEWindowActive = true;
            }
            else
            {
                IEWindowActive = false;
            }
        }

        void ClickButtonHandle_WriteIEClickToXMLEvent()
        {
            //TO DO: Finish this up.. Implement Writing IE Click Info..
            LogWriteUserEvents("In Future this will Write IE Click info to XML...");

            //XmlWrite.writeMouseClick_From_IE("IETitle", "data", "datatype");
            //IEWindowActive = false;
        }

        bool ClickButtonHandle_CHECK_IF_CLICKED_ON_IE_BROWSER_EVENT()
        {
            return IEWindowActive;
        }

        void Playback_PushWindowTitleToMainFormFromPlayBackMainViaPlayBackActionsEvent(string windowtitle)
        {
            LastReadTitleFromXML = windowtitle;
            MonitorActiveWindowTimer.Enabled = true;
        }

        void WriteMonitorResolutions_LogWrite(string message)
        {
            LogWriteUserEvents(message);
        }

        void KeyBoardEvents_ToggleReadXMLValueEvent()
        {
            LogWritePlayBackEvents("Detected Halt command..Stopping Plack Back..");
            HaltReadingXMLValue = true;
        }

        bool Playback_HaltReadingXMLEvent()
        {
            MonitorActiveWindowTimer.Enabled = false;
            return HaltReadingXMLValue;
        }

        void IEFunctional_TriggerDisplayOfWebPicture(string url)
        {
            LogWriteParseWeb("Displaying Picture: " + url);
            LogWriteParseWeb("--------------------------");
            pictureBox1.Load(url);  
        }

        void IE_EventHook_WebCaptureReadForDiplay(string url)
        {
            LogWriteParseWeb("Displaying Picture: " + url);
            LogWriteParseWeb("--------------------------");
            pictureBox1.Load(url);
        }

        void CaptureIEAsPictureMethod()
        {
            //WebCapture.Capture_Web_Page();
            //IE_EventHook.NewHookIE(false, false, 0, null, null);
            //IE_EventHook.Capture_Web_Page();
        }

        void KeyBoardEvents_WriteSpecialKeyBoardEvent(string data)
        {
            XmlWrite.writeSpecialKeyboardEvent_To_XML(data, GlobalPlayBackFileName);
        }

        void KeyBoardEvents_WriteGeneralKeyBoardEvent(string data)
        {
            XmlWrite.writeNormalKeyboardEvent_To_XML(data, GlobalPlayBackFileName);
        }

        void KeyBoardEvents_WriteComboPressedEvent(string data)
        {
            XmlWrite.writeComboPressedKeyBoardEvent_To_XML(data, GlobalPlayBackFileName);
        }

        int ReturnGlobalCounter()
        {
            return GlobalIndexLoop;
        }

        bool Playback_GetUsingObjectiveBoolValueEvent()
        {
            return GlobalBoolUsingObjective;
        }

        void XmlSourceController_LogWrite(string data)
        {
            LogWritePlayBackEvents(data);
        }

        string Playback_ReplaySourceXMLData(int index, string data)
        {
            return XmlSourceController.RetrieveInfoFromSourceXML(data, index);
        }

        void Playback_TriggerObjectiveResourceEvent(string objectivename, string sourcexml)
        {
            GlobalXMLSource = sourcexml;
            GlobalBoolUsingObjective = true;
           
            GlobalObjectiveName = objectivename;

            if (sourcexml != "")
            {
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("Loading XML Source Data");
                LogWritePlayBackEvents("");

                XmlSourceController.LoadXml(sourcexml);

                  LogWritePlayBackEvents("");
                  LogWritePlayBackEvents("Processing XML Source Data");
                  LogWritePlayBackEvents("");
                  XmlSourceController.ProcessXMLSource(); // This will use PopulateListOfNodes internally. This response is the total number of data processed.
                  LogWritePlayBackEvents("");
                  LogWritePlayBackEvents("Processing Complete");
                  LogWritePlayBackEvents("");
            }
        }

        bool GetGlobalBoolUsingObjectiveValue()
        {
            return GlobalBoolUsingObjective; 
        }

        string GetObjectiveNameValue()
        {
            return GlobalObjectiveName; 
        }

        string GetXMLSource()
        {
            return GlobalXMLSource;
        }

        void IE_EventHook_IE_DISCOVERED_NONFRAME()
        {
            LogWriteParseWeb("");
            LogWriteParseWeb("DETECTED NON FRAME");
            LogWriteParseWeb("-------------------");
            LogWriteParseWeb("");
        }

        void IE_EventHook_IE_DISCOVERED_FRAMES(int FrameLength)
        {
            LogWriteParseWeb("");
            LogWriteParseWeb("DETECTED FRAMES");
            LogWriteParseWeb("Number of Frames within Browser: " + FrameLength.ToString());
            LogWriteParseWeb(""); 
        }

        void IE_EventHook_IE_Window_FRAMEEVEMT(string WindowFrameName, string FrameBody, IELinksClass links, int FrameNumber, string FrameURL)
        {
            LogWriteParseWeb("FRAME NAME: " + WindowFrameName);
            LogWriteParseWeb("FRAME NUMNER: " + FrameNumber);
            LogWriteParseWeb("FRAME URL: " + FrameURL);
            LogWriteParseWeb("FRAME BODY: " + FrameBody);

            LogWriteParseWeb("");

            LogWriteParseWeb("LINKS FROM FRAME " + FrameNumber);
            LogWriteParseWeb("-------------------------");

            while (links != null)
            {
                LogWriteParseWeb("Link: " + links.Val);
                links = (IELinksClass)links.NextItem;
            }
        }

        void IE_EventHook_IE_Window_NO_FrameEvent(string HTMLBody, IELinksClass links)
        {
            LogWriteParseWeb("---------");
            LogWriteParseWeb("Body HTML");
            LogWriteParseWeb("---------");
            LogWriteParseWeb(HTMLBody);

            LogWriteParseWeb("");
            LogWriteParseWeb("LINKS");
            LogWriteParseWeb("-----");

            while (links != null)
            {
                LogWriteParseWeb("Link: " + links.Val);
                links = (IELinksClass)links.NextItem;
            }    
        }

        void IE_EventHook_IE_BrowserSpecificHookEvent(string LocationURL, int handleID, int browserNumber, string LocationName)
        {
            //Hooks to Specfic IE Browser. This method needs to be in the Main.
            LogWriteParseWeb("------------------------");
            LogWriteParseWeb("Locked into Click Events");
            LogWriteParseWeb("Browser Number: " + browserNumber);
            LogWriteParseWeb("Location Name: " + LocationName);
            LogWriteParseWeb("BrowserURL: " + LocationURL);
            LogWriteParseWeb("");
            LogWriteParseWeb("HandleID: " + handleID);
            LogWriteParseWeb("------------------------");
            LogWriteParseWeb("");
        }

        void KeyBoardEvents_DisplayImage(Image image, bool value, string mode)
        {
            Rectangle rcCanvas = pictureBox1.ClientRectangle;

            // if we've got an image
            if (image != null)
            {
                // see if we need to stretch it to fit
                if (image.Width > rcCanvas.Width || image.Height > rcCanvas.Height)
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                // of if it's small enough to center
                else if (image.Width <= rcCanvas.Width && image.Height <= rcCanvas.Height)
                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            // either way, display it
            pictureBox1.Image = image;
        }

        void IEFunctional_LogWriteIEParse(string message)
        {
            IEParseWeb.AppendText(message + Environment.NewLine);
            IEParseWeb.SelectionStart = IEParseWeb.Text.Length;
        }

        void IEFunctional_LogWriteIEInfo(string message)
        {
            IEBrowserInfo.AppendText(message + Environment.NewLine);
            IEBrowserInfo.SelectionStart = IEBrowserInfo.Text.Length;
        }

        XmlTextReader XmlWrite_GetXmlTextReaderEvent()
        {
            return GlobalDefinitionXmlTextReader;
        }

        XmlTextReader Playback_GetPlayBackObjectDefinitionReaderEvent()
        {
            return GlobalDefinitionXmlTextReader;
        }

        void SpyWindowClass_SetWindowTextEvent(string CurrentWindowText)
        {
            GlobalWindowTextForSpyClass = CurrentWindowText;
        }

        void SpyWindowClass_SetParentWindowTextEvent(string ParentWindowText)
        {
            GlobalParentWindowTextForSpyClass = ParentWindowText;
        }

        void SpyWindowClass_SetClassNameEvent(string ClassName)
        {
            GlobalClassNameForSpyClass = ClassName;
        }

        void Embedded_LogWriteTEMP(string Message)
        {
            LogWriteUserEvents(Message);
        }

        void OnFinderToolMouseDown(object sender, MouseEventArgs e)
        {
            bool IEBrowserFound = false;

            NewWindowInfo.Discover_Open_Applications();

            // TO DO: Create within CaptureWindowInfo CheckIfIEBrowserIsRunning
            // IEBrowserFound = NewWindowInfo.CheckIfIEBrowserIsRunning();
        
           // int NumberOfAppsOpen = NewWindowInfo.OpenApplicationArray.Length;

           // LogWritePlayBackEvents("");
           // LogWritePlayBackEvents("App # " + NumberOfAppsOpen.ToString());
           // LogWritePlayBackEvents("");

           // for (int I = 0; I < NumberOfAppsOpen; I++)
           // {
           //     LogWritePlayBackEvents("");
           //     LogWritePlayBackEvents("Process: " + NewWindowInfo.OpenApplicationArray[I].ProcessName);
           //     LogWritePlayBackEvents("");

           //    if (NewWindowInfo.OpenApplicationArray[I].ProcessName == "iexplore")
           //   {    
           //         IEBrowserFound = true;
           //         break;
           //     }
           // }

            IEBrowserFound = NewWindowInfo.CheckIfIEBrowserIsRunning();

            // if IE Browser Exists
            if (IEBrowserFound)
            {
                IEFunctional.RefreshIEBrowserList();

                // To do..
                // IEFunctinal.SetBrowser(1)
                IEFunctional.HocktoBrowserEventsByIENumber(1);    
            }

            Thread.Sleep(300);

            acthook.Start();

            acthook.MouseMoved += new MouseEventHandler(acthook_MouseMoved);

            acthook.MouseUp +=new MouseEventHandler(acthook_MouseUp_ShowChildrenInfo);
            acthook.MouseUp += new MouseEventHandler(acthook_MouseUp_ReflectionCall);
            acthook.MouseUp += new MouseEventHandler(acthook_MouseUp);
            acthook.MouseUp += new MouseEventHandler(acthook_MouseUp_TurnIEHookingOff);

            if (e.Button == MouseButtons.Left)
                this.CaptureMouse();
        }

        void acthook_MouseUp_ShowChildrenInfo(object sender, MouseEventArgs e)
        { 
            int HandleID = SpyWindowClass.CurrentWindowHandleID.ToInt32();
            ChildrenControlClass.Start_Looking_For_Controls(HandleID);
        }

        void acthook_MouseUp_TurnIEHookingOff(object sender, MouseEventArgs e)
        {
            IEFunctional.UnSubscribeEventsFromIE();
        }

        void acthook_MouseUp_ReflectionCall(object sender, MouseEventArgs e)
        {
             // LogWritePlayBackEvents("");
             // LogWritePlayBackEvents("Reflection.. Get All Control Properties");

            if (GlobalClassNameForSpyClass == "Internet Explorer_Server")
             { 
               Reflection_RelayControlInfoBackToMainEvent(wecontrolProperties);
             }
             else
             {
                 IntPtr HandleID = SpyWindowClass.CurrentWindowHandleID; 
              Reflection.UseReflectionOnControl(HandleID); 
             }
        }

        // TODO: Create and place in new CLASS: SpyMouseMovedClass
        void acthook_MouseMoved(object sender, MouseEventArgs e)
        {
            // This will look like the following soon ...
            
            IntPtr hWnd = Win32.WindowFromPoint(Cursor.Position);

            int CursorPositionX = Cursor.Position.X;
            int CursorPositionY = Cursor.Position.Y;

            CurrentCursorPosition.Text = "Current Cursor Position: " + string.Format("{0},{1}", CursorPositionX.ToString(), CursorPositionY.ToString());
            
            SpyWindowClass.SetUpSpyInformation(hWnd);

            string TrueParentWindow = NewWindowInfo.Return_True_Parent_Window(CursorPositionX, CursorPositionY);
            GlobalTrueParentWindow = TrueParentWindow;

            // (Within Main_Load subscript to the following events)
            // SpyClass.getClassNameEvent += new GetClassNameDelegate(ClassNameMethod);

            // New Method within MainForm.cs
            // void ClassNameMethod(string ClassName)
            // {
            //    GlobalClassName = ClassName;
            // }

            try
            {
                // capture the window under the cursor's position
                //IntPtr hWnd = Win32.WindowFromPoint(Cursor.Position);

                // if the window we're over, is not the same as the one before, and we had one before, refresh it
                if (_hPreviousWindow != IntPtr.Zero && _hPreviousWindow != hWnd)
                    WindowHighlighter.Refresh(_hPreviousWindow);

                // if we didn't find a window.. that's pretty hard to imagine.
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
                    _hPreviousWindow = hWnd;

                    // handle
                    HandleIDlabel.Text = "HandleID: " + hWnd.ToInt32().ToString();

                    // class
                    ClassLabel.Text = "Class: " + GlobalClassNameForSpyClass; 

                    // caption
                    CaptionLabel.Text = "Caption: " + GlobalWindowTextForSpyClass;
                    Name_textBox.Visible = true;
                    Name_textBox.Text = GlobalWindowTextForSpyClass;

                    //internal name. The real name defined withint the parent application
                   // ApplicatioDefinedName.Text = "Internal Name: " + NewWindowInfo.ReturnInternalName(hWnd);

                    // True Control Parent Window 
                    ParentWindow.Text = "Parent Window: ";
                    ParentWindowTextBox.Text = TrueParentWindow;
                    // Parent Window to that control
                    Parent.Text = "Parent: " + GlobalParentWindowTextForSpyClass;

                    ////// THIS IS A TEST ////////////////////
                  //  LogWritePlayBackEvents("");
                  //  LogWritePlayBackEvents("Reflection.. Get All Control Properties");
                  //  Reflection.UseReflectionOnControl(hWnd);
                  //  LogWritePlayBackEvents("");
                  //  LogWritePlayBackEvents("TEST ONLY --- NEXT CONTROL!!!!!");
                  //  LogWritePlayBackEvents("");

                    /////////////////////////////////////////

                    // Position Based on Handle ID
                    Win32.Rect rc = new Win32.Rect();
                    Win32.GetWindowRect(hWnd, ref rc);

                    // rect
                    RectLabel.Text = "Rect: " + string.Format("[{0} x {1}]", rc.right - rc.left, rc.bottom - rc.top);
                   
                    //({2},{3})-({4},{5}) left , top , right , bottom ", rc.right - rc.left, rc.bottom - rc.top, rc.left, rc.top, rc.right, rc.bottom);
                   
                    TopLeftPosition.Text = "Top and Left Position: " + string.Format("{0} , {1}", rc.top, rc.left);
                    RightBottomPosition.Text = "Right and Bottom Position: " + string.Format("{0} , {1}", rc.right, rc.bottom);

                    //string test = NewWindowInfo.Return_True_Parent_Window(1, 1);
                    
                    GlobalTopPosition = rc.top;
                    GlobalBottomPosition = rc.bottom;

                    GlobalLeftPosition = rc.left;
                    GlobalRightPosition = rc.right;

                    // highlight the window
                    WindowHighlighter.Highlight(hWnd);

                    GlobalHandleID = hWnd;
                }
            }

            catch (Exception ex)
            {
               // Debug.WriteLine(ex);
            }
        }

        void acthook_MouseUp(object sender, MouseEventArgs e)
        {
            acthook.Stop();
            _pictureBox.Image = Automation.Properties.Resources.FinderHome;
            WindowHighlighter.Refresh(GlobalHandleID);

            //List<ControlPropertiesClass> info = IE_EventHook.ReturnActiveObject();
            //LogWritePlayBackEvents("TESTER!!: " + info);
            //LogWritePlayBackEvents("");

            //Reflection_RelayControlInfoBackToMainEvent(wecontrolProperties);
        }

        private void CaptureMouse()
        {
                // capture the mouse movements and send them to ourself
                Win32.SetCapture(this.Handle);

                // set the mouse cursor to our finder cursor
                //Cursor.Current = _cursorFinder;
                Cursor.Current = new Cursor("Finder.cur");

                // change the image to the finder gone image
                _pictureBox.Image = Automation.Properties.Resources.FinderGone;
        }

        void NewWindowInfo_WriteNewWindowInfoToXMLEvent(string CurrentActiveWindow, int Top, int Left)
        {
            XmlWrite.writeWindowCaption_To_XML(CurrentActiveWindow,Top,Left, GlobalPlayBackFileName);
        }

        void PlayBackResolutionRead_LogWritePlayBack(string Message)
        {
            LogWritePlayBackEvents(Message);
        }

        void Playback_CompleteProcessBar()
        {
            xpProgressBar1.Text = "     Completed: " + GlobalNodePositionIndex.ToString() + " / " + GlobalTotalInitialNodeCount.ToString() + " Tasks";

            if (GlobalNodePositionIndex < GlobalTotalInitialNodeCount)
            {
                // Change Color of Progress Bar to Red ....

                xpProgressBar1.ColorBarCenter = Color.Red;
                xpProgressBar1.Text = "     Completed with Failures: " + GlobalNodePositionIndex.ToString() + " / " + GlobalTotalInitialNodeCount.ToString() + " Tasks";
            }
            else
            {
                //  xpProgressBar1.Text = "     Successfully Completed: " + GlobalNodePositionIndex.ToString() + " / " + GlobalTotalInitialNodeCount.ToString() + " Tasks";
                xpProgressBar1.Text = "     Successfully Completed ";
            }
         }

        void Playback_UpdateProgressBar()
        {
            GlobalNodePositionIndex++;
           
            Application.DoEvents();

            xpProgressBar1.Text = "     Total Nodes Read: " + GlobalNodePositionIndex.ToString() + " / " + GlobalTotalInitialNodeCount.ToString();
            xpProgressBar1.Position++;
        }

        void Playback_InitializeProgressBar(int max_value)
        {
            GlobalNodePositionIndex = 0;
            GlobalTotalInitialNodeCount = max_value;

            xpProgressBar1.Position = 0;
            xpProgressBar1.PositionMax = max_value;

            xpProgressBar1.ColorBarCenter = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(10)))));
        }

        void KeyBoardEvents_RemoveData(int current_index, int place)
        {
            data.Remove(current_index, 1);
        }

        void MouseEvents_SendClickEventToMain()
        {
            KeyBoardEvents.ResetUserData();
        }

        StringBuilder KeyBoardEvents_GetData()
        {
            return data;
        }

        void ClickButtonHandle_UpdateData(string userdata)
        {
            if (userdata == "")
            {
                data.Remove(0, data.Length);
            }
            else
            {
                data.Append(userdata);
            }
        }

        StringBuilder ClickButtonHandle_GetDataString()
        {
            return data;
        }

        void KeyBoardEvents_UpdateData(string userdata)
        {
            if (userdata == "")
            {
                data.Remove(0, data.Length);
            }
            else
            {
                data.Append(userdata);
            }
        }

        void MonitorActiveWindowTimer_Tick(object sender, EventArgs e)
        {
            // USED FOR THE UP AND COMING Timer --> MonitorActiveWindowTimer
            // Timer isn't implemented yet. 
            // Use this method when Timer Triggers!!!!

            string CurrentActiveWindow = NewWindowInfo.ReturnActiveWindow();

             if (CurrentActiveWindow == LastReadTitleFromXML)
             {
                CorrectWindowActiveState = true;
                LogWritePlayBackEvents("Active Window Check: " + CurrentActiveWindow);
             }
             else
             {
                HaltReadingXMLValue = true;
                MonitorActiveWindowTimer.Enabled = false;
                CorrectWindowActiveState = false;
                LogWritePlayBackEvents("UNEXPECTED Window Has Appeared: " + CurrentActiveWindow);
                LogWritePlayBackEvents("Stopping Test!!!");
                LogWritePlayBackEvents("This is where I would fire of a user defined actions...");
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("I EXPECTED the following window: " + LastReadTitleFromXML);
             }
        }

        void XMLViewer_MoveToListinBoxLine(int ln)
        {
            RecorderedListBox.SetSelected(ln - 1, true);
        }

        void XMLViewer_SetSelectedListBox()
        {
            RecorderedListBox.SetSelected(1, true);
        }

        void XMLViewer_AddtoListBox(string addition)
        {
            RecorderedListBox.Items.Add(addition);
        }

        string XMLViewer_GetLatestXMLFile()
        {
            return GlobalPlayBackFileName;
        }

        void XMLViewer_PopulateTreeView()
        {
            //MyDelegate dlg_obj;
            //dlg_obj = new MyDelegate(XMLViewer.ParseFile);
            //RecorderedViewer.Invoke(dlg_obj); 
        }

        TreeNode XMLViewer_ReturnTreeViewRootNode(string readerName)
        {
            TreeNode RootNode = this.RecorderedViewer.Nodes.Add(readerName);
            return RootNode;
        }

        void XMLViewer_ListBoxControl(string control)
        {
            if (control == "clear")
            RecorderedListBox.Items.Clear();
        }

        void XMLViewer_TreeviewControl(string control, ImageList TreeImage)
        {
            if (control == "clear")
                RecorderedViewer.Nodes.Clear();

            if (TreeImage != null)
                RecorderedViewer.ImageList = TreeImage;
        }

        void MouseEvents_Send_CheckifControlMessage(IntPtr hWnd, string CurrentClassName, string ButtonUsed, string ButtonX, string ButtonY, string ParentWindowTitle, string ClickCount, int WindowPositionLeft, int WindowPositionTop)
        {
            // Checks to see if the Clicked Control is a BUTTON --> In the future, check to see if this is a "Control" -- Change to ClickControlHandle. This is more generic
            // ////////////////////////////////////////////////
            ClickButtonHandle.CheckIfControlIsAButtonThenWriteXML(hWnd, CurrentClassName, ButtonUsed, ButtonX, ButtonY, ParentWindowTitle, ClickCount, WindowPositionLeft, WindowPositionTop);   
        }
        
        void ClickButtonHandle_WriteButtonClickToXML(string CurrentCaptionstring, string ClickCountType, string ButtonUsed, string ButtonX, string ButtonY, string ParentWindowTitle, int WindowPositionLeft, int WindowPositionTop)
        {
            // Writing BUTTON CLICK to XML --> Modify Later: Change to When Control is Clicked  
            // This is called when a Button Click is Detected ..
            ///////////////////////////////////////////////////////

            if (ClickCountType == "Single")
            {
                // Revamping Saving Button Click...
                XmlWrite.writeButtonSingleClickToXml(CurrentCaptionstring, ButtonUsed, ButtonX, ButtonY, GlobalPlayBackFileName);
                //XmlWrite.writeButtonSingleClickToXML(ButtonUsed, ButtonX, ButtonY, ParentWindowTitle, window_position_top, window_position_left);
            }
            else // ClickCountType is Double 
            {
               
                //XmlWrite.writeButtonDoubleClickToXML(ButtonUsed, ButtonX, ButtonY, ParentWindowTitle, window_position_top, window_position_left);
            }
        }
       
        void ClickButtonHandle_WriteMouseClickToXML(string ButtonUsed, string ClickCount, string ButtonX, string ButtonY, string ParentWindowTitle, int window_position_top, int window_position_left)
        {
            // Writing MOUSE CLICK to XML 
            // This is called when a Normal Mouse Click is Detected ..
            ///////////////////////////////////////////////////////

            if (ClickCount == "Single")
            {
                XmlWrite.writeMouseClick_To_XML(ButtonUsed, ButtonX, ButtonY, GlobalPlayBackFileName);

                // Revamping Saving Mouse Click...

                //XmlWrite.writeMouseSingleClickToXML(ButtonUsed, ButtonX, ButtonY, ParentWindowTitle, window_position_top, window_position_left);
            }
            else // ClickCount is Double 
            {
                XmlWrite.writeMouseDoubleClick_To_XML(ButtonUsed, ButtonX, ButtonY, GlobalPlayBackFileName);
                //XmlWrite.writeMouseDoubleClickToXML(ButtonUsed, ButtonX, ButtonY, ParentWindowTitle, window_position_top, window_position_left);
            }
        }

        void MouseEvents_WriteMouseUPToXML(string buttontype, string x, string y)
        {
           // Writing MOUSE UP to XML
           ////////////////////////// 

            XmlWrite.writeMouseUPEvent_To_XML(buttontype, x, y, GlobalPlayBackFileName);

            //if(DisplayXMLUpdates)
            //{
                LogWriteUserEvents("Updated XML, UP Event: ");
            //}
        }

        void MouseEvents_WriteMouseDOWNToXML(string buttontype, string x, string y)
        {
          // Writing MOUSE DOWN to XML
          ////////////////////////////

            XmlWrite.writeMouseDOWNEvent_To_XML(buttontype, x, y, GlobalPlayBackFileName);

            //if(DisplayXMLUpdates)
            //{
                LogWriteUserEvents("Updated XML, DOWN Event: ");
            //}
        }

        void KeyBoardEvents_SendKeyBoardMessage(string Message)
        {
            DateTime dt = DateTime.Now;

            if (FirstTimeStartingApplication2)
            {
                LogWriteUserEvents("");
                FirstTimeStartingApplication2 = false;
            }

            LogWriteUserEvents(dt + " KeyBoardMessage: " + Message);
        }

        void SetDateFormat(string format)
        {
            // allowed format values:
            // ----------------------
            // US     --> "en-US";
            // Japan  --> "ja-JP";
            // FRENCH --> "fr-FR"

            Thread.CurrentThread.CurrentCulture = new CultureInfo(format);
        }

        void NewWindowInfo_SendWindowMessage(string WindowTitle)
        {
            DateTime dt = DateTime.Now;

            if (FirstTimeStartingApplication1)
            {
                LogWriteWindowEvent("");
                FirstTimeStartingApplication1 = false;
            }   
            LogWriteWindowEvent(dt + " Active Window: " + WindowTitle);
        }

        void MouseEvents_TurnTimerOnEvent()
        {
            theDoubleClickTimer.Enabled = true;
        }

        void MouseEvents_TurnTimerOffEvent()
        {
            theDoubleClickTimer.Enabled = false;
        }

        void MouseEvents_SendUserMessage(string Message)
        {
            DateTime dt = DateTime.Now;

            if (FirstTimeStartingApplication2)
            {
                LogWriteUserEvents("");
                FirstTimeStartingApplication2 = false;
            }
            LogWriteUserEvents(dt + " " + Message);
        }

        void PlayBackEvents_SendUserMessage(string Message)
        {
            DateTime dt = DateTime.Now;

           // if (FirstTimeStartingApplication3)
           // {
           //     LogWritePlayBackEvents("");
           //     FirstTimeStartingApplication3 = false;
           // }

            if (Message != "")
            {
                LogWritePlayBackEvents(dt + " " + Message);
            }
            else
            {
                LogWritePlayBackEvents(Message);
            }
        }

        private void LogWriteParseWeb(string txt)
        {
            IEParseWeb.AppendText(txt + Environment.NewLine);
            IEParseWeb.SelectionStart = IEParseWeb.Text.Length;
        }

        private void LogWriteUserEvents(string txt)
        {
            UserEvents.AppendText(txt + Environment.NewLine);
            UserEvents.SelectionStart = UserEvents.Text.Length;
        }

        private void LogWritePlayBackEvents(string txt)
        {
            PlayBackTextBox.AppendText(txt + Environment.NewLine);
            PlayBackTextBox.SelectionStart = PlayBack.Text.Length;
        }

        private void LogWriteWindowEvent(string WindowTitle)
        {
            WindowEvents.AppendText(WindowTitle + Environment.NewLine);
            WindowEvents.SelectionStart = WindowEvents.Text.Length;
        }

        private void CreateRecordGroupButton1_Click(object sender, EventArgs e)
        {
            //-> Initializing the XML FILE. 
            //////////////////////////////
            SaveFileDialogControl.SaveFileDialogBox("Create Play Back File", "PlayBack");

            if (GlobalPlayBackFileName != "")
            {
                kryptonPage1.TextTitle = "Record File Name: " + GlobalPlayBackFileName;
                XmlWrite.setup_XML(GlobalPlayBackFileName);
                WriteMonitorResolutions.WriteScreenResolutionToXML(GlobalPlayBackFileName);
            }
        }

        private void StartHocksGroupButton2_Click(object sender, EventArgs e)
        {
            acthook.Stop();
            NewWindowMonitorTimer.Enabled = false;
            Thread.Sleep(200);

            acthook.Start(); //-> Starting the Mouse and KeyBoard Hooks
            NewWindowMonitorTimer.Enabled = true;
        }

        private void StopHocksGroupButton3_Click(object sender, EventArgs e)
        {
            MonitorActiveWindowTimer.Enabled = false;
            acthook.Stop();
            NewWindowMonitorTimer.Enabled = false;
        }

        private void NewWindowMonitorTimer_Tick(object sender, EventArgs e)
        {
            NewWindowInfo.CheckForNewWindow();
        }

      //  void fileOpen_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
      //  {
      //      GlobalFileNameSetByUser = fileOpen.FileName;
      //  }

        private void LoadNewXMLButton_Click(object sender, EventArgs e)
        {
            OpenFileDialogControl.OpenFileDialogBox("Load PlayBack File", "PlayBack");

          //  fileOpen.Title = "TITLE GOES HERE!";
          //  fileOpen.InitialDirectory = ".\\";
          //  fileOpen.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";

          //  fileOpen.FilterIndex = 0;
          //  fileOpen.RestoreDirectory = false;
           

          //  fileOpen.FileOk += new System.ComponentModel.CancelEventHandler(fileOpen_FileOk);
          //  fileOpen.ShowDialog(); 
           // GlobalFileNameSetByUser = Get_GlobalFileNameSetByUser();

            if (GlobalPlayBackFileName != "")
            {
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("Opened File for PlayBack");
                LogWritePlayBackEvents("----------------------------------");
                LogWritePlayBackEvents(GlobalPlayBackFileName);
                LogWritePlayBackEvents("");
               
                LoadNewXML(GlobalPlayBackFileName);
                GlobalAreObjectivesBeingUsedValue = ValidateXML.IsThereAnObjectiveBeingUsedWithinXML(GlobalPlayBackFileName);
                LogWritePlayBackEvents("Objective Being Used: " + GlobalAreObjectivesBeingUsedValue.ToString());
                LogWritePlayBackEvents("");
            }
            // string WorkingDir = Directory.GetCurrentDirectory();
           // XMLViewer.SETUP_PROCEDURE(WorkingDir, GlobalRecordFileName);
        }

        private void LoadNewXML(string RecordFileName)
        {
            string WorkingDir = Directory.GetCurrentDirectory();
            XMLViewer.SETUP_PROCEDURE(WorkingDir, RecordFileName);      
        }

        private void DefineNewXML_For_SeperateXMLPlayBack(string XMLFileName)
        {
            GlobalPlayBackFileName = XMLFileName;
        }

        private void RefreshXMLButton_Click(object sender, EventArgs e)
        {

        }

        private void RecorderedViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            XMLViewer.RecorderedViewer_AfterSelect(sender,e);
        }

        private void StartPlayBack_Click(object sender, EventArgs e)
        {
            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("Global FileName Set to: " + GlobalPlayBackFileName);
            LogWritePlayBackEvents("");

            StartingPlayBack(GlobalAreObjectivesBeingUsedValue);
        }

        private void StartingPlayBack(bool UsingObjectives)
        {
          //  if (objectivecontrolfile != "")
          //      SetUpObjectiveControlList(objectivecontrolfile);

            HaltReadingXMLValue = false;
            bool ObjectControlFileIsSet = false;
            bool UseObjectiveControlPath = false;

            if (!FirstTimeStartingApplication3)
            {
                PlayBackTextBox.Clear();
            }

            xpProgressBar1.Position = 0;
            xpProgressBar1.Text = "Begining PlayBack ...";

            Application.DoEvents();

            Win32.CopyFile(GlobalObjectDefinitionFile, "TEMP", 0);

            if (GlobalObjectDefinitionFile != "")
            {
                //GlobalDefinitionXmlTextReader = new XmlTextReader(GlobalObjectDefinitionFile);
                GlobalDefinitionXmlTextReader = new XmlTextReader("TEMP");
                LogWritePlayBackEvents("Object Definition File set to: " + GlobalObjectDefinitionFile);
                LogWritePlayBackEvents("");
            }

            objDom.Load(GlobalPlayBackFileName);

            int ElementsStoredInObjectControlArrayList = ObjectDataInfo.ReturnNumberOfObjectivesInArray();

            LogWritePlayBackEvents("");
            LogWritePlayBackEvents("Elements Stored In Objective Control Array: " + ElementsStoredInObjectControlArrayList.ToString());
            LogWritePlayBackEvents("");

            if (ElementsStoredInObjectControlArrayList == 0)
            {
                ObjectControlFileIsSet = false;
            }
            else
            {
                ObjectControlFileIsSet = true;
            }

            if (UsingObjectives && ObjectControlFileIsSet)
                UseObjectiveControlPath = true;
            

            if (!UseObjectiveControlPath)
            {
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("Playing NOT Using Objective Loop Control!!!!");
                LogWritePlayBackEvents("");
                // StartPlayBackThread = new Thread(new ThreadStart(StartReadingXML));
                // StartPlayBackThread.SetApartmentState(ApartmentState.STA);
                // StartPlayBackThread.Start();
                Playback.StartReadingXML(objDom);
            }
            else
            {
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("Playing Back Using Objectives Loop Control");
                LogWritePlayBackEvents("");
                ReadObjectives(objDom);
            }
            // This is for clean up. This was used by objDom and Playback...
            objDom.RemoveAll();
            GlobalDefinitionXmlTextReader = null;
        }

        private void ReadObjectives(XmlDocument obj)
        {
            int maxindex = 0;

            string CurrentObjectiveName = "";

            int NumberOfTotalElementsStored = ObjectDataInfo.ReturnNumberOfObjectivesInArray();

           // int TotalNumberOfObjectives = NumberOfTotalElementsStored / 2;

            // Go Through List Of Stored Objectives set into Array

            for (int A = 0; A < NumberOfTotalElementsStored; A = A + 2)
            {
                FirstTimeInLoop = true;

                CurrentObjectiveName = ObjectDataInfo.ReturnObjectiveBasedOnIndex(A);
                maxindex = ObjectDataInfo.ReturnCountForObjective(CurrentObjectiveName);

                LogWritePlayBackEvents("");
                LogWritePlayBackEvents("Starting Objective: " + CurrentObjectiveName);
                LogWritePlayBackEvents("Loop Index Set to: " + maxindex.ToString());
                LogWritePlayBackEvents("");

                if (maxindex == 0)
                {
                    LogWritePlayBackEvents("Skipping Test: " + CurrentObjectiveName);
                    LogWritePlayBackEvents("");
                }

                for (int I = 0; I < maxindex; I++)
                {
                    if (I > 0)
                    {
                        if (I == 1)
                        {
                            LogWritePlayBackEvents("");
                            LogWritePlayBackEvents("First Loop..");
                        }

                        LogWritePlayBackEvents("Loop Index: " + I.ToString());
                    }

                    if (HaltReadingXMLValue)
                        break;

                    GlobalIndexLoop = I;
                    //  StartPlayBackThread = new Thread(new ThreadStart(StartReadingXMLWithObjectives));
                    //  StartPlayBackThread.SetApartmentState(ApartmentState.STA);
                    //  StartPlayBackThread.Start();
                    Playback.StartReadingXML(objDom, CurrentObjectiveName, FirstTimeInLoop);
                    FirstTimeInLoop = false;
                }
            }

            maxindex = 0;

            CurrentObjectiveName = "";

            NumberOfTotalElementsStored = 0;
        
        } // End of ReadObjectives


      //  private void StartReadingXML()
      //  {
      //      Playback.StartReadingXML(objDom);
      //  }

      //  private void StartReadingXMLWithObjectives()
      //  {
      //      Playback.StartReadingXML(objDom, CurrentObjectiveName, FirstTimeInLoop);
      //      FirstTimeInLoop = false;
      //  }

        private void CreateObjectButton_Click(object sender, EventArgs e)
        {
            //GlobalDefinitionXmlTextReader = new XmlTextReader(GlobalObjectDefinitionFile);
            //XmlWrite.SetUP_Definition(GlobalObjectDefinitionFile);

            SaveFileDialogControl.SaveFileDialogBox("Save Object File", "ObjectFile");

            if (GlobalObjectDefinitionFile != "")
            {
                XmlWrite.setup_XML(GlobalObjectDefinitionFile);
                SaveObjectFileMethod();
                ShowObjectFile();
            }
        }

        private void SaveObjectButton_Click(object sender, EventArgs e)
        {
            //Create a Definition File using XML
            //<Definition>
            //<parentwindow name="window1">
            //  <control name="control1">
            //      <positiontop>10</positiontop>
            //      <positionbottom>20</positionbottom>
            //      <positionleft>15</positionleft>
            //      <positionright>30</positionright>
            //  </control>
            //</parentwindow>
            //</Definition>

            //Assuming the Definition File Already exists
            //This can be setup via the menu.

            if (GlobalObjectDefinitionFile != "")
            {
                SaveObjectFileMethod();
                ShowObjectFile();
            }
            else // In case LOAD FILE WAS NOT PRESSED YET
            { 
               // Send Error Message.. Load Define New ObjectFile.. Then launch SaveObjectFileMethod
                SaveFileDialogControl.SaveFileDialogBox("New Object Definition File", "ObjectFile");
                if (GlobalObjectDefinitionFile != "")
                {
                    XmlWrite.setup_XML(GlobalObjectDefinitionFile);
                    SaveObjectFileMethod();
                    ShowObjectFile();
                }
            }
        }

        private void SaveObjectFileMethod()
        {
            string parent_window;

            //if (GlobalTrueParentWindow != "")
            //{
            //    parent_window = GlobalTrueParentWindow;
            //}
            //else
            //{
                parent_window = ParentWindowTextBox.Text;
            //}

            string control_name = Name_textBox.Text;

            int Top = 0;
            int Bottom = 0;
            int Left = 0;
            int Right = 0;

            int Length = NewWindowInfo.OpenApplicationArray.Length;

            for (int I = 0; I < Length; I++)
            {
                if (NewWindowInfo.OpenApplicationArray[I].WindowTitle == parent_window)
                {
                    Top = NewWindowInfo.OpenApplicationArray[I].Top;
                    Bottom = NewWindowInfo.OpenApplicationArray[I].Bottom;
                    Left = NewWindowInfo.OpenApplicationArray[I].Left;
                    Right = NewWindowInfo.OpenApplicationArray[I].Right;
                    break;
                }
            }

            if (Top == 0)
            {
                int HandleID = NewWindowInfo.GetWindowHandleID(parent_window);
                Win32.Rect rc = NewWindowInfo.ReturnWindowPositionInfo(HandleID);
                Top = rc.top;
                Bottom = rc.bottom;
                Left = rc.left;
                Right = rc.right;
            }

            XmlWrite.writeToDefinitionFile(GlobalObjectDefinitionFile, control_name, parent_window, Top.ToString(), Bottom.ToString(), Left.ToString(), Right.ToString(), GlobalTopPosition.ToString(), GlobalBottomPosition.ToString(), GlobalLeftPosition.ToString(), GlobalRightPosition.ToString());

            this.ObjectViewer.Nodes.Clear();
            //this.ObjectListBox.Text = "";     /// NO LONGER NEEDED TAKE OUT!!
            //this.ObjectListBox.Items.Clear(); // TAKE OUT

            //TreeViewSerializer serializer = new TreeViewSerializer();
            serializer.LoadXmlFileInTreeView(this.ObjectViewer, GlobalObjectDefinitionFile);
            //serializer.LoadXmlFileInListBox(this.ObjectListBox, GlobalObjectDefinitionFile); // TAKE OUT

            this.ObjectViewer.SelectedNode = this.ObjectViewer.Nodes[0];
            //this.RecorderedViewer.ExpandAll();
        }

        private void ShowObjectButton_Click(object sender, EventArgs e)
        {
            ShowObjectFile();
        }

        private void ShowObjectFile()
        {
            if (GlobalObjectDefinitionFile != "")
            {
                LogWritePlayBackEvents("Global Definition File is set to: " + GlobalObjectDefinitionFile);
                LogWritePlayBackEvents("");
                this.ObjectViewer.Nodes.Clear();
               // this.ObjectListBox.Text = "";   // TAKE OUT
               // this.ObjectListBox.Items.Clear(); // TAKE OUT
                //TreeViewSerializer serializer = new TreeViewSerializer();
                serializer.LoadXmlFileInTreeView(this.ObjectViewer, GlobalObjectDefinitionFile);
                //serializer.LoadXmlFileInListBox(this.ObjectListBox, GlobalObjectDefinitionFile);
            }
            else
            { 
                // Send Error Message.. Load or Create New Object File
            }
        
        }

        private void LoadObjectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialogControl.OpenFileDialogBox("Load Object File", "ObjectFile");
            LogWritePlayBackEvents("");
            //DefineObjectFile(GlobalObjectDefinitionFile);
            if (GlobalObjectDefinitionFile != "")
            ShowObjectFile();
        }


        private void RefreshIEButton_Click(object sender, EventArgs e)
        {
            IEFunctional.RefreshIEBrowserList();
        }

        

        void IE_EventHook_IE_ClickEventReadyForDisplay(string data, string ClientX, string ClientY)
        {
          LogWriteParseWeb("IE Mouse Info Event Detected: " + data);
          LogWriteParseWeb("X Position: " + ClientX);
          LogWriteParseWeb("Y Position: " + ClientY);
          LogWriteParseWeb("");
          LogWriteParseWeb("");

          if (data != "")
              XmlWrite.writeMouseClick_From_IE(data, Global_HTMLOBJECT_TYPE, ClientX, ClientY, GlobalPlayBackFileName);
          else
              XmlWrite.writeMouseClick_From_IE(ClientX, ClientY, GlobalPlayBackFileName);
          //  data_from_ie = data;
          //  data_type = type;

          // After this information is written to XML then set IEWindowActive to false;
        }

        private void SetBrowserNumber_Click(object sender, EventArgs e)
        {
            // Launch the --> BrowserNumberForm Window

            // TO DO: Create New Method within IEFunctionCommands...

            Browser_NumberForm IENumberWindow = new Browser_NumberForm();
            IENumberWindow.Show();
            IENumberWindow.BrowserNumber += new BrowserNumberDelegate(IENumberWindow_BrowserNumber);
            // if (IENumberWindowEventCount == 0)
            //{
                //-> Setting Browser Number. This subscribes to that event

            // COME BACK AND FIX THIS BUG!!!

      ////--> BUG!!!          IENumberWindow.BrowserNumber += new BrowserNumberDelegate(SetBrowserNumberMethod);
            //    IENumberWindowEventCount++;
            //}

            ///////////// PLACE IN IEFUNTIONAIL COMMANDS...
        }

        void IENumberWindow_BrowserNumber(int NumberofBrowsers)
        {
            SetBrowserNumberMethod(NumberofBrowsers);
        }

        private void ParseWebButton_Click(object sender, EventArgs e)
        {
            //IE_EventHook.New_IEWindowEvent();
            IEFunctional.ParseWebPage();
        }

        private void HTMLObjectSelector_Click(object sender, EventArgs e)
        {
            // TO DO:: CLEAN UP!!
            // TAKE THIS OUT.. NO LONGER NEEDED
           // HTMLObjectSelector HTMLObject = new HTMLObjectSelector();
           // HTMLObject.Show();
           // HTMLObject.SendHTMLObjectEvent +=new SendHTMLObjectDelegate(HTMLObject_SendHTMLObjectEvent);
           

           /// TAKE OUT ABOVE FIRST... TAKE OUT EACH METHOD AND WINDOW THAT IS NO LONGER USED..
           /// 
            // 
           // string DataValue = "InnerText";
           // string DataValue = "InnerHtml";
           // string DataValue = "HREF";
           // string DataValue = "NAME";
           // string DataValue = "outerHTML";
           // string DataValue = "ALT";
           // string DataValue = "tagName";
           // string DataValue = "USERDEFINED";

           // string DataValue = "OuterText";

           // IE_EventHook.IEdataselector = DataValue;

           // IE_EventHook.UserDefinedValue = USERDEFINEDVALUE_TEXTBOX.text;
        }

        void  HTMLObject_SendHTMLObjectEvent(string htmlobjecttype)
        {
           // IE_EventHook.IEdataselector = htmlobjecttype;
            IEFunctional.SetHTMLObject(htmlobjecttype);
            IEFunctional.HocktoBrowserEventsByIENumber(1);
            Global_HTMLOBJECT_TYPE = htmlobjecttype;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
          //  XmlSourceController.LoadXml("source.xml");
          //  int count = XmlSourceController.PopulateListOfNodes("*","name");
          //  string value = XmlSourceController.RetrieveInfoFromSourceXML("name",0);
            

          //  LogWriteUserEvents("");
          //  LogWriteUserEvents("found total count: " + count.ToString());
          // LogWriteUserEvents("first value: " + value);
          //  value = "";

          //  for (int I = 1; I < count; I++)
          //  {
          //      value = XmlSourceController.RetrieveInfoFromSourceXML("name", I);
          //      LogWriteUserEvents("next value: " + value);
          //      value = "";
          //  }
          //  XmlSourceController.clearlist();
        }

        private void kryptonRibbonGroupButton2_Click(object sender, EventArgs e)
        {
            IEFunctional.CaptureIEAsPicture();
            //IE_EventHook.Capture_Web_Page();

           // WebCapture WebCaptureWindow2 = new WebCapture();
           // WebCaptureWindow2.Show();
        }

        private void kryptonRibbonGroupButton3_Click(object sender, EventArgs e)
        {
            //TimeFormatWindow TimeFormat = new TimeFormatWindow();
            //TimeFormat.SendUpdateTimeFormatEvent += new SetTimeFormatDelegate(TimeFormat_SendUpdateTimeFormatEvent);
            TimeFormat.Show();
        }

        void TimeFormat_SendUpdateTimeFormatEvent(string Time)
        {
            SetDateFormat(Time);
        }

        void SetUpObjectiveControlList(string ObjectiveControlFile)
        { 
        //string ObjectiveControlFile = "objectcontroller.txt";

           LogWritePlayBackEvents("");
           LogWritePlayBackEvents("");
           LogWritePlayBackEvents("Reading Objective Control File: " + ObjectiveControlFile);

           ArrayList Objectives = ObjectiveControl.ReadFile(ObjectiveControlFile);

           int TotalElementsStored = Objectives.Count;
           int TotalObjectives = (int)TotalElementsStored / 2;

           LogWritePlayBackEvents("");

           LogWritePlayBackEvents("Total Objectives Found: " + TotalObjectives.ToString());
           LogWritePlayBackEvents("");

           ObjectDataInfo.ClearArray();

           for (int I = 0; I < TotalElementsStored; I=I+2)
           {
               string ObjectiveName = Objectives[I].ToString();
               string Counter = Objectives[I + 1].ToString();

               int CounterInt = Convert.ToInt16(Counter);

               LogWritePlayBackEvents("Storing ObjectiveName: " + ObjectiveName);
               LogWritePlayBackEvents("Loop Counter Set to: " + Counter);
               LogWritePlayBackEvents("");

               LogWritePlayBackEvents("Locating Objective: " + ObjectiveName + " Within File");
               if (ValidateXML.IsObjectiveWithinXML(GlobalPlayBackFileName, ObjectiveName))
               {
                   LogWritePlayBackEvents("Objective Found..");
                   LogWritePlayBackEvents("");
               }
               else
               {
                   LogWritePlayBackEvents("Objective Not Found..Reseting Counter to Zero");
                   LogWritePlayBackEvents("");
                   CounterInt = 0;
               }

               ObjectDataInfo.AddToArrayList(ObjectiveName, CounterInt);

           }

           Objectives.Clear();
 
        }

        private void kryptonRibbonGroupButton7_Click(object sender, EventArgs e)
        {
           //string ObjectiveControlFile = "objectcontroller.txt";
            OpenFileDialogControl.OpenFileDialogBox("Load Object Controller File", "ObjectiveController");

            if (GlobalObjectiveControllFile != "" && GlobalPlayBackFileName !="")
           SetUpObjectiveControlList(GlobalObjectiveControllFile);

            if (GlobalPlayBackFileName == "")
           LogWritePlayBackEvents("Global PlayBack is Not Set.. Object Control Not Loaded");
          

      //     LogWritePlayBackEvents("");
      //     LogWritePlayBackEvents("");
      //     LogWritePlayBackEvents("Reading Objective Control File: " + ObjectiveControlFile);

      //     ArrayList Objectives = ObjectiveControl.ReadFile(ObjectiveControlFile);

      //     int TotalElementsStored = Objectives.Count;
      //     int TotalObjectives = (int)TotalElementsStored / 2;

      //     LogWritePlayBackEvents("");

      //     LogWritePlayBackEvents("Total Objectives Found: " + TotalObjectives.ToString());
      //     LogWritePlayBackEvents("");

      //     ObjectDataInfo.ClearArray();

      //     for (int I = 0; I < TotalElementsStored; I=I+2)
      //     {
      //         string ObjectiveName = Objectives[I].ToString();
      //         string Counter = Objectives[I + 1].ToString();

      //         int CounterInt = Convert.ToInt16(Counter);

      //         ObjectDataInfo.AddToArrayList(ObjectiveName, CounterInt);

      //         LogWritePlayBackEvents("Storing ObjectiveName: " + ObjectiveName);
      //         LogWritePlayBackEvents("Loop Counter Set to: " + Counter);
      //        LogWritePlayBackEvents("");
      //     }

      //     Objectives.Clear();
 
        }

        private void RefreshPlayBackButton_Click(object sender, EventArgs e)
        {
            if (GlobalPlayBackFileName != "")
            {
                LoadNewXML(GlobalPlayBackFileName);
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents(GlobalPlayBackFileName + " Reloaded..");
            }
            else
            {
                // Send Error Message..Load New Playback..
            }
        }

        private void RefreshObjectFileButton_Click(object sender, EventArgs e)
        {
            if (GlobalObjectDefinitionFile != "")
            {
                LogWritePlayBackEvents("");
                LogWritePlayBackEvents(GlobalObjectDefinitionFile + " Reloaded..");
                ShowObjectFile();
            }
            else
            {
                // Send Error Message..Load New Object File
            }

        }

        private void RefreshObjectiveControlButton_Click(object sender, EventArgs e)
        {
            if (GlobalObjectiveControllFile != "")
                SetUpObjectiveControlList(GlobalObjectiveControllFile);
            else
            { 
                // Send Error Message.. Load New Objective Control File
            }
        }

        private void Clear_Record_List_Click(object sender, EventArgs e)
        {
            UserEvents.Clear();
            WindowEvents.Clear();
            
        }

        private void Clear_PlayBack_Button_Click(object sender, EventArgs e)
        {
            PlayBackTextBox.Clear(); 
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            
        }

        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TriggerPictureCapture.TakePictureOfActiveWindow(GlobalHandleID);
        }

        private void deskTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TriggerPictureCapture.TakePictureOfDeskTop();
        }
    }
}