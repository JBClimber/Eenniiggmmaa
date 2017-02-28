using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ConsoleTestEnigma
{
    class Program
    {

        static void Main(string[] args)
        {
            //Program cte = new Program();
            //cte.CryptoTextFiles();
            //cte.BruteForceCrib("", "hello", 8, "B");

            DataSQLcreation dsc = new DataSQLcreation();
            dsc.CreateData();

            //cte.TestOdometer();
            Console.WriteLine("press enter to exit ...");
            Console.ReadLine();
        }

        public void CryptoTextFiles()
        {
            Enigma.Machine.Rotor rl;
            Enigma.Machine.Rotor rm;
            Enigma.Machine.Rotor rr;
            
            for (int c = 65; c <= 90; c++)
            {
                string letter = Convert.ToChar(c).ToString().ToLower();
                StreamWriter file = new System.IO.StreamWriter("C:\\Users\\JiB\\Desktop\\enigmaMapsNumeric\\Test" + letter.ToUpper() + "num2.txt");

                Stopwatch timer2 = new Stopwatch();
                timer2.Start();

                string line = "";
                int count = 0;  // count for all possible calculations

                int currentPosL = 0;
                int currentPosM = 0;
                int currentPosR = 0;

                for (int i = 65; i <= 90; i++)      // 3 loops for rotor ground settings A -> Z
                {
                    for (int j = 65; j <= 90; j++)
                    {
                        for (int k = 65; k <= 90; k++)
                        {
                            count++;

                            currentPosL = i - 65;
                            currentPosM = j - 65;
                            currentPosR = k - 65;

                            rl = new Enigma.Machine.Rotor("III", currentPosL, 0);   //  left rotor III, A -> Z, ring setting a. 
                            rm = new Enigma.Machine.Rotor("II", currentPosM, 0);    //  middle rotor II, A -> Z, ring setting a.
                            rr = new Enigma.Machine.Rotor("I", currentPosR, 0);     //  right rotor I, A -> Z, ring setting a.

                            // plug board is set to: no plugs used
                            string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

                            // mirror position is set to "B" and entry rotor is set to "O"
                            Enigma.Machine.MachineRun ma = new Enigma.Machine.MachineRun("B", rl, rm, rr, "O", plugs);
                            currentPosL = rl.GetCpos();
                            currentPosM = rm.GetCpos();
                            currentPosR = rr.GetCpos();

                            line = letter + ":" + currentPosL + "," + currentPosM + "," + currentPosR + ":" + ma.EncryptDecrypt(letter).ToLower() + ";\tnext:"+rl.GetCpos()+","+rm.GetCpos()+","+rr.GetCpos()+"\n";
                            Console.WriteLine(line);
                            file.WriteLine(line);
                            file.Flush();
                        }
                    }
                }
                file.WriteLine("\r\n\r\n" + count);
                timer2.Stop();
                long sec = timer2.ElapsedMilliseconds / 1000;
                long mil = timer2.ElapsedMilliseconds % 1000;
                file.WriteLine(sec + "." + mil + " seconds");
                file.Flush();
                file.Close();
            }
        }

        public void TestOdometer()
        {
            StreamWriter file = new StreamWriter("C:\\Users\\JiB\\Desktop\\odometerTest.txt");

            Odometer o = new Odometer(0, 0, 0);
            file.WriteLine(o);
            for (int i=0; i<(26*26*26); i++)
            {
                o.NextStep();
                file.WriteLine(o);
            }

            file.Close();
        }

        public void BruteForceCrib(string msg, string crib, int numOFRotors, string mirror)
        {
            msg = msg.ToLower();
            crib = crib.ToLower();
            string decrypted = "";

            Enigma.Machine.Rotor rl;
            Enigma.Machine.Rotor rm;
            Enigma.Machine.Rotor rr;
            StreamWriter file = new System.IO.StreamWriter("C:\\Users\\JiB\\Desktop\\enigmaMAPnew\\BruteForceNew_1.txt");

            Console.WriteLine("crib: " + crib + "\n");
            file.WriteLine("crib: " + crib + "\n");
            Console.WriteLine(" msg: "+msg+"\n");
            file.WriteLine(" msg: " + msg + "\n");

            Stopwatch found = new Stopwatch();
            found.Start();

            Stopwatch timer2 = new Stopwatch();
            timer2.Start();

            string line = "";
            ulong count = 0;

            int currentPosL = 0;
            int currentPosM = 0;
            int currentPosR = 0;

            for (int ii = 1; ii <= numOFRotors; ii++)
            {
                for (int jj = 1; jj <= numOFRotors; jj++)
                {
                    for (int kk = 1; kk <= numOFRotors; kk++)
                    {
                        if (ii != jj && ii != kk && jj != kk)
                        {
                            Console.WriteLine("Searching rotor setting:  "+ii+", "+jj+", "+kk);
                            file.WriteLine("Searching rotor setting:  " + ii + ", " + jj + ", " + kk);
                            file.Flush();

                            for (int i = 65; i <= 90; i++)
                            {
                                for (int j = 65; j <= 90; j++)
                                {
                                    for (int k = 65; k <= 90; k++)
                                    {
                                        count++;

                                        currentPosL = i - 65;
                                        currentPosM = j - 65;
                                        currentPosR = k - 65;
                                        //--------------------------------------------------------------------

                                        rl = new Enigma.Machine.Rotor(ii, currentPosL, 0);
                                        rm = new Enigma.Machine.Rotor(jj, currentPosM, 0);
                                        rr = new Enigma.Machine.Rotor(kk, currentPosR, 0);
                                        string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
                                        Enigma.Machine.MachineRun ma = new Enigma.Machine.MachineRun(mirror, rl, rm, rr, "O", plugs);
                                        currentPosL = rl.GetCpos();
                                        currentPosM = rm.GetCpos();
                                        currentPosR = rr.GetCpos();
                                        decrypted = ma.EncryptDecrypt(msg).ToLower();

                                        if (decrypted.Contains(crib))
                                        {
                                            found.Stop();
                                            long se = timer2.ElapsedMilliseconds / 1000;    //seconds
                                            long mi = timer2.ElapsedMilliseconds % 1000;    // milliseconds
                                            line = "\n MATCH FOUND AT SETTING:  " + ii + ", " + jj + ", " + kk + " : " + (char)(i) + "," + (char)(j) + "," + (char)(k) + "   : timed at " + se + "." + mi+" seconds";
                                            line += "\r\n  number of comparisons: " + count;
                                            line += "\r\n\t  decrypted msg: "+decrypted+"\n";
                                            Console.WriteLine(line);
                                            file.WriteLine(line);
                                            file.Flush();
                                            //return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            file.WriteLine("\r\n\r\n" + count);

            timer2.Stop();
            long sec = timer2.ElapsedMilliseconds / 1000;
            long min = sec / 60;
            sec = sec % 60;
            long hours = min / 60;
            min = min % 60;
            long mil = timer2.ElapsedMilliseconds % 1000;
            Console.WriteLine("completed in: " + hours + ":" + min + ":" + sec + "." + mil);

            file.WriteLine("completed in: " + hours + ":" + min + ":" + sec + "." + mil);

            file.Flush();
            file.Close();



        }
    }


}
