using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    class Larvae : Ant
    {
        Image itSelf;
        int ticks = 60;
        public Larvae(float x, float y, string name) : base(x, y, name)
        {
            Speed = 0;
            itSelf = Image.FromFile("../../../Photos/larvae.png");
        }

        public override void Thinking()
        {

            if (ticks-- == 0)
            {
                Home.BornAnt("ant");
                disp(this);
            }
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(itSelf, Center.X - itSelf.Width / 1.5f, Center.Y - itSelf.Height / 1.5f);
        }
    }
}
