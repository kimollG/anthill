using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    interface IStrategy
    {
        IPlace place { get; set; }
        void Execute();
    }
}
