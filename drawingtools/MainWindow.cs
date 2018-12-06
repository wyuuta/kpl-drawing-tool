using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    public partial class MainWindow : Form
    {
        private ICanvas canvas;
        private IToolbox toolbox;

        public MainWindow()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm() {

            this.canvas = new Canvas();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.canvas);

            this.toolbox = new Toolbox();
            this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control)this.toolbox);
            this.toolbox.addTool(new MoveTool())
;            this.toolbox.addSeparator();
            this.toolbox.addTool(new LineTool());
            this.toolbox.addSeparator();
            this.toolbox.addTool(new RectangleTool());
            this.toolbox.toolSelected += selectTool;
        }

        private void selectTool(ITool tool) {
            this.canvas.setCurrentTool(tool);
            tool.setCanvas(this.canvas);
        }

        private void toolStripContainer1_Click(object sender, EventArgs e)
        {

        }
    }
}
