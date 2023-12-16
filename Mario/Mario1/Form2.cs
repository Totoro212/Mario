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
    public partial class Form2 : Form
    {
        Form3 fr3 = new Form3();

        int jumpingHeight2 = 10;

        int X;

        int gravity = 5;

        int mcSpeed = 20;
        int score = 0;


        bool jumping = false, goLeft = false, goRight = false;
        int jumpingHeight = 10;



        public Form2()
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
                if (x is PictureBox && (string)x.Tag == "door")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {
                        timer1.Stop();
                        this.Hide();
                        fr3.Show();
                    }

                }
                if (x is PictureBox && (string)x.Tag == "enemy2")
                {
                    if (mc.Bounds.IntersectsWith(x.Bounds))
                    {
                        
                        if ((mc.Right >= x.Left || mc.Left <= x.Right) || (mc.Location.X - mc.Height == x.Top) && x.Visible == true)
                        {
                            if (heart1.Visible == true)
                            {
                                heart1.Visible = false;
                                x.Visible = false;
                            }
                            else if (heart2.Visible == false && x.Visible == true)
                            {
                                heart3.Visible = false;
                                x.Visible = false;
                                timer1.Stop();
                                MessageBox.Show("lose");
                                Application.Restart();
                            }
                            else if (heart1.Visible == false && x.Visible == true)
                            {
                                heart2.Visible = false;
                                x.Visible = false;
                            }

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


        private void Form2_KeyDown(object sender, KeyEventArgs e)
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

        private void Form2_KeyUp(object sender, KeyEventArgs e)
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




        private void mc_Click(object sender, EventArgs e)
        {

        }
    }
}
