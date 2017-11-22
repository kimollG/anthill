using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class WalkatHomeStrategy : IStrategy
    {
        public IPlace place { get; set; }
        public TypeOfNodes type { get; set; }
        private Ant ant;
        private List<IStrategy> actions;
        public WalkatHomeStrategy( Ant ant, TypeOfNodes p,IPlace place)
        {
            type = p;
            this.ant = ant;
            actions = new List<IStrategy>();
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
            actions.Add(new MovingStrategy(first.point.X, first.point.Y, ant, first));
            Node finish = ant.Home.NearestNode(ant.X, ant.Y, type);
            List<Node> Nodes = ant.Home.Deicstra(first, finish);
            for (int i = 0; i < Nodes.Count; i++)
            {
                actions.Add(new MovingStrategy(Nodes[i].point.X, Nodes[i].point.Y, ant, Nodes[i]));
            }
            if(place is Field)
              actions.Add(new FindingStrategy(ant, place));
            else
            actions.Add(new DoStrategy(ant, finish));
        }
        public void Execute()
        {
            actions[0].Execute();
            if (actions[0] is MovingStrategy)
            {
                MovingStrategy s = (MovingStrategy)actions[0];
                double d1 = Math.Sqrt((s.X - ant.X) * (s.X - ant.X) + (s.Y - ant.Y) * (s.Y - ant.Y));
                if (d1 < 3)
                {
                    actions.RemoveAt(0);
                }
            }
        }
    }
}
