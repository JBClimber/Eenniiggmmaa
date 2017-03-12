using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Machine
{
    public class Rotor
    {
        private int[] rotor;
        private int cPos;   //curent position   0 -> 25
        private int tOver1;  // 1st turn over position
        private int tOver2;  // 2nd turn over position
        private int sRing;  // ring setting     0 -> 25
        private int n;      // rotor number in int ("I", ..., "Gamma")

        public Rotor(string n, int sPos, int sRing)
        {   // n = which rotor, sPos = starting position, sRing = ring position
            // rotor wireing referenced from www.cryptomuseum.com
            if (n.Equals("I"))
            {
                SetChooseRotor(1);
            }
            else if (n.Equals("II"))
            {
                SetChooseRotor(2);
            }
            else if (n.Equals("III"))
            {
                SetChooseRotor(3);
            }
            else if (n.Equals("IV"))
            {
                SetChooseRotor(4);
            }
            else if (n.Equals("V"))
            {
                SetChooseRotor(5);
            }
            else if (n.Equals("VI"))
            {
                SetChooseRotor(6);
            }
            else if (n.Equals("VII"))
            {
                SetChooseRotor(7);
            }
            else if (n.Equals("VIII"))
            {
                SetChooseRotor(8);
            }
            else if (n.Equals("Beta"))
            {
                this.rotor = new int[] { 11, 4, 24, 9, 21, 2, 13, 8, 23, 22, 15, 1, 16, 12, 3, 17, 19, 0, 10, 25, 6, 5, 20, 7, 14, 18 };
                // no turnovers for this rotor, does not move
                this.n = 9;
            }
            else if (n.Equals("Gamma"))
            {
                this.rotor = new int[] { 5, 18, 14, 10, 0, 13, 20, 4, 17, 7, 12, 1, 19, 8, 24, 2, 22, 11, 16, 15, 25, 23, 21, 6, 9, 3 };
                // no turnovers for this rotor, does not move
                this.n = 10;
            }
            this.cPos = sPos;
            setRotor(sPos);
            Console.WriteLine("ring setting: " + sRing);
            this.sRing = sRing;
            setRotorRing(sRing);
        }

        public Rotor(int n, int sPos, int sRing)
        {
            SetChooseRotor(n);
            this.cPos = sPos;
            setRotor(sPos);
            //Console.WriteLine("ring setting: " + sRing);
            this.sRing = sRing;
            setRotorRing(sRing);
        }

        private void setRotor(int at)
        {
            if (at >= rotor.Length)		// makes sure the value is between 0 and length of rotor
                at = at % rotor.Length;
            if (at > 0)
            {
                for (int i = 0; i < rotor.Length; i++) {
                    rotor[i] = (((rotor[i] - at) + rotor.Length) % rotor.Length);
                }
            }
        }

        private void setRotorRing(int at)
        {
            int temp;
            int lastPos = this.rotor.Length - 1;
            for (int i = 0; i < at; i++)
            {
                for (int j = 0; j < lastPos; j++) { 
                    temp = this.rotor[lastPos];
                    this.rotor[lastPos] = this.rotor[j];
                    this.rotor[j] = temp;
                }
            }
        }

        public int In(int x)
        {
            //Console.WriteLine("in calc=" + ((this.cPos % this.rotor.Length) + x) % this.rotor.Length);
            return (this.rotor[ ((this.cPos%this.rotor.Length) + x) % this.rotor.Length ]+sRing) % this.rotor.Length;
            //return this.rotor[((this.cPos % this.rotor.Length) + x) % this.rotor.Length];
        }

        public int Out(int y)
        {
            return (FindNum((y+this.rotor.Length -sRing)%this.rotor.Length) + this.rotor.Length - cPos) % this.rotor.Length;
        }

        private int FindNum(int y){

		    for(int i=0; i < this.rotor.Length; i++)
			    if (this.rotor[i] == y)
				    return i;
		    // display an error message
		    return -1;
	    }

        public int GetCpos()
        {   // returns current ground position
            return this.cPos;
        }

        public int GetTover1()
        {   // returns first turn over position
            return this.tOver1;
        }

        public int GetTover2()
        {   // returns second turn over position
            return this.tOver2;
        }

        public int GetN()
        {   // returns rotor number
            return this.n;
        }

        public int GetsRing()
        {   // returns the ring position
            return this.sRing;
        }

        public void AdvanceRotor()
        {
            this.cPos++;
            if (this.cPos >= this.rotor.Length)
                this.cPos = this.cPos % this.rotor.Length;
            setRotor(1);
        }

        private void SetChooseRotor(int num)
        {
            if (num == 1)   // introduced in 1930
            {                        //  A,  B,  C, D,  E, F, G,  H,  I,  J,  K,  L,  M,  N,  O, P,  Q,  R,  S,  T, U, V, W,  X, Y, Z
                this.rotor = new int[] { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 };
                this.tOver1 = 16;   // turn over at "Q"
                this.tOver2 = 16;   // turn over at "Q" 
                this.n = 1;
            }
            else if (num == 2)  // introduced in 1930
            {                       //   A, B, C,  D,  E, F,  G,  H,  I, J,  K, L,  M,  N,  O, P,  Q, R,  S,  T,  U,  V, W,  X,  Y, Z
                this.rotor = new int[] { 0, 9, 3, 10, 18, 8, 17, 20, 23, 1, 11, 7, 22, 19, 12, 2, 16, 6, 25, 13, 15, 24, 5, 21, 14, 4 };
                this.tOver1 = 4;    // turn over at "E"
                this.tOver2 = 4;    // turn over at "E"
                this.n = 2;
            }
            else if (num == 3)  // introduction in 1930
            {
                this.rotor = new int[] { 1, 3, 5, 7, 9, 11, 2, 15, 17, 19, 23, 21, 25, 13, 24, 4, 8, 22, 6, 0, 10, 12, 20, 18, 16, 14 };
                this.tOver1 = 21;    // turn over at "V"
                this.tOver2 = 21;    // turn over at "V"
                this.n = 3;
            }
            else if (num == 4)  // introduced in December 1938
            {
                this.rotor = new int[] { 4, 18, 14, 21, 15, 25, 9, 0, 24, 16, 20, 8, 17, 7, 23, 11, 13, 5, 19, 6, 10, 3, 2, 12, 22, 1 };
                this.tOver1 = 9;    // turn over at "J"
                this.tOver2 = 9;    // turn over at "J"
                this.n = 4;
            }
            else if (num == 5)  // introduced in December 1938
            {
                this.rotor = new int[] { 21, 25, 1, 17, 6, 8, 19, 24, 20, 15, 18, 3, 13, 7, 11, 23, 0, 22, 12, 9, 16, 14, 5, 4, 2, 10 };
                this.tOver1 = 25;    // turn over at "Z" was 26
                this.tOver2 = 25;    // turn over at "Z" was 26
                this.n = 5;
            }
            else if (num == 6)  // introduced in 1939
            {
                this.rotor = new int[] { 9, 15, 6, 21, 14, 20, 12, 5, 24, 16, 1, 4, 13, 7, 25, 17, 3, 10, 0, 18, 23, 11, 8, 2, 19, 22 };
                this.tOver1 = 12;    // turn over at "M"
                this.tOver2 = 25;     // turn over at "Z"
                this.n = 6;
            }
            else if (num == 7)  // introduced in 1939
            {
                this.rotor = new int[] { 13, 25, 9, 7, 6, 17, 2, 23, 12, 24, 18, 22, 1, 14, 20, 5, 0, 8, 21, 11, 15, 4, 10, 16, 3, 19 };
                this.tOver1 = 12;    // turn over at "M"
                this.tOver2 = 25;     // turn over at "Z"
                this.n = 7;
            }
            else if (num == 8)  // introduced in 1939
            {
                this.rotor = new int[] { 5, 10, 16, 7, 19, 11, 23, 14, 2, 1, 9, 18, 15, 3, 25, 17, 0, 12, 4, 22, 13, 8, 20, 24, 6, 21 };
                this.tOver1 = 12;     // turn over at "M"
                this.tOver2 = 25;     // turn over at "Z"
                this.n = 8;
            }
        }

        public override string ToString()
        {
            LetterConverter lc = new LetterConverter();
            string from = "A  B  C  D  E  F  G  H  I  J  K  L  M  N  O  P  Q  R  S  T  U  V  W  X  Y  Z\n";
            string to   = "";
            for(int i=0; i<this.rotor.Length; i++)
            {
                to += lc.GetNumLet(this.rotor[i]) + "  ";
            }
            return from + to;
        }
    }
}
