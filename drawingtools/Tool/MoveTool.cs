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
        private DrawingObject selectedObject;

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
                if (this.flag != 0) this.selectedObject = this.canvas.getSelectedObject();
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && startPoint != null)
            {
                if (this.flag == 1)
                {
                    int x = e.X - this.startPoint.X;
                    int y = e.Y - this.startPoint.Y;
                    //this.canvas.moveObjectBy(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    this.selectedObject.moveObject(x, y);
                    this.selectedObject.update(x, y);
                    this.canvas.drawCanvas();
                }
                if (this.flag == 2)
                {
                    //this.canvas.rotateObjectBy(this.startPoint, new Point(e.X, e.Y));
                    Point centroid = selectedObject.getCentroid();
                    //double angle = Math.Atan2(first.Y - centroid.Y, first.X - centroid.X) - Math.Atan2(second.Y - centroid.Y, second.X - centroid.X);
                    if (e.X - this.startPoint.X + e.Y - this.startPoint.Y > 0) this.selectedObject.rotateObject(1);
                    else if (e.X - this.startPoint.X + e.Y - this.startPoint.Y < 0) this.selectedObject.rotateObject(-1);
                    this.canvas.drawCanvas();
                }
                if (this.flag == 3)
                {
                    int x = e.X - this.startPoint.X;
                    int y = e.Y - this.startPoint.Y;
                    //this.canvas.moveCentroidBy(e.X - this.startPoint.X, e.Y - this.startPoint.Y);
                    this.selectedObject.moveCentroid(x, y);
                    this.canvas.drawCanvas();
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
