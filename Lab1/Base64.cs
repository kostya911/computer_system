using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {

            byte[] data = new byte[0];
            bool isDone = false;
            int Switch;
            string pathIN = "";
            string pathOut = "";
            Console.WriteLine("Enter the number of your text:");
            while (!isDone)
            {
                try
                {
                    Switch = int.Parse(Console.ReadLine());
                    switch (Switch)
                    {
                        case 1:
                            isDone = true;
                            pathIN = @"C:\Users\user\Desktop\CS_Labs\gilka\gilka.bz2";
                            pathOut = @"C:\Users\user\Desktop\CS_Labs\gilka\gilka(Base64.bz2).txt";

                            break;
                        case 2:
                            isDone = true;
                            pathIN = @"C:\Users\user\Desktop\CS_Labs\kavkaz\kavkaz.bz2";
                            pathOut = @"C:\Users\user\Desktop\CS_Labs\kavkaz\kavkaz(Base64.bz2).txt";

                            break;
                        case 3:
                            isDone = true;
                            pathIN = @"C:\Users\user\Desktop\CS_Labs\spec\spec.bz2";
                            pathOut = @"C:\Users\user\Desktop\CS_Labs\spec\spec(Base64.bz2).txt";
                            break;

                    }
                    data = Encoding.UTF8.GetBytes(File.ReadAllText(pathIN, Encoding.Default));
                }
                catch
                {
                    Console.WriteLine("The number must be in the range from 1 to 3. Try again:");
                }
            }

            char[] value = Base64Encoding(data);
            Console.WriteLine(value);
            string sValue = "";
            for (int i = 0; i < value.LongLength; i++)
            {
                sValue += value[i].ToString();
            }

            File.WriteAllText(pathOut, sValue, Encoding.Default);


            Console.ReadKey();
        }

        private static char GetCharFromIndexTable(byte b)
        {
            char[] indexTable = new char[64] {
        'A','B','C','D','E','F','G','H','I','J','K','L','M',
        'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        'a','b','c','d','e','f','g','h','i','j','k','l','m',
        'n','o','p','q','r','s','t','u','v','w','x','y','z',
        '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return indexTable[b];
            }
            else
            {
                return ' ';
            }
        }

        public static char[] Base64Encoding(byte[] data)
        {
            int length, length2;
            int blockCount;
            int paddingCount;
            length = data.Length;

            if ((length % 3) == 0)
            {
                paddingCount = 0;
                blockCount = length / 3;
            }
            else
            {
                paddingCount = 3 - (length % 3);
                blockCount = (length + paddingCount) / 3;
            }

            length2 = length + paddingCount;

            byte[] source2 = new byte[length2];

            for (int x = 0; x < length2; x++)
            {
                if (x < length)
                {
                    source2[x] = data[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte b1, b2, b3; //для маніпулювання з бітами
            byte temp, temp1, temp2, temp3, temp4;      //використовується для розбиття на 6 бітів з 8
            byte[] buffer = new byte[blockCount * 4];
            char[] result = new char[blockCount * 4];

            for (int x = 0; x < blockCount; x++)    // зсуви для того щоб досягти значення в 6 біт і не втратити нічого
            {
                b1 = source2[x * 3];
                b2 = source2[x * 3 + 1];
                b3 = source2[x * 3 + 2];

                temp1 = (byte)((b1 & 252) >> 2);

                temp = (byte)((b1 & 3) << 4);
                temp2 = (byte)((b2 & 240) >> 4);
                temp2 += temp;

                temp = (byte)((b2 & 15) << 2);
                temp3 = (byte)((b3 & 192) >> 6);
                temp3 += temp;

                temp4 = (byte)(b3 & 63);

                buffer[x * 4] = temp1;
                buffer[x * 4 + 1] = temp2;
                buffer[x * 4 + 2] = temp3;
                buffer[x * 4 + 3] = temp4;

            }

            for (int x = 0; x < blockCount * 4; x++)
            {
                result[x] = GetCharFromIndexTable(buffer[x]);
            }

            switch (paddingCount)
            {
                case 0:
                    break;
                case 1:
                    result[blockCount * 4 - 1] = '=';  // 6 бітів зі значенням 0 позначаються як =
                    break;
                case 2:
                    result[blockCount * 4 - 1] = '=';
                    result[blockCount * 4 - 2] = '=';
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
