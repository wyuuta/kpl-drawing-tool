using drawingtools.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    class Line : DrawingObject,IObservable
    {
        private DrawingObject leftConnected;
        private DrawingObject rightConnected;

        public Line() {
            this.setPen(new Pen(Color.Black, 2));
            this.setState(new StaticState());
        }

        public Line(Point start) : this() {
            this.setStartPoint(start);
        }

        public Line(Point start, Point end) : this(start) {
            this.setEndPoint(end);
        }

        public void setLeftConnected(DrawingObject leftConnected)
        {
            this.leftConnected = leftConnected;
        }

        public DrawingObject getLeftConnected()
        {
            return this.leftConnected;
        }

        public void setRightConnected(DrawingObject rightConnected)
        {
            this.rightConnected = rightConnected;
        }

        public DrawingObject getRightConnected()
        {
            return this.rightConnected;
        }

        public override List<DrawingObject> getObjectList()
        {
            throw new NotImplementedException();
        }

        public override void removeDrawingObject()
        {
            throw new NotImplementedException();
        }

        public override void addDrawingObject(DrawingObject drawingObject)
        {
            throw new NotImplementedException();
        }

        public override void clearDrawingObject()
        {
            throw new NotImplementedException();
        }

        public override void moveObject(int x, int y)
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();

            this.setStartPoint(new Point(start.X + x, start.Y + y));
            this.setEndPoint(new Point(end.X + x, end.Y + y));
        }

        public override bool isIntersect(Point point)
        {
            double m = (this.getEndPoint().Y - this.getStartPoint().Y) / (double)(this.getEndPoint().X - this.getStartPoint().X);
            double c = this.getEndPoint().Y - m * this.getEndPoint().X;

            if (Math.Abs(point.Y - (m * point.X + c)) < 2.5f)
            {
                return true;
            }
            return false;
        }

        public override void draw() {
            this.getGraphics().DrawLine(
                this.getState().getPen(),
                this.getStartPoint(),
                this.getEndPoint()
                );
        }

        public override void update(int x, int y)
        {
            if (this.leftConnected != null)
            {
                this.leftConnected.moveObject(x, y);
            }
            if (this.rightConnected != null)
            {
                this.rightConnected.moveObject(x, y);
            }
        }

        public void updatePoint(DrawingObject drawingObject, int x, int y)
        {
            if(this.getLeftConnected() == drawingObject)
            {
                Point start = this.getStartPoint();

                this.setStartPoint(new Point(start.X + x, start.Y + y));
            }
            if(this.getRightConnected() == drawingObject)
            {
                Point end = this.getEndPoint();

                this.setEndPoint(new Point(end.X + x, end.Y + y));
            }
        }
    }
}
