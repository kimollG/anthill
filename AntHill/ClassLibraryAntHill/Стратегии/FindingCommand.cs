using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class FindingCommand:ICommand
    {
        public IPlace place { get; set; }
        private Ant ant;
        public FindingCommand(Ant ant,IPlace p)
        {
            this.ant = ant;
            place = p;
        }
        public bool Execute()
        {
            if (place.isInside(ant.X,ant.Y))
            {
                double a = Math.Atan((ant.LastY - ant.Y) / (ant.LastX - ant.X));
                ant.Move(Math.Cos(a) * ant.Speed, Math.Sin(a) * ant.Speed);
            }
            else
            {
                return true;//Выход за границы
            }
            Food food = MinDictanceFood();
            if(food!=null)
            {
                ant.SetCommand(new MovingCommand(food.X, food.Y, this.ant, food));
            }
            return false;
        }
        private Food MinDictanceFood()//Здесь есть проблема надо создать общий тип вместо Iplace!!!!
        {
            double dist = 300;
            Food food = null;
            for (int i = 0; i < ant.Home.OpenFoods.Count; i++)
            {
                double d = Math.Sqrt((ant.X - ant.Home.OpenFoods[i].X) * (ant.X - ant.Home.OpenFoods[i].X) + (ant.Y - ant.Home.OpenFoods[i].Y) * (ant.Y - ant.Home.OpenFoods[i].Y));
                if (dist > d)
                {
                    dist = d;
                    food = ant.Home.OpenFoods[i];
                }
            }
            return food;
        }
    }
}
