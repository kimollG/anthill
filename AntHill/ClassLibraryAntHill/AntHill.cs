using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public enum TypeOfNodes
    {
        mother,
        militaryroom,
        storage,
        emptyroom,
        larvae,//личинки
        exit,
    }
    public class Node
    {
        public PointF point { get; private set; }
        public List<Ant> Ants;
        public List<Edge> Edges; 
        public TypeOfNodes type { get; private set; }
        public bool visit;
        public float r { get; private set; }
        public Node(PointF point, TypeOfNodes type, List<Edge> Edges,float r)
        {
            this.point = point;
            this.type = type;
            Ants = new List<Ant>();
            this.Edges = Edges;
            visit = false;
            this.r = r;
        }
    }
    public class Edge
    {
        public Node followignode{ get; private set; }
        public float r { get; private set; }
        public List<Ant> Ants;
        public bool visit;
        public Edge(Node followignode, float r)
        {
            this.r = r;
            Ants = new List<Ant>();
            this.followignode = followignode;
            visit = false;
        }
    }
    public class AntHill
    {
        PointF center;
        float radius;
        public int Food { get; private set; }
        List<Ant> LeaveAnts;
        public List<Node> Nodes { get; private set; }

        public AntHill(PointF center, List<Node> N,float rad)
        {
            Food = 100;
            LeaveAnts = new List<Ant>();
            Nodes = N;
            radius = rad;
            this.center = center;
        }
        public void Process()
        {
            for(int i=0;i<Nodes.Count;i++)
            {
                for (int j = 0; j < Nodes[i].Ants.Count; j++)
                {
                    Nodes[i].Ants[j].Thinking();
                    double x = Nodes[i].point.X;
                    double y = Nodes[i].point.Y;
                    double x1 = Nodes[i].Ants[j].X;
                    double y1 = Nodes[i].Ants[j].Y;
                    double d = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                    if (d>15)
                    {
                        int index = 0;
                        double min = 1000000;
                        for (int f = 0; f < Nodes[i].Edges.Count; f++)
                        {
                            double x0 = Nodes[i].Edges[f].followignode.point.X;
                            double y0 = Nodes[i].Edges[f].followignode.point.X;
                            double x10 = Nodes[i].Ants[j].commands[0].X;
                            double y10 = Nodes[i].Ants[j].commands[0].Y;
                            double d0 = Math.Sqrt((x0 - x10) * (x0 - x10) + (y0 - y10) * (y0 - y10));
                            if(min>d0)
                            {
                                min = d0;
                                index = f;
                            }
                           
                        }
                        if (Nodes[i].type == TypeOfNodes.exit && (Math.Abs(Nodes[i].Ants[j].commands[0].X - center.X) > radius || Math.Abs(Nodes[i].Ants[j].commands[0].Y - center.Y) > radius))
                        {
                            LeaveAnts.Add(Nodes[i].Ants[j]);
                        }
                        else
                        {
                            Nodes[i].Edges[index].Ants.Add(Nodes[i].Ants[j]);
                        }
                        Nodes[i].Ants.RemoveAt(j);
                        j--;
                    }
                }
                for (int j = 0; j < Nodes[i].Edges.Count; j++)
                {
                    if (Nodes[i].Edges[i].visit == false)
                    {
                        for (int f = 0; f < Nodes[i].Edges[j].Ants.Count; f++)
                        {
                            Nodes[i].Edges[j].Ants[f].Thinking();
                            double x = Nodes[i].Edges[j].Ants[f].X;
                            double y = Nodes[i].Edges[j].Ants[f].Y;
                            double x1 = Nodes[i].Edges[j].followignode.point.X;
                            double y1 = Nodes[i].Edges[j].followignode.point.Y;
                            double d = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                            if (d<3)
                            {
                                Nodes[i].Edges[j].followignode.Ants.Add(Nodes[i].Edges[j].Ants[f]);
                                Nodes[i].Edges[j].Ants.RemoveAt(f);
                                f--;
                            }
                        }
                        Nodes[i].Edges[j].visit = true;
                    }
                }
            }
           
        }
        public void Clear()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes[i].Edges.Count; j++)
                {
                    Nodes[i].Edges[j].visit = false;
                }
            }
            LeaveAnts.Clear();
        }
        public List<Ant> PostAntOut()
        {
            return LeaveAnts;
        }
        public void GetAntToHill(List<Ant> ants)
        {
            for(int i=0;i<ants.Count;i++)
            {
                for(int j=0;j<Nodes.Count;j++)
                {
                    if(Nodes[j].type==TypeOfNodes.exit)
                    {
                        double x = ants[i].X;
                        double y = ants[i].Y;
                        double x1 = Nodes[j].point.X;
                        double y1 = Nodes[j].point.Y;
                        double d = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                        if(d<5)
                        {
                            Nodes[j].Ants.Add(ants[i]);
                            break;
                        }
                    }
                }
            }
        }
        public void Draw(ref Graphics g)
        {
            g.FillEllipse(Brushes.Black, center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
            for(int i=0;i<Nodes.Count;i++)
            {
                PointF point = Nodes[i].point;
                float r = Nodes[i].r;
                g.FillEllipse(Brushes.White, point.X - r, point.Y - r, 2 * r, 2 * r);
                for (int j = 0; j < Nodes[i].Edges.Count; j++)
                {
                    float rad = Nodes[i].Edges[j].r;
                    g.DrawLine(new Pen(Color.White, rad), point.X, point.Y, Nodes[i].Edges[j].followignode.point.X, Nodes[i].Edges[j].followignode.point.Y);
                }
            }
            Clear();
        }
    }
}
