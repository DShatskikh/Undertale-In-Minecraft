using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Super_Auto_Mobs
{
    public static class RandomExtensions
    {
        public static List<T> GetRandomList<T>(List<T> list, int count)
        {
            var startList = new List<T>(list);
            var newList = new List<T>();

            while (newList.Count < count)
            {
                if (startList.Count > 0)
                {
                    int numberElement = Random.Range(0, startList.Count);
                    var element = startList[numberElement];
                    newList.Add(element);
                    startList.Remove(element); 
                }
                else
                {
                    newList.Add(list[Random.Range(0, list.Count)]);
                }
            }

            return newList;
        }
        
        public static T GetRandomElement<T>(List<T> list)
        {
            int numberElement = Random.Range(0, list.Count);
            var element = list[numberElement];

            return element;
        }
        
        public static string GetRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[Random.Range(0, chars.Length)]).ToArray());
        }
    }
}