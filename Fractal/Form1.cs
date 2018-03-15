using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        private readonly int MAX = 256;      // max iterations
        private readonly double SX = -2.025; // start value real
        private readonly double SY = -1.125; // start value imaginary
        private readonly double EX = 0.6;    // end value real
        private readonly double EY = 1.125;  // end value imaginary
        private static int x1, y1, xs, ys, xe, ye;
        private static double xstart, ystart, xende, yende, xzoom, yzoom;


        private static bool action, rectangle, finished;
        private static float xy;

        private Image picture;
        private Graphics g1;
        private HSB HSBcol = new HSB();
        private Pen pen;

        public Form1()
        {

            InitializeComponent();
            init();
            start();

        }
    

   
        public void init() // all instances will be prepared
        {
            //HSBcol = new HSB();

            finished = false;
            x1 = pictureBox2.Width;
            y1 = pictureBox2.Height;
            xy = (float)x1 / (float)y1;
            picture = new Bitmap(x1, y1);
            g1 = Graphics.FromImage(picture);
            finished = true;
        }
        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;
            mandelbrot();
        }

        private void pictureBox2_paint(object sender, PaintEventArgs e)
        {
            Graphics obj = e.Graphics;
            obj.DrawImage(picture, new Point(0, 0));
            //Console.WriteLine("Working???");
        }

        private void mandelbrot() // calculate all points
        {
            int x, y;
            float h, b, alt = 0.0f;

            action = false;
            //showStatus("Mandelbrot-Set will be produced - please wait...");
           //info.Text = "Please wait for the Mandelbrot!";
            //info.Enabled = false;
            
            for (x = 0; x < x1; x += 2)
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // color value
                    if (h != alt)
                    {
                        b = 1.0f - h * h; // brightnes
                                          ///djm added
                        HSBcol.fromHSB(h * 255, 0.8f * 255, b * 255); //convert hsb to rgb then make a Java Color
                        Color col = Color.FromArgb((int)HSBcol.rChan, (int)HSBcol.gChan, (int)HSBcol.bChan);
                        Pen p = new Pen(col);

                        alt = h;

                    }
                    g1.DrawLine(pen, x, y, x + 1, y);
                }
            // showStatus("Mandelbrot-Set ready - please select zoom area with pressed mouse.");

            Cursor.Current = Cursors.Cross;
            //info.Text = "aa";
            //info.Enabled=false;

            action = true;
        }

        private float pointcolour(double xwert, double ywert) // color value from 0.0 to 1.0 by iterations
        {
            double r = 0.0, i = 0.0, m = 0.0;
            int j = 0;

            while ((j < MAX) && (m < 4.0))
            {
                j++;
                m = r * r - i * i;
                i = 2.0 * r * i + ywert;
                r = m + xwert;
            }
            return (float)j / (float)MAX;
        }

        private void initvalues() // reset start values
        {
            xstart = SX;
            ystart = SY;
            xende = EX;
            yende = EY;
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
                xstart = xende - (yende - ystart) * (double)xy;
        }
        public void destroy() // delete all instances 
        {
            if (finished)
            {

                picture = null;
                g1 = null;
                // garbage collection
            }
        }
        public void update()
        {
            Graphics g = pictureBox2.CreateGraphics();
            g.DrawImage(picture, 0, 0);
            if (rectangle)
            {
                Pen mypen = new Pen(Color.White, 1);
                if (xs < xe)
                {
                    if (ys < ye) g.DrawRectangle(mypen, xs, ys, (xe - xs), (ye - ys));
                    else g.DrawRectangle(mypen, xs, ye, (xe - xs), (ys - ye));
                }
                else
                {
                    if (ys < ye) g.DrawRectangle(mypen, xe, ys, (xs - xe), (ye - ys));
                    else g.DrawRectangle(mypen, xe, ye, (xs - xe), (ys - ye));
                }
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (action)
            {
                xs = e.X;
                ys = e.Y;
            }
        }

    }
}
