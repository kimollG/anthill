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
        double angle;
        public FindingCommand(Ant ant,IPlace p,List<IObjectField> f,float direction)
        {
            angle=direction;
            this.ant = ant;
            place = p;
            findingObjects = new List<IObjectField>();
            findingObjects = f;
            
            
        }
        public bool Execute()
        {
            if (place.isInside(ant.Center.X,ant.Center.Y))
            {
                double dAlpMax=0.1;                
                
                angle +=  rnd.NextDouble()* rnd.NextDouble() * rnd.NextDouble() *(rnd.Next(2)==0?-1:1)* dAlpMax;
                ant.Move(Math.Cos(angle) * ant.Speed, Math.Sin(angle) * ant.Speed);
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
