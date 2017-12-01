using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    /*public class DoCommand:ICommand
    {
        public IPlace place { get; set; }
        private Ant ant;
        public DoCommand(Ant ant,IPlace p)
        {
            this.ant = ant;
            place = p;
        }
        public bool Execute()
        {
            if (place is Food)
            {
                if (((Food)place).Hp > 0)
                {
                    ant.SetCommand(new WalkatHomeCommand(ant, TypeOfNodes.storage, ant.Home));
                    ((WorkerAnt)ant).IsBringing = true;
                    ((Food)place).ChangeFood(); 
                }
                else
                {
                    ant.SetCommand(new FindingCommand(this.ant, new Field()));
                    //ant.SetCommand(new WalkatHomeCommand(ant, TypeOfNodes.exit, new Field()));//Неверно!!!
                }
            }
            else
            {
                
                return true;*/
               /* ant.SetCommand(new WalkatHomeCommand(ant, TypeOfNodes.exit, new Field()));
                if (((WorkerAnt)ant).IsBringing)
                {
                    ((WorkerAnt)ant).IsBringing = false;
                    ant.Home.GiveFood();
                }*/
           /* }
            return false;
        }
    }*/
}
