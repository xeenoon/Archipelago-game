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
        public bool isPort;
        public List<Ship> ships = new List<Ship>();
        public Point location;
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
                total += s.loaded;
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
                if (s.loaded > remainder)
                {
                    s.loaded -= remainder;
                }
                else
                {
                    remainder -= s.loaded;
                    s.loaded = new Materials(0,0,0);
                }
            }
        }
    }
}
