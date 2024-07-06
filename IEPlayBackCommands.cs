using System;
using System.Collections;
using System.Threading;
using System.Text;
using SHDocVw;
using mshtml;

namespace Automation
{
    public class IEPlayBackCommands
    {
        private HTMLDocument myDoc;
        private HTMLDocument myWebBrowserReference;
        private IHTMLDocument2 document;
    
        private SHDocVw.WebBrowser current_MBrowser;
        private IHTMLElementCollection HTMLObject;

        static InternetExplorer ie_explore = null;

        private IHTMLElement framebody;
        private IHTMLElementCollection framelinks;

        private IHTMLWindow2 window;
        private IHTMLDocument2 frameDocument;

        private string linkstotext;

        private int count;

        //This is for Testing and Debugging
        //public event TEST_COLLECT_LINKS SENDLIKS;
      
        public IEPlayBackCommands(SHDocVw.WebBrowser m_browser)
        {
            current_MBrowser = m_browser;
        }

        ~IEPlayBackCommands()
        {
            current_MBrowser = null;
        }

        public void Navigate(string url, bool open_in_new_window)
        {
            //isDocumentComplete = false;
            object empty = "";

            if (!open_in_new_window)
            {
                current_MBrowser.Navigate(url, ref empty, ref empty, ref empty, ref empty);
                //WaitForComplete();
            }
            else
            {
                //Open a new IE Browser
                ie_explore = new SHDocVw.InternetExplorer();
                ie_explore.Visible = true;

                current_MBrowser = (WebBrowser)ie_explore;
                current_MBrowser.Navigate(url, ref empty, ref empty, ref empty, ref empty);

                //PLACE BACK IN!!! TOOK OUT FOR NOW>> 

                //TO BE USED BY APPLICATION WHEN NEW IE WINDOW APPEARS
                 //Hook to Last Browser but don't lock in Events
                 //SelectedBrowser = IEhandler.NewHookIE(false, false, "", "", "");
                 //ieplayback = new IEPlayBackCommands(SelectedBrowser);
    
                //PLACE THIS BACK IN SOON!!!
                //current_MBroswer.DocumentComplete += new SHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(current_MBrowser_DocumentComplete);
            }
        }

        public void CurrentIE_Foward()
        {
            current_MBrowser.GoForward();
        }

        public void CurrentIE_Back()
        {
            current_MBrowser.GoBack();
            //current_MBrowser.GoHome();
        }

        #region NEW_JAVASCRIPT_EVENT

        public void FireEventOnJavaScript(string scriptname)
        {
            myDoc = (HTMLDocument)current_MBrowser.Document;
            //HTMLSelectElement eventCat = (HTMLSelectElement)myDoc.all.item("EventCategory", 0);
            //eventCat.selectedIndex = 13;

            HTMLSelectElement eventCat = (HTMLSelectElement)myDoc.all.item(scriptname, 0);

            IHTMLElement eventCatElem = eventCat as IHTMLElement;
            IHTMLDocument4 doc = eventCatElem.document as IHTMLDocument4;
            object dummy = null;
            object eventObj = doc.CreateEventObject(ref dummy);
            HTMLSelectElementClass se = eventCat as HTMLSelectElementClass;

            doc.FireEvent("onclick", ref eventObj);
            //se.FireEvent("onclick", ref dummy);
            //eventCat.FireEvent("onchange", eventCat);
        }

        #endregion

        #region NON_FRAMES

        public void ClickAnchor(string anchorId)
        {
            try
            {
                //isDocumentComplete = false;
                HTMLAnchorElement input = GetAnchorElement(anchorId);
                input.click();
                //WaitForComplete();
            }
            catch
            {
                // Error. Link not valid or Browser not Set

                //if current_MBrowser == null then
                //Browser is not Set
                //Else
                //Link not Found
            }
        }

        public void ClickAnchorWithParent(string parentControlId, string anchorId)
        {
            try
            {
                //isDocumentComplete = false;
                anchorId = parentControlId + anchorId;
                HTMLAnchorElement input = GetAnchorElement(anchorId);
                input.click();
                //WaitForComplete();
            }
            catch
            {
                // Error. Link not valid or Browser not Set

                //if current_MBrowser == null then
                //Browser is not Set
                //Else
                //Link not Found
            }
        }

        public void ClickAnchorWithValue(string anchorValue)
        {
            try
            {
                HTMLAnchorElement anchor = (HTMLAnchorElement)GetElementByValue("A", anchorValue);
                anchor.click();
                //WaitForComplete();
            }
            catch
            { 
            // Error. Link not valid or Browser not Set

            //if current_MBrowser == null then
            //Browser is not Set
            //Else
            //Link not Found
            }
        }


        public void ClickButton(string buttonId)
        {
            try
            {
                //isDocumentComplete = false;
                HTMLInputElementClass input = GetInputElement(buttonId);
                input.click();
                //WaitForComplete();
            }
            catch
            { 
            // Error 
            }
        }


