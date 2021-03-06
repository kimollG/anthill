﻿using System;
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
    public class Node : IObjectField
    {
        Random rnd = new Random();

        public PointF Center { get; private set; }
        public List<Edge> Edges;
        public TypeOfNodes type { get; private set; }
        public bool visit;
        public float r { get; private set; }
        public Node(PointF point, TypeOfNodes type, List<Edge> Edges, float r)
        {
            this.Center = point;
            this.type = type;
            this.Edges = Edges;
            visit = false;
            this.r = r;
        }
        public bool isInside(double x, double y)
        {
            if (AntMath.Dist(x, y, Center.X, Center.Y)< r)
            {
                return true;
            }
            return false;
        }
    }
    public class Edge 
    {
        public Node followignode { get; private set; }
        public float r { get; private set; }
        public bool visit;
        public Edge(Node followignode, float r)
        {
            this.r = r;
            this.followignode = followignode;
            visit = false;
        }
        public bool isInside(double x, double y)
        {
            if (x > followignode.Center.X && y > followignode.Center.Y)
            {
                return true;
            }
            return false;
        }
    }
    public class AntHill : IObjectField
    {
        public PointF Center { get; private set; }
        public float radius { get; private set; }
        public int Food { get; private set; }
        public List<Node> Nodes { get; private set; }
        public List<Ant> Ants { get; private set; }
        public List<IObjectField> OpenFoods { get; private set; }
        public List<IObjectField> OpenEnemies { get; private set; }
        public int Totalnumber { get; private set; }
        private int NumberWorkers = 0;
        private int NumberWarrors = 0;
        private int NumberLarvae = 0;
        public Field field { get; private set; }

        public AntHill(PointF center, List<Node> N, float rad, int n)
        {
            Ants = new List<Ant>();
            Food = 1000;
            Totalnumber = 0;
            OpenFoods = new List<IObjectField>();
            OpenEnemies = new List<IObjectField>();
            Nodes = N;
            radius = rad;
            this.Center = center;
            CreateAnts(n);
        }
        public void SetField(Field f)
        {
            if(field==null)
            {
                field = f;
            }
        }

        public void GiveFood()//Увеличение еды
        {
            Food += 100;
        }
        public Node NearestNode(double x, double y, TypeOfNodes type)//Ближайший узел по типу
        {
            List<int> indexes = new List<int>();
            double d = 1000000;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (type == Nodes[i].type)
                {
                    double d1 = Math.Sqrt((Nodes[i].Center.X - x) * (Nodes[i].Center.X - x) + (Nodes[i].Center.Y - y) * (Nodes[i].Center.Y - y));
                    if (d1 < d)
                    {
                        d = d1;
                        indexes.Add(i);
                    }
                }
            }
            return Nodes[indexes[rnd.Next(indexes.Count)]];
        }
        public void CorrectLocation(Ant ant)//Начальный метод корректирования позиции вызывается один раз
        {
            if (isInside(ant.Center.X, ant.Center.Y))
            {
                int index = -1;
                double d = 1000000;
                for (int i = 0; i < Nodes.Count; i++)
                {

                    double d1 = AntMath.Dist(Nodes[i].Center, ant.Center);
                    if (d1 < d)
                    {
                        d = d1;
                        index = i;
                    }

                }
                if (Nodes[index].r < d)
                {
                    double a = Math.Atan((ant.LastY - ant.Center.Y) / (ant.LastX - ant.Center.X));
                    ant.Move(Nodes[index].Center.X - ant.Center.X, Nodes[index].Center.Y - ant.Center.Y);
                    ant.Move(Math.Cos(a), Math.Sin(a));
                }
            }
        }
        public List<IObjectField> GiveAnts(float x, float y)//Для вредителей
        {
            List<IObjectField> objects = new List<IObjectField>();
            for (int j = 0; j < Ants.Count; j++)
            {
                double d = AntMath.Dist(x, y, Ants[j].Center.X, Ants[j].Center.Y);
                if (d < 50)
                {
                    objects.Add(Ants[j]);
                }
            }
            
            return objects;
        }
        public Node FindingNode(Ant ant)//Узел ,в котором муравей, всё по координатам
        {
            double x = ant.Center.X;
            double y = ant.Center.Y;
            if (isInside(x, y))
            {
                int index = -1;
                double d = 1000000;
                for (int i = 0; i < Nodes.Count; i++)
                {

                    double d1 = Math.Sqrt((Nodes[i].Center.X - x) * (Nodes[i].Center.X - x) + (Nodes[i].Center.Y - y) * (Nodes[i].Center.Y - y));
                    if (d1 < d)
                    {
                        d = d1;
                        index = i;
                    }

                }
                if (index != -1)
                    return Nodes[index];
                else
                    return null;
            }
            return null;
        }
        List<Node> list = new List<Node>();
        List<Node> list2 = new List<Node>();
        double dist;
        private void CreateAnts(int n)
        {
            if(Totalnumber==0)
            for (int i = 0; i < n; i++)
            {
                BornAnt("Ant" + Totalnumber.ToString());
            }
            BornMother("Mother 1");
        }
        private static Random rnd = new Random();
        public void BornAnt(string name)
        {
            Totalnumber++;           
            float x;
            float y;
            do
            { 
                x = rnd.Next((int)(Center.X - radius), (int)(Center.X + radius));
                y = rnd.Next((int)(Center.Y - radius), (int)(Center.Y + radius));
            }
            while (!isInside(x, y));
            Ant ant;
            if (NumberWorkers <= 2 * NumberWarrors)
            {
                ant = new WorkerAnt(x, y, name) { Dispose = (a) => { Ants.Remove((Ant)a); NumberWorkers--; Totalnumber--; } };
                NumberWorkers++;
            }
            else
            {
                ant= new WarriorAnt(x, y, name) { Dispose = (a) => { Ants.Remove((Ant)a); NumberWarrors--;Totalnumber--; } };
                NumberWarrors++;
            }
            ant.SetHome(this);//Установить дом
            CorrectLocation(ant);//Установить начальное положение в каком-то из Node
            Ants.Add(ant);
        }
        public void CreateLarvae()
        {
            Totalnumber++;
            float x;
            float y;
            do
            {
                x = rnd.Next((int)(Center.X - radius/4), (int)(Center.X + radius/4));
                y = rnd.Next((int)(Center.Y-radius/2 - radius/4), (int)(Center.Y - radius / 2 + radius/4));
            }
            while (!isInside(x, y));
            Ant ant = new Larvae(x, y, "larvae") { Dispose = (a) => { Ants.Remove((Ant)a); NumberLarvae--; Totalnumber--; } }; 
            NumberLarvae++;
            ant.SetHome(this);
            CorrectLocation(ant);
            Ants.Add(ant);
        }
        public void BornMother(string name)
        {
            Totalnumber++;
            Ant ant = new Mother(Center.X, Center.Y, name);
            ant.SetHome(this);
            CorrectLocation(ant);
            Ants.Add(ant);
        }
        public List<Node> Deicstra(Node firstnode, Node finishnode)
        {
            list = new List<Node>();
            list2 = new List<Node>();
            Clear();
            dist = 10000;
            Deicstra(firstnode, finishnode, 0);
            return list;
        }
        private void Deicstra(Node firstnode, Node finishnode, double d)
        {
            list2.Add(firstnode);
            firstnode.visit = true;
            if (firstnode.Center.Equals(finishnode.Center))
            {
                if (d < dist)
                {
                    dist = d;
                    list = new List<Node>(list2);
                }
            }
            for (int i = 0; i < firstnode.Edges.Count; i++)
            {
                if (firstnode.Edges[i].followignode.visit == false)
                {
                    double x = firstnode.Edges[i].followignode.Center.X;
                    double y = firstnode.Edges[i].followignode.Center.Y;
                    double dd = Math.Sqrt((firstnode.Center.X - x) * (firstnode.Center.X - x) + (firstnode.Center.Y - y) * (firstnode.Center.Y - y));
                    Deicstra(firstnode.Edges[i].followignode, finishnode, d + dd);
                    list2.RemoveAt(list2.Count - 1);
                }
            }
        }
        public void Process()
        {
            Food -= Ants.Count / 10 + 1;//Уменьшение еды в каждый тик в зависимости от количества муравьёв
            if (Food < 0)
            {
                Food = 0;
            }
            for (int i = 0; i < Ants.Count; i++)
            {
                Ants[i].Thinking();
                    
            }
            if (Food == 0)//Смерть от голода
            {
                Random rnd = new Random();
                int index = rnd.Next(0, Ants.Count * 3);
                if (index < Ants.Count)
                    Ants[index].BeAttaсked(10000);
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
        }
        public bool isInside(double x, double y)//Принадлежность муравейнику
        {
            if (AntMath.Dist(x,y,Center.X,Center.Y )<radius)
            {
                return true;
            }
            return false;
        }
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Black, Center.X - radius, Center.Y - radius, 2 * radius, 2 * radius);
            bool ok = true;
            int count = 0;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].type == TypeOfNodes.storage)
                {
                    count++;
                }
            }
            for (int i = 0; i < Nodes.Count; i++)
            {
                PointF point = Nodes[i].Center;
                float r = Nodes[i].r;
                g.FillEllipse(Brushes.White, point.X - r, point.Y - r, 2 * r, 2 * r);
                if (Nodes[i].type == TypeOfNodes.storage)
                {
                    int d = 5;
                    int[] arr = { 4, 8, 12, 16, 20, 16, 12, 8, 4 };
                    int summ = 0;
                    int kk = 0;
                    for (int j = 0; j < Food / count - (ok == true ? 0 : 1); j += 100)
                    {
                        if (j / 100 - summ == arr[kk])
                        {
                            summ += arr[kk];
                            kk++;
                            if (kk > 8)
                            {
                                break;
                            }
                        }
                    }
                    int cc = 1;
                    summ = 0;
                    for (int j = 0; j < Food / count - (ok == true ? 0 : 1); j += 100)
                    {
                        if (j / 100 - summ == arr[cc - 1])
                        {
                            summ += arr[cc - 1];
                            cc++;
                            if (cc > 8)
                            {
                                break;
                            }
                        }
                        float step = cc * r / (kk + 1);
                        float rel = j / 100 - summ;
                        float dd = rel * step / arr[cc - 1];
                        g.FillEllipse(Brushes.Green, point.X - step + dd, point.Y + r - step, d, d);
                        g.DrawEllipse(Pens.Black, point.X - step + dd, point.Y + r - step, d, d);
                    }
                    ok = false;
                }
                for (int j = 0; j < Nodes[i].Edges.Count; j++)
                {
                    float rad = Nodes[i].Edges[j].r;
                    g.DrawLine(new Pen(Color.White, rad), point.X, point.Y, Nodes[i].Edges[j].followignode.Center.X, Nodes[i].Edges[j].followignode.Center.Y);
                }
            }
            Clear();
            Ants.ForEach(a => a.Draw(g));
        }
    }
}
