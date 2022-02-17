using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Universal.Sound
{
    
    internal static partial class StaticFunctions
    {
        // Tested
        public static void PrintArray<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Debug.Log(item + "");
            }
        }

        // Tested
        // Stuffle a ienumerable
        internal static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            // Create a random number generator
            System.Random rnd = new System.Random(DateTime.Now.Millisecond);
            return collection
                .Select(item => new { item, order = rnd.Next() })
                .OrderBy(x => x.order)
                .Select(x => x.item);
        }

        // Tested
        internal static Queue<T> ToQueue<T>(this IEnumerable<T> collection)
        {
            // Create the queue
            var queue = new Queue<T>();

            // Create a random number generator
            collection
                .Select(x => { queue.Enqueue(x); return true; })
                .ToArray();

            return queue;
        }

        // Tested
        internal static Stack<T> ToStack<T>(this IEnumerable<T> collection)
        {
            // Create the queue
            var stack = new Stack<T>();

            // Create a random number generator
            collection
                .Select(x => { stack.Push(x); return true; })
                .ToArray();

            return stack;
        }

        // Tested
        // Recorre una colo x posiciones, poniendo los que quita al inicio
        internal static Queue<T> Jump<T>(this Queue<T> queue, int positions)
        {

            // Create the queue
            var outQueue = new Queue<T>(queue);

            // Create a random number generator
            for (int i = 0; i < positions; i++)
            {
                if (outQueue.Count() < 1)
                    break;

                var bk = outQueue.Dequeue();
                outQueue.Enqueue(bk);
            }

            return outQueue;
        }







        // Tested
        internal static int RandomInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        // Tested
        public static float RandomFloat(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }


        // Tested
        internal static float MinMaxFloat(this float num, float min, float max)
        {
            // if min and max are inverted
            if (max < min)
            {
                var v = min;
                min = max;
                max = v;
            }

            if (num < min) return min;
            else if (num > max) return max;
            else return num;
        }

        internal static int MinMaxInt(this int num, int min, int max)
        {
            // if min and max are inverted
            if (max < min)
            {
                var v = min;
                min = max;
                max = v;
            }

            if (num < min) return min;
            else if (num > max) return max;
            else return num;
        }

        // Tested
        public static int MinInt(this int num, int min)
        {
            if (num < min) return min;
            else return num;
        }


        public static float MinFloat(this float num, float min)
        {
            if (num < min) return min;
            else return num;
        }


    }
}