        public void ClickElementByValue(string tagName, string elementName)
        {
            //This is for Clicking Buttons within a Frame
            HTMLInputElementClass element = GetInputElementValue(tagName, elementName);
            element.click();
        }

        private HTMLInputElementClass GetInputElementValue(string tagName, string elementName)
        {
            return (HTMLInputElementClass)GetElementByValue(tagName, elementName);
 
            //HTMLInputElementClass InputButton = GetInputButtonByValue(type, name);
            //InputButton.click();
            //InputButton.id;
            //InputButton.innerText;
            //InputButton.innerHTML;
        }

        private IHTMLElement ClickInputButtonByValue(string type, string name)
        {
            IHTMLElement referenceHTMLObject = null;
            
            myWebBrowserReference = (mshtml.HTMLDocument)current_MBrowser.Document;

            HTMLObject = myWebBrowserReference.getElementsByTagName("input");

            foreach(IHTMLElement HTMLElement in HTMLObject)
            {                
                if (HTMLElement.getAttribute("name", 0) != null || HTMLElement.getAttribute("type",0) != null)
                {
                    if (HTMLElement.tagName == name || HTMLElement.tagName == type)
                    {
                        referenceHTMLObject = HTMLElement;
                        break;  
                    }
                                                          
                 }
            }

           return referenceHTMLObject;
        }

        public void ClickCheckbox(string anchorId)
        {
            //isDocumentComplete = false;
            HTMLInputElement input = GetCheckboxElement(anchorId);
            input.click();
        }

        public bool DoesElementExist(string elementId)
        {
            IHTMLElement input = GetElementById(elementId);
            return input != null;
        }

        private HTMLInputElementClass GetInputElement(string inputId)
        {
            return (HTMLInputElementClass)GetElementById(inputId);
        }

        private HTMLInputElement GetCheckboxElement(string inputId)
        {
            return (HTMLInputElement)GetElementById(inputId);
        }

        private IHTMLElement GetElementById(string elementId)
        {
            HTMLDocument document = ((HTMLDocument)current_MBrowser.Document);
            IHTMLElement element = document.getElementById(elementId);

            int nullElementCount = 0;
            // The following loop is to account for any latency that IE
            // might experience.  Tweak the number of times to attempt
            // to continue checking the document before giving up.
            while (element == null && nullElementCount < 10)
            {
                //Thread.Sleep(500);
                element = document.getElementById(elementId);
                nullElementCount++;
            }

            return element;
        }

        private object GetElementAttribute(string elementId, string attributeName)
        {
            IHTMLElement element = GetElementById(elementId);
            if (element == null)
            {
                return null;
            }
            return element.getAttribute(attributeName, 0);
        }

        protected IHTMLElement GetElementByValue(string tagName, string elementValue)
        {
            int nullElementCount = 0;
            IHTMLElement element = GetElementByValueOnce(tagName, elementValue);

            // The following loop is to account for any latency that IE
            // might experience.  Tweak the number of times to attempt
            // to continue checking the document before giving up.
            while (element == null && nullElementCount < 10)
            {
                //Thread.Sleep(500);
                element = GetElementByValueOnce(tagName, elementValue);
                nullElementCount++;
            }

            return element;
        }

        private IHTMLElement GetElementByValueOnce(string tagName, string elementValue)
        {
            HTMLDocument document = ((HTMLDocument)current_MBrowser.Document);
            IHTMLElementCollection tags = document.getElementsByTagName(tagName);

            IEnumerator enumerator = tags.GetEnumerator();

            while (enumerator.MoveNext())
            {
                IHTMLElement element = (IHTMLElement)enumerator.Current;
                if (element.innerText == elementValue)
                {
                    return element;
                }
            }

            return null;
        }

        private HTMLAnchorElement GetAnchorElement(string inputId)
        {
            return (HTMLAnchorElement)GetElementById(inputId);
        }

        private HTMLSelectElement GetSelectElement(string inputId)
        {
            return (HTMLSelectElement)GetElementById(inputId);
        }

        public void SetCheckboxValue(string checkboxId, bool isChecked, bool failIfNotExist)
        {
            HTMLInputElementClass input = GetInputElement(checkboxId);

            if (input == null && failIfNotExist)
            {
                throw new ApplicationException("CheckBox ID: " + checkboxId + " was not found.");
            }
            if (input != null)
            {
                input.@checked = isChecked;
            }
        }

        public void SetInputStringValue(string inputId, string elementValue)
        {
            HTMLInputElementClass input = GetInputElement(inputId);
            input.value = elementValue;
        }

        public void SetInputIntValue(string inputId, int elementValue)
        {
            HTMLInputElementClass input = GetInputElement(inputId);
            input.value = elementValue.ToString();
        }

