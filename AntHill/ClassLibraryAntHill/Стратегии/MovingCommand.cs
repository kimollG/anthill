using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
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
        /*private void ChangeAngle()
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
        }*/
        private double step = 0;
        //private double len = 0;
        private double lastangle;
        private double angle;
        private PointF lastpos;
        private void ChangeAngle()
        {
            if (step == 0)
            {
                AntHill pl;
                if (!(ant is Enemy) && !ant.AtHome && !(place is Food) && !(place is Enemy))
                {
                    pl = ant.Home;
                    if (AntMath.Dist(ant.Center, pl.Center) < pl.radius )
                    {
                        step = ant.Speed / (pl.radius);
                        lastangle = angle;
                        double a = Math.Atan2(ant.Center.Y - pl.Center.Y, ant.Center.X - pl.Center.X);
                        angle = -Math.PI / 2 + a;
                        lastpos = place.Center;
                        if (Math.Cos(lastangle - angle) < 0)
                        {
                            angle += Math.PI;
                            step = -step;
                        }
                        //double difangle = Math.Abs(lastangle - a);
                        //len = 2 * (pl.radius + 20) * Math.Abs(Math.Cos(difangle));
                    }
                }
                /*else
                if (ant is Enemy)
                {
                    pl = ((Enemy)ant).field.AntHills[0];
                    if (AntMath.Dist(ant.Center, pl.Center) < pl.radius + 20)
                    {
                        ant.SetCommand(new MovingCommand(ant,)
                    }
                }*/
            }
        }
        private void TurnAngle()
        {
            ChangeAngle();
            angle -= step;
            if (step != 0 && AntMath.Dist(ant.Center, lastpos) < 50)
            {
                step = 0;
               // len = 0;
                //angle = lastangle;
            }
        }
        public bool Execute()
        {
            angle = Math.Atan2(ant.Center.Y - ant.LastY, ant.Center.X - ant.LastX);
            TurnAngle();
            X = place.Center.X;
            Y = place.Center.Y;
            double d = AntMath.Dist(X, Y, ant.Center.X, ant.Center.Y);
            if (d < 3)
            {
                return true;//Достигли цели
            }
            else
            {
                double a;
                if (step == 0)
                    a = Math.Atan((Y - ant.Center.Y) / (X - ant.Center.X));
                else
                    a = angle;
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
