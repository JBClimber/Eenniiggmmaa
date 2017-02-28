using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Machine
{
    class Mirror
    {
        int[] mirror;

        public Mirror(string x)
        {
            // CM = www.cryptomuseum.com
            if (x.Equals("A"))
            {   // mirror "A" used before WWII - CM
                this.mirror = new int[] { 4, 9, 12, 25, 0, 11, 24, 23, 21, 1, 22, 5, 2, 17, 16, 20, 14, 13, 19, 18, 15, 8, 10, 7, 6, 3 };
            }
            else if (x.Equals("B"))
            {   // mirror "B" used during the entire WW II - CM
                this.mirror = new int[] { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
            }
            else if (x.Equals("C"))
            {   // mirror used temporarly during WW II - CM
                this.mirror = new int[] { 5, 21, 15, 9, 8, 0, 14, 24, 4, 3, 17, 25, 23, 22, 6, 2, 19, 10, 20, 16, 18, 1, 13, 12, 7, 11 };
            }
            else if (x.Equals("B thin"))
            {   // mirror for M4 introduced 1940
                this.mirror = new int[] { 4, 13, 10, 16, 0, 20, 24, 22, 9, 8, 2, 14, 15, 1, 11, 12, 3, 23, 25, 21, 5, 19, 7, 17, 6, 18 };
            }
            else if(x.Equals("C thin"))
            {   // mirro for M4 inroduced 1940
                this.mirror = new int[] { 17, 3, 14, 1, 9, 13, 19, 10, 21, 4, 7, 12, 11, 5, 2, 22, 25, 0, 23, 6, 24, 8, 15, 18, 20, 16 };
            }
        }

        public int getInOut(int x)
        {
            //Console.WriteLine("mirror MIRROR x: "+mirror[x]);
            return mirror[x];
        }

        public override string ToString()
        {
            LetterConverter lc = new LetterConverter();
            string from = "A  B  C  D  E  F  G  H  I  J  K  L  M  N  O  P  Q  R  S  T  U  V  W  X  Y  Z\n";
            string to = "";

            for (int i = 0; i < this.mirror.Length; i++)
            {
                to += lc.GetNumLet(this.mirror[i]) + "  ";
            }

            return from + to;
        }

    }
}
