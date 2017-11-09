using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class Ant
    {
        private double x;
        private double y;
        public double X { get { return x; } }
        public double Y { get { return y; } }
        private double lx, ly;
        public double LastX { get { return lx; } }
        public double LastY { get { return ly; } }
        private string name;
        public int Hp { get; }
        public List<Command> commands;
        public virtual int Thinking()
        {
            return -1;
        }
        public void Move(double dx,double dy)
        {
            lx = x;
            ly = y;
            x = x + dx;
            y = y + dy;
        }
        public Ant(double x ,double y,string name)
        {
            this.x = x;
            this.y = y;
            Random rnd = new Random();
            double a = Math.PI * rnd.NextDouble();
            Move(Math.Cos(a), Math.Sin(a));
            Hp = 100;
            commands = new List<Command>();
            this.name = name;
        }
        public virtual void  BeAttak()
        {

        }
    }
}
