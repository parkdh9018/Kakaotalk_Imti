using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace kakaoImti
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        /// 

        public const int WM_LBUTTONDOWN = 513;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;

        public const int VK_TAB = 0x09;

        [STAThread]
        static void Main()
        {


            int talking = Connect.FindWindow(null, "박동환");
            Console.WriteLine(talking);
            

            if(talking > 0)
            {
                Thread.Sleep(2000);
                Console.WriteLine("start");

                int result = Connect.SendMessage(talking, WM_KEYDOWN, Convert.ToInt32(Keys.Tab), 0);
                Console.WriteLine(result);

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
