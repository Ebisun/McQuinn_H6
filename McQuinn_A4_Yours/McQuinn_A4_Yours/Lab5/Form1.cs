using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {   
        int loginAttCount = 0;
        string PROGRAMMER = "Hunter;
        public int GetRandom(int min, int max)
        {
            //Create a random object with no seed value.
            Random rand = new Random();;
            
            //Generate a random number between the min and max values sent in.
            int num = rand.Next(min, max);
            //Return the random number from the function
            return num;
        }
        public void ResetTextGrp()
        {
            txtString1.Text = "";
            txtString2.Text = "";
            lblResults.Text = "";
            txtString1.Focus();
            chkSwap.Checked = false;
            this.AcceptButton = btnJoin;
            this.CancelButton = btnReset;
        }
        public void ResetStatsGrp()
        {
            nudHowMany.Value = 10;
            lblSum.Text = "";
            lblMean.Text = "";
            lblOdd.Text = "";
            this.AcceptButton = btnGenerate;
            this.CancelButton = btnClear;
            lstNumbers.Items.Clear();
        }
        public void SetupOption()
        {
            if (radStats.Checked)
            {
                grpStats.Visible = true;
                grpText.Visible = false;
            }
            else if (radText.Checked)
            {
                grpText.Visible = true;
                grpStats.Visible = false;
            }
        }
        public void Swap(ref string strOne, string strTwo)
        {
            string placeholder = strOne;
            strOne = strTwo;
            strTwo = placeholder;

        }
        public bool CheckInput()
        {
            bool result = true;
            if (txtString1.Text == "" && txtString2.Text != "")
                result = false;
            return result;
        }
        public int AddList()
        {
            int sum = 0, count = 0;
            while (count < lstNumbers.Items.Count)
            {
                sum += Convert.ToInt32(lstNumbers.Items[count]);
                count++;
            }
            return sum;
        }
        public int CountOdd()
        {
            int count = 0, oddCount = 0;
            do
            {
                if (Convert.ToInt32(lstNumbers.Items[count]) % 2 != 0)
                {
                    oddCount++;
                }
                count++;
            }
            while (count < lstNumbers.Items.Count);
            return oddCount;
        }
        public Form1()
        {
            InitializeComponent();
            //Add your name to the end of the form title using the constant. Do not type
            this.Text == PROGRAMMER;
            //Lab 5 by (it is already there!). Hide all the groups except Login.
            grpChoose.Visible =false;
            grpStats.Visible =false;
            grpText.Visible =false;
            //Put the cursor in the textbox for code.
            txtCode.Focus();
            //Call the function to get a random between 100,000 and 200,000. 
            int randNum = GetRandom(100000, 200001);
            lblCode.Text = randNum.ToString(); 
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (loginAttCount <= 3)
            {
                //Count how many login attempts are made where the code does not match.
                loginAttCount++;
                if (lblCode.Text == txtCode.Text)
                {
                    //If it matches, show the Choose group and disable the Login group.
                    grpChoose.Visible = true;
                    btnLogin.Enabled = false;

                }
                //The user will get a message if less than 3 and your program will select what has been typed in the textbox.
                else
                {
                    MessageBox.Show(loginAttCount.ToString() + " Incorrect code entered\nTry again - only 3 attempts allowed", PROGRAMMER);
                    txtCode.SelectAll();
                    txtCode.Focus();
                }
            }
            else
            {
                //Once the user has attempted to login 3 times without success, they will get this message and the program will close.
                MessageBox.Show("3 attempts to login\nAccount locked - Closing program", PROGRAMMER);
                this.Close();
            }

        }

        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }

        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }

        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                string strOne = txtString1.Text;
                string strTwo = txtString2.Text;

                Swap(ref strOne, ref strTwo);

                txtString1.Text = strOne;
                txtString2.Text = strTwo;
                lblResults.Text += "Strings have been swapped\n";
            }

        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            lblResults.Text += "First String: " + txtString1.Text + "\nSecond String: " + txtString2.Text + "\nJoined = " + txtString1.Text + " --> " + txtString2.Text + "\n";
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            lblResults.Text += "First String: " + txtString1.Text + "\nlength = " + txtString1.Text.Length + "\nSecond String: " + txtString2.Text + "\nlength = " + txtString2.Text.Length + "\n";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Create a random object with a seed value of 733.
            Random rand = new Random(733);
            //Clear the listbox before writing to it. Run a for loop generating a random number between 1000 and 5000 and writing it to the listbox.
            lstNumbers.Items.Clear();
            for (int i=0; i < nudHowMany.Value; i++)
            {
                lstNumbers.Items.Add(rand.Next(1000, 5001));
            }
            //The numeric updown is how many numbers to generate. This loop does not do any analysis.
            //Call the function AddList to get the sum. Display in the label formatted with commas and no decimal places.
            lblSum.Text = AddList().ToString();
            //Calculate the mean and display in the label formatted with commas and 2 decimal places.
            double mean = AddList()/Convert.ToDouble(lstNumbers.Items.Count);
            lblMean.Text = mean.ToString("f2");
            //Call the function CountOdd returning the number of odd numbers found in the list. Display in the label.
            lblOdd.Text = Convert.ToString(CountOdd());
        }
    }
}
