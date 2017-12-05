using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    public class ImageFlyweight
    {
        public ImageFlyweight(Image im)
        {
            this.im = im;
        }
        Image im;
        public Image GetImage
        {
            get { return im; }
        }

    }
}
