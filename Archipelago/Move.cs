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
        public static void DoRandomMove()
        {
            List<Square> teamSquares = new List<Square>();

            foreach (var s in MainGameForm.squares)
            {
                if (s.GetShipTeams() == MainGameForm.hasTurn)
                {
                    teamSquares.Add(s);
                }
            } //Add all the ships on my team into the teamShips list
            if (teamSquares.Count() == 0)
            {
                var ship = Ship.BuildShipInBudget(MainGameForm.teamMaterials.GetMaterials(MainGameForm.hasTurn)); //Build a random ship
                if (ship != null) //Make sure we have enough materials to build the ship
                {
                    MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, ship.required); ///Pay for the ship
                    ship.team = MainGameForm.hasTurn;
                    foreach(var square in MainGameForm.squares)
                    {
                        if (square.isPort && square.team == MainGameForm.hasTurn) //Is the square a port owned by me?
                        {
                            square.ships.Add(ship); //Add the ship to the port
                        }
                    }
                }
            }
            else
            {
                var squareToMoveFrom = teamSquares.Shuffle().FirstOrDefault(); //Choose a random ship
                var destinations = Ship.Destinations(squareToMoveFrom.ships.FirstOrDefault().shipType, squareToMoveFrom.location); //Find the destinations possible for the first ship in the squareToMoveFrom ships
                var randomDestination = destinations.Where(p=>MainGameForm.CanMove(p.X, p.Y)).ToList().Shuffle().FirstOrDefault(); //Choose a random destination that we can move too
                Move randomMove = new Move(squareToMoveFrom.ships.FirstOrDefault(), squareToMoveFrom.location, randomDestination);
                randomMove.DoMove();
            }
        }
    }
}
