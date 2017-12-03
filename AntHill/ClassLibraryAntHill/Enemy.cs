using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public abstract class Enemy : IObjectField, IDisposable
    {
        PointF coordiate;
        public PointF Center
        {
            get
            {
                return coordiate;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool isInside(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
