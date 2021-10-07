using System;
using System.Collections.Generic;
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

        /// <summary>
        /// used to bring back health
        /// </summary>
        public static void Repair()
        {

        }


        private Ship(ShipType shipType, int cannons, int health, string name)
        {
            this.shipType = shipType;
            this.cannons = cannons;
            this.health = health;
            this.name = name;
            required = Materials.Generate(shipType, cannons, health);
        }
        public static Ship Create(string shipType)
        {
            switch (shipType)
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
    }
}