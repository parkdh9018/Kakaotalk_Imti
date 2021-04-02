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

            if(bitmap != null)
            {
                pictureBox1.Image = bitmap;
            }
        }
    }
}
