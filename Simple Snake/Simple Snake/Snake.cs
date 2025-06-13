using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Snake
{
    public class Snake
    {
        public List<Point> Body { get; set; }
        public int Size { get; set; }

        public bool IsEatingSelf {  get; set; }

        public string Direction;

        public Snake(int Size)
        {
            this.Size = Size;
            this.Direction = "Down";
            Body = new List<Point>();
            Body.Insert(0, new Point(0, 0));
            Body.Insert(0, new Point(0, 1));
            Body.Insert(0, new Point(0, 2));
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Green);
            foreach (Point p in Body)
            {
                g.FillRectangle(b, p.X * Size, p.Y * Size, Size, Size);
            }
            b.Dispose();
            
        }

        public void Move()
        {
            switch (Direction)
            {
                case "Down":
                    Body.Insert(0, new Point(Body[0].X, Body[0].Y + 1));
                    break;
                case "Up":
                    Body.Insert(0, new Point(Body[0].X, Body[0].Y - 1));
                    break;
                case "Left":
                    Body.Insert(0, new Point(Body[0].X - 1, Body[0].Y));
                    break;
                case "Right":
                    Body.Insert(0, new Point(Body[0].X + 1, Body[0].Y));
                    break;
            }
        }
    }
}
