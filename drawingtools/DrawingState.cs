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
    }
}
