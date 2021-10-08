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
    }
}
