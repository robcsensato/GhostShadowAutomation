using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Automation
{
    public class OpenFileDialogBoxClass
    {
        OpenFileDialog fileOpen = new OpenFileDialog();

        public event ReturnFileNameFromDialogBoxDelegate ReturnFileNameEvent;

        string TypeOfFileBeingStored = "";

        public void OpenFileDialogBox(string Title, string FileType)
        {
            fileOpen.Title = Title;
            fileOpen.InitialDirectory = ".\\";

            if (FileType == "PlayBack")
            {
                fileOpen.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                TypeOfFileBeingStored = "PlayBack";
            }

            if (FileType == "ObjectFile")
            {
                fileOpen.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                TypeOfFileBeingStored = "ObjectFile";
            }
                             
            if (FileType == "ObjectiveController")
            {
                fileOpen.Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
                TypeOfFileBeingStored = "ObjectiveController";            
            }

            fileOpen.FilterIndex = 0;
            fileOpen.RestoreDirectory = false;
            
            fileOpen.FileOk += new System.ComponentModel.CancelEventHandler(fileOpen_FileOk);
            fileOpen.ShowDialog();
        }

        void fileOpen_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ReturnFileNameEvent(fileOpen.FileName, TypeOfFileBeingStored); 
        }
    }
}
