using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class WalkatHomeCommand : ICommand
    {
        public IPlace place { get; set; }
        public TypeOfNodes type { get; set; }
        private Ant ant;
        private List<ICommand> actions;
        public WalkatHomeCommand( Ant ant, TypeOfNodes p,IPlace place)
        {
            type = p;
            this.ant = ant;
            actions = new List<ICommand>();
            this.place = place;
            CreateActions();
        }
        private void CreateActions()
        {
            Node first=ant.Home.FindingNode(ant.X,ant.Y,ant);
            if(first==null)
            {
                first = ant.Home.NearestNode(ant.X, ant.Y, TypeOfNodes.exit);
            }
            Node finish = ant.Home.NearestNode(ant.X, ant.Y, type);
            List<Node> Nodes = ant.Home.Deicstra(first, finish);
            for (int i = 0; i < Nodes.Count; i++)
            {
                actions.Add(new MovingCommand(Nodes[i].point.X, Nodes[i].point.Y, ant, Nodes[i]));
            }
            if(place is Field)
              actions.Add(new FindingCommand(ant, place));
            else
            actions.Add(new DoCommand(ant, finish));
        }
        public void Execute()
        {
            actions[0].Execute();
            if (actions[0] is MovingCommand)
            {
                MovingCommand s = (MovingCommand)actions[0];
                double d1 = Math.Sqrt((s.X - ant.X) * (s.X - ant.X) + (s.Y - ant.Y) * (s.Y - ant.Y));
                if (d1 < 3)
                {
                    actions.RemoveAt(0);
                }
            }
        }
    }
}
