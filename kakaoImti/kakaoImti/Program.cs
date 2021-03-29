using System;
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

        public static List<IntPtr> GetRootWindowsOfProcess(int pid)
        {
            List<IntPtr> rootWindows = GetChildWindows(IntPtr.Zero);
            List<IntPtr> dsProcRootWindows = new List<IntPtr>();
            foreach (IntPtr hWnd in rootWindows)
            {
                uint lpdwProcessId;
               
                GetWindowThreadProcessId(hWnd, out lpdwProcessId);

                Console.WriteLine(lpdwProcessId.ToString("X"));
               
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


        public const int WM_LBUTTONDOWN = 513;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int VK_TAB = 0x09;

        [STAThread]
        static void Main()
        {


            IntPtr talking = FindWindow(null, "박동환");
            Console.WriteLine(talking.ToString("X"));

            IntPtr processId = GetWindowThreadProcessId(talking, 0);

            Console.WriteLine(processId.ToString("X"));

            List<IntPtr> result = GetChildWindows(processId);

            foreach(IntPtr w in result)
            {
                Console.WriteLine(w);
            }

            //if(talking != IntPtr.Zero)
            //{
            //    Thread.Sleep(4000);
            //    Console.WriteLine("start");

            //    IntPtr result = IntPtr.Zero;
            //    do
            //    {
            //        StringBuilder caption = new StringBuilder(260);

            //        result = Connect.FindWindowEx(talking, result, null, null);

            //        if (Connect.GetClassName(result, caption, 260) > 0)
            //        {
            //            Console.WriteLine(caption);
            //        }

            //    }
            //    while (result != IntPtr.Zero);


            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

     
    }


}
