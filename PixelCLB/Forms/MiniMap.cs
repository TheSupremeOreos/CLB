using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PixelCLB
{
    public partial class MiniMap : Form
    {
        public int finalx;
		public int finaly;
		private short xclick;
		private short yclick;
		private Bitmap bm;
		private MiniMapInfo mmi;
        private Component components;

        private void CharacterInfo_Load(object sender, EventArgs e)
        {
        }

        public MiniMap(MiniMapInfo minimapInfo, Portal portal = null)
		{
			InitializeComponent();
            mmi = minimapInfo;
			if (portal != null)
			{
				object[] x = new object[] { "MiniMap X:", portal.X, " Y:", portal.Y };
				Text = string.Concat(x);
				double width = (double)(portal.X - mmi.centerX * -1);
				width = width / (double)mmi.width;
				width = width * (double)mmi.canvas.Width;
				double y = (double)(portal.Y - mmi.centerY * -1);
				y = y / (double)mmi.height;
				y = y * (double)mmi.canvas.Height;
				SolidBrush solidBrush = new SolidBrush(Color.Red);
				Graphics graphic = Graphics.FromImage(mmi.canvas);
				graphic.FillRectangle(solidBrush, new Rectangle((int)width - 3, (int)y - 3, 7, 6));
				solidBrush.Dispose();
				graphic.Dispose();
			}
			bm = mmi.canvas;
            mmi.canvas = MiniMap.ResizeBitmap(mmi.canvas, (int)((double)mmi.canvas.Width * 1.5), (int)((double)mmi.canvas.Height * 1.5));
			pictureBox.Image = mmi.canvas;
			pictureBox.Height = mmi.canvas.Height;
			pictureBox.Width = mmi.canvas.Width;
			base.Size = new System.Drawing.Size(mmi.canvas.Width + 18, mmi.canvas.Height + 36);
		}


        private void MiniMap_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                double num = 0;
                double num1 = 0;
                xclick = (short)e.X;
                yclick = (short)e.Y;
                num = (xclick < 1 ? 50 : (double)xclick / (double)mmi.canvas.Width);
                double num2 = num;
                num1 = (yclick < 1 ? 50 : (double)yclick / (double)mmi.canvas.Height);
                double num3 = num1;
                int num4 = (int)(num2 * (double)mmi.width);
                int num5 = (int)(num3 * (double)mmi.height);
                finalx = mmi.centerX * -1 + num4;
                finaly = mmi.centerY * -1 + num5;
                Foothold foothold = Database.loadMap(mmi.id).footholds.findBelow(new Point(finalx, finaly));
                if (foothold == null)
                {
                    MessageBox.Show("Pick a spawnpoint with a foothold below it.");
                    return;
                }
                else
                {
                    Clipboard.SetText(finalx.ToString() + "," + finaly.ToString());
                    MessageBox.Show("Copied to following coords to clipboard:\n" + finalx.ToString() + "," + finaly.ToString());
                    return;
                }
            }
            catch
            {
            }
        }

        private void MiniMap_Resize(object sender, EventArgs e)
        {
            System.Drawing.Size size = base.Size;
            int width = size.Width - 36;
            System.Drawing.Size size1 = base.Size;
            int height = size1.Height - 72;
            if (width < 20)
            {
                width = 20;
            }
            if (height < 40)
            {
                height = 40;
            }
            mmi.canvas = MiniMap.ResizeBitmap(bm, width, height);
            pictureBox.Image = mmi.canvas;
            pictureBox.Height = mmi.canvas.Height;
            pictureBox.Width = mmi.canvas.Width;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double num;
            double num1;
            xclick = (short)e.X;
            yclick = (short)e.Y;
            num = (xclick < 1 ? 50 : (double)xclick / (double)mmi.canvas.Width);
            double num2 = num;
            num1 = (yclick < 1 ? 50 : (double)yclick / (double)mmi.canvas.Height);
            double num3 = num1;
            int num4 = (int)(num2 * (double)mmi.width);
            int num5 = (int)(num3 * (double)mmi.height);
            finalx = mmi.centerX * -1 + num4;
            finaly = mmi.centerY * -1 + num5;
            toolStripLabelX.Text = "X: " + finalx.ToString();
            toolStripLabelY.Text = "Y: " + finaly.ToString();
            Foothold foothold = Database.loadMap(mmi.id).footholds.findBelow(new Point(finalx, finaly));
            if (foothold == null)
            {
                pictureBox.Refresh();
                SolidBrush solidBrush = new SolidBrush(Color.White);
                Graphics graphic = pictureBox.CreateGraphics();
                graphic.FillRectangle(solidBrush, new Rectangle(e.X - 3, e.Y - 3, 7, 7));
                solidBrush.Dispose();
                graphic.Dispose();
                return;
            }
            else
            {
                double width = (double)(finalx - mmi.centerX * -1);
                width = width / (double)mmi.width;
                width = width * (double)mmi.canvas.Width;
                double y1 = (double)(foothold.getY1() - mmi.centerY * -1);
                y1 = y1 / (double)mmi.height;
                y1 = y1 * (double)mmi.canvas.Height;
                pictureBox.Refresh();
                SolidBrush solidBrush1 = new SolidBrush(Color.Pink);
                Graphics graphic1 = pictureBox.CreateGraphics();
                graphic1.FillRectangle(solidBrush1, new Rectangle((int)width - 3, (int)y1 - 3, 7, 6));
                solidBrush1.Dispose();
                graphic1.Dispose();
                return;
            }
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphic = Graphics.FromImage(bitmap);
            using (graphic)
            {
                graphic.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return bitmap;
        }
    }
}
