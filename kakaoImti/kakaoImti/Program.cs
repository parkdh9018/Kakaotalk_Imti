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
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        /// 

        public const int WM_LBUTTONDOWN = 513;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int VK_TAB = 0x09;

        [STAThread]
        static void Main()
        {


            IntPtr talking = Connect.FindWindow(null, "박동환");
            Console.WriteLine(talking.ToString("X"));
            

            if(talking != IntPtr.Zero)
            {
                Thread.Sleep(4000);
                Console.WriteLine("start");

                IntPtr result = IntPtr.Zero;
                do
                {
                    StringBuilder caption = new StringBuilder(260);

                    result = Connect.FindWindowEx(talking, result, null, null);
                   
                    if (Connect.GetClassName(result, caption, 260) > 0)
                    {
                        Console.WriteLine(caption);
                    }

                }
                while (result != IntPtr.Zero);


            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }

     
    }


}
