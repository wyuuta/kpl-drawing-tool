using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawingtools
{
    class Toolbox : ToolStrip, IToolbox
    {
        private ITool currentTool;
        
        public ITool CurrentTool { get => this.currentTool; set => this.currentTool = value; }

        public event ToolSelectedEventHandler toolSelected;

        public void addSeparator() {
            this.Items.Add(new ToolStripSeparator());
        }

        public void addTool(ITool tool)
        {
            if (tool is ToolStripButton)
            {
                ToolStripButton toggleButton = (ToolStripButton)tool;

                if (toggleButton.CheckOnClick)
                {
                    toggleButton.CheckedChanged += buttonClick;
                }

                this.Items.Add(toggleButton);
            }
        }

        public void removeTool(ITool tool)
        {
            foreach (ToolStripItem item in this.Items)
            {
                if (item is ITool)
                {
                    if (item.Equals(tool))
                    {
                        this.Items.Remove(item);
                    }
                }
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton button = (ToolStripButton)sender;
                if (button.Checked)
                {
                    if (button is ITool)
                    {
                        this.currentTool = (ITool)button;
                        if(toolSelected != null)
                        {
                            toolSelected(this.currentTool);
                        }
                        buttonUnclick();
                    }
                }
            }
        }

        private void buttonUnclick()
        {
            foreach(ToolStripItem item in this.Items)
            {
                if (item != this.currentTool)
                {
                    if (item is ToolStripButton)
                    {
                        ((ToolStripButton)item).Checked = false;
                    }
                }
            }
        }
    }
}
