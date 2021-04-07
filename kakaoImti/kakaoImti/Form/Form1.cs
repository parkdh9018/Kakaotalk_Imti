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
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private DataSaveLoad dl;
        private DataObject dataObject;
        private KeywordAnalysis keywordAnalysis;
        private HandleManege handleManege;

        List<PictureBox> pictureBoxes;

        public Form1()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox>();
            handleManege = new HandleManege();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dl = new DataSaveLoad();
            keywordAnalysis = new KeywordAnalysis();

            dataObject = dl.LoadData("케장1");

            keywordAnalysis.AddData(dataObject.listIndex, dataObject.kewordTexts);

            for (int i = 0; i < dataObject.imageList.Count; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = dataObject.imageList[i];
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Click += new EventHandler((Handlesender,HandleE) 
                    => image_click(Handlesender, HandleE, dataObject.listIndex,i));
                pictureBoxes.Add(pictureBox);
            }

        }
        private void button1_click(object sender, EventArgs e)
        {
            Bitmap bitmap = handleManege.ImoticonWindowLoad();

            if (bitmap.Size.Height == 1 || bitmap.Size.Width == 1)
                MessageBox.Show("창이 없습니다.");

            pb.Image = bitmap;
            this.bitmap = bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this.bitmap);
            form2.ShowDialog();
        }


        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            ImagePanel.Controls.Clear();

            List<Value> values = keywordAnalysis.findIndex(searchBox.Text);

            //Console.WriteLine(values.Count);

            for (int i = 0; i < values.Count; i++)           
                ImagePanel.Controls.Add(pictureBoxes[values[i].index]);
            
        }

        private void image_click(object sender, EventArgs e, int listIndex, int index)
        {
            handleManege.ImageClick(listIndex, index);
        }
    }
}
