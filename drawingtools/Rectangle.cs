using drawingtools.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    class Rectangle : DrawingObject
    {
        public Rectangle() {
            this.setPen(new Pen(Color.Black, 2));
            this.setState(new StaticState());
        }

        public Rectangle(Point start) : this() {
            this.setStartPoint(start);
        }

        public Rectangle(Point start, Point end) : this(start) {
            this.setEndPoint(end);
        }

        public override void moveObject(int x, int y)
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();

            this.setStartPoint(new Point(start.X + x, start.Y + y));
            this.setEndPoint(new Point(end.X + x, end.Y + y));
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

        public override bool isIntersect(Point point)
        {
            int horizontalLen = Math.Abs(this.getStartPoint().X - this.getEndPoint().X);
            int verticalLen = Math.Abs(this.getStartPoint().Y - this.getEndPoint().Y);

            if(
                point.X >= this.getStartPoint().X &&
                point.X <= (this.getStartPoint().X + horizontalLen) && 
                point.Y >= this.getStartPoint().Y && 
                point.Y <= (this.getStartPoint().Y + verticalLen)
                )
            {
                return true;
            }
            return false;
        }

        public override void draw()
        {
            this.getGraphics().DrawRectangle(
                this.getState().getPen(),
                Math.Min(this.getStartPoint().X, this.getEndPoint().X),
                Math.Min(this.getStartPoint().Y, this.getEndPoint().Y),
                Math.Abs(this.getStartPoint().X - this.getEndPoint().X),
                Math.Abs(this.getStartPoint().Y - this.getEndPoint().Y)
                );
        }

        public override void update(int x, int y)
        {
            foreach (IObservable observable in this.getConnected())
            {
                observable.updatePoint(this, x, y);
            }
        }
    }
}
