using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public class Memento : IMemento
    {
        private List<DrawingObject> current;
        private List<DrawingObject> previous;

        public Memento(List<DrawingObject> current, List<DrawingObject> previous)
        {
            this.current = current;
            this.previous = previous;
        }

        public Memento()
        {

        }

        public void setCurrent(List<DrawingObject> current)
        {
            this.current = current;
        }

        public List<DrawingObject> getCurrent()
        {
            return this.current;
        }

        public void setPrevious(List<DrawingObject> previous)
        {
            this.previous = previous;
        }

        public List<DrawingObject> getPrevious()
        {
            return this.previous;
        }
    }
}
