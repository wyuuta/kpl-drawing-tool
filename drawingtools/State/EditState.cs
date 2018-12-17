using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace drawingtools.State
{
    class EditState : DrawingState
    {
        private static DrawingState instance;

        private EditState()
        {
            Pen pen = new Pen(Color.Blue, 2);
            this.setPen(pen);
        }

        public static DrawingState getInstance()
        {
            if (instance == null)
            {
                instance = new EditState();
            }
            return instance;
        }

        public override void deselect(DrawingObject drawingObject)
        {
            drawingObject.setState(StaticState.getInstance());
        }
    }
}
