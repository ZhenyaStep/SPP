using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SPP_lab_4
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("not enough data");
                return 1;
            }

            string filePath = args[0];
           // string filePath = "C:\\Users\\zheny\\source\\repos\\SPP\\SPP_lab_4\\SPP_lab_4\\bin\\Debug\\ClassLibrary1.dll";
            var assembly = Assembly.LoadFile(filePath);
            //Type[] types = assembly.GetTypes();

            foreach (Type type in assembly.ExportedTypes)
            {
                Console.WriteLine(type.ToString());

                /*foreach (FieldInfo typeField in type.GetFields())
                {
                    if (typeField.IsPublic)
                    {
                        Console.WriteLine(typeField.ToString());
                    }
                }*/
            } 


            //Console.WriteLine(assembly.GetType("a").ToString());
            //for (int i = 0; i < types.Length; i++)
            //{
            //    Console.WriteLine(types.ToString());
            //}

            Console.ReadKey();

            return 0;
        }
    }
}
