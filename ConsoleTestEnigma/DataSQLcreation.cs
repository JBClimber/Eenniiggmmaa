using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Enigma;

namespace ConsoleTestEnigma
{
    public class DataSQLcreation
    {

        public void CreateData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\JiB\\Documents\\Enigma.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);


            Enigma.Machine.Rotor rl;// = new Enigma.Machine.Rotor("III", 0, 0);
            Enigma.Machine.Rotor rm;// = new Enigma.Machine.Rotor("II", 0, 0);
            Enigma.Machine.Rotor rr;// = new Enigma.Machine.Rotor("I", 0, 0);

            Console.WriteLine("Starting SQL creation ...");

            for (int c = 65; c <= 90; c++)
            {
                string letter = Convert.ToChar(c).ToString().ToLower();
                Console.WriteLine("working on letter : " + letter);
                int count = 0;

                Stopwatch timer2 = new Stopwatch();
                timer2.Start();

                for (int i = 65; i <= 90; i++)
                {
                    for (int j = 65; j <= 90; j++)
                    {
                        for (int k = 65; k <= 90; k++)
                        {

                            rl = new Enigma.Machine.Rotor("I", i - 65, 0);
                            rm = new Enigma.Machine.Rotor("III", j - 65, 0);
                            rr = new Enigma.Machine.Rotor("II", k - 65, 0);
                            string[] plugs = { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
                            Enigma.Machine.MachineRun ma = new Enigma.Machine.MachineRun("B", rl, rm, rr, "O", plugs);

                            string sqlInsert = "INSERT INTO InOut(I, L, M, R, O, nextL, nextM, nextR, numLrotor, numMrotor, numRrotor ) VALUES(@letter, @L, @M, @R, @O, @nextL, @nextM, @nextR, @numLrotor, @numMrotor, @numRrotor)";
                            SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
                            insertCommand.Parameters.AddWithValue("@letter", letter);
                            insertCommand.Parameters.AddWithValue("@L", (i-65));
                            insertCommand.Parameters.AddWithValue("@M", (j-65));
                            insertCommand.Parameters.AddWithValue("@R", (k-65));
                            insertCommand.Parameters.AddWithValue("@O", ma.EncryptDecrypt(letter));
                            insertCommand.Parameters.AddWithValue("@nextL", rl.GetCpos());
                            insertCommand.Parameters.AddWithValue("@nextM", rm.GetCpos());
                            insertCommand.Parameters.AddWithValue("@nextR", rr.GetCpos());
                            insertCommand.Parameters.AddWithValue("@numLrotor", rl.GetN());
                            insertCommand.Parameters.AddWithValue("@numMrotor", rm.GetN());
                            insertCommand.Parameters.AddWithValue("@numRrotor", rr.GetN());

                            try
                            {
                                connection.Open();
                                insertCommand.ExecuteNonQuery();
                                count++;
                            }
                            catch (SqlException se)
                            {
                                MessageBox.Show(""+se);
                            }
                            finally
                            {
                                connection.Close();
                            }

                        }
                    }
                }

                Console.WriteLine("letter " + letter + " has been inserted " + count + " times");
                timer2.Stop();
                long sec = timer2.ElapsedMilliseconds / 1000;
                long mil = timer2.ElapsedMilliseconds % 1000;
                Console.WriteLine(sec + "." + mil + " seconds");
            }

            Console.WriteLine("SQL created ...");
            Console.WriteLine("press any key to close ...");
            Console.ReadLine();
        }
    }

}
