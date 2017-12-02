using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public class WorkerAnt : Ant
    {   
        internal bool IsBringing { get; set; }
        public WorkerAnt(float x, float y, string name,AntHill home) : base(x, y, name,home)
        {
            
            IsBringing = false;
            Speed = 3;
        }
       
        public override void BeAttaсked(int damage)
        {
            if ((Hp -= damage) <= 0)
                disp(this);
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
                        if (((Food)command.place).Hp > 0)
                        {
                            IsBringing = true;
                            ((Food)command.place).ChangeFood();
                            SetCommand(new WalkatHomeCommand(this, TypeOfNodes.storage, Home));
                        }
                        else
                        {
                            SetCommand(new FindingCommand(this, new Field(),Home.OpenFoods));
                        }
                    }
                    else
                    if (command is FindingCommand)
                    {
                        SetCommand(new WalkatHomeCommand(this, TypeOfNodes.storage, Home));
                    }
                    else
                    {
                        if (command is WalkatHomeCommand)
                        {
                            if (command.place is Field)
                                SetCommand(new FindingCommand(this, new Field(),Home.OpenFoods));
                            else
                            {
                                SetCommand(new WalkatHomeCommand(this, TypeOfNodes.exit, new Field()));
                                if (IsBringing)
                                {
                                    IsBringing = false;
                                    Home.GiveFood();
                                }
                            }
                        }

                    }
                }

            }
        }
        public override void Draw(Graphics g)
        {
            double a = 180 / Math.PI * Math.Atan((this.LastY - this.Center.Y) / (this.LastX - this.Center.X));
            g.TranslateTransform(Convert.ToSingle(this.Center.X - 4), Convert.ToSingle(this.Center.Y - 4));
            g.RotateTransform(Convert.ToSingle(a));
            bool b = this.LastX > this.Center.X;
            g.RotateTransform(90);
            if (b)
                g.RotateTransform(180);
            var im = Image.FromFile("../../../Photos/smallAnt.png");
            var im2 = Image.FromFile("../../../Photos/leaf.png");
            g.DrawImage(im, new PointF(-im.Width * 0.5f, -im.Height * 0.5f));
            if (b)
                g.RotateTransform(-180);
            g.RotateTransform(-90);
            if (((WorkerAnt)this).IsBringing)
            {
                g.DrawImage(im2, new PointF(-im2.Width * 0.5f, -im2.Height * 0.5f));
            }
            else
            {
                //g.FillEllipse(Brushes.Green, 0, 0, 10, 6);
            }
            float x = 8;
            if (this.LastX > this.Center.X)
                x = -2;
            float y = 1.115f;
            //if (((WorkerAnt)this).IsBringing)
            //g.FillEllipse(Brushes.Black, x, y, 10, 10);
            g.RotateTransform(-Convert.ToSingle(a));
            g.TranslateTransform(-Convert.ToSingle(this.Center.X - 4), -Convert.ToSingle(this.Center.Y - 4));
        }
    }
}
