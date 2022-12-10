using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTry2
{
    internal class Cell
    {
        public string name { get; set; }
        public Coordinates Coordinates { get; set; }

        public Cell(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
