using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    class Canvas : Control, ICanvas
    {
        private ITool currentTool;
        private List<DrawingObject> drawingObjectList;

        public Canvas() {
            this.drawingObjectList = new List<DrawingObject>();

            this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += this.paint;
            this.MouseUp += this.mouseUp;
            this.MouseDown += this.mouseDown;
            this.MouseMove += this.mouseMove;

        }

        public void setCurrentTool(ITool tool) {
            this.currentTool = tool;
        }

        public ITool getCurrentTool()
        {
            return this.currentTool;
        }

        public void addDrawingObject(DrawingObject drawingObject) {
            this.drawingObjectList.Add(drawingObject);
            this.drawCanvas();
        }

        private void paint(object sender, PaintEventArgs e) {
            foreach (DrawingObject drawingObject in drawingObjectList)
            {
                drawingObject.setGraphics(e.Graphics);
                drawingObject.draw();
            }
        }

        private void mouseDown(object sender, MouseEventArgs e) {
            if (this.currentTool != null) {
                this.currentTool.mouseDown(sender, e);
            }
        }

        private void mouseMove(object sender, MouseEventArgs e) {
            if (this.currentTool != null)
            {
                this.currentTool.mouseMove(sender, e);
            }
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            if (this.currentTool != null)
            {
                this.currentTool.mouseUp(sender, e);
            }
        }

        public void drawCanvas() {
            this.Invalidate();
            this.Update();
        }
    }
}
