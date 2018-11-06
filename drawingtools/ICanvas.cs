using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public interface ICanvas
    {
        void setCurrentTool(ITool tool);
        void drawCanvas();

        void addDrawingObject(DrawingObject drawingObject);
    }
}
