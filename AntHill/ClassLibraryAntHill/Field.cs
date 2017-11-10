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
            private const int speed = 3;
            private List<Food> OpenFoods;
            public bool Bring { get; private set; }
            public AntBuilder(double x, double y, string name) : base(x, y, name)
            {
                OpenFoods = new List<Food>();
                Bring = false;
            }
            public override void BeAttak()
            {
            }
            public void GiveOpenFoods(List<Food> food)
            {
                OpenFoods = food;
            }
            public override int Thinking()
            {
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
                        if (X > 10 && X < 650 && Y > 10 && Y < 400)
                        {
                            double a =Math.Atan((LastY - Y)/(LastX - X));
                            Move(Math.Cos(a)*speed, Math.Sin(a)*speed);
                            return -2;
                        }
                        else
                        {
                            commands.Clear();
                            commands.Add(new Command(Action.gotoAntHill, 100, 100));
                            double a = Math.Atan((100 - Y) / (100 - X));
                            Move(Math.Cos(a), Math.Sin(a));
                        }
                        
                    }
                }

                if (commands[0].action == Action.gotoFood)
                {
                    if (index == -1)
                    {
                        commands.Clear();
                        commands.Add(new Command(Action.findfood));
                    }
                    else
                    {
                        double d = Math.Sqrt((X - OpenFoods[index].X) * (X - OpenFoods[index].X) + (Y - OpenFoods[index].Y) * (Y - OpenFoods[index].Y));
                        if (d < 10)
                        {
                            commands.Clear();
                            commands.Add(new Command(Action.gotoAntHill, 100, 100));
                            double a = Math.Atan((OpenFoods[index].Y - Y) / (OpenFoods[index].X - X));
                            Move(Math.Cos(a), Math.Sin(a));
                            Bring = true;
                            OpenFoods[index].ChangeFood();
                        }
                        else
                        {
                            double a = Math.Atan((OpenFoods[index].Y - Y) / (OpenFoods[index].X - X));
                            double al = AntMath.GetAnkleBetwentwoVector(-OpenFoods[index].X + X, -OpenFoods[index].Y + Y, X - LastX, Y - LastY);
                            if (Math.Abs(al) < Math.PI / 2)
                            {
                                Move(Math.Cos(a), Math.Sin(a));
                                return -1;
                            }

                                Move( Math.Cos(a) * speed, Math.Sin(a) * speed);
    
                        }
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
                        double a = Math.Atan((100 - Y)/(100 - X));
                        double al = AntMath.GetAnkleBetwentwoVector( X - 100 ,  Y - 100 , X - LastX, Y - LastY);
                        if (Math.Abs(al) < Math.PI / 2)
                        {
                            Move(-Math.Cos(a), -Math.Sin(a));
                            return -2;
                        }
 
                            Move(- Math.Cos(a) * speed,-Math.Sin(a) * speed);

                        return -2;
                    }
                }
                if (!(X > 10 && X < 650 && Y > 10 && Y < 400))
                {
                    bool ok = true;
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
        public List<Food> OpenFoods { get; private set; }
        public Field()
        {
            OpenFoods = new List<Food>();
            Foods = new List<Food>();
            Pests = new List<Ant>();
            Ants = new List<Ant>();
        }
        public void BornAnt(double x,double y,string name)
        {
            Ants.Add(new AntBuilder(x, y,name));
        }
        public void BornFood(double x, double y)
        {
            Foods.Add(new Food(x, y));
        }
        private void Alive()
        {
            Ants.RemoveAll(x => x.Hp <= 0);
            Pests.RemoveAll(x => x.Hp <= 0);
            Foods.RemoveAll(x => x.Hp <= 0);
            OpenFoods.RemoveAll(x => x.Hp <= 0);
        }
        public void Process()
        {
            for (int i = 0; i < Ants.Count; i++)
            {
                if (Ants[i].commands.Count == 0)
                {
                    Ants[i].commands.Add(new Command(Action.findfood));
                }

                ((AntBuilder)Ants[i]).GiveOpenFoods(OpenFoods);
                int h = Ants[i].Thinking();
                if (h == -2)
                {
                    for (int j = 0; j < Foods.Count; j++)
                    {
                        double d = Math.Sqrt((Ants[i].X - Foods[j].X) * (Ants[i].X - Foods[j].X) + (Ants[i].Y - Foods[j].Y) * (Ants[i].Y - Foods[j].Y));
                        if (d < 50)
                        {
                            if (!OpenFoods.Exists(x => x == Foods[j]))
                            {
                                OpenFoods.Add(Foods[j]);
                            }
                        }
                    }
                }

            }
            Alive();
        }
    }
}
