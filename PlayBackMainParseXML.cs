using System;
using System.Xml;
using System.Threading;

namespace Automation
{
    public class PlayBackMainParseXML
    {
        //This is the Main Play Back Controller Class

        static LoadPlugins LoadPlugin = new LoadPlugins();

        static MonitorConfigData MonitorConfiguration = new MonitorConfigData();

        public event Relay_ReadingNewXMLWithObjectivesDelegate RelayStartingPlayToMainForm;

        public event PushWindowTitleFromPlayBackMainDelegate PushWindowTitleToMainFormFromPlayBackMainViaPlayBackActionsEvent;

        public event HaltXMLReadingDelegate HaltReadingXMLEvent;

       // public event GetObjectDefinition GetObjectDefinitionFileName;
        public event SendMessageToMainPlayBackControl LogWritePlayBack;

        public event GetPlayBackObjectDefinitionReaderDelegate GetPlayBackObjectDefinitionReaderEvent;

        public event UpdateTotalNodeProgressBar UpdateProgressBar;
        public event SendCompleteMessageProgressBar CompleteProcessBar;
        public event InitializeTotalNodesProgressBar InitializeProgressBar;

        public event TriggerObjectiveResourceDelegate TriggerObjectiveResourceEvent;
        public event ReplaySourceXMLData ReplaySourceXMLData;

        public event ReplayUsingObjectiveBoolValueDelegate GetUsingObjectiveBoolValueEvent;

        WaitClass Wait = new WaitClass();

        ReadSeperateXMLFile ReadXMLFile = new ReadSeperateXMLFile();
        
        PlayBackActions PlayAction = new PlayBackActions();
        PlayBackResolutionReader ResolutionReader = new PlayBackResolutionReader();
        XmlSourceDataController XmlSourceData = new XmlSourceDataController();

        TriggerCaptureWindow TriggerCaptureWindow = new TriggerCaptureWindow();

        IEHandler IEhandler = new IEHandler();

        public event ReturnGlobalCounterDelegate GlobalCounterEvent;

        public event ReturnDLLDirectory ThePluginDirectory;

        string GlobalObjectiveName = "";

        int PluginReturnValue = 1;

        public void SetUpDelegateEvent()
        {
            PlayAction.LogWritePlayBack += new RelayPlayBackEventMessage(PlayAction_LogWritePlayBack);
            PlayAction.SetUpDelegateEvents();
            
            PlayAction.ReplayMonitorsTotal += new ReplayGlobalTotalNumberMonitors(PlayAction_ReplayMonitorsTotal);
            PlayAction.ReplayRecordedMonitorFound += new ReplayRecorderedMonitorFound(PlayAction_ReplayRecordedMonitorFound);
            PlayAction.ReplayRecordedMonitorData += new ReplayRecorderedMonitorData(PlayAction_ReplayRecordedMonitorData);
            PlayAction.ReplayCurrentMonitorData += new ReplayCurrentMonitorData(PlayAction_ReplayCurrentMonitorData);
            PlayAction.UpdateProgressBar += new ReplayProgressBar(PlayAction_UpdateProgressBar);

            PlayAction.GetPlayBlackDefinitionFromPlayMain += new GetReplayFromPlayBackMainDelegate(PlayAction_GetPlayBlackDefinitionFromPlayMain);
            PlayAction.RetrieveXMLSourceDataEvent += new RetrieveXMLSourceData(PlayAction_RetrieveXMLSourceDataEvent);
            PlayAction.GetCurrentIndexNumberEvent += new RetrieveGlobalIndexer(PlayAction_GetCurrentIndexNumberEvent);

            PlayAction.GetObjectiveNameEvent += new GetObjectiveName(PlayAction_GetObjectiveNameEvent);
            PlayAction.GetObjectiveBoolEvent += new GetObjectiveBoolValue(PlayAction_GetObjectiveBoolEvent);
            PlayAction.ReturnWindowTitleToPlayBackMainEvent += new ReplayWindowTitleToPlayBackMainDelegate(PlayAction_ReturnWindowTitleToPlayBackMainEvent);

            ResolutionReader.LogWritePlayBack += new SendMessageToMainPlayBackControl(ResolutionReader_LogWritePlayBack);
            ResolutionReader.GetCurrentMonitorDataEvent += new GetCurrentMonitorDataDelegate(ResolutionReader_GetCurrentMonitorDataEvent);
            ResolutionReader.SetCurrentMonitorDataEvent += new SetCurrentMonitorDataDelegate(ResolutionReader_SetCurrentMonitorDataEvent);
            ResolutionReader.SetRecorderedMoitorFoundEvent += new SetRecorderedMonitorFoundDelegate(ResolutionReader_SetRecorderedMoitorFoundEvent);
            ResolutionReader.SetRecorderedMonitorDataEvent += new SetRecorderedMonitorDataDelegate(ResolutionReader_SetRecorderedMonitorDataEvent);
            ResolutionReader.GetRecorderedMonitorDataEvent += new GetRecorderedMonitorDataDelegate(ResolutionReader_GetRecorderedMonitorDataEvent);
            ResolutionReader.AdjustRecordMonitorDataEvent += new AdjustRecordedMonitorDataDelegate(ResolutionReader_AdjustRecordMonitorDataEvent);
            ResolutionReader.SetTotalGlobalNumberMonitors += new SetGlobalTotalNumberMonitors(ResolutionReader_SetTotalGlobalNumberMonitors);

            ReadXMLFile.TriggerReadingNewXMLWithObjectives += new ReadingNewXMLWithObjectivesDelegate(ReadXMLFile_TriggerReadingNewXMLWithObjectives);
            ReadXMLFile.TriggerReadingNewXMLWITHOUTObjective += new ReadingNewXMLWithOUTObjectives(ReadXMLFile_TriggerReadingNewXMLWITHOUTObjective);

            LoadPlugin.SetUpDelegate();
            LoadPlugin.sendMessenger += new DisplayPlayBackEventMessage(LoadPlugin_sendMessenger);
        }

