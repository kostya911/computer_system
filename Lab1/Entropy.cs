using System;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;


namespace Entropy
{
    class Program
    {
        public static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.Default;
            string s, path = "";
            bool isTrue;
            int numberOfText;
            Console.WriteLine("Enter the number of your text:");
            do
            {
                numberOfText = int.Parse(Console.ReadLine());
                switch (numberOfText)
                {
                    case 1:
                        isTrue = true;
                        path = @"C:\Users\user\Desktop\CS_Labs\gilka\gilka(Base64.bz2).txt";
                        break;
                    case 2:
                        isTrue = true;
                        path = @"C:\Users\user\Desktop\CS_Labs\kavkaz\kavkaz(Base64.bz2).txt";
                        break;
                    case 3:
                        path = @"C:\Users\user\Desktop\CS_Labs\spec\spec(Base64.bz2).txt";
                        isTrue = true;
                        break;
                    default:
                        isTrue = false;
                        break;
                }
                s = File.ReadAllText(path, Encoding.Default);

            }
            while (!isTrue);

            int[] count = new int[char.MaxValue];
            foreach (char t in s)
            {
                count[t]++;
            }

            double frequency, entropy = 0, information;
            for (int i = 0; i < char.MaxValue; i++)
            {
                if (count[i] > 0)
                {
                    frequency = (double)count[i] / s.Length;
                    entropy += frequency * Math.Log(1 / frequency, 2);
                    Console.WriteLine("{0} = {1}", (char)i, frequency);
                }
            }


            information = entropy * s.Length / 8.0;
            FileInfo file = new FileInfo(path);
            Console.WriteLine("Entropy: {0} bit\nAmount of information: {1} bytes", entropy, information); 
            Console.WriteLine("Size: {0} bytes\nFile name: {1}", file.Length, file.Name);
            Console.ReadKey();
        }
    }
}
