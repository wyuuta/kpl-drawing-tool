using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools.State
{
    class StaticState : DrawingState
    {
        private static DrawingState instance;

        private StaticState()
        {
            Pen pen = new Pen(Color.Black, 2);
            this.setPen(pen);
        }

        public static DrawingState getInstance()
        {
            if (instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public override void select(DrawingObject drawingObject)
        {
            drawingObject.setState(EditState.getInstance());
        }
    }
}
