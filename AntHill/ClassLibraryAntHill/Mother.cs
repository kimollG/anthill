using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassLibraryAntHill
{
    class Mother : Ant
    {
        Image itSelf;
        public Mother(float x, float y, string name) : base(x, y, name)
        {
            Speed = 0;
            Hp = 1000000000;
            itSelf = Image.FromFile("../../../Photos/mother.png");
        }
        public override void Thinking()
        {
            if (Home.field.Numberticks % 100 == 99)
            {
                Home.CreateLarvae();
            }
        }
        public override void Draw(Graphics g)
        {
             g.DrawImage(itSelf, Center.X - itSelf.Width / 1.5f, Center.Y - itSelf.Height / 1.5f);
        }
    }
}
