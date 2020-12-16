using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace fractals
{
    public partial class sierpinski_triangle : Form
    {

        ThreadStart job;
        Thread oThread;

        int width = 1280;
        int height = 1024;

        Bitmap oDrawing;

        Point pntA;
        Point pntB;
        Point pntC;
        Point pntZ;
        Point pntN;

        Boolean running = false;
        int loops = 10;

        die dice = new die(3);

        public sierpinski_triangle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                try
                {
                    if (running)
                    {
                        running = false;
                        button1.Text = "Start";
                        try
                        {
                            oThread.Abort();
                        }
                        catch { }
                    }
                    else
                    {
                        running = true;
                        button1.Text = "Stop";
                        oThread = null;
                        oThread = new Thread(job);
                        oThread.Start();
                    }
                }
                catch
                {
                }
                //timer1.Enabled = running;
        }

        private delegate void ShowImage(); //Bitmap myDrawing);
        private void UpdateImage() //Bitmap myDrawing)
        {
            if (!pictureBox1.InvokeRequired) //Running on UI Thread
            {
                try
                {
                    //Bitmap myDrawing = new Bitmap(oDrawing);
                    //pictureBox1.Image = myDrawing;
                    //pictureBox1.Image = oDrawing;
                    Thread.Sleep(10);
                    pictureBox1.Refresh();
                }
                catch (Exception ex)
                {
                    throw new Exception("UpdateImage Broke", ex);
                }
            }
            else //Not Running on UI thread
            {
                ShowImage _showImage = new ShowImage(UpdateImage);
                this.Invoke(_showImage);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdateImage(); //oDrawing);
            //pictureBox1.BackgroundImage = oDrawing;
            //try
            //{
            //    pictureBox1.Refresh();
            //}
            //catch { }

            //loops -= 1;
            //if (loops <= 0)
            //{
            //    button1_Click(sender, e);
            //    loops = 5;
            //}
        }

        private  void ThreadJob()
        {
            int x = dice.roll();
            Color clr = Color.White;
            switch (x)
            {
                case 1:
                    pntN = pntA;
                    clr = Color.Red;
                    break;
                case 2:
                    pntN = pntB;
                    clr = Color.Green;
                    break;
                case 3:
                    pntN = pntC;
                    clr = Color.Blue;
                    break;
            }
            //clr = RandomColor();
            pntZ = middlePoint(pntN, pntZ);
            try
            {
                oDrawing.SetPixel(pntZ.X, pntZ.Y, clr);
            }
            catch { }

        }

        private Color RandomColor()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int clrPick = rnd.Next(0, 9);
            Color outColor = Color.White;

            switch (clrPick)
            {
                //case 0:
                //    outColor = Color.Black;
                //    break;

                //case 1:
                //    outColor = Color.Brown;
                //    break;

                //case 2:
                //    outColor = Color.Red;
                //    break;

                //case 3:
                //    outColor = Color.Orange;
                //    break;

                //case 4:
                //    outColor = Color.Yellow;
                //    break;

                //case 5:
                //    outColor = Color.Green;
                //    break;

                //case 6:
                //    outColor = Color.Blue;
                //    break;

                //case 7:
                //    outColor = Color.Violet;
                //    break;

                //case 8:
                //    outColor = Color.Gray;
                //    break;

                //case 9:
                //    outColor = Color.White;
                //    break;

                case 0:
                    outColor = Color.Pink;
                    break;

                case 1:
                    outColor = Color.Red;
                    break;

                case 2:
                    outColor = Color.LightPink;
                    break;

                case 3:
                    outColor = Color.Orange;
                    break;

                case 4:
                    outColor = Color.Salmon;
                    break;

                case 5:
                    outColor = Color.LightSalmon;
                    break;

                case 6:
                    outColor = Color.Maroon;
                    break;

                case 7:
                    outColor = Color.Violet;
                    break;

                case 8:
                    outColor = Color.MediumVioletRed;
                    break;

                case 9:
                    outColor = Color.OrangeRed;
                    break;
            }

            return outColor;



        }

        void threadLoop()
        {
            int x=0;
            while (running)
            {
                x++;
                ThreadJob();
                if (x >= 1000)
                {
                    x = 0;
                    UpdateImage();
                }
            }
        }

        private Point middlePoint(Point pntIn1, Point pntIn2)
        {
            Point pntWork = new Point(0,0);
            pntWork.X = (pntIn1.X + pntIn2.X) / 2;
            pntWork.Y = (pntIn1.Y + pntIn2.Y) / 2;
            return pntWork;
        }

        private void sierpinski_Load(object sender, EventArgs e)
        {
            //this.OnFormClosing += sierpinski_FormClosing;

            oDrawing = new Bitmap(width, height);

            pntA = new Point(width / 2, 0);
            pntB = new Point(0, height);
            pntC = new Point(width, height);
            pntZ = pntA;

            job = new ThreadStart(threadLoop);
            oThread = new Thread(job);

            pictureBox1.BackgroundImage = oDrawing;

        }

        private void sierpinski_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                oThread.Abort();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (running)
            {
                button1_Click(sender, e);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG Image (*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                oDrawing.Save(sfd.FileName,ImageFormat.Png);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (running)
            {
                button1_Click(sender, e);
            }

            oDrawing = null;
            oDrawing = new Bitmap(width, height);
            pictureBox1.Refresh();

        }

    }
}