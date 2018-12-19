using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools.State
{
    class RotateState : DrawingState
    {
        private static DrawingState instance;

        private RotateState()
        {
            Pen pen = new Pen(Color.Gray, 2);
            this.setPen(pen);
        }

        public static DrawingState getInstance()
        {
            if (instance == null)
            {
                instance = new RotateState();
            }
            return instance;
        }

        public override void select(DrawingObject drawingObject)
        {
            drawingObject.setState(EditState.getInstance());
        }

        public override void deselect(DrawingObject drawingObject)
        {
            drawingObject.setState(StaticState.getInstance());
        }
    }
}
