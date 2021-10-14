using System;
using System.Collections.Generic;
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
        public static Rule DefendPort = new Rule(0, RuleFunctions.IsVulnerablePort, RuleFunctions.BuildShipInPort);
    }
    public static class RuleFunctions
    {
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
                                return true; //A ship is within moving distance of our port
                            }
                        }
                    }
                }
            }
            return false; //Ports are not in danger
        }
        public static List<Move> BuildShipInPort()
        {
            List<Move> movesToMake = new List<Move>();
            Team hasTurn = MainGameForm.hasTurn;
            var ship = Ship.BuildBiggestShipInBudget(MainGameForm.teamMaterials.GetMaterials(hasTurn), Ship.ShipType.Heavy); //Build a random heavy ship
            if (ship != null) //Make sure we have enough materials to build the ship
            {
                foreach (var square in MainGameForm.squares) 
                {
                    if (square.isPort == true && square.team == hasTurn) //Is it one of my ports
                    {
                        movesToMake.Add(new Move(square, ship)); //Add the move
                        break; //We only want to build one ship in one of the ports
                    }
                }
            }
            return movesToMake;//Return the moves
        }
    }
}
