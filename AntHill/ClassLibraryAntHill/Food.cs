using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
    public class Food:IDrawable,IDispose,IObjectField
    {
        private int hp;
        public PointF Center { get; }
        public int Hp { get { return hp; } }
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

        public Food(PointF center)
        {
            hp = 100;
            this.Center = center;
        }
        public void ChangeFood()
        {
            if ((hp -= 10) <= 0)
                disp(this);
        }
        public bool isInside(double x, double y)
        {
            if (x < Center.X + 7 && x > Center.X - 7 && y < Center.Y + 7 && y > Center.Y - 7)
            {
                return true;
            }
            return false;
        }
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Green, Convert.ToSingle(this.Center.X -7.5), Convert.ToSingle(this.Center.Y - 5), 10, 10);
        }
    }
}
