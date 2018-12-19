using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools.State
{
    class PreviewState : DrawingState
    {
        private static DrawingState instance;

        private PreviewState()
        {
            Pen pen = new Pen(Color.Red, 2);   
            this.setPen(pen);
        }

        public static DrawingState getInstance()
        {
            if (instance == null)
            {
                instance = new PreviewState();
            }
            return instance;
        }

        public override void select(DrawingObject drawingObject)
        {
            drawingObject.setState(StaticState.getInstance());
        }

        public override void deselect(DrawingObject drawingObject)
        {
            drawingObject.setState(StaticState.getInstance());
        }
    }
}
