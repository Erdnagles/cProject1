using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sammelselg_kursusetöö
{
    public partial class Form1 : Form
    {
        Point startLocation;
        int countDown = 0;
        Graphics g;
        int x = 26;
        int y = 483;
        bool drawLine;
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            g = PanelGameFloor.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 2);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void InitializeGame()
        {
            GameTimeLeft.Start();
            startLocation = GameSquirrelStart.Location;
            Cursor.Position = PointToScreen(startLocation);
            countDown = 100;
            PanelGameFloor.Refresh();
        }

        private void LabyrinthWall_MouseEnter(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void GameTimeLeft_Tick(object sender, EventArgs e)
        {
            if(countDown < 0)
            {
                GameTimeLeft.Stop();
                DialogResult userChoice = MessageBox.Show("You did not make it this time! Unfortunately Squirrel Bob feels let down \nWant to try again?", "Information", MessageBoxButtons.YesNo);
                if (userChoice == DialogResult.Yes)
                {
                    InitializeGame();
                }
                else
                {
                    this.Close();
                }
            }

            lblTimeLeft.Text = countDown.ToString();
            countDown--;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            GameTimeLeft.Stop();
            DialogResult userChoice=MessageBox.Show("Success! Squirrel Bob received his chestnut and is grateful for your help.\nPlay Again?", "Information", MessageBoxButtons.YesNo);
            if(userChoice == DialogResult.Yes)
            {
                InitializeGame();
            }
            else
            {
                this.Close();
            }
        }

        private void buttonHowTo_Click(object sender, EventArgs e)
        {

            HowToPlay howToPlayScreen = new HowToPlay();
            howToPlayScreen.Show();
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            GameTimeLeft.Stop();
        }

        private void buttonEasyMode_Click(object sender, EventArgs e)
        {
            countDown = 150;
        }

        private void buttonHardMode_Click(object sender, EventArgs e)
        {
            countDown = 50;
        }

        private void PanelGameFloor_MouseDown(object sender, MouseEventArgs e)
        {
            drawLine = true;
        }

        private void PanelGameFloor_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawLine == true)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }
    }
}
