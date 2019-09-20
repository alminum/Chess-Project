using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(b.toString());
                string s = Console.ReadLine();
                int a1 = 0, a2 = 0, a3 = 0, a4 = 0;
                if (s[0] == 'a')
                    a2 = 0;
                else if (s[0] == 'b')
                    a2 = 1;
                else if (s[0] == 'c')
                    a2 = 2;
                else if (s[0] == 'd')
                    a2 = 3;
                else if (s[0] == 'e')
                    a2 = 4;
                else if (s[0] == 'f')
                    a2 = 5;
                else if (s[0] == 'g')
                    a2 = 6;
                else if (s[0] == 'h')
                    a2 = 7;
                if (s[2] == 'a')
                    a4 = 0;
                else if (s[2] == 'b')
                    a4 = 1;
                else if (s[2] == 'c')
                    a4 = 2;
                else if (s[2] == 'd')
                    a4 = 3;
                else if (s[2] == 'e')
                    a4 = 4;
                else if (s[2] == 'f')
                    a4 = 5;
                else if (s[2] == 'g')
                    a4 = 6;
                else if (s[2] == 'h')
                    a4 = 7;
                a1 = 8 - int.Parse(s[1].ToString());
                a3 = 8 - int.Parse(s[3].ToString());
                Console.WriteLine("" + a1 + a2 + a3 + a4);

                if (b.CanMove(a1, a2, a3, a4))
                {
                    Console.WriteLine("yes");
                    b.delete(a3, a4, b.getWhiteArr(), b.getBlackArr());
                    b.Move(a1, a2, a3, a4);
                }
                else
                    Console.WriteLine("no");
                //Console.ReadLine();
                //b.delete(7, 0);
                //Console.Clear();
                //Console.WriteLine(b.toString());
                //Console.WriteLine(b.getWLP());
                Console.ReadLine();

            }
        }
    }
}
