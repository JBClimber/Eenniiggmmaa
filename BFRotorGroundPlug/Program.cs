using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFRotorGroundPlug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" - - - - BF on rotor, ground and plugboard - - - -\r\n");

            string file =  "";

            do {
                Console.Write("enter the file and path: ");
                file = Console.ReadLine();
            } while (!File.Exists(file));

            int wires = 0;
            bool isConverted = false;
            do
            {
                Console.Write("\nenter number of available wires: ");
                isConverted = Int32.TryParse( Console.ReadLine(), out wires);
            } while ( !isConverted );

            isConverted = false;
            int numRotors = 0;
            do
            {
                Console.Write("\nenter number of rotors to check: ");
                isConverted = Int32.TryParse( Console.ReadLine(), out numRotors);
                if (numRotors <= 0 || numRotors >= 9)
                {
                    isConverted = false;
                }
            } while (!isConverted);


            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open));
            int count = 0;

            try
            {
                for (int i=0; i < Int32.MaxValue; i++)
                {
                    Console.Write(br.ReadChar());
                    Console.Write(br.ReadChar());
                    Console.Write(br.ReadChar());
                    Console.WriteLine(br.ReadChar());
                    count++;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Finished reading from file...");
            }

            br.Close();
            Console.WriteLine("COUNT: "+count);
            Console.WriteLine("\r\n... Exiting Program...");
            Console.ReadLine();
        }
    }
}
