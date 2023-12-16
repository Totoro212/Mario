using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario1
{
    public partial class Form1 : Form
    {
        


        Form2 fr2 = new Form2();
        Bitmap B;
        Graphics G;
        Random R = new Random();
        int jumpingHeight2 = 10;

        int X;

        int gravity = 5;

        int mcSpeed = 7;
        int bgSpeed = 7;
        int score = 0;

        int Kx1, Ky1, Kx2, Ky2;

        bool jumping = false, goLeft = false, goRight = false;
        int jumpingHeight = 10;


        bool canRightEn = true;


        double mcWidth = 60;
        double mcHeight = 60;

        Image clouds = Properties.Resources.clouds;
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;

            Kx1 = bg.Width; Kx2 = bg.Width; Ky1 = R.Next(40, 80); Ky2 = R.Next(40, 80);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Action_Cloud();
            Gravity();
            Jumping();
            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                        label1.Text = "Score: " + score;

                    }
                }
                if (x is PictureBox && (string)x.Tag == "grounds")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {

                        if (mc.Location.X + mc.Width >= x.Location.X || mc.Right == x.Left)
                            mc.Top = x.Top - mc.Height;

                        else if (mc.Location.Y != x.Location.Y)
                            Gravity();



                        X = mc.Location.X;
                    }

                }
                if (x is PictureBox && (string)x.Tag == "truba")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {
                        mc.Top = x.Top - mc.Height;
                        if (mc.Top + mc.Height == x.Top/* && score == 43*/)
                        {
                            mc.Visible = false;
                            this.Hide();
                            try 
                            { 
                                fr2.Show(); 
                            }
                            catch(ObjectDisposedException)
                            {
                                MessageBox.Show("чкта не то");
                                Application.Exit();
                            }
                        }
                    }

                }
                if (x is PictureBox && (string)x.Tag == "enemy")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {

                        if (mc.Bottom >= x.Top && mc.Location.X - mc.Height != x.Location.X - x.Height)
                        {
                            x.Visible = false;
                            x.Top = -bg.Top;
                            mc.Size = new Size((int)mcWidth * 3 / 2, (int)mcHeight * 3 / 2);
                        }
                    }

                }
                if (mc.Top + mc.Height > this.ClientSize.Height && mc.Visible == true)
                {
                    timer1.Stop();
                    MessageBox.Show("gg");
                    Application.Exit();
                }
            }

            if (goLeft)
            {
                if (mc.Left > mc.Width * 3)
                    mc.Left -= mcSpeed;
                mc.Image = Properties.Resources.mc2;
                Action_Elements("forward");
            }
            if (goRight)
            {
                if (mc.Right < this.Width - mc.Width * 3)
                    mc.Left += mcSpeed;
                mc.Image = Properties.Resources.mc;
                Action_Elements("back");
            }


            if (enemy_mash.Left <= pictureBox9.Location.X && canRightEn) canRightEn = false;
            if (enemy_mash.Left >= pictureBox8.Location.X + pictureBox8.Width - enemy_mash.Width && !canRightEn) canRightEn = true;
            if (canRightEn)
                enemy_mash.Left -= 5;
            else enemy_mash.Left += 5;


        }


        public void Jumping()
        {
            if (jumping)
            {
                if (jumpingHeight != 0)
                {
                    mc.Top -= 12;
                    jumpingHeight -= 1;
                }
                if (jumpingHeight == 0)
                {
                    mc.Top += 12;
                    jumpingHeight2 -= 1;
                    if (jumpingHeight2 == 0) 
                    { 
                        jumping = false; 
                        jumpingHeight = 10; 
                        jumpingHeight2 = 10; 
                    }
                }

            }
        }


        void Gravity()
        {
            if (mc.Top - mc.Height != X)
            {
                mc.Top += gravity;
            }
        }

        private void mc_Click(object sender, EventArgs e)
        {

        }

        void Action_Cloud()
        {

            B = new Bitmap(bg.Width, bg.Height);
            G = Graphics.FromImage(B);
            G.DrawImage(new Bitmap(clouds), Kx1, Ky1, 160, 80);

            if (Kx1 < bg.Width / 2)
            {
                Kx2 = bg.Width;
                Ky2 = R.Next(0, 40);
                G.DrawImage(new Bitmap(clouds), Kx2, Ky2, 100, 80);
            }
            if (Kx1 < -clouds.Width)
            {
                Kx1 = bg.Width;
                Ky1 = R.Next(40, 80);
                G.DrawImage(new Bitmap(clouds), Kx1, Ky1, 160, 80);

            }

            Kx1 -= 3;
            Kx2 -= 3;
            bg.Image = B;
        }

        void Action_Elements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if ((x is PictureBox && (string)x.Tag == "grounds")
                    || (x is PictureBox && (string)x.Tag == "coin")
                    || (x is PictureBox && (string)x.Tag == "truba")
                    || (x is PictureBox && (string)x.Tag == "enemy"))
                {
                    if (direction == "back")
                    {
                        x.Left -= bgSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += bgSpeed;
                    }
                }


            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = true;

            }
            if (e.KeyCode == Keys.D)
            {
                goRight = true;

            }
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;

            }
            if (e.KeyCode == Keys.D)
            {
                goRight = false;

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
