using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago
{
    public static class Extensions
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            Random r = new Random();
            Dictionary<int, T> keyValuePairs = new Dictionary<int, T>(); //A dictionary to store values in
            foreach (var t in list)
            {
                int next = 0;
                do
                {
                    next = r.Next();
                } while (keyValuePairs.ContainsKey(next)); //Generate a random value that is not already in the dictionary
                keyValuePairs.Add(next, t); //Add that value to the type of thing in the list
            }
            return keyValuePairs.OrderBy(s => s.Key).Select(p => p.Value).ToList(); //Order the list by the randomized values inputted. This will shuffle the list
        }
        public static void Consolidate(this List<Move> moves)
        {
            List<Move> removeThis = new List<Move>();//The moves to be removed from the list
            List<Move> addThis = new List<Move>();//The moves to be added to the list
            foreach (var move in moves)
            {
                var same = moves.Where(m => m.currentLocation == move.currentLocation && m.destination == move.destination); //Find all moves with the same current location and the same final destination
                if (same.Count() >= 2) //Are the other moves that are the same? Using the number two so that we do not include the current move
                {
                    removeThis.AddRange(same); //Add these ships to the list of ships to remove
                    List<Ship> allMovingShips = same.Select(s=>s.toMove[0]).ToList(); //Add the first ship in the moving array for each 'same' ship
                    addThis.Add(new Move(allMovingShips, move.currentLocation, move.destination)); //Add the aggregate move
                }
            }
            foreach (var move in removeThis)
            {
                moves.Remove(move);
            }//Remove all the moves that are suposed to be removed
            moves.AddRange(addThis); //Add the moves that are supposed to be added
            //Since this is an extension method this will modify the 'moves' being used externally
        }

        public static PointF MoveTowards(this Point origin, Point dest, float distance) //Moves from one point to another for a distance
        {
            var vector = new PointF(dest.X - origin.X, dest.Y - origin.Y); //Calculate vector
            var length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y); //Determine length
            var unitVector = new PointF(vector.X / (float)length, vector.Y / (float)length); //relative vector
            PointF result = new PointF((float)(origin.X + unitVector.X * distance), (float)(origin.Y + unitVector.Y * distance)); //Result point
            double xdiff = Math.Abs(dest.X - origin.X);
            double ydiff = Math.Abs(dest.Y - origin.Y);
            if (xdiff <= distance / 2)
            {
                result.X = dest.X;
            }
            if (ydiff <= distance / 2)
            {
                result.Y = dest.Y;
            }//Settle differences
            return result;
        }

        public static double DistanceTo_D(this Point origin, Point dest)
        {
            return Math.Sqrt(Math.Pow(origin.X - dest.X, 2) + Math.Pow(origin.Y - dest.Y, 2));
        }
        public static float DistanceTo_F(this Point origin, Point dest)
        {
            return (float)Math.Sqrt(Math.Pow(origin.X - dest.X, 2) + Math.Pow(origin.Y - dest.Y, 2));
        }

        public static Point Round(this PointF pos)
        {
            return new Point((int)pos.X, (int)pos.Y);
        }

        public static T RandomItem<T>(this List<T> list)
        {
            Random r = new Random();
            return list[r.Next(0,list.Count()-1)];
        }
    }
}
