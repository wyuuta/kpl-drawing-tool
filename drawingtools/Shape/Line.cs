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
            this.setState(PreviewState.getInstance());
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

        private PointF rotatePoint(PointF point, double angle)
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

        public override void rotateObject(double angle)
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();
            PointF[] points = this.getPoints();

            points[0] = rotatePoint(points[0], angle);
            points[1] = rotatePoint(points[1], angle);

            this.setStartPoint(new Point((int)points[0].X, (int)points[0].Y));
            this.setEndPoint(new Point((int)points[1].X, (int)points[1].Y));
            this.setPoints(points);
        }

        public override void moveCentroid(int x, int y)
        {
            Point centroid = this.getCentroid();
            this.setCentroid(new Point(centroid.X + x, centroid.Y + y));
        }

        public override void updatePoints()
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();
            PointF[] points =
            {
                new PointF(start.X, start.Y),
                new PointF(end.X, end.Y),
            };

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
                int x = (int)(minX + (maxX - minX) / 2);
                int y = (int)(minY + (maxY - minY) / 2);

                this.setCentroid(new Point(x, y));
            }
        }

        public override void moveObject(int x, int y)
        {
            Point start = this.getStartPoint();
            Point end = this.getEndPoint();

            this.setStartPoint(new Point(start.X + x, start.Y + y));
            this.setEndPoint(new Point(end.X + x, end.Y + y));
            this.moveCentroid(x, y);
            this.updatePoints();
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
            double m = (this.getEndPoint().Y - this.getStartPoint().Y) / (double)(this.getEndPoint().X - this.getStartPoint().X);
            double c = this.getEndPoint().Y - m * this.getEndPoint().X;

            if (Math.Abs(point.Y - (m * point.X + c)) < 2.5f)
            {
                return true;
            }
            return false;
        }

        private void drawControlPoint(Pen pen)
        {
            PointF[] points = this.getPoints();
            this.getGraphics().DrawEllipse(
                pen,
                this.getCentroid().X - 3,
                this.getCentroid().Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                pen,
                points[0].X - 3,
                points[0].Y - 3,
                6,
                6);
            this.getGraphics().DrawRectangle(
                pen,
                points[1].X - 3,
                points[1].Y - 3,
                6,
                6);
        }

        public override void draw(Pen pen) {
            this.getGraphics().DrawLine(
                pen,
                this.getStartPoint(),
                this.getEndPoint()
                );

            if (this.getState() is EditState)
            {
                drawControlPoint(pen);
            }
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
                this.updatePoints();
                this.updateCentroid();
            }
            if (this.getRightConnected() == drawingObject)
            {
                Point end = this.getEndPoint();

                this.setEndPoint(new Point(end.X + x, end.Y + y));
                this.updatePoints();
                this.updateCentroid();
            }
        }
    }
}
