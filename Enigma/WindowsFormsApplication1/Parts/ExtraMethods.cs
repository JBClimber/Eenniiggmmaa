using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Enigma.Parts
{
    public static class ExtraMethods
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        public static Button VerticalButton(Button btnVertical)
        {
            btnVertical.Width = 35;
            string name = btnVertical.Text;
            btnVertical.Text = string.Empty;
            for (int i = 0; i < name.Length; i++)
            {
                btnVertical.Text += name[i] + Environment.NewLine;
                btnVertical.Height += 12;
            }
            return btnVertical;
        }

        public static string[] GetPlugs(MaskedTextBox[] plugs)
        {
            string[] indPlugs = new string[26];

            for (int i = 0; i < plugs.Length; i++)
            {
                if (plugs[i].Text.Length == 2)
                {
                    string plugPair = plugs[i].Text.ToString();
                    indPlugs[i*2] = plugPair.Substring(0, 1);
                    indPlugs[i*2+1] = plugPair.Substring(1, 1);
                    //Console.WriteLine(indPlugs[i * 2] + " and " + indPlugs[i * 2 + 1]);
                }
            }
            return indPlugs;
        }

        public static void AddRotors(ComboBox[] rotors)
        {
            for (int i = 1; i <= 3; i++)
            {
                rotors[i].Items.Add("VI");
                rotors[i].Items.Add("VII");
                rotors[i].Items.Add("VIII");
            }
        }

        public static void RemoveRotors(ComboBox[] rotors)
        {
            for (int i = 1; i < 4; i++)
            {
                rotors[i].Items.Remove("VI");
                rotors[i].Items.Remove("VII");
                rotors[i].Items.Remove("VIII");
            }
        }

        public static void AddM4Mirrors(ComboBox mirrors)
        {
                mirrors.Items.Add("B thin");
                mirrors.Items.Add("C thin");
        }

        public static void RemoveM4Mirrors(ComboBox mirrors)
        {
            mirrors.Items.Remove("B thin");
            mirrors.Items.Remove("C thin");
        }

        public static void AddIMirrors(ComboBox mirrors)
        {
            mirrors.Items.Add("A");
            mirrors.Items.Add("B");
            mirrors.Items.Add("C");
        }

        public static void RemoveIMirrors(ComboBox mirrors)
        {
            mirrors.Items.Remove("A");
            mirrors.Items.Remove("B");
            mirrors.Items.Remove("C");
        }

        public static string BlocksOfText(int n, String t)
        {
            int ending = t.Length % n;
            string r = "";
            int i = 0;
            for ( i=0 ; i<t.Length-ending; i=i+4)
            {
                r += t.Substring(i, n) + " ";
            }
            return r+t.Substring(i);
        }

        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}
