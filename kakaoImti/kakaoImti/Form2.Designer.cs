namespace kakaoImti
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.drawImage = new System.Windows.Forms.PictureBox();
            this.backImage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.drawImage);
            this.panel1.Controls.Add(this.backImage);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 611);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 629);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // drawImage
            // 
            this.drawImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.drawImage.BackColor = System.Drawing.Color.Transparent;
            this.drawImage.Location = new System.Drawing.Point(104, 121);
            this.drawImage.Name = "drawImage";
            this.drawImage.Size = new System.Drawing.Size(396, 346);
            this.drawImage.TabIndex = 1;
            this.drawImage.TabStop = false;
            this.drawImage.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawImage_Paint);
            // 
            // backImage
            // 
            this.backImage.BackColor = System.Drawing.Color.Transparent;
            this.backImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.backImage.Image = global::kakaoImti.Properties.Resources.Image1;
            this.backImage.Location = new System.Drawing.Point(19, 17);
            this.backImage.Name = "backImage";
            this.backImage.Size = new System.Drawing.Size(72, 76);
            this.backImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.backImage.TabIndex = 0;
            this.backImage.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 664);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox drawImage;
        private System.Windows.Forms.PictureBox backImage;
    }
}