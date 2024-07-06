using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Automation
{
    class PlayBackMouseEvents
    {
        Point pt = Cursor.Position;

        private void BufferResponse(int time)
        {
            Thread.Sleep(time);
        }

        public void MouseActions(int x, int y, string ButtonUsed, string MouseEventType)
        {
            const UInt32 MouseEventLeftDown = 0x0002;
            const UInt32 MouseEventLeftUp = 0x0004;
            const UInt32 MouseEventRightDown = 0x0008;
            const UInt32 MouseEventRightUp = 0x0010;
            const UInt32 MouseEventMiddleDown = 0x0020;
            const UInt32 MouseEventMiddleUp = 0x0040;

            int DoubleClickTime = Win32.GetDoubleClickTime();

            int start_x;
            int start_y;

            int diff_x;
            int diff_y;

            int y_new_position;

            double b;
            double m;

            double y_position;

            double diff_x_convert;
            double diff_y_convert;

            // Getting ready to move the mouse
            // using formula y=mx+b
            // First solve for m then b
            // m = (end_y - start_y) / (end_x - start_x)
            // **IMPORTANT** Have to make sure end_x - start_x is not zero

            //**** TAKING OUT FOR NOW *****
            // if (first_start_of_ghost)
            // {
            //    pt.X = 576;
            //    pt.Y = 376;
            //
            //    Cursor.Position = pt;
            //    first_start_of_ghost = false;
            // }
            ////////////////////////////////

            start_x = pt.X;
            start_y = pt.Y;

            diff_x = x - start_x;
            diff_y = y - start_y;

            diff_x_convert = Convert.ToDouble(diff_x);
            diff_y_convert = Convert.ToDouble(diff_y);

            diff_x_convert = diff_x_convert + 0.01;
            diff_y_convert = diff_y_convert + 0.01;

            // diff_x_convert will not equal zero. 

            m = diff_y_convert / diff_x_convert;
            b = start_y - (m * start_x);

            if (x > start_x)
            {
                for (int a = start_x; a < x + 1; a++)
                {
                    y_position = m * a + b;
                    y_new_position = Convert.ToInt32(y_position);

                    pt.X = a;
                    pt.Y = y_new_position;
                    Cursor.Position = pt;
                    Thread.Sleep(1);
                }
            }
            else
            {
                for (int a = start_x; a > x - 1; a--)
                {
                    y_position = m * a + b;
                    y_new_position = Convert.ToInt32(y_position);

                    pt.X = a;
                    pt.Y = y_new_position;
                    Cursor.Position = pt;
                    Thread.Sleep(1);
                }
            }

            // Get Current X and Y Positions
            pt.X = x;
            pt.Y = y;

            // Set Current Position
            Cursor.Position = pt;

            switch (ButtonUsed)
            {
                // Layout for Left button
                case "Left":
                    {
                        switch (MouseEventType)
                        {
                            case "clicked":
                                {
                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);

                                    BufferResponse(200);
                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "UP":
                                {
                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "DOWN":
                                {
                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }
                            case "doubleclicked":
                                {
                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    //BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);

                                    BufferResponse(DoubleClickTime);

                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    //BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    //BufferResponse(200);

                                    break;
                                }
                        }
                        break;
                    } // END of Left Button Case

                // Layout for Right button
                case "Right":
                    {
                        switch (MouseEventType)
                        {
                            case "clicked":
                                {
                                    Win32.mouse_event(MouseEventRightDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);

                                    BufferResponse(200);
                                    Win32.mouse_event(MouseEventRightUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "UP":
                                {
                                    Win32.mouse_event(MouseEventRightUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "DOWN":
                                {
                                    Win32.mouse_event(MouseEventRightDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "doubleclicked":
                                {
                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);

                                    break;
                                }

                        }

                        break;
                    } // End of Right Button Case

                case "Middle":
                    {
                        switch (MouseEventType)
                        {
                            case "clicked":
                                {
                                    Win32.mouse_event(MouseEventMiddleDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);

                                    BufferResponse(200);
                                    Win32.mouse_event(MouseEventMiddleUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "UP":
                                {
                                    Win32.mouse_event(MouseEventMiddleUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "DOWN":
                                {
                                    Win32.mouse_event(MouseEventMiddleDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);
                                    break;
                                }

                            case "doubleclicked":
                                {
                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftDown, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    // create: DelayForClick();
                                    // Same as BufferResponse(200);
                                    BufferResponse(200);

                                    Win32.mouse_event(MouseEventLeftUp, 0, 0, 0, new System.IntPtr());
                                    //Thread.Sleep(200);
                                    BufferResponse(200);

                                    break;
                                }
                        }

                        break;
                    } // END of Middle Button Case

                //  NEW CASE STATEMENT FOR FUTURE BUTTON GOES HERE
                //  CASE "NEW BUTTON":
                //  {
                //     ..
                //   break;
                //  }

            } // 
        }
    }
}
