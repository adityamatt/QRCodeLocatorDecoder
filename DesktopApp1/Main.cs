using Emgu.CV;
using Emgu.CV.Structure;
using QRManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.CurrentSize = this.Size;

            this.xdist = this.richTextBox1.Location.X;
            this.ydist = this.Size.Height - this.richTextBox1.Location.Y;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");

        }

        private void SelectImageandDecodeQR_Button(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                richTextBox1.Text = "";
                this.ImageBox1.Image = new Image<Bgr, byte>(open.FileName);
                String[] data = QRCodeDecoder.DecodeQR(this.ImageBox1.Image.Bitmap);
                int i = 1;
                foreach(string s in data)
                {
                    richTextBox1.Text += i.ToString() + "." + s+"\n";
                    i++;
                }
                // image file path  
            }
        }
        private void Main_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
            ResizeStuffs();
        }
        private void ResizeStuffs()
        {
            Size oldSize = this.CurrentSize;
            Size newSize = this.Size;

            //text box needs to be resized and relocated
            this.richTextBox1.Size = new Size(newSize.Width - 40, this.richTextBox1.Height);
            this.richTextBox1.Location = new Point(xdist, newSize.Height - ydist);

            //ImageBox needs to be resized
            this.ImageBox1.Size = new Size(newSize.Width - 40, this.richTextBox1.Location.Y - 40-this.ImageBox1.Location.Y);
            this.CurrentSize = newSize;
        }
        
        private int widthtAfterResize(int width,Size oldSize,Size newSize)
        {
            double multiplier = (double)newSize.Height / oldSize.Height;
            double output = (double)width * multiplier;
            return (int)output;

        }
    }
}
