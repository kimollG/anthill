using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{

    public class Field
    {
        public class AntBuilder : Ant
        {
            private List<Food> OpenFoods;
            public bool Bring { get; private set; }
            public AntBuilder(double x, double y, string name) : base(x, y, name)
            {
                OpenFoods = new List<Food>();
                Bring = false;
            }
            public void GiveOpenFoods(List<Food> food)
            {
                OpenFoods.RemoveAll(x => true);
                OpenFoods = food;
            }
            public override int Thinking()
            {
                base.Thinking();
                int index = MinDictance();
                if (commands[0].action == Action.findfood)
                {
                    if (index != -1)
                    {
                        commands.RemoveAll(x => true);
                        commands.Add(new Command(Action.gotoFood, OpenFoods[index].X, OpenFoods[index].Y));
                    }
                    else
                    {
                        if (X > 10 && X < 1300 && Y > 10 && Y < 700)
                        {
                            double a = Math.Atan2((LastY - Y), (LastX - X));
                            Move(Math.Cos(a), Math.Sin(a));
                            return -2;
                        }
                        else
                        {
                            commands.RemoveAll(x => true);
                            commands.Add(new Command(Action.gotoAntHill, 100, 100));
                        }
                        
                    }
                }

                if (commands[0].action == Action.gotoFood)
                {
                    double d = Math.Sqrt((X - OpenFoods[index].X) * (X - OpenFoods[index].X) + (Y - OpenFoods[index].Y) * (Y - OpenFoods[index].Y));
                    if (d < 10)
                    {
                        commands.RemoveAll(x => true);
                        commands.Add(new Command(Action.gotoAntHill, 100, 100));
                        Bring = true;
                        return index;
                    }
                    else
                    {
                        double a = Math.Atan2((OpenFoods[index].Y - Y),(OpenFoods[index].X - X));
                        Move(Math.Cos(a), Math.Sin(a));
                    }
                }
                if (commands[0].action == Action.gotoAntHill)
                {
                    double d = Math.Sqrt((X - 100) * (X - 100) + (Y - 100) * (Y - 100));
                    if (d < 5)
                    {
                        Bring = false;
                        commands.RemoveAll(x => true);
                        commands.Add(new Command(Action.findfood));
                    }
                    else
                    {
                        double a = Math.Atan2((100 - Y), (100 - X));
                        Move(Math.Cos(a), Math.Sin(a));
                    }
                }
                return -1;
            }
            private int MinDictance()
            {
                double dist = 400;
                int index = -1;
                for(int i=0;i<OpenFoods.Count;i++)
                {
                    double d = Math.Sqrt((X - OpenFoods[i].X) * (X - OpenFoods[i].X) + (Y - OpenFoods[i].Y) * (Y - OpenFoods[i].Y));
                    if(dist>d)
                    {
                        dist = d;
                        index = i;
                    }
                }
                return index;
            }
        }
        public List<Ant> Ants { get; private set; }
        public List<Ant> Pests { get; private set; }
        public List<Food> Foods { get; private set; }
        public List<int> OpenFoods { get; private set; }
        public Field()
        {
            OpenFoods = new List<int>();
            Foods = new List<Food>();
            Pests = new List<Ant>();
            Ants = new List<Ant>();
        }
        public void BornAnt(double x,double y,string name)
        {
            Ants.Add(new AntBuilder(x, y,name));
        }
        private void Alive()
        {
            Ants.RemoveAll(x => x.Hp <= 0);
            Pests.RemoveAll(x => x.Hp <= 0);
            Foods.RemoveAll(x => x.Hp <= 0);
        }
        public void Process()
        {
            for(int i=0;i<Ants.Count;i++)
            {
                if(Ants[i].commands.Count==0)
                {
                    Ants[i].commands.Add(new Command(Action.findfood));
                }
                List<Food> food = new List<Food>();
                for(int j=0;j<OpenFoods.Count;j++)
                {
                    food.Add(Foods[OpenFoods[j]]);
                }
                ((AntBuilder)Ants[i]).GiveOpenFoods(food);
                int h= Ants[i].Thinking();
                if(h>=0)
                {
                    Foods[OpenFoods[h]].ChangeFood();
                }
                else
                {
                    if(h==-2)
                    {
                        for(int j=0;j<Foods.Count;j++)
                        {
                            double d = Math.Sqrt((Ants[i].X - Foods[i].X) * (Ants[i].X - Foods[i].X) + (Ants[i].Y - Foods[i].Y) * (Ants[i].Y - Foods[i].Y));
                            if(d<50)
                            {
                                if(!OpenFoods.Exists(x=>x==j))
                                {
                                    OpenFoods.Add(j);
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