        void LoadPlugin_sendMessenger(string Message)
        {
            LogWritePlayBack(Message);
        }

        public void LogWriteMethod(string message)
        {
           
        }

        void ReadXMLFile_TriggerReadingNewXMLWITHOUTObjective(string XmlFile)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void ReadXMLFile_TriggerReadingNewXMLWithObjectives(string XmlFile, string ObjectiveControlFile, string objectfile)
        {
            // Event to Trigger Reading XML to Main Class
            RelayStartingPlayToMainForm(XmlFile, ObjectiveControlFile, objectfile);
        }

        void PlayAction_ReturnWindowTitleToPlayBackMainEvent(string windowtitle)
        {
            PushWindowTitleToMainFormFromPlayBackMainViaPlayBackActionsEvent(windowtitle);
        }

        int PlayAction_GetCurrentIndexNumberEvent()
        {
            return GlobalCounterEvent();
        }

        bool PlayAction_GetObjectiveBoolEvent()
        {
            return GetUsingObjectiveBoolValueEvent();
        }

        string PlayAction_GetObjectiveNameEvent()
        {
            return "";
        }

        string PlayAction_RetrieveXMLSourceDataEvent(int index, string data)
        {
            return ReplaySourceXMLData(index, data);
        }

        XmlTextReader PlayAction_GetPlayBlackDefinitionFromPlayMain()
        {
          return GetPlayBackObjectDefinitionReaderEvent();    
        }

        void PlayAction_UpdateProgressBar()
        {
            UpdateProgressBar();
        }

        public void StartReadingXML(XmlDocument obj)
        {
            XmlElement root = obj.DocumentElement;

            int TotalNumberofNodes = root.ChildNodes.Count;

            root.SelectSingleNode("//*");

            if (root.ChildNodes.Count > 0)
            {
                TotalNumberofNodes = TotalNumberofNodes - 1;
            }

            InitializeProgressBar(TotalNumberofNodes);

            LoopThroughChildren(root,false);

            CompleteProcessBar();
        }

        // This will be used soon!
        public void StartReadingXML(XmlDocument obj,string ObjectiveName, bool FirstTimeInLoop)
        {
            XmlElement root = obj.DocumentElement;

            int TotalNumberofNodes = root.ChildNodes.Count;

            if (root.ChildNodes.Count > 0)
            {
                TotalNumberofNodes = TotalNumberofNodes - 1;
            }

                InitializeProgressBar(TotalNumberofNodes);
                GlobalObjectiveName = ObjectiveName;

                LoopThroughChildren(root.SelectSingleNode("//Objective[@name='" + ObjectiveName + "']"), FirstTimeInLoop);
               
                CompleteProcessBar();
        }

