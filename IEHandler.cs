using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;
using AxSHDocVw;
using SHDocVw;
using mshtml;
using System.Text.RegularExpressions;

namespace Automation
{
    public class IEHandler
    {
        //IEBrowserInfo IEventWindow;

        CaptureWindowInfo WindowInfo = new CaptureWindowInfo();
        PlayBackEnterControl SendMessage = new PlayBackEnterControl();

        HTMLSearchResult htmlsearch = new HTMLSearchResult();

        public List<ControlPropertiesClass> webcontrolprop = new List<ControlPropertiesClass>();
        public event ReplayWebControlPropertiesToMainViaIEFunctionalDelegate RelayControlInfoBackToMainEvent;

        public event RelayWebTableDataFromHTMLSearhResults webtable_event;

        public event DisplayIE_Event_ClickHandler IE_ClickEventReadyForDisplay;
        public event DisplayIEInfo IE_BrowserInfoReadyForDisplay;
        public event DisplayIESelectedHook IE_BrowserSpecificHookEvent;

        public event IE_NonFrameWindowInfoReadyForDisplay IE_Window_NO_FrameEvent;
        public event IE_FrameWindowInfoReadyForDisplay IE_Window_FRAMEEVEMT;

        public event IE_FRAME_DETECTED IE_DISCOVERED_FRAMES;
        public event IE_NON_FRAME_DETECTED IE_DISCOVERED_NONFRAME;

     //--> NO LONGER USED..CLEAN UP   public event IE_ShowBasicInfo IE_HandleIDReadyForDisplay;

        public event DisplayWebCaptureImage WebCaptureReadForDiplay;

        public event RelayLogWriteToFunctionalCommandDelegate LogWrite;

        public event MoveXMLFileFromHTMLSearchResultDelegate MoveXMLToIEFunctional;
                     
        public string IEdataselector;
        public string UserDefinedValue;

        public string globaldata = "";
        public bool global_IEClickEventFlag;

        //private int completedDocuments;
        //private int expectedDocuments;

        //private IHTMLDocument2 myContentFrameDoc;

        //private HTMLDocument myDoc2;
        private HTMLDocument myDoc;
        private string linkstotext;

        private int count = 0;

        //private AxSHDocVw.AxWebBrowser m_browser;

        private SHDocVw.WebBrowser m_browser;

        System.Windows.Forms.WebBrowser WebBrowser1;
        
        //Coming Soon...
        //private "FIRE FOX DLL" fire_browser;

        private SHDocVw.ShellWindows allBrowsers = new SHDocVw.ShellWindowsClass();

        private mshtml.HTMLDocument IEObject;

        private mshtml.HTMLDocumentEvents2_Event iEvent;

        private mshtml.IHTMLElement framebody;
        private mshtml.IHTMLElementCollection framelinks;

        private mshtml.IHTMLWindow2 window;
        private mshtml.IHTMLDocument2 frameDocument;

        private mshtml.IHTMLDocument2 document;
        private mshtml.IHTMLDocument2 HTMLDocument;

        private mshtml.IHTMLElement Body;

        private mshtml.IHTMLElementCollection links;


      //  public event LogWriteIEInfoDelegate LogWrite;

        // This is used for the CAPTURE BROWSER HAS A JPEG //////////
        int GW_CHILD = 5;
        int GW_HWNDNEXT = 2;

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        private Image webpageImage;

        string current_globaldata = "";
        string previous_globaldata = "";

        //.//...///..////..////////......./////////.///////./././/././././

       // public List<ControlPropertiesClass> ReturnActiveObject()
       // {
       //     //return IEObject.activeElement.tagName;
       //    //return globaldata;
       //     return webcontrolprop;
       // }

        public void SubscribeToDelegate()
        {
            htmlsearch.SendWebTableXMLToMainForViewingInDataView += new MoveXMLFileFromHTMLSearchResultDelegate(htmlsearch_SendWebTableXMLToMainForViewingInDataView);
            htmlsearch.SendWebTableResults += new RelayWebTableDataFromHTMLSearhResults(htmlsearch_SendWebTableResults);
        }

        void htmlsearch_SendWebTableResults(List<WebTableDataClass> webtableresults)
        {
            webtable_event(webtableresults);
        }

        void htmlsearch_SendWebTableXMLToMainForViewingInDataView(string xmlsource)
        {
            // Replay xmlsource to IEFunctionCommands
            MoveXMLToIEFunctional(xmlsource);
        }

        public void Capture_Web_Page()
        {
            webpageImage = CaptureWebPage();
            //CaptureWebPage();
            //Get Time Stamp
            DateTime myTime = DateTime.Now;
            String format = "MM.dd.hh.mm.ss";

            //Create Directory to save image to.
            Directory.CreateDirectory("C:\\IECapture_TEST");

            //Write Image.
            EncoderParameters eps = new EncoderParameters(1);
            //long myQuality = Convert.ToInt64(qualityselect.Text);

            long myQuality = Convert.ToInt64(100);
            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, myQuality);
            ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

