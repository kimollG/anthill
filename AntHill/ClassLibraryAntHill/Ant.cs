using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public abstract class Ant:IDrawable,IDispose,IObjectField
    {
        public abstract void Draw(Graphics g);
        internal  ICommand command;
        public IPlace Place { get; private set; }
        private PointF center;
        public PointF Center { get { return center; } }
        private double lx, ly;
        public double LastX { get { return lx; } }
        public double LastY { get { return ly; } }
        private string name;
        public int Hp { get; protected set; }
        public int Speed { get; set; }
        public abstract void Thinking();
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
        public bool isInside(double x, double y)
        {
            if (Math.Abs(Center.X -x)<5 && Math.Abs(Center.Y - x) < 5)
            {
                return false;
            }
            return true;
        }
        internal void SetCommand(ICommand s)
        {
            command = s;
        }
        public AntHill Home { get; private set; }
        public void Move(double dx,double dy)
        {
            lx = center.X;
            ly = center.Y;
            center.X = center.X + Convert.ToSingle(dx);
            center.Y = center.Y + Convert.ToSingle(dy);
        }
        public void ChangePlace(IPlace p)
        {
            Place = p;
        }
        private static Random rnd= new Random();
        public Ant(float x ,float y,string name,AntHill home)
        {
            this.Home = home;
            center.X = x;
            center.Y = y;
            double a = Math.PI * rnd.NextDouble();
            Move(Math.Cos(a), Math.Sin(a));
            Hp = 100;
            //commands = new List<Command>();
            this.name = name;
            Place = home;
            home.CorrectLocation(this);
        }
        public abstract void BeAttaсked(int damage);
    }
}
