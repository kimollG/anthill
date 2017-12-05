using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    class FindingCommand:ICommand
    {
        Random rnd = new Random();
        public IObjectField place { get; set; }
        private Ant ant;
        private List<IObjectField> findingObjects;
        public double angle { get; private set; }        
        public FindingCommand(Ant ant, IObjectField p,List<IObjectField> f,float direction)
        {
            angle=direction+(rnd.NextDouble()*Math.PI-Math.PI/2);
            this.ant = ant;
            place = p;
            findingObjects = f;
        }
        public void SetFindObjects(List<IObjectField> f)//очень плохо
        {
            findingObjects = f;
        }
        private bool CheckOrientation( )
        {

            if(ant.Center.X>630)
            {
                if(Math.Cos(angle)>0)
                {
                    return false;
                }
            }
            if (ant.Center.X < 20)
            {
                if (Math.Cos(angle)<0)
                {
                    return false;
                }
            }
            if (ant.Center.Y > 380)
            {
                if (Math.Sin(angle) > 0 )
                {
                    return false;
                }
            }
            if (ant.Center.Y < 20)
            {
                if (Math.Sin(angle) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        private double step = 0;
        private double len = 0;
        private PointF lastpos;
        private double lastangle;
        private void ChangeAngle()
        {
            if (step == 0)
            {
                AntHill pl;
                if (ant is Enemy)
                {
                    pl = ((Enemy)ant).field.AntHills[0];
                    if (AntMath.Dist(ant.Center, pl.Center) < pl.radius + 20)
                    {
                        step = ant.Speed / (pl.radius + 20);
                        lastangle = angle;
                        lastpos = ant.Center;
                        double a = Math.Atan2(ant.Center.Y - pl.Center.Y, ant.Center.X - pl.Center.X);
                        angle = -Math.PI / 2 + a;
                        if (Math.Cos(lastangle - angle) < 0)
                        {
                            angle += Math.PI;
                            step = -step;
                        }
                        double difangle = Math.Abs(lastangle - a);
                        len = 2 * (pl.radius + 20) * Math.Abs(Math.Cos(difangle));
                    }
                }
            }
        }
        private void TurnAngle()
        {
            ChangeAngle();
            angle -= step;
            if(step != 0 && AntMath.Dist(ant.Center,lastpos)>len)
            {
                step = 0;
                len = 0;
                lastpos = new PointF();
                angle=lastangle;
            }
        }
        public bool Execute()
        {
            TurnAngle();      
            if (place.isInside(ant.Center.X, ant.Center.Y)||CheckOrientation())
            {
                double dAlpMax = 0.0001;
                angle += rnd.NextDouble() * rnd.NextDouble() * rnd.NextDouble() * (rnd.Next(2) == 0 ? -1 : 1) * dAlpMax;
                ant.Move(Math.Cos(angle) * ant.Speed, Math.Sin(angle) * ant.Speed);
            }
            else
            {
                return true;//Выход за границы
            }
            IObjectField obj = MinDictance();
            if (obj != null)
            {
                ant.SetCommand(new MovingCommand(this.ant, obj));
            }
            return false;
        }
        private IObjectField MinDictance()
        {
            double dist = 300;
            IObjectField obj = null;
            for (int i = 0; i < findingObjects.Count; i++)
            {
                double d = AntMath.Dist(findingObjects[i].Center, ant.Center);                  
                if (dist > d)
                {
                    dist = d;
                    obj = findingObjects[i];
                }
            }
            return obj;
        }
    }
}
