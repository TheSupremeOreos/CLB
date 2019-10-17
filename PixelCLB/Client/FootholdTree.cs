using System;
using System.Collections.Generic;
using System.Drawing;

namespace PixelCLB
{
    public class FootholdTree : IDisposable
    {
        private List<Foothold> footholds = new List<Foothold>();

        private Point p1;

        private Point p2;

        public FootholdTree(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Foothold findBelow(Point p)
        {
            int num;
            Foothold foothold;
            List<Foothold> footholds = new List<Foothold>();
            foreach (Foothold foothold1 in this.footholds)
            {
                footholds.Add(foothold1);
            }
            List<Foothold> footholds1 = new List<Foothold>();
            foreach (Foothold foothold2 in footholds)
            {
                if (foothold2.getX1() > p.X || foothold2.getX2() < p.X)
                {
                    continue;
                }
                footholds1.Add(foothold2);
            }
            footholds1.Sort();
            List<Foothold>.Enumerator enumerator = footholds1.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Foothold current = enumerator.Current;
                    if (current.isWall() || current.getY1() == current.getY2())
                    {
                        if (current.isWall() || current.getY1() < p.Y)
                        {
                            continue;
                        }
                        foothold = current;
                        return foothold;
                    }
                    else
                    {
                        double num1 = (double)Math.Abs(current.getY2() - current.getY1());
                        double num2 = (double)Math.Abs(current.getX2() - current.getX1());
                        double num3 = (double)Math.Abs(p.X - current.getX1());
                        double num4 = Math.Atan(num2 / num1);
                        double num5 = Math.Atan(num1 / num2);
                        double num6 = Math.Cos(num4) * num3 / Math.Cos(num5);
                        num = (current.getY2() >= current.getY1() ? current.getY1() + (int)num6 : current.getY1() - (int)num6);
                        if (num < p.Y)
                        {
                            continue;
                        }
                        foothold = current;
                        return foothold;
                    }
                }
                return null;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
        }

        public Point getCenter()
        {
            Point x = new Point();
            x.X = (this.p1.X + this.p2.X) / 2;
            x.Y = (this.p1.Y + this.p2.Y) / 2;
            return x;
        }

        public Point getFraction(int multiply, int divide)
        {
            Point x = new Point();
            x.X = ((this.p1.X + this.p2.X) * multiply / divide);
            x.Y = (this.p1.Y + this.p2.Y) / 2;
            return x;
        }


        public void Insert(Foothold f)
        {
            this.footholds.Add(f);
        }

        public void Dispose()
        {
            footholds.Clear();
        }

    }
}