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

    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private DataSaveLoad dl;
        private List<DataObject> dataObjectList;
        private KeywordAnalysis keywordAnalysis;
        private HandleManege handleManege;
        private Timer timer;

        List<PictureBox> pictureBoxes;

        public Form1()
        {
            InitializeComponent();

            //this.ControlBox = false;

            pictureBoxes = new List<PictureBox>();
            handleManege = new HandleManege();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_handler);
            timer.Start();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            dl = new DataSaveLoad();
            keywordAnalysis = new KeywordAnalysis();

            dataObjectList = dl.LoadData();

            for(int i = 0; i < dataObjectList.Count; i++)
                keywordAnalysis.AddData(i,dataObjectList[i].listIndex, dataObjectList[i].kewordTexts);

        }
        private void button1_click(object sender, EventArgs e)
        {
            Bitmap bitmap = handleManege.ImoticonWindowLoad();

            if (bitmap.Size.Height == 1 || bitmap.Size.Width == 1)
                MessageBox.Show("다시 시도해주세요");

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

            HashSet<Value> setValues = keywordAnalysis.findIndex(searchBox.Text);
            List<Value> values = setValues.ToList();

            foreach(Value v in values)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = dataObjectList[v.dataIndex].imageList[v.index];
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Click += new EventHandler((Handlesender, HandleE)
                    => image_click(Handlesender, HandleE, v.listIndex, v.index));
                pictureBoxes.Add(pictureBox);

                ImagePanel.Controls.Add(pictureBox);
            }

        }

        private void image_click(object sender, EventArgs e, int listIndex, int index)
        {
            handleManege.ImageClick(listIndex, index);
        }

        private void timer_handler(object sender, EventArgs e)
        {
            IntPtr talkBoxHandle = handleManege.getWindowOfProcess("KakaoTalk", "#32770", null);
            //WindowRect windowRect = new WindowRect(talkBoxHandle);

            if (talkBoxHandle != IntPtr.Zero)
            {
                //this.Location = new Point(windowRect.rect.left - 100, windowRect.rect.top);
                
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void MenuItemFix_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }
    }
}
