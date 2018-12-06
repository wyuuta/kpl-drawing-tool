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
            this.setState(new StaticState());
        }

        public override void setState(DrawingState state)
        {
            this.state = state;
            foreach(DrawingObject drawingObject in objectList)
            {
                drawingObject.setState(state);
            }
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

        public override void rotateObject(int angle)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.rotateObject(angle);
            }
        }

        public override void moveCentroid(int x, int y)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.moveCentroid(x, y);
            }
        }

        public override void updatePoints()
        {
            throw new NotImplementedException();
        }

        public override void updateCentroid()
        {

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
            throw new NotImplementedException();
        }

        public override bool isCenterPoint(Point point)
        {
            throw new NotImplementedException();
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

        public override void draw()
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.draw();
            }
        }

        public override void update(int x, int y)
        {
            foreach (DrawingObject drawingObject in objectList)
            {
                drawingObject.update(x,y);
            }
        }
    }
}
