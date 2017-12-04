using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
   /* public class Beetle : Enemy
    {
        public Beetle(int hp,PointF c):base(hp,c)
        {

        }
        public override void Draw(Graphics g)
        {
            double a = 180 / Math.PI * Math.Atan((this.LastY - this.Center.Y) / (this.LastX - this.Center.X));
            g.TranslateTransform(Convert.ToSingle(this.Center.X - 4), Convert.ToSingle(this.Center.Y - 4));
            g.RotateTransform(Convert.ToSingle(a));
            bool b = this.LastX > this.Center.X;
            g.RotateTransform(90);
            if (b)
                g.RotateTransform(180);

            //g.DrawImage(imSelf, new PointF(-imSelf.Width * 0.5f, -imSelf.Height * 0.5f));
            g.FillEllipse(Brushes.Firebrick, Center.X - 5, Center.Y - 3, 10, 6);
            if (b)
                g.RotateTransform(-180);
            g.RotateTransform(-90);
            g.RotateTransform(-Convert.ToSingle(a));
            g.TranslateTransform(-Convert.ToSingle(this.Center.X - 4), -Convert.ToSingle(this.Center.Y - 4));
        }
        public  override void Thinking()
        {

        }
    }*/
}
