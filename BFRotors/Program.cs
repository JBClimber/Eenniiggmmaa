using Enigma.Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BFRotors
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] order = CreateRotorOrder(8);
            int[,] grdSet = CreateGroundSets();

            Console.WriteLine("What the fuck");

            CompleteBeep();

            Console.WriteLine(" test complete ");
            Console.ReadKey();
        }

        public static void BFrotors(int[,] order, string cryb, string msg)
        {   // brute force through order of rotors and their ground settings  NOT including ring settings and NOT including the plugboard
            // uses the B mirror 
            // runs one enigma at a time

            Rotor rl, rm, rr;

			StreamWriter file = new StreamWriter("C:\\EnigmaTests\\Stopwatch\\BFrotors\\BFrotorsMethod_B_orderOf8" + cryb + ".txt");

            file.WriteLine("\ncryb: "+cryb);
            file.WriteLine("\n msg: "+ msg+ "\r\n");
            file.Flush();

			Stopwatch timer = new Stopwatch();
			timer.Start();

			string line = "";
			int count = 0;  // count for all possible calculations

            for (int orderRow = 1; orderRow < order.GetLength(0); orderRow++) {

                Console.WriteLine("Checking row: " + orderRow);

                for (int gpl = 0; gpl <= 25; gpl++)      // ground position of left rotor
                {
                    for (int gpm = 0; gpm <= 25; gpm++)    // ground position of middle rotor
                    {
                        for (int gpr = 0; gpr <= 25; gpr++)    // ground position right rotor
                        {
                            count++;

                            //Console.WriteLine(order[orderRow, 1] + ":" + order[orderRow, 2] + ":" + order[orderRow, 3] + ":" + gpl + ":" + gpm + ":" + gpr);

                            rl = new Rotor(order[orderRow, 1], gpl, 0);      //  left rotor , A -> Z, ring setting a. 
                            rm = new Rotor(order[orderRow, 2], gpm, 0);      //  middle rotor , A -> Z, ring setting a.
                            rr = new Rotor(order[orderRow, 3], gpr, 0);      //  right rotor , A -> Z, ring setting a.

                            // plug board is set to: no plugs used
                            string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

                            // mirror position is set to "B" and entry rotor is set to "O"
                            Enigma.Machine.MachineRun ma = new Enigma.Machine.MachineRun("B", rl, rm, rr, "O", plugs);
                            string decMsg = ma.EncryptDecrypt(msg);

                            if (decMsg.Contains(cryb.ToUpper()))
                            {
                                line = "FOUND at rotor order: "+order[orderRow, 1]+order[orderRow, 2]+order[orderRow, 3]+"  at GS: "+gpl+"."+gpm+"."+gpr;
                                line += Environment.NewLine + "decoded: " + decMsg;
                                //Console.WriteLine(line);
                                file.WriteLine(line);
                                file.Flush();
                            }

                        }
                    }
                }
            }
			file.WriteLine("\r\n\r\n" + count);
			timer.Stop();
			TimeSpan ts = timer.Elapsed;

			file.WriteLine("running time at "+ts.Days+":"+ts.Hours+":"+ts.Minutes+":"+ts.Seconds + "." + ts.Milliseconds/10 );
			file.Flush();
			file.Close();
        }

        public static void BFrotorsEveryTwo(int[,] order, string cryb, string msg)
        {   // brute force through order of rotors and their ground settings  NOT including ring settings and NOT including the plugboard
            // uses the B mirror
            // runs two enigmas consecutively

            StreamWriter file = new StreamWriter("C:\\EnigmaTests\\Stopwatch\\BFrotors\\BFrotorsEveryTwo_B_orderOf8" + cryb + ".txt");

            // plug board is set to: no plugs used
            string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

            file.WriteLine("\ncryb: " + cryb);
            file.WriteLine("\n msg: " + msg + "\r\n");
            file.Flush();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            int count = 0;      // count for all possible calculations

            string line = "";   // for output to the file
            string decMsg_1 = "";
            string decMsg_2 = "";

            Rotor rl_1, rm_1, rr_1, rl_2, rm_2, rr_2;
            MachineRun ma_1, ma_2;


            for (int orderRow = 1; orderRow < order.GetLength(0); orderRow += 2)
            {

                Console.WriteLine("Checking row: " + orderRow);

                for (int gpl = 0; gpl <= 25; gpl++)      // ground position of left rotor
                {
                    for (int gpm = 0; gpm <= 25; gpm++)    // ground position of middle rotor
                    {
                        for (int gpr = 0; gpr <= 25; gpr++)    // ground position right rotor
                        {
                            rl_1 = new Rotor(order[orderRow, 1], gpl, 0);      //  left rotor , A -> Z, ring setting a. 
                            rm_1 = new Rotor(order[orderRow, 2], gpm, 0);      //  middle rotor , A -> Z, ring setting a.
                            rr_1 = new Rotor(order[orderRow, 3], gpr, 0);      //  right rotor , A -> Z, ring setting a.

                            rl_2 = new Rotor(order[orderRow + 1, 1], gpl, 0);      //  left rotor , A -> Z, ring setting a. 
                            rm_2 = new Rotor(order[orderRow + 1, 2], gpm, 0);      //  middle rotor , A -> Z, ring setting a.
                            rr_2 = new Rotor(order[orderRow + 1, 3], gpr, 0);      //  right rotor , A -> Z, ring setting a.

                            // mirror position is set to "B" and entry rotor is set to "O"
                            ma_1 = new MachineRun("B", rl_1, rm_1, rr_1, "O", plugs);
                            ma_2 = new MachineRun("B", rl_2, rm_2, rr_2, "O", plugs);

                            decMsg_1 = ProcessOrder1(ma_1, msg);
                            count++;

                            decMsg_2 = ProcessOrder1(ma_2, msg);
                            count++;

                            //Console.WriteLine(order[orderRow, 1]+":"+ order[orderRow, 2]+":"+order[orderRow, 3]+":"+gpl+":"+gpm+":"+gpr);
                            //Console.WriteLine(order[orderRow+1, 1] + ":" + order[orderRow+1, 2] + ":" + order[orderRow+1, 3] + ":" + gpl + ":" + gpm + ":" + gpr);

                            if (decMsg_1.Contains(cryb.ToUpper()))
                            {
                                line = "FOUND at rotor order: " + order[orderRow, 1] + order[orderRow, 2] + order[orderRow, 3] + "  at GS: " + gpl + "." + gpm + "." + gpr;
                                line += Environment.NewLine + "decoded: " + decMsg_1;
                                //Console.WriteLine(line);
                                file.WriteLine(line);
                                file.Flush();
                            }

                            if (decMsg_2.Contains(cryb.ToUpper()))
                            {
                                line = "FOUND at rotor order: " + order[orderRow+1, 1] + order[orderRow+1, 2] + order[orderRow+1, 3] + "  at GS: " + gpl + "." + gpm + "." + gpr;
                                line += Environment.NewLine + "decoded: " + decMsg_2;
                                //Console.WriteLine(line);
                                file.WriteLine(line);
                                file.Flush();
                            }

                        }
                    }
                }
            }
            file.WriteLine("\r\n\r\n" + count);
            timer.Stop();
            TimeSpan ts = timer.Elapsed;

            file.WriteLine("running time at " + ts.Days + ":" + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds / 10);
            file.Flush();
            file.Close();
        }

        public static void BFrotorsANDgroundset(int[,] order, int[,] grdSet, string cryb, string msg)
        {   // uses brute force on rotor order and ground settings
            // order set and ground setting set are created previously

            Rotor rl, rm, rr;

            StreamWriter file = new StreamWriter("C:\\EnigmaTests\\Stopwatch\\BFrotorsANDgroundset\\Test_B_orderANDgrdSet_" + cryb + ".txt");

            file.WriteLine("\ncryb: " + cryb);
            file.WriteLine("\n msg: " + msg + "\r\n");
            file.Flush();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            string line = "";
            int count = 0;  // count for all possible calculations

            for (int ord = 1; ord < order.GetLength(0); ord++)      // order set row
            {
                for (int grd = 1; grd < grdSet.GetLength(0); grd++) // grdSet set row
                {
                    count++;

                    //Console.WriteLine(order[orderRow, 1] + ":" + order[orderRow, 2] + ":" + order[orderRow, 3] + ":" + gpl + ":" + gpm + ":" + gpr);

                    rl = new Rotor(order[ord, 1], grdSet[grd, 1], 0);      //  left rotor , A -> Z, ring setting a. 
                    rm = new Rotor(order[ord, 2], grdSet[grd, 2], 0);      //  middle rotor , A -> Z, ring setting a.
                    rr = new Rotor(order[ord, 3], grdSet[grd, 3], 0);      //  right rotor , A -> Z, ring setting a.

                    // plug board is set to: no plugs used
                    string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

                    // mirror position is set to "B" and entry rotor is set to "O"
                    Enigma.Machine.MachineRun ma = new Enigma.Machine.MachineRun("B", rl, rm, rr, "O", plugs);
                    string decMsg = ma.EncryptDecrypt(msg);

                    if (decMsg.Contains(cryb.ToUpper()))
                    {
                        line = "FOUND at rotor order: " + order[ord, 1] + order[ord, 2] + order[ord, 3] + "  at GS: " + grdSet[grd, 1] + "." + grdSet[grd, 2] + "." + grdSet[grd, 3];
                        line += Environment.NewLine + "decoded: " + decMsg;
                        //Console.WriteLine(line);
                        file.WriteLine(line);
                        file.Flush();
                    }
                }
            }
            file.WriteLine("\r\n\r\n" + count);
            timer.Stop();
            TimeSpan ts = timer.Elapsed;

            file.WriteLine("running time at " + ts.Days + ":" + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds + "." + ts.Milliseconds / 10);
            file.Flush();
            file.Close();
        }

        public static string ProcessOrder1(MachineRun ma, string msg)
        {
            string r = ma.EncryptDecrypt(msg);
            //Console.WriteLine(r);
            return r;
        }

        public static int[,] CreateRotorOrder(int o)
        {       // creates complete order for the specific number of rotors "o"

            int nOf = o * (o - 1) * (o-2) + 1;      //  columns will start from 1 -> nOf-1
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

        public static int[,] CreateGroundSets()
        {   // creates complete set of possible ground settings per rotor three rotors
            //  note: first setting starts at 1,1 NOT 0,0
            int rows = 26 * 26 * 26 + 1;
            int[,] grdSet = new int[rows, 4];
            int gsl, gsm, gsr;      // ground setting left rotor, ground setting middle rotor, ground setting right rotor.

            rows = rows - (26 * 26 *26);

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

        public static void Print(int[,] order)  // can be deleted
        {
            for (int i = 1; i < order.GetLength(0); i++)
            {

                Console.WriteLine("row "+i+"   "+order[i,1]+":"+order[i,2]+":"+order[i,3]);
            }
        }

        public static void CompleteBeep()
        {
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(500, 1500);
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(5000, 500);
            Console.Beep(2000, 500);
            Console.Beep(500, 1500);

        }

    }
}
