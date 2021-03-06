﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace imageOperations
{
    public partial class Form1 : Form
    {

        Bitmap bmp = new Bitmap(800, 500);

        public Form1()
        {
            InitializeComponent();
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);    //TurnOff Aplication
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                pictureBox.ImageLocation = openFileDialog1.FileName;
                bmp = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                textBoxFileName.Text = openFileDialog1.FileName;
                labelResolution.Text = pictureBox.Height.ToString() + " X " + pictureBox.Width.ToString();

                this.Refresh();
            }
            catch
            {
                MessageBox.Show("Please add a photo", "information");
            }
        }


        private void buttonGrayScale_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                // bmp refresh
                bmp = (Bitmap)Bitmap.FromFile(textBoxFileName.Text);

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color c = bmp.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        int avg = (r + g + b) / 3;
                        bmp.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                    }
                }
                pictureBox.Image = bmp;
                this.Refresh();
            }
        }

        private void buttonBitmap_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                // bmp refresh
                bmp = (Bitmap)Bitmap.FromFile(textBoxFileName.Text);
                int treshold = Convert.ToInt32(numericTreshold2.Text);
                

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color c = bmp.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        int avg = (r + g + b) / 3;

                        if (avg >= treshold)
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                    }
                }
                pictureBox.Image = bmp;
                this.Refresh();
            }       
        }


        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                bmp = (Bitmap)Bitmap.FromFile(textBoxFileName.Text);
                pictureBox.Image = bmp;
                this.Refresh();
            }
        }
        
        private void buttonBorderDetection_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {

                int treshold = Convert.ToInt32(numericTreshold.Text);
                int[,] IpikselXY = new int[bmp.Width, bmp.Height];
                // bmp refresh
                bmp = (Bitmap)Bitmap.FromFile(textBoxFileName.Text);

                // Gray Scale
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color c = bmp.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        int avg = (r + g + b) / 3;
                        IpikselXY[x, y] = avg;
                    }
                }

                //Object Detection
                for (int y = 1; y < bmp.Height - 1; y++)
                {
                    for (int x = 1; x < bmp.Width - 1; x++)
                    {

                        int IpikselX = (IpikselXY[(x + 1), y] - IpikselXY[(x - 1), y]) / 2;
                        int IpikselY = (IpikselXY[x, (y + 1)] - IpikselXY[x, (y - 1)]) / 2;
                        int IpikselGradient = Convert.ToInt32(Math.Sqrt(Math.Pow(IpikselX, 2) + Math.Pow(IpikselY, 2)));

                        if (IpikselGradient >= treshold)
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                    }
                }

                pictureBox.Image = bmp;
                this.Refresh();
            }
        }
    }
}
//  Batuhan Güneş  
//  201513171055