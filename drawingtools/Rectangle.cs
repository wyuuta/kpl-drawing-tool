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
            PointF[] points = this.getPoints();

            points[0] = new PointF(points[0].X + x, points[0].Y + y);
            points[1] = new PointF(points[1].X + x, points[1].Y + y);
            points[2] = new PointF(points[2].X + x, points[2].Y + y);
            points[3] = new PointF(points[3].X + x, points[3].Y + y);


            this.setStartPoint(new Point(start.X + x, start.Y + y));
            this.setEndPoint(new Point(end.X + x, end.Y + y));
            this.moveCentroid(x, y);
        }

        public override void moveCentroid(int x, int y)
        {
            Point centroid = this.getCentroid();
            this.setCentroid(new Point(centroid.X + x, centroid.Y + y));
        }

        private PointF rotatePoint(PointF point, int angle)
        {
            double cos = Math.Cos(angle * Math.PI / 180);
            double sin = Math.Sin(angle * Math.PI / 180);
            Point centroid = this.getCentroid();

            return new PointF
            {
                X = (float)(cos * (point.X - centroid.X) - sin * (point.Y - centroid.Y) + centroid.X),
                Y = (float)(sin * (point.X - centroid.X) + cos * (point.Y - centroid.Y) + centroid.Y)
            };
        }

        public override void rotateObject(int angle)
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();
            PointF[] points = this.getPoints();
            int maxX = (int) points.Max(element => element.X);
            int maxY = (int) points.Max(element => element.Y);
            int minX = (int) points.Min(element => element.X);
            int minY = (int) points.Min(element => element.Y);

            points[0] = rotatePoint(points[0], angle);
            points[1] = rotatePoint(points[1], angle);
            points[2] = rotatePoint(points[2], angle);
            points[3] = rotatePoint(points[3], angle);

            this.setStartPoint(new Point(minX, minY));
            this.setEndPoint(new Point(maxX, maxY));
            this.setPoints(points);
        }

        public override void updatePoints()
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();

            PointF[] points =
            {
                new PointF(Math.Min(start.X, end.X),Math.Min(start.Y, end.Y)),
                new PointF(Math.Max(start.X, end.X),Math.Min(start.Y, end.Y)),
                new PointF(Math.Max(start.X, end.X),Math.Max(start.Y, end.Y)),
                new PointF(Math.Min(start.X, end.X),Math.Max(start.Y, end.Y)),
            };

            //this.setStartPoint(new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y)));
            //this.setEndPoint(new Point(Math.Max(start.X, end.X), Math.Max(start.Y, end.Y)));

            this.setPoints(points);
        }

        public override void updateCentroid()
        {
            PointF[] points = this.getPoints();

            if (points != null)
            {
                double maxX = points.Max(element => element.X);
                double maxY = points.Max(element => element.Y);
                double minX = points.Min(element => element.X);
                double minY = points.Min(element => element.Y);
                int x = (int) (minX + (maxX - minX) / 2);
                int y = (int) (minY + (maxY - minY) / 2);

                this.setCentroid(new Point(x, y));
            }
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

        public override bool isControlPoint(Point point)
        {
            PointF[] points = this.getPoints();

            if (
                point.X >= points[0].X - 3 &&
                point.X <= points[0].X + 3 &&
                point.Y >= points[0].Y - 3 &&
                point.Y <= points[0].Y + 3
                )
            {
                return true;
            }

            if (
                point.X >= points[1].X - 3 &&
                point.X <= points[1].X + 3 &&
                point.Y >= points[1].Y - 3 &&
                point.Y <= points[1].Y + 3
                )
            {
                return true;
            }

            if (
                point.X >= points[2].X - 3 &&
                point.X <= points[2].X + 3 &&
                point.Y >= points[2].Y - 3 &&
                point.Y <= points[2].Y + 3
                )
            {
                return true;
            }

            if (
                point.X >= points[3].X - 3 &&
                point.X <= points[3].X + 3 &&
                point.Y >= points[3].Y - 3 &&
                point.Y <= points[3].Y + 3
                )
            {
                return true;
            }

            return false;
        }

        public override bool isCenterPoint(Point point)
        {
            if (
                point.X >= this.getCentroid().X - 3 &&
                point.X <= (this.getCentroid().X + 6) &&
                point.Y >= this.getCentroid().Y - 3 &&
                point.Y <= (this.getCentroid().Y + 6)
                )
            {
                return true;
            }

            return false;
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

        private void drawControlPoint()
        {
            PointF[] points = this.getPoints();

            this.getGraphics().DrawEllipse(
                new Pen(Color.Gray, 1),
                this.getCentroid().X - 3,
                this.getCentroid().Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                new Pen(Color.Gray, 1),
                points[0].X - 3,
                points[0].Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                new Pen(Color.Gray, 1),
                points[1].X - 3,
                points[1].Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                new Pen(Color.Gray, 1),
                points[2].X - 3,
                points[2].Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                new Pen(Color.Gray, 1),
                points[3].X - 3,
                points[3].Y - 3,
                6,
                6);
        }

        public override void draw()
        {
            this.getGraphics().DrawPolygon(
                this.getState().getPen(),
                this.getPoints());
            
            if(this.getState() is EditState)
            {
                this.drawControlPoint();
            }
            
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
