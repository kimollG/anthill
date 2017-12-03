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
        BuilderAntHill builder;
        List<ClassLibraryAntHill.AntHill> anthills = new List<ClassLibraryAntHill.AntHill>();
        Field field;
        private void timer1_Tick(object sender, EventArgs e)
        {
            field.Process();
            Draw();            
        }
        Bitmap bitmap;
        Graphics g;
        int indexname=0;
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            int n = 0;
            int.TryParse(textBoxCount.Text, out n);
            if (n != 0)
            {
                builder = new MyBuilderAntHill(new PointF(400, 200), 100,n);
                List<ClassLibraryAntHill.AntHill> anthills = new List<ClassLibraryAntHill.AntHill>();
                anthills.Add(builder.CreateAntHill());
                field = new Field(anthills);               
                timer1.Start();
            }
        }
        public void Draw()
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            field.Draw(g);
            pictureBoxAntHill.Image = bitmap;
        }
    }
}
