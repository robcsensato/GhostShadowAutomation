using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Automation
{
    public class ReadSeperateXMLFile
    {
        public event ReadingNewXMLWithObjectivesDelegate TriggerReadingNewXMLWithObjectives;
        public event ReadingNewXMLWithOUTObjectives TriggerReadingNewXMLWITHOUTObjective;

        string controlfile="";
        string objectfile = "";

        public void ReadXmlFile(XmlNode n)
        {
            //usage:
            // <XMLFILE objectivecontrolfile="controlfile" objectfile="objectfile">XMLFILE1</XMLFILE>

            string XMLFile = n.ChildNodes.Item(0).InnerText.ToString();

            if (n.Attributes.Count > 0) // using control file
            {
                if (n.Attributes.Item(0).Name == "objectivecontrolfile")
                {
                    controlfile = n.Attributes.Item(0).Value;
                }

                if (n.Attributes.Item(1).Name == "objectfile")
                {
                    objectfile = n.Attributes.Item(1).Value;
                }

                TriggerReadingNewXMLWithObjectives(XMLFile, controlfile, objectfile);

            }
            else // not using control file
            {
               TriggerReadingNewXMLWITHOUTObjective(XMLFile);
            }
        } // END OF READXMLFILE Method
    }
}
