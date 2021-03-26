using System;
using System.Runtime.InteropServices;

class Connect {

    [DllImport("user32.dll")]
    public static extern int FindWindow(string lpClassName, string lpWindowName);
    // 탑레벨 윈도우 중에서 원하는 윈도우 찾는 함수

    [DllImport("user32.dll")]
    public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpClassName, string lpWindowName);
    //자식 윈도우 찾는 함수

    [DllImport("user32.dll")]
    public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

}

