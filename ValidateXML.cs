using System;
using System.Collections.Generic;
//using System.Text;
using System.Xml;

namespace Automation
{
    public class ValidateXML
    {
        private XmlDocument obj = new XmlDocument();
        bool value;

        public bool IsObjectiveWithinXML(string XMLFile, string ObjectiveName)
        {
            obj.Load(XMLFile);

            XmlElement root = obj.DocumentElement;

            try
            {
               value = root.SelectSingleNode("//Objective[@name='" + ObjectiveName + "']").HasChildNodes;
               
            }
            catch
            {
               value = false;
            }

            obj.RemoveAll();
            return value;
        }

        public bool IsThereAnObjectiveBeingUsedWithinXML(string XMLFile)
        {
            obj.Load(XMLFile);

            XmlElement root = obj.DocumentElement;

            if (root.SelectSingleNode("//Objective") != null)
            {

                try
                {
                    value = root.SelectSingleNode("//Objective").HasChildNodes;

                }
                catch
                {
                    value = false;
                }
            }
            else
            {
                value = false;
            }

            obj.RemoveAll();
            return value;
        
        }
    }
}
