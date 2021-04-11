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
    public partial class Form3 : Form
    {
        DataSaveLoad dataSaveLoad;

        List<DataObject> dataObjectList;
        List<PictureBox> listPicture;
        List<TextBox> listTextBox;

        TabPage firstPage;

        int listIndex;
        int imoticonWIdth = 75;
        int imoticonHeight = 75;

        public Form3()
        {
            InitializeComponent();

            dataSaveLoad = new DataSaveLoad();

            dataObjectList = dataSaveLoad.LoadData();

            tabControl.Selecting += new TabControlCancelEventHandler(TabControl_Selecting);

            firstPage = new TabPage(dataObjectList[0].imoticonName);
            tabControl.TabPages.Add(firstPage);

            for (int i = 1; i < dataObjectList.Count; i++)
            {
                TabPage tabPage = new TabPage(dataObjectList[i].imoticonName);
                tabControl.TabPages.Add(tabPage);
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            createListElement(firstPage, dataObjectList[0].imageList.Count, dataObjectList[0]);
        }

        void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int n = e.TabPageIndex;
            createListElement(e.TabPage, dataObjectList[n].imageList.Count, dataObjectList[n]);
        }

        void createListElement(TabPage tabPage, int n, DataObject dataObject)
        {
            listIndex = dataObject.listIndex;

            listPicture = new List<PictureBox>();
            listTextBox = new List<TextBox>();
            tabPage.Controls.Clear();

            FlowLayoutPanel flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.FlowDirection = FlowDirection.TopDown;
            flowLayout.WrapContents = false;
            flowLayout.AutoScroll = true;
            tabPage.Controls.Add(flowLayout);

            for (int i = 0; i < n; i++)
            {
                Panel element = new Panel();
                element.Size = new Size(396, 113);
                element.BorderStyle = BorderStyle.FixedSingle;

                PictureBox picture = new PictureBox();
                picture.Size = new Size(imoticonWIdth, imoticonHeight);
                picture.Location = new Point(9, 9);
                picture.BorderStyle = BorderStyle.FixedSingle;
                picture.Image = dataObject.imageList[i];
                listPicture.Add(picture);

                TextBox text = new TextBox();
                text.Size = new Size(166, 21);
                text.Location = new Point(131, 42);
                text.Text = dataObject.kewordTexts[i];
                listTextBox.Add(text);

                element.Controls.Add(picture);
                element.Controls.Add(text);

                flowLayout.Controls.Add(element);
            }

            Button saveButton = new Button();
            saveButton.Text = "저장";
            saveButton.Click += new EventHandler((sender,e) 
                => Button_click(sender, e, dataObject.imoticonName));
            flowLayout.Controls.Add(saveButton);
        }

        private void Button_click(object sender, EventArgs e, String name)
        {
            dataSaveLoad.SaveData(
                new DataObject(name,listIndex,
                listPicture.Select(picture => picture.Image).ToList(),
                listTextBox.Select(textbox => textbox.Text).ToList()));

            Program.saveEventCheck = true;

            this.Close();
        }
    }
}
