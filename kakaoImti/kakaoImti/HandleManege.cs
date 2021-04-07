using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace kakaoImti
{
    class HandleManege
    {
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        // 탑레벨 윈도우 중에서 원하는 윈도우 찾는 함수

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpClassName, string lpWindowName);
        //자식 윈도우 찾는 함수

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32")]
        public static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;

        int imoticonWindowWidth;
        int imoticonWindowHeight;

        public HandleManege()
        {

        }

        private int MAKEPOINT(int x, int y)
        {
            return ((y << 16) | (x & 0xFFFF));
        }

        private List<IntPtr> FindAllWindowEx(IntPtr parent, String className, String windowName)
        {
            List<IntPtr> result = new List<IntPtr>();

            IntPtr window = IntPtr.Zero;

            do
            {
                window = FindWindowEx(parent, window, className, windowName);
                if (window != IntPtr.Zero)
                    result.Add(window);

            }
            while (window != IntPtr.Zero);

            return result;
        }

        //private IntPtr getImoticonWindow()
        //{
        //    IntPtr imoticon_window = FindWindow("EVA_Window_Dblclk", null);
        //    IntPtr child_window = FindWindowEx(imoticon_window, IntPtr.Zero, "EVA_ChildWindow", null);

        //    return child_window;
        //}

        public Bitmap ImoticonWindowLoad()
        {
            Thread.Sleep(3000);
            IntPtr imoticon_window = FindWindowEx(FindWindow("EVA_Window_Dblclk", null), IntPtr.Zero, "EVA_ChildWindow", null);

            RECT stRect = default(RECT);
            GetWindowRect(imoticon_window, ref stRect);

            Console.WriteLine("{0} {1} {2} {3}", stRect.left, stRect.top, stRect.right, stRect.bottom);

            int width = stRect.right - stRect.left;
            int height = stRect.bottom - stRect.top;

            if (width == 0 || height == 0)
                return new Bitmap(1, 1);

            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(stRect.left, stRect.top, 0, 0, bitmap.Size);

            return bitmap;
        }



        private IntPtr getWindowOfProcess(string processName ,string className, string windowText)
        {            
            IntPtr result = IntPtr.Zero;

            List<IntPtr> intPtrs = FindAllWindowEx(IntPtr.Zero, className, windowText);

            foreach (IntPtr p in intPtrs)
            {
                IntPtr processId = IntPtr.Zero;
                GetWindowThreadProcessId(p, out processId);

                Process process = Process.GetProcessById(processId.ToInt32());

                if (process.ProcessName == processName)
                {
                    result = p;
                    break;
                }

            }

            return result;
        }

        private void ClickMessage(IntPtr hwnd, int x, int y)
        {
            PostMessage(hwnd, WM_LBUTTONDOWN, 0, MAKEPOINT(x, y));
            PostMessage(hwnd, WM_LBUTTONUP, 0, MAKEPOINT(x, y));
        }

        public void ImageClick(int row, int col, int index)
        {
            IntPtr talkBoxWindow = getWindowOfProcess("KakaoTalk","#32770", null);

            //이모티콘 창 클릭 17, 71
            ClickMessage(talkBoxWindow, 17, 710);

            Thread.Sleep(50);

            IntPtr imoticonWindow = FindAllWindowEx(IntPtr.Zero, "EVA_Window_Dblclk", null)[1];
            IntPtr childWindow = FindWindowEx(imoticonWindow, IntPtr.Zero, "EVA_ChildWindow", null);

            ////이모티콘 탭 클릭
            ClickMessage(imoticonWindow, 130, 40);


        }
    }
}
