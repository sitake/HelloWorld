using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            String str = Console.ReadLine();
            Console.WriteLine(str);

            List<int> list = new List<int> {1,5,3,2,4 };
            list.Sort();
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
            list.Reverse();
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }


        }
    }
}
