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
            Graphics.FromImage(bitmap);
        }
        Field field = new Field();
        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
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
            bitmap = new Bitmap(pictureBoxAntHill.Image, pictureBoxAntHill.Width, pictureBoxAntHill.Height);

        }
    }
}
