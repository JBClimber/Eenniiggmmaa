using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma.Machine
{
    public class MachineRun
    {
        PlugBoard pBoard;
        Mirror mirror;
        Rotor M4Rotor;
        Rotor lRotor;
        Rotor mRotor;
        Rotor rRotor;
        EntryRotor eRotor;
        Boolean mType;

        public MachineRun(string m, Rotor lR, Rotor mR, Rotor rR, string ern, string[] indPlugs)
        {
            // constructor for Enigma I - army / M1
            this.mType = false; // = army enigma

            this.pBoard = new PlugBoard(indPlugs);
            this.mirror = new Mirror(m);
            this.lRotor = lR;
            this.mRotor = mR;
            this.rRotor = rR;
            this.eRotor = new EntryRotor(ern);
            Console.WriteLine(lR.GetN()+":"+mR.GetN()+":"+rR.GetN()+"|"+lR.GetCpos()+"."+mR.GetCpos()+"."+rR.GetCpos()+"|"+lR.GetsRing()+"."+mR.GetsRing()+"."+rR.GetsRing());
        }
        public MachineRun(string m, Rotor m4, Rotor lR, Rotor mR, Rotor rR, string ern, string[] indPlugs)
        {
            // constructor for Enigma M4 - navy
            this.mType = true; // = navy enigma

            this.pBoard = new PlugBoard(indPlugs);
            this.mirror = new Mirror(m);
            this.M4Rotor = m4;
            this.lRotor = lR;
            this.mRotor = mR;
            this.rRotor = rR;
            this.eRotor = new EntryRotor(ern);

        }

        public string EncryptDecrypt(string plain)
        {

            LetterConverter lc = new LetterConverter();

            //Console.WriteLine("R Rotor:\n" + rRotor);
            //Console.WriteLine("M Rotor\n" + mRotor);
            //Console.WriteLine("L Rotor\n" + lRotor);
            //Console.WriteLine("Mirror\n" + mirror);
            //Console.WriteLine("  Plugs\n" + pBoard);
            //Console.WriteLine(" plain text:"+plain+":");

            String cipher = "";
            char[] letArray = plain.ToCharArray();
            int x;
            for (int i = 0; i < letArray.Length; i++)
            {
                TurnRotor();


                //Console.WriteLine("\n\n\nR Rotor:\n" + rRotor);
                //Console.WriteLine("M Rotor\n" + mRotor);
                //Console.WriteLine("L Rotor\n" + lRotor);
                //Console.WriteLine(" Mirror\n" + mirror);
                //Console.WriteLine("  Plugs\n" + pBoard);

                String a = letArray[i].ToString();
                //Console.WriteLine("processing:-"+a+"-");
                //Console.Write("P Board in: " + a);
                x = pBoard.LetterIn(a);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("E Rotor in: " + lc.GetNumLet(x) + x);
                x = eRotor.In(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("R Rotor in: "+ lc.GetNumLet(x) + x);
                x = rRotor.In(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("M Rotor in: " + lc.GetNumLet(x) + x);
                x = mRotor.In(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("L Rotor in: " + lc.GetNumLet(x) + x);
                x = lRotor.In(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);
                if (mType == true)
                {
                    x = M4Rotor.In(x);
                    x = mirror.getInOut(x);
                    x = M4Rotor.Out(x);
                }
                else
                {
                    //Console.Write(" Mirror in: " + lc.GetNumLet(x) + x);
                    x = mirror.getInOut(x);
                    //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);
                }

                //Console.Write("L Rotor in: " + lc.GetNumLet(x) + x);
                x = lRotor.Out(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("M Rotor in: " + lc.GetNumLet(x) + x);
                x = mRotor.Out(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("R Rotor in: " + lc.GetNumLet(x) + x);
                x = rRotor.Out(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("E Rotor in: " + lc.GetNumLet(x) + x);
                x = eRotor.Out(x);
                //Console.WriteLine("  out: " + lc.GetNumLet(x) + x);

                //Console.Write("P Board in: " + lc.GetNumLet(x) + x);
                cipher += pBoard.LetterOut(x);
                //Console.WriteLine("  out: " + cipher[cipher.Length-1]);
                //Console.WriteLine("================");
                //TurnRotor();
            }

            return cipher;
        }

        private void TurnRotor()
        {
            if (rRotor.GetCpos() == rRotor.GetTover1() || rRotor.GetCpos() == rRotor.GetTover2())
            {
                if (mRotor.GetCpos() == rRotor.GetTover1() || mRotor.GetCpos() == mRotor.GetTover2())
                {
                    lRotor.AdvanceRotor();
                }
                mRotor.AdvanceRotor();
            }
            else
            {
                if(mRotor.GetCpos() == mRotor.GetTover1() || mRotor.GetCpos() == mRotor.GetTover2())
                {
                    mRotor.AdvanceRotor();
                    lRotor.AdvanceRotor();
                }
            }
            rRotor.AdvanceRotor();
        }

        public int RotorPos(string x)
        {
            if (x.Equals("l"))
                return lRotor.GetCpos();
            else if (x.Equals("m"))
                return mRotor.GetCpos();
            else if (x.Equals("r"))
                return rRotor.GetCpos();
            else if (x.Equals("M4"))
                return M4Rotor.GetCpos();
            else
                return -1;
        }

    }
}