            string directory = "c:\\IECapture_TEST\\";

            string imagename = directory + "Captured_" + myTime.ToString(format) + ".jpg";

            webpageImage.Save(@"c:\\IECapture_TEST\Captured_" + myTime.ToString(format) + ".jpg", ici, eps);

            WebCaptureReadForDiplay(imagename);

            webpageImage.Dispose();
        }

        private Image CaptureWebPage()
        {
            bool UsingSpecialCase = false;
            
            //// Updating.. Creating new Method

        //    SHDocVw.WebBrowser m_browser = null;

        //    string filename;

        //        foreach (SHDocVw.WebBrowser ie in allBrowsers)
        //        {
        //            filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

        //            if (filename.Equals("iexplore"))
        //            {
        //                m_browser = ie;
        //                break;
        //            }
        //        }
        //        if (m_browser == null)
        //        {
                    // MessageBox.Show("No Browser Open");

        //        }

            // This is used to define --> myLocalLink later in the code..
                mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)m_browser.Document;

                /// End of New Function

                mshtml.IHTMLDocument2 frameDocument = (mshtml.IHTMLDocument2)m_browser.Document;

            //    frameDocument = document; // frameDocument and document are used seperately later on..
                //Set scrolling on.
                frameDocument.body.setAttribute("scroll", "yes", 0);

                //Get Browser Window Height
               // int heightsize = (int)frameDocument.body.getAttribute("scrollHeight", 0);
               // int widthsize = (int)frameDocument.body.getAttribute("scrollWidth", 0);

                int heightsize;
                int widthsize;

                try
                {
                    heightsize = (int)frameDocument.parentWindow.document.body.getAttribute("scrollHeight", 0);
                }
                catch
                {
                    heightsize = (int)frameDocument.body.getAttribute("scrollHeight", 0);
                }


                try
                {
                    widthsize = (int)frameDocument.parentWindow.document.body.getAttribute("scrollWidth", 0);
                }
                catch
                {
                    widthsize = (int)frameDocument.body.getAttribute("scrollWidth", 0);
                }

                LogWrite("Scroll Height: " + heightsize);
                LogWrite("Scroll Width: " + widthsize);
                LogWrite("");

                //Get Screen Height
           //  int screenHeight = (int)frameDocument.body.getAttribute("clientHeight", 0);
           //  int screenWidth = (int)frameDocument.body.getAttribute("clientWidth", 0);

                int screenHeight;
                int screenWidth;

                try
                {
                    screenHeight = (int)frameDocument.parentWindow.document.body.getAttribute("clientHeight", 0);
                }
                catch
                {
                    screenHeight = (int)frameDocument.body.getAttribute("clientHeight", 0);
                }

                try
                {
                    screenWidth = (int)frameDocument.parentWindow.document.body.getAttribute("clientWidth", 0);
                }
                catch
                {
                    screenWidth = (int)frameDocument.body.getAttribute("clientWidth", 0);
                }

                LogWrite("New Client Height: " + screenHeight.ToString());
                LogWrite("New Client Width: " + screenWidth.ToString());
                LogWrite("");

            //HERE///

                if (screenHeight == heightsize)
                {
                    UsingSpecialCase = true;
                    LogWrite("New Client Height and Scroll Height Size are the same. Using Special Case.");
                    LogWrite("");
                }

                IntPtr myIntptr = (IntPtr)m_browser.HWND;

                //Get inner browser window.
                //IntPtr hwnd = myIntptr;

                int hwndInt = myIntptr.ToInt32();
                IntPtr hwnd = myIntptr;
                hwnd = GetWindow(hwnd, GW_CHILD);
                StringBuilder sbc = new StringBuilder(256);
                Win32.GetClassName(hwnd, sbc, 256);

                // In IE6 and previous, the handle now points to a WorkerW window that is a 
                // sibling of the "Document" window (Shell DocObject View) and we can go find
                // it. 
                // In IE7, the handle now points at an intermediate layer at a sibling of the
                // TabWindowClass window which is the parent of the "Document" window. Loop
                // through these siblings to find that TabWindowClass and then drop down to
                // its children.

                if (sbc.ToString().IndexOf("WorkerW", 0) == -1) // IE 7
                {
                    while (hwnd != IntPtr.Zero)
                    {
                        Win32.GetClassName(hwnd, sbc, 256);
                        if (sbc.ToString().IndexOf("TabWindowClass", 0) > -1)
                        {
                            break;
                        }
                        hwnd = GetWindow(hwnd, GW_HWNDNEXT);
                    }
                    hwnd = GetWindow(hwnd, GW_CHILD);
                }

