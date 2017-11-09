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
        private double X { get; }
        private double Y { get; }
        private Action action { get;}
        public Command(Action act,int x,int y)
        {
            X = x;
            Y = y;
            action = act;
        }
        public Command(Action act)
        {
            action = act;
        }
    }
}
