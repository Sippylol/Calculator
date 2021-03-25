using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    class Operator
    {
        string equation; // Declare

        protected Operator(string equation)
        {
            this.equation = equation;
        }




        internal static async Task<string> CheckBrackets(string equation)
        {
            equation = Operator.CloseBrackets(equation);

            string lastNum = GetLastNumber(equation);

            bool bracketIsOpen = false;
            int start = 0;
            do
            {
                // Check for the brackets
                for (int i = 0; i < equation.Length; i++)
                {
                    // Check for the first bracket
                    string currentChar = equation.Substring(i, 1);
                    if (currentChar == "(")
                    {
                        bracketIsOpen = true;
                        start = i;
                    }

                    if (bracketIsOpen && currentChar != ")")
                    {
                        // Check for nested bracket
                        if (currentChar == "(")
                        {
                            bracketIsOpen = true;
                            lastNum = "";
                            start = i;
                            continue;
                        }

                        lastNum += currentChar;

                    }


                    if (currentChar == ")")
                    {
                        // Solve the first bracket
                        equation = equation.Substring(0, start)
                            + await CheckMDM(lastNum)
                            + equation.Substring(i + 1);

                        //counter++;
                        //MessageBox.Show(brackets.Last());
                        //CheckBrackets(lastNum);
                        bracketIsOpen = false;
                        break;
                    }
                }
            }
            while (equation.Contains<char>('('));

            return await CheckMDM(equation);
        }

        // Check for the second precedence *, / or %
        internal static async Task<string> CheckMDM(string equation)
        {
            string beforeOperator = ""; // Contains the number before the operator
            string afterOperator = ""; // Contains the number after the operator
            string currentOperator = ""; // Contains the current operator

            int start = 0;
            int end = 0;

            while (equation.Contains<char>('*')
                || equation.Contains<char>('/')
                || equation.Contains<char>('%'))
            {
                for (int i = 0; i < equation.Length; i++)
                {
                    string currentChar = equation.Substring(i, 1);

                    if (currentChar == "*" || currentChar == "/"
                        || currentChar == "%")
                    {
                        currentOperator = currentChar;
                        start = i;
                        // Save the number before the operator
                        while (true)
                        {
                            start--;
                            if (start > 0)
                            {
                                currentChar = equation.Substring(start, 1);
                                if (currentChar == "/" || currentChar == "*"
                                    || currentChar == "%" || currentChar == "-"
                                    || currentChar == "+")
                                {
                                    beforeOperator = equation.Substring(start, i - start);
                                    break;
                                }
                            }
                            else
                            {
                                beforeOperator = equation.Substring(0, i);
                                break;
                            }
                        }

                        end = i + 1;
                        // Save the number after the operator
                        while (true)
                        {
                            if (end <= equation.Length - 1)
                            {
                                currentChar = equation.Substring(end, 1);
                                if ((currentChar == "/" || currentChar == "*"
                                    || currentChar == "%" || currentChar == "-"
                                    || currentChar == "+") && end != (i + 1))
                                {
                                    afterOperator = equation.Substring(i + 1, end - (i + 1));
                                    break;
                                }
                            }
                            else
                            {
                                afterOperator = equation.Substring(i + 1, end - (i + 1));
                                break;
                            }
                            end++;
                        }

                        // Calculate the current equation
                        double before = double.Parse(beforeOperator);
                        double after = double.Parse(afterOperator);
                        double result = 0;
                        // Calculate accordingly to the operator
                        switch (currentOperator)
                        {
                            case "*":
                                result = before * after;
                                break;
                            case "/":
                                if (after == 0)
                                {
                                    MessageBox.Show("Error #2: divide by zero!");
                                    Environment.Exit(1);
                                }
                                result = before / after;
                                break;
                            case "%":
                                result = before % after;
                                break;
                        }
                        equation = equation.Substring(0, start)
                            + result
                            + equation.Substring(end, equation.Length - end);
                        //MessageBox.Show(equation);
                        break;

                    }

                }
            }

            return await CheckPM(equation);
        }

        // Check minus and plus and solve them
        internal static async Task<string> CheckPM(string equation)
        {
            string beforeOperator = ""; // Contains the number before the operator
            string afterOperator = ""; // Contains the number after the operator
            string currentOperator = ""; // Contains the current operator

            int start = 0;
            int end = 0;

            while (equation.Contains<char>('-')
                || equation.Contains<char>('+'))
            {
                // Check if the equation contains a real - or only negative number
                if (equation.Substring(0, 1) == "-")
                {
                    string checkNegative = equation.Substring(1);
                    if (!checkNegative.Contains<char>('-') && !checkNegative.Contains<char>('+'))
                        break;
                }

                for (int i = 0; i < equation.Length; i++)
                {
                    string currentChar = equation.Substring(i, 1);

                    if (currentChar == "+" || currentChar == "-")
                    {
                        currentOperator = currentChar;
                        start = i;
                        // Save the number before the operator
                        while (true)
                        {
                            start--;
                            if (start > 0)
                            {
                                currentChar = equation.Substring(start, 1);
                                if (currentChar == "-" || currentChar == "+")
                                {
                                    beforeOperator = equation.Substring(start, i - start);
                                    break;
                                }
                            }
                            else
                            {
                                beforeOperator = equation.Substring(0, i);
                                break;
                            }
                        }

                        end = i + 1;
                        // Save the number after the operator
                        while (true)
                        {
                            if (end <= equation.Length - 1)
                            {
                                currentChar = equation.Substring(end, 1);
                                if (currentChar == "+" || currentChar == "-")
                                {
                                    afterOperator = equation.Substring(i + 1, end - (i + 1));
                                    break;
                                }
                            }
                            else
                            {
                                afterOperator = equation.Substring(i + 1, end - (i + 1));
                                break;
                            }
                            end++;
                        }

                        // Calculate the current equation
                        double before = double.Parse(beforeOperator);
                        double after = double.Parse(afterOperator);
                        double result = 0;
                        // Calculate accordingly to the operator
                        switch (currentOperator)
                        {
                            case "+":
                                result = before + after;
                                break;
                            case "-":
                                result = before - after;
                                break;
                        }
                        equation = equation.Substring(0, start)
                            + result
                            + equation.Substring(end, equation.Length - end);
                        //MessageBox.Show(equation);
                        break;

                    }

                }
            }

            return equation;
        }











        // Function to extract the last number until the first operator
        internal static string GetLastNumber(string equation)
        {
            string number = "";
            string currentChar = "";
            // Check if the equation is empty
            if (equation == "")
                return "";

            // Get the last insered number
            for(int i = equation.Length - 1; i >= 0; i--)
            {
                currentChar = equation.Substring(i, 1);
                if (CheckOperator(char.Parse(currentChar)) || currentChar == "(" || currentChar == ")")
                {
                    break;
                }

                number = currentChar + number; // Invert adding

            }
            return number;
        }

        // Function to close the brackets
        internal static string CloseBrackets(string equation)
        {
            string currentChar = "";
            int bracketsCounter = 0;
            // Check if the equation contains open brackets
            if (!equation.Contains<char>('('))
                return "";

            for (int i = 0; i < equation.Length; i++)
            {
                currentChar = equation.Substring(i, 1);
                if (currentChar == "(")
                    bracketsCounter++;
                else if (currentChar == ")")
                    bracketsCounter--;
            }

            if(bracketsCounter > 0)
            {
               for(int i = 0; i < bracketsCounter; i++)
                {
                    equation += ")";
                }
            }

            return equation;
        }

        internal static bool CheckNumber(char checkNumber)
        {
            bool number = false; // Declare && Initialise 

            if(checkNumber == '1' || checkNumber == '2'
                || checkNumber == '3' || checkNumber == '4'
                || checkNumber == '5' || checkNumber == '6'
                || checkNumber == '7' || checkNumber == '8'
                || checkNumber == '9' || checkNumber == '0'
                || checkNumber == '.')
            {
                number = true;
            }

            return number; // Result
        }

        internal static bool CheckOperator(char checkOperator)
        {
            bool operatorChar = false; // Declare && Initialise

            if(checkOperator == '*' || checkOperator == '/'
                || checkOperator == '%'
                || checkOperator == '+' || checkOperator == '-')
            {
                operatorChar = true;
            }

            return operatorChar;
        }
    }
}
