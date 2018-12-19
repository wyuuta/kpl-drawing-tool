using drawingtools.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    class RectangleTool : ToolStripButton, ITool
    {
        private Rectangle rectangle;
        private ICanvas canvas;

        public RectangleTool()
        {
            this.Name = "Rectangle Tool";
            this.ToolTipText = "Rectangle Tool";
            this.Text = "Rectangle";
            this.CheckOnClick = true;
        }

        public void setCanvas(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public ICanvas getCanvas() {
            return this.canvas;
        }

        public void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.rectangle = new Rectangle(new Point(e.X, e.Y), new Point(e.X, e.Y));
                this.rectangle.updatePoints();
                this.canvas.addDrawingObject(this.rectangle);
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.rectangle.setEndPoint(new Point(e.X, e.Y));
                this.rectangle.updatePoints();
                this.canvas.drawCanvas();
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.rectangle.setEndPoint(new Point(e.X, e.Y));
                this.rectangle.deselect();
                this.rectangle.updatePoints();
                this.rectangle.updateCentroid();
                this.canvas.drawCanvas();

                List<DrawingObject> current = new List<DrawingObject>();
                List<DrawingObject> previous = new List<DrawingObject>();
                current.Add(this.rectangle);
                previous.Add(null);

                IMemento memento = new Memento(current, previous);
                this.canvas.addUndoStack(memento);
            }
        }

        public void ctrlAndMouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
