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
    }
}
