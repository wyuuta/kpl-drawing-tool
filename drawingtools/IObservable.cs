using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public interface IObservable
    {
        void setRightConnected(DrawingObject drawingObject);
        DrawingObject getRightConnected();
        void setLeftConnected(DrawingObject drawingObject);
        DrawingObject getLeftConnected();

        void updatePoint(DrawingObject drawingObject,int x,int y);
    }
}
