using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Machine
{
    class EntryRotor
    {
        int[] entryRotor;

        public EntryRotor(string n)
        {
            if (n.Equals("O"))
            {
                // the original entry rotor
                this.entryRotor = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            }
        }

        public int In(int x)
        {
            return this.entryRotor[x];
        }

        public int Out(int y)
        {
            return FindNum(y);
        }

        private int FindNum(int y){
		    for(int i=0; i < entryRotor.Length; i++)
			    if (this.entryRotor[i] == y)
				    return i;
		    // print error message
		    return -1;
	    }
    }
}
