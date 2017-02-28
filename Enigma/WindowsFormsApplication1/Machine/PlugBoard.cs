using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma.Machine
{
    class PlugBoard
    {
        protected int[] plug;
        LetterConverter lc;

        public PlugBoard(String[] indPlugs)
        {
            this.plug = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,18, 19, 20, 21, 22, 23, 24, 25 };
            this.lc = new LetterConverter();

            SetPlugs(indPlugs);

            //printPlugs();
        }

        private void SetPlugs(string[] l)
        {
            for (int i = 0; i < l.Length/2; i++)
            {
                if (l[i*2]!=null && l[i*2+1]!=null)
                    XchangePlugs(l[i*2], l[i*2+1]);
            }
        }

        public void XchangePlugs(string x, string y)
        {
            int i = lc.GetLetNum(x);	// converts letter to number
            int j = lc.GetLetNum(y);	// converts letter to number

            this.plug[i] = j;			// swaps two numbers in the array
            this.plug[j] = i;
        }

        public int LetterIn(String l)
        {
            return plug[lc.GetLetNum(l)];
        }

        public String LetterOut(int x)
        {
            return lc.GetNumLet(FindPlug(x));
        }

        private int FindPlug(int x)
        {
            for (int i = 0; i < plug.Length; i++)
            {
                if (plug[i] == x)
                {
                    return i;
                }
            }
		    return -1;
	    }

        public void printPlugs()
        {
            for (int i = 0; i < plug.Length; i++)
            {
                Console.Write(i+":"+plug[i]+", ");
            }
        }

        public override string ToString()
        {
            LetterConverter lc = new LetterConverter();
            string from = "A  B  C  D  E  F  G  H  I  J  K  L  M  N  O  P  Q  R  S  T  U  V  W  X  Y  Z\n";
            string to = "";
            for (int i = 0; i < this.plug.Length; i++)
            {
                to += lc.GetNumLet(this.plug[i]) + "  ";
            }
            return from + to;
        }
    }
}
