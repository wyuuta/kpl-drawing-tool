using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    class MoveTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Point startPoint;

        public MoveTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Text = "Selection";
            this.CheckOnClick = true;
        }

        public void setCanvas(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public ICanvas getCanvas()
        {
            return this.canvas;
        }

        public void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.startPoint = new Point(e.X, e.Y);
                this.canvas.searchSelectedObject(this.startPoint);
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && startPoint != null)
            {
                this.canvas.moveObjectBy(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                this.startPoint = new Point(e.X, e.Y);
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
        }

        public void ctrlAndMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
        }
    }
}
