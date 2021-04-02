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
                backImage.Image = bitmap;
            }

            drawImage.Parent = backImage;


        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int d = 10;
            Point p;
            switch (keyData)
            {
                case Keys.Right: // left arrow key
                    p = new Point(drawImage.Location.X + d, drawImage.Location.Y);
                    drawImage.Location = p;
                    return true;

                case Keys.Left: // right arrow key
                    p = new Point(drawImage.Location.X - d, drawImage.Location.Y);
                    drawImage.Location = p;
                    return true;

                case Keys.Up:
                    p = new Point(drawImage.Location.X, drawImage.Location.Y - d);
                    drawImage.Location = p;
                    return true;

                case Keys.Down:
                    p = new Point(drawImage.Location.X, drawImage.Location.Y + d);
                    drawImage.Location = p;
                    return true;

                    // etc.
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DrawImage_Paint(object sender, PaintEventArgs e)
        {

            
            Graphics g = drawImage.CreateGraphics();
            Pen p = new Pen(Color.Blue, 2);

            Rectangle rec = new Rectangle(20, 20, 40, 40);
            g.DrawRectangle(p, rec);

            
        }
    }
    
    
}
