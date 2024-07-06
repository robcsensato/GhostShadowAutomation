using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Automation
{
    public class HTMLSearchResult
    {
        public event MoveXMLFileFromHTMLSearchResultDelegate SendWebTableXMLToMainForViewingInDataView;

        public event RelayWebTableDataFromHTMLSearhResults SendWebTableResults;

        public List<WebTableDataClass> listOfEntries = new List<WebTableDataClass>();

        XMLWriter XmlWrite = new XMLWriter();

        public void start_processing(string html_table, string outputxml)
        {
           // string html_table_caps = html_table.ToUpper();
            // Create a xml file from html_table
            FileStream fs = new FileStream("temp.xml", FileMode.Create,
                FileAccess.ReadWrite, FileShare.None);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                //sw.Write(html_table);
                sw.WriteLine("<?xml version='1.0'?>");
                sw.WriteLine(html_table);
                sw.Close();
            }

            ReadFromXMLFile_SearchForTable_WriteNewXML(outputxml);
        }

        private void ReadFromXMLFile_SearchForTable_WriteNewXML(string outputxml)
        {
           // int tr_counter = 1;
           // listOfEntries.Clear();

            XmlDocument doc = new XmlDocument();
            //doc.Load(@".\webtable_output.xml");
            doc.Load(@".\temp.xml");

            XmlWrite.initialize_webtable(outputxml);

            // Find out How Many <TD> Tags they are

            // .....<TD>
            // .........<TR>10</TR>
            // .........<TR>15</TR>
            // .....</TD>
            // .....<TD>
            // .........<TR>20</TR>
            // .........<TR>25</TR>
            // .....</TD>

            int NumberOfTDTag = doc.GetElementsByTagName("TD").Count / 2;

            for (int I = 1; I < NumberOfTDTag+1; I++)
            {
                //--> This will write <TD column="1"></TD>
                //-----> Next Loop will write <TD column="2"></TD>
                XmlWrite.WriteTDTagToWebTable(outputxml,I);
            }

            try
            {
                foreach (XmlNode node in doc.GetElementsByTagName("TR"))
                {
                  int counter = 1;
                 
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        // Write to XML to outputxml
                        int temp = node.ChildNodes.Count;
                        XmlWrite.updatewebtable(outputxml, childNode.InnerText, counter);
                        counter++;
                    }
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("Error: " + e.Message + "\r\n" + e.StackTrace);
                
            }
        }

        public void start_processing(string html_table)
        {
            string html_table_caps = html_table.ToUpper();
            // Create a xml file from html_table
            FileStream fs = new FileStream("webtable_output.xml", FileMode.Create,
                FileAccess.ReadWrite, FileShare.None);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                //sw.Write(html_table);
                sw.WriteLine("<?xml version='1.0'?>");
                sw.WriteLine(html_table_caps);
            }

            ReadFromXMLFile_SearchForTable();

            SendWebTableXMLToMainForViewingInDataView("webtable_output.xml");

            SendWebTableResults(listOfEntries);
        }

        private void ReadFromXMLFile_SearchForTable()
        {
            int tr_counter = 1;
            listOfEntries.Clear();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\webtable_output.xml");

                foreach (XmlNode node in doc.GetElementsByTagName("TR"))
                {
                    int td_counter = 1;

                    foreach (XmlNode childNode in node.ChildNodes)
                    {      
         listOfEntries.Add(new WebTableDataClass(childNode.InnerText,tr_counter,td_counter));     

                       td_counter++;
                    }
                    
                    tr_counter++;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error: " + e.Message + "\r\n" + e.StackTrace);
            }
        }

    }
}
