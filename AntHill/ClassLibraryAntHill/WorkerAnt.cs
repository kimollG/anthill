using System;
using System.Collections.Generic;
using System.Drawing;
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
            if ((Hp -= damage) <= 0)
                disp(this);
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
                        double al = AntMath.GetAnkleBetwentwoVector(-OpenFoods[index].X + X, -OpenFoods[index].Y + Y, X - LastX, Y - LastY);
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
                    double al = AntMath.GetAnkleBetwentwoVector(X - 100, Y - 100, X - LastX, Y - LastY);
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
        public override void Draw(Graphics g)
        {
            double a = 180 / Math.PI * Math.Atan((this.LastY - this.Y) / (this.LastX - this.X));
            g.TranslateTransform(Convert.ToSingle(this.X - 4), Convert.ToSingle(this.Y - 4));
            g.RotateTransform(Convert.ToSingle(a));
            if (((WorkerAnt)this).IsBringing == false)
            {
                g.FillEllipse(Brushes.Brown, 0, 0, 10, 6);
            }
            else
            {
                g.FillEllipse(Brushes.Green, 0, 0, 10, 6);
            }
            float x = 8;
            if (this.LastX > this.X)
                x = -2;
            float y = 1.115f;
            g.FillEllipse(Brushes.Black, x, y, 4, 4);
            g.RotateTransform(-Convert.ToSingle(a));
            g.TranslateTransform(-Convert.ToSingle(this.X - 4), -Convert.ToSingle(this.Y - 4));
        }
    }
}
