﻿using System;
using System.Windows.Forms;

namespace kakaoImti
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 mainForm = new Form1();      
            Application.Run();
            
        }

     
    }


}
