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
        int imoticonWIdth = 75;
        int imoticonHeight = 75;
        int widthSpace = 5;
        int heightSpace = 8;

        int index = 0;
        int row;
        int col;
        int limitnum;

        Bitmap MainImage;
        List<PictureBox> listPicture;
        List<TextBox> listTextBox;

        List<Panel> ListPanel = new List<Panel>();

        public Form2(Bitmap bitmap)
        {
            InitializeComponent();

            listTextBox = new List<TextBox>();

            backImage.Image = bitmap;
            MainImage = bitmap;

            row = (MainImage.Width - 9) / (imoticonWIdth + widthSpace);
            col = MainImage.Height / (imoticonHeight + heightSpace);
            limitnum = row * col;

            createBoxList(panel2, limitnum);

            LimitCntTextBox.Text = limitnum.ToString();

        }

        public void createBoxList(FlowLayoutPanel panel, int n)
        {
            listPicture = new List<PictureBox>();

            for (int i = 0; i < n; i++)
            {
                Panel element = new Panel();
                element.Size = new Size(396, 113);
                element.BorderStyle = BorderStyle.FixedSingle;

                PictureBox picture = new PictureBox();
                picture.Size = new Size(imoticonWIdth, imoticonHeight);
                picture.Location = new Point(9, 9);
                picture.BorderStyle = BorderStyle.FixedSingle;
                listPicture.Add(picture);

                TextBox text = new TextBox();
                text.Size = new Size(166, 21);
                text.Location = new Point(131, 42);
                listTextBox.Add(text);

                element.Controls.Add(picture);
                element.Controls.Add(text);

                panel.Controls.Add(element);
            }


        }



        private void backImage_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Blue, 1);

            int cnt = 0;

            for (int i = 0; i < col; i++)
            {
                Point firstPoint = new Point(9, 0);

                for(int j = 0; j < row; j++)
                {
                    if (cnt >= limitnum )
                        return;

                    Rectangle rec = new Rectangle(firstPoint.X + (imoticonWIdth + widthSpace) *j, firstPoint.Y + (imoticonHeight + heightSpace) *i, imoticonWIdth, imoticonHeight);

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


                DataObject dataObject = new DataObject
                (
                    this.name.Text,
                    Int32.Parse(this.ListIndexTextBox.Text),
                    listPicture.Select(picture => picture.Image).ToList<Image>(),
                    listTextBox.Select(textBox => textBox.Text).ToList()
                );

                ds.SaveData(dataObject);

                Program.saveEventCheck = true;

                this.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ListPanel.Add(panel1);
            ListPanel.Add(panel2);
            ListPanel[index].BringToFront();


        }

        private void LimitCntTextBox_TextChanged(object sender, EventArgs e)
        {
            int num;
            Int32.TryParse(LimitCntTextBox.Text, out num);

            if(0 < num && num <= row * col)
            {
                //draw
                limitnum = num;

                panel2.Controls.Clear();
                createBoxList(panel2, limitnum);
                backImage.Image = MainImage;

            }
        }
    }


}
