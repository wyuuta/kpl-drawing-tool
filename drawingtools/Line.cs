using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    class Line : DrawingObject
    {
        public Line() {
            this.setPen(new Pen(Color.Black, 2));
        }

        public Line(Point start) : this() {
            this.setStartPoint(start);
        }

        public Line(Point start, Point end) : this(start) {
            this.setEndPoint(end);
        }

        public override void draw() {
            this.getGraphics().DrawLine(
                this.getPen(),
                this.getStartPoint(),
                this.getEndPoint()
                );
        }
    }
}
