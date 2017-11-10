using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryAntHill;
namespace AntHill
{
    public partial class FormAntHill : Form
    {
        public FormAntHill()
        {
            InitializeComponent();
            timer1.Stop();
            bitmap = new Bitmap(pictureBoxAntHill.Width, pictureBoxAntHill.Height);
            g = Graphics.FromImage(bitmap);
        }
        Field field = new Field();
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            field.Process();
            Draw();
            i++;
            if(i%20==19)
            {
                Random rnd = new Random();
                double x = rnd.Next(110, 650);
                double y = rnd.Next(110, 400);
                field.BornFood(x, y);
            }
        }
        Bitmap bitmap;
        Graphics g;
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            int n = 0;
            int.TryParse(textBoxCount.Text, out n);
            Random rnd = new Random();
            for(int i=0;i<n;i++)
            {
                double x = rnd.Next(30, 170);
                double y = rnd.Next(30, 170);
                field.BornAnt(x, y, "Ant" + i.ToString());
            }
            timer1.Start();
        }
        public void Draw()
        {
            g.Clear(Color.White);
            g.DrawEllipse(new Pen(Color.Black, 4), 30, 30, 140, 140);
            for(int i=0;i<field.Ants.Count;i++)
            {
                double a = 180/ Math.PI*Math.Atan((field.Ants[i].LastY - field.Ants[i].Y) / (field.Ants[i].LastX - field.Ants[i].X));
                g.TranslateTransform(Convert.ToSingle(field.Ants[i].X - 4), Convert.ToSingle(field.Ants[i].Y - 4));
                g.RotateTransform(Convert.ToSingle(a));
                if (((Field.AntBuilder)field.Ants[i]).Bring == false)
                {
                    g.FillEllipse(Brushes.Brown, 0, 0, 10, 6);
                }
                else
                {
                    g.FillEllipse(Brushes.Green, 0, 0, 10, 6);
                }
                float x = 5;
                if (field.Ants[i].LastX > field.Ants[i].X)
                    x = 0;
                float y = 5;
                if (field.Ants[i].LastY > field.Ants[i].Y)
                    y = 0;
                g.FillEllipse(Brushes.Black, x, y, 3, 3);
                g.RotateTransform(-Convert.ToSingle(a));
                g.TranslateTransform(-Convert.ToSingle(field.Ants[i].X - 4), -Convert.ToSingle(field.Ants[i].Y - 4));
            }
            for (int i=0;i<field.Foods.Count;i++)
            {
                g.FillEllipse(Brushes.Green, Convert.ToSingle(field.Foods[i].X - 5), Convert.ToSingle(field.Foods[i].Y - 5), 10, 10);
            }
            pictureBoxAntHill.Image = bitmap;
        }
    }
}
