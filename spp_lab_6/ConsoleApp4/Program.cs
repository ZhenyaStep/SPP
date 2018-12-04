using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Type[] Types = { typeof(int),
                             typeof(string),
                             typeof(double) ,
                           };

            //Type T = Types[0];
            //DynamicList<T> Dlist = new DynamicList<T>();
            DynamicList<int> Dlist_int = new DynamicList<int>();

            Dlist_int.Add(0);
            Dlist_int.Add(1);
            Dlist_int.Add(2);
            Dlist_int.Add(3);
            Dlist_int.Add(4);


            
            foreach (int num in Dlist_int)
            {
                Console.WriteLine(num);
            }


            Console.WriteLine("\n");
            Console.WriteLine(Dlist_int[1]);
            Console.WriteLine(Dlist_int[0]);

            Dlist_int.Remove(1);
            Console.WriteLine(Dlist_int[1]);

            Dlist_int.RemoveAt(0);
            Console.WriteLine(Dlist_int[0]);

            Dlist_int.Clear();
            if (Dlist_int.Count == 0)
            {
                Console.WriteLine("array is empty");
            } else
            {
                Console.WriteLine(Dlist_int[0]);
            }
            
            foreach (int num in Dlist_int)
            {
                Console.WriteLine(num);
            }

            Console.ReadKey();


            ///////////////////////////////////////////////////

            DynamicList<string> Dlist_str = new DynamicList<string>();

            Dlist_str.Add("hello");
            Dlist_str.Add("bye");
            Dlist_str.Add("yellow");
            Dlist_str.Add("green");
            Dlist_str.Add("red");



            foreach (String str in Dlist_str)
            {
                Console.WriteLine(str);
            }


            Console.WriteLine("\n");
            Console.WriteLine(Dlist_str[1]);
            Console.WriteLine(Dlist_str[0]);

            Dlist_str.Remove("bye");
            Console.WriteLine(Dlist_str[1]);

            Dlist_str.RemoveAt(0);
            Console.WriteLine(Dlist_str[0]);

            Dlist_str.Clear();
            if (Dlist_str.Count == 0)
            {
                Console.WriteLine("array is empty");
            }
            else
            {
                Console.WriteLine(Dlist_str[0]);
            }

            foreach (String str in Dlist_str)
            {
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }

    }
}