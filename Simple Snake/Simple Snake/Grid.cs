using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Snake
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int CellSize { get; set; }


        public List<int> VerticesH;
        public List<int> VerticesV;

        public List<Point> Apples;

        public int ApplesGoal;
        public int ApplesEaten;
        public int ApplesPresent;

        public int[,] Coords { get; set; }

        public Grid(int Width, int Height, int CellSize, int ApplesGoal) {
            this.Width = Width;
            this.Height = Height;
            this.CellSize = CellSize;
            this.ApplesGoal = ApplesGoal;
            VerticesH = new List<int>();
            VerticesV = new List<int>();
            Apples = new List<Point>();
            
            for(int i = 0; i <= Width; i++)
            {
                if (i % CellSize == 0)
                    VerticesH.Add(i);
            }
            for (int i = 0; i <= Height; i++)
            {
                if(i % CellSize == 0)
                    VerticesV.Add(i);
            }

            Coords = new int[VerticesV.Count - 1, VerticesH.Count - 1];

            for (int i = 0; i < VerticesV.Count - 1; i++)
            {
                for (int j = 0; j < VerticesH.Count - 1; j++)
                    Coords[i, j] = 0;
            }

            Coords[0, 0] = 1;
            Coords[1, 0] = 1;
            Coords[2, 0] = 1;
        
        }

        

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            Brush b = new SolidBrush(Color.Red);

            for (int i = 0; i < VerticesV.Count; i++)
            {
                g.DrawLine(p, new Point(0, VerticesV[i]), new Point(Width, VerticesV[i]));
            }
            for(int i = 0; i < VerticesH.Count; i++)
            {
                g.DrawLine(p, new Point(VerticesH[i], 0), new Point(VerticesH[i], Height));
            }

            foreach(Point a in Apples)
            {
                g.FillRectangle(b, a.X * CellSize, a.Y * CellSize, CellSize, CellSize);
            }
            p.Dispose();
            b.Dispose();
        }
    }
}
