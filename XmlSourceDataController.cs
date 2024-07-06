using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Automation
{
    public class XmlSourceDataController
    {
        public event LogWriteFromXMLSourceDataControllerDelegate LogWrite;  

        XmlDocument doc = new XmlDocument();
        XmlNodeList nodeList;
        List<XmlNodeList> MyList = new List<XmlNodeList>();

        string LastKnowValue = "";

        
        public void LoadXml(string File)
        {
            doc.Load(File);
        }

        public void ProcessXMLSource()
        {
            clearlist(); // Clear the Generic List
            LoopThroughSourceXML(doc.SelectSingleNode("//*"));
        }

        private void LoopThroughSourceXML(XmlNode root)
        {
            int count = 0;

            foreach (XmlNode n in root.ChildNodes)
            {
              string SourceTagName = n.Name;
              //LogWrite("Before SourceTagName: " + SourceTagName);
              //LogWrite("Count: " + count.ToString());
              
                  if (count == 0)
                  {
                      PopulateListOfNodes("*", SourceTagName);
                      //LogWrite("First SourceTagName: " + SourceTagName);
                      //LogWrite("List Count is Now from first: " + MyList.Count);
                  }
                  else
                  {
                      if (TagNameNotWithinList(SourceTagName))
                          PopulateListOfNodes("*", SourceTagName);
                  }
              
                count++;
             }
        }

        private bool TagNameNotWithinList(string SourceName)
        {
            int NodeListCount = MyList.Count;

            if (MyList.Count > 0 && MyList != null)
            {
                for (int I = 0; I < NodeListCount; I++)
                {
                    if (MyList[I].Item(0).Name == SourceName)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int PopulateListOfNodes(string rootnode, string data)
        {
            int listcount = 0;
            XmlNode root = doc.DocumentElement;

            try
            {
                string newexpression = "//" + rootnode + "/" + data;
                nodeList = root.SelectNodes(newexpression);
                MyList.Add(nodeList);
                listcount = nodeList.Count;
            }
            catch (Exception ex)
            {
               //send a error report. Data not found: ex
                LogWrite("Error: " + ex.ToString());
            }

            //LogWrite("Populate List of Nodes:-> List Count is Now: " + listcount.ToString());
            //LogWrite("Data: " + data);

            LogWrite("Found '" + data + "' within SourceXML. Total Number Found: " + listcount.ToString());

            return listcount;
        }

        public string RetrieveInfoFromSourceXML(string data, int index)
        { 
            // First Search MyList for a nodeList that contains 'data'
            int NumberOfNodes = MyList.Count;
           
            for (int I=0; I < NumberOfNodes; I++)
            {
                try
                {
                    if (MyList[I].Item(0).Name == data)
                    {
                        LastKnowValue = MyList[I].Item(index).InnerText;
                        return MyList[I].Item(index).InnerText;
                    }
                }
                catch
                {
                    return LastKnowValue;
                }
            }
            return "NONE-ERROR-999";
        }

        public void clearlist()
        {
            MyList.Remove(nodeList);
        }
    }
}
