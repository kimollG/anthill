using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{

    public class Field
    {        
        public List<Ant> Ants { get; private set; }
        public List<Ant> Pests { get; private set; }
        public List<Food> Foods { get; private set; }
        public List<Food> OpenFoods { get; private set; }
        private AntHill Anthill;
        public Field(AntHill Anthill)
        {
            this.Anthill = Anthill;
            OpenFoods = new List<Food>();
            Foods = new List<Food>();
            Pests = new List<Ant>();
            Ants = new List<Ant>();
        }
        public void BornAnt(double x,double y,string name)
        {
            Ants.Add(new WorkerAnt(x, y,name,null));
        }
        public void BornFood(double x, double y)
        {
            Foods.Add(new Food(x, y));
        }
        private void RemoveDead()
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

                ((WorkerAnt)Ants[i]).GiveOpenFoods(OpenFoods);
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
            RemoveDead();
        }
    }
}
