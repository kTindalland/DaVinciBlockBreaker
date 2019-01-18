using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker.Resources
{
    public class BallMovedEventArgs : EventArgs
    {
        public double[] Position { get; set; }
    }
}
