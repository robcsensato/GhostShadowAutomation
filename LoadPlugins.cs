using System;
using System.Collections.Generic;
using System.Text;

namespace Automation
{
    public class LoadPlugins
    {
        public string PluginAuthor = "";
        public string PluginDescription = "";
        public string PluginVersion = "";
        bool dll_found = false;

        public event DisplayPlayBackEventMessage sendMessenger;

        PluginServices Plugins = new PluginServices();

        public void SetUpDelegate()
        {
            Plugins.SendMessage += new DisplayPlayBackEventMessage(Plugins_SendMessage);
        }

        void Plugins_SendMessage(string Message)
        {
            sendMessenger(Message);
        }

        public bool CheckToSeeifPlugExists(string StartUpPath, string Name)
        {
            dll_found = false;

            //Global.Plugins.FindPlugins(StartUpPath);
            Plugins.FindPlugins(StartUpPath);

            //foreach (Types.AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
            foreach (Types.AvailablePlugin pluginOn in Plugins.AvailablePlugins)
            {
                if (pluginOn.Instance.Name == Name)
                {
                    PluginAuthor = pluginOn.Instance.Author;
                    PluginDescription = pluginOn.Instance.Description;
                    PluginVersion = pluginOn.Instance.Version;
                    dll_found = true;
                    break;
                }
            }

            return dll_found;
            
        }

        //Call the find plugins routine, to search in our Plugins Folder
        //public void LoadPlugin(string startup_path, string pluginName, string attribute_name, string attribute_value, string innertext)
        //{
        //    //Global.Plugins.FindPlugins(Application.StartupPath + @"\Plugins");
        //    Global.Plugins.FindPlugins(startup_path);

        //    foreach (Types.AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
        //    {
        //        if (pluginOn.Instance.Name == pluginName)
        //        {
        //            start_plugin(pluginName,attribute_name, attribute_value, innertext);
        //            break;
        //        }
        //    }
        //}

        public int start_plugin(string plugin_name,string attribute_name, string attribute_value, string innertext)
        {
            //Types.AvailablePlugin selectedPlugin = Global.Plugins.AvailablePlugins.Find(plugin_name);
            Types.AvailablePlugin selectedPlugin = Plugins.AvailablePlugins.Find(plugin_name);
           
            int plugin_value = selectedPlugin.Instance.ProcessData(attribute_name, attribute_value, innertext);
      
               
            close_plugin();

            return plugin_value;
        }

        private void close_plugin()
        {
            //Global.Plugins.ClosePlugins();
            Plugins.ClosePlugins();
        }
    }
}
