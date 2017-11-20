using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class FindingStrategy:IStrategy
    {
        public IPlace place { get; set; }
        private Ant ant;
        public FindingStrategy(Ant ant,IPlace p)
        {
            this.ant = ant;
            place = p;
        }
        public void Execute()
        {
            if (place.isInside(ant.X,ant.Y))
            {
                double a = Math.Atan((ant.LastY - ant.Y) / (ant.LastX - ant.X));
                ant.Move(Math.Cos(a) * ant.Speed, Math.Sin(a) * ant.Speed);
            }
            else
            {
                ant.SetStrategy(new MovingStrategy(ant.Home.center.X, ant.Home.center.Y, this.ant,place));
            }
            int index = MinDictance();
            if(index!=-1)
            {
                ant.SetStrategy(new MovingStrategy(ant.Home.OpenFoods[index].X, ant.Home.OpenFoods[index].Y, this.ant, ant.Home.OpenFoods[index]));
            }
        }
        private int MinDictance()
        {
            double dist = 400;
            int index = -1;
            for (int i = 0; i < ant.Home.OpenFoods.Count; i++)
            {
                double d = Math.Sqrt((ant.X - ant.Home.OpenFoods[i].X) * (ant.X - ant.Home.OpenFoods[i].X) + (ant.Y - ant.Home.OpenFoods[i].Y) * (ant.Y - ant.Home.OpenFoods[i].Y));
                if (dist > d)
                {
                    dist = d;
                    index = i;
                }
            }
            return index;
        }
    }
}
