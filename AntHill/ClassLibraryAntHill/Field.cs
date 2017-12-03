using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{

    public class Field:IPlace
    {
        public List<AntHill> AntHills { get; private set; }
        private List<Ant> Pests;
        private List<Food> Foods;
        public int Numberticks { get; private set; }
        public Field(List<AntHill> aaa)
        {
            AntHills = aaa;
            Numberticks = 0;
            Foods = new List<Food>();
            Pests = new List<Ant>();
            for (int i = 0; i < AntHills.Count;i++)
            {
                AntHills[i].SetField(this);
            }

        }
        public void  NewField(int numberOfAnts)
        {

        }
        public List<IObjectField> FindObjects(float x, float y)
        {
            List<IObjectField> objects = new List<IObjectField>();
            for (int j = 0; j < Foods.Count; j++)
            {
                double d = AntMath.Dist(x,y, Foods[j].Center.X, Foods[j].Center.Y);
                if (d < 50)
                {
                     objects.Add(Foods[j]);
                }
            }
            for (int j = 0; j < Pests.Count; j++)
            {
                double d = AntMath.Dist(x, y, Pests[j].Center.X, Pests[j].Center.Y);
                if (d < 50)
                {
                    objects.Add(Pests[j]);
                }
            }
            return objects;
        }
        public Field()
        {
            Foods = new List<Food>();
            Pests = new List<Ant>();
        }
        public void BornFood()
        {
            float x;
            float y;
            do
            {
                Random rnd = new Random();
                x = rnd.Next(5, 650);
                y = rnd.Next(5, 400);
            }
            while (AntHills[0].isInside(x + 100, y - 100) && AntHills[0].isInside(x - 100, y + 100) &&
                AntHills[0].isInside(x-100, y-100)&& AntHills[0].isInside(x+100, y+100));
            Foods.Add(new Food(new PointF(x, y)) { Dispose = (a) => { Foods.Remove((Food)a); AntHills[0].OpenFoods.Remove((Food)a); } });
        }
        public bool isInside(double x,double y)
        {
            if (x > 10 && x < 650 && y > 10 && y < 400)
            {
                return true;
            }
            return false;
        }
        public void Process()
        {
            Numberticks++;
            AntHills[0].Process();
            if (Numberticks % 20 == 19)
            {
                BornFood();
            }
                BornFood();

            BornFood();
        }
        public void Draw(Graphics g)
        {
            AntHills.ForEach(ah => ah.Draw(g));
            Foods.ForEach(f => f.Draw(g));
            Pests.ForEach(f => f.Draw(g));
        }
    } 
}
