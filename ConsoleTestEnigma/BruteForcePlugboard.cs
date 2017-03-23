using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestEnigma
{
    class BruteForcePlugboard
    {
        static long count = 0;
        static void Main(string[] args)
        {
            //if (args.Length != 2)   // check if there are two parameters
            //{
            //    Console.WriteLine("Takes two integers; number of wires and number of plug sockets");
            //    Console.WriteLine("Restart with correct numbers");
            //    Console.WriteLine("-- END OF PROGRAM --");
            //    return;
            //}

            int w=1;
            int p=26;

            //bool check1 = int.TryParse(args[0], out w);
            //bool check2 = int.TryParse(args[1], out p);

            //if (!check1 && !check2) // check if the parameters are numbers
            //{
            //    Console.WriteLine("inputs are not numbers.");
            //    Console.WriteLine("Restart with correct numbers");
            //    Console.WriteLine("-- END OF PROGRAM --");
            //    return;
            //}

            //if (p % 2 != 0 && !(w <= p / 2) && !(w >= 1))   // check for correct numbers even number of p and w may not excede half of p or p <=0
            //{
            //    Console.WriteLine("first paramenter must be bigger than 0 and <= second paramenter/2");
            //    Console.WriteLine("second parameter must be multiple of 2");
            //    Console.WriteLine("-- END OF PROGRAM --");
            //    return;
            //}

            BruteForcePlugboard bfp = new BruteForcePlugboard();

            StreamWriter file = new StreamWriter("D:\\ET\\bfOnPlugboardUsingLetters\\BruteForcePlugboard_"+p+"per"+w+"wires_ABC.txt");

            int[] plugs = new int[p];

            Stopwatch timer2 = new Stopwatch();
            timer2.Start();

            bfp.PlugBoardSettings(w, 0, 0, plugs, file);
            Console.WriteLine("\n\ncount: "+count);
            //file.WriteLine("END_FILE\n\ncount: " + count);

            timer2.Stop();
            long sec = timer2.ElapsedMilliseconds / 1000;
            long min = sec / 60;
            sec = sec % 60;
            long hours = min / 60;
            min = min % 60;
            long mil = timer2.ElapsedMilliseconds % 1000;
            Console.WriteLine("completed in: "+hours+":"+min+":"+sec + "." + mil);

            //file.WriteLine("completed in: " + hours + ":" + min + ":" + sec + "." + mil);
            //file.Flush();

            Console.Beep();
            Console.Beep();
            Console.Beep();

            Console.ReadKey();
            return;
        }

        public void PlugBoardSettings(int w, int cw, int i, int[]p, StreamWriter file)  // w=number of wires, cw=current number wire, p[]= plugs
        {
            if (cw==w)
            {
                //PrintPlugs(p);
                //SavePlugs(p, file);
                SavePlugsToLetters(p, file);
                count++;
            }
            else {
                cw++;
                for ( ; i < p.Length; i++)
                {
                    for (int j = i + 1; j < p.Length; j++)
                    {
                        if (p[i] == 0 && p[j] == 0)
                        {
                            p[i] = cw;
                            p[j] = cw;
                            PlugBoardSettings(w, cw, i+1, p, file);
                            p[i] = 0;
                            p[j] = 0;
                        }
                        else if (p[i] == cw && p[j] == 0)
                        {
                            p[j] = cw;
                            PlugBoardSettings(w, cw, i+1, p, file);
                            p[j] = 0;
                        }
                    }
                }
            }
        }

        public void PrintPlugs(int[] p)
        {   // prints out plugs as numbers

            for (int i=0; i<p.Length; i++)
            {
                Console.Write(p[i]+" ");
            }
            Console.WriteLine();
        }

        public void SavePlugs(int[] p, StreamWriter file)
        {   // saves plugs as numbers to a txt file

            for (int i = 0; i < p.Length; i++)
            {
                file.Write( p[i] + " ");
            }

            file.WriteLine();
            file.Flush();
        }

        public void SavePlugsToLetters(int[] p, StreamWriter file)
        {   // converts plug numbers to letters and saves them to a file

            for(int i=0; i<p.Length; i++)
            {
                for(int j=i+1; j<p.Length; j++)
                {
                    if (p[i]!=0 && p[i] == p[j])
                    {
                        file.Write((char)(i+65)+""+(char)(j+65));
                    }
                }
            }
            //file.WriteLine();
            file.Flush();
        }

    }
}
