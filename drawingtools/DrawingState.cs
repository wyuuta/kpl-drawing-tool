using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools
{
    public abstract class DrawingState
    {
        private Pen pen;

        public void setPen(Pen pen)
        {
            this.pen = pen;
        }

        public Pen getPen()
        {
            return this.pen;
        }

        public void draw(DrawingObject drawingObject)
        {
            drawingObject.draw(this.pen);
        }

        public virtual void select(DrawingObject drawingObject)
        {

        }

        public virtual void deselect(DrawingObject drawingObject)
        {

        }
    }
}
