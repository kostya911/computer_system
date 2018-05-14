using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Arithmetic_operations
{
    class Program
    {
        static void Main(string[] args)
        {
                Console.OutputEncoding = Encoding.Default;
                    Console.WriteLine("Enter A number:");
                    float multiplicandF = float.Parse(Console.ReadLine());
                    Console.WriteLine("Enter B number:");
                    float multiplierF = float.Parse(Console.ReadLine());
                    Console.WriteLine("Result is: {0}", IEEE.MultiplyIEEE(multiplicandF, multiplierF));
            Console.ReadKey();
        }
    }
    public class IEEE
    {
        public static float MultiplyIEEE(float multiplicand, float multiplier)
        {
            byte[] multiplicandBit = BitConverter.GetBytes(multiplicand);
            byte[] multiplierBit = BitConverter.GetBytes(multiplier);
            int mantissaA, mantissaB;
            int signA, signB;
            int expA, expB;
            int resA = BitConverter.ToInt32(multiplicandBit, 0);
            int resB = BitConverter.ToInt32(multiplierBit, 0);
            const int bias = 127;

            signA = resA & Convert.ToInt32("10000000000000000000000000000000", 2);
            signB = resB & Convert.ToInt32("10000000000000000000000000000000", 2);
            expA = resA & Convert.ToInt32("01111111100000000000000000000000", 2);
            expA >>= 23;
            expB = resB & Convert.ToInt32("01111111100000000000000000000000", 2);
            expB >>= 23;
            mantissaA = resA & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);
            mantissaB = resB & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);

            if (multiplicand == 0 || multiplier == 0)
            {
                return 0f;
            }

            Console.WriteLine("INITIAL VALUES:");
            Console.WriteLine("Multiplicand : Significand = " + Convert.ToString(signA, 2) + " Exponent = " + Convert.ToString(expA, 2) + " Mantissa = " + Convert.ToString(mantissaA, 2));
            Console.WriteLine("Multiplier   : Significand = " + Convert.ToString(signB, 2) + " Exponent = " + Convert.ToString(expB, 2) + " Mantissa = " + Convert.ToString(mantissaB, 2));

            Console.WriteLine("COMPUTE EXPONENTS:");
            int exponent = expA + expB - bias;
            Console.WriteLine(Convert.ToString(expA, 2) + "(2) * " + Convert.ToString(expB, 2) + "(2) - 127(10) = " + Convert.ToString(exponent, 2) + "\n");

            Console.WriteLine("MULTIPLY SIGNIFICANDS:");
            int significand = signA ^ signB;
            Console.WriteLine(Convert.ToString(signA, 2) + " XOR " + Convert.ToString(signB, 2) + " = " + Convert.ToString(significand, 2) + "\n");

            Console.WriteLine("NORMALIZE RESULT:");
            long mantisaLong = ShiftRightForIEEE(mantissaA, mantissaB);
            int mantisa = 0;
            Console.WriteLine("Mantissa = " + Convert.ToString(mantisaLong, 2));
            if ((mantisaLong & 0x800000000000) == 0x800000000000)//чи є 47 біт "1"
            {
                Console.WriteLine("Exponent = " + exponent + " +1");
                exponent++;
            }
            else
                mantisaLong <<= 1;

            for (int i = 0; i < 24; i++)
            {
                if ((mantisaLong & 0x1000000) == 0x1000000)
                {
                    mantisa |= 0x800000;
                }
                if (i == 23)
                    break;
                mantisa >>= 1;
                mantisaLong >>= 1;
            }
            mantisa &= ~(1 << 23);
            Console.WriteLine("Final mantissa = " + "1." + Convert.ToString(mantisa, 2) + "\n");

            Console.WriteLine("FINAL RESULT:");
            Console.WriteLine(Convert.ToString(significand, 2) + " " + Convert.ToString(exponent, 2) + " " + Convert.ToString(mantisa, 2));
            int res = significand | (exponent << 23) | mantisa;
            byte[] b = BitConverter.GetBytes(res);
            return BitConverter.ToSingle(b, 0);
        }
        public static long ShiftRightForIEEE(int multiplicand, int multiplier)
        {
            bool isMultiplierNegative = multiplier < 0;
            if (isMultiplierNegative)
                multiplier = ~multiplier + 1;
            long shiftedMultiplicand = multiplicand;
            long product = 0;
            shiftedMultiplicand <<= 32;

            for (int i = 0; i < 32; i++)
            {
                if ((multiplier & 1) == 1)
                    product += shiftedMultiplicand;
                product >>= 1;
                multiplier >>= 1;
            }
            if (isMultiplierNegative)
                product = ~product + 1;
            return product;
        }
    }
}