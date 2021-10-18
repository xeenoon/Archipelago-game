using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archipelago
{
    public enum Team
    {
        None,
        Red,
        Green,
        Black,
        Blue,
        Pirate,
    }
    public class Ship
    {
        public enum ShipType
        {
            None,
            Heavy,
            Medium,
            Fast,
            VeryFast
        }
        public ShipType shipType;
        public int cannons;
        public int health;
        public string name;
        public bool moveNext;
        public Team team = Team.None;

        public Materials required;

        public bool hasMoved;
        public int cargoCapacity;
        public Materials loaded = new Materials(0, 0, 0);

        public bool LoadMaterials(Materials m)
        {
            if ((m.GetWeight() + loaded.GetWeight()) <= cargoCapacity) //Is there capacity for the materials to be loaded
            {
                loaded += m; //Add the materials
                return true; //The materials have been successfully loaded
            }
            else
            {
                return false; //Materials cannot be loaded
            }
        }

        /// <summary>
        /// used to bring back health
        /// </summary>
        public static void Repair()
        {
            //Does the player have enough materials
            //Pay for materials using the HealthToWood();
            //Replenish health
        }
        public static void Setup()
        {
            for (int i = 1; i < 25; ++i)
            {
                switch (i)
                {
                    case 1:
                        AllShips.Add(CreateBrig());
                        break;
                    case 2:
                        AllShips.Add(CreateRigger());
                        break;
                    case 3:
                        AllShips.Add(CreateCarrack());
                        break;
                    case 4:
                        AllShips.Add(CreateGalleon());
                        break;
                    case 5:
                        AllShips.Add(Create4thRate());
                        break;
                    case 6:
                        AllShips.Add(Create3rdRate());
                        break;
                    case 7:
                        AllShips.Add(Create2ndRate());
                        break;
                    case 8:
                        AllShips.Add(Create1stRate());
                        break;
                    case 9:
                        AllShips.Add(CreateSloop());
                        break;
                    case 10:
                        AllShips.Add(CreateSchooner());
                        break;
                    case 11:
                        AllShips.Add(CreateCutter());
                        break;
                    case 12:
                        AllShips.Add(CreateKetch());
                        break;
                    case 13:
                        AllShips.Add(CreatePinnance());
                        break;
                    case 14:
                        AllShips.Add(CreateSloopOfWar());
                        break;
                    case 15:
                        AllShips.Add(CreateSnow());
                        break;
                    case 16:
                        AllShips.Add(CreateWarGalleon());
                        break;
                    case 17:
                        AllShips.Add(CreateBrigantine());
                        break;
                    case 18:
                        AllShips.Add(CreateFrigate());
                        break;
                    case 19:
                        AllShips.Add(CreateGalley());
                        break;
                    case 20:
                        AllShips.Add(CreateCorvette());
                        break;
                    case 21:
                        AllShips.Add(CreateXebec());
                        break;
                    case 22:
                        AllShips.Add(CreateManOWar());
                        break;
                    case 23:
                        AllShips.Add(CreateSteamCorvette());
                        break;
                    case 24:
                        AllShips.Add(CreateClipper());
                        break;
                } //Add all possible ship types to the list of ships
            }
        }

        internal Ship Copy()
        {
            return new Ship(shipType, cannons, health, name) {team = team};
        }

        private Ship(ShipType shipType, int cannons, int health, string name)
        {
            this.shipType = shipType;
            this.cannons = cannons;
            this.health = health;
            this.name = name;
            required = Materials.Generate(shipType, cannons, health);
            cargoCapacity = health / 2; //Cargo capacity does not dimish with ship health
        }

        internal static Ship BuildShipInBudget(Materials materials, ShipType shipType = ShipType.None)
        {
            if (CreateSloop().required > materials)
            {
                return null;
            }

            Ship result;
            do
            {
                result = RandomShip();
            } while (result.required > materials && (shipType == ShipType.None || result.shipType == shipType)); //Do we have the required materials and does the ship fir the specifications
            return result;
        }
        static List<Ship> AllShips = new List<Ship>();
        public bool canAttack=true;

        internal static Ship BuildBiggestShipInBudget(Materials materials, ShipType shipType)
        {
            var fittingShips = AllShips.Where(s=>s.shipType == shipType).OrderByDescending(m=>m.required.wood).ToList(); //Get all ships of my shiptype, then order them so that the most expensive ones are at the top
            foreach (var ship in fittingShips)
            {
                if (ship.required < materials) //Can we afford to build it
                {
                    return ship; //This is the most expensive ship, return it
                }
            }
            return null; //We cannot build a ship with our current resources
        }
        internal static Ship BuildFastestShipInBudget(Materials materials)
        {
            var fittingShips = AllShips.OrderByDescending(s=>s.shipType).ThenBy(s=>s.required.wood).ToList(); //Order ships so that the fastest ships are at the top
            foreach (var ship in fittingShips)
            {
                if (ship.required < materials) //Can we afford to build it
                {
                    return ship; //This is fastest, most expensive ship, return it
                }
            }
            return null; //We cannot build a ship with our current resources
        }
        public static Ship Create(string shipName)
        {
            switch (shipName)
            {
                case "Brig":
                    return CreateBrig();
                case "Rigger":
                    return CreateRigger();
                case "Carrack":
                    return CreateCarrack();
                case "Galleon":
                    return CreateGalleon();
                case "4th rate":
                    return Create4thRate();
                case "3rd rate":
                    return Create3rdRate();
                case "2nd rate":
                    return Create2ndRate();
                case "1st rate":
                    return Create1stRate();
                case "Sloop":
                    return CreateSloop();
                case "Schooner":
                    return CreateSchooner();
                case "Cutter":
                    return CreateCutter();
                case "Ketch":
                    return CreateKetch();
                case "Pinnance":
                    return CreatePinnance();
                case "Sloop of war":
                    return CreateSloopOfWar();
                case "Snow":
                    return CreateSnow();
                case "War Galleon":
                    return CreateWarGalleon();
                case "Brigantine":
                    return CreateBrigantine();
                case "Frigate":
                    return CreateFrigate();
                case "Galley":
                    return CreateGalley();
                case "Corvette":
                    return CreateCorvette();
                case "Xebec":
                    return CreateXebec();
                case "Man O War":
                    return CreateManOWar();
                case "Steam Corvette":
                    return CreateSteamCorvette();
                case "Clipper":
                    return CreateClipper();
            }
            return null;
        }
        //      Heavy       //
        public static Ship CreateBrig()
        {
            return new Ship(ShipType.Heavy, 28,250,"Brig");
        }
        public static Ship CreateRigger()
        {
            return new Ship(ShipType.Heavy, 30,300,"Rigger");
        }
        public static Ship CreateCarrack()
        {
            return new Ship(ShipType.Heavy, 40, 500, "Carrack");
        }
        public static Ship CreateGalleon()
        {
            return new Ship(ShipType.Heavy, 42, 500, "Galleon");
        }
        public static Ship Create4thRate()
        {
            return new Ship(ShipType.Heavy, 50, 750, "4th rate");
        }
        public static Ship Create3rdRate()
        {
            return new Ship(ShipType.Heavy, 75, 1000, "3rd rate");
        }
        public static Ship Create2ndRate()
        {
            return new Ship(ShipType.Heavy, 100, 1500, "2nd rate");
        }

        public static Ship Create1stRate()
        {
            return new Ship(ShipType.Heavy, 120, 2000, "1st rate");
        }
        //      Heavy       //

        //     Medium       //
        public static Ship CreateSloop()
        {
            return new Ship(ShipType.Medium, 10, 30, "Sloop");
        }
        public static Ship CreateSchooner()
        {
            return new Ship(ShipType.Medium, 12, 50, "Schooner");
        }
        public static Ship CreateCutter()
        {
            return new Ship(ShipType.Medium, 12, 40, "Cutter");
        }
        public static Ship CreateKetch()
        {
            return new Ship(ShipType.Medium, 12, 45, "Ketch");
        }
        public static Ship CreatePinnance()
        {
            return new Ship(ShipType.Medium, 20, 100, "Pinnance");
        }
        public static Ship CreateSloopOfWar()
        {
            return new Ship(ShipType.Medium, 18, 100, "Sloop of war");
        }
        public static Ship CreateSnow()
        {
            return new Ship(ShipType.Medium, 30, 250, "Snow");
        }
        public static Ship CreateWarGalleon()
        {
            return new Ship(ShipType.Medium, 60, 750, "War Galleon");
        }
        //     Medium       //

        //       Fast       //
        public static Ship CreateBrigantine()
        {
            return new Ship(ShipType.Fast, 20, 100, "Brigantine");
        }
        public static Ship CreateFrigate()
        {
            return new Ship(ShipType.Fast, 30, 250, "Frigate");
        }
        public static Ship CreateGalley()
        {
            return new Ship(ShipType.Fast, 30, 300, "Galley");
        }
        public static Ship CreateCorvette()
        {
            return new Ship(ShipType.Fast, 20, 145, "Corvette");
        }
        public static Ship CreateXebec()
        {
            return new Ship(ShipType.Fast, 24, 200, "Xebec");
        }
        public static Ship CreateManOWar()
        {
            return new Ship(ShipType.Fast, 50, 750, "Man O War");
        }
        //       Fast       //

        //    Very Fast     //
        public static Ship CreateSteamCorvette()
        {
            return new Ship(ShipType.VeryFast, 20, 150, "Steam corvette");
        }
        public static Ship CreateClipper()
        {
            return new Ship(ShipType.VeryFast, 15, 75, "Clipper");
        }
        //    Very Fast     //
        internal static Ship RandomShip()
        {
            Random r = new Random();
            switch (r.Next(1,25))
            {
                case 1:
                    return CreateBrig();
                case 2:
                    return CreateRigger();
                case 3:
                    return CreateCarrack();
                case 4:
                    return CreateGalleon();
                case 5:
                    return Create4thRate();
                case 6:
                    return Create3rdRate();
                case 7:
                    return Create2ndRate();
                case 8:
                    return Create1stRate();
                case 9:
                    return CreateSloop();
                case 10:
                    return CreateSchooner();
                case 11:
                    return CreateCutter();
                case 12:
                    return CreateKetch();
                case 13:
                    return CreatePinnance();
                case 14:
                    return CreateSloopOfWar();
                case 15:
                    return CreateSnow();
                case 16:
                    return CreateWarGalleon();
                case 17:
                    return CreateBrigantine();
                case 18:
                    return CreateFrigate();
                case 19:
                    return CreateGalley();
                case 20:
                    return CreateCorvette();
                case 21:
                    return CreateXebec();
                case 22:
                    return CreateManOWar();
                case 23:
                    return CreateSteamCorvette();
                case 24:
                    return CreateClipper();
                default:
                    throw new Exception("Random value exceeded boundries");
            }
        }

        internal static List<Point> Destinations(ShipType shipType, Point location)
        {
            List<Point> locations = new List<Point>();
            for (int x = -(int)shipType; x <= (int)shipType && x < 27; ++x)
            {
                for (int y = -(int)shipType; y <= (int)shipType && y < 20; ++y)
                {
                    locations.Add( new Point(location.X + x,location.Y + y));
                }
            }
            locations.RemoveAll(p=>p.X <0 || p.Y < 0 || p.X >= 27 || p.Y >= 20);
            return locations;
        }
    }
}