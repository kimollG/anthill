using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public abstract class Ant:IDrawable,IDispose
    {
        public abstract void Draw(Graphics g);
        internal  IStrategy strategy;
        private double x;
        private double y;
        public double X { get { return x; } }
        public double Y { get { return y; } }
        private double lx, ly;
        public double LastX { get { return lx; } }
        public double LastY { get { return ly; } }
        private string name;
        public int Hp { get; protected set; }
        public List<Command> commands;
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
        internal void SetStrategy(IStrategy s)
        {
            strategy = s;
        }
        public AntHill Home { get; private set; }
        public void Move(double dx,double dy)
        {
            lx = x;
            ly = y;
            x = x + dx;
            y = y + dy;
        }
        private static Random rnd= new Random();
        public Ant(double x ,double y,string name,AntHill home)
        {
            this.Home = home;
            this.x = x;
            this.y = y;
            double a = Math.PI * rnd.NextDouble();
            Move(Math.Cos(a), Math.Sin(a));
            Hp = 100;
            commands = new List<Command>();
            this.name = name;
        }
        public abstract void BeAttaсked(int damage);
    }
}
