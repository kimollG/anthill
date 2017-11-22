using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class DoStrategy:IStrategy
    {
        public IPlace place { get; set; }
        private Ant ant;
        public DoStrategy(Ant ant,IPlace p)
        {
            this.ant = ant;
            place = p;
        }
        public void Execute()
        {
            if (ant is WorkerAnt)
            {
                if (place is Food)
                {
                    if (((Food)place).Hp > 0)
                    {
                        // ant.SetStrategy(new MovingStrategy(ant.Home.center.X, ant.Home.center.Y, this.ant, ant.Home));
                        ant.SetStrategy(new WalkatHomeStrategy(ant, TypeOfNodes.storage, ant.Home));
                        ((WorkerAnt)ant).IsBringing = true;
                        ((Food)place).ChangeFood();
                    }
                    else
                    {
                        ant.SetStrategy(new FindingStrategy(this.ant, new Field()));
                    }
                }
                else
                {
                    //ant.SetStrategy(new FindingStrategy(this.ant, new Field()));
                    ant.SetStrategy(new WalkatHomeStrategy(ant, TypeOfNodes.exit,new Field()));
                    ((WorkerAnt)ant).IsBringing = false;
                }
            }
        }
    }
}
