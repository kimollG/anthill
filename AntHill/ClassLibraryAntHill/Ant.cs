using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public abstract class Ant : IDrawable, IDispose, IObjectField
    {
        public abstract void Draw(Graphics g);
        internal ICommand command;
        private PointF center;
        public PointF Center { get { return center; } }
        private double lx, ly;
        public double LastX { get { return lx; } }
        public double LastY { get { return ly; } }
        private string name;
        public int Hp { get; protected set; }
        public int Speed { get;  set; }
        public abstract void Thinking();
        protected DisposeMethod disp;
        public AntHill Home { get; private set; }
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
        public bool isInside(double x, double y)
        {
            if (AntMath.Dist(x,y,center.X,center.Y)< 5)
            {
                return false;
            }
            return true;
        }
        internal void SetCommand(ICommand s)
        {
            command = s;
        }
        public void Move(double dx, double dy)
        {

                lx = center.X;
                ly = center.Y;
                center.X = center.X + Convert.ToSingle(dx);
                center.Y = center.Y + Convert.ToSingle(dy);
        }
        private static Random rnd= new Random();
        public Ant(float x ,float y,string name)
        {
            center.X = x;
            center.Y = y;
            
            Hp = 100;
            this.name = name;
        }
        internal virtual void SetHome(AntHill home)//Типа одиночки
        {
            if(Home==null)
            {
                Home = home;
            }
            double a = Math.PI * rnd.NextDouble();
            Move(Math.Cos(a), Math.Sin(a));
        }       
        public void BeAttaсked(int damage)
        {
            double a = -Math.Atan2(this.Center.Y - this.LastY, Center.X - this.LastX);
            Move(Math.Cos(a)*Speed, Math.Sin(a) * Speed);
            a = -a;
            Move(Math.Cos(a) , Math.Sin(a));
            if ((Hp -= damage) <= 0)
                disp(this);
        }


    }
}
