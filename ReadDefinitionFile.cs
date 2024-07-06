using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Automation
{
    class ReadDefinitionFile: Object
    {
        private XmlTextReader global_definition_reader;

        public event ReplayLogWriteForReadDefinitionFileDelegate LogWrite;
        public event GetGlobalDefinitionReaderDelegate GetGlobalDefinitionReaderEvent;
       
        public string top;
        public string bottom;
        public string left;
        public string right;

        public string ParentTop;
        public string ParentBottom;
        public string ParentLeft;
        public string ParentRight;

        bool GlobalDefinitionFileIsRead = false;

        public bool CheckForControls(string parentwindow, string controlname)
        {
            global_definition_reader = GetGlobalDefinitionReaderEvent();

            if (global_definition_reader == null)
            {
                // Write to Error Log file that Definition file is not defined
                LogWrite("Error. XML Reader is not set");
                return false;
            }

            // You only need to load the Definition File once. This is in place since multiple of reads of the Object file is possible
            // Reloading the XML multiple times causes problems... Using XmlDocument xmldoc = new XmlDoc and Re-loading is the problem...
    
            if (!GlobalDefinitionFileIsRead)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(global_definition_reader);

                GlobalDefinitionFileIsRead = true; // Object File is loaded..

                XmlNode xmlnode = xmldoc.SelectSingleNode("//parentwindow[@name='" + parentwindow + "']/control[@name='" + controlname + "']");
                XmlNode xmlnodeParentINFO = xmldoc.SelectSingleNode("//parentwindow[@name='" + parentwindow + "']");

                if (xmlnode != null)
                {
                    if (xmlnode.ChildNodes.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        ParentTop = xmlnodeParentINFO.ChildNodes.Item(0).InnerText.ToString();
                        ParentBottom = xmlnodeParentINFO.ChildNodes.Item(1).InnerText.ToString();
                        ParentLeft = xmlnodeParentINFO.ChildNodes.Item(2).InnerText.ToString();
                        ParentRight = xmlnodeParentINFO.ChildNodes.Item(3).InnerText.ToString();

                        top = xmlnode.ChildNodes.Item(0).InnerText.ToString();
                        bottom = xmlnode.ChildNodes.Item(1).InnerText.ToString();
                        left = xmlnode.ChildNodes.Item(2).InnerText.ToString();
                        right = xmlnode.ChildNodes.Item(3).InnerText.ToString();
                        return true;
                    }
                }
                else
                {
                    LogWrite("ParentWindow Not Defined in Object Definition File.");
                    LogWrite("");
                    return false;
                }
            }
            else
            {
                return true;  //XMLDoc ObjectDefinition file is already loaded. All values are set.. 
            }
        }
    }
}
