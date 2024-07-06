using System;
using System.Xml;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace Automation
{
	public class TreeViewSerializer
	{
		// Xml tag for node, e.g. 'node' in case of <node></node>
		private const string XmlNodeTag = "parentwindow";
		
		// Xml attributes for node e.g. <node text="Asia" tag="" imageindex="1"></node>
		private const string XmlNodeTextAtt = "text";
		private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        //TreeNode RootNode;

		public TreeViewSerializer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void SerializeTreeView(TreeView treeView, string fileName) 
		{
			XmlTextWriter textWriter = new XmlTextWriter(fileName, System.Text.Encoding.ASCII);
			// writing the xml declaration tag
			textWriter.WriteStartDocument();
			//textWriter.WriteRaw("\r\n");
			// writing the main tag that encloses all node tags
			textWriter.WriteStartElement("TreeView");
			
			// save the nodes, recursive method
			SaveNodes(treeView.Nodes, textWriter);
			
			textWriter.WriteEndElement();
					
			textWriter.Close();
		}

		private void SaveNodes(TreeNodeCollection nodesCollection, 
			XmlTextWriter textWriter)
		{
			for(int i = 0; i < nodesCollection.Count; i++)
			{
				TreeNode node = nodesCollection[i];
				textWriter.WriteStartElement(XmlNodeTag);
				textWriter.WriteAttributeString(XmlNodeTextAtt, node.Text);
				textWriter.WriteAttributeString(XmlNodeImageIndexAtt, node.ImageIndex.ToString());
				if(node.Tag != null) 
					textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());
				
				// add other node properties to serialize here
				
				if (node.Nodes.Count > 0)
				{
					SaveNodes(node.Nodes, textWriter);	    
				} 				
				textWriter.WriteEndElement();
			}
		}		

		public void DeserializeTreeView(TreeView treeView, string fileName)
		{
			XmlTextReader reader = null;

			try
			{
                // disabling re-drawing of treeview till all nodes are added
				treeView.BeginUpdate();				
				reader = new XmlTextReader(fileName);

				TreeNode parentNode = null;
				
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{						
						if (reader.Name == XmlNodeTag)
						{
							TreeNode newNode = new TreeNode();
							bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
							int attributeCount = reader.AttributeCount;
							if (attributeCount > 0)
							{
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									
									SetAttributeValue(newNode, reader.Name, reader.Value);
								}								
							}

                            // add new node to Parent Node or TreeView
                            if(parentNode != null)
                                parentNode.Nodes.Add(newNode);
                            else
                                treeView.Nodes.Add(newNode);

                            // making current node 'ParentNode' if its not empty
							if (!isEmptyElement)
							{
                                parentNode = newNode;
							}
														
						}						                    
					}

                    // moving up to in TreeView if end tag is encountered
					else if (reader.NodeType == XmlNodeType.EndElement)
					{
						if (reader.Name == XmlNodeTag)
						{
							parentNode = parentNode.Parent;
						}
					}
					else if (reader.NodeType == XmlNodeType.XmlDeclaration)
					{ //Ignore Xml Declaration                    
					}
					else if (reader.NodeType == XmlNodeType.None)
					{
						return;
					}
					else if (reader.NodeType == XmlNodeType.Text)
					{
						//parentNode.Nodes.Add(reader.Value);

                        // add new node to Parent Node or TreeView
                        if (parentNode != null)
                            parentNode.Nodes.Add(reader.Value);
                        else
                            treeView.Nodes.Add(reader.Value);
					}

				}
			}
			finally
			{
                // enabling redrawing of treeview after all nodes are added
				treeView.EndUpdate();      
                reader.Close();	
			}
		}

		private void SetAttributeValue(TreeNode node, string propertyName, string value)
		{
			if (propertyName == XmlNodeTextAtt)
			{                
				node.Text = value;				
			}
			else if (propertyName == XmlNodeImageIndexAtt) 
			{
				node.ImageIndex = int.Parse(value);
			}
			else if (propertyName == XmlNodeTagAtt)
			{
				node.Tag = value;
			}		
		}

        public void LoadXmlFileInTreeView(TreeView treeView, string fileName)
        {
            XmlTextReader reader = null;
            bool start_node = false;
            //RootNode = null;

            try
            {
                treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);

                TreeNode n = new TreeNode(fileName);
                //treeView.Nodes.Add(n);

                while(reader.Read())
                {
                    if(reader.NodeType == XmlNodeType.Element)
                    {
                        bool isEmptyElement = reader.IsEmptyElement;
                        StringBuilder text = new StringBuilder();
                        text.Append(reader.Name);
                        int attributeCount = reader.AttributeCount;

                        if(attributeCount > 0)
                        {
                            text.Append(" ( ");
                            for(int i = 0; i < attributeCount; i++)
                            {
                                if(i!=0) text.Append(", ");
                                reader.MoveToAttribute(i);
                                text.Append(reader.Name);
                                text.Append(" = ");
                                text.Append(reader.Value);
                            }
                            text.Append(" ) ");
                        }
            
                        if(isEmptyElement)
                        {
                                n.Nodes.Add(text.ToString());
                           
                        }
                        else
                        { 
                                n = n.Nodes.Add(text.ToString());   
                        }

                        if (!start_node)
                        {
                            start_node = true;
                            treeView.Nodes.Add(n);
                            string nameNode = n.Text;
                            //RootNode = treeView.Nodes.Add(nameNode);
                        }
 
                    }
                    else if(reader.NodeType == XmlNodeType.EndElement)
                    {
                            n = n.Parent;       
                    }
                    else if(reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                    
                    }
                    else if(reader.NodeType == XmlNodeType.None) 
                    {
                        return;
                    }
                    else if(reader.NodeType == XmlNodeType.Text)
                    {
                       n.Nodes.Add(reader.Value);
                       
                    }

                }        
            }
            finally
            {
                
                //RootNode.Expand();
                treeView.EndUpdate();
                reader.Close();
            }
        }

