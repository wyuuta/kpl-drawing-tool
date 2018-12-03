using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawingtools
{
    public interface IObserver
    {
        void update(int x, int y);
    }
}
