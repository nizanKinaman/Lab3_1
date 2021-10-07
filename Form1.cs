using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab3_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g.Clear(Color.White);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
        

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }


        //public class Points
        //{
        //    public Point[] points_array;
        //    public int index = 0;
        //}

        static Bitmap bmp = new Bitmap(455, 426);

        bool mouse_Down = false;

        bool is_fill = false;

        public Graphics g = Graphics.FromImage(bmp);

        Pen myPen = new Pen(Color.Black, 3f);

        Pen pen_fill = new Pen(Color.Black);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_Down = true;
            is_fill = true;
            points[0] = new Point(e.X, e.Y);
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.Color = colorDialog1.Color;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_Down = false;

            if (!checkBox1.Checked)
            {
                if (is_fill)
                    FloodFill(e.X, e.Y);
            }
            else
            {
                if (is_fill)
                    FloodFillImg(e.X, e.Y);
            }
        }

        Point[] points = new Point[2];
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            is_fill = false;
            if (mouse_Down)
            {
                points[1] = new Point(e.X, e.Y);
                g.DrawLines(myPen, points);
                pictureBox1.Image = bmp;
                points[0] = points[1];
                points[1] = new Point(e.X, e.Y);
            }
        }

        //void FloodFill(int x, int y)
        //{
        //    Color col = bmp.GetPixel(x, y);
        //    pen_fill.Color = colorDialog1.Color;
        //    int left_x_bound = x - 1;
        //    int cur_y = y;
        //    Color cur_col = bmp.GetPixel(left_x_bound, cur_y);
        //    while (left_x_bound != 0 && cur_col == col)
        //    {
        //        left_x_bound--;
        //        cur_col = bmp.GetPixel(left_x_bound, cur_y);

        //    }
        //    left_x_bound++;


        //    int right_x_bound = x + 1;
        //    cur_col = bmp.GetPixel(right_x_bound, cur_y);
        //    while (right_x_bound != pictureBox1.Width && cur_col == col)
        //    {
        //        right_x_bound++;
        //        cur_col = bmp.GetPixel(right_x_bound, cur_y);
        //        x++;
        //    }
        //    right_x_bound--;


        //    g.DrawLine(pen_fill, new Point(left_x_bound, y), new Point(right_x_bound, y));

        //    //while (bmp.GetPixel(check_x++, y + 1) != col)
        //    //{
        //    //    x = check_x + 1;
        //    //}
        //    pictureBox1.Image = bmp;
        //    if (y + 1 < pictureBox1.Height && col == bmp.GetPixel(x, y + 1))
        //    {
        //        FloodFill(x, y + 1);
        //    }
        //    if (y - 1 > 0 && col == bmp.GetPixel(x, y - 1))
        //    {
        //        FloodFill(x, y - 1);
        //    }

        //    //pictureBox1.Image = bmp;
        //}

        void FloodFill(int x, int y)
        {
            Color col = bmp.GetPixel(x, y);
            //MessageBox.Show(Color.Black + "");
            pen_fill.Color = colorDialog1.Color;
            
            //MessageBox.Show(col.Name +" "+ pen_fill.Color.Equals(col.Name));
            if (!(pen_fill.Color.R == col.R && pen_fill.Color.G == col.G && pen_fill.Color.B == col.B))
            {
                int left_x_bound = x;
                int cur_y = y;
                Color cur_col = bmp.GetPixel(left_x_bound, cur_y);
                while (left_x_bound != 1 && cur_col == col)
                {
                    left_x_bound--;
                    cur_col = bmp.GetPixel(left_x_bound, cur_y);
                }
                left_x_bound++;

                int right_x_bound = x;
                cur_col = bmp.GetPixel(right_x_bound, cur_y);
                while (right_x_bound != pictureBox1.Width - 1 && cur_col == col)
                {
                    right_x_bound++;
                    cur_col = bmp.GetPixel(right_x_bound, cur_y);
                }
                right_x_bound--;
                g.DrawLine(pen_fill, new Point(left_x_bound, y), new Point(right_x_bound, y));
                pictureBox1.Image = bmp;

                int check_x = left_x_bound;
                while (bmp.GetPixel(++check_x, y + 1) != col && check_x < right_x_bound) { }
                if (y + 1 < pictureBox1.Height - 1 && col == bmp.GetPixel(check_x, y + 1))
                {
                    FloodFill(check_x, y + 1);
                }

                check_x = right_x_bound;
                while (bmp.GetPixel(--check_x, y + 1) != col && check_x > left_x_bound) { }
                if (y + 1 < pictureBox1.Height - 1 && col == bmp.GetPixel(check_x, y + 1))
                {
                    FloodFill(check_x, y + 1);
                }

                check_x = left_x_bound;
                while (bmp.GetPixel(++check_x, y - 1) != col && check_x < right_x_bound) { }
                if (y - 1 > 0 && col == bmp.GetPixel(check_x, y - 1))
                {
                    FloodFill(check_x, y - 1);
                }

                check_x = right_x_bound;
                while (bmp.GetPixel(--check_x, y - 1) != col && check_x > left_x_bound) { }
                if (y - 1 > 0 && col == bmp.GetPixel(check_x, y - 1))
                {
                    FloodFill(check_x, y - 1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
        }

        public string img;
        public Bitmap bmp_pic;
        
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpeg;*.jpg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.img = ofd.FileName;
                    bmp_pic = new Bitmap(this.img); 
                }
                
            }
        }
        
        void FloodFillImg(int x, int y)
        {
            Color col = bmp.GetPixel(x, y);
            //bmp_pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //MessageBox.Show(Color.Black + "");
            pen_fill.Color = colorDialog1.Color;
            int left_x_bound = x;
            int cur_y = y;
            Color cur_col = bmp.GetPixel(left_x_bound, cur_y);
            int right_x_bound = x;
            Color cur_col2 = bmp.GetPixel(right_x_bound, cur_y);
			if (!(pen_fill.Color.R == col.R && pen_fill.Color.G == col.G && pen_fill.Color.B == col.B))
			{
				while (left_x_bound != 1 && cur_col == col)
				{
					cur_col = bmp.GetPixel(left_x_bound, cur_y);
					bmp.SetPixel(left_x_bound, cur_y, bmp_pic.GetPixel(left_x_bound % bmp_pic.Width, cur_y % bmp_pic.Height));
					left_x_bound--;
				}
				left_x_bound++;

				while (right_x_bound != pictureBox1.Width - 1 && cur_col2 == col)
				{
					right_x_bound++;
					cur_col2 = bmp.GetPixel(right_x_bound, cur_y);
					bmp.SetPixel(right_x_bound, cur_y, bmp_pic.GetPixel(right_x_bound % bmp_pic.Width, cur_y % bmp_pic.Height));
				}
				//g.DrawLine(pen_fill, new Point(left_x_bound, y), new Point(right_x_bound, y));
				right_x_bound--;
				pictureBox1.Image = bmp;

				int check_x = left_x_bound;
				while (bmp.GetPixel(++check_x, y + 1) != col && check_x < right_x_bound) { }
				if (y + 1 < pictureBox1.Height - 1 && col == bmp.GetPixel(check_x, y + 1))
				{
					FloodFillImg(check_x, y + 1);
				}

				check_x = right_x_bound;
				while (bmp.GetPixel(--check_x, y + 1) != col && check_x > left_x_bound) { }
				if (y + 1 < pictureBox1.Height - 1 && col == bmp.GetPixel(check_x, y + 1))
				{
					FloodFillImg(check_x, y + 1);
				}

				check_x = left_x_bound;
				while (bmp.GetPixel(++check_x, y - 1) != col && check_x < right_x_bound) { }
				if (y - 1 > 0 && col == bmp.GetPixel(check_x, y - 1))
				{
					FloodFillImg(check_x, y - 1);
				}

				check_x = right_x_bound;
				while (bmp.GetPixel(--check_x, y - 1) != col && check_x > left_x_bound) { }
				if (y - 1 > 0 && col == bmp.GetPixel(check_x, y - 1))
				{
					FloodFillImg(check_x, y - 1);
				}
			}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
