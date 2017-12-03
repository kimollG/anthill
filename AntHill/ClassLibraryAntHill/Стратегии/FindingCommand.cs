using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class FindingCommand:ICommand
    {
        Random rnd = new Random();
        public IPlace place { get; set; }
        private Ant ant;
        private List<IObjectField> findingObjects;
        public FindingCommand(Ant ant,IPlace p,List<IObjectField> f)
        {
            this.ant = ant;
            place = p;
            findingObjects = new List<IObjectField>();
            findingObjects = f;
            
            
        }
        public bool Execute()
        {
            if (place.isInside(ant.Center.X,ant.Center.Y))
            {
                double dAlpMax=0.5;                
                double a = Math.Atan((ant.LastY - ant.Center.Y) / (ant.LastX - ant.Center.X));
                a +=  rnd.NextDouble()* rnd.NextDouble() * rnd.NextDouble() *(rnd.Next(2)==0?-1:1)* dAlpMax;
                ant.Move(Math.Cos(a) * ant.Speed, Math.Sin(a) * ant.Speed);
            }
            else
            {
                return true;//Выход за границы
            }
            IObjectField obj = MinDictanceFood();
            if (obj != null)
            {
                ant.SetCommand(new MovingCommand(obj.Center.X, obj.Center.Y, this.ant, obj));
            }
            return false;
        }
        private IObjectField MinDictanceFood()
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
