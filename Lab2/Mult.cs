using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mult
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter A number");
            string num1 = Console.ReadLine();
            Console.WriteLine("Enter B number");
            string num2 = Console.ReadLine();
            multiplication(num1, num2);
            Console.ReadKey();
        }

        public static void multiplication(string num1, string num2)
        {
            Int64 multiplicand, multiplier, product = 0, startMultiplicand, startMultiplier;
            startMultiplicand = multiplicand = Int32.Parse(num1);
            startMultiplier = multiplier = Int32.Parse(num2);
            for (int i = 0; i < 32; ++i)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Step №" + (i + 1) + ":");

                Console.WriteLine("Multiplicand:\n" + FinishStringWithZeros(Convert.ToString(multiplicand, 2)) +
                    "\nMultiplier:\n" + FinishStringWithZeros(Convert.ToString(multiplier, 2)));

                short lsb = (short)(multiplier & 1);

                if (lsb == 1)
                {
                    Console.WriteLine("Add multiplicand and product.\n");
                    product += multiplicand;
                }
                Console.WriteLine("Product:\n" + FinishStringWithZeros(Convert.ToString(product, 2)));
                Console.WriteLine("Shift multiplicand left");
                Console.WriteLine("Shift multiplier right");
                Console.WriteLine("--------------------------");

                multiplicand <<= 1;
                multiplier >>= 1;
            }

            Console.WriteLine("\n" + FinishStringWithZeros(Convert.ToString(startMultiplicand, 2)) +
                "\n*\n" +
                FinishStringWithZeros(Convert.ToString(startMultiplier, 2)) +
                "\nResult\n" +
                FinishStringWithZeros(Convert.ToString(product, 2)) + "\n");

            Console.WriteLine(startMultiplicand + " * " + startMultiplier + " = " + product);
        }

        static string FinishStringWithZeros(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }
    }
}