// BEGINNING OF NEW LOADXML_IN_LISTBOX

        // TAKE THIS OUT!!!!!!!


        // TODO: Create and Place in New Class: ListBoxSerializer

        public void LoadXmlFileInListBox(ListBox listbox, string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                //treeView.BeginUpdate();
                reader = new XmlTextReader(fileName);

                //TreeNode n = new TreeNode(fileName);
                //treeView.Nodes.Add(n);
                
                //listbox.Items.Add(fileName);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        bool isEmptyElement = reader.IsEmptyElement;
                        StringBuilder text = new StringBuilder();
                        text.Append(reader.Name);
                        int attributeCount = reader.AttributeCount;

                        if (attributeCount > 0)
                        {
                            text.Append(" ( ");
                            for (int i = 0; i < attributeCount; i++)
                            {
                                if (i != 0) text.Append(", ");
                                reader.MoveToAttribute(i);
                                text.Append(reader.Name);
                                text.Append(" = ");
                                text.Append(reader.Value);
                            }
                            text.Append(" ) ");
                        }

                        if (isEmptyElement)
                        {
                            //n.Nodes.Add(text.ToString());
                            //listbox.Items.Add(text.ToString());
                            if (text.ToString() != "Definition" || text.ToString() != "Controls")
                            {
                            
                            }

                        }
                        else
                        {
                          //  n = n.Nodes.Add(text.ToString());
                            string TextValue = text.ToString();

                            Regex NodeMatch = new Regex("position");
                            Regex NodeMatch2 = new Regex("Definition");
                            Regex NodeMatch3 = new Regex("Controls");
                            
                            if (!NodeMatch.IsMatch(TextValue))
                            {
                               if (!NodeMatch2.IsMatch(TextValue) && !NodeMatch3.IsMatch(TextValue))
                               {
                                 //if (!NodeMatch3.IsMatch(TextValue))
                                 //{
                                 listbox.Items.Add(text);
                                 //}
                               }         
                            }
  
                        }

                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        Regex NodeMatchParentWindowNode = new Regex("parent");

                        string TextValue = reader.Name.ToString();
                       
                        if (NodeMatchParentWindowNode.IsMatch(TextValue))
                        {
                            // Adding a Space in listBox
                           listbox.Items.Add("");
                        }
                      
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {

                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        //n.Nodes.Add(reader.Value);
                    }

                }
            }
            finally
            {
                listbox.EndUpdate();
                //treeView.EndUpdate();
                reader.Close();
            }
        }


// END OF ADD NEW LOADXML_IN_LISTBOX
	}
}
