using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public interface IDrawable
    {
        ImageFlyweight ImageFlyWeight { get; set; }
        void Draw(Graphics g);
    }
}
