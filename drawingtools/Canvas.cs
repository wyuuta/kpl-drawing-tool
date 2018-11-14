using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using drawingtools.State;

namespace drawingtools
{
    class Canvas : Control, ICanvas
    {
        private ITool currentTool;
        private List<DrawingObject> drawingObjectList;
        private DrawingObject selectedObject;
        private bool isCtrlPressed;

        public Canvas() {
            this.drawingObjectList = new List<DrawingObject>();
            this.selectedObject = new Group();
            this.isCtrlPressed = false;

            this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += this.paint;
            this.MouseUp += this.mouseUp;
            this.MouseDown += this.mouseDown;
            this.MouseMove += this.mouseMove;
            this.KeyUp += this.keyUp;
            this.KeyDown += this.keyDown;

        }

        public void setCurrentTool(ITool tool) {
            this.currentTool = tool;
        }

        public ITool getCurrentTool()
        {
            return this.currentTool;
        }

        public void addSelectedObject(DrawingObject drawingObject)
        {
            this.selectedObject.addDrawingObject(drawingObject);
        }

        public DrawingObject getSelectedObject()
        {
            return this.selectedObject;
        }

        public void searchSelectedObject(Point point)
        {
            if (drawingObjectList != null) {
                foreach (DrawingObject drawingObject in drawingObjectList)
                {
                    if (drawingObject.isIntersect(point))
                    {  
                        if (drawingObject.getState() is StaticState)
                        {
                            if (!isCtrlPressed)
                            {
                                this.unselectObject();
                            }
                            drawingObject.setState(new EditState());
                            this.selectedObject.addDrawingObject(drawingObject);
                        }
                        this.drawCanvas();
                        return;
                    }
                }
            }

            this.unselectObject();
            this.drawCanvas();
            return;
        }

        public void moveObjectBy(int x, int y)
        {
            if (selectedObject != null)
            {
                this.selectedObject.moveObject(x, y);
                this.drawCanvas();
            }
        }

        public void addDrawingObject(DrawingObject drawingObject) {
            this.drawingObjectList.Add(drawingObject);
            this.drawCanvas();
        }

        private void unselectObject()
        {
            this.selectedObject.setState(new StaticState());
            this.selectedObject.clearDrawingObject();
        }

        private void groupSelectedObject()
        {
            foreach(DrawingObject drawingObject in selectedObject.getObjectList())
            {
                drawingObjectList.Remove(drawingObject);
            }
            this.selectedObject.setState(new StaticState());
            this.addDrawingObject(this.selectedObject);
            selectedObject = new Group();
        }

        private void ungroupSelectedObject(DrawingObject select)
        {
            if (!(select is Group))
            {
                return;
            }
            foreach(DrawingObject drawingObject in select.getObjectList())
            {
                drawingObjectList.Add(drawingObject);
            }
            drawingObjectList.Remove(select);
            selectedObject = select;
        }

        private void paint(object sender, PaintEventArgs e) {
            foreach (DrawingObject drawingObject in drawingObjectList)
            {
                drawingObject.setGraphics(e.Graphics);
                drawingObject.draw();
            }
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (this.currentTool != null)
            {
                this.currentTool.mouseDown(sender, e);
            }
        }

        private void mouseMove(object sender, MouseEventArgs e) {
            if (this.currentTool != null)
            {
                this.currentTool.mouseMove(sender, e);
            }
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            if (this.currentTool != null)
            {
                this.currentTool.mouseUp(sender, e);
            }
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.isCtrlPressed = true;
            }
            if (e.KeyCode == Keys.G)
            {
                if (this.isCtrlPressed)
                {
                    if (selectedObject.getObjectList().Count() == 1)
                    {
                        this.ungroupSelectedObject(selectedObject.getObjectList()[0]);
                    }
                    else
                    {
                        this.groupSelectedObject();
                    }
                }
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.isCtrlPressed = false;
            }
        }

        public void drawCanvas() {
            this.Invalidate();
            this.Update();
        }
    }
}
