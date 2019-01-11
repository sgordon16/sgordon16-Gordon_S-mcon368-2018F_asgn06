using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCON_368_Asgn05
{
    class Program
    {
        static void Main(string[] args)
        {
            var vals = new int[] { 5, 9, 4, 7, 15, 16, 10, 11, 14, 19, 18 };
            var result1 = vals.MaxOverPrevious();
            result1.ToList().ForEach(i => Console.Write(i + ", "));
            Console.WriteLine();

            var result2 = vals.LocalMaxima();
            result2.ToList().ForEach(i => Console.Write(i + ", "));
            Console.WriteLine();

            var result3 = vals.AtLeastK(3, i => i >= 16 && i < 20);
            Console.WriteLine(result3);

            var result4 = vals.AtLeastHalf(i => i >= 16 && i < 20);
            Console.WriteLine(result4);

            var result5 = vals.MaxOverPrevious(i => i % 2 == 0 ? 2 : 3);
            Console.WriteLine(result4);

            var result6 = vals.LocalMaxima(i => i / 2 + 7);
            Console.ReadKey();
        }
        
    }
    static class ExtensionMethods
    {
        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> o) where T : IComparable<T>
        {
            bool max = true;
            //saved some linear time operations by using the Count method only once
            //I had to use the count method in my for loop so I used that approach 
            //as opposed to keeping track of the elements from every iteration
            int size = o.Count();
            for (int x = 0; x < size; x++)
            {
                max = true;
                for (int y = 0; y < x; y++)
                {
                    if (o.ElementAt(x).CompareTo(o.ElementAt(y)) < 0)
                    {
                        max = false;
                        break;
                    }
                }
                if (max)
                {
                    //uses deferred execution
                    yield return o.ElementAt(x);
                }
            }
        }
        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> o, Func<T, IComparable> func)
        {
            bool max = true;
            //saved some linear time operations by using the Count method only once
            //I had to use the count method in my for loop so I used that approach 
            //as opposed to keeping track of the elements from every iteration
            int size = o.Count();
            for (int x = 0; x < size; x++)
            {
                max = true;
                for (int y = 0; y < x; y++)
                {
                    if (func(o.ElementAt(x)).CompareTo(o.ElementAt(y)) < 0)
                    {
                        max = false;
                        break;
                    }
                }
                if (max)
                {
                    //uses deferred execution
                    yield return o.ElementAt(x);
                }
            }
        }
        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> o) where  T : IComparable<T>
        {
            //saved some linear time operations by using the Count method only once
            //I had to use the count method in my for loop so I used that approach 
            //as opposed to keeping track of the elements from every iteration
            int size = o.Count();
            for (int i = 0; i < size; i++)
            {
                if(i == 0)
                {
                    if (o.ElementAt(i).CompareTo(o.ElementAt(i + 1)) > 0)
                        //uses deferred execution
                        yield return o.ElementAt(i);
                }
                else if(i == size - 1)
                {
                    if (o.ElementAt(i).CompareTo(o.ElementAt(i - 1)) > 0)
                        //uses deferred execution
                        yield return o.ElementAt(i);
                }
                else if(o.ElementAt(i).CompareTo(o.ElementAt(i - 1)) > 0 && o.ElementAt(i).CompareTo(o.ElementAt(i + 1)) > 0)
                {
                    //uses deferred execution
                    yield return o.ElementAt(i);
                }
            }
        }
        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> o, Func<T, IComparable> func)
        {
            //saved some linear time operations by using the Count method only once
            //I had to use the count method in my for loop so I used that approach 
            //as opposed to keeping track of the elements from every iteration
            int size = o.Count();
            for (int i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    if (func(o.ElementAt(i)).CompareTo(func(o.ElementAt(i + 1))) > 0)
                        //uses deferred execution
                        yield return o.ElementAt(i);
                }
                else if (i == size - 1)
                {
                    if (func(o.ElementAt(i)).CompareTo(o.ElementAt(i - 1)) > 0)
                        //uses deferred execution
                        yield return o.ElementAt(i);
                }
                else if (func(o.ElementAt(i)).CompareTo(func(o.ElementAt(i - 1))) > 0 && 
                    func(o.ElementAt(i)).CompareTo(func(o.ElementAt(i + 1))) > 0)
                {
                    //uses deferred execution
                    yield return o.ElementAt(i);
                }
            }
        }
        public static bool AtLeastK<T>(this IEnumerable<T> o, int k, Func<T, bool> func)
        {
            //Differed execution not possible
            int count = 0;
            foreach(T t in o)
            {
                if (func(t))
                    count++;
            }
            return (count >= k);
        }
        public static bool AtLeastHalf<T>(this IEnumerable<T> o, Func<T, bool> func)
        {
            //Differed execution not possible
            int size = 0;
            int count = 0;
            foreach (T t in o)
            {
                size++;
                if (func(t))
                    count++;
            }
            return count >= (size/2);
        }
    }
}
