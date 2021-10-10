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
        }

        //Default defined rules
        public static Rule AttackEverything = new Rule(0, RuleFunctions.CanAttack, RuleFunctions.AttackFirst);
        public static Rule DefendPort = new Rule(0, RuleFunctions.IsEmptyPort, RuleFunctions.BuildShipInPort);
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
                        var positions = Ship.Destinations(ship.shipType, square.location).Where(p => MainGameForm.CanMove(p.X, p.Y)); //Figure out the possible moves the ship cam make
                        foreach (var p in positions)
                        {
                            if (MainGameForm.squares[p.X, p.Y].GetTeam() != hasTurn && MainGameForm.squares[p.X, p.Y].GetTeam() != Team.None) //Can it attack?
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
                            if (MainGameForm.squares[p.X, p.Y].GetTeam() != hasTurn && MainGameForm.squares[p.X, p.Y].GetTeam() != Team.None) //Can it attack?
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
        public static bool IsEmptyPort()
        {
            Team hasTurn = MainGameForm.hasTurn;
            foreach (var square in MainGameForm.squares)
            {
                if (square.isPort && square.team == hasTurn) //Is it one of my ports
                {
                    if (square.ships.Count() == 0) //Are there no ships defending the port
                    {
                        return true;
                    }
                }
            }
            return false; //All ports are defended
        }
        public static List<Move> BuildShipInPort()
        {
            List<Move> movesToMake = new List<Move>();
            Team hasTurn = MainGameForm.hasTurn;
            var ship = Ship.BuildShipInBudget(MainGameForm.teamMaterials.GetMaterials(hasTurn)); //Build a random ship
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
