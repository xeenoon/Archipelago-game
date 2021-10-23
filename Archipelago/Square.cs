using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago
{
    public class Square
    {
        public bool isPort; //Using boolean value instead of subclass for simplicity
        public List<Ship> ships = new List<Ship>(); //List of ships in the square
        public Point location; //Squares relative location
        public Team team = Team.None;
        public int level = 1;
        public Square(Point location)
        {
            this.location = location;
        }

        public Square(int x, int y)
        {
            this.location = new Point(x,y);
        }

        public Materials generates;
        internal bool red = false;
        internal bool orange = false;

        internal Materials GetMaterials()
        {
            Materials total = new Materials(0,0,0);
            foreach (var s in ships)
            {
                total += s.loaded; //Add all the materials from all the ships in the square
            }
            return total;
        }
        public void UpgradePort()
        {
            if (isPort)//Is the square a port
            {
                generates *= 1.5f; //Increase generates by 1.5 times its original amount
                ++level;
            }
        }
        internal void Buy(Materials materials)
        {
            Materials remainder = materials;
            foreach (var s in ships)
            {
                if (s.loaded > remainder) //Does one ship have enough cargo to pay for all the materials?
                {
                    s.loaded -= remainder; //Pay for the materials and exit
                    return;
                }
                else
                {
                    remainder -= s.loaded; //Decrement remainder by the amount loaded into the ship
                    s.loaded = new Materials(0,0,0); //Set the amount of materials in the ship to 0
                }
            }
        }

        internal static float WinningChance(Square attackingSquare, Square targetSquare)
        {
            Random r = new Random();
            List<bool> results = new List<bool>();
            for (int i = 0; i < 20; ++i) 
            {
                List<Ship> simulatedShips = new List<Ship>();
                foreach (var ship in attackingSquare.ships)
                {
                    simulatedShips.Add(ship.Copy());
                }
                foreach (var ship in targetSquare.ships)
                {
                    simulatedShips.Add(ship.Copy());
                } //Add a copy of all ships to the simulated ships list

                while (simulatedShips.Select(s => s.team).Distinct().Count() >= 2) //While there are ships to attack and ships to defend
                {
                    var teams = simulatedShips.Select(s => s.team).Distinct().ToList(); //What teams are in the square
                    foreach (var team in teams) //Iterate through the teams in the square
                    {
                        int totalCannons = simulatedShips.Where(s => s.team == team).Sum(s => s.cannons); //Find out how many total cannons their are on all of the teams ships
                        Ship ship = simulatedShips.First(p => p.team != team); //Select a ship to do damage to
                        var damage = totalCannons * r.Next(1, 7); //Calculate damage done, random number from 1 inclusive to 7 exclusive
                                                                  //We have to use current form because this is a static function
                        ship.health -= damage; //Decrement ships health
                        if (ship.health <= 0) //Did the ship die
                        {
                            simulatedShips.Remove(ship); //Remove the ship from the square
                        }
                    }
                }
                if (simulatedShips[0].team == attackingSquare.GetTeam())
                {
                    results.Add(true); //Attacking square won, so add to the results table
                }
                if (simulatedShips[0].team == targetSquare.GetTeam())
                {
                    results.Add(false); //Defending square won, so add to the results table
                }
            }
            int amountWrong = results.Where(b => b == false).Count();
            return 1f / (amountWrong+1); //Return the count of things we got wrong as a percentage
        }

        internal Team GetShipTeams()
        {
            Team result = Team.None;
            foreach (var s in ships)
            {
                result = s.team;
            }
            return result;
        }

        internal Team GetTeam()
        {
            if (isPort)
            {
                return team;
            }
            else
            {
                Team result = Team.None;
                foreach (var s in ships)
                {
                    result = s.team;
                }
                return result;
            }
        }

        internal Square Closest(List<Square> enemyports)
        {
            double smallestDistance = double.MaxValue;
            Square closestSquare = new Square(-1,-1);
            foreach (var p in enemyports)
            {
                var distance = location.DistanceTo_D(p.location);
                if (distance < smallestDistance) //Is this the closest port?
                {
                    smallestDistance = distance;
                    closestSquare = p;
                }
            }
            return closestSquare;
        }

        internal List<Ship> ShipsNearby(int distance)
        {
            if (location.X == -1 || location.Y == -1)
            {
                return new List<Ship>();
            }
            var result = new List<Ship>();
            int left4 = location.X - 4 < 0 ? 0 : location.X - 4; //The furtherest left we can go
            int down4 = location.Y - 4 < 0 ? 0 : location.Y - 4; //The furtherest right we can go

            int right4 = location.X + 4 > MainGameForm.horizontalSquares - 1 ? MainGameForm.horizontalSquares - 1 : location.X + 4; //The furtherst right we can go
            int up4 = location.Y + 4 > MainGameForm.verticalSquares ? MainGameForm.verticalSquares : location.Y + 4; //The furtherest up we can go

            for (int x = left4; x < right4; ++x) //Go from the furtherest left to the furtherest right
            {
                for (int y = down4; y < up4; ++y) //Go from the furtherest down to the furtherest up
                {
                    result.AddRange(MainGameForm.squares[x,y].ships);
                }
            }
            return result;
        }
    }
}
