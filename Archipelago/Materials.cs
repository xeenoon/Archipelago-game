using System;
using System.Windows.Forms;

namespace Archipelago
{
    public class Materials
    {
        public int wood;
        public int metal;
        public int cloth;

        public Materials(int wood, int metal, int cloth)
        {
            this.wood = wood;
            this.metal = metal;
            this.cloth = cloth;
        }

        public static Materials Generate(Ship.ShipType shipType, int cannons, int health)
        {
            int cloth=0;
            switch (shipType)
            {
                case Ship.ShipType.Heavy:
                    cloth = 3;
                    break;
                case Ship.ShipType.Medium:
                    cloth = 1;
                    break;
                case Ship.ShipType.Fast:
                    cloth = 3;
                    break;
                case Ship.ShipType.VeryFast:
                    cloth = 6;
                    break;
            }//Different ship types require different amounts of cloth to be produced
            return new Materials(HealthToWood(health), cannons, cloth);
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", wood, metal, cloth);
        }
        internal int GetWeight()
        {
            return wood + metal*2 + cloth;
        }

        public static int HealthToWood(int health)
        {
            //A decremental formula where the first 100 hp costs 100 wood, the next 100 hp costs 75 wood...
            int result = 0;
            if (health <= 100)
            {
                return health;
            }
            if (health > 100)
            {
                result += 100;
                if (health < 200)
                {
                    result += (health - 50);
                }
            }
            if (health > 200)
            {
                result += 75;
                if (health < 400)
                {
                    result += (int)((health - 175)*0.75f);
                }
            }
            if (health > 400)
            {
                result += 120;
                if (health < 800)
                {
                    result += (int)((health - 295) * 0.60f);
                }
            }
            if (health > 800)
            {
                result += 200;
                if (health < 1501)
                {
                    result += (int)((health - 495) * 0.50f);
                }
            }
            if (health > 1501)
            {
                result += 280;
                if (health < 2500)
                {
                    result += (int)((health - 775) * 0.40f);
                }
            }

            return result;
        }

        internal static bool Canbuild(Materials materials, Materials required)
        {
            if (materials.wood >= required.wood && materials.metal >= required.metal && materials.cloth >= required.cloth) //Do we have enough materials to build the ship?
            {
                return true;
            }
            return false;
        }

        internal Materials Pay(Materials required)
        {
            int w = wood - required.wood;
            int c = cloth - required.cloth;
            int m = metal - required.metal;
            return new Materials(w,m,c); //Use local materials to pay for ports or other items
        }

        public static Materials operator +(Materials a, Materials b)
        {
            return new Materials(a.wood+b.wood, a.metal+b.metal,a.cloth+b.cloth);
        }
        public static Materials operator -(Materials a, Materials b)
        {
            return new Materials(a.wood - b.wood, a.metal - b.metal, a.cloth - b.cloth);
        }

        public static Materials operator *(Materials a, float b)
        {
            return new Materials((int)Math.Floor(a.wood * b), (int)Math.Floor(a.metal * b), (int)Math.Floor(a.cloth * b));
        }
        public static Materials operator /(Materials a, float b)
        {
            return new Materials((int)Math.Floor(a.wood / b), (int)Math.Floor(a.metal / b), (int)Math.Floor(a.cloth / b));
        }

        public static bool operator >(Materials a, Materials b)
        {
            return a.wood >= b.wood && a.metal >= b.metal && a.cloth >= b.cloth;
        }
        public static bool operator <(Materials a, Materials b)
        {
            return a.wood <= b.wood && a.metal <= b.metal && a.cloth <= b.cloth;
        }
        /**
         * Using operators to allow comparisons, decrementation and incrementation of materials
         * */
    }
    public class TeamMaterials //Stores the different materials for different teams
    {
        Label WoodResourceLabel; 
        Label MetalResourceLabel;
        Label ClothResourceLabel; //Labels to update with material data


        Materials redMATS = new Materials(10000, 500, 50);
        public Materials redMaterials
        {
            get
            {
                return redMATS;
            }
            set
            {
                redMATS = value;
                WoodResourceLabel.Text = "Wood: " + redMATS.wood.ToString();
                MetalResourceLabel.Text = "Metal: " + redMATS.metal.ToString();
                ClothResourceLabel.Text = "Cloth: " + redMATS.cloth.ToString();
            }
        }

        Materials greenMATS = new Materials(10000, 500, 50);
        public Materials greenMaterials
        {
            get
            {
                return greenMATS;
            }
            set
            {
                greenMATS = value;
                WoodResourceLabel.Text = "Wood: " + greenMATS.wood.ToString();
                MetalResourceLabel.Text = "Metal: " + greenMATS.metal.ToString();
                ClothResourceLabel.Text = "Cloth: " + greenMATS.cloth.ToString();
            }
        }

