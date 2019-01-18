using System;

namespace DaVinci_Framework.KeyGetterThread
{
    public class KeyEventArgs : EventArgs
    {
        public ConsoleKey KeyPressed { get; set; } // Create property to hold what key was pressed
    }
}

