using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago
{
    public class Move
    {
        public List<Ship> toMove = new List<Ship>();
        public Point currentLocation;
        public Point destination;

        public Move(List<Ship> toMove, Point currentLocation, Point destination)
        {
            this.toMove = toMove;
            this.currentLocation = currentLocation;
            this.destination = destination;
        }
        public Move(Ship toMove, Point currentLocation, Point destination)
        {
            this.toMove.Add(toMove);
            this.currentLocation = currentLocation;
            this.destination = destination;
        }

        public void DoMove()
        {
            foreach (var ship in toMove)
            {
                MainGameForm.squares[currentLocation.X, currentLocation.Y].ships.Remove(ship); //Remove the ship from its old destination
                MainGameForm.squares[destination.X, destination.Y].ships.Add(ship); //Add the ship to its new destination
                MainGameForm.RunAttack(MainGameForm.squares[destination.X, destination.Y]); //Attack the square
                ship.hasMoved = false; //The ship has now moved, and cannot move again until the next turn
            }
        }
    }
}
