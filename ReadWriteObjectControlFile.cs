using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Automation
{
    public class ReadWriteObjectControlFile
    {
        ArrayList ObjectiveList = new ArrayList();

        private static string dir = @"C:\my_dir";
        public string path = dir + "objectcontroller.txt";

        public ArrayList ReadFile(string filename)
        {
            ObjectiveList.Clear();

            //StreamReader textIn = new StreamReader(new FileStream(path, FileMode.OpenOrCreate,FileAccess.Read));

            //while (textIn.Peek() != -1)
            //{
            //    string row = textIn.ReadLine();
            //    char[] sep = {','};
            //    String[] columns = row.Split(sep);
            //    string value = columns.GetValue(0).ToString();

            //    ObjectiveList.Add(value);
           // }

            StreamReader re = File.OpenText(filename);
            string input = null;
            while ((input = re.ReadLine()) != null)
            {
                //Console.WriteLine(input);
                //ObjectiveList.Add(input);

                char[] sep = { ',' };
                String[] columns = input.Split(sep);
                object ObjectiveName = columns.GetValue(0);
                object counter = columns.GetValue(1);
                ObjectiveList.Add(ObjectiveName);
                ObjectiveList.Add(counter);
            }

            re.Close();


            return ObjectiveList;
        }

        
    }
}
