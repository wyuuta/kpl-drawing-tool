using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools
{
    public abstract class DrawingObject : IObserver
    {
        private Point start;
        private Point end;
        private Point centroid;
        private Pen pen;
        private Graphics graphics;
        private List<IObservable> connected;
        private PointF[] points;
        protected DrawingState state;

        public DrawingObject()
        {
            this.connected = new List<IObservable>();
        }

        public virtual void setState(DrawingState state)
        {
            this.state = state;
        }

        public virtual DrawingState getState()
        {
            return this.state;
        }

        public virtual void setGraphics(Graphics graphics) {
            this.graphics = graphics;
        }

        public virtual Graphics getGraphics() {
            return this.graphics;
        }

        public virtual void setPen(Pen pen)
        {
            this.pen = pen;
        }

        public virtual Pen getPen()
        {
            return this.pen;
        }

        public virtual void setStartPoint(Point start)
        {
            this.start = start;
        }

        public virtual Point getStartPoint()
        {
            return this.start;
        }

        public virtual void setEndPoint(Point end)
        {
            this.end = end;
        }

        public virtual Point getEndPoint()
        {
            return this.end;
        }

        public virtual void setCentroid(Point centroid)
        {
            this.centroid = centroid;
        }

        public virtual Point getCentroid()
        {
            return this.centroid;
        }

        public virtual void setPoints(PointF[] points)
        {
            this.points = points;
        }

        public virtual PointF[] getPoints()
        {
            return this.points;
        }

        public virtual void addConnected(IObservable connected)
        {
            this.connected.Add(connected);
        }

        public virtual List<IObservable> getConnected()
        {
            return this.connected;
        }

        public abstract List<DrawingObject> getObjectList();
        public abstract void removeDrawingObject();
        public abstract void addDrawingObject(DrawingObject drawingObject);
        public abstract void clearDrawingObject();

        public abstract bool isControlPoint(Point point);
        public abstract bool isCenterPoint(Point point);
        public abstract bool isIntersect(Point point);
        public abstract void rotateObject(double angle);
        public abstract void moveObject(int x, int y);
        public abstract void moveCentroid(int x, int y);
        public abstract void draw(Pen pen);

        public abstract void update(int x, int y);
        public abstract void updatePoints();
        public abstract void updateCentroid();

        public virtual void select()
        {
            this.state.select(this);
        }

        public virtual void deselect()
        {
            this.state.deselect(this);
        }
    }
}
