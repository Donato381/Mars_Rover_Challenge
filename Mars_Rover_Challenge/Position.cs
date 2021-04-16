using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover_Challenge
{
    public class Position : IPosition
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public Directions direction { get; set; } = Directions.N;
    }
}
