using System.Drawing;

namespace LR5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                float x1 = float.Parse(textBox1.Text);
                float y1 = float.Parse(textBox2.Text);
                float x2 = float.Parse(textBox3.Text);
                float y2 = float.Parse(textBox4.Text);
                float x3 = float.Parse(textBox5.Text);
                float y3 = float.Parse(textBox6.Text);
                float x4 = float.Parse(textBox7.Text);
                float y4 = float.Parse(textBox8.Text);

                panel1.Refresh();
                DrawBezierCurve(x1, y1, x2, y2, x3, y3, x4, y4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private static PointF BezierPoint(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            float x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
            float y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

            return new PointF(x, y);
        }

        public static void DrawCustomBezier(Graphics g, Pen pen, PointF p0, PointF p1, PointF p2, PointF p3, int segments)
        {
            if (segments < 1) segments = 10;

            PointF previousPoint = BezierPoint(p0, p1, p2, p3, 0);
            for (int i = 1; i <= segments; i++)
            {
                float t = (float)i / segments;
                PointF currentPoint = BezierPoint(p0, p1, p2, p3, t);
                g.DrawLine(pen, previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }
        private void DrawBezierCurve(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Blue, 2);

            g.DrawRectangle(pen, x1 - 2, y1 - 2, 4, 4);
            g.DrawRectangle(pen, x2 - 2, y2 - 2, 4, 4);
            g.DrawRectangle(pen, x3 - 2, y3 - 2, 4, 4);
            g.DrawRectangle(pen, x4 - 2, y4 - 2, 4, 4);

            PointF[] points = { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4) };
            DrawCustomBezier(g, pen, points[0], points[1], points[2], points[3], 20);

            pen.Dispose();
            g.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                float x1 = float.Parse(textBox9.Text);
                float y1 = float.Parse(textBox10.Text);
                float x2 = float.Parse(textBox11.Text);
                float y2 = float.Parse(textBox12.Text);
                float x3 = float.Parse(textBox13.Text);
                float y3 = float.Parse(textBox14.Text);
                int order = int.Parse(textBox15.Text);

                panel2.Refresh();
                DrawKochFractal(x1, y1, x2, y2, x3, y3, order);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DrawKochFractal(float x1, float y1, float x2, float y2, float x3, float y3, int order)
        {
            Graphics g = panel2.CreateGraphics();
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Red, 2);

            DrawKochLine(g, pen, new PointF(x1, y1), new PointF(x2, y2), order);
            DrawKochLine(g, pen, new PointF(x2, y2), new PointF(x3, y3), order);
            DrawKochLine(g, pen, new PointF(x3, y3), new PointF(x1, y1), order);

            pen.Dispose();
            g.Dispose();
        }

        private void DrawKochLine(Graphics g, Pen pen, PointF start, PointF end, int order)
        {
            if (order == 0)
            {
                g.DrawLine(pen, start, end);
                return;
            }

            float dx = end.X - start.X;
            float dy = end.Y - start.Y;
            PointF p1 = start;
            PointF p2 = new PointF(start.X + dx / 3, start.Y + dy / 3);
            PointF p3 = new PointF(start.X + dx / 2, start.Y + dy / 2 - (float)(dx / (2 * Math.Sqrt(3))));
            PointF p4 = new PointF(start.X + 2 * dx / 3, start.Y + 2 * dy / 3);
            PointF p5 = end;

            DrawKochLine(g, pen, p1, p2, order - 1);
            DrawKochLine(g, pen, p2, p3, order - 1);
            DrawKochLine(g, pen, p3, p4, order - 1);
            DrawKochLine(g, pen, p4, p5, order - 1);
        }
    }
}
