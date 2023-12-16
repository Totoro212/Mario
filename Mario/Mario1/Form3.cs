using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario1
{
    public partial class Form3 : Form
    {
        int jumpingHeight2 = 10;

        int X;

        int gravity = 5;

        int mcSpeed = 10;
        public int score = 0;


        bool jumping = false, goLeft = false, goRight = false;
        int jumpingHeight = 10;

        public Form3()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Gravity();
            Jumping();

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score++;
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
                if (x is PictureBox && (string)x.Tag == "princes")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {
                        timer1.Stop();
                        MessageBox.Show("Ну и зачем ты к ней пришел");
                        Application.Restart();
                    }


                }
                if (x is PictureBox && (string)x.Tag == "flag")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {
                        timer1.Stop();
                        MessageBox.Show("надеюсь добавлять ничего не надо будет");
                        Application.Restart();
                    }


                }
                if (mc.Top + mc.Height > bg.ClientSize.Height && mc.Visible == true)
                {
                    timer1.Stop();
                    MessageBox.Show("gg");
                    Application.Exit();
                }
            }

            if (goLeft)
            {
                if (mc.Left > mc.Width)
                    mc.Left -= mcSpeed;
                mc.Image = Properties.Resources.mc2;
            }
            if (goRight)
            {
                if (mc.Right < this.Width - mc.Width)
                    mc.Left += mcSpeed;
                mc.Image = Properties.Resources.mc;
            }

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
                    if (jumpingHeight2 == 0) { jumping = false; jumpingHeight = 10; jumpingHeight2 = 10; }
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


        private void Form3_KeyDown(object sender, KeyEventArgs e)
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

        private void Form3_KeyUp(object sender, KeyEventArgs e)
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
    }
}
