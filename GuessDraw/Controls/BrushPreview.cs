using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Controls
{
    class BrushPreview : Control
    {
        public int BrushSize { get; private set; }
        public Color BrushColor { get; private set; }

        private SolidBrush drawBrush;
        public BrushPreview()
        {
            BrushSize = 10;
            BrushColor = Color.Black;
            drawBrush = new SolidBrush(BrushColor);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = this.Width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int mid = (this.Width / 2) - (BrushSize / 2);

            var g = e.Graphics;
            g.FillEllipse(drawBrush, new Rectangle(mid, mid, BrushSize, BrushSize));
        }

        public void SetColor(Color c)
        {
            BrushColor = c;
            drawBrush = new SolidBrush(BrushColor);
            Invalidate();
        }

        public void SetSize(int s)
        {
            BrushSize = s;
            Invalidate();
        }

    }
}
