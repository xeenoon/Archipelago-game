using System;
using System.Collections.Generic;
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
    }
}
