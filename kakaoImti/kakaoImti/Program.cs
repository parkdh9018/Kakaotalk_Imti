﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace kakaoImti
{
    class Program
    {
        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // 탑레벨 윈도우 중에서 원하는 윈도우 찾는 함수

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpClassName, string lpWindowName);
        //자식 윈도우 찾는 함수

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        [DllImport("user32")]
        public static extern int GetParent(int hwnd);

        public static List<IntPtr> GetRootWindowsOfProcess(int pid)
        {
            List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
            List<IntPtr> dsProcRootWindows = new List<IntPtr>();
            foreach (IntPtr hWnd in rootWindows)
            {
                uint lpdwProcessId;
               
                GetWindowThreadProcessId(hWnd, out lpdwProcessId);

                if (lpdwProcessId == pid)
                    dsProcRootWindows.Add(hWnd);
                
            }
            return dsProcRootWindows;
        }

        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                Win32Callback childProc = new Win32Callback(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            return result;
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        private static List<IntPtr> FindAllWindowEx(IntPtr parent, String className, String windowName)
        {
            List<IntPtr> result = new List<IntPtr>();

            IntPtr window = IntPtr.Zero;

            do
            {
                window = FindWindowEx(parent, window, className, windowName);
                if(window != IntPtr.Zero)
                    result.Add(window);

            }
            while (window != IntPtr.Zero);

            return result;
        }

        private static void PrintAllWindow(int d,IntPtr parent)
        {


            List<IntPtr> result = FindAllWindowEx(parent, null, null);

            StringBuilder caption = new StringBuilder(260);
            StringBuilder className = new StringBuilder(260);

            foreach (IntPtr p in result)
            {
                GetClassName(p, className, 260);
                GetWindowText(p, caption, 260);

                Console.WriteLine("{4} {0} className:{1}, caption:{2}, parent:{3}", p.ToString("X"), className, caption, GetParent(p.ToInt32()).ToString("X"), new String('-', d));
                PrintAllWindow(d+1, p);

            }
        }

        public const int WM_LBUTTONDOWN = 513;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int VK_TAB = 0x09;

        [STAThread]
        static void Main()
        {
            StringBuilder caption = new StringBuilder(260);
            StringBuilder className = new StringBuilder(260);

            Thread.Sleep(3000);
            List<IntPtr> result = GetRootWindowsOfProcess(0x4678);

            ////////////////////////////////

            foreach (IntPtr w in result)
            {
                //Console.WriteLine(w);


                GetClassName(w, className, 260);
                GetWindowText(w, caption, 260);

                Console.WriteLine("handle: {3}, captin : {0}, className : {1}, parent: {2}", caption, className, GetParent(w.ToInt32()).ToString("X"), w.ToString("X"));
            }

            //////////////////////////////////////////



            IntPtr EVA_WINDOW_DB = IntPtr.Zero;

            EVA_WINDOW_DB = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "EVA_Window_Dblclk",null);

            PrintAllWindow(1, EVA_WINDOW_DB);

            //List<IntPtr> EVA_List = FindAllWindowEx(IntPtr.Zero, "EVA_Window_Dblclk", null);

            //foreach(IntPtr p in EVA_List)
            //{
            //    Console.WriteLine("EVA {0} parent:{1}", p.ToString("X"), GetParent(p.ToInt32()).ToString("X"));
            //    List<IntPtr> EVAChildList = FindAllWindowEx(p, null, null);

            //    foreach(IntPtr c in EVAChildList)
            //    {

            //        GetClassName(c, className, 260);
            //        GetWindowText(c, caption, 260);

            //        Console.WriteLine("- {0} className:{1}, caption:{2}, parent:{3}", c.ToString("X"), className, caption, GetParent(c.ToInt32()).ToString("X"));

            //        List<IntPtr> child_List = FindAllWindowEx(c, "EVA_ChildWindow", null);

            //        foreach(IntPtr c2 in child_List)
            //        {
            //            GetClassName(c2, className, 260);
            //            GetWindowText(c2, caption, 260);

            //            Console.WriteLine("-- {0} className:{1}, caption:{2}, parent:{3}", c2.ToString("X"), className, caption, GetParent(c2.ToInt32()).ToString("X"));
            //        }

            //    }

            //}



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

     
    }


}
