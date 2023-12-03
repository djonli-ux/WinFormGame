using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18_05_2023_WinForms_Exam_It_Step_
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging, lose = false;
        private int countCoins = 0;
        public Form1()
        {
            InitializeComponent();

            road1.MouseDown += MouseClickDown;
            road1.MouseUp += MouseClickUp;
            road1.MouseMove += MouseClickMove;

            road2.MouseDown += MouseClickDown;
            road2.MouseUp += MouseClickUp;
            road2.MouseMove += MouseClickMove;

            labelLose.Visible = false;
            btnRestart.Visible = false;
            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging) 
            {
                Point curPos = PointToScreen(new Point(e.X, e.Y)); 
                this.Location = new Point(curPos.X - pos.X, curPos.Y - pos.Y + road1.Top);
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int speed = 5;
            road1.Top += speed;
            road2.Top += speed;

            int carSpeed = 5;
            enemy1.Top += carSpeed;
            enemy2.Top += carSpeed;

            coin.Top += speed;

            if (coin.Top >= 650)
            {
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(150, 560);
            }

            if (enemy1.Top >= 650)
            {
                Random rand = new Random();
                enemy1.Left = rand.Next(150, 300);
                enemy1.Top = -130;
            }

            if (enemy2.Top >= 650)
            {
                Random rand = new Random();
                enemy2.Left = rand.Next(300, 560);
                enemy2.Top = -400;
            }

            if (road1.Top >= 650) 
            {
                road1.Top = 0;
                road2.Top = -650;
            }

            if (player.Bounds.IntersectsWith(enemy1.Bounds) 
                || player.Bounds.IntersectsWith(enemy2.Bounds)) 
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                btnRestart.Visible = true;
                lose = true;
            }

            if (player.Bounds.IntersectsWith(coin.Bounds)) 
            {
                countCoins++;
                labelCoins.Text = "Coins: " + countCoins.ToString();
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(150, 560);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose)
                return;

            int speed = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left > 150)
                player.Left -= speed;
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Right < 700) 
                player.Left += speed;

        }

        private void road1_Click(object sender, EventArgs e)
        {

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            enemy1.Top = -130;
            enemy2.Top = -400;
            labelLose.Visible = false;
            btnRestart.Visible = false;
            timer.Enabled = true;
            lose = false;
            countCoins = 0;
            labelCoins.Text = "Coins: 0";
            coin.Top = -500;

        }
    }
}
