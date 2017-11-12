using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public static class AntMath
    {
        public static double GetAngleBetwenTwoVectors(double x1,double y1,double x2,double y2)
        {
            double g = Math.Sqrt(x1 * x1 + y1 * y1) * Math.Sqrt(x2 * x2 + y2 * y2);

            if(Math.Abs((x1 * x2 + y1 * y2) / g)>1)
            {
                if ((x1 * x2 + y1 * y2) / g > 1)
                    return Math.Acos(1);
                else
                    return Math.Acos(-1);

            }
            double f = x1 * x2 + y1 * y2;
            return Math.Acos((f )/ g);
        }
    }
}
