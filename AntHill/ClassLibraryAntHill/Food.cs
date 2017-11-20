using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
    public class Food:IDrawable,IDispose,IPlace
    {
        private int hp;
        public int Hp { get { return hp; } }
        public double X { get; }
        public double Y { get; }
        protected DisposeMethod disp;
        public DisposeMethod Dispose
        {
            get
            {
                return disp;
            }

            set
            {
                disp = value;
            }
        }

        public Food(double x,double y)
        {
            hp = 100;
            X = x;
            Y = y;
        }
        public void ChangeFood()
        {
            if ((hp -= 10) <= 0)
                disp(this);
        }
        public bool isInside(double x, double y)
        {
            if (x < X + 7 && x > X - 7 && y < Y + 7 && y > Y - 7)
            {
                return true;
            }
            return false;
        }
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Green, Convert.ToSingle(this.X -7.5), Convert.ToSingle(this.Y - 5), 10, 10);
        }
    }
}
