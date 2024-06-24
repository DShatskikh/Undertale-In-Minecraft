using System.Collections.Generic;
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
    }
}