using Enigma;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceGroundSetting
{
    class Program
    {
        static ulong count =0;

        static void Main(string[] args)
        {
            Program bfgs = new Program();

            bfgs.StartBFOnGroundSetting("ACE", "PMSYEQ", 8);

            Console.WriteLine("\n...\nEnd Program.");
            Console.ReadLine();
        }

        public void StartBFOnGroundSetting(string gs, string egs, int numRotors)
        {   // gs = ground setting transmited in the clear (consists of 3 letters)
            // egs = encrypted ground setting (consists of 6 letters)

            StreamWriter file = new System.IO.StreamWriter("D:\\ET\\bfOnGroundSetting\\BruteForceOnGroundSetting_"+gs+".txt");

            Stopwatch timer2 = new Stopwatch();
            timer2.Start();

            StartRotorOrder(gs, egs, numRotors, file);

            timer2.Stop();
            long sec = timer2.ElapsedMilliseconds / 1000;
            long min = sec / 60;
            sec = sec % 60;
            long hours = min / 60;
            min = min % 60;
            long mil = timer2.ElapsedMilliseconds % 1000;
            Console.WriteLine("completed in: " + hours + ":" + min + ":" + sec + "." + mil);

            file.WriteLine("completed in: " + hours + ":" + min + ":" + sec + "." + mil);
            file.WriteLine("results: "+count);
            file.Flush();
        }

        public void StartRotorOrder(string gs, string egs, int numRotors, StreamWriter file)
        {   // brute force for 3 rotors
            // numRotors = number of Rotors
            int countR = 0;
            for(int l=1; l <= numRotors; l++)
            {
                for(int m=1; m <= numRotors; m++)
                {
                    for(int r=1; r <= numRotors; r++)
                    {
                        if(l != m && l != r && m != r)
                        {
                            Console.WriteLine(l+" "+m+" "+r);
                            countR++;
                            //StartRingSettings(gs, egs, numRotors, l, m, r, file);
                        }
                    }
                }
            }
            Console.WriteLine("countR : "+countR);
        }

        public void StartRingSettings(string gs, string egs, int numRotors, int l, int m, int r, StreamWriter file)
        {   // ring setting lower case letters a -> z;  0 -> 25
            // l = left ground setting, m = middle ground setting , r = right ground setting

            Enigma.Machine.Rotor lr;
            Enigma.Machine.Rotor mr;
            Enigma.Machine.Rotor rr;
            Enigma.Machine.MachineRun ma;

            int gsL = (int)gs[0]-65;    // 1st letter of ground setting. uppercase to 0 -> 25
            int gsM = (int)gs[1]-65;    // 2nd letter of ground setting
            int gsR = (int)gs[2]-65;    // 3rd letter of ground setting

            string dgs = "";    // decrypted ground setting

            for (int rsL=0; rsL <= 25; rsL++)   // ring setting left rotor
            {
                for(int rsM=0; rsM <= 25; rsM++)    // ring setting middle rotor
                {
                    for(int rsR=0; rsR <= 25; rsR++)    // ring setting right rotor
                    {
                        // new rotors
                        lr = new Enigma.Machine.Rotor(l, gsL, rsL);
                        mr = new Enigma.Machine.Rotor(m, gsM, rsM);
                        rr = new Enigma.Machine.Rotor(r, gsR, rsR);

                        // blank plugs
                        string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
                        // new enigma
                        ma = new Enigma.Machine.MachineRun("B", lr, mr, rr, "O", plugs);

                        dgs = ma.EncryptDecrypt(egs);

                        if (dgs[0] == dgs[3] && dgs[1] == dgs[4] && dgs[2] == dgs[5]) // must be ABCABC
                        {
                            count++;

                            //Console.WriteLine("\n\n" + egs);
                            //Console.WriteLine(dgs);

                            file.WriteLine("\n"+egs);
                            file.WriteLine(dgs+"\n");
                            file.WriteLine("settings are ");
                            file.WriteLine("nrotor order:" + l + "" + m + "" + r);
                            file.WriteLine("ground:" + dgs);
                            file.WriteLine("ring:"+(char)(rsL+97)+""+(char)(rsM+97)+""+(char)(rsR+97));
                            file.Flush();

                        }

                    }
                }
            }


        }


    }
}
