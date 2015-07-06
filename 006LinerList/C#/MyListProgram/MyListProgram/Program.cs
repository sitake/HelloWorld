using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var l1 = new MyList<String>();
            var l2 = l1.Add("apple").Add("an").Add("have").Add("I");
            Console.WriteLine(l2);
            Console.WriteLine(l2.Get(2));
        }
    }
}
