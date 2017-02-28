using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma
{
    public partial class Enigma : Form
    {
        MaskedTextBox[] plugs;
        ComboBox[] rotors;
        Boolean mType; // false= "Enigma I - army" true= "Enigma M4 -navy"

        public Enigma()
        {
            InitializeComponent();
        }

        private void Enigma_Load(object sender, EventArgs e)
        {
            plugs = new MaskedTextBox[13] { boxPlug1, boxPlug2, boxPlug3, boxPlug4, boxPlug5, boxPlug6, boxPlug7, boxPlug8, boxPlug9, boxPlug10, boxPlug11, boxPlug12, boxPlug13}; //
            rotors = new ComboBox[4] { boxM4rotor, boxLrotor, boxMrotor, boxRrotor};

            btnEncDec = Parts.ExtraMethods.VerticalButton(btnEncDec); // changes to vertical button
            
            SetResetRotor();

            mType = false;      // upon opening default is army enigma
            boxM4rotor.Visible = false;
            boxM4rotorSet.Visible = false;
            boxM4rotorRingSet.Visible = false;

            //CheckArray();
        }

        private void boxMirror_SelectedIndexChanged(object sender, EventArgs e)
        {
            // changes mirror selection
        }

        private void btnEncDec_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(errorInput.GetError(txtInput).Equals("") + "\n" + errorPlugs.GetError(boxPlug9).Equals("") + "\n" + errorRotors.GetError(boxRrotor).Equals(""), "before endy");
            if (errorInput.GetError(txtInput)=="" && errorPlugs.GetError(boxPlug9)=="" && errorRotors.GetError(boxRrotor)=="")
            {
                Machine.Rotor M4Rotor = new Machine.Rotor( boxM4rotor.SelectedItem.ToString(), boxM4rotorSet.SelectedIndex, boxM4rotorRingSet.SelectedIndex);
                Machine.Rotor lRotor = new Machine.Rotor( boxLrotor.SelectedItem.ToString(), boxLrotorSet.SelectedIndex, boxLrotorRingSet.SelectedIndex);
                Machine.Rotor mRotor = new Machine.Rotor( boxMrotor.SelectedItem.ToString(), boxMrotorSet.SelectedIndex, boxMrotorRingSet.SelectedIndex);
                Machine.Rotor rRotor = new Machine.Rotor( boxRrotor.SelectedItem.ToString(), boxRrotorSet.SelectedIndex, boxRrotorRingSet.SelectedIndex);

                String msg = Parts.ExtraMethods.ReplaceWhitespace(txtInput.Text, "");

                if (mType == false)
                {
                    Machine.MachineRun armyMachine = new Machine.MachineRun(boxMirror.SelectedItem.ToString(), lRotor, mRotor, rRotor, boxErotor.SelectedItem.ToString(), Plugs());

                    txtOutput.Text = Parts.ExtraMethods.BlocksOfText(4, armyMachine.EncryptDecrypt(msg));
                    boxLrotorSet.SelectedIndex = armyMachine.RotorPos("l"); // shows positions of rotor after process
                    boxMrotorSet.SelectedIndex = armyMachine.RotorPos("m");
                    boxRrotorSet.SelectedIndex = armyMachine.RotorPos("r");
                }
                else
                {
                    Machine.MachineRun navyMachine = new Machine.MachineRun(boxMirror.SelectedItem.ToString(), M4Rotor, lRotor, mRotor, rRotor, boxErotor.SelectedItem.ToString(), Plugs());

                    txtOutput.Text = Parts.ExtraMethods.BlocksOfText(4, navyMachine.EncryptDecrypt(msg));
                    boxM4rotorSet.SelectedIndex = navyMachine.RotorPos("M4");
                    boxLrotorSet.SelectedIndex = navyMachine.RotorPos("l"); // shows positions of rotor after process
                    boxMrotorSet.SelectedIndex = navyMachine.RotorPos("m");
                    boxRrotorSet.SelectedIndex = navyMachine.RotorPos("r");
                }
            }
            else
                MessageBox.Show("Contains incorrect input", "Error");
        }

        private void RotorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // checks for correct rotor setting
            if (!Parts.Validator.IsRotorSet(rotors))
                errorRotors.SetError(boxRrotor, "Rotors numbers can not have the same settings");
            else
                errorRotors.Clear();
        }

        public void MaskedTextBoxes_Leave(object sender, EventArgs e)
        {
            // checks the plugboard for correct input
            if (!Parts.Validator.IsPlugSet(plugs))
                errorPlugs.SetError(boxPlug9, "Plug entered contains errors\nPlease check settings");
            else
                errorPlugs.Clear();
        }

        public void InputTextBox_Leave(object sender, EventArgs e)
        {
            if (!Parts.Validator.IsTextValid(Parts.ExtraMethods.ReplaceWhitespace(txtInput.Text, "")))
                errorInput.SetError(txtInput, "Letters only, no numbers\nor special characters");
            else
                errorInput.Clear();
        }

        private string[] Plugs()
        {
            return Parts.ExtraMethods.GetPlugs(plugs);
        }

        private void mnuReset_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void switchMachine_Click(object sender, EventArgs e)
        {
            if (mType == false)
            {
                // moves all rotors to the center for enigma "M4"
                boxLrotor.Location = new Point(boxLrotor.Location.X +37, boxLrotor.Location.Y);
                boxLrotorSet.Location = new Point(boxLrotorSet.Location.X+37, boxLrotorSet.Location.Y);
                boxLrotorRingSet.Location = new Point(boxLrotorRingSet.Location.X + 37, boxLrotorRingSet.Location.Y);
                boxMrotor.Location = new Point(boxMrotor.Location.X + 35, boxMrotor.Location.Y);
                boxMrotorSet.Location = new Point(boxMrotorSet.Location.X + 35, boxMrotorSet.Location.Y);
                boxMrotorRingSet.Location = new Point(boxMrotorRingSet.Location.X + 35, boxMrotorRingSet.Location.Y);
                boxRrotor.Location = new Point(boxRrotor.Location.X + 33, boxRrotor.Location.Y);
                boxRrotorSet.Location = new Point(boxRrotorSet.Location.X + 33, boxRrotorSet.Location.Y);
                boxRrotorRingSet.Location = new Point(boxRrotorRingSet.Location.X + 33, boxRrotorRingSet.Location.Y);

                // remove "I" mirrors  replace with "M4" mirrors
                Parts.ExtraMethods.RemoveIMirrors(boxMirror);
                Parts.ExtraMethods.AddM4Mirrors(boxMirror);

                // resets entire view 
                ResetAll();

                Enigma.ActiveForm.Text = "U-Boat Enigma - M4";
                boxM4rotor.Visible = true;
                boxM4rotorSet.Visible = true;
                boxM4rotorRingSet.Visible = true;
                mType = true;
            }
            else
            {
                // moves all rotors to the center for enigma "I"
                boxLrotor.Location = new Point(boxLrotor.Location.X - 37, boxLrotor.Location.Y);
                boxLrotorSet.Location = new Point(boxLrotorSet.Location.X - 37, boxLrotorSet.Location.Y);
                boxLrotorRingSet.Location = new Point(boxLrotorRingSet.Location.X - 37, boxLrotorRingSet.Location.Y);
                boxMrotor.Location = new Point(boxMrotor.Location.X - 35, boxMrotor.Location.Y);
                boxMrotorSet.Location = new Point(boxMrotorSet.Location.X - 35, boxMrotorSet.Location.Y);
                boxMrotorRingSet.Location = new Point(boxMrotorRingSet.Location.X - 35, boxMrotorRingSet.Location.Y);
                boxRrotor.Location = new Point(boxRrotor.Location.X - 33, boxRrotor.Location.Y);
                boxRrotorSet.Location = new Point(boxRrotorSet.Location.X - 33, boxRrotorSet.Location.Y);
                boxRrotorRingSet.Location = new Point(boxRrotorRingSet.Location.X - 33, boxRrotorRingSet.Location.Y);

                // remove "M4" mirrors and replace with "I" mirrors
                Parts.ExtraMethods.RemoveM4Mirrors(boxMirror);
                Parts.ExtraMethods.AddIMirrors(boxMirror);

                // resets entire view and removes conflict(s) between previous settings of the M4 rotor
                ResetAll();

                Enigma.ActiveForm.Text = "Army Enigma - I";
                boxM4rotor.Visible = false;
                boxM4rotorSet.Visible = false;
                boxM4rotorRingSet.Visible = false;
                mType = false;
            }
        }

        private void ResetAll()
        {
            plugs = new MaskedTextBox[13] { boxPlug1, boxPlug2, boxPlug3, boxPlug4, boxPlug5, boxPlug6, boxPlug7, boxPlug8, boxPlug9, boxPlug10, boxPlug11, boxPlug12, boxPlug13 };

            SetResetRotor();

            foreach (MaskedTextBox boxPlug in plugs)
                boxPlug.Clear();

            txtInput.Text = string.Empty;
            txtOutput.Text = string.Empty;
        }
        private void SetResetRotor()
        {
            boxMirror.SelectedIndex = 0;
            // rotor number
            boxM4rotor.SelectedIndex = 0;       // M4 rotor set to "VI"
            boxLrotor.SelectedIndex = 2;
            boxMrotor.SelectedIndex = 1;
            boxRrotor.SelectedIndex = 0;
            // rotor position
            boxM4rotorSet.SelectedIndex = 0;    // M4 rotor
            boxLrotorSet.SelectedIndex = 0;
            boxMrotorSet.SelectedIndex = 0;
            boxRrotorSet.SelectedIndex = 0;
            // ring setting
            boxM4rotorRingSet.SelectedIndex = 0;
            boxLrotorRingSet.SelectedIndex = 0;
            boxMrotorRingSet.SelectedIndex = 0;
            boxRrotorRingSet.SelectedIndex = 0;

            boxErotor.SelectedIndex = 0;
        }

    }
}
