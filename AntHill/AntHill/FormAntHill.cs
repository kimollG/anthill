﻿using System;
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
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            g.DrawEllipse(new Pen(Color.Black, 4), 30, 30, 140, 140);
            field.Ants.ForEach(a => a.Draw(g));
            field.Foods.ForEach(f => f.Draw(g));
            pictureBoxAntHill.Image = bitmap;
        }
    }
}
