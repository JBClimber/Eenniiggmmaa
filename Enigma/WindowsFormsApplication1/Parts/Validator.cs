using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Enigma.Parts
{
    public static class Validator
    {
        public static bool IsRotorSet(ComboBox[] rotors)
        {
            if (rotors[1].SelectedIndex == rotors[2].SelectedIndex || rotors[1].SelectedIndex == rotors[3].SelectedIndex || rotors[2].SelectedIndex == rotors[3].SelectedIndex)
                return false;

            return true;
        }

        public static bool IsPlugSet(MaskedTextBox[] plugs)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //System.Console.WriteLine("inside IsPlugSet");

            for (int i = 0; i < plugs.Length; i++)
            {
                string l = plugs[i].Text;
                //System.Console.WriteLine(l + " with length: "+l.Length);
                l = l.ToUpper();

                if (l.Length == 1 )
                {
                    return false;
                }
                else if (l.Length == 2)
                {
                    if (alpha.Contains(l[0])) {
                        alpha = alpha.Remove(alpha.IndexOf(l[0]), 1);
                    }
                    else
                    {
                        return false;
                    }

                    if (alpha.Contains(l[1]))
                    {
                        alpha = alpha.Remove(alpha.IndexOf(l[1]), 1);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //MessageBox.Show(" NO ifs HIT");
            return true;
        }

        public static bool IsTextValid(string text)
        {
            if (Regex.IsMatch(text, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
