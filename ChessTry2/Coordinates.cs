﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTry2
{
    internal class Coordinates
    {
       public int x { get; set; }
       public int y { get; set; }   

       public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coordinates()
        {

        }
    }
}
