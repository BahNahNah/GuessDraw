using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Controls
{
    delegate void OnDotDrawDelegate(Color c, Point p);
    class DrawCanvas : Control
    {
        public SolidBrush DrawingBrush { get; set; }
        public event OnDotDrawDelegate OnDotDraw;
        public int DrawingSize
        {
            get { return drawSize.Width; }
            set { drawSize = new Size(value, value); }
        }
        public bool CanDraw { get; set; }
        private bool IsDrawing = false;
        private Size drawSize;
        public DrawCanvas()
        {
            CanDraw = true;
            DrawingBrush = new SolidBrush(Color.Black);
            DrawingSize = 10;
            
        }

        public void ClearDraw()
        {
            this.Invalidate();
        }

        public void DrawDot(Point p)
        {
            int halfDraw = (int)(drawSize.Width / 2);
            Point newPoint = new Point(p.X - halfDraw, p.Y - halfDraw);
            DrawDot(newPoint, true);
        }

        public void DrawDot(Point p, bool sendToServer)
        {
            if (DrawingBrush == null)
                return;
           
            lock (DrawingBrush)
            {
                Graphics g = this.CreateGraphics();
                g.FillEllipse(DrawingBrush, new Rectangle(p, drawSize));
                if(sendToServer)
                    OnDotDraw?.Invoke(DrawingBrush.Color, p);
            }
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CanDraw)
                IsDrawing = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            
            IsDrawing = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //if (e.X <= -1 || e.Y <= -1)
            //  IsDrawing = false;

            if (IsDrawing)
                DrawDot(e.Location);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
        }

    }
}
