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

        [DllImport("user32.dll")]
        static extern int EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);

        delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

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

        public Bitmap ImoticonWindowLoad()
        {
            ////////////////이전방법
            //Thread.Sleep(3000);

            //WindowRect imoticonWindow = new WindowRect();
            //imoticonWindow.setWindow(FindWindowEx(FindWindow("EVA_Window_Dblclk", null), IntPtr.Zero, "EVA_ChildWindow", null));

            //if (imoticonWindow.width == 0 || imoticonWindow.height == 0)
            //    return new Bitmap(1, 1);

            //Bitmap bitmap = new Bitmap(imoticonWindow.width, imoticonWindow.height);
            //Graphics graphics = Graphics.FromImage(bitmap);

            //graphics.CopyFromScreen(imoticonWindow.rect.left, imoticonWindow.rect.top, 0, 0, bitmap.Size);

            WindowRect EntireWindow = getEntireImoticonWIndow();

            ////이모티콘 탭 클릭
            ClickMessage(EntireWindow.wIndow, EntireWindow.width / 4 + 50, 40);

            Thread.Sleep(200);

            WindowRect childWindow = new WindowRect(FindWindowEx(EntireWindow.wIndow, IntPtr.Zero, "EVA_ChildWindow", null));
            WindowRect listWindow = new WindowRect(FindWindowEx(childWindow.wIndow, IntPtr.Zero, "EVA_ChildWindow_Dblclk", null));
            WindowRect imoticonWindow = new WindowRect(FindWindowEx(childWindow.wIndow, listWindow.wIndow, "EVA_ChildWindow_Dblclk", null));

            if (imoticonWindow.width == 0 || imoticonWindow.height == 0)
                return new Bitmap(1, 1);

            Bitmap bitmap = new Bitmap(imoticonWindow.width, imoticonWindow.height);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(imoticonWindow.rect.left, imoticonWindow.rect.top, 0, 0, bitmap.Size);

            return bitmap;
        }

        
        public WindowRect getEntireImoticonWIndow()
        {
            WindowRect talkBoxWindow = new WindowRect();
            talkBoxWindow.setWindow(getWindowOfProcess("KakaoTalk", "#32770", null));

            ////이모티콘 창 클릭
            ClickMessage(talkBoxWindow.wIndow, 20, talkBoxWindow.height - 20);

            Thread.Sleep(200);

            WindowRect EntireWindow = new WindowRect(FindAllWindowEx(IntPtr.Zero, "EVA_Window_Dblclk", null)[1]);

            return EntireWindow;
        }


        public IntPtr getWindowOfProcess(string processName ,string className, string windowText)
        {            
            List<IntPtr> intPtrs = FindAllWindowEx(IntPtr.Zero, className, windowText);

            foreach (IntPtr p in intPtrs)
            {
                IntPtr processId = IntPtr.Zero;
                GetWindowThreadProcessId(p, out processId);

                Process process = Process.GetProcessById(processId.ToInt32());

                if (process.ProcessName == processName)
                {
                    return p;
                    
                }

            }

            return IntPtr.Zero;
        }

        private void ClickMessage(IntPtr hwnd, int x, int y)
        {
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_LBUTTONUP = 0x202;

            PostMessage(hwnd, WM_LBUTTONDOWN, 0, MAKEPOINT(x, y));
            PostMessage(hwnd, WM_LBUTTONUP, 0, MAKEPOINT(x, y));
        }

        private void KeyBoardMessage(IntPtr hwnd)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_SPACE = 0x20;

            PostMessage(hwnd, WM_KEYDOWN, VK_SPACE, 0);
            PostMessage(hwnd, WM_KEYUP, VK_SPACE, 0);
        }



        public void ImageClick(int listIndex, int index)
        {
            WindowRect EntireWindow = getEntireImoticonWIndow();

            ////이모티콘 탭 클릭
            ClickMessage(EntireWindow.wIndow, EntireWindow.width / 4 + 50, 40);

            Thread.Sleep(200);

            WindowRect childWindow = new WindowRect(FindWindowEx(EntireWindow.wIndow, IntPtr.Zero, "EVA_ChildWindow", null));
            WindowRect listWindow = new WindowRect(FindWindowEx(childWindow.wIndow, IntPtr.Zero, "EVA_ChildWindow_Dblclk", null));
            WindowRect imoticonWindow = new WindowRect(FindWindowEx(childWindow.wIndow, listWindow.wIndow, "EVA_ChildWindow_Dblclk", null));


            ////이모티콘 리스트 클릭
            Point first = new Point(29, 24);
            int maxWidthCnt = (listWindow.width - 14) / 38;
            //Console.WriteLine("{0} {1}",maxWidthCnt, listWindow.width);

            if (maxWidthCnt == 0)
                return;

            ClickMessage(listWindow.wIndow, first.X + 38 * (listIndex % maxWidthCnt - 1) ,first.Y + 30 * (listIndex / maxWidthCnt));

            ////이모티콘 클릭
            first = new Point(45, 40);
            maxWidthCnt = (imoticonWindow.width - 8) / 82;
            ClickMessage(imoticonWindow.wIndow, first.X + 82 * (index % maxWidthCnt), first.Y + 75 * (index / maxWidthCnt));


            ////스페이스바
            SetForegroundWindow(EntireWindow.wIndow);
            KeyBoardMessage(imoticonWindow.wIndow);

        }


    }
}
