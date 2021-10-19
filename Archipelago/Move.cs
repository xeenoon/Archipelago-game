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

        public Square port;
        public Ship shipToBuild;

        public Square portToBuild;

        public Materials cargoToLoad;
        public Ship shipForCargo;

        public Square upgradePortSquare;
        public int newLevel;

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
        public Move(Square port, Ship shipToBuild)
        {
            this.port = port;
            this.shipToBuild = shipToBuild;
        }
        public Move(Materials cargoToLoad, Ship shipForCargo)
        {
            this.cargoToLoad = cargoToLoad;
            this.shipForCargo = shipForCargo;
        }
        public Move(Square portToBuild)
        {
            this.portToBuild = portToBuild;
        }
        public Move(Square upgradePortSquare, int newLevel)
        {
            this.upgradePortSquare = upgradePortSquare;
            this.newLevel = newLevel;
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
            if (shipToBuild != null) //Is there a ship to build
            {
                port.ships.Add(shipToBuild);
                MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, shipToBuild.required); ///Pay for the ship
                shipToBuild.team = MainGameForm.hasTurn; //Set the ships team
                shipToBuild.canAttack = false; //Ship cannot move on the same turn it was built
            }
            if (portToBuild != null) //Are we building a port?
            {
                MainGameForm.CreatePort(portToBuild.location.X, portToBuild.location.Y, MainGameForm.hasTurn); //Build the port
                portToBuild.Buy(new Materials(1000,0,0)); //Pay for the port
            }
            if (cargoToLoad != null) //Are we loading cargo
            {
                if (!shipForCargo.LoadMaterials(cargoToLoad)) //Load the cargo
                {
                    throw new Exception("Cannot load required cargo. Move line 76"); //Handle errors
                }
                MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, cargoToLoad); ///Pay for the cargo
            }
            if (upgradePortSquare != null)
            {
                upgradePortSquare.level = newLevel; //Upgrade the port
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
