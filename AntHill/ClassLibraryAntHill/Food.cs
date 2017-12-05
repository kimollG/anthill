using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
    public class Food:IDrawable,IDispose,IObjectField
    {
        public ImageFlyweight ImageFlyWeight { get; set; }
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
            Image im = ImageFlyWeight.GetImage;
            g.DrawImage(im, Center.X - im.Width/1.5f, Center.Y - im.Height/1.5f);
           // g.FillEllipse(Brushes.Red, Convert.ToSingle(this.Center.X -1), Convert.ToSingle(this.Center.Y - 1), 2, 2);
        }
    }
}
