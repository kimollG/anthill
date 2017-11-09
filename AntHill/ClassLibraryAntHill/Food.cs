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
        public void EatFood()
        {
            hp -= 10;
        }
    }
}
