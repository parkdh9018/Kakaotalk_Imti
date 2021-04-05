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
        int width = 84;
        int index = 0;
        int row = 4;
        int col = 6;

        Bitmap MainImage;
        List<PictureBox> listPicture;
        List<TextBox> listTextBox;
        String ImoticonName;

        List<Panel> ListPanel = new List<Panel>();

        public Form2(String name, Bitmap bitmap = null)
        {
            InitializeComponent();

            listPicture = new List<PictureBox>();
            listTextBox = new List<TextBox>();
            ImoticonName = name;

            if (bitmap != null)
            {
                backImage.Image = bitmap;
                MainImage = bitmap;
            }

        }


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
            picture.Size = new Size(d, d);
            picture.Location = new Point(9, 9);
            picture.BorderStyle = BorderStyle.FixedSingle;
            listPicture.Add(picture);

            TextBox text = new TextBox();
            text.Size = new Size(166,21);
            text.Location = new Point(131,42);
            listTextBox.Add(text);

            element.Controls.Add(picture);
            element.Controls.Add(text);

            panel2.Controls.Add(element);
        }
        private void backImage_Paint(object sender, PaintEventArgs e)
        {

            Pen p = new Pen(Color.Blue, 1);

            int cnt = 0;

            for (int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    Rectangle rec = new Rectangle(0+d*i, width + d*j, d, d);
                    e.Graphics.DrawRectangle(p,rec);

                    Bitmap bitmap = MainImage.Clone(rec, System.Drawing.Imaging.PixelFormat.DontCare);
                    listPicture[cnt++].Image = bitmap;

                }

            }

            p.Dispose();
      
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
            else
            {
                DataSaveLoad ds = new DataSaveLoad();
                ds.SaveData(ImoticonName, listPicture.Select(listPicture => listPicture.Image).ToList<Image>(),listTextBox.Select(listTextBox => listTextBox.Text).ToList<String>());
                this.Close(); 
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ListPanel.Add(panel1);
            ListPanel.Add(panel2);
            ListPanel[index].BringToFront();

            for (int i = 0; i < row * col; i++)
                createElement();

        }
    }
    
    
}
