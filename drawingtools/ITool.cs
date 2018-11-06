using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    public interface ITool
    {
        void setCanvas(ICanvas canvas); 
        ICanvas getCanvas();

        void mouseDown(object sender, MouseEventArgs e);
        void mouseMove(object sender, MouseEventArgs e);
        void mouseUp(object sender, MouseEventArgs e);
    }
}
