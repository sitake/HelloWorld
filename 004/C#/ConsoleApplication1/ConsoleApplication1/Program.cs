using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    

    class Program
    {
        static double phi = (1 + Math.Sqrt(5)) / 2;

        static void Main(string[] args)
        {
            for (int i = 1; i < 30; i++)
            {
                Console.WriteLine(GetFib(i));
            }

            List<int> list = new List<int>();
            list.Add(1);
            list.Add(3);
            list.Add(2);
            list.Add(5);
            list.Add(4);
            Console.WriteLine(list.Min());
            Console.WriteLine(list.Max());

        }

        static int GetFib(int x)
        {
            
            if(x <= 0 )return 0;
            return (int)((Math.Pow(phi,x)-(Math.Pow(-phi,-x)))/Math.Sqrt(5));
        }



    }
}
