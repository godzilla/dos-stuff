using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace recon
{
    public struct RECT {   
        public int left;       
        public int top;       
        public int right;       
        public int bottom;    
    }   


    public static class NativeMethods  
    {  
        public const UInt32 WM_MOVE = 0x0003;  
    
        [DllImport("kernel32.dll")]    
        public static extern IntPtr GetConsoleWindow();  
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]  
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
          
        [DllImport("user32.dll", SetLastError = true)]  
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);  
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]  
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);  
    
    }




    class Program
    {
        public static void RePositionWindow(int x, int y)   
        {   
            IntPtr windowHandle = NativeMethods.GetConsoleWindow();   
            RECT rect = new RECT();   
            NativeMethods.GetWindowRect(windowHandle, ref rect);   
            NativeMethods.MoveWindow(windowHandle, x, y, rect.right - rect.left, rect.bottom - rect.top, true);   
        }



        static void Main(string[] args)
        {

            RePositionWindow(15,15);   
            
            int width = Console.LargestWindowWidth-10;
            int height = Console.LargestWindowHeight-10;
            if (Console.WindowWidth > 80)
            {
                width = 80;
                height = 25;
            }
            int buffHieght = 32766;
            if (args.Length > 0) width = int.Parse(args[0]);
            if (args.Length > 1) height = int.Parse(args[1]);
            if (args.Length > 2) buffHieght = int.Parse(args[2]);
            string szTempPath = System.IO.Path.GetTempPath()+"ConsoleBuffer.txt";
            try
            {
                Console.SetWindowSize(width, height);
                Console.BufferHeight = buffHieght;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }
    }
}


