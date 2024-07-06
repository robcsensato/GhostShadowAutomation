using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
using System.Windows.Forms;
using System.Threading;

namespace Automation
{
    public class PlayBackKeyBoardEvents
    {
        private void BufferResponse(int time)
        {
            Thread.Sleep(time);
        }

        //public void KeyBoardEvent(XmlNode n)
        public void KeyBoardEvent(string UserAction, string UserTextData)
        {
            //string ComboString;
            //string NewComboString;
            //string eventID;
            //string keyPressed;
            //string UserAction;

            //LogWrite("KeyBoard Event Found");

            //eventID = n.ChildNodes.Item(0).InnerText;
            //LogWrite("KeyBoard Event ID: " + eventID);

            //UserAction = n.ChildNodes.Item(1).Name.ToString();

            switch (UserAction)
            {
                case "combo":
                    {
                        //string ComboString = "{" + UserTextData + "}";
                        //ComboString = UserTextData.ToLower();
                       

                       // NewComboString = ComboString.Replace("RControl+", "^");
                       // NewComboString = ComboString.Replace("lcontrol+", "^");
                        //ComboString.ToLower();
                        //BufferResponse(300);

                        try
                        {
                            SendKeys.SendWait(UserTextData);
                            //SendKeys.SendWait("^s");
                            BufferResponse(300);
                        }
                        catch
                        { 
                            // Error Handling.. In case Send Command doesn't work..
                        }
                        break;
                    }

                case "typed":
                    {
                      //  LogWrite("");
                      //  LogWrite("Sending Text: " + n.ChildNodes.Item(1).InnerText);
                      //  LogWrite("");

                        BufferResponse(300);
                        //SendKeys.SendWait(n.ChildNodes.Item(1).InnerText);
                        try
                        {
                            SendKeys.SendWait(UserTextData);
                        }
                        catch (Exception ex)
                        { 
                        // Did not recognize what was sent...
                        // Create a resonsable error handler...
                        // This can happen if they type {SHIFT}+k... This is not legal..
                        // Maybe Check For {SHIFT} or {CONTROL} to place in the proper charater...
                        
                        // Create a LogError To PlayBack for a detailed message stating it did not understand.
                        // Maybe a Break in the thread for allow to move on or correct the XML??
                        }
                        break;
                    }

                case "pressed":
                    {
                        //keyPressed = n.ChildNodes.Item(1).InnerText;

                        switch (UserTextData)
                        {
                            case "RETURN":
                                {
                             //       LogWrite("");
                             //       LogWrite("Hitting Enter Key");
                             //       LogWrite("");
                                    SendKeys.SendWait("{Enter}");
                                    BufferResponse(300);
                                    break;
                                }
                            case "SOME_OTHER_KEY":
                                {
                                    //Some other Special Key to send. Example: TAB
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        BufferResponse(300);
                        //SendKeys.SendWait(n.ChildNodes.Item(1).InnerText);
                        SendKeys.SendWait(UserTextData);
                        break;
                    }
            }
        }
    }
}
