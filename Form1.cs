using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeGame
{
    public partial class snake : Form
    {

        private List<circle> Snake = new List<circle>();
        private circle food = new circle();

        int maxWidth;
        int maxHeigth;

        int score;

        Random rand = new Random();

        bool goLeft, goRight, goUp, goDown;

        public snake()
        {
            InitializeComponent();
            new setting();
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && setting.directions != "right")
            {
                goLeft = true;
            }

            if(e.KeyCode == Keys.Right && setting.directions != "left")
            {
                goRight = true;
            }

            if(e.KeyCode == Keys.Up && setting.directions != "down")
            {
                goUp = true;
            }

            if(e.KeyCode == Keys.Down && setting.directions != "up")
            {
                goDown = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            restartGame();
        }

        private void restartGame()
        {
            maxWidth = picCanvas.Width / setting.width - 1;
            maxHeigth = picCanvas.Height / setting.height - 1;

            Snake.Clear();

            btnStart.Enabled = false;
            score = 0;
            lblScore.Text = "Score : " + score;

            circle head = new circle { x = 10, y = 5 };
            Snake.Add(head);

            for (int i = 0; i < 5; i++)
            {
                circle body = new circle();
                Snake.Add(body);    
            }

            food = new circle { x = rand.Next(2, maxWidth),y = rand.Next(2, maxHeigth) };

            gameTimer.Start();
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush snakeColor;

            for (int i = 0; i < Snake.Count -1; i++)
            {
                if(i == 0)
                {
                    snakeColor = Brushes.White;
                }
                else
                {
                    snakeColor = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColor, new Rectangle
                    (
                    Snake[i].x * setting.width,
                    Snake[i].y * setting.height,
                    setting.width, setting.height
                    ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.x * setting.width,
            food.y * setting.height,
            setting.width, setting.height
            ));
        }

        private void gameTimerEvent_Tick(object sender, EventArgs e)
        {
            if (goLeft) { setting.directions = "left"; }

            if (goRight) { setting.directions = "right"; }

            if (goDown) { setting.directions = "down"; }

            if (goUp) { setting.directions = "up"; }

            for (int i = Snake.Count -1; i >= 0; i--)
            {
                if(i == 0)
                {
                    switch(setting.directions)
                    {
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }

                    if (Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeigth;
                    }
                    if (Snake[i].y > maxHeigth)
                    {
                        Snake[i].y = 0;
                    }

                    if (Snake[i].x == food.x && Snake[i].y == food.y)
                    {
                        eatPoint();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            gameOver();
                        }
                    }
                }
                else
                {
                    Snake[i].x = Snake[i-1].x;
                    Snake[i].y = Snake[i-1].y;
                }
            }
            picCanvas.Invalidate();
        }

        private void eatPoint()
        {
            score += 1;

            lblScore.Text = "Score : " + score;

            circle body = new circle
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };
            Snake.Add(body);

            food = new circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeigth) };
        }

        private void gameOver()
        {
            gameTimer.Stop();
            btnStart.Enabled = true;
        }
    }
}
