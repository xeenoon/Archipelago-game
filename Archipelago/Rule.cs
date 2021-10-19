using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago
{
    public class Rule
    {
        public int ID;
        public Func<bool> Condition; //The board state for the condition to be satisfied
        public Func<List<Move>> Reaction; //The resulting move if the board state is fitting for the condition
        public Rule(int ID, Func<bool> Condition, Func<List<Move>> Reaction)
        {
            this.ID = ID;
            this.Condition = Condition;
            this.Reaction = Reaction;

            rules.Add(this);
        }
        public static List<Rule> rules = new List<Rule>(); 

        //Default defined rules
        public static Rule AttackEverything = new Rule(0, RuleFunctions.CanAttack, RuleFunctions.AttackFirst);
        public static Rule DefendPort = new Rule(1, RuleFunctions.IsVulnerablePort, RuleFunctions.BuildShipInPort);
        public static Rule AttackPort = new Rule(2, RuleFunctions.VulnerableEnemyPort, RuleFunctions.AttackEnemyPort);
        public static Rule BuildPort = new Rule(3, RuleFunctions.HasMaterials, RuleFunctions.BuildPort);
        public static Rule UpgradePort = new Rule(4, RuleFunctions.HasPortsAndMats, RuleFunctions.UpgradePorts);
    }
    public static class RuleFunctions
    {
        static List<Square> vulnerablePorts = new List<Square>();

        //Attack everything functions
        public static bool CanAttack()
        {
            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                foreach (var ship in square.ships) //Iterate through all ships
                {
                    if (ship.team ==hasTurn) //Is it on my team?
                    {
                        var positions = Ship.Destinations(ship.shipType, square.location).Where(p => MainGameForm.CanMove(p.X, p.Y)).ToList(); //Figure out the possible moves the ship cam make
                        foreach (var p in positions)
                        {
                            var target = MainGameForm.squares[p.X, p.Y];
                            var targetTeam = target.GetTeam();
                            if (targetTeam != hasTurn && targetTeam != Team.None && Square.WinningChance(square, target) > 0.7f) //Can it attack, and is it worth attacking?
                            {
                                return true; //We can attack a square
                            }
                        }
                    }
                }
            }
            return false; //There is no ship of ours that can attack a square
        }
        public static List<Move> AttackFirst()
        {
            List<Move> movesToMake = new List<Move>();
            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                foreach (var ship in square.ships) //Iterate through all ships
                {
                    if (ship.team == hasTurn) //Is it on my team?
                    {
                        var positions = Ship.Destinations(ship.shipType, square.location).Where(p => MainGameForm.CanMove(p.X, p.Y)); //Figure out the possible moves the ship cam make
                        foreach (var p in positions)
                        {
                            var target = MainGameForm.squares[p.X, p.Y];
                            var targetTeam = target.GetTeam();
                            if (targetTeam != hasTurn && targetTeam != Team.None && Square.WinningChance(square, target) > 0.7f) //Can it attack, and is it worth attacking?
                            {
                                movesToMake.Add(new Move(ship, square.location, p)); //Add the move to the list
                                break; //We do not want one ship to have multiple moves, so just add this one
                            }
                        }
                    }
                }
            }
            movesToMake.Consolidate();
            return movesToMake; //Return the moves
        }

        //Defend port functions
        public static bool IsVulnerablePort()
        {
            vulnerablePorts = new List<Square>();

            bool result = false;
            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                if (square.isPort && square.team == hasTurn) //Is it one of my ports
                {
                    if (square.ships.Count() != 0) //Does it already have a guard
                    {
                        continue; //Ignore it, we only ever need one guard
                    }
                    //Record the positions of everything that is close enough to move to us
                    int left4  = square.location.X - 4 < 0 ? 0 : square.location.X - 4; //The furtherest left we can go
                    int down4 = square.location.Y - 4 < 0 ? 0 : square.location.Y - 4; //The furtherest right we can go

                    int right4 = square.location.X + 4 > MainGameForm.horizontalSquares-1 ? MainGameForm.horizontalSquares-1: square.location.X + 4; //The furtherst right we can go
                    int up4    = square.location.Y + 4 > MainGameForm.verticalSquares ? MainGameForm.verticalSquares : square.location.Y + 4; //The furtherest up we can go

                    for (int x = left4; x < right4; ++x) //Go from the furtherest left to the furtherest right
                    {
                        for (int y = down4; y < up4; ++y) //Go from the furtherest down to the furtherest up
                        {
                            if (MainGameForm.squares[x,y].ships.Count() != 0) //Is there 1 or more ships in the square
                            {
                                vulnerablePorts.Add(square);
                                result =  true; //A ship is within moving distance of our port
                            }
                        }
                    }
                }
            }
            return result; //Ports are not in danger
        }
        public static List<Move> BuildShipInPort()
        {
            List<Move> movesToMake = new List<Move>();
            Team hasTurn = MainGameForm.hasTurn;
            var ship = Ship.BuildBiggestShipInBudget(MainGameForm.teamMaterials.GetMaterials(hasTurn), Ship.ShipType.Heavy); //Build a random heavy ship
            if (ship != null) //Make sure we have enough materials to build the ship
            {
                foreach (var vulnerablePort in vulnerablePorts)
                {
                    if (vulnerablePort.isPort == true && vulnerablePort.team == hasTurn) //Is it one of my ports
                    {
                        movesToMake.Add(new Move(vulnerablePort, ship)); //Add the move
                    }
                }
            }
            return movesToMake;//Return the moves
        }

        //Advance on port function
        public static bool VulnerableEnemyPort()
        {
            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                if (square.isPort && square.team != hasTurn) //Is it not one of my ports
                {
                    if (square.ships.Count() != 0) //Does it already have a guard
                    {
                        continue; //Ignore it, we only ever need one guard
                    }
                    //Record the positions of everything that is close enough to move to us
                    int left4 = square.location.X - 8 < 0 ? 0 : square.location.X - 8; //The furtherest left we can go
                    int down4 = square.location.Y - 8 < 0 ? 0 : square.location.Y - 8; //The furtherest right we can go

                    int right4 = square.location.X + 8 > MainGameForm.horizontalSquares - 1 ? MainGameForm.horizontalSquares - 1 : square.location.X + 8; //The furtherst right we can go
                    int up4 = square.location.Y + 8 > MainGameForm.verticalSquares ? MainGameForm.verticalSquares : square.location.Y + 8; //The furtherest up we can go

                    for (int x = left4; x < right4; ++x) //Go from the furtherest left to the furtherest right
                    {
                        for (int y = down4; y < up4; ++y) //Go from the furtherest down to the furtherest up
                        {
                            var targetSquare = MainGameForm.squares[x, y];
                            if (targetSquare.GetTeam() == hasTurn) //Does the square have one of our ships or contain one of our ports
                            {
                                return true; //The enemy has a vulnerable port
                            }
                        }
                    }
                }
            }
            return false; //Ports are not in danger
        }
        public static List<Move> AttackEnemyPort()
        {
            List<Move> movesToMake = new List<Move>();

            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                if (square.isPort && square.team != hasTurn) //Is it not one of my ports
                {
                    if (square.ships.Count() != 0) //Does it already have a guard
                    {
                        continue; //Ignore it, we only ever need one guard
                    }
                    //Record the positions of everything that is close enough to move to us
                    int left4 = square.location.X - 8 < 0 ? 0 : square.location.X - 8; //The furtherest left we can go
                    int down4 = square.location.Y - 8 < 0 ? 0 : square.location.Y - 8; //The furtherest right we can go

                    int right4 = square.location.X + 8 > MainGameForm.horizontalSquares - 1 ? MainGameForm.horizontalSquares - 1 : square.location.X + 8; //The furtherst right we can go
                    int up4 = square.location.Y + 8 > MainGameForm.verticalSquares ? MainGameForm.verticalSquares : square.location.Y + 8; //The furtherest up we can go

                    for (int x = left4; x < right4; ++x) //Go from the furtherest left to the furtherest right
                    {
                        for (int y = down4; y < up4; ++y) //Go from the furtherest down to the furtherest up
                        {
                            var targetSquare = MainGameForm.squares[x, y];
                            if (targetSquare.GetTeam() == hasTurn) //Does the square have one of our ships or contain one of our ports
                            {
                                //The enemy has a vulnerable port
                                if (targetSquare.isPort && targetSquare.ships.Where(s=>s.shipType == Ship.ShipType.VeryFast).Count() == 0) //Is it one of our ports, and are there not already any verfast ships
                                {
                                    new Move(targetSquare, Ship.BuildFastestShipInBudget(MainGameForm.teamMaterials.GetMaterials(hasTurn))).DoMove(); //Build the fastest ship we can
                                    //Do not add this move to the movesToMake because the operations after this line require data from the newly built ship
                                }
                                var ship = targetSquare.ships.OrderByDescending(s=>s.shipType).FirstOrDefault(); //Get the fastest ship in the square

                                int distanceShipMoves = (int)ship.shipType + 1; //We start with +1 since we decrement at the start of the 'do' function
                                Point closestReal;
                                do
                                {
                                    distanceShipMoves--; //Decrement ship moves so that each time we cant move somewhere, we move one square less
                                    PointF closest = targetSquare.location.MoveTowards(square.location, distanceShipMoves + 0.45f); //Calculate the closest point the ship can move too
                                    closestReal = new Point((int)(Math.Floor(closest.X)), (int)(Math.Floor(closest.Y)));
                                } while (!MainGameForm.CanMove(closestReal.X, closestReal.Y)); //If we cannot move to the square, try again except move one square less

                                movesToMake.Add(new Move(ship, targetSquare.location, closestReal)); //Add the move for the ship to go as close as it can
                            }
                        }
                    }
                }
            }
            return movesToMake;//Return the moves
        }

        //Build port functions
        public static bool HasMaterials()
        {
            bool hasfirstRate = false;
            Materials generates = new Materials(0,0,0);
            foreach (var s in MainGameForm.squares)
            {
                if (s.isPort && s.team == MainGameForm.hasTurn) //Is it one of our ports
                {
                    if (s.ships.Where(ship => ship.name == "1st rate").Count() != 0) //Do we have a first rate in port
                    {
                        hasfirstRate = true;
                    }
                    generates += s.generates; //Increment total generates
                }
            }
            if (generates.wood >= 500) //Are we generating in excess of 1000 wood?
            {
                return false; //At this point, we want to stop building ports and we want to start upgrading them!
            }
            if (hasfirstRate)
            {
                if (MainGameForm.teamMaterials.GetMaterials(MainGameForm.hasTurn).wood >= 1000) //Do we have enough materials to build a port
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var firstRate = Ship.Create("1st rate");
                if (MainGameForm.teamMaterials.GetMaterials(MainGameForm.hasTurn) > (firstRate.required+new Materials(1000,0,0))) //Do we have enough materials to build a first rate and a port?
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static List<Move> BuildPort()
        {
            List<Move> result = new List<Move>();
            List<Square> ports = new List<Square>();
            foreach (var s in MainGameForm.squares)
            {
                if (s.isPort&&s.team == MainGameForm.hasTurn) //Is it our port
                {
                    ports.Add(s); //Add it too the list
                }
            }
            var firstRatePort = ports.Where(p=>p.ships.Count(s=>s.name == "1st rate")>=1).FirstOrDefault(); //Find the port with a firstrate ship
            var firstRate = Ship.Create("1st rate");
            if (firstRatePort == null) //Are there no first rate ports?
            {
                ports[0].ships.Add(firstRate); //Build a ship in the first port we see
                MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, firstRate.required); //Pay for the ship
                firstRatePort = ports[0]; //Assign the port to the firstratePort
            }
            else //We already have a first rate in port
            {
                firstRate = firstRatePort.ships.FirstOrDefault(s=>s.name == "1st rate"); //Find the first rate ship
            }
            result.Add(new Move(new Materials(1000,0,0), firstRate)); //Load the cargo into the first rate

            Point current = firstRatePort.location;
            var destination = Ship.Destinations(Ship.ShipType.Heavy, current).Where(p=>MainGameForm.CanMove(p.X, p.Y)).OrderByDescending(p=>MainGameForm.CalculatedGenerated(p.X, p.Y).wood).FirstOrDefault(); //Put the square witht the highest generated amount at the top
            result.Add(new Move(firstRate, current, destination)); //Move the ship to the destination
            result.Add(new Move(MainGameForm.squares[destination.X, destination.Y])); //Build the port at the destination
            return result;
        }

        //Upgrade port functions
        public static bool HasPortsAndMats()
        {
            Materials generates = new Materials(0, 0, 0);
            foreach (var s in MainGameForm.squares)
            {
                if (s.isPort && s.team == MainGameForm.hasTurn) //Is it one of our ports
                {
                    generates += s.generates; //Increment total generates
                }
            }
            if (generates.wood >= 500) //Are we generating in excess of 1000 wood?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Move> UpgradePorts()
        {
            List<Move> result = new List<Move>();
            Dictionary<Square, int> myports = new Dictionary<Square, int>();
            foreach (var s in MainGameForm.squares)
            {
                if (s.isPort && s.team == MainGameForm.hasTurn) //Is it one of our ports
                {
                    myports.Add(s, (int)(2000 * (((float)s.level) / 2f))); //Casting as float to force decimal math, formula used to slowly increase the price of a port); 
                    //Add it to the list!
                }
            }
            var orderedByCost = myports.OrderByDescending(k=>k.Value).ToList(); //Get a list of all the squares, ordered by cost(value)
            foreach (var s in orderedByCost) 
            {
                if (MainGameForm.teamMaterials.GetMaterials(MainGameForm.hasTurn).wood>s.Value) //Can we afford it?
                {
                    MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, new Materials(s.Value, 0, 0)); //Pay for the port
                    result.Add(new Move(s.Key, s.Key.level)); //Add the move to upgrade the port
                }
            }
            return result;
        }
    }
}
