using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using drawingtools.State;

namespace drawingtools
{
    class Group : DrawingObject
    {
        private List<DrawingObject> objectList;

        public Group()
        {
            this.objectList = new List<DrawingObject>();
            this.setState(PreviewState.getInstance());
        }

        public override void setState(DrawingState state)
        {
            this.state = state;
        }

        public override void setGraphics(Graphics graphics)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.setGraphics(graphics);
            }
        }

        public override List<DrawingObject> getObjectList()
        {
            return this.objectList;
        }

        public override void addDrawingObject (DrawingObject drawingObject)
        {
            this.objectList.Add(drawingObject);
        }

        public override void removeDrawingObject()
        {
            throw new NotImplementedException();
        }

        public override void clearDrawingObject()
        {
            this.objectList.Clear();
        }

        public override void rotateObject(double angle)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.rotateObject(angle);
            }
        }

        public override void moveCentroid(int x, int y)
        {
            Point centroid = this.getCentroid();
            this.setCentroid(new Point(centroid.X + x, centroid.Y + y));

            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.setCentroid(this.getCentroid());
            }
        }

        public override void updatePoints()
        {
            throw new NotImplementedException();
        }

        public override void updateCentroid()
        {
            double maxX = 0;
            double maxY = 0;
            double minX = 99999;
            double minY = 99999;

            foreach(DrawingObject drawingObject in objectList)
            {
                PointF[] points = drawingObject.getPoints();

                maxX = Math.Max(maxX, points.Max(element => element.X));
                maxY = Math.Max(maxY, points.Max(element => element.Y));
                minX = Math.Min(minX, points.Min(element => element.X));
                minY = Math.Min(minY, points.Min(element => element.Y));
            }
            int x = (int)(minX + (maxX - minX) / 2);
            int y = (int)(minY + (maxY - minY) / 2);

            this.setCentroid(new Point(x, y));

            foreach(DrawingObject drawingObject in objectList)
            {
                drawingObject.setCentroid(this.getCentroid());
            }
        }

        public override void moveObject(int x, int y)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.moveObject(x, y);
            }
        }

        public override bool isControlPoint(Point point)
        {
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
            foreach (DrawingObject drawingObject in objectList)
            {
                if (drawingObject.isIntersect(point))
                {
                    return true;
                }
            }

            return false;
        }

        public override void draw(Pen pen)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.draw(pen);
            }
        }

        public override void update(int x, int y)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.update(x,y);
            }
        }

        public override void select()
        {
            this.getState().select(this);
            foreach(DrawingObject drawingObject in objectList)
            {
                drawingObject.select();
            }
        }

        public override void deselect()
        {
            this.getState().deselect(this);
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.deselect();
            }
        }
    }
}
