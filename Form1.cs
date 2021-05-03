using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Lab6
{
    public partial class Form1 : Form
    {
         Bitmap bitmap;

        int[] histR = new int[256];
        int[] histG = new int[256];
        int[] histB = new int[256];

        bool read = false;
    
        public Form1()
        {
            InitializeComponent();
        }

        private void suwak1_Scroll(object sender, EventArgs e)
        {
            Bitmap bitmapEdited = (Bitmap)bitmap.Clone();


            int width = pic.Image.Width;
            int height = pic.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    Color cA = bitmapEdited.GetPixel(x, y);

                    int r = cA.R;
                    int g = cA.G;
                    int b = cA.B;


                    // R
                    if (r < 127 + suwak1.Value)
                    {
                        r = (127 / (127 + suwak1.Value)) * r;
                    }
                    else if (r > 127 - suwak1.Value)
                    {
                        r = (127 * r + 255 * suwak1.Value) / (127 + suwak1.Value);
                    }
                    else
                    {
                        r = 127;
                    }

                    // G
                    if (g < 127 + suwak1.Value)
                    {
                        g = (127 / (127 + suwak1.Value)) * g;
                    }
                    else if (g > 127 - suwak1.Value)
                    {
                        g = (127 * g + 255 * suwak1.Value) / (127 + suwak1.Value);
                    }
                    else
                    {
                        g = 127;
                    }

                    // B
                    if (b < 127 + suwak1.Value)
                    {
                        b = (127 / (127 + suwak1.Value)) * b;
                    }
                    else if (b > 127 - suwak1.Value)
                    {
                        b = (127 * b + 255 * suwak1.Value) / (127 + suwak1.Value);
                    }
                    else
                    {
                        b = 127;
                    }

                    bitmapEdited.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pic.Image = bitmapEdited;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog Opfile = new OpenFileDialog();
            if (DialogResult.OK == Opfile.ShowDialog())
            {
                this.pic.Image = new Bitmap(Opfile.FileName);
                bitmap = (Bitmap)this.pic.Image;

                Array.Clear(histR, 0, histR.Length);
                Array.Clear(histG, 0, histG.Length);
                Array.Clear(histB, 0, histB.Length);


                histogram();

                read = true;

                panel1.Invalidate();
                panel2.Invalidate();
                panel3.Invalidate();
                panel1.Paint += new PaintEventHandler(panel1_Paint);
                panel2.Paint += new PaintEventHandler(panel2_Paint);
                panel3.Paint += new PaintEventHandler(panel3_Paint);
                suwak1.Value = 0;
            }
        }

        private void histogram()
        {
            int width = pic.Image.Width;
            int height = pic.Image.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cA = bitmap.GetPixel(x, y);

                    int r = cA.R;
                    int g = cA.G;
                    int b = cA.B;

                    histR[r]++;
                    histG[g]++;
                    histB[b]++;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (read == true)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float r = histR[i];

                    r = r / (pic.Image.Height * pic.Image.Width);
                    r *= 6000;

                    graphR.DrawLine(new Pen(Color.Red), i, panel1.Height, i, panel1.Height - r);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (read == true)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float r = histG[i];

                    r = r / (pic.Image.Height * pic.Image.Width);
                    r *= 6000;

                    graphR.DrawLine(new Pen(Color.Green), i, panel1.Height, i, panel1.Height - r);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (read == true)
            {
                Graphics graphR = e.Graphics;

                for (int i = 0; i < 256; i++)
                {
                    float r = histB[i];

                    r = r / (pic.Image.Height * pic.Image.Width);
                    r *= 6000;

                    graphR.DrawLine(new Pen(Color.Blue), i, panel1.Height, i, panel1.Height - r);
                }
            }
        }
    }
}
