using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Machine
{
    public class LetterConverter
    {
        protected readonly string[] ALPHA = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public int GetLetNum(string l)
        {
            // gets letter's  number
            //return SearchForLetter(l, 0, 25);
            return SearchForLetter(l);
        }

        public string GetNumLet(int n)
        {
            // gets number of letter
            if (n >= 0 && n <= 25)
            {
                return this.ALPHA[n];
            }
            else
            {
                return "ERROR: does not exist";
            }
        }

        private int SearchForLetter(string l)
        {
            for (int i = 0; i < ALPHA.Length; i++ )
            {
                if (l.ToUpper().CompareTo(this.ALPHA[i]) == 0)
                    return i;
            }
            return -1;
        }
        private int SearchForLetter(string l, int low, int high)
        {
            if (low <= high)
            {
                int m = (high + low) / 2;
                if (l.ToUpper().CompareTo(this.ALPHA[m]) == 0)
                    return m;
                else if (l.ToUpper().CompareTo(this.ALPHA[m]) < 0)
                    return SearchForLetter(l, low, m - 1);
                else
                    return SearchForLetter(l, m + 1, high);
            }
            else
                return -1;
        }
    }
}