        void PlayAction_LogWritePlayBack(string Message)
        {
            LogWritePlayBack(Message);
        }

        void ResolutionReader_LogWritePlayBack(string Message)
        {
            LogWritePlayBack(Message);
        }
        
        private void LoopThroughChildren(XmlNode root, bool FirstTimeInLoop)
        {
            //string lastchild_name = root.LastChild.Name;

            if (FirstTimeInLoop)
            {
                string name = "";
                string sourcexml = "";

                if (root.Attributes.Item(0).Name == "name")
                {
                    name = root.Attributes.Item(0).Value;
                    LogWritePlayBack("Using Objective: " + name);
                }

                try
                {
                    if (root.Attributes.Item(1).Name == "sourcexml")
                    {
                        sourcexml = root.Attributes.Item(1).Value;
                        LogWritePlayBack("XML DataSource Set to: " + sourcexml);
                        TriggerObjectiveResourceEvent(name, sourcexml);
                    }
                }
                catch
                {
                    LogWritePlayBack("No XML Source Defined.");
                    LogWritePlayBack("");
                }

            }
           
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (HaltReadingXMLEvent())
                        break;

                    if (!PlayAction.CorrectWindowActiveState)
                        break;

                     if (PluginReturnValue == 0)
                     {
                        LogWritePlayBack("Plugin Has Returned a Stop Message. Halting test");    
                         break;
                     }

                    switch (n.Name)
                    {
                        case "WAIT":
                            {
                                Wait.StartWaiting(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "XMLFILE":
                            {
                                ReadXMLFile.ReadXmlFile(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "CapturePicture":
                            {
                                TriggerCaptureWindow.CapturePicture(n);
                                UpdateProgressBar();
                                break;
                            }

                        //  case "CaptureActiveWindow":
                        //      {
                        // This captures The Active Window
                        //TriggerCaptureWindow.StartCapturingOfScreen(n);
                        //          TriggerCaptureWindow.StartCapturingOfActiveWindow(n);
                        //          break;
                        //      }

                        //case "CaptureDeskTopWindow":
                        //    {
                        //        // This captures the Desktop Window
                        //        TriggerCaptureWindow.StartCapturingOfDesktop(n);
                        //        break;
                        //    }

                        case "SourceXML":
                            {
                                LogWritePlayBack("Located XML Source");
                                LogWritePlayBack("");

                                string sourcexml = n.ChildNodes.Item(0).InnerText.ToString();
                                TriggerObjectiveResourceEvent(GlobalObjectiveName, sourcexml);

                                LogWritePlayBack("Source Name: " + sourcexml);
                                LogWritePlayBack("");
                                UpdateProgressBar();
                                break;
                            }

                        case "WindowScreenResolution":
                            {
                                ResolutionReader.GetOrignalResolutionWindowManger(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "windowEvent":
                            {
                                //  WindowEvent(n);
                                PlayAction.Playback_WindowEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "mouseUpEvent":
                            {
                                //  MouseUpEvent(n);
                                PlayAction.Playback_MouseUpEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "mouseDownEvent":
                            {
                                PlayAction.Playback_MouseDownEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "mouseClickEvent":
                            {
                                PlayAction.Playback_MouseClickEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "ButtonClickEvent":
                            {
                                PlayAction.Playback_MouseClickEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "mouseDoubleClickEvent":
                            {
                                PlayAction.Playback_MouseDoubleClickEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "keyboardEvent":
                            {
                                PlayAction.Playback_KeyBoardEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "keyboardComboEvent":
                            {
                                PlayAction.Playback_ComboKeyBoardEvent(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "EnterText":
                            {
                                PlayAction.EnterText(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "PressButton":
                            {
                                PlayAction.PressButton(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "SendClickToControl":
                            {
                                //<SendClickToControl parentwindow="test">CONTROL_NAME_FROM_OBJECT_FILE</SendClickToControl>
                                //<SendClickToControl>CONTROL_NAME_FROM_OBJECT</SendClickToControl>
                                PlayAction.SendClickToControl(n);
                                UpdateProgressBar();
                                break;
                            }

                        case "IEBrowerset":
                            {
                                // SetIEBrowser(n);
                                break;
                            }

                        case "IEClickLink":
                            {
                                // IEClickLink(n);
                                break;
                            }

                        case "IEClickButton":
                            {
                                // IEButtonClick(n);
                                break;
                            }

                        case "IETextEnter":
                            {
                                break;
                            }

                        case "IEParser":
                            {
                                break;
                            }

                        case "ParseWebTable":
                            {
                                PlayAction.ParseWebPage(n);
                                UpdateProgressBar();
                                break;
                            }

                        // LOOK INTO TAKING THIS OUT.. NO LONGER NEEDED.. DOUBLE CHECK!!
                        case "Objective":
                            {
                                // This IS come out!!!! ????
                                // COME BACK...

                                string name = "";
                                string sourcexml = "";

                                // get name
                                if (n.Attributes.Item(0).Name == "name")
                                {
                                    name = n.Attributes.Item(0).Value;
                                }

                                try
                                {
                                    if (n.Attributes.Item(1).Name == "sourcexml")
                                    {
                                        sourcexml = n.Attributes.Item(1).Value;
                                    }
                                }
                                catch
                                { }

                                int counter = GlobalCounterEvent();

                                if (counter == 0)
                                {
                                    // get xmlsource

                                    //  if (n.Attributes.Item(1).Name == "sourcexml")
                                    //  {
                                    //      sourcexml = n.Attributes.Item(1).Value;
                                    //  }

                                    LogWritePlayBack("Using Objective: " + name);
                                    LogWritePlayBack("XML DataSource Set to: " + sourcexml);
                                    LogWritePlayBack("Counter Set to: " + counter.ToString());
                                    TriggerObjectiveResourceEvent(name, sourcexml);
                                }
                                else
                                    LogWritePlayBack("Index Counter for Objective: " + name + " is set to: " + counter.ToString());

                                break;
                            }

                        default:
                            {
                                //CHECK IF PART OF SPECIAL TAGS....
                                // string The_DLL_DIRECTORY = Application.StartupPath + @"\Plugins";
                                string TEST_DIR = ThePluginDirectory() + @"\";

                                string attribute_name = "";
                                string attribute_value = "";
                                string inner_text = "";

                                // LogWritePlayBack("Checking for Plugins");

                                string PluginName = n.Name;

                                // LogWritePlayBack("Looking for " + PluginName);

                                if (LoadPlugin.CheckToSeeifPlugExists(TEST_DIR, PluginName))
                                {
                                    LogWritePlayBack("Plugin Being Used: " + PluginName);

                                    if (n.Attributes.Count > 0)
                                    {
                                        //attribute_name = n.Attributes.Item(1).Name;
                                        //attribute_value = n.Attributes.Item(1).Value;
                                        attribute_name = "ATTRIBUTE_NAME";
                                        attribute_value = "ATTRIBUTE_VALUE";
                                    }

                                    //inner_text = n.InnerText;
                                    inner_text = "INNER_TEXT";

                                    string ThePluginAuthor = LoadPlugin.PluginAuthor;
                                    string ThePluginDescription = LoadPlugin.PluginDescription;
                                    string ThePluginVersion = LoadPlugin.PluginVersion;

                                    LogWritePlayBack("The Plugin Autor: " + ThePluginAuthor);
                                    LogWritePlayBack("The Plugin Description: " + ThePluginDescription);
                                    LogWritePlayBack("The Plugin Version: " + ThePluginVersion);
                                    LogWritePlayBack("");
                                    LogWritePlayBack("Starting Plugin..");
                                    LogWritePlayBack("...");

                                    PluginReturnValue = LoadPlugin.start_plugin(PluginName, attribute_name, attribute_value, inner_text);
                                    LogWritePlayBack("VALUE FOR PLUGIN METHOD: " + PluginReturnValue.ToString());
                                    LogWritePlayBack("...");

                                    UpdateProgressBar();
                                }
                                break;
                            }

                    } // End of Switch Case Statement

                    LoopThroughChildren(n, false);

                } // End of For Each Statement
  
            // SendEvent to Turn MonitorActiveWindowTimer OFF

        } // End of Loop Through Children

        //---> Resolution Changes Handler and Target Window Move Auto Adjust

        #region ### Methods (Triggered by Events) to Read and Write to Monitor Data. Used for Resolution and Window Move Adjustment
        
        int PlayAction_ReplayCurrentMonitorData(int index, string position)
        {
            switch (position)
            {
                case "left":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Left;

                    }
                case "right":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Right;
                    }

                case "bottom":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Bottom;
                    }
                case "top":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Top;
                    }
                case "delta_width":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Delta_Width;
                    }
                case "delta_height":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Delta_Height;
                    }

                default:
                    {
                        return 9999; // Error occured
                    }
            }
        }

        int PlayAction_ReplayRecordedMonitorData(int index, string position)
        {
            switch (position)
            {
                case "left":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Left;

                    }
                case "right":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Right;
                    }

                case "bottom":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Bottom;
                    }
                case "top":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Top;
                    }
                default:
                    {
                        return 9999; // Error occured
                    }
            }
        }

        bool PlayAction_ReplayRecordedMonitorFound(int index)
        {
            return MonitorConfiguration.RecoderedMonitorSettings[index].Monitor_Found;
        }

        int PlayAction_ReplayMonitorsTotal()
        {
            return MonitorConfiguration.GlobalTotalNumberOfMonitors;
        }

        void ResolutionReader_SetTotalGlobalNumberMonitors(int Count)
        {
            MonitorConfiguration.GlobalTotalNumberOfMonitors = Count;
        }

        void ResolutionReader_AdjustRecordMonitorDataEvent(int Count, string position, int Length)
        {
            if (position == "left")
            {
                MonitorConfiguration.RecoderedMonitorSettings[Count].Position_Left = MonitorConfiguration.RecoderedMonitorSettings[Count - 1].Position_Left + Length;
            }

            if (position == "right")
            {
                MonitorConfiguration.RecoderedMonitorSettings[Count].Position_Right = MonitorConfiguration.RecoderedMonitorSettings[Count - 1].Position_Right + Length;
            }
        }

        int ResolutionReader_GetRecorderedMonitorDataEvent(int index, string position)
        {
            switch (position)
            {
                case "bottom":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Bottom;
                    }
                case "top":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Top;
                    }
                case "left":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Left;
                    }
                case "right":
                    {
                        return MonitorConfiguration.RecoderedMonitorSettings[index].Position_Right;
                    }
                default:
                    {
                        return 9999; //Error Occured. Position not known
                    }
            }
        }

        void ResolutionReader_SetRecorderedMonitorDataEvent(int index, string position, int value)
        {
            switch (position)
            {
                case "top":
                    {
                        MonitorConfiguration.RecoderedMonitorSettings[index].Position_Top = value;
                        break;
                    }
                case "bottom":
                    {
                        MonitorConfiguration.RecoderedMonitorSettings[index].Position_Bottom = value;
                        break;
                    }
                case "right":
                    {
                        MonitorConfiguration.RecoderedMonitorSettings[index].Position_Right = value;
                        break;
                    }
                case "left":
                    {
                        MonitorConfiguration.RecoderedMonitorSettings[index].Position_Left = value;
                        break;
                    }
            }
        }

        void ResolutionReader_SetRecorderedMoitorFoundEvent(int index, bool result)
        {
            MonitorConfiguration.RecoderedMonitorSettings[index].Monitor_Found = result;
        }

        void ResolutionReader_SetCurrentMonitorDataEvent(int index, string position, int value)
        {
            switch (position)
            {
                case "delta_height":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Delta_Height = value;
                        break;
                    }
                case "delta_width":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Delta_Width = value;
                        break;
                    }
                case "height":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Height = value;
                        break;
                    }
                case "width":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Width = value;
                        break;
                    }
                case "top":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Position_Top = value;
                        break;
                    }
                case "bottom":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Position_Bottom = value;
                        break;
                    }
                case "left":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Position_Left = value;
                        break;
                    }
                case "right":
                    {
                        MonitorConfiguration.CurrentMonitorSettings[index].Position_Right = value;
                        break;
                    }
            }
        }

        int ResolutionReader_GetCurrentMonitorDataEvent(int index, string position)
        {
            switch (position)
            {
                case "bottom":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Bottom;
                    }
                case "top":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Top;
                    }
                case "left":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Left;
                    }
                case "right":
                    {
                        return MonitorConfiguration.CurrentMonitorSettings[index].Position_Right;
                    }
                default:
                    {
                        return 9999; //Error Occured. Position not known
                    }
            }
        }

        #endregion
    }
}
