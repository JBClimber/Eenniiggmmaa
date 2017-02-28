using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestEnigma
{
    public class DataSet
    {
        private char i; // input
        private int l;  // left rotor
        private int m;  // middle rotor
        private int r;  // right rotor
        private char o; // output

        public DataSet(char i, int l, int m, int r, char o)
        {
            this.i = i;
            this.l = l;
            this.m = m;
            this.r = r;
            this.o = o;
        }

        public char GetI()
        {
            return this.i;
        }
        public int GetL()
        {
            return this.l;
        }
        public int GetM()
        {
            return this.m;
        }
        public int GetR()
        {
            return this.r;
        }
        public char GetO()
        {
            return this.o;
        }

        public Boolean CompareToNext(DataSet y)
        {

            return false;
        }

        public override string ToString()
        {
            return "DataSet =  "+this.i+":"+this.l+","+this.m+","+this.r+":"+this.o;
        }
    }
}
