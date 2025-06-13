using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Snake
{
    public partial class Form1 : Form
    {

        public Scene Scene {  get; set; }
        public bool IsPaused = false;
        public int Level = 1;
        public int[] Speeds = { 100, 150, 200 };
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(800, 624);
            this.Text = "Simple Snake";
            this.DoubleBuffered = true;
            this.Icon = new Icon("snake.ico");
            Init();

        }
        public void Init()
        {
            Scene = new Scene(ClientSize.Width, ClientSize.Height - 24);
            applesLabel.Text = String.Format("Level: {0} Apples: {1}/{2}", Level, Scene.Grid.ApplesEaten, Scene.Grid.ApplesGoal);
            int Speed = Speeds[Scene.Random.Next(Speeds.Length)];
            moveTicks.Interval = Speed;
            KeyDelay.Interval = Speed;
            moveTicks.Start();
        }
        public void GameOver(string message)
        {
            Level = 1;

            DialogResult result = MessageBox.Show(message, "Game Over", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                Init();
            else
                this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Scene.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Scene.Snake.Move();
            int SnakeHeadX = Scene.Snake.Body[0].X;
            int SnakeHeadY = Scene.Snake.Body[0].Y;
            int SnakeTailX = Scene.Snake.Body.Last().X;
            int SnakeTailY = Scene.Snake.Body.Last().Y;
        

            for (int i = 1; i < Scene.Snake.Body.Count - 1; i++)
            {
                if (Scene.Snake.Body[i].Equals(Scene.Snake.Body[0]))
                {
                    moveTicks.Stop();
                    GameOver("You seem to have collided with the snake. Try again?");
                    return;
                }
            }

            if( SnakeHeadX >= Scene.Grid.VerticesH.Count - 1 || SnakeHeadX < 0 ||
                SnakeHeadY >= Scene.Grid.VerticesV.Count - 1 || SnakeHeadY < 0)
            {
                moveTicks.Stop();
                GameOver("You seem to have hit a wall. Try again?");
                return;
            }

            if (Scene.Grid.Coords[SnakeHeadY, SnakeHeadX] != 2)
            {
                Scene.Grid.Coords[SnakeTailY, SnakeTailX] = 0;
                Scene.Snake.Body.RemoveAt(Scene.Snake.Body.Count - 1);
            }
            else
            {
                Scene.Grid.Apples.Remove(new Point(SnakeHeadX, SnakeHeadY));
                Scene.Grid.ApplesEaten++;
                Scene.Grid.ApplesPresent--;
                applesLabel.Text = String.Format("Level: {0} Apples: {1}/{2}", Level, Scene.Grid.ApplesEaten, Scene.Grid.ApplesGoal);
                Scene.GenerateApple();
                if(Scene.Grid.ApplesEaten == Scene.Grid.ApplesGoal)
                {
                    Level++;
                    Init();
                }
            }

            Scene.Grid.Coords[SnakeHeadY, SnakeHeadX] = 1;
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDelay.Enabled)
                return;

            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (Scene.Snake.Direction != "Up" && !IsPaused)
                        Scene.Snake.Direction = "Down";
                    break;
                case Keys.Up:
                    if (Scene.Snake.Direction != "Down" && !IsPaused)
                        Scene.Snake.Direction = "Up";
                    break;
                case Keys.Left:
                    if (Scene.Snake.Direction != "Right" && !IsPaused)
                        Scene.Snake.Direction = "Left";
                    break;
                case Keys.Right:
                    if (Scene.Snake.Direction != "Left" && !IsPaused)
                        Scene.Snake.Direction = "Right";
                    break;
                case Keys.Escape:
                    IsPaused = !IsPaused;
                    if (IsPaused)
                    {
                        moveTicks.Stop();
                        KeyDelay.Stop();
                    }
                    else
                    {
                        moveTicks.Start();
                        KeyDelay.Start();
                    }
                    break;
            

            }
            KeyDelay.Start();
        }

        private void KeyDelay_Tick(object sender, EventArgs e)
        {
            KeyDelay.Stop();
        }

      
    }
}
