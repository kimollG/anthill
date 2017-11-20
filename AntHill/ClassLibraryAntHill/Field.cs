using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{

    public class Field:IPlace
    {
        public List<AntHill> AntHills { get; private set; }
        public List<Ant> Ants { get; private set; }
        public List<Ant> Pests { get; private set; }
        public List<Food> Foods { get; private set; }
        public Field(List<AntHill> aaa)
        {
            AntHills = aaa;
            Foods = new List<Food>();
            Pests = new List<Ant>();
            Ants = new List<Ant>();
        }
        public Field()
        {
            Foods = new List<Food>();
            Pests = new List<Ant>();
            Ants = new List<Ant>();
        }
        public void BornAnt(double x, double y, string name)
        {
            Ants.Add(new WorkerAnt(x, y, name, AntHills[0]) { Dispose = (a) => Ants.Remove((Ant)a) });
        }
        public void BornFood(double x, double y)
        {
            Foods.Add(new Food(x, y) { Dispose = (a) => { Foods.Remove((Food)a); AntHills[0].OpenFoods.Remove((Food)a); } });
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
            for (int i = 0; i < Ants.Count; i++)
            {
                /*if (Ants[i].commands.Count == 0)
                {
                    Ants[i].commands.Add(new Command(Action.findfood));
                }*/

                ((WorkerAnt)Ants[i]).GiveOpenFoods(AntHills[0].OpenFoods);
                Ants[i].Thinking();
                if (Ants[i] is WorkerAnt)
                    for (int j = 0; j < Foods.Count; j++)
                    {
                        double d = Math.Sqrt((Ants[i].X - Foods[j].X) * (Ants[i].X - Foods[j].X) + (Ants[i].Y - Foods[j].Y) * (Ants[i].Y - Foods[j].Y));
                        if (d < 50)
                        {
                            if (!AntHills[0].OpenFoods.Exists(x => x == Foods[j]))
                            {
                                AntHills[0].OpenFoods.Add(Foods[j]);
                            }
                        }
                    }
            }

        }
    }
}
