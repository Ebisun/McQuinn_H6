using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    /*
     * Author: Damian Bernatchez
     * for Cathy Lab 5 2022-12-06
     * 
     */
    public partial class Form1 : Form
    {
        const string PROGRAMMER = "Damian Bernatchez";
        public Form1()
        {
            InitializeComponent();
        }
        int GetRandom(int min, int max)
        {
            Random randomNumber = new Random();

            return randomNumber.Next(min, max);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " "+PROGRAMMER;
            grpChoose.Hide();
            grpText.Hide();
            grpStats.Hide();
            txtCode.Focus();
            lblCode.Text = GetRandom(100000, 200000).ToString();
        }
        int incorrectGuesses = 0;

        //This button click does login thingys
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int attemptsAllowed = 3;
            if (int.TryParse(txtCode.Text, out int authCode))
            {
                if (incorrectGuesses == attemptsAllowed)
                {
                    MessageBox.Show(attemptsAllowed + " attempts to login\nAccount locked - Closing program", PROGRAMMER);
                    this.Close();
                }
                if (authCode == int.Parse(lblCode.Text))
                {
                    grpLogin.Enabled = false;
                    grpChoose.Show();
                }
                else
                {
                    incorrectGuesses++;
                    MessageBox.Show(incorrectGuesses + " incorrect code(s) entered\nTry Again - only "+ attemptsAllowed +" attempts allowed",PROGRAMMER);
                }
            }
            else
            {
                MessageBox.Show("Not an integer",PROGRAMMER);
            }
        }
        //This function clears the Text Group box
        void ResetTextGrp()
        {
            this.AcceptButton = btnJoin;
            this.CancelButton = btnReset;
            txtString1.Text = "";
            txtString2.Text = "";
            lblResults.Text = "";
            chkSwap.Checked = false;
        }
        //This function clears the Stats group box
        void ResetStatsGrp()
        {
            this.AcceptButton = btnGenerate;
            this.CancelButton = btnClear;
            lblSum.Text = "";
            lblMean.Text = "";
            lblOdd.Text = "";
            lstNumbers.Items.Clear();
            nudTimes.Value = 10;
        }
        //This function is used to check if the Text or the Stats radio button was clicked
        void SetupOption()
        {
            if (radText.Checked)
            {
                grpStats.Hide();
                grpText.Show();
                ResetTextGrp();
            }
            if (radStats.Checked)
            {
                grpText.Hide();
                grpStats.Show();
                ResetStatsGrp();
            }
        }
        //This is the button click event for Text Reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }
        //This is the Button Click event for the Stats Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }
        //This is the checked changed event for the Text Radio Button
        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }
        //This is the checked changed event for the Stats radio button
        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }
        //This function takes two strings and swaps them
        void Swap(string string1, string string2)
        {
            txtString1.Text = string2;
            txtString2.Text = string1;
            lblResults.Text = "Strings have been swapped!";
        }
        //This function verifies that there is data in the field
        bool CheckInput()
        {
            bool result = true;
            if (!(txtString1.Text.Length > 0))
            {
                result = false;
            }
            if (!(txtString2.Text.Length > 0))
            {
                result = false;
            }
            return result;
        }
        //This checks to see if the Swap Checkbox was clicked
        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSwap.Checked)
                Swap(txtString1.Text, txtString2.Text);
        }
        //This button click does stuff
        private void btnJoin_Click(object sender, EventArgs e)
        {
            lblResults.Text = "First string = " + txtString1.Text +"\nSecond string = " + txtString2.Text + "\nJoined = " + txtString1.Text + "-->"+ txtString2.Text;
        }
        //This button click returns the character length of string1 and string2
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            lblResults.Text = "First string = " + txtString1.Text + "\n Characters = " + txtString1.Text.Length + "\nSecond string = " + txtString2.Text + "\n Characters = " + txtString2.Text.Length;
        }
        //This button generates the random numbers in the listbox
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Random rand = new Random(733);
            for (int i = 0;i < nudTimes.Value; i++)
            {
                lstNumbers.Items.Add(rand.Next(1000,5001));
            }
            lblSum.Text = AddList().ToString();
            lblMean.Text = (AddList() / (lstNumbers.Items.Count*1.0)).ToString("f2");
            lblOdd.Text = CountOdd().ToString();
        }
        //this function returns the sum of the list box
        int AddList()
        {
            int count = 0;
            int sum = 0;
            while(count < lstNumbers.Items.Count)
            {

                sum += Convert.ToInt32(lstNumbers.Items[count]);
                count++;
            }
            return sum;
        }
        //this function returns the ammount of odd numbers in the list box
        int CountOdd()
        {
            int count = 0;
            int oddCount = 0;
            do
            {
                if ((Convert.ToInt32(lstNumbers.Items[count]) % 2) != 0)
                {
                    oddCount++;
                }
                count++;
            } while (count < lstNumbers.Items.Count);
            return oddCount;
        }
    }   
}