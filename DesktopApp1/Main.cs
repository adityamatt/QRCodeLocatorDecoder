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
            //open.Filter = "Image Files(*.jpg; *.png ; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                Image<Bgr, byte> displayImage = new Image<Bgr, byte>(open.FileName);
                richTextBox1.Text = "";
               
                Tuple<string, Rectangle>[] data = QRCodeDecoder.DecodeQR(displayImage.ToBitmap());
                int i = 1;
                if (data!=null)
                { 
                    foreach (Tuple<String,Rectangle> info in data)
                    {
                        try
                        {
                            String text = info.Item1;
                            Rectangle location = info.Item2;
                            CvInvoke.Rectangle(displayImage, location, new MCvScalar(0, 0, 255), 3);
                            Point leftTop = new Point(location.X, location.Y+location.Height);
                            CvInvoke.PutText(displayImage, i.ToString(), leftTop, Emgu.CV.CvEnum.FontFace.HersheyComplex, 1, new MCvScalar(0, 0, 255), 3);
                            richTextBox1.Text += i.ToString() + "." + text + "\n";
                            i++;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        
                    }
                }
                else
                {
                    richTextBox1.Text = "No Valid QR code detected or maybe its too destroyred to be read";
                }
                this.ImageBox1.Image = displayImage;
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
