﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools
{
    public interface ICanvas
    {
        void setCurrentTool(ITool tool);
        ITool getCurrentTool();
        void addSelectedObject(DrawingObject drawingObject);
        DrawingObject getSelectedObject();

        DrawingObject searchObject(Point point);
        int selectObject(Point point);
        void drawCanvas();
        void addDrawingObject(DrawingObject drawingObject);
        void addUndoStack(IMemento memento);
        void clearRedoStack();
    }
}
