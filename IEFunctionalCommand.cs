using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class IEFunctionalCommand
    {
        // Temp Values for the Compiler
        int IE_eventcounter = 0;
        string DataValue = "";
        string lastText = "";
        string currentText = "";
        int eventcounter = 0;
        string IEBrowserNumberString = "";
        // NOT CURRENT VALUES. Create Events to retrieve information...

        IEHandler IE_EventHook = new IEHandler();
        public event DisplayIEClickEventInfo ClickEventDetectedIE;

        public event ReplayWebControlPropertiesToMainViaIEFunctionalDelegate ReplayWebControlPropertiesEvent;

        public event RelayWebTableDataFromHTMLSearhResults webtable_results_event;

        public event LogWriteIEInfoDelegate LogWriteIEInfo;
        public event LogWriteIEWebParseDelegate LogWriteIEParse;

        public event DisplayWebCaptureImage TriggerDisplayOfWebPicture;

        public event MoveXMLFileFromHTMLSearchResultDelegate MoveXMLSourceToMainEvent;

        public void SetUpDelegates()
        {                                                                   
            IE_EventHook.IE_BrowserInfoReadyForDisplay += new DisplayIEInfo(IE_EventHook_IE_BrowserInfoReadyForDisplay);
            IE_EventHook.IE_BrowserSpecificHookEvent += new DisplayIESelectedHook(IE_EventHook_IE_BrowserSpecificHookEvent);
            IE_EventHook.IE_Window_NO_FrameEvent += new IE_NonFrameWindowInfoReadyForDisplay(IE_EventHook_IE_Window_NO_FrameEvent);
            IE_EventHook.IE_Window_FRAMEEVEMT += new IE_FrameWindowInfoReadyForDisplay(IE_EventHook_IE_Window_FRAMEEVEMT);
            IE_EventHook.IE_DISCOVERED_FRAMES += new IE_FRAME_DETECTED(IE_EventHook_IE_DISCOVERED_FRAMES);
            IE_EventHook.IE_DISCOVERED_NONFRAME += new IE_NON_FRAME_DETECTED(IE_EventHook_IE_DISCOVERED_NONFRAME);
            IE_EventHook.WebCaptureReadForDiplay += new DisplayWebCaptureImage(IE_EventHook_WebCaptureReadForDiplay);
            IE_EventHook.RelayControlInfoBackToMainEvent += new ReplayWebControlPropertiesToMainViaIEFunctionalDelegate(IE_EventHook_RelayControlInfoBackToMainEvent);
            IE_EventHook.IE_ClickEventReadyForDisplay +=new DisplayIE_Event_ClickHandler(IE_EventHook_IE_ClickEventReadyForDisplay);
            IE_EventHook.LogWrite += new RelayLogWriteToFunctionalCommandDelegate(IE_EventHook_LogWrite);
            IE_EventHook.SubscribeToDelegate();
            IE_EventHook.MoveXMLToIEFunctional += new MoveXMLFileFromHTMLSearchResultDelegate(IE_EventHook_MoveXMLToIEFunctional);
            IE_EventHook.webtable_event += new RelayWebTableDataFromHTMLSearhResults(IE_EventHook_webtable_event);
        }

        void IE_EventHook_webtable_event(List<WebTableDataClass> webtableresults)
        {
            webtable_results_event(webtableresults);
        }

        void IE_EventHook_MoveXMLToIEFunctional(string xmlsource)
        {
            // Send xmlsource to MainClass 
            MoveXMLSourceToMainEvent(xmlsource);
        }

        void IE_EventHook_LogWrite(string message)
        {
            LogWriteIEInfo(message);
        }

        void IE_EventHook_RelayControlInfoBackToMainEvent(List<ControlPropertiesClass> properties_control)
        {
            ReplayWebControlPropertiesEvent(properties_control);
        }

        void IE_EventHook_WebCaptureReadForDiplay(string url)
        {
            TriggerDisplayOfWebPicture(url);
        }

        public void ParseWebPage(string outputxml)
        {
            IE_EventHook.ParseWebTable(outputxml);
        }

        public void SetHTMLObject(string htmlobject)
        {
            IE_EventHook.IEdataselector = htmlobject;
        }

        public void ParseWebPage()
        {
            IE_EventHook.New_IEWindowEvent();
        }

        public void IE_EventHook_IE_DISCOVERED_NONFRAME()
        {
            LogWriteIEParse("");
            LogWriteIEParse("DETECTED NON FRAME");
            LogWriteIEParse("-------------------");
            LogWriteIEParse("");
        }

        public void IE_EventHook_IE_DISCOVERED_FRAMES(int FrameLength)
        {
            LogWriteIEParse("");
            LogWriteIEParse("DETECTED FRAMES");
            LogWriteIEParse("Number of Frames within Browser: " + FrameLength.ToString());
            LogWriteIEParse("");
        }


        public void IE_EventHook_IE_Window_FRAMEEVEMT(string WindowFrameName, string FrameBody, IELinksClass links, int FrameNumber, string FrameURL)
        {
            LogWriteIEParse("FRAME NAME: " + WindowFrameName);
            LogWriteIEParse("FRAME NUMNER: " + FrameNumber);
            LogWriteIEParse("FRAME URL: " + FrameURL);
            LogWriteIEParse("FRAME BODY: " + FrameBody);

            LogWriteIEParse("");

            LogWriteIEParse("LINKS FROM FRAME " + FrameNumber);
            LogWriteIEParse("-------------------------");

            while (links != null)
            {
                LogWriteIEParse("Link: " + links.Val);
                links = (IELinksClass)links.NextItem;
            }
        }

        public void IE_EventHook_IE_Window_NO_FrameEvent(string HTMLBody, IELinksClass links)
        {
            LogWriteIEParse("---------");
            LogWriteIEParse("Body HTML");
            LogWriteIEParse("---------");
            LogWriteIEParse(HTMLBody);

            LogWriteIEParse("");
            LogWriteIEParse("LINKS");
            LogWriteIEParse("-----");

            while (links != null)
            {
                LogWriteIEParse("Link: " + links.Val);
                links = (IELinksClass)links.NextItem;
            }
        }

        public void IE_EventHook_IE_BrowserSpecificHookEvent(string LocationURL, int handleID, int browserNumber, string LocationName)
        {
             LogWriteIEInfo("------------------------");
             LogWriteIEInfo("Found Relavent Browser");
             LogWriteIEInfo("Browser Number: " + browserNumber);
             LogWriteIEInfo("Location Name: " + LocationName);
             LogWriteIEInfo("BrowserURL: " + LocationURL);
             LogWriteIEInfo("");
             LogWriteIEInfo("HandleID: " + handleID);
             LogWriteIEInfo("------------------------");
             LogWriteIEInfo("");
        }

        public void IE_EventHook_IE_BrowserInfoReadyForDisplay(string LocationURL, int handleID, int browserNumber, string LocationName)
        {
            //New Connection to Browser has been created

              LogWriteIEInfo("Located Open IE URL Address");
              LogWriteIEInfo("Browser Number: " + browserNumber);
              LogWriteIEInfo("LOCATION NAME: " + LocationName);
              LogWriteIEInfo("URL: " + LocationURL);
              LogWriteIEInfo("IE HandleID: " + handleID);
              LogWriteIEInfo("");
        }


        // TO DO: // CHANGE THE METHOD NAME TO IE_MouseMovedEventReadyForDisplay
        public void IE_EventHook_IE_ClickEventReadyForDisplay(string data, string ClientX, string ClientY)
        {
            // May Still use this method later... 
            // Note: This is not really showing the IE CLICK EVENT. This is current being used with
            // MOUSE MOVED...

           LogWriteIEInfo("IE Mouse Moved Event Detected: " + data);
            //LogWriteIEInfo("X Position: " + ClientX);
            //LogWriteIEInfo("Y Position: " + ClientY);
           LogWriteIEInfo("");
           LogWriteIEInfo("");

           //ClickEventDetectedIE(data, DataValue);
        }

        public void RefreshIEBrowserList()
        {
          LogWriteIEInfo("");
          LogWriteIEInfo("Refreshed IE URL List");
          LogWriteIEInfo("---------------------");
          IE_EventHook.DefineNewIE(true, false, 0, "N/A", "N/A");
        }

        public void DefineNewIEBrowser(int number)
        {
            LogWriteIEInfo("");
            LogWriteIEInfo("Defining New Browser by Number: " + number.ToString());
            LogWriteIEInfo("---------------------");
            IE_EventHook.DefineNewIE(true, true, number, "", "");
        }

        public void DefineNewIEBrowser(string url)
        {
            LogWriteIEInfo("");
            LogWriteIEInfo("Defining New Brower at url: " + url.ToString());
            LogWriteIEInfo("---------------------");
            IE_EventHook.DefineNewIE(true, true, 0, "", url);
        }

        public void DefineNewIEBrowser(object webpagename)
        {
            string webpage = webpagename.ToString();

            LogWriteIEInfo("");
            LogWriteIEInfo("Setting to Browser for webpage name: " + webpage);
            LogWriteIEInfo("---------------------");
            IE_EventHook.DefineNewIE(true, true, 0, webpage, "");
        }

        public void NavigateIEBrowser(string url)
        { 
        // Coming soon..
            LogWriteIEInfo("");
            LogWriteIEInfo("Navigating Browser to URL: " + url.ToString());
            LogWriteIEInfo("---------------------");
        // IE_EventHook.NavigateIEBrowser(url);
        // IE_EventHook.DocumentCompleteEvent += IE_BrowserDocumentComplete(....);
        }

        

        public void HocktoBrowserEventsByIENumber(int IEBrowserNumber)
        {
            //   int CurrentNumber = IEBrowserNumber;
            //   Global_Current_Browser_Number = CurrentNumber;

            //   if (CurrentNumber != Last_IE_Number)
            //  {
            //       IE_NumberValueChanged = true;
            //   }
            //   else
            //   {
            //       IE_NumberValueChanged = false;
            //   }

            //    Last_IE_Number = CurrentNumber;

            // Remove ALL prior onClick events
            //  if (IE_eventcounter > 0)
            //  {
            //      for (int I = 0; I < IE_eventcounter + 1; I++)
            //      {
            //          try
            //          {
            //            IE_EventHook.IE_ClickEventReadyForDisplay -= new DisplayIE_Event_ClickHandler(IE_EventHook_IE_ClickEventReadyForDisplay);
            //          // IE_EventHook.RelayControlInfoBackToMainEvent -= new RelayControlPropertiesInfoBackToMain(Reflection_RelayControlInfoBackToMainEvent);
            //          }
            //          catch
            //          {
            // Error Handling. Exception when an event hasn't been locked in (LockInEventsIEBrowser has not been called)
            //          }
            ///      }

            //      IE_eventcounter = 0;
            //   }

            IE_EventHook.DefineNewIE(true, true, IEBrowserNumber, "N/A", "N/A");
            IE_EventHook.LockinEventsIEBrowser(true);

            IE_EventHook.IE_ClickEventReadyForDisplay += new DisplayIE_Event_ClickHandler(IE_EventHook_IE_ClickEventReadyForDisplay);
            IE_eventcounter = IE_eventcounter + 1;
        }

        public void UnSubscribeEventsFromIE()
        {
            for (int I = 0; I < 3; I++)
            {
                try
                {
                    IE_EventHook.UnSubscribeIEMouseMoveEvents();
                    IE_EventHook.IE_ClickEventReadyForDisplay -= new DisplayIE_Event_ClickHandler(IE_EventHook_IE_ClickEventReadyForDisplay);
                }
                catch (Exception ex)
                {
                // this is used to case or error..
                }
            }
            IE_eventcounter = 0;
        }

        public void CaptureIEAsPicture()
        {
            LogWriteIEInfo("");
            LogWriteIEInfo("Capturing IE as JPEG");
            LogWriteIEInfo("------------------------");
            IE_EventHook.Capture_Web_Page();
        }
    }
}
