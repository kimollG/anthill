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
        private string name;
        public int Hp { get; }
        public List<Command> commands;
        public virtual void Thinking()
        {
           
        }
        private void Move(double dx,double dy)
        {
            x = x + dx;
            y = y + dy;
        }
        public Ant(int x ,int y,string name)
        {
            this.x = x;
            this.y = y;
            Hp = 100;
            commands = new List<Command>();
            this.name = name;
        }
        public virtual void  BeAttak()
        {
           Move(0,0);
        }
    }
}
