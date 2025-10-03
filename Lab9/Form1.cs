using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
            DefoltDis();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DefoltDis()
        {
            comboBox1.Items.AddRange(new string[] { "y = 2u tg(ax)", "y = |bx|", "y = d e^(kx)" });
            comboBox1.SelectedIndex = 0;

            textBoxA.Text = "1";
            textBoxB.Text = "1";
            textBoxD.Text = "1";
            textBoxU.Text = "1";
            textBoxK.Text = "1";

            buttonUpdate.Click += (sender, e) => pictureBox1.Invalidate();
        }

        private double GetCoefficient(string text, double defaultValue)
        {
            if (double.TryParse(text, out double value))
                return value;
            return defaultValue;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            double a = GetCoefficient(textBoxA.Text, 1);
            double b = GetCoefficient(textBoxB.Text, 1);
            double d = GetCoefficient(textBoxD.Text, 1);
            double u = GetCoefficient(textBoxU.Text, 1);
            double k = GetCoefficient(textBoxK.Text, 1);

            Pen pen = new Pen(Color.Red, 2);

            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            double scale = 20;

            e.Graphics.DrawLine(Pens.Black, 0, centerY, pictureBox1.Width, centerY);
            e.Graphics.DrawLine(Pens.Black, centerX, 0, centerX, pictureBox1.Height);

            Point prevPoint = new Point();
            bool firstIteration = true;

            for (double x = -10; x <= 0; x += 0.01)
            {
                double xScaled = x * scale;
                double y;

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        if (Math.Abs(Math.Cos(a * x)) < 0.0001)
                            continue;
                        y = 2 * u * Math.Tan(a * x);
                        break;
                    case 1: 
                        y = Math.Abs(b * x);
                        break;
                    case 2:
                        y = d * Math.Exp(k * x);
                        break;
                    default:
                        continue;
                }

                int currentX = centerX + (int)xScaled;
                int currentY = centerY - (int)(y * scale);

                if (!firstIteration)
                {
                    e.Graphics.DrawLine(pen, prevPoint.X, prevPoint.Y, currentX, currentY);
                }
                else
                {
                    firstIteration = false;
                }

                prevPoint = new Point(currentX, currentY);
            }
        }
    }
}
