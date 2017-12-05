using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{

    public class Field : IObjectField
    {
       
        ImageFlyweight enemyImage = new ImageFlyweight(Image.FromFile("../../../Photos/enemy.png"));
        ImageFlyweight fullFoodImage = new ImageFlyweight(Image.FromFile("../../../Photos/FullFood.png"));
        
        static Random rnd = new Random();
        public List<AntHill> AntHills { get; private set; }
        private List<Ant> Pests;
        private List<Food> Foods;
        public int Numberticks { get; private set; }
        private static int TotalLength;
        private static int TotalWide;
        public PointF Center { get; private set; }
        public Field(List<AntHill> aaa)
        {
            AntHills = aaa;
            Numberticks = 0;
            Foods = new List<Food>();
            Pests = new List<Ant>();
            TotalLength = 650;
            TotalWide = 400;
            Center = new PointF(TotalLength / 2, TotalWide / 2);
            for (int i = 0; i < AntHills.Count; i++)
            {
                AntHills[i].SetField(this);
            }

        }
        public List<IObjectField> FindAnts(float x, float y)
        {
            return AntHills[0].GiveAnts(x, y);
        }
        public List<IObjectField> FindObjects(float x, float y)
        {
            List<IObjectField> objects = new List<IObjectField>();
            for (int j = 0; j < Foods.Count; j++)
            {
                double d = AntMath.Dist(x, y, Foods[j].Center.X, Foods[j].Center.Y);
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
                x = rnd.Next(5, TotalLength);
                y = rnd.Next(5, TotalWide);
            }
            while (AntMath.Dist(AntHills[0].Center, new PointF(x, y)) < AntHills[0].radius + 50);
            Foods.Add(new Food(new PointF(x, y)) { Dispose = (a) => { Foods.Remove((Food)a); AntHills[0].OpenFoods.Remove((Food)a); },ImageFlyWeight=fullFoodImage });
        }
        public bool isInside(double x, double y)
        {
            if (x > 10 && x < TotalLength && y > 10 && y < TotalWide)
            {
                return true;
            }
            return false;
        }
        public void Process()
        {
            Numberticks++;
            AntHills[0].Process();
            for(int i=0;i<Pests.Count;i++)
            {
                Pests[i].Thinking();
            }
            if (Numberticks % 20 == 19)
            {
                BornFood();
            }
            if (Numberticks % 40 == 39 &&Pests.Count<5)
            {
                BornPest();
            }
        }
        public void BornPest()
        {

            float x;
            float y;
            x = rnd.Next(0, TotalLength);
            y = rnd.Next(0, TotalWide);
           int z = rnd.Next(0, 2);
            if(z==0)
            {
                x = Math.Abs(rnd.Next(0, 2) * TotalLength - 20);
            }
            else
            {
                y = Math.Abs(rnd.Next(0, 2) * TotalWide - 20);
            }
            Enemy pest;
            pest = new Enemy(100, new PointF(x, y) ) { Dispose = (a) => { Pests.Remove((Ant)a); },ImageFlyWeight=enemyImage };
            pest.SetHome(null);//Установить дом
            pest.setField(this);//Установить поле
            Pests.Add(pest);
        }
        public void Draw(Graphics g)
        {
            AntHills.ForEach(ah => ah.Draw(g));
            Foods.ForEach(f => f.Draw(g));
            Pests.ForEach(f => f.Draw(g));
        }
    }
}
