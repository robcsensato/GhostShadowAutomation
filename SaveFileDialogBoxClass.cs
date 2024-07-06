using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Automation
{

    public class SaveFileDialogBoxClass
    {
        SaveFileDialog fileSave = new SaveFileDialog();
        string TypeOfFileBeingStored = "";

        public event ReturnFileNameFromSaveDialogBoxDelegate SendFileNameToBeSaved;

        public void SaveFileDialogBox(string Title, string FileType)
        {
             fileSave.Title = Title;
             fileSave.InitialDirectory = ".\\";

             if (FileType == "PlayBack")
             {
                fileSave.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                TypeOfFileBeingStored = "PlayBack";
             }

            if (FileType == "ObjectFile")
            {
                fileSave.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                TypeOfFileBeingStored = "ObjectFile";
            }

            fileSave.FilterIndex = 0;
            fileSave.RestoreDirectory = false;

            fileSave.FileOk +=new System.ComponentModel.CancelEventHandler(fileSave_FileOk);
            fileSave.ShowDialog();
        }

        void  fileSave_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
 	      SendFileNameToBeSaved(fileSave.FileName,TypeOfFileBeingStored);
        }
    }
}
