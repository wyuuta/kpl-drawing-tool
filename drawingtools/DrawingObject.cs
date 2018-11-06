using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools
{
    public abstract class DrawingObject
    {
        private int id;
        private Point start;
        private Point end;
        private Pen pen;
        private Graphics graphics;

        public void setGraphics(Graphics graphics) {
            this.graphics = graphics;
        }

        public Graphics getGraphics() {
            return this.graphics;
        }

        public void setPen(Pen pen)
        {
            this.pen = pen;
        }

        public Pen getPen()
        {
            return this.pen;
        }

        public void setStartPoint(Point start)
        {
            this.start = start;
        }

        public Point getStartPoint()
        {
            return this.start;
        }

        public void setEndPoint(Point end)
        {
            this.end = end;
        }

        public Point getEndPoint()
        {
            return this.end;
        }

        public abstract void draw();
    }
}
