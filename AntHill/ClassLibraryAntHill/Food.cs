using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class Food
    {
        private int hp;
        public int Hp { get { return hp; } }
        public double X { get; }
        public double Y { get; }
        public Food(double x,double y)
        {
            hp = 100;
            X = x;
            Y = y;
        }
        public void ChangeFood()
        {
            hp -= 10;
        }
    }
}
