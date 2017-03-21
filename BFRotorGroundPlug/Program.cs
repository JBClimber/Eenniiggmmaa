using Enigma.Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFRotorGroundPlug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" - - - - BF on rotor, ground and plugboard - - - -\r\n");

            // enter file and path to read from
            BinaryReader fileIn = GetFileAndPath();

            // enter file and path to save to
            StreamWriter fileOut = CreateSaveFile();

            // enter number of wires
            int wires = 1;// NumberOfWires();

            // enter number of rotors to check  ( 1 through ?) max is 8
            int numOfRotors = 3;// NumberOfRotors();

            // enter cryb
            Console.Write("\nenter cryb: ");
            string cryb = "helloworld";// Console.ReadLine();

            // enter message
            Console.Write("\nenter msg: ");
            string msg = "VKHZQPADBN";// Console.ReadLine();

            fileOut.WriteLine("cryb: "+cryb);
            fileOut.WriteLine(" msg: "+msg);
            fileOut.Flush();

            // creates rotor order set and groundsettings set
            int[,] order = CreateRotorOrder(numOfRotors);
            int[,] ground = CreateGround();

            // start timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            ulong count = 0;


            try
            {   
                while (fileIn.PeekChar() > 0) 
                {
                    string[] plugs = new string[26];
                    // reads the plugs from file
                    for (int i=1; i<=wires; i++)
                    {
                        plugs[i - 1] = fileIn.ReadChar().ToString();
                        plugs[i] = fileIn.ReadChar().ToString();
                    }
                    PrintPlugs(plugs);

                    for (int o = 1; o < order.GetLength(0); o++)      // loop for the rotor order
                    {
                        //List<Task> tasks = new List<Task>();
                        for (int g = 1; g < ground.GetLength(0); g++)
                        {
                            int oo = o;     // reassigns the variable for parallel processing
                            int rsl = order[oo, 1], rsm = order[oo, 2], rsr = order[oo, 3];

                            int gg = g;     // reassigns the variable for parallel processing
                            int gsl = ground[gg, 1], gsm = ground[gg, 2], gsr = ground[gg, 3];

                            //Console.WriteLine(rsl + ":" + rsm + ":" + rsr + "|" + gsl + ":" + gsm + ":" + gsr + "|");
                            //fileOut.Flush();
                            //Task t = new Task(() =>
                            //{
                                MachineRotorGroundPlug(rsl, rsm, rsr, gsl, gsm, gsr, plugs, cryb, msg, fileOut);
                            //});
                            //t.Start();
                            //tasks.Add(t);

                        }
                        //Task.WaitAll(tasks.ToArray());
                    }


                    count++;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("MY ERROR: " + e.Message);
            }
            finally
            {
                // close files
                fileIn.Close();
                fileOut.Flush();
                fileOut.Close();
            }

            Console.WriteLine("COUNT: "+count);

            // stop timer
            timer.Stop();

            // calculate and show elapsed time
            ElapsedTime(timer);

            CompleteBeep();
            Console.WriteLine("\r\n... press enter to exit ...");
            Console.ReadLine();
        }

        public static BinaryReader GetFileAndPath()
        {   // returns file that exists

            string file = @"D:\ET\bfOnPlugboardUsingLetters\BruteForcePlugboard_26per1wires_ABC.txt";
            do
            {
                Console.Write("read from (file and path): ");
                //file = Console.ReadLine();
            } while (!File.Exists(file));

            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open));
            return br;
        }

        public static StreamWriter CreateSaveFile()
        {
            string file = @"E:\Enigma\BF\RotorGroundPlug\Test1_Wires1.txt";
            //do
            //{
                Console.Write("save to (file and path): ");
                //file = Console.ReadLine();
            //} while (File.Exists(file));

            StreamWriter sw = File.CreateText(file);
            return sw;
        }

        public static int NumberOfWires()
        {   // returns correct number of wires

            int wires = 0;
            bool isConverted = false;
            do
            {
                Console.Write("\nenter number of available wires: ");
                isConverted = Int32.TryParse(Console.ReadLine(), out wires);
            } while (!isConverted);

            return wires;
        }

        public static int NumberOfRotors()
        {   //  return number of rotors to check

            bool isConverted = false;
            int numRotors = 0;
            do
            {
                Console.Write("\nenter number of rotors to check: ");
                isConverted = Int32.TryParse(Console.ReadLine(), out numRotors);
                if (numRotors <= 0 || numRotors >= 9)
                {
                    isConverted = false;
                }
            } while (!isConverted);

            return numRotors;
        }

        public static int[,] CreateRotorOrder(int o)
        {       // creates complete order for the specific number of rotors "o"

            int nOf = o * (o - 1) * (o - 2) + 1;      //  columns will start from 1 -> nOf-1
            int[,] order = new int[nOf, 4];         //  rows will start from 1 -> 3

            for (int i = o; i > 0; i--)
            {
                for (int j = o; j > 0; j--)
                {
                    for (int k = o; k > 0; k--)
                    {
                        if (i != j && i != k && j != k && nOf != 0)
                        {
                            order[nOf - 1, 1] = i;
                            order[nOf - 1, 2] = j;
                            order[nOf - 1, 3] = k;
                            nOf--;
                        }

                    }
                }
            }

            return order;
        }

        public static int[,] CreateGround()
        {   // creates complete set of possible ground settings per rotor three rotors
            //  note: first setting starts at 1,1 NOT 0,0
            // 
            int rows = 26 * 26 * 26 + 1;
            int[,] grdSet = new int[rows, 4];
            int gsl, gsm, gsr;      // ground setting left rotor, ground setting middle rotor, ground setting right rotor.

            rows = rows - (26 * 26 * 26);

            for (gsl = 0; gsl < 26; gsl++)          // left rotor
            {
                for (gsm = 0; gsm < 26; gsm++)      // middle rotor
                {
                    for (gsr = 0; gsr < 26; gsr++)  // right rotor
                    {
                        grdSet[rows, 1] = gsl;
                        grdSet[rows, 2] = gsm;
                        grdSet[rows, 3] = gsr;
                        rows++;
                    }
                }
            }

            return grdSet;
        }

        public static void MachineRotorGroundPlug(int rsl, int rsm, int rsr, int gsl, int gsm, int gsr, string[] plugs, string cryb, string msg, StreamWriter fileOut)
        {
            // mirror position is set to "B" and entry rotor is set to "O"
            MachineRun ma = new MachineRun("B", new Rotor(rsl, gsl, 0), new Rotor(rsm, gsm, 0), new Rotor(rsr, gsr, 0), "O", plugs);
            string decMsg = ma.EncryptDecrypt(msg);

            //Console.WriteLine(rsl + ":" + rsm + ":" + rsr + "|" + gsl + ":" + gsm + ":" + gsr + "|");
            if (decMsg.Equals(cryb.ToUpper()))
            {
                string line = "FOUND at rotor order: " + rsl + rsm + rsr + "  at GS: " + gsl + "." + gsm + "." + gsr +" plugs: "+ plugs[0]+plugs[1];
                line += Environment.NewLine + "decoded: " + decMsg;
                Console.WriteLine(line);
                fileOut.WriteLine(line);
                fileOut.Flush();
            }
        }

        public static void PrintPlugs(string[] p)
        {   // prints string array to console

            string outS = "";
            foreach (string s in p)
            {
                if (s == null)
                {
                    outS += "";
                }
                else
                {
                    outS += s + " ";
                }
            }
            Console.WriteLine ("plugs: " + outS);
        }

        public static void ElapsedTime(Stopwatch timer)
        {
            TimeSpan t = timer.Elapsed;
            Console.WriteLine("running time at " + t.Days + ":" + t.Hours + ":" + t.Minutes + ":" + t.Seconds + "." + t.Milliseconds / 10);
        }

        public static void CompleteBeep()
        {
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(500, 1500);
            //Console.Beep(5000, 500);
            //Console.Beep(2000, 500);
            //Console.Beep(5000, 500);
            //Console.Beep(2000, 500);
            //Console.Beep(500, 1500);
        }
    }
}
