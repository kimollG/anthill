using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ClassLibraryAntHill
{
    public interface IObjectField:IPlace
    {
        PointF Center { get; }
    }
}
