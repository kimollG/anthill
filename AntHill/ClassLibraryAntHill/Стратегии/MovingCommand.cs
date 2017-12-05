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
        public IObjectField place { get; set; }
        private Ant ant;
        public MovingCommand( Ant ant, IObjectField p)
        {        
            place = p;
            this.ant = ant;
        }
        private void ChangeAngle()
        {
            if (ant is WarriorAnt || ant is WorkerAnt)
            {
                IObjectField pl = ant.Home.FindingNode(ant);
                if (pl!=null && !pl.isInside(ant.Center.X, ant.Center.Y))
                {
                    double a = Math.Atan((Y - ant.Center.Y) / (X - ant.Center.X));
                    ant.SetCommand(new FindingCommand(ant, place, ((Enemy)ant).findingobjects, (float)a));
                }
            }
        }
        public bool Execute()
        {
            ChangeAngle();
            X = place.Center.X;
            Y = place.Center.Y;
            double d = AntMath.Dist(X, Y, ant.Center.X, ant.Center.Y);
            if (d < 3)
            {
                return true;//Достигли цели
            }
            else
            {
                double a = Math.Atan((Y - ant.Center.Y) / (X - ant.Center.X));
                if (ant.Center.X >= X)
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
