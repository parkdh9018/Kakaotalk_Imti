using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakaoImti
{
    public partial class Form2 : Form
    {
        public Form2(Bitmap bitmap = null)
        {
            InitializeComponent();

            if (bitmap != null)
            {
                pictureBox1.Image = bitmap;
            }



        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int d = 10;
            Point p;
            switch (keyData)
            {
                case Keys.Right: // left arrow key
                    p = new Point(pictureBox1.Location.X + d, pictureBox1.Location.Y);
                    pictureBox1.Location = p;
                    return true;

                case Keys.Left: // right arrow key
                    p = new Point(pictureBox1.Location.X - d, pictureBox1.Location.Y);
                    pictureBox1.Location = p;
                    return true;

                case Keys.Up:
                    p = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - d);
                    pictureBox1.Location = p;
                    return true;

                case Keys.Down:
                    p = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + d);
                    pictureBox1.Location = p;
                    return true;

                    // etc.
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