        public void SelectValueByIndex(string inputId, int index)
        {
            HTMLSelectElementClass input = (HTMLSelectElementClass)GetSelectElement(inputId);
            input.selectedIndex = index;
        }

        #endregion

        #region FRAMES

        #region WorkingClickLinkFrameArea

        public void NewClickLinkFrame(string frameName, string linkName)
        {
            document = (IHTMLDocument2)current_MBrowser.Document;

            IHTMLFramesCollection2 frames = (IHTMLFramesCollection2)document.frames;

            int FrameLength = frames.length;

            HandleFrameRoutine(frameName, linkName, FrameLength, frames);
        }
        
        private void HandleFrameRoutine(string frameName, string LinkName,int FrameLength, IHTMLFramesCollection2 frames)
        {            
            for (int index = 0; index < FrameLength; index++)
            {
                object i = index;
                frames.item(ref i);

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

                        if (frame_el.innerText == LinkName && WindowFrameName == frameName)
                        { 
                            frame_el.click(); //Could do way more if wanted
                            index = FrameLength;
                            break;
                        }                        
                    }
                }

                catch
                {
                    //Handle Exceptions
                }

                //EVENTS

                //1: Have the Option to send over more data via an Event Handler
                //2: Could send "Finished Click" Event to stop Thread running in "Play Back".
            }
        }

        #endregion

        private IHTMLDocument2 LoadFrame(string frameName)
        {
            //Note: m_browser will have to be set first
            //Specific IE will have to be Loaded. Hooked Events are not necessary

            if (current_MBrowser != null)
            {
                //myDoc = new HTMLDocumentClass();
                //myDoc = (HTMLDocument)current_MBrowser.Document;

                HTMLDocumentClass iedoc = (HTMLDocumentClass)current_MBrowser.Document;
                //FramesCollection could break with IFrames
                FramesCollection myFrameCol = iedoc.frames;

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

                //EventSendBrowserNotSetMessagetoUser(frameName);

                return null;
            }

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
                //IFrames may not work.
            }
        }

        private void EnterTextinFrame(IHTMLDocument2 myContentFrameDocument, string TextBoxName, string TextData)
        {
            //Enter a Value in TextBox within the Frame
            //Frame has already been set using LoadFrame

            HTMLInputElement objTextBox = (HTMLInputElement)myContentFrameDocument.all.item(TextBoxName, 0);
            objTextBox.value = TextData;
        }

        private void ClickItemWithinFrame(IHTMLDocument2 myContentFrameDocument, string itemName)
        {
            //Click any "Item" within the Frame
            //Frame has already been set using LoadFrame
            //object tagName = itemName;

            HTMLInputElement itemObject = (HTMLInputElement)myContentFrameDocument.all.item(itemName, 0);
            itemObject.click();
        }

        #region ExperimentClickLinkWithinFrame
        public void XClickLinkWithinFrameCommand(string frameName, string linkName)
        {

            //LOOK IN IEHANDLE HandleFrameRoutine Method..
            //Adjust to Enter in Frame Name and LinkName

            IHTMLDocument2 doc = (IHTMLDocument2)current_MBrowser.Document;
           
            IHTMLDocument2 frame = getFrameDoc(doc, frameName);
            IHTMLElement2 elt = (IHTMLElement2)getLink(doc, linkName);

            //Send over Links via Event back to "PlayBack"
            //SENDLIKS(count);

            if (elt != null)      
            FireEvent((IHTMLDocument4)frame, (IHTMLElement)elt, "onclick");        
        }

        private IHTMLDocument2 getFrameDoc(IHTMLDocument2 doc, string frameName)
        {
            object oI;
            int len = (int)doc.frames.length;
            for (int i = 0; i < len; i++) 
            {
                oI = i; 
                IHTMLWindow2 frame = (IHTMLWindow2)doc.frames.item(ref oI);
                if (frame.name == frameName)
                    return frame.document;
            }
            throw new ArgumentException("No frame found");
            
        }

        private IHTMLElement getLink(IHTMLDocument2 doc, string linkText)
        {
           count = doc.links.length+777;

            try
            {
                //foreach (IHTMLElement link in doc.links)
                foreach (HTMLAnchorElementClass link in doc.links)
                {
                    //linkhere = link.innerHTML;

                    count = count + 1;
                  
                    if (linkText == link.innerText)
                        return (IHTMLElement)link;

                }
                return null;
            }
            catch
            {
                count = 999;
                return null; //Something went wrong!!
            }
        }

        private bool FireEvent(IHTMLDocument4 doc, IHTMLElement elt, string eventName)
        {
            object dummy = null;
            object oEvt = doc.CreateEventObject(ref dummy);
            IHTMLEventObj2 evt = (IHTMLEventObj2)oEvt; //cast

            //set various properties of the event here.
            evt.button = 1;
  
            //fire
            return doc.FireEvent(eventName, ref oEvt);
        }
        #endregion

        #endregion
    }
}
