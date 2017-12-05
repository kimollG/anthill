using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class WalkatHomeCommand : ICommand
    {
        public IObjectField place { get; set; }
        public TypeOfNodes type { get; set; }
        private Ant ant;
        private List<ICommand> actions;
        public WalkatHomeCommand( Ant ant, TypeOfNodes p, IObjectField place)
        {
            type = p;
            this.ant = ant;
            actions = new List<ICommand>();
            this.place = place;
            CreateActions();
        }
        private void CreateActions()//Вся команда нужна для того чтобы выйти из муравейника или прийти в него до нужного типа узла
        {
            Node first=ant.Home.FindingNode(ant);
            if(first==null )
            {
                first = ant.Home.NearestNode(ant.Center.X, ant.Center.Y, TypeOfNodes.exit);
            }
            Node finish = ant.Home.NearestNode(ant.Center.X, ant.Center.Y, type);
            List<Node> Nodes = new List<Node>();
            Nodes = ant.Home.Deicstra(first, finish);
            for (int i = 0; i < Nodes.Count; i++)
            {
                actions.Add(new MovingCommand( ant, Nodes[i]));
            }
        }
        public bool Execute()
        {
            if (actions.Count == 0)
                return true;
            if (actions[0].Execute())
            {
                actions.RemoveAt(0);
                ant.AtHome = true;
            }
            return false;
        }
    }
}
