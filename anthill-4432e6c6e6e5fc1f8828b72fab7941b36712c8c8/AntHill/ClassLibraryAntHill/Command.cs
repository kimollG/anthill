using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public enum Action
    {
        free,
        findfood,
        attack,
        gotoFood,
        gotoAntHill,
    }

    public class Command
    {
        public double X { get; }
        public double Y { get; }
        public Action action { get;}
        public Command(Action act,double x,double y)
        {
            X = x;
            Y = y;
            action = act;
        }
        public Command(Action act)
        {
            X = -1;
            Y = -1;
            action = act;
        }
    }
}
