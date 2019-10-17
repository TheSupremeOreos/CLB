using System;
using System.Drawing;

namespace PixelCLB
{
    public class Foothold : IComparable
    {
        private Point p1;

        private Point p2;

        private int id;

        private int next;

        private int prev;

        public Foothold(Point p1, Point p2, int id)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.id = id;
        }

        public int CompareTo(object othera)
        {
            Foothold foothold = (Foothold)othera;
            if (this.p2.Y >= foothold.getY1())
            {
                if (this.p1.Y <= foothold.getY2())
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int getId()
        {
            return this.id;
        }

        public int getNext()
        {
            return this.next;
        }

        public int getPrev()
        {
            return this.prev;
        }

        public int getX1()
        {
            return this.p1.X;
        }

        public int getX2()
        {
            return this.p2.X;
        }

        public int getY1()
        {
            return this.p1.Y;
        }

        public int getY2()
        {
            return this.p2.Y;
        }

        public bool isWall()
        {
            return this.p1.X == this.p2.X;
        }

        public void setNext(int next)
        {
            this.next = next;
        }

        public void setPrev(int prev)
        {
            this.prev = prev;
        }
    }
}