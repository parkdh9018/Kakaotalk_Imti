using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace kakaoImti
{
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public partial class Form1 : Form
    {
        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpClassName, string lpWindowName);

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            IntPtr imoticon_window = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "EVA_Window_Dblclk", null);
            IntPtr child_window = FindWindowEx(imoticon_window, IntPtr.Zero, "EVA_ChildWindow", null);

            RECT stRect = default(RECT);
            GetWindowRect(child_window, ref stRect);

            Console.WriteLine("{0} {1} {2} {3}", stRect.left, stRect.top, stRect.right, stRect.bottom);

            int width = stRect.right - stRect.left;
            int height = stRect.bottom - stRect.top;

            if (width == 0)
            {
                MessageBox.Show("창을 찾을 수 없습니다.");
                return;
            }

            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(PointToScreen(new Point(this.pb.Location.X, this.pb.Location.Y)), new Point(0, 0), this.pb.Size);

            graphics.CopyFromScreen(stRect.left, stRect.top, 0, 0, this.pb.Size);

            pb.Image = bitmap;

        }
        

        private void Form1_Resize(object sender, EventArgs e)
        {
            //Console.WriteLine("{0}, {1}",this.Width, this.Height);
        }
    }
}
