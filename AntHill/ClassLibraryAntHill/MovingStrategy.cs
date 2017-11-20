using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class MovingStrategy : IStrategy
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public IPlace place { get; set; }
        private Ant ant;
        public MovingStrategy(double x,double y, Ant ant, IPlace p)
        {
            X = x;
            Y = y;
            place = p;
            this.ant = ant;
        }
        public void Execute()
        {
            double a = Math.Atan((Y- ant.Y) / (X - ant.X));
            if (ant.X >= X)
                ant.Move(-Math.Cos(a) * ant.Speed, -Math.Sin(a) * ant.Speed);
            else
            {
                ant.Move(Math.Cos(a) * ant.Speed, Math.Sin(a) * ant.Speed);
            }

            double d = Math.Sqrt((X - ant.X) * (X - ant.X) + (Y - ant.Y) * (Y - ant.Y));
            if (d < 7)
            {
                if (place is Food)
                {
                    if (((Food)place).Hp>0)
                    {
                        ant.SetStrategy(new MovingStrategy(ant.Home.center.X, ant.Home.center.Y, this.ant, ant.Home));
                        ((WorkerAnt)ant).IsBringing = true;
                        ((Food)place).ChangeFood();
                    }
                    else
                    {
                        ant.SetStrategy(new FindingStrategy(this.ant, new Field()));
                    }
                }
                else
                {
                    ant.SetStrategy(new FindingStrategy(this.ant, new Field()));
                    ((WorkerAnt)ant).IsBringing = false;
                }
            }
        }
    }
}
