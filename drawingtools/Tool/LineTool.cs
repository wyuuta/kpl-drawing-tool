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
    class LineTool : ToolStripButton, ITool
    {
        private Line line;
        private ICanvas canvas;

        public LineTool()
        {
            this.Name = "Line Tool";
            this.ToolTipText = "Line Tool";
            this.Text = "Line";
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
                this.line = new Line(new Point(e.X, e.Y), new Point(e.X, e.Y));
                this.line.updatePoints();
                this.canvas.addDrawingObject(this.line);

                DrawingObject connectedObject = this.canvas.searchObject(new Point(e.X, e.Y));
                if (connectedObject != null && connectedObject != this.line)
                {
                    this.line.setLeftConnected(connectedObject);
                    connectedObject.addConnected(line);
                }
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.line.setEndPoint(new Point(e.X, e.Y));
                this.line.updatePoints();
                this.canvas.drawCanvas();
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.line.setEndPoint(new Point(e.X, e.Y));
                this.line.updatePoints();
                this.line.updateCentroid();
                this.line.deselect();
                this.canvas.drawCanvas();

                DrawingObject connectedObject = this.canvas.searchObject(new Point(e.X, e.Y));
                if (connectedObject != null && connectedObject != this.line)
                {
                    this.line.setRightConnected(connectedObject);
                    connectedObject.addConnected(line);
                }

                List<DrawingObject> current = new List<DrawingObject>();
                List<DrawingObject> previous = new List<DrawingObject>();
                current.Add(this.line);
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
