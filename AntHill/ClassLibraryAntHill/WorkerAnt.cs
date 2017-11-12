using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class WorkerAnt : Ant
    {
        private const int speed = 3;
        private List<Food> OpenFoods;
       
        public bool IsBringing { get; private set; }
        public WorkerAnt(double x, double y, string name,AntHill home) : base(x, y, name,home)
        {
            
            OpenFoods = new List<Food>();
            IsBringing = false;
        }
       
        public override void BeAttaсked(int damage)
        {
            Hp -= damage;
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
                        double a = Math.Atan((LastY - Y) / (LastX - X));
                        Move(Math.Cos(a) * speed, Math.Sin(a) * speed);
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
                        IsBringing = true;
                        OpenFoods[index].ChangeFood();
                    }
                    else
                    {
                        double a = Math.Atan((OpenFoods[index].Y - Y) / (OpenFoods[index].X - X));
                        double al = AntMath.GetAngleBetwenTwoVectors(-OpenFoods[index].X + X, -OpenFoods[index].Y + Y, X - LastX, Y - LastY);
                        if (Math.Abs(al) < Math.PI / 2)
                        {
                            Move(Math.Cos(a), Math.Sin(a));
                            return -1;
                        }

                        Move(Math.Cos(a) * speed, Math.Sin(a) * speed);

                    }
                }
            }
            if (commands[0].action == Action.gotoAntHill)
            {
                double d = Math.Sqrt((X - 100) * (X - 100) + (Y - 100) * (Y - 100));
                if (d < 5)
                {
                    IsBringing = false;
                    commands.RemoveAll(x => true);
                    commands.Add(new Command(Action.findfood));
                }
                else
                {
                    double a = Math.Atan((100 - Y) / (100 - X));
                    double al = AntMath.GetAngleBetwenTwoVectors(X - 100, Y - 100, X - LastX, Y - LastY);
                    if (Math.Abs(al) < Math.PI / 2)
                    {
                        Move(-Math.Cos(a), -Math.Sin(a));
                        return -2;
                    }

                    Move(-Math.Cos(a) * speed, -Math.Sin(a) * speed);

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
            for (int i = 0; i < OpenFoods.Count; i++)
            {
                double d = Math.Sqrt((X - OpenFoods[i].X) * (X - OpenFoods[i].X) + (Y - OpenFoods[i].Y) * (Y - OpenFoods[i].Y));
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
