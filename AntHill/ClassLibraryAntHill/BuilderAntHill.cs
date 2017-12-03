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
        public int N { get; set; }
        public BuilderAntHill(PointF center, float radius,int n)
        {
            this.center = center;
            this.radius = radius;
            N = n;
            Nodes = new List<Node>();
        }
        public abstract AntHill CreateAntHill();
        public abstract void AddNode(Node node);
        public abstract void AddEdgeToNode(int index,Edge edge);
    }
    public class MyBuilderAntHill : BuilderAntHill
    {
        public MyBuilderAntHill(PointF center, float radius,int n):base(center,radius,n)
        {
        }
        public override AntHill CreateAntHill()
        {
            NodesBuild();
            AntHill home= new AntHill(center, Nodes, radius,N);
            return home;
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
            AddNode(new Node(new PointF(center.X , center.Y - radius / 2),TypeOfNodes.larvae, new List<Edge>(), radius / 4));
            AddNode(new Node(new PointF(center.X, center.Y ), TypeOfNodes.mother, new List<Edge>(), radius / 3));
            AddNode(new Node(new PointF(center.X , center.Y + 2*radius / 3), TypeOfNodes.storage, new List<Edge>(), radius / 5));
            AddNode(new Node(new PointF(center.X - 2* radius / 3, center.Y ), TypeOfNodes.militaryroom, new List<Edge>(), radius / 6f));
            AddNode(new Node(new PointF(center.X + 6*radius/7, center.Y ), TypeOfNodes.exit, new List<Edge>(), radius / 4));
            AddNode(new Node(new PointF(center.X - radius, center.Y ), TypeOfNodes.exit, new List<Edge>(), radius / 10));

            AddEdgeToNode(0, new Edge(Nodes[1], 10));
            AddEdgeToNode(1, new Edge(Nodes[0], 10));
            AddEdgeToNode(2, new Edge(Nodes[1], 25));
            AddEdgeToNode(3, new Edge(Nodes[1], 10));
            AddEdgeToNode(4, new Edge(Nodes[1], 20));
            AddEdgeToNode(1, new Edge(Nodes[2], 10));
            AddEdgeToNode(1, new Edge(Nodes[3], 10));
            AddEdgeToNode(1, new Edge(Nodes[4], 10));
            AddEdgeToNode(3, new Edge(Nodes[5], 10));
            AddEdgeToNode(5, new Edge(Nodes[3], 10));
        }
    }

} 
