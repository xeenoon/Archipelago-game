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
        public int PortLevel { get; set; } = 0;
        public List<Ship> ships = new List<Ship>();
        public Point location;
        public Team team = Team.None;
        public Square(Point location)
        {
            this.location = location;
        }

        public Square(int x, int y)
        {
            this.location = new Point(x,y);
        }

        public Materials generates;
    }
}