                //Get Browser "Document" Handle
                while (hwnd != IntPtr.Zero)
                {
                    Win32.GetClassName(hwnd, sbc, 256);
                    if (sbc.ToString().IndexOf("Shell DocObject View", 0) > -1)
                    {
                        hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "Internet Explorer_Server", IntPtr.Zero);
                        break;
                    }
                    hwnd = GetWindow(hwnd, GW_HWNDNEXT);
                }

                int ConvertedHandle = hwnd.ToInt32();

                Win32.Rect rec = WindowInfo.ReturnWindowPositionInfo(ConvertedHandle);
              
                int NewHeight = rec.Height;
                int NewWidth = rec.Width;

                screenHeight = NewHeight - 21; //Adjusting for scroll arrows
                screenWidth = NewWidth - 21;

                LogWrite("");
                LogWrite("IE Height: " + NewHeight);
                LogWrite("IE Width: " + NewWidth);
                LogWrite("");

                
            ///HERE///

             //   if (screenHeight > 311)
             //   {
                   // LogWrite("Auto Adjusting");
                   // LogWrite("");
                   // screenHeight = 311;
                   // screenWidth = 850;                             
                   // frameDocument.parentWindow.scrollBy(500, 500);
             //   }

                int URLExtraHeight = 0;
                int URLExtraLeft = 0;

                //string myLocalLink = myDoc.url;

                string myLocalLink = document.url;
                //Adjustment variable for capture size.

                //   if (writeURL.Checked == true)
                       URLExtraHeight = 25;

                //TrimHeight and TrimLeft trims off some captured IE graphics.
                int trimHeight = 3;
                int trimLeft = 4;

                //Use UrlExtra height to carry trimHeight.
                URLExtraHeight = URLExtraHeight - trimHeight;
                URLExtraLeft = URLExtraLeft - trimLeft;

                //Get bitmap to hold screen fragment.
                Bitmap bm = new Bitmap(screenWidth, screenHeight, PixelFormat.Format16bppRgb555);

                //Create a target bitmap to draw into.
                Bitmap bm2 = new Bitmap(widthsize + URLExtraLeft, heightsize +
                URLExtraHeight - trimHeight, PixelFormat.Format16bppRgb555);

                Graphics g2 = Graphics.FromImage(bm2);

                Graphics g;
                IntPtr hdc;
                Image screenfrag;
                int brwTop;
                int brwLeft;
                int myPage = 0;

       //         IntPtr myIntptr = (IntPtr)m_browser.HWND;

                //Get inner browser window.
                //IntPtr hwnd = myIntptr;

       //         int hwndInt = myIntptr.ToInt32();
       //         IntPtr hwnd = myIntptr;
       //         hwnd = GetWindow(hwnd, GW_CHILD);
       //         StringBuilder sbc = new StringBuilder(256);
       //         Win32.GetClassName(hwnd, sbc, 256);

                // In IE6 and previous, the handle now points to a WorkerW window that is a 
                // sibling of the "Document" window (Shell DocObject View) and we can go find
                // it. 
                // In IE7, the handle now points at an intermediate layer at a sibling of the
                // TabWindowClass window which is the parent of the "Document" window. Loop
                // through these siblings to find that TabWindowClass and then drop down to
                // its children.

        //        if (sbc.ToString().IndexOf("WorkerW", 0) == -1) // IE 7
        //        {
        //            while (hwnd != IntPtr.Zero)
        //            {
        //                Win32.GetClassName(hwnd, sbc, 256);
        //                if (sbc.ToString().IndexOf("TabWindowClass", 0) > -1)
        //                {
        //                    break;
        //                }
        //                hwnd = GetWindow(hwnd, GW_HWNDNEXT);
        //            }
        //            hwnd = GetWindow(hwnd, GW_CHILD);
        //        }

                //Get Browser "Document" Handle
        //        while (hwnd != IntPtr.Zero)
        //        {
        //            Win32.GetClassName(hwnd, sbc, 256);
        //            if (sbc.ToString().IndexOf("Shell DocObject View", 0) > -1)
        //            {
        //                hwnd = Win32.FindWindowEx(hwnd, IntPtr.Zero, "Internet Explorer_Server", IntPtr.Zero);
        //                break;
        //            }
        //            hwnd = GetWindow(hwnd, GW_HWNDNEXT);
        //        }

        //        int ConvertedHandle = hwnd.ToInt32();

        //     Win32.Rect rec =  WindowInfo.ReturnWindowPositionInfo(ConvertedHandle);
        //     int NewHeight = rec.Height;
        //     int NewWidth = rec.Width;

        //     LogWrite("");
        //     LogWrite("IE Height: " + NewHeight);
        //     LogWrite("IE Width: " + NewWidth);
        //     LogWrite("");

                //Get Screen Height (for bottom up screen drawing)

            while ((myPage * screenHeight) < heightsize)
                {
                    //frameDocument.body.setAttribute("scrollTop", (screenHeight - 5) * myPage, 0);
                    try
                    {
                        frameDocument.parentWindow.scrollBy(0, (screenHeight - 5) * myPage);
                    }
                    catch
                    {
                    // When a Can Not load message appears in the IE Browser the scrollBy won't work.
                    }

                    ++myPage;
                    LogWrite("Page Number: " + myPage.ToString());
                    LogWrite("");
                }

                //Rollback the page count by one
                --myPage;

                int myPageWidth = 0;

                brwTop = 0;
                brwLeft = 0;
                
             //   int HelloRob3 = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollTop", 0);

             //   LogWrite("HELLO ROB3333: " + HelloRob3.ToString());
             //   LogWrite("");

             //   int brwTop2 = (int)frameDocument.body.getAttribute("scrollTop", 0);
             //   LogWrite("brwTopFIRST: " + brwTop2.ToString());
             //   LogWrite("");

                while ((myPageWidth * screenWidth) < widthsize)
                {
             //       LogWrite("HELLO!!");
             //       LogWrite("");

                    Thread.Sleep(400);

                    //frameDocument.body.setAttribute("scrollLeft", (screenWidth - 4) * myPageWidth, 0);
                    try
                    {
                        frameDocument.parentWindow.scrollBy((screenWidth - 4) * myPageWidth, 0);
                    }
                    catch
                    { 
                        // Error Handler... Sometimes using the parentWindow can cause an error. 
                        // OR If IE Returns unable to Open IE Browser...

                        UsingSpecialCase = false;
                    }

                    //frameDocument.parentWindow.scroll((screenWidth - 4) * myPageWidth, 0);
                    //frameDocument.parentWindow.scrollTo((screenWidth - 4) * myPageWidth, 0);

                    Thread.Sleep(400);

                    if (myPageWidth > 0)
                    {
                        try
                        {
                            frameDocument.parentWindow.scrollTo((screenWidth - 4) * myPageWidth, 0);
                        }
                        catch
                        { 
                            // Error Handling
                            UsingSpecialCase = false;
                        }
                    }

                    if (UsingSpecialCase)
                    {
                        brwLeft = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollLeft", 0);   
                    }
                    else
                    {
                        brwLeft = (int)frameDocument.body.getAttribute("scrollLeft", 0);
                    }

                    //brwLeft = (int)frameDocument.body.getAttribute("scrollLeft", 0);

                   // LogWrite("OLD LEFT: " + brwLeft.ToString());
                   // LogWrite("");

                   // int LEFT = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollLeft", 0);

                   // LogWrite("NEW LEFT: " + LEFT.ToString());
                   // LogWrite("");
     
                   // int HEY = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollTop", 0);
                   // LogWrite("hi there: " + HEY.ToString());
                   // LogWrite("");

                    for (int i = myPage; i >= 0; --i)
                    {
                        LogWrite("LOOP PAGE: " + i.ToString());
                        LogWrite("");
                        //Shoot visible window
                        g = Graphics.FromImage(bm);
                        hdc = g.GetHdc();
                        //frameDocument.body.setAttribute("scrollTop", (screenHeight - 5) * i, 0);
                        //frameDocument.parentWindow.scrollBy(0, (screenHeight - 5) * i);
                        try
                        {
                            frameDocument.parentWindow.scroll(brwLeft, (screenHeight - 5) * i);
                        }
                        catch
                        { 
                            // Error Handling
                            UsingSpecialCase = false;
                        }

                        if (UsingSpecialCase)
                        {
                            brwTop = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollTop", 0);
                        }
                        else
                        {
                            brwTop = (int)frameDocument.body.getAttribute("scrollTop", 0);
                        }

                     //   brwTop = (int)frameDocument.body.getAttribute("scrollTop", 0);
                     //   LogWrite("brwTop: " + brwTop.ToString());
                     //   LogWrite("");

                     //   int HEY2 = (int)frameDocument.parentWindow.document.body.parentElement.getAttribute("scrollTop", 0);
                     //   LogWrite("hi there2: " + HEY2.ToString());
                     //   LogWrite("");
  
                        Win32.PrintWindow(hwnd, hdc, 0);
                        g.ReleaseHdc(hdc);
                        g.Flush();
                        screenfrag = Image.FromHbitmap(bm.GetHbitmap());

                        //Write URL
                        //    if (writeURL.Checked == true)
                        //    {   //Backfill URL paint location
                                SolidBrush whiteBrush = new SolidBrush(Color.White);
                                Rectangle fillRect = new Rectangle(0, 0, widthsize, URLExtraHeight + 2);
                                Region fillRegion = new Region(fillRect);
                                g2.FillRegion(whiteBrush, fillRegion);

                               SolidBrush drawBrushURL = new SolidBrush(Color.Black);
                                Font drawFont = new Font("Arial", 12);

                                StringFormat drawFormat = new StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.FitBlackBox;

                               g2.DrawString(myLocalLink, drawFont, drawBrushURL, 0, 0, drawFormat);
                        //    }

                        //Reduce Resolution Size
                        //double myResolution = Convert.ToDouble(capturesize.Text) * 0.01;

                        double myResolution = Convert.ToDouble(90) * 0.01;

                        int finalWidth = (int)((widthsize + URLExtraLeft) * myResolution);
                        int finalHeight = (int)((heightsize + URLExtraHeight) * myResolution);
                        Bitmap finalImage = new Bitmap(finalWidth, finalHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                        Graphics gFinal = Graphics.FromImage((Image)finalImage);
                        gFinal.DrawImage(bm2, 0, 0, finalWidth, finalHeight);

                        g2.DrawImage(screenfrag, brwLeft + URLExtraLeft, brwTop +
                        URLExtraHeight);
                    }
                    ++myPageWidth;
                }

                try
                {
                    frameDocument.parentWindow.scrollTo(0, 0);
                }
                catch
                { 
                    // Error Handling.. In Case no scroll bars are defined..
                }

                return bm2;
            }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        //././././././././../../.././././././././././././././.././././.././.

        /// <summary>
        ///  May take out bool NewEventHook. iEvent is defined but is not set to anything when NewEventHook is false
        /// This only works when it is true... Come back and think about clean up....
        /// CLEAN UP>>> Change to --> LockinEventsIEBrowser()
        /// </summary>
        /// <param name="NewEventHook"></param>
        public void LockinEventsIEBrowser(bool NewEventHook)
        {    
            //m_browser.DocumentComplete += new SHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(m_browser_DocumentComplete);
            
            IEObject = (mshtml.HTMLDocument)m_browser.Document;
           
            iEvent = (mshtml.HTMLDocumentEvents2_Event)m_browser.Document;

           // if (count > 0)
           // {    
           //     for (int I=0; I < count+1; I++)
           //     {
           //         try
           //         {
           //             iEvent.onclick -= new HTMLDocumentEvents2_onclickEventHandler(iEvent_onclick);
                       
           //         }
           ///         catch
           //         { 
           //         //This handles the exception where it doesn't have an onclick event to remove
           //         }
           //     }

           //     count = 0;
           // }


            /// TO DO:: MAY NOT NEED NewEventHook anymore..
            /// 
            if (NewEventHook)
            {
               // iEvent.onclick += new HTMLDocumentEvents2_onclickEventHandler(iEvent_onclick);
               // iEvent.onmouseover += new HTMLDocumentEvents2_onmouseoverEventHandler(iEvent_onmouseover);
                iEvent.onmousemove +=new HTMLDocumentEvents2_onmousemoveEventHandler(iEvent_onmouseover);
                iEvent.onmousemove += new HTMLDocumentEvents2_onmousemoveEventHandler(iEvent_onclick); // iEvent_onclick is a TEMP name.. TO DO: Change to Get HTMLOBJECT
               // iEvent.onmouseup +=new HTMLDocumentEvents2_onmouseupEventHandler(iEvent_onmouseover);
               // iEvent.onfocusin +=new HTMLDocumentEvents2_onfocusinEventHandler(iEvent_onmouseover);
                count = count + 1;
            }
        }

        public void UnSubscribeIEMouseMoveEvents()
        {
            for (int I = 0; I < 3; I++)
            {
                try
                {
                    iEvent.onmousemove -= new HTMLDocumentEvents2_onmousemoveEventHandler(iEvent_onclick);
                }
                catch (Exception ex)
                {
                    // In Case Nothing was subscribed.
                }
            }
        }

        void iEvent_onmouseover(IHTMLEventObj pEvtObj)
        {
            webcontrolprop.Clear();

            string name = "";
            string value = "";

          //  globaldata = pEvtObj.srcElement.tagName;

            name = "HRef";
            value = pEvtObj.srcElement.getAttribute("href", 0).ToString();
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            globaldata = value;
           
            name = "tagname";
            value = pEvtObj.srcElement.tagName;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "type";
            value = pEvtObj.srcElement.GetType().ToString();
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "id";
            value = pEvtObj.srcElement.id;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "innerHTML";
            value = pEvtObj.srcElement.innerHTML;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            Regex table_capital = new Regex("TABLE");

            //table_capital.IsMatch(value);
            //table_lowercase.IsMatch(value);

            if (table_capital.IsMatch(value))
            {
                htmlsearch.start_processing(value);
            }

            name = "innerText";
            value = pEvtObj.srcElement.innerText;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "outerHTML";
            value = pEvtObj.srcElement.outerHTML;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            if (table_capital.IsMatch(value))
            {
                htmlsearch.start_processing(value);
            }

            name = "outerText";
            value = pEvtObj.srcElement.outerText;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "parentElement";
            value = pEvtObj.srcElement.parentElement.toString();
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "title";
            value = pEvtObj.srcElement.title;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "className";
            value = pEvtObj.srcElement.className;
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "positionX";
            value = pEvtObj.clientX.ToString();
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            name = "positionY";
            value = pEvtObj.clientY.ToString();
            webcontrolprop.Add(new ControlPropertiesClass(name, value));

            RelayControlInfoBackToMainEvent(webcontrolprop);

            string ClientX = pEvtObj.clientX.ToString();
            string ClientY = pEvtObj.clientY.ToString();

            if (globaldata != "")
            {
                IE_ClickEventReadyForDisplay(globaldata, ClientX, ClientY);
            }

            //return true;
        }

        private void HandleNonFrameRoutine()
        {
            IE_DISCOVERED_NONFRAME();  //NON Frames Detected

            HTMLDocument = (mshtml.IHTMLDocument2)m_browser.Document;

            Body = HTMLDocument.body;

            string BodyInnerText = Body.innerText;

            string BodyOuterHTML = Body.outerHTML;

            links = HTMLDocument.links;

            IELinksClass obList = new IELinksClass();
            obList.Val = "Head of the List";

            IELinksClass obHead = obList;

            try
            {
                foreach (HTMLAnchorElementClass el in links)
                {
                    linkstotext = el.outerHTML;
         
                    obList.NextItem = new IELinksClass();
                    obList = (IELinksClass)obList.NextItem;
                    obList.Val = linkstotext;
                }
            }
            catch
            {
                // Handle any exceptions
            }

            BodyInnerText = BodyOuterHTML;

            IE_Window_NO_FrameEvent(BodyInnerText, obHead);

            // LOOK FOR WEB TABLES..
            // if(FoundWebTables(BodyInnerText))
                 // SendWebTableEventBackToMain(BodyInnerText);

            
        }

        public void EnterTextWithinFrameCommand(string frameName, string textboxName, string textData)
        {
            IHTMLDocument2 frameObject = LoadFrame(frameName);
            
            if (frameObject != null)
                EnterTextinFrame(frameObject, textboxName, textData);
            else
            { 
            //Error! FrameLoading Did not Work. Is the FrameName Correct???
            }
        }

        public void ClickItemWithinFrameCommand(string frameName, string itemName)
        {
            IHTMLDocument2 frameObject = LoadFrame(frameName);

            if (frameObject != null)
                ClickItemWithinFrame(frameObject, itemName);
            else
            { 
            //Error! FrameLoading Did not Work. Is the FrameName Correct??
            //IFrames may not currently work.
            }
        }

        //public string clicklink_NONFRAME(HtmlElement linkToClick)
        //{
            //HTMLInputElement here = (HTMLInputElement)test.all.item(itemName, 0);
            //here.click();

        //    try
        //    {       
        //        HTMLAnchorElementClass linkElement = (HTMLAnchorElementClass)linkToClick.DomElement;
        //        linkElement.click();
        //    }
        //    catch (Exception ex)
        //    {
        //        return String.Format("Unable to click link: {0}", ex.Message);
        //    }

        //    return "Link was clicked, new page opened...";
        //}

        private IHTMLDocument2 LoadFrame(string frameName)
        {   
            //Note: m_browser will have to be set first
            //Specific IE will have to be Loaded. Hooked Events are not necessary

            if (m_browser != null)
            {
                myDoc = new HTMLDocumentClass();
                myDoc = (HTMLDocument)m_browser.Document;
                //myDoc2 = new HTMLDocument();
                //myDoc2 = (HtmlDocument)m_browser.Document;
                
                FramesCollection myFrameCol = myDoc.frames;
                
                IHTMLWindow2 myContentFrame = null;

                IHTMLDocument2 myContentFrameDoc = null;

                try
                {
                    for (int i = 0; i < myContentFrame.length; i++)
                    {
                        object refIndex = i;
                        IHTMLWindow2 frame = (IHTMLWindow2)myFrameCol.item(ref refIndex);

                        if (frame.name == frameName)
                        {
                            myContentFrame = frame;
                            break;
                        }
                    }
                }
                catch
                {
                    //Handle Exceptions
                    //Maybe be using IFRAMES. FrameCollection may not work correctly in all cases
                    //See Blog HTMLEditor using Google "MSHTML and Frames HTMLEditor IWebbrowser2"
                    //Look on last page
                }

                if (myContentFrame != null)
                    myContentFrameDoc = myContentFrame.document;

                return myContentFrameDoc;
            }
            else
            {
                //A IE is not Set
                //Send Event Message to USer Stating to Load a Browser

                //  EventSendBrowserNotSetMessagetoUser(frameName);

                return null;
            }
        
        }

        private void EnterTextinFrame(IHTMLDocument2 myContentFrameDocument, string TextBoxName, string TextData)
        {
            //Enter a Value in TextBox within the Frame
            //Frame has already been set using LoadFrame

            HTMLInputElement objTextBox = (HTMLInputElement)myContentFrameDocument.all.item(TextBoxName, 0);
            objTextBox.value = TextData;
        }

        private void ClickItemWithinFrame(IHTMLDocument2 myContentFrameDocument,string itemName)
        {
            //Click any "Item" within the Frame
            //Frame has already been set using LoadFrame

            HTMLInputElement itemObject = (HTMLInputElement)myContentFrameDocument.all.item(itemName, 0);
            itemObject.click();
        }

        private void HandleFrameRoutine(int FrameLength, IHTMLFramesCollection2 frames)
        {
            IE_DISCOVERED_FRAMES(FrameLength);  //Frames Detected Event

            for (int index = 0; index < FrameLength; index++)
            {
                object i = index;
                frames.item(ref i);

                try
                {
                    window = (IHTMLWindow2)frames.item(ref i);
                    string WindowFrameName = window.name;

                    frameDocument = (IHTMLDocument2)window.document;

                    framebody = frameDocument.body;
                    framelinks = frameDocument.links;

                    string FrameURL = frameDocument.url;
                    string FrameBodyData = framebody.innerText;

                    IELinksClass obList = new IELinksClass();
                    obList.Val = "List of Links from FrameName: " + WindowFrameName;

                    IELinksClass obHead = obList;

                    try
                    {
                        foreach (HTMLAnchorElementClass frame_el in framelinks)
                        {
                            linkstotext = frame_el.outerHTML;

                            obList.NextItem = new IELinksClass();
                            obList = (IELinksClass)obList.NextItem;
                            obList.Val = linkstotext;
                        }
                    }

                    catch
                    {
                        //Handle Exceptions
                    }

                    IE_Window_FRAMEEVEMT(WindowFrameName, FrameBodyData, obHead, index, FrameURL);
                }
                catch
                { 
                  // in case anything goes wrong while tring to define a window..
                }
            } 
        }

        public void ParseWebTable(string outputxml)
        { 
            // This is for NonFrames
            //document = (IHTMLDocument2)m_browser.Document;
            JustDefineNewIE(false, false, 0, "", "");

            HTMLDocument = (mshtml.IHTMLDocument2)m_browser.Document;

            string Body = HTMLDocument.body.innerHTML;
            htmlsearch.start_processing(Body, outputxml);
        }

        public void New_IEWindowEvent() // TO DO: Change to ParseIEBrowser
        {
            document = (IHTMLDocument2)m_browser.Document;

            IHTMLFramesCollection2 frames = (IHTMLFramesCollection2)document.frames;

            int FrameLength = frames.length;

            if (FrameLength == 0)
            {
                HandleNonFrameRoutine();
            }
            else
            {
                HandleFrameRoutine(FrameLength, frames);
            }   
        }

        public void JustDefineNewIE(bool LockinEvents, bool HoockToSpecificIE, int BrowserMarkerNumber, string WebPageName, string BrowserURL)
        {
            int NumberOfBrowsers = 0;

            if (allBrowsers.Count > 0)
            {
              //  LogWrite("");

                foreach (SHDocVw.WebBrowser ie in allBrowsers)
                {
                    string filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

                    if (filename.Equals("iexplore")) // This can be removed. Not necessary
                    {
                        m_browser = null;

                        m_browser = ie;

                        string LocationName = m_browser.LocationName;
                        string LocationURL = m_browser.LocationURL;
                        int HandleID = (int)m_browser.HWND;

                        NumberOfBrowsers = NumberOfBrowsers + 1;

                     //   LogWrite("Browser Number: " + NumberOfBrowsers.ToString());
                     //   LogWrite("Web Page Name: " + LocationName);
                     //   LogWrite("URL: " + LocationURL);
                     //   LogWrite("Handle ID: " + HandleID.ToString());
                     //   LogWrite("");
                     //   LogWrite("");

                        if (LockinEvents)
                        {
                            //  if (!HoockToSpecificIE) //Hooking To LAST Browser. Not Hooking to any number within list of Browsers
                            //  IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);

                            if (HoockToSpecificIE)
                                if (NumberOfBrowsers == BrowserMarkerNumber || (WebPageName == LocationName || BrowserURL == LocationURL))
                                {
                                    //IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);

                                    //  object hello = null;

                                    //  m_browser.Navigate(LocationURL, ref hello, ref hello, ref hello, ref hello);
                                    //          int number = 135074;

                                    //          WindowInfo.ActiveWindowBaedOnHandleID(number);


                           //->taken out         IE_BrowserSpecificHookEvent(LocationURL, HandleID, NumberOfBrowsers, LocationName);
                                    break;  //At Current IE Browser location
                                }
                           //-->taken out     else
                           //-->taken out         IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);
                        }
                    } // ENDOF CHECKING IF MS_IEXPLORE

                } // ENDOF FOREACH ALLBROWSERS
            } // ENDOF IF ALLBROWSERS > 0 

            //  return m_browser;
        }

        //public SHDocVw.WebBrowser NewHookIE(bool LockinEvents,bool HookToSpecificIE, int BrowserMarkerNumber, string WebPageName, string BrowserURL)
        public void DefineNewIE(bool LockinEvents, bool HoockToSpecificIE, int BrowserMarkerNumber, string WebPageName, string BrowserURL)
        {   
          int NumberOfBrowsers = 0;

          if (allBrowsers.Count > 0)
          {
              LogWrite("");

              foreach (SHDocVw.WebBrowser ie in allBrowsers)
              {
                  string filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

                     if (filename.Equals("iexplore")) // This can be removed. Not necessary
                     {
                        m_browser = null;

                        m_browser = ie;

                        string LocationName = m_browser.LocationName; 
                        string LocationURL = m_browser.LocationURL;
                        int HandleID = (int)m_browser.HWND;

                        NumberOfBrowsers = NumberOfBrowsers + 1;

                        LogWrite("Browser Number: " + NumberOfBrowsers.ToString());
                        LogWrite("Web Page Name: " + LocationName);
                        LogWrite("URL: " + LocationURL);
                        LogWrite("Handle ID: " + HandleID.ToString());
                        LogWrite("");
                        LogWrite("");

                         if (LockinEvents)
                         {
                           //  if (!HoockToSpecificIE) //Hooking To LAST Browser. Not Hooking to any number within list of Browsers
                           //  IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);

                             if (HoockToSpecificIE)
                                if (NumberOfBrowsers == BrowserMarkerNumber || (WebPageName == LocationName || BrowserURL == LocationURL))
                                {
                                //IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);

                                  //  object hello = null;

                                  //  m_browser.Navigate(LocationURL, ref hello, ref hello, ref hello, ref hello);
                          //          int number = 135074;

                          //          WindowInfo.ActiveWindowBaedOnHandleID(number);
                                  

                                IE_BrowserSpecificHookEvent(LocationURL,HandleID, NumberOfBrowsers, LocationName);
                                break;  //At Current IE Browser location
                                }
                             else
                                IE_BrowserInfoReadyForDisplay(LocationURL, HandleID, NumberOfBrowsers, LocationName);
                         }
                     } // ENDOF CHECKING IF MS_IEXPLORE

               } // ENDOF FOREACH ALLBROWSERS
            } // ENDOF IF ALLBROWSERS > 0 

          //  return m_browser;
        }

        void m_browser_DocumentComplete(object pDisp, ref object URL)
        {
            // IE Browser as Finished loading...
        }

        void iEvent_onclick(IHTMLEventObj pEvtObj)
        {
            switch (IEdataselector)
            {
                case "OuterText":
                    {
                        globaldata = pEvtObj.srcElement.outerText;
                        break;
                    }

                case "InnerText":
                    {
                        globaldata = pEvtObj.srcElement.innerText;
                        break;
                    }

                case "InnerHTML":
                    {
                        globaldata = pEvtObj.srcElement.innerHTML;
                        break;
                    }

                case "HREF":
                    {
                        globaldata = pEvtObj.srcElement.getAttribute("href", 0).ToString();
                        break;
                    }

                case "NAME":
                    {
                        globaldata = pEvtObj.srcElement.getAttribute("name", 0).ToString();
                        break;
                    }

                case "outerHTML":
                    {
                        globaldata = pEvtObj.srcElement.outerHTML;
                        break;
                    }

                case "ALT":
                    {
                       globaldata = pEvtObj.srcElement.getAttribute("alt", 0).ToString();
                       break;
                    }

                case "tagName":
                    {
                        globaldata = pEvtObj.srcElement.tagName;
                        break;
                    }
                default:
                    {
                        globaldata = "";
                        break;
                    }
            }

            string ClientX = pEvtObj.clientX.ToString();
            string ClientY = pEvtObj.clientY.ToString();

            current_globaldata = globaldata;

         //   LogWrite("Current Global: " + current_globaldata);
         //   LogWrite("Previous Global: " + previous_globaldata);
         //   LogWrite("");

            if (globaldata != "")
            {
                if (current_globaldata != previous_globaldata)
                IE_ClickEventReadyForDisplay(globaldata, ClientX, ClientY);
            }

            previous_globaldata = current_globaldata;
        }
    }
}
