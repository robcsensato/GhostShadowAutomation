using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Automation
{
    public class XMLWriter : Object 
    {
        private int EventID = 0;
        private string global_file_name;
        private string global_definition_file;

        public event GetXmlTextReaderDelegate GetXmlTextReaderEvent;

        public struct CurrentMonitorSettingsToWrite
        {
            //public int Position_Top;
            //public int Position_Bottom;
            //public int Position_Left;
            //public int Position_Right;
            public int Height;
            public int Width;
        };

        private CurrentMonitorSettingsToWrite[] CurrentMonitorSettingsToXML = new CurrentMonitorSettingsToWrite[6];
        
        public void setup_XML(string XML_NAME)
        {
            // This method intially set's up the XML document

            XmlTextWriter objXmlTextWriter = new XmlTextWriter(XML_NAME, null);
            objXmlTextWriter.Formatting = Formatting.Indented;
            objXmlTextWriter.WriteStartDocument();
            objXmlTextWriter.WriteStartElement("MyRecordedValues");
            objXmlTextWriter.WriteStartElement("Automation");
            objXmlTextWriter.WriteString("START");
            objXmlTextWriter.WriteEndElement();
                    
            objXmlTextWriter.WriteEndElement();
            objXmlTextWriter.WriteEndDocument();
            objXmlTextWriter.Flush();
            objXmlTextWriter.Close();

            global_file_name = XML_NAME;
            //GlobalRecord.GLOBAL_RECORDFILE_NAME = XML_NAME;
        }

        public void WriteTDTagToWebTable(string XMLFILE, int I)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<TD column=\"" + I.ToString() + "\">" + "</TD>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }
        
        }


        public void initialize_webtable(string XML_NAME)
        {
            // Setup the Webtable XML File

            XmlTextWriter objXmlTextWriter = new XmlTextWriter(XML_NAME, null);
            objXmlTextWriter.Formatting = Formatting.Indented;
            objXmlTextWriter.WriteStartDocument();
            objXmlTextWriter.WriteStartElement("MyWebTableValues");
            objXmlTextWriter.WriteStartElement("Description");
            objXmlTextWriter.WriteString("Users can define their own description.. Later enhancement..");
            objXmlTextWriter.WriteEndElement();

            objXmlTextWriter.WriteEndElement();
            objXmlTextWriter.WriteEndDocument();
            objXmlTextWriter.Flush();
            objXmlTextWriter.Close();
        }

        public void AddEndingTDTagToWebTable(string XMLFILE)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "</TD>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }
        
        }

        public void AddStartingTDTagToWebTable(string XMLFILE)
        {
            try
            {
                XmlTextWriter objXmlTextWriter = new XmlTextWriter(XMLFILE, null);
                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartElement("TD");

                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();

               // XmlTextReader reader = new XmlTextReader(XMLFILE);
               // XmlDocument doc = new XmlDocument();
               // doc.Load(reader);
               // reader.Close();
               // XmlNode currNode;

               // XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
               // docFrag.InnerXml = "<TD>" + "";
                // insert the availability node into the document 
               // currNode = doc.DocumentElement;
               // currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
               // doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        
        }

        public void updatewebtable(string XMLFILE, string Value, int counter)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();

                XmlNode AppendXMLNode = doc.SelectSingleNode("//TD[@column='" + counter.ToString() + "']");

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();

                int row_counter = AppendXMLNode.ChildNodes.Count + 1;

                docFrag.InnerXml = "<TR row=\"" + row_counter.ToString() + "\">" + Value + 
                "</TR>";
                // insert the availability node into the document 
                
                //currNode = doc.DocumentElement; --> KEEP THIS... OLD WAY OF ADDING
                //currNode.InsertAfter(docFrag, currNode.LastChild); --> KEEP THIS... OLD WAY OF ADDING
                
                //save the output to a file
                AppendXMLNode.AppendChild(docFrag);
                doc.Save(XMLFILE);

            }
            catch (Exception ex)

            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        }

        public void WriteInitialMonitorConfigurationNode(string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            /// NEW !!!
            ///
            try
            {
            XmlTextReader reader = new XmlTextReader(XMLFILE);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode currNode;

            //EventID = EventID + 1;

            XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
            docFrag.InnerXml = "<WindowScreenResolution>" +
                "</WindowScreenResolution>";
            // insert the availability node into the document 
            currNode = doc.DocumentElement;
            currNode.InsertAfter(docFrag, currNode.LastChild);
            //save the output to a file 
            doc.Save(XMLFILE);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            } 

            //// END

         //   try
         //   {
         //      XmlDocument doc = new XmlDocument();
         //       doc.Load(global_file_name);
         //       XmlNode currNode;
         //
         //       XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
         //       docFrag.InnerXml = "<WindowScreenResolution>" +
         //
         //          "</WindowScreenResolution>";
         //
         //       // insert the availability node into the document 
         //       currNode = doc.DocumentElement;
         //       currNode.InsertAfter(docFrag, currNode.LastChild);
         //       //save the output to a file 
         //       doc.Save(global_file_name);
         //   }
         //   catch (Exception ex)
         //   {
         //      Console.WriteLine("Exception: {0}", ex.ToString());
         //   } 
        }

        public void updateMonitorConfig(string Height, string Width, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            XmlDocument doc = new XmlDocument();

            doc.Load(XMLFILE);

            XmlNode AppendXMLNode = doc.SelectSingleNode("//WindowScreenResolution");
                                  
            XmlNode docFrag = doc.CreateDocumentFragment();

            docFrag.InnerXml = "<monitor>" +
                    "<Width>" + Width + "</Width>" +
                    "<Height>" + Height + "</Height>" +
             "</monitor>";

            AppendXMLNode.AppendChild(docFrag);
            doc.Save(XMLFILE);
        }

        public void InitialwriteScreenResolutionXML(int Right, int Bottom, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<WindowScreenResolution>" +
                    "<right>" + Right.ToString() + "</right>" +
                    "<bottom>" + Bottom.ToString() + "</bottom>" +
                    "</WindowScreenResolution>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            } 
        }

        public void AddWritetoScreenMonitorResolutionToXML(int right, int bottom, string XMLFILE)
        {   // Coming Soon!
            // Add more monitors to <WindowScreenResolution> Tag
        }

        public void SetUP_Definition(string DefinitionFile)
        {
            XmlTextWriter objXmlTextWriter = new XmlTextWriter(DefinitionFile, null);
            objXmlTextWriter.Formatting = Formatting.Indented;
            objXmlTextWriter.WriteStartDocument();
            objXmlTextWriter.WriteStartElement("Definition");
            objXmlTextWriter.WriteStartElement("Controls");
            objXmlTextWriter.WriteString("START");
            objXmlTextWriter.WriteEndElement();

            objXmlTextWriter.WriteEndElement();
            objXmlTextWriter.WriteEndDocument();
            objXmlTextWriter.Flush();
            objXmlTextWriter.Close();

            global_definition_file = DefinitionFile;
        }

        public void writeSpecialKeyboardEvent_To_XML(string keyPressed, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;
            try
            {  
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<keyboardEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<pressed>" + keyPressed.ToString() + "</pressed>" +
                    "</keyboardEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        }

        public void writeComboPressedKeyBoardEvent_To_XML(string data, string XMLFILE)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<keyboardComboEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<combo>" + data + "</combo>" +
                    "</keyboardComboEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //update the XML file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }


        public void writeNormalKeyboardEvent_To_XML(string userTyped, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<keyboardEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<typed>" + userTyped.ToString() + "</typed>" +
                    "</keyboardEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //update the XML file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        }

        public void writeIECaption_To_XML(string IEwindowTitle, int Top, int Left, string XMLFILE)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<IEwindowEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<windowCaption>" + IEwindowTitle + "</windowCaption>" +
                    "<top>" + Top.ToString() + "</top>" +
                    "<left>" + Left.ToString() + "</left>" +
                    "</IEwindowEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        }


        public void writeWindowCaption_To_XML(string windowTitle, int Top, int Left, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<windowEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" + 
                    "<windowCaption>" + windowTitle + "</windowCaption>" +
                    "<top>" + Top.ToString() + "</top>" +
                    "<left>" + Left.ToString() + "</left>" + 
                    "</windowEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            } 
        }

        public void writeMouseClick_To_XML(string ActiveWindow, string button, string x_value, string y_value, int ParentWindow_Length, int ParentWindow_Height, int Parent_PositionLeft, int Parent_PositionRight, int Parent_PositionTop, int Parent_PositionBottom, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<Button>" + button + "</Button>" + 
                    "<x_corridante>" + x_value + "</x_corridante>" + 
                    "<y_corridante>" + y_value + "</y_corridante>" +
                    //"<ReferenceWindowName>" + ActiveWindow + "</ReferenceWindowName>" +
                    //"<ReferenceLength>" + ParentWindow_Length + "</ReferenceLength>" +
                    //"<ReferenceHeighth>" + ParentWindow_Height + "</ReferenceHeighth>" +
                    //"<ReferenceLeft>" + Parent_PositionLeft + "</ReferenceLeft>" +
                    //"<ReferenceRight>" + Parent_PositionRight + "</ReferenceRight>" +
                    //"<ReferenceTop>" + Parent_PositionTop + "</ReferenceTop>" +
                    //"<ReferenceBottom>" + Parent_PositionBottom + "</ReferenceBottom>" +
                     "</mouseClickEvent>";
        
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }

        public void writeMouseClick_From_IE(string data, string datatype, string PositionX, string PositionY, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<IEBrowserClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<datatype>" + datatype + "</datatype>" +
                    "<data>" + data + "</data>" +
                    "<positionX>" + PositionX + "</positionX>" +
                    "<positionY>" + PositionY + "</positionY>"+
                     "</IEBrowserClickEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            } 
        }

        public void writeMouseClick_From_IE(string PositionX, string PositionY, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<IEBrowserClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<positionX>" + PositionX + "</positionX>" +
                    "<positionY>" + PositionY + "</positionY>" +
                     "</IEBrowserClickEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }

        public void writeMouseClick_To_XML(string button, string x_value, string y_value, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<Button>" + button + "</Button>" +
                    "<x_corridante>" + x_value + "</x_corridante>" +
                    "<y_corridante>" + y_value + "</y_corridante>" +

                    //"<Description>" +
                    //"<parentwindow>" + AttachWindow + "</parentwindow>" +
                    //"<caption>" + Child_Caption + "</caption>" +
                    //"<controltype>" + Type + "</controltype>" +
                    //"</Description>" +

                    //"<ReferenceLength>" + ParentWindow_Length + "</ReferenceLength>" +
                    //"<ReferenceHeighth>" + ParentWindow_Height + "</ReferenceHeighth>" +
                    //"<ReferenceLeft>" + Parent_PositionLeft + "</ReferenceLeft>" +
                    //"<ReferenceRight>" + Parent_PositionRight + "</ReferenceRight>" +
                    //"<ReferenceTop>" + Parent_PositionTop + "</ReferenceTop>" +
                    //"<ReferenceBottom>" + Parent_PositionBottom + "</ReferenceBottom>" +

                     "</mouseClickEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }

        public void writeMouseUPEvent_To_XML(string button, string x_position, string y_position, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseUpEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<Button>" + button.ToString() + "</Button>" +
                    "<x_corridante>" + x_position + "</x_corridante>" +
                    "<y_corridante>" + y_position + "</y_corridante>" +
                    "</mouseUpEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }
        }

        public void writeButtonSingleClickToXml(string CurrentCaptionstring, string ButtonUsed, string ButtonX, string ButtonY, string XMLFILE)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<ButtonClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<Caption>" + CurrentCaptionstring + "</Caption>" +
                    "<Button>" + ButtonUsed + "</Button>" +
                    "<x_corridante>" + ButtonX + "</x_corridante>" +
                    "<y_corridante>" + ButtonY + "</y_corridante>" +
                     "</ButtonClickEvent>";

                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }

        public void writeMouseDoubleClick_To_XML(string button, string x_value, string y_value, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseDoubleClickEvent>" +
                    "<eventID>" + EventID + "</eventID>" +
                    "<Button>" + button + "</Button>" +
                    "<x_corridante>" + x_value + "</x_corridante>" +
                    "<y_corridante>" + y_value + "</y_corridante>" +
                     "</mouseDoubleClickEvent>";

                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
        }

        public void writeMouseDOWNEvent_To_XML(string button, string x_position, string y_position, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseDownEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<Button>" + button.ToString() + "</Button>" +
                    "<x_corridante>" + x_position + "</x_corridante>" +
                    "<y_corridante>" + y_position + "</y_corridante>" +
                    "</mouseDownEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }
        }

        public void writeMouseWaitPosition(int x_position, int y_position, int wait_time, string XMLFILE)
        {
            //global_file_name = GlobalRecord.GLOBAL_RECORDFILE_NAME;

            try
            {
                XmlTextReader reader = new XmlTextReader(XMLFILE);
                XmlDocument doc = new XmlDocument();
                
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;

                EventID = EventID + 1;

                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = "<mouseWaitEvent>" +
                    "<eventID>" + EventID.ToString() + "</eventID>" +
                    "<wait_time>" + wait_time + "</wait_time>" + 
                    "<x_corridante>" + x_position + "</x_corridante>" +
                    "<y_corridante>" + y_position + "</y_corridante>" +
                    "</mouseWaitEvent>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                doc.Save(XMLFILE);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }
        }

        private bool DoesParentNodeExist(string ParentNode)
        {
                XPathDocument doc = new XPathDocument(global_definition_file);
                XPathNavigator nav = doc.CreateNavigator();

                // Compile a standard XPath expression
                XPathExpression expr;
                expr = nav.Compile("//parentwindow[@name='" + ParentNode + "']");

                XPathNodeIterator iterator = nav.Select(expr);
           
                if (iterator.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }
        
        public void writeToDefinitionFile(string the_global_definition_file, string controlname,string parentwindow, string ParentTOP, string ParentBOTTOM, string ParentLEFT, string ParentRIGHT, string top, string bottom, string left, string right)
        {
            //<parentwindowtop>ParentTOP</parentwindowtop>
            //<parentwindowbottom>ParentBOTTOM</parentwindowbottom>
            //<parentwindowleft>ParentLEFT</parentwindowleft>
           //<parentwindowright>ParentRIGHT</parentwindowright>

            // This is not the most efficient way of handling this.
            // It is easyist to manage.

            //Check if Definition File Exists Else this will break;
            
            //Check to see if ParentWindow Already Exist
            //If it does then go to Else (Update the Parent Window Node).
            global_definition_file = the_global_definition_file;

            if (!DoesParentNodeExist(parentwindow))
            {
               // XmlTextReader reader = new XmlTextReader(global_definition_file);
                XmlDocument doc = new XmlDocument();

                XmlTextReader XmlReaderFile = GetXmlTextReaderEvent();

                // Check to See if XmlTextReader is already defined
                // if so then use that Read
                
                //XmlTextReader XmlTextReaderFile = GetXmlTextReader();
                if (XmlReaderFile != null)
                {
                    doc.Load(XmlReaderFile);
                }
                else
                {
                doc.Load(global_definition_file);
                }
                
                XmlNode currNode;
                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                
                //TO DO: WRITE <parentwindowtop>ParentTOP</parentwindowtop>
                             //<parentwindowbottom>ParentBOTTOM</parentwindowbottom>
                             //<parentwindowleft>ParentLEFT</parentwindowleft>
                             //<parentwindowright>ParentRIGHT</parentwindowright>

                docFrag.InnerXml = "<parentwindow name=\"" + parentwindow + "\">" +
                    "<parentwindowtop>" + ParentTOP + "</parentwindowtop>" + 
                    "<parentwindowbottom>" + ParentBOTTOM + "</parentwindowbottom>" +
                    "<parentwindowleft>" + ParentLEFT + "</parentwindowleft>" +
                    "<parentwindowright>" + ParentRIGHT + "</parentwindowright>" +
                    "<control name=\"" + controlname + "\">" +
                        "<positiontop>" + top + "</positiontop>" +
                        "<positionbottom>" + bottom + "</positionbottom>" +
                        "<positionleft>" + left + "</positionleft>" +
                        "<positionright>" + right + "</positionright>" +
                    "</control>" +
                 "</parentwindow>";
                // insert the availability node into the document 
                currNode = doc.DocumentElement;

                currNode.InsertAfter(docFrag, currNode.LastChild);
                //save the output to a file 
                try
                {
                    doc.Save(global_definition_file);
                }
                catch (Exception e)
                { }
            }

            else 
            {
                //The Parent Winodow Already Exists. Update ParentWindow Node
                XmlDocument doc = new XmlDocument();
                //load your xml file
                doc.Load(global_definition_file);
                //Select main node
                XmlNode AppendXMLNode = doc.SelectSingleNode("//parentwindow[@name='" + parentwindow + "']");

                XmlNode docFrag = doc.CreateDocumentFragment();
                
                docFrag.InnerXml = "<control name=\"" + controlname + "\">" +
                        "<positiontop>" + top + "</positiontop>" +
                        "<positionbottom>" + bottom + "</positionbottom>" +
                        "<positionleft>" + left + "</positionleft>" +
                        "<positionright>" + right + "</positionright>" +
                    "</control>";
                 
                AppendXMLNode.AppendChild(docFrag);
                try
                {
                    doc.Save(global_definition_file);
                }
                catch
                { }
            }
        }
    }
}
