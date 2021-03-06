﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class Enemy : Ant, IObjectField, IDispose, IDrawable
    {
        public Field field { get; private set; }
        public List<IObjectField> findingobjects;
        public override void Draw(Graphics g)
        {
            double a = 180 / Math.PI * Math.Atan((this.LastY - this.Center.Y) / (this.LastX - this.Center.X));
            bool b = this.LastX > this.Center.X;

            g.TranslateTransform(Convert.ToSingle(this.Center.X - 12), Convert.ToSingle(this.Center.Y - 20));
            g.RotateTransform(Convert.ToSingle(a) + 90 + (b ? 180 : 0));
            g.FillEllipse(Brushes.Chocolate, - 6,- 10, 12, 20);
            g.RotateTransform(-Convert.ToSingle(a) - 90 - (b ? 180 : 0));
            g.TranslateTransform(-Convert.ToSingle(this.Center.X - 12), -Convert.ToSingle(this.Center.Y - 20));

        }
        public Enemy(int hp, PointF c) : base(c.X, c.Y, "")
        {
            Speed = 3;
            Hp = hp;
        }
        public void setField(Field f)
        {
            if(field==null)
            {
                field = f;
            }
        }
        public override void Thinking()
        {
            findingobjects = field.FindAnts(Center.X, Center.Y);
            if (command is FindingCommand)
            {
                ((FindingCommand)command).SetFindObjects(findingobjects);
            }
            if (command == null)
            {
                SetCommand(new FindingCommand(this, field, findingobjects, Convert.ToSingle(Math.Atan2(this.Center.Y - this.LastY, Center.X - this.LastX))));
            }
            else
            {
                if (command.Execute())
                {
                    if (command is MovingCommand)
                    {
                        if (command.place is Ant && ((Ant)command.place).Hp > 0)
                        {
                            ((Ant)command.place).BeAttaсked(5);
                            SetCommand(new MovingCommand( this, command.place));
                        }
                        else
                        {
                            findingobjects.RemoveAll((x) => ((Ant)x).Hp == 0);
                            SetCommand(new FindingCommand(this, field, findingobjects, Convert.ToSingle(Math.Atan2(this.Center.Y - this.LastY, Center.X - this.LastX))));
                        }
                    }
                    else
                    if (command is FindingCommand)
                    {
                        SetCommand(new FindingCommand(this, field, findingobjects,-(float)((FindingCommand)command).angle));
                    }                 
                }
            }
        }

    }
}