        Materials blackMATS = new Materials(10000, 500, 50);
        public Materials blackMaterials
        {
            get
            {
                return blackMATS;
            }
            set
            {
                blackMATS = value;
                WoodResourceLabel.Text = "Wood: " + blackMATS.wood.ToString();
                MetalResourceLabel.Text = "Metal: " + blackMATS.metal.ToString();
                ClothResourceLabel.Text = "Cloth: " + blackMATS.cloth.ToString();
            }
        }

        Materials blueMATS = new Materials(10000, 500, 50);
        public Materials blueMaterials
        {
            get
            {
                return blueMATS;
            }
            set
            {
                blueMATS = value;
                WoodResourceLabel.Text = "Wood: " + blueMATS.wood.ToString();
                MetalResourceLabel.Text = "Metal: " + blueMATS.metal.ToString();
                ClothResourceLabel.Text = "Cloth: " + blueMATS.cloth.ToString();
            }
        }

        /*
         * These functions allow an outside class to get and set the amount of materials a team has
         * When changing the amount of materials, the labels are automatically updated
         * */

        public TeamMaterials(Label woodResourceLabel,
                             Label metalResourceLabel,
                             Label clothResourceLabel,
                             /////////////////////////
                             Materials redMaterials,
                             Materials greenMaterials,
                             Materials blackMaterials,
                             Materials blueMaterials)
        {
            WoodResourceLabel = woodResourceLabel;
            MetalResourceLabel = metalResourceLabel;
            ClothResourceLabel = clothResourceLabel;
            ///////////////////////////////////
            this.redMaterials = redMaterials;
            this.greenMaterials = greenMaterials;
            this.blackMaterials = blackMaterials;
            this.blueMaterials = blueMaterials;
        } //Constructor

        internal Materials GetMaterials(Team hasTurn)
        {
            switch (hasTurn)
            {
                case Team.Red:
                    return redMaterials;
                case Team.Green:
                    return greenMaterials;
                case Team.Black:
                    return blackMaterials;
                case Team.Blue:
                    return blueMaterials;
            }//Return the materials for the team inputted
            throw new Exception("Team cannot be none");
        }

        internal void Pay(Team hasTurn, Materials required)
        {
            switch (hasTurn)
            {
                case Team.Red:
                    redMaterials = redMaterials.Pay(required);
                    break;
                case Team.Green:
                    greenMaterials = greenMaterials.Pay(required);
                    break;
                case Team.Black:
                    blackMaterials = blackMaterials.Pay(required);
                    break;
                case Team.Blue:
                    blueMaterials = blueMaterials.Pay(required);
                    break;
            } //Have a specific team pay an amount of materials
        }


        internal void Show(Team hasTurn)
        {
            switch (hasTurn)
            {
                case Team.Red:
                    WoodResourceLabel.Text = "Wood: " + redMATS.wood.ToString();
                    MetalResourceLabel.Text = "Metal: " + redMATS.metal.ToString();
                    ClothResourceLabel.Text = "Cloth: " + redMATS.cloth.ToString();
                    break;
                case Team.Green:
                    WoodResourceLabel.Text = "Wood: " + greenMATS.wood.ToString();
                    MetalResourceLabel.Text = "Metal: " + greenMATS.metal.ToString();
                    ClothResourceLabel.Text = "Cloth: " + greenMATS.cloth.ToString();
                    break;
                case Team.Black:
                    WoodResourceLabel.Text = "Wood: " + blackMATS.wood.ToString();
                    MetalResourceLabel.Text = "Metal: " + blackMATS.metal.ToString();
                    ClothResourceLabel.Text = "Cloth: " + blackMATS.cloth.ToString();
                    break;
                case Team.Blue:
                    WoodResourceLabel.Text = "Wood: " + blueMATS.wood.ToString();
                    MetalResourceLabel.Text = "Metal: " + blueMATS.metal.ToString();
                    ClothResourceLabel.Text = "Cloth: " + blueMATS.cloth.ToString();
                    break;
            } //Update the labels for a specific team   
        }

        internal void AddMaterials(Team hasTurn, Materials total)
        {
            switch (hasTurn)
            {
                case Team.Red:
                    redMaterials += total;
                    break;
                case Team.Green:
                    greenMaterials += total;
                    break;
                case Team.Black:
                    blackMaterials += total;
                    break;
                case Team.Blue:
                    blueMaterials += total;
                    break;
            } //Add materials to a team
        }

        internal static Materials Parse(string v)
        {
            var strs = v.Split(',');
            return new Materials(int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2]));
        }
    }

}