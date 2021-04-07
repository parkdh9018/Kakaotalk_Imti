using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace kakaoImti
{

    class WindowRect
    {
        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public IntPtr wIndow;
        public RECT rect;
        public int width;
        public int height;

        public WindowRect()
        {
            rect = default(RECT);
        }

        public WindowRect(IntPtr window)
        {
            rect = default(RECT);
            setWindow(window);
        }

        public void setWindow(IntPtr window)
        {
            this.wIndow = window;
            GetWindowRect(wIndow, ref rect);
            width = rect.right - rect.left;
            height = rect.bottom - rect.top;
        }

    }
}
