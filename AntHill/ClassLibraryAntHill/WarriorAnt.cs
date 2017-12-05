using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    class WarriorAnt : Ant
    {
        Image im;
        public WarriorAnt(float x, float y, string name) : base(x, y, name)
        {
            Speed = 4;
            im = Image.FromFile("../../../Photos/smallAntWar.png");
        }
        public override void Draw(Graphics g)
        {
            double a = 180 / Math.PI * Math.Atan((this.LastY - this.Center.Y) / (this.LastX - this.Center.X));
            bool b = this.LastX > this.Center.X;

            g.TranslateTransform(Convert.ToSingle(this.Center.X - 4), Convert.ToSingle(this.Center.Y - 4));
            g.RotateTransform(Convert.ToSingle(a) + 90 + (b ? 180 : 0));
            g.DrawImage(im, new PointF(-im.Width * 0.5f, -im.Height * 0.5f));

            g.RotateTransform(-Convert.ToSingle(a) - 90 - (b ? 180 : 0));
            g.TranslateTransform(-Convert.ToSingle(this.Center.X - 4), -Convert.ToSingle(this.Center.Y - 4));
        }

        public override void Thinking()
        {
            if (command == null)
            {
                SetCommand(new WalkatHomeCommand(this, TypeOfNodes.exit, new Field()));
            }
            else
            {
                if (command.Execute())
                {
                    if (command is MovingCommand)
                    {
                        if (((Ant)command.place).Hp > 0)
                        {
                            ((Ant)command.place).BeAttaсked(5);
                            SetCommand(new MovingCommand(this, command.place));
                        }
                        else
                        {
                            Home.OpenEnemies.RemoveAll((x) => ((Ant)x).Hp == 0);
                            SetCommand(new FindingCommand(this, new Field(), Home.OpenEnemies, Convert.ToSingle(Math.Atan2(this.Center.Y - Home.Center.Y, Center.X - Home.Center.X))));
                        }
                    }
                    else
                    if (command is FindingCommand)
                    {
                        SetCommand(new WalkatHomeCommand(this, TypeOfNodes.militaryroom, Home));                     
                    }
                    else
                    if (command is WalkatHomeCommand)
                    {
                        if (command.place is Field)
                            SetCommand(new FindingCommand(this, new Field(), Home.OpenEnemies, Convert.ToSingle(Math.Atan2(this.Center.Y - Home.Center.Y, Center.X - Home.Center.X))));
                        else
                        {
                            SetCommand(new WalkatHomeCommand(this, TypeOfNodes.exit, new Field()));
                        }
                    }
                }
            }
            List<IObjectField> list = Home.field.FindObjects(Center.X, Center.Y);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Ant)
                {
                    if (!Home.OpenEnemies.Exists(x => x == list[i]))
                    {
                        Home.OpenEnemies.Add(list[i]);
                    }
                }
            }
        }
    }
}
