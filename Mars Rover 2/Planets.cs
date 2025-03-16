using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover
{
    public class Planet
    {
        // Planet Properties.
        public string Name { get; set; }
        public int[,] Coords { get; set; }


        //Planet Methods.
        // The constructor for planets so that the rover can explore planets of different sizes.
        public Planet(string Name, int[,] Coords) 
        {
            this.Name = Name;
            this.Coords = Coords;
        }
    }
}
