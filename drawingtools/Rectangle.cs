using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    class Rectangle : DrawingObject
    {
        public Rectangle() {
            this.setPen(new Pen(Color.Black, 2));
        }

        public Rectangle(Point start) : this() {
            this.setStartPoint(start);
        }

        public Rectangle(Point start, Point end) : this(start) {
            this.setEndPoint(end);
        }

        public override void draw()
        {
            this.getGraphics().DrawRectangle(
                this.getPen(),
                Math.Min(this.getStartPoint().X, this.getEndPoint().X),
                Math.Min(this.getStartPoint().Y, this.getEndPoint().Y),
                Math.Abs(this.getStartPoint().X - this.getEndPoint().X),
                Math.Abs(this.getStartPoint().Y - this.getEndPoint().Y)
                );
        }
    }
}
