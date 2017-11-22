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
    public class Node:IPlace
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
        public bool isInside(double x,double y)
        {
            if(x<point.X+r && x > point.X-r && y < point.Y + r && y > point.Y - r)
            {
                return true;
            }
            return false;
        }
    }
    public class Edge:IPlace
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
        public bool isInside(double x, double y)
        {
            if (x > followignode.point.X  && y > followignode.point.Y  )
            {
                return true;
            }
            return false;
        }
    }
    public class AntHill:IPlace
    {
        public PointF center { get; private set; }
        public float radius { get; private set; }
        public int Food { get; private set; }
        List<Ant> LeaveAnts;
        public List<Node> Nodes { get; private set; }
        public List<Food> OpenFoods { get; private set; }
        public AntHill(PointF center, List<Node> N,float rad)
        {
            Food = 100;
            LeaveAnts = new List<Ant>();
            OpenFoods = new List<Food>();
            Nodes = N;
            radius = rad;
            this.center = center;
        }
        public Node NearestNode(double x, double y, TypeOfNodes type)
        {
            int index = -1;
            double d = 1000000;
            for (int i = 0; i < Nodes.Count;i++)
            {
                if(type==Nodes[i].type)
                {
                    double d1 = Math.Sqrt((Nodes[i].point.X - x) * (Nodes[i].point.X - x) + (Nodes[i].point.Y - y) * (Nodes[i].point.Y - y));
                    if(d1<d)
                    {
                        d = d1;
                        index = i;
                    }
                }
            }
            return Nodes[index];
        }
        public Node FindingNode(double x, double y,Ant ant)
        {
            if (isInside(x, y))
            {
                int index = -1;
                double d = 1000000;
                for (int i = 0; i < Nodes.Count; i++)
                {

                    double d1 = Math.Sqrt((Nodes[i].point.X - x) * (Nodes[i].point.X - x) + (Nodes[i].point.Y - y) * (Nodes[i].point.Y - y));
                    if (d1 < d)
                    {
                        d = d1;
                        index = i;
                    }

                }
                if (Nodes[index].r < d)
                {
                    double a = Math.Atan((ant.LastY - ant.Y) / (ant.LastX - ant.X));
                    ant.Move(Nodes[index].point.X-ant.X, Nodes[index].point.Y-ant.Y);
                    ant.Move(Math.Cos(a) , Math.Sin(a) );
                }
                return Nodes[index];
            }
            return null;
        }
        List<Node> list = new List<Node>();
        List<Node> list2 = new List<Node>();
        double dist;
        public List<Node> Deicstra(Node firstnode,Node finishnode )
        {
            list = new List<Node>();
            list2 = new List<Node>();
            Clear();
            dist = 10000;
            Deicstra(firstnode,finishnode,0);
            return list;
        }
        private void Deicstra(Node firstnode, Node finishnode,double d)
        {
            list2.Add(firstnode);
            firstnode.visit = true;
            if (firstnode.point.Equals(finishnode.point))
            {
                if(d<dist)
                {
                    dist = d;
                    list =new List<Node>(list2);
                }
            }
            for(int i=0;i<firstnode.Edges.Count;i++)
            {
                if(firstnode.Edges[i].followignode.visit==false)
                {
                    double x = firstnode.Edges[i].followignode.point.X;
                    double y = firstnode.Edges[i].followignode.point.Y;
                    double dd= Math.Sqrt((firstnode.point.X - x) * (firstnode.point.X - x) + (firstnode.point.Y - y) * (firstnode.point.Y - y));
                    Deicstra(firstnode.Edges[i].followignode, finishnode, d + dd);
                    list2.RemoveAt(list2.Count - 1);
                }
            }
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
                           // double x10 = Nodes[i].Ants[j].commands[0].X;
                            //double y10 = Nodes[i].Ants[j].commands[0].Y;
                           // double d0 = Math.Sqrt((x0 - x10) * (x0 - x10) + (y0 - y10) * (y0 - y10));
                           /* if(min>d0)
                            {
                                min = d0;
                                index = f;
                            }*/
                           
                        }
                       // if (Nodes[i].type == TypeOfNodes.exit && (Math.Abs(Nodes[i].Ants[j].commands[0].X - center.X) > radius || Math.Abs(Nodes[i].Ants[j].commands[0].Y - center.Y) > radius))
                        {
                            LeaveAnts.Add(Nodes[i].Ants[j]);
                        }
                        //else
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
                Nodes[i].visit = false;
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
        public bool isInside(double x, double y)
        {
            if (x < center.X + radius && x > center.X - radius && y < center.Y + radius && y > center.Y - radius)
            {
                return true;
            }
            return false;
        }
        public void Draw(Graphics g)
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
