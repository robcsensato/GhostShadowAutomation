using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;

namespace Automation
{
    public class WaitClass
    {
        public void StartWaiting(XmlNode n)
        {
           // default value
           string Format = "milliseconds";

           string TimeValue = n.ChildNodes.Item(0).InnerText.ToString();

             if (n.Attributes.Count > 0) // using control file
                {
                    if (n.Attributes.Item(0).Name == "time")
                    {
                      Format = n.Attributes.Item(0).Value;
                    }
                }

            int TimeInt = Convert.ToInt16(TimeValue);

            BufferResponse(ConvertToMillisecond(TimeInt,Format));
            // 
        }
   
        private void BufferResponse(int time)
        {
            Thread.Sleep(time); // waits in milliseconds
        }

        private int ConvertToMillisecond(int time, string format)
        {
            switch (format)
            {
                case "seconds":
                    {
                        time = time * 100; 
                        // If Time > 5 Seconds.. Send Event back to PlayMouseEvents 
                        // that this is longer than 5 seconds. Replay Event back to ManForm.
                        // This can use the LogwriteToMainForm Event on PlayBack.
                        // Must have an Event saying this longer than 5 seconds
          
                        break;
                    }
                case "minutes":
                    {
                        time = time * 1000;
                        // If Time > 1 Minutes.. Send Event back to PlayMouseEvents 
                        // that this is longer than 1 minute. Replay Event back to ManForm.
                        // This can use the LogwriteToMainForm Event on PlayBack.
                        // Must have an Event saying this longer than 1 minute
                        break;
                    }

                default:
                    {
                        // If Time > .. Send Event back to PlayMouseEvents
                        time = time * 100;
                        // that this is longer than 5 seconds. Replay Event back to ManForm.
                        // This can use the LogwriteToMainForm Event on PlayBack.
                        // Must have an Event saying this longer than ..
                        break;
                    }
            }
            return time;
        }

    }
}
