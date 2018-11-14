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
        private Point start;
        private Point end;
        private Pen pen;
        private Graphics graphics;
        protected DrawingState state;

        public virtual void setState(DrawingState state)
        {
            this.state = state;
        }

        public DrawingState getState()
        {
            return this.state;
        }

        public virtual void setGraphics(Graphics graphics) {
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

        public abstract List<DrawingObject> getObjectList();
        public abstract void addDrawingObject(DrawingObject drawingObject);
        public abstract void removeDrawingObject();
        public abstract void clearDrawingObject();

        public abstract bool isIntersect(Point point);
        public abstract void moveObject(int x, int y);
        public abstract void draw();
    }
}
