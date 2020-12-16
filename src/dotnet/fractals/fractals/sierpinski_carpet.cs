using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace fractals
{
    public partial class sierpinski_carpet : Form
    {

        ThreadStart job;
        Thread oThread;

        int width = 2560; //1280;
        int height = 2048; //1024;

        Bitmap oDrawing;
        Graphics oGraphic; // = Graphics.FromImage(oDrawing);

        Point pntA;
        Point pntB;
        Point pntC;
        Point pntD;
        Point pntZ;
        Point pntN;

        Boolean running = false;
        int loops = 10;

        die dice = new die(4);

        public sierpinski_carpet()
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
                    oThread.Suspend();
                }
                else
                {
                    running = true;
                    button1.Text = "Stop";
                    oThread.Start();
                }
            }
            catch
            {
            }
            timer1.Enabled = running;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = oDrawing;
            try
            {
                pictureBox1.Refresh();
            }
            catch { }

            loops -= 1;
            if (loops <= 0)
            {
                button1_Click(sender, e);
                loops = 5;
            }

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
                case 4:
                    pntN = pntD;
                    clr = Color.Yellow;
                    break;
            }
            pntZ = middlePoint(pntN, pntZ);
            try
            {
                oDrawing.SetPixel(pntZ.X, pntZ.Y, clr);
            }
            catch { }
        }

        private void drawCarpet(int xTL, int yTL, int _width, int _height) {
            if (_width > 2 && _height > 2)
            {
                int w = _width / 3;
                int h = _height / 3;
                oGraphic.FillRectangle(System.Drawing.Brushes.Blue, xTL + w, yTL + h, w, h);
                for (int k = 0; k < 9; k++) if (k != 4)
                    {
                        int i = k / 3;
                        int j = k % 3;
                        drawCarpet(xTL + i * w, yTL + j * k, w, h);
                    }
            }
        }


        void threadLoop()
        {
            /*
            while (running)
            {
                    ThreadJob();
            }
            */
            drawCarpet(0, 0, oDrawing.Width, oDrawing.Height);
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
            oDrawing = new Bitmap(width, height);
            oGraphic = Graphics.FromImage(oDrawing);

            pntA = new Point(0, 0);
            pntB = new Point(0, height);
            pntC = new Point(width, height);
            pntD = new Point(width, 0);
            pntZ = pntA;

            job = new ThreadStart(threadLoop);
            oThread = new Thread(job);
        }
    }
}