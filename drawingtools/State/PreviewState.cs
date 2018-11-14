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
        public PreviewState()
        {
            Pen pen = new Pen(Color.Yellow, 2);
            this.setPen(pen);
        }
    }
}
