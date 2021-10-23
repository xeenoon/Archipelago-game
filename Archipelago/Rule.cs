using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        public static Rule AttackEverything = new Rule(0, RuleFunctions.CanAttack, RuleFunctions.AttackFirst); //Attack ships that get too close to port
        public static Rule DefendPort = new Rule(1, RuleFunctions.IsVulnerablePort, RuleFunctions.BuildShipInPort); //Add a guard to port if an enemy is trying to attack
        public static Rule AttackPort = new Rule(2, RuleFunctions.VulnerableEnemyPort, RuleFunctions.AttackEnemyPort); //Move a ship towards enemy port if it is empty

        public static Rule BuildPort = new Rule(3, RuleFunctions.HasMaterials, RuleFunctions.BuildPort); //Build new port to gather more resources
        public static Rule UpgradePort = new Rule(4, RuleFunctions.HasPortsAndMats, RuleFunctions.UpgradePorts); //Once we have built enough ports, start upgrading the ones we have

        public static Rule PrepInvasion = new Rule(4, RuleFunctions.GeneratesEnough, RuleFunctions.Build_MOW); //Prepare invasion of enemy port
        public static Rule Invade = new Rule(4, RuleFunctions.CanInvade, RuleFunctions.Invade); //Invade enemy port
    }
    public static class RuleFunctions
    {
        static List<Square> vulnerablePorts = new List<Square>();
        static Team mostVulnerable = Team.None;
        static Square invasionPrepSquare;
        static Square invasionForceSquare;
        static Square invasionTarget;
        static bool firstCalc = true;

        static Process mlProcess = null;

        public static void OnStart()
        {
            if (mlProcess == null)
            {
                //                ProcessStartInfo info = new ProcessStartInfo(@"C:\Program Files\Archipelago\ArchipelagoML.exe");
                ProcessStartInfo info = new ProcessStartInfo(@"C:\Program Files\Archipelago\ArchipelagoML.exe")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                };

                mlProcess = Process.Start(info);
            }
        }
        public static float MLPredict(ModelInput m)
        {
            if (mlProcess == null)
            {
                return 0;
            }

            mlProcess.StandardInput.WriteLine(m.ToString());
            string output = mlProcess.StandardOutput.ReadLine();

            return float.Parse(output);

        }

        

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
                    var nearby = square.ShipsNearby(4);
                    if (nearby.Where(s=>s.team != hasTurn).Count() >= 1)
                    {
                        return true;
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
            if (generates.wood >= 500 && generates.wood < 3000) //Are we generating in excess of 1000 wood?
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
                    result.Add(new Move(s.Key, s.Key.level+1)); //Add the move to upgrade the port
                }
            }
            return result;
        }
        
        //Prep invasion functions
        public static bool GeneratesEnough()
        {
            Materials generates = new Materials(0, 0, 0);
            foreach (var s in MainGameForm.squares)
            {
                if (s.isPort && s.team == MainGameForm.hasTurn) //Is it one of our ports
                {
                    generates += s.generates; //Increment total generates
                }
            }
            if (generates.wood >= 3000) //Are we generating in excess of 3000 wood?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Move> Build_MOW()
        {
            List<Move> result = new List<Move>();
            if (firstCalc)
            {
                Materials redGenerates = new Materials(0,0,0);
                Materials greenGenerates = new Materials(0,0,0);
                Materials blueGenerates = new Materials(0,0,0);
                Materials blackGenerates = new Materials(0,0,0); //Assign generates variables

                Square redport   = new Square(-1, -1);
                Square blueport  = new Square(-1, -1);
                Square greenport = new Square(-1, -1);
                Square blackport = new Square(-1, -1);

                Square m_redport = new Square(-1, -1);
                Square m_blueport = new Square(-1, -1);
                Square m_greenport = new Square(-1, -1);
                Square m_blackport = new Square(-1, -1);

                List<Square> myports = new List<Square>();

                foreach (var square in MainGameForm.squares) //Iterate through squares
                {
                    if (square.isPort) //Is it a port?
                    {
                        if (square.team == MainGameForm.hasTurn)
                        {
                            myports.Add(square);
                        }
                    }
                }
                float redDist   = float.MaxValue;
                float greenDist = float.MaxValue;
                float blueDist  = float.MaxValue;
                float blackDist = float.MaxValue;
                foreach (var square in MainGameForm.squares) //Iterate through squares
                {
                    if (square.isPort) //Is it a port?
                    {
                        var myport = new Square(-1,-1);
                        var distance = 0f;
                        myport = square.Closest(myports); //Get the closest one of our ports to their port
                        distance = myport.location.DistanceTo_F(square.location);
                        switch (square.team)
                        {
                            case Team.Red:
                                redGenerates += square.generates;
                                if (distance < redDist)
                                {
                                    redDist = distance;
                                    redport = square;
                                    m_redport = myport;
                                } //Make distance the smallest distance if it is smaller
                                break;
                            case Team.Green:
                                greenGenerates += square.generates;
                                if (distance < greenDist)
                                {
                                    greenDist = distance;
                                    greenport = square;
                                    m_greenport = myport;
                                } //Make distance the smallest distance if it is smaller
                                break;
                            case Team.Blue:
                                blueGenerates += square.generates;
                                if (distance < blueDist)
                                {
                                    blueDist = distance;
                                    blueport = square;
                                    m_blueport = myport;
                                } //Make distance the smallest distance if it is smaller
                                break;
                            case Team.Black:
                                blackGenerates += square.generates;
                                if (distance < blackDist)
                                {
                                    blackDist = distance;
                                    blackport = square;
                                    m_blackport = myport;
                                } //Make distance the smallest distance if it is smaller
                                break; //Adjust the amount each player generates accordingly
                        }
                    }
                }
                Dictionary<Team, float> teamscores = new Dictionary<Team, float>();
                if (redport.location.X >= 0)
                {
                    List<Ship> redShips = redport.ShipsNearby(4);
                    ModelInput redData = new ModelInput()
                    {
                        Wood = redGenerates.wood, //Get reds wood
                        Distance = redDist, //get the distance between our closest port and reds port
                        _4hp = redShips.Sum(s => s.health),
                        _4can = redShips.Sum(s => s.cannons),
                    }; //Use machine learning library to create a modelinput
                    teamscores.Add(Team.Red, MLPredict(redData));
                }
                if (blueport.location.X >= 0)
                {
                    List<Ship> blueShips = blueport.ShipsNearby(4);
                    ModelInput blueData = new ModelInput()
                    {
                        Wood = blueGenerates.wood, //Get blues wood
                        Distance = blueDist, //get the distance between our closest port and blues port
                        _4hp = blueShips.Sum(s => s.health),
                        _4can = blueShips.Sum(s => s.cannons),
                    }; //Use machine learning library to create a modelinput
                    teamscores.Add(Team.Blue, MLPredict(blueData));
                }
                if (greenport.location.X >= 0)
                {
                    List<Ship> greenShips = greenport.ShipsNearby(4);
                    ModelInput greenData = new ModelInput()
                    {
                        Wood = greenGenerates.wood, //Get greens wood
                        Distance = greenDist, //get the distance between our closest port and greens port
                        _4hp = greenShips.Sum(s => s.health),
                        _4can = greenShips.Sum(s => s.cannons),
                    }; //Use machine learning library to create a modelinput
                    teamscores.Add(Team.Green, MLPredict(greenData));
                }
                if (blackport.location.X >= 0) {
                    List<Ship> blackShips = blackport.ShipsNearby(4);
                    ModelInput blackData = new ModelInput()
                    {
                        Wood = blackGenerates.wood, //Get blacks wood
                        Distance = blackDist, //get the distance between our closest port and blacks port
                        _4hp = blackShips.Sum(s => s.health),
                        _4can = blackShips.Sum(s => s.cannons),
                    }; //Use machine learning library to create a modelinput
                    teamscores.Add(Team.Black, MLPredict(blackData));
                }
                mostVulnerable = teamscores.Where(t=>t.Key != MainGameForm.hasTurn).OrderBy(t=>t.Value).FirstOrDefault().Key; //Get the most vulnerable player using the scores calculated earlier
                switch (mostVulnerable)
                {
                    case Team.Red:
                        invasionPrepSquare = m_redport;
                        invasionTarget = redport;
                        break;
                    case Team.Green:
                        invasionPrepSquare = m_greenport;
                        invasionTarget = redport; break;
                    case Team.Black:
                        invasionPrepSquare = m_blackport;
                        invasionTarget = blackport; break;
                    case Team.Blue:
                        invasionPrepSquare = m_blueport;
                        invasionTarget = blueport; break;
                }
                var invasionForceSquareLoc = invasionPrepSquare.location.MoveTowards(invasionTarget.location, 3).Round();
                invasionForceSquare = MainGameForm.squares[invasionForceSquareLoc.X, invasionForceSquareLoc.Y];
            }
            if (invasionForceSquare.ships.Count() > 20) //Do we already have 20 ships in the invasion force square?
            {
                return result; //Return
            }
            bool stop = false;
            do
            {
                Ship MOW = Ship.CreateManOWar();
                MOW.team = MainGameForm.hasTurn; //Create the ship and put it on our team
                if (MOW.required < MainGameForm.teamMaterials.GetMaterials(MainGameForm.hasTurn)) //Can we afford it?
                {
                    MainGameForm.teamMaterials.Pay(MainGameForm.hasTurn, MOW.required); //Pay for it
                    invasionForceSquare.ships.Add(MOW); //Put it in the square
                }
                else
                {
                    stop = true; //We cannot afford this one or any more ones, so exit the loop
                }
            } while (!stop);
            return result;
        }

        //Invade functions
        public static bool CanInvade()
        {
            if (invasionForceSquare == null)
            {
                return false;
            }
            return invasionForceSquare.ships.Count() >= 20;
        }
        public static List<Move> Invade()
        {
            List<Move> result = new List<Move>();
            var targetLocation = invasionForceSquare.location.MoveTowards(invasionTarget.location, 3).Round(); //Move towards the target port
            var ships = new List<Ship>();
            foreach (var s in invasionForceSquare.ships)
            {
                ships.Add(s); //Copy the shiplist to stop reference errors in Move.DoMove()
            }

            result.Add(new Move(ships, invasionForceSquare.location, targetLocation)); //Add the move
            invasionForceSquare = MainGameForm.squares[targetLocation.X, targetLocation.Y]; //Reassign invasion force square to the square the invasion force is now in
            if (invasionForceSquare == invasionTarget) //Have we successfully invaded the target square
            {
                mostVulnerable = Team.None;
                invasionPrepSquare = null;
                invasionForceSquare = null;
                invasionTarget = null;
                firstCalc = true; //Reset all the variables to do the calculation again
            }
            return result;
        }

    }
    public class ModelInput
    {
        public int Wood;
        public float Distance;
        public int _4hp;
        public int _4can;

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", Wood, Distance, _4hp, _4can);
        }
    }
}
