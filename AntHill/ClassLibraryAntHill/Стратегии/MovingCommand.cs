using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class MovingCommand : ICommand
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public IPlace place { get; set; }
        private Ant ant;
        public MovingCommand(double x,double y, Ant ant, IPlace p)
        {
            X = x;
            Y = y;
            place = p;
            this.ant = ant;
        }
        public bool Execute()
        {
            double d = Math.Sqrt((X - ant.X) * (X - ant.X) + (Y - ant.Y) * (Y - ant.Y));
            if (d < 3)
            {
                return true;//Достигли цели
            }
            else
            {
                double a = Math.Atan((Y - ant.Y) / (X - ant.X));
                if (ant.X >= X)
                    ant.Move(-Math.Cos(a) * ant.Speed, -Math.Sin(a) * ant.Speed);
                else
                {
                    ant.Move(Math.Cos(a) * ant.Speed, Math.Sin(a) * ant.Speed);
                }
            }
            return false;

        }
    }
}
