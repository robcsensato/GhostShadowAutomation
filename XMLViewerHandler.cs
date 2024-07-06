using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    class XMLViewerHandler
    {
        delegate void MyDelegate();

        public event TreeViewControlerDelegate TreeviewControl;
        public event ListBoxControlerDelegate ListBoxControl;
        public event TreeViewRootNodeDelegate ReturnTreeViewRootNode;
        public event PopulateTreeDelegate PopulateTreeView;
        public event SendRECORDFileInfo GetLatestXMLFile;
        public event ListBoxAddToList AddtoListBox;
        public event ListBoxSetSelected SetSelectedListBox;
        public event MoveToLineDelegate MoveToListinBoxLine;

        enum VIEW { TREE_VIEW = 0 };

        string FileSize = "";
        //string OrigFormTitle = ""; --> No Longer Used
        bool bFileLoaded = false;
        //int CurrentView = (int)VIEW.TREE_VIEW; --> No Longer Used
        Object NodeTag = null;
        //Thread t = null; --> No longer necessary
        TreeNode RootNode = null;
        Point ClickedPoint = new Point(0, 0);
        ArrayList TreeNodeArray = new ArrayList();
        ImageList TreeImage = new ImageList();

        string GlobalXMLFile;

        public void SETUP_PROCEDURE(string WorkingDir, string XMLInputFile)
        {
            //RecorderedViewer.Nodes.Clear(); //--> Send event back to clear treeview
            //RecorderedListBox.Items.Clear();  //--> Send event back to clear listbox1

            GlobalXMLFile = XMLInputFile;
            
            string action = "clear";

            TreeviewControl(action,null);
            ListBoxControl(action);

            TreeNodeArray.Clear();

            bFileLoaded = false;

            FileInfo f = new FileInfo(XMLInputFile);
            FileSize = f.Length.ToString();

            TreeImage.Images.Add(new Icon(WorkingDir + "\\ROOT.ICO"));		//ROOT		0
            TreeImage.Images.Add(new Icon(WorkingDir + "\\ELEMENT.ICO"));	//ELEMENT	1
            TreeImage.Images.Add(new Icon(WorkingDir + "\\EQUAL.ico"));		//ATTRIBUTE	2

            TreeviewControl(null, TreeImage);
            //RecorderedViewer.ImageList = TreeImage; --> No longer needed

            // Begin thread to read input file and load into the ListBox
            Thread populateListBoxThread = new Thread(new ThreadStart(PopulateList));
            populateListBoxThread.Start();

            // Begin thread to read input file and populate the Tree
            Thread populateTreeViewThread = new Thread(new ThreadStart(PopulateTree));
            populateTreeViewThread.Start();
        }

        private void PopulateList()
        {
            // Load the File
            LoadFileIntoListBox();
        }

        private void PopulateTree()
        {
            // TreeView Nodes cannot be added in a thread , until the thread is marshalled
            // using an Invoke or beginInvoke call.
            // We create a delegate ( Funtion Pointer ) and invoke the thread using he delegate

            PopulateTreeView();
            //MyDelegate dlg_obj;
            //dlg_obj = new MyDelegate(ParseFile);
            //RecorderedViewer.Invoke(dlg_obj); 
        }

        public void ParseFile()
        {
            // Use the XmlTextReader class to read the XML File and populate the
            // treeview
            try
            {
                string XMLFile = GetLatestXMLFile();

                XmlTextReader reader = null;
                reader = new XmlTextReader(XMLFile);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                string readerName = "";
                bool start_node = false;
                int depth = 0;
                TreeNode WORKINGNODE = null;
                RootNode = null;
                TreeNode AttrNode = null;
                TreeNode newNode = null;
                bool bIsEmpty = false;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            readerName = reader.Name;
                            bIsEmpty = reader.IsEmptyElement;

                            if (!start_node)
                            {
                                start_node = true;

                                //RootNode = this.RecorderedViewer.Nodes.Add(readerName); //-> No longer needed
                                RootNode = ReturnTreeViewRootNode(readerName);

                                AssociateTag(RootNode, reader.LineNumber);
                                RootNode.SelectedImageIndex = 0;
                                RootNode.ImageIndex = 0;
                                continue;
                            }
                            depth = reader.Depth;

                            if (reader.IsStartElement() && depth == 1)
                            {
                                WORKINGNODE = RootNode.Nodes.Add(reader.Name);
                                AssociateTag(WORKINGNODE, reader.LineNumber);
                            }
                            else
                            {
                                TreeNode parent = WORKINGNODE;
                                WORKINGNODE = parent.Nodes.Add(reader.Name);
                                AssociateTag(WORKINGNODE, reader.LineNumber);
                            }

                            WORKINGNODE.SelectedImageIndex = 1;
                            WORKINGNODE.ImageIndex = 1;

                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                string rValue = reader.Value.Replace("\r\n", " ");

                                AttrNode = WORKINGNODE.Nodes.Add(reader.Name);
                                //          AttrNode = WORKINGNODE.Nodes.Add(reader.Name +"="+rValue);
                                AssociateTag(AttrNode, reader.LineNumber);

                                AttrNode.SelectedImageIndex = 1;
                                AttrNode.ImageIndex = 1;
                                TreeNode tmp = AttrNode.Nodes.Add(rValue);
                                tmp.SelectedImageIndex = 2;
                                tmp.ImageIndex = 2;
                                AssociateTag(tmp, reader.LineNumber);

                                AttrNode.SelectedImageIndex = 2;
                                AttrNode.ImageIndex = 2;
                            }

                            if (bIsEmpty)
                                WORKINGNODE = WORKINGNODE.Parent;
                            break;

                        case XmlNodeType.Text:
                            {
                                string rValue = reader.Value.Replace("\r\n", " ");
                                newNode = WORKINGNODE.Nodes.Add(rValue);
                                AssociateTag(newNode, reader.LineNumber);
                                newNode.SelectedImageIndex = 2;
                                newNode.ImageIndex = 2;
                            }
                            break;

                        case XmlNodeType.EndElement:
                            WORKINGNODE = WORKINGNODE.Parent;
                            break;
                    }
                }
                reader.Close();
                RootNode.Expand();
            }
            catch (Exception eee)
            {
                Console.WriteLine(eee.Message);
            }
        } // End Parse File

        private void LoadFileIntoListBox()
        {
            // Load the xml file into a listbox.
            try
            {
                StreamReader sr = new StreamReader(GlobalXMLFile, Encoding.ASCII);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                while (sr.Peek() > -1)
                {
                    Thread.Sleep(5);
                    string str = sr.ReadLine();
                    //RecorderedListBox.Items.Add(str); --> No longer needed
                    AddtoListBox(str);
                }
                sr.Close();
                bFileLoaded = true;
                //RecorderedListBox.SetSelected(1, true); --> No longer needed
                SetSelectedListBox();
            }
            catch (Exception ee)
            {
                Console.WriteLine("Error Reading File into ListBox " + ee.Message);
            }
        } // End of LoadFileIntoListBox

        private void AssociateTag(TreeNode t, int l)
        {
            // Associate a line number Tag with every node in the tree
            NodeTag = new Object();
            NodeTag = l;
            t.Tag = NodeTag;
        } // End of AssociateTag


        public void RecorderedViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!bFileLoaded) return;

            // The treenode is selected. Every node is tagged with LineNumber ( from input file ).
            // This allows us to jump to the line in the file.

            TreeNode tn;
            tn = (TreeNode)e.Node;
            Object ln = tn.Tag;
            int line = Convert.ToInt32(ln.ToString());
            MoveToLine(line);
        }

        private void MoveToLine(int ln)
        {
            // Select the input line from the file in the listbox
            // RecorderedListBox.SetSelected(ln - 1, true); --> no longer needed
            MoveToListinBoxLine(ln);
        }

    }
}
