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
        public EditState()
        {
            Pen pen = new Pen(Color.Red, 2);
            this.setPen(pen);
        }
    }
}
