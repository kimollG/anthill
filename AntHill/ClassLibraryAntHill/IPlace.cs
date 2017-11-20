using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public interface IPlace
    {
        bool isInside(double x, double y);
    }
}
