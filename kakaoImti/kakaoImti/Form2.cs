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
        int d = 84;
        int index = 0;
        int row = 4;
        int col = 6;
        int ele_index = 0;

        List<Panel> ListPanel = new List<Panel>();

        public Form2(String name, Bitmap bitmap = null)
        {
            InitializeComponent();

            if (bitmap != null)
            {
                backImage.Image = bitmap;
            }

        }

        //private void DrawImage_Paint(object sender, PaintEventArgs e)
        //{


        //    Graphics g = drawImage.CreateGraphics();
        //    Pen p = new Pen(Color.Blue, 2);

        //    Rectangle rec = new Rectangle(20, 20, 40, 40);
        //    g.DrawRectangle(p, rec);


        //}

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    const int d = 10;
        //    Point p;
        //    switch (keyData)
        //    {
        //        case Keys.Right: // left arrow key
        //            p = new Point(drawImage.Location.X + d, drawImage.Location.Y);
        //            drawImage.Location = p;
        //            return true;

        //        case Keys.Left: // right arrow key
        //            p = new Point(drawImage.Location.X - d, drawImage.Location.Y);
        //            drawImage.Location = p;
        //            return true;

        //        case Keys.Up:
        //            p = new Point(drawImage.Location.X, drawImage.Location.Y - d);
        //            drawImage.Location = p;
        //            return true;

        //        case Keys.Down:
        //            p = new Point(drawImage.Location.X, drawImage.Location.Y + d);
        //            drawImage.Location = p;
        //            return true;

        //            // etc.
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        void createElement()
        {
            Panel element = new Panel();
            element.Size = new Size(396, 113);
            element.BorderStyle = BorderStyle.FixedSingle;

            PictureBox picture = new PictureBox();
            picture.Size = new Size(100, 90);
            picture.Location = new Point(9, 9);
            picture.BorderStyle = BorderStyle.FixedSingle;

            TextBox text = new TextBox();
            text.Size = new Size(166,21);
            text.Location = new Point(131,42);

            element.Controls.Add(picture);
            element.Controls.Add(text);

            panel2.Controls.Add(element);
            panel2.Controls.SetChildIndex(element, ele_index++);
        }
        private void backImage_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Blue, 1);

            for (int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    Rectangle rec = new Rectangle(0+d*i, 84+d*j, d, d);
                    e.Graphics.DrawRectangle(p,rec);
                }

            }
      
        }

        private void Previous_click(Object sender, EventArgs e)
        {
            if (index > 0)
                ListPanel[--index].BringToFront();
        }

        private void Next_click(Object sender, EventArgs e)
        {
            if (index < ListPanel.Count - 1)
                ListPanel[++index].BringToFront();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ListPanel.Add(panel1);
            ListPanel.Add(panel2);
            ListPanel[index].BringToFront();

            createElement();
            createElement();
            createElement();
            createElement();

        }
    }
    
    
}
