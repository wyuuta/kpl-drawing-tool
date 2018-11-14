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
        public StaticState()
        {
            Pen pen = new Pen(Color.Black, 2);
            this.setPen(pen);
        }
    }
}
