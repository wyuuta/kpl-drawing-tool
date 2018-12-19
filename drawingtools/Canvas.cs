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
        private Stack<IMemento> undoStack;
        private Stack<IMemento> redoStack;
        private Group selectedObject;
        private bool isCtrlPressed;

        public Canvas() {
            this.drawingObjectList = new List<DrawingObject>();
            this.selectedObject = new Group();
            this.isCtrlPressed = false;
            this.undoStack = new Stack<IMemento>();
            this.redoStack = new Stack<IMemento>();

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

        public DrawingObject searchObject(Point point)
        {
            if (drawingObjectList != null)
            {
                foreach (DrawingObject drawingObject in drawingObjectList) {
                    if (drawingObject.isIntersect(point))
                    {
                        return drawingObject;
                    }
                }
            }

            return null;
        }

        public int selectObject(Point point)
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
                            drawingObject.select();
                            this.selectedObject.addDrawingObject(drawingObject);
                            this.drawCanvas();

                            return 1;
                        }
                        if(drawingObject.getState() is EditState)
                        {
                            return 1;
                        }
                        if (drawingObject.getState() is RotateState)
                        {
                            if (drawingObject.isControlPoint(point))
                            {
                                return 2;
                            }
                            if (drawingObject.isCenterPoint(point))
                            {
                                return 3;
                            }
                            return 1;
                        }
                    }
                }
            }

            this.unselectObject();
            this.drawCanvas();
            return 0;
        }

        public void addDrawingObject(DrawingObject drawingObject) {
            this.drawingObjectList.Add(drawingObject);
            this.drawCanvas();
        }

        public void addUndoStack(IMemento memento)
        {
            this.undoStack.Push(memento);
        }

        public void clearRedoStack()
        {
            this.redoStack.Clear();
        }

        private void unselectObject()
        {
            this.selectedObject.deselect();
            foreach(DrawingObject drawingObject in selectedObject.getObjectList())
            {
                drawingObject.updateCentroid();
            }
            this.selectedObject.clearDrawingObject();
        }

        private void groupSelectedObject()
        {
            foreach(DrawingObject drawingObject in selectedObject.getObjectList())
            {
                drawingObjectList.Remove(drawingObject);
            }
            this.selectedObject.deselect();
            this.addDrawingObject(this.selectedObject);
            selectedObject = new Group();
        }

        private void ungroupSelectedObject(Group select)
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
                drawingObject.getState().draw(drawingObject);
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
                    if (selectedObject.getObjectList().Count() == 1 && selectedObject.getObjectList()[0] is Group)
                    {
                        this.ungroupSelectedObject((Group)selectedObject.getObjectList()[0]);
                    }
                    else
                    {
                        this.groupSelectedObject();
                    }
                }
            }
            if (e.KeyCode == Keys.Z)
            {
                if (this.isCtrlPressed)
                {
                    if (undoStack.Count != 0 )
                    {
                        IMemento memento = undoStack.Pop();
                        foreach (DrawingObject drawingObject in memento.getCurrent())
                        {
                            drawingObjectList.Remove(drawingObject);
                        }
                        foreach (DrawingObject drawingObject in memento.getPrevious())
                        {
                            if (drawingObject != null)  drawingObjectList.Add(drawingObject);
                        }

                        redoStack.Push(new Memento(memento.getPrevious(), memento.getCurrent()));
                        this.drawCanvas();
                    }
                }
            }
            if (e.KeyCode == Keys.Y)
            {
                if (this.isCtrlPressed)
                {
                    if (redoStack.Count != 0)
                    {
                        IMemento memento = redoStack.Pop();
                        foreach(DrawingObject drawingObject in memento.getCurrent())
                        {
                            if (drawingObject != null) drawingObjectList.Remove(drawingObject);
                        }
                        foreach(DrawingObject drawingObject in memento.getPrevious())
                        {
                            drawingObjectList.Add(drawingObject);
                        }

                        undoStack.Push(new Memento(memento.getPrevious(), memento.getCurrent()));
                        this.drawCanvas();
                    }
                }
            }
            if (e.KeyCode == Keys.R)
            {
                if (this.isCtrlPressed)
                {
                    if (selectedObject.getState() is EditState || selectedObject.getState() is RotateState)
                    {
                        selectedObject.select();
                        this.drawCanvas();
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
