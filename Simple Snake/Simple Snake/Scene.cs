using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Snake
{
    public class Scene
    {
        public Grid Grid { get; set; }
        public Snake Snake { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CellSize { get; set; }

        public Random Random { get; set; }

        public Scene(int Width, int Height) {
            Random = new Random();

            this.CellSize = Random.Next(40, 100);
            while(Width % CellSize != 0 || Height % CellSize != 0)
                this.CellSize = Random.Next(40, 100);

            this.Width = Width;
            this.Height = Height;
            Grid = new Grid(Width, Height, CellSize, Random.Next(5, 30));
            Snake = new Snake(CellSize);
            GenerateApple();
            GenerateApple();
            GenerateApple();


        }

        public void GenerateApple()
        {
            if (Grid.ApplesPresent + Grid.ApplesEaten != Grid.ApplesGoal)
            {
                Grid.ApplesPresent++;
                int AppleX = Random.Next(Grid.VerticesH.Count - 1);
                int AppleY = Random.Next(Grid.VerticesV.Count - 1);
                while (Grid.Coords[AppleY, AppleX] != 0)
                {
                    AppleX = Random.Next(Grid.VerticesH.Count - 1);
                    AppleY = Random.Next(Grid.VerticesV.Count - 1);
                }
                Grid.Apples.Add(new Point(AppleX, AppleY));
                Grid.Coords[AppleY, AppleX] = 2;
            }
        }

        public void Draw(Graphics g)
        {
            Grid.Draw(g);
            Snake.Draw(g);
        }
    }
}
