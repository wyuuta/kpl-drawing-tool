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
        private int flag;

        public MoveTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Text = "Selection";
            this.CheckOnClick = true;
            this.flag = 0;
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
                this.flag = this.canvas.selectObject(this.startPoint);
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && startPoint != null)
            {
                if (this.flag == 1)
                {
                    this.canvas.moveObjectBy(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                }
                if (this.flag == 2)
                {
                        if (e.X - this.startPoint.X > 0 || e.Y - this.startPoint.Y > 0) this.canvas.rotateObjectBy(1);
                        else if (e.X - this.startPoint.X < 0 || e.Y - this.startPoint.Y < 0) this.canvas.rotateObjectBy(-1);
                }
                if (this.flag == 3)
                {
                    this.canvas.moveCentroidBy(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                }
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
