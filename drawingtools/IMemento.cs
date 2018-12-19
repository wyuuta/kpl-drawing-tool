using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public interface IMemento
    {
        void setCurrent(List<DrawingObject> current);
        List<DrawingObject> getCurrent();
        void setPrevious(List<DrawingObject> previous);
        List<DrawingObject> getPrevious();
    }
}
