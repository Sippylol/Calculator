using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        // Declare
        private bool checkLastChar;
        private bool checkLastOperator;
        private string lastNum;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Setup(); // Initialise
        }

        private void Setup()
        {
            checkLastChar = false;
            checkLastOperator = false;
            lastNum = textBoxPreview.Text;
        }

        private void AddChar(string mathInfoToAdd)
        {
            Intestning.AddChar(mathInfoToAdd, textBoxPreview);
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            AddChar("(-");
            checkLastChar = false;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            AddChar(".");
            checkLastChar = false;
        }


        private void btnEqual_Click(object sender, EventArgs e)
        {
            
            string result = "";

            if (textBoxPreview.TextLength == 0) return; // Return if there is no equation
            if (textBoxPreview.Text.Contains<char>('('))
                result = Operator.CheckBrackets(textBoxPreview.Text).Result;
            else if (textBoxPreview.Text.Contains<char>('*')
                || textBoxPreview.Text.Contains<char>('/')
                || textBoxPreview.Text.Contains<char>('%'))
                result = Operator.CheckMDM(textBoxPreview.Text).Result;
            else if (textBoxPreview.Text.Contains<char>('-')
                || textBoxPreview.Text.Contains<char>('+'))
                result = Operator.CheckPM(textBoxPreview.Text).Result;


            textBoxPreview.Text = result;
            
            // Reset the calculator
            Setup();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            lastNum += "0";
            if(float.Parse(lastNum) != 0/*More than one zero not allowed*/ || lastNum.Length == 1 || lastNum.Contains<char>('.'))
            {
                AddChar("0");
            }
            else
            {
                lastNum = lastNum.Substring(0, lastNum.Length - 1);
            }

            if (checkLastOperator)
            {
                checkLastChar = false;
            }
            
        }

        private void NumberClick(object sender, EventArgs e)
        {
            BtnNumberClicked(((Button)sender).Text);
        }

        private void BtnNumberClicked(string number)
        {
            AddChar(number);
            if (checkLastOperator)
            {
                checkLastChar = false;
            }
        }


        private void OperatorPress(object sender, EventArgs e)
        {
            if (textBoxPreview.Text == "") return;
            string lastChar = textBoxPreview.Text.Substring(textBoxPreview.TextLength - 1, 1);
            if (lastChar == "(") return;
            OperatorClicked();
            string currentOperator = ((Button)sender).Text;
            if (currentOperator == "÷") currentOperator = "/";
            else if (currentOperator == "×") currentOperator = "*";
            AddChar(currentOperator);
        }

        private void OperatorClicked()
        {
            if (checkLastChar)
            {
                // Remove the last operator and add the new one
                textBoxPreview.Text =
                    textBoxPreview.Text.Substring(0, textBoxPreview.Text.Length - 1);
            }
            
            checkLastChar = true;
            checkLastOperator = true;
        }


        // Add open and close brackets
        private void btnBracket_Click(object sender, EventArgs e)
        {
            string result = Intestning.AddBrackets(textBoxPreview.Text);
            textBoxPreview.AppendText(result);
            
        }

        private void textBoxPreview_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '(' || e.KeyChar == ')') btnBracket_Click(null, EventArgs.Empty);

            if(Operator.CheckOperator(e.KeyChar))
            {
                OperatorClicked();
                AddChar(e.KeyChar.ToString());
            }

            if (e.KeyChar == '=') btnEqual_Click(null, EventArgs.Empty);

            if (e.KeyChar == '.') btnDot_Click(null, EventArgs.Empty);

            if (e.KeyChar == '0') btnZero_Click(null, EventArgs.Empty);


            if (Operator.CheckNumber(e.KeyChar) && e.KeyChar != '.'
                && e.KeyChar != '0') BtnNumberClicked(e.KeyChar.ToString());


            if (e.KeyChar.GetHashCode() == 524296 || e.KeyChar.GetHashCode() == Keys.Delete.GetHashCode())
            {
                // Change the checkLastChar to determine whether the last char is an operator or not
                int length = textBoxPreview.Text.Length;
                if(length >= 2 && e.KeyChar.GetHashCode() == 524296)
                {
                    char lastChar = char.Parse(textBoxPreview.Text.Substring(textBoxPreview.Text.Length - 1, 1));
                    if (lastChar == '/' || lastChar == '*' || lastChar == '%'
                        || lastChar == '-' || lastChar == '+')
                    {
                        checkLastChar = false;
                    }
                }
                return;
            }
            e.Handled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBoxPreview.Text = "";
            Setup();
        }

        private void arcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircleArc ca = new CircleArc();
            ca.ShowDialog();
        }

        private void ciircumferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Circumference cf = new Circumference();
            cf.ShowDialog();
        }

        private void chordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chord c = new Chord();
            c.ShowDialog();
        }
        bool solve = false;
        private void solveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (solve)
                return;

            Solve s = new Solve();
            s.Show();
            solve = true;
        }
    }
}
