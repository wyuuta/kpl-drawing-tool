﻿using drawingtools.State;
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
                this.rectangle.setState(new PreviewState());
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
                this.rectangle.setState(new StaticState());
                this.rectangle.updatePoints();
                this.rectangle.updateCentroid();
                this.canvas.drawCanvas();
            }
        }

        public void ctrlAndMouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
