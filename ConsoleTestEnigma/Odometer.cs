using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestEnigma
{
    class Odometer
    {
        private int nextL;  // left rotor
        private int nextM;  // middle rotor
        private int nextR;  // right rotor

        public Odometer(int l, int m, int r)
        {
            this.nextL = l;
            this.nextM = m;
            this.nextR = r;
        }

        public Odometer NextStep()
        {
            this.nextR = nextR + 1;
            if (this.nextR >= 26)
            {
                this.nextR = 0;
                this.nextM = nextM + 1;
            }
            if (this.nextM >= 26)
            {
                this.nextM = 0;
                this.nextL = nextL + 1;
            }
            if (this.nextL >= 26)
            {
                this.nextL = 0;
            }

            return this;
        }

        public void Permutations(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    for(int k=1; k<=n; k++)
                    {
                        if (i!=j && i!=k && j!=k)
                        {
                            Console.WriteLine(i+" "+j+" "+k);
                        }
                    }
                }
            }
            Console.WriteLine("press any enter to close ...");
            Console.ReadLine();
        }

        public override string ToString()
        {
            return "Next setting = "+nextL+","+nextM+","+nextR;
        }
    }


}
