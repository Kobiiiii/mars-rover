using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover
{
    public class Rover
    {
        // Rover Properties.
        public enum directions{N, E, S, W };
        public int directionsI { get; set; } // addition is clocwise while substraction is anticlockwise.
        public int x { get; set; }
        public int y { get; set; }

        public string name { get; set; }


        // This is the rover constuctor.
        // This constructor is used when the construcor isn't going to be empty.
        public Rover(string name, directions direction, int x, int y)
        {
            this.name = name;
            this.directionsI = (int)direction;
            this.x = x;
            this.y = y;
        }

        // An empty constructor if there are no rovers to be refered to.
        // The "selectedR" variable has this by defult before I assign a rover to it.
        public Rover()
        {
            this.name = "";
            this.directionsI = 0;
            this.x = 0;
            this.y = 0;
        }

        // Rover Methods.
        // Remember, dividing a small number by a number larger than it will always be 0.
        // With this logic, any number below 4 which is divided by 4 will always be the number that is getting divided by 4.
        // Modulo is the remainer of a division.
        // For example, 2 % 4 = 2 as there is no 4's that go into 2 and 2 is the only number remaining.
        // I add 4 for rotate left to prevent dividing with negative numbers.
        public void RotateLeft()
        {
            directionsI = (4 + directionsI - 1) % 4;
        }

        public void RotateRight()
        {
            directionsI = (directionsI + 1) % 4;
        }
         
        // For Moving the rover forward dependant on the direction it's facing.
        public void Move()
        {
            switch (directionsI)
            {
                case 0: y += 1; break;
                case 1: x += 1; break;
                case 2: y -= 1; break;
                case 3: x -= 1; break;

            }

        }
    }
}
