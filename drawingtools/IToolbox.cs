using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public delegate void ToolSelectedEventHandler(ITool tool);

    public interface IToolbox
    {
        event ToolSelectedEventHandler toolSelected;
        void addSeparator();
        void addTool(ITool tool);
        void removeTool(ITool tool);
        ITool CurrentTool { get; set; }
    }
}
