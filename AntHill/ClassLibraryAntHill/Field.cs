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
            private List<int> OpenFoods;
            public AntBuilder(int x, int y, string name) : base(x, y, name)
            {
                OpenFoods = new List<int>();
            }
            public void GetOpenFoods(List<int> food)
            {
                OpenFoods.RemoveAll(x => true);
                OpenFoods = food;
            }
            public override void Thinking()
            {
                base.Thinking();

            }
        }
        private List<Ant> Ants;
        private List<Ant> Pests;
        private List<Food> Foods;
        private List<int> OpenFoods;
        public Field()
        {
            OpenFoods = new List<int>();
        }
        public void BornAnt(int x,int y,string name)
        {
            Ants.Add(new AntBuilder(x, y,name));
        }
        private void Alive()
        {
            Ants.RemoveAll(x => x.Hp <= 0);
            Pests.RemoveAll(x => x.Hp <= 0);
        }
        public void Process()
        {
            for(int i=0;i<Ants.Count;i++)
            {
                if(Ants[i].commands.Count==0)
                {
                    Ants[i].commands.Add(new Command(Action.findfood));
                }
                Ants[i].Thinking();
            }
        }
    }
}
