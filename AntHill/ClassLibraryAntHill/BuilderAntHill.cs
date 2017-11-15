using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
    public abstract class BuilderAntHill
    {
        public PointF center { get; private set; }
        public float radius { get; private set; }
        public List<Node> Nodes { get; private set; }
        public BuilderAntHill(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
            Nodes = new List<Node>();
        }
        public abstract AntHill CreateAntHill();
        public abstract void AddNode(Node node);
        public abstract void AddEdgeToNode(int index,Edge edge);
    }
    public class MyBuilderAntHill : BuilderAntHill
    {
        public MyBuilderAntHill(PointF center, float radius):base(center,radius)
        {
        }
        public override AntHill CreateAntHill()
        {
            NodesBuild();
            return new AntHill(center, Nodes, radius);
        }
        public override void AddNode(Node node)
        {
            Nodes.Add(node);
        }
        public override void AddEdgeToNode(int index,Edge edge)
        {
            Nodes[index].Edges.Add(edge);
        }
        private void NodesBuild ()
        {
            AddNode(new Node(new PointF(center.X , center.Y - radius / 2),TypeOfNodes.larvae, new List<Edge>(), radius / 3));
            AddNode(new Node(new PointF(center.X, center.Y ), TypeOfNodes.mother, new List<Edge>(), radius / 3));
            AddNode(new Node(new PointF(center.X , center.Y + radius / 2), TypeOfNodes.storage, new List<Edge>(), radius / 3));
            AddNode(new Node(new PointF(center.X - 2* radius / 3, center.Y ), TypeOfNodes.exit, new List<Edge>(), radius / 2));
            AddNode(new Node(new PointF(center.X + 2 * radius / 3, center.Y ), TypeOfNodes.exit, new List<Edge>(), radius / 2));
            AddEdgeToNode(0, new Edge(Nodes[1], 10));
            AddEdgeToNode(1, new Edge(Nodes[0], 10));
            AddEdgeToNode(2, new Edge(Nodes[1], 10));
            AddEdgeToNode(3, new Edge(Nodes[1], 10));
            AddEdgeToNode(4, new Edge(Nodes[1], 10));
            AddEdgeToNode(1, new Edge(Nodes[2], 10));
            AddEdgeToNode(1, new Edge(Nodes[3], 10));
            AddEdgeToNode(1, new Edge(Nodes[4], 10));
        }
    }

} 
