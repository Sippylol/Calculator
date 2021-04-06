using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MohsenCalculator
{
    internal static class Operator
    {
        internal static string CheckBrackets(string equation)
        {
            // This step is used to close all the unclosed brackets to avoid any error
            equation = Operator.CloseBrackets(equation);

            string lastNum = "";
            bool bracketIsOpen = false; // This boolean is used to check if the current char is a number
            int start = 0;
            do
            {
                // Check for the brackets
                for (int i = 0; i < equation.Length; i++)
                {
                    // Check for the first open bracket
                    string currentChar = equation.Substring(i, 1); // Extract the current char
                    if (currentChar == "(") // Check if the current char is open brackets
                    {
                        bracketIsOpen = true;
                        lastNum = ""; // This string is used to remeber the current equation inside this bracket
                        start = i; // This int is used to remember the start of the current equation
                        continue; // Go to the next round
                    }

                    // Check if an open bracket was found
                    if (bracketIsOpen && currentChar != ")")
                    {
                        lastNum += currentChar; // Add this char to the equation
                        continue;
                    }

                    // Check if the current char is close bracket
                    if (currentChar == ")")
                    {
                        // Solve the equation and replace it with the result
                        equation = equation.Substring(0, start) // The chars before the open bracket of this equation
                            + (lastNum.Contains<char>('-') || lastNum.Contains<char>('+')
                            || lastNum.Contains<char>('*') || lastNum.Contains<char>('/') 
                            || lastNum.Contains<char>('%') ? CheckMDM(lastNum) : lastNum) // Calculate the equation inside this bracket
                            + equation.Substring(i + 1); // The chars after the open bracket


                        lastNum = ""; // Make sure to remove the value of the last calculated part-equation

                        // Remove any useless operator
                        equation = equation.Replace("+-", "-");

                        bracketIsOpen = false; // This bracket is closed
                        break;
                    }
                }
            }
            while (equation.Contains<char>('(')); // Continue this block of code as long as there is open bracket in the equation

            return CheckMDM(equation); // Send the equation to be solved in the next step and return the solution when it is ready
        }

        // Check for the second precedence *, / or %
        internal static string CheckMDM(string equation)
        {
            string beforeOperator = ""; // Contains the number before the operator
            string afterOperator; // Contains the number after the operator
            string currentOperator; // Contains the current operator

            int start = 0;
            int end;

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
                                    return "";
                                    //Environment.Exit(1);
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

                    }else if (CheckNumber(currentChar.ToCharArray()[0]))
                    {
                        if (beforeOperator == "")
                            start = i; // Will be used to replace this number with the result

                        beforeOperator += currentChar;
                    }else if (CheckOperator(currentChar.ToCharArray()[0]))
                    {
                        beforeOperator = "";
                    }

                }
            }

            return CheckPM(equation);
        }

        // Check minus and plus and solve them
        //internal static async Task<string> CheckPM(string equation)
        internal static string CheckPM(string equation)
        {
            string beforeOperator; // Contains the number before the operator
            string afterOperator; // Contains the number after the operator
            string currentOperator; // Contains the current operator

            int start;
            int end;

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

                    if ((currentChar == "+" || currentChar == "-") && i != 0)
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
                                if ((currentChar == "+" || currentChar == "-") && end != i + 1)
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
            string currentChar;
            // Check if the equation is empty
            if (equation == "")
                return "";

            // Get the last inserted number
            for (int i = equation.Length - 1; i >= 0; i--)
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
            string currentChar;
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

            if (bracketsCounter > 0)
            {
                for (int i = 0; i < bracketsCounter; i++)
                {
                    equation += ")";
                }
            }

            return equation;
        }

        internal static bool CheckNumber(char checkNumber)
        {
            bool number = false; // Declare && Initialise 

            if (checkNumber == '1' || checkNumber == '2'
                || checkNumber == '3' || checkNumber == '4'
                || checkNumber == '5' || checkNumber == '6'
                || checkNumber == '7' || checkNumber == '8'
                || checkNumber == '9' || checkNumber == '0'
                || checkNumber == '.' || checkNumber == ',')
            {
                number = true;
            }

            return number; // Result
        }

        internal static bool CheckOperator(char checkOperator)
        {
            bool operatorChar = false; // Declare && Initialise

            if (checkOperator == '*' || checkOperator == '/'
                || checkOperator == '%'
                || checkOperator == '+' || checkOperator == '-')
            {
                operatorChar = true;
            }

            return operatorChar;
        }


        internal static string[] OneUnknownVarSolveGD(string equation)
        {
            //string[] equationParts = equation;

            string[] equationParts = equation.Split('=');

            char currentOperation = ' ';
            string currentNumber = ""; /*Initialize*/
            string unknownVar = "";

            for (int i = 0/*Initialize*/; i < equationParts[0].Length/*Condition*/; i++/*Increment || Decrement*/)
            {
                /*(int)1.1 <-- TypeCasting*/
                if (CheckNumber(equationParts[0][i])/* && (currentOperation == '/' || currentOperation == '*')Condition*/)
                {
                    currentNumber += equationParts[0][i];
                    if(i == equationParts[0].Length - 1)
                    {
                        if (currentOperation == '*')
                        {
                            equationParts[1] = "(" + equationParts[1] + ")/" + currentNumber;

                        }

                        else if (currentOperation == '/' && unknownVar == "")
                        {
                            equationParts[1] = currentNumber + "/(" + equationParts[1] + ")";
                        }
                        else if (currentOperation == '/')
                        {
                            equationParts[1] = currentNumber + "*(" + equationParts[1] + ")";

                        }
                    }

                }
                else if ((CheckOperator(equationParts[0][i]) || (currentOperation != ' ' && i == equationParts[0].Length - 1)) && currentNumber != "")
                {
                    /*if (unknownVar != "")
                    {
                        unknownVar = "";

                        currentOperation = equationParts[0][i];
                        continue;

                    }*/


                    if (equationParts[0][i] == '*')
                    {
                        equationParts[1] = "(" + equationParts[1] + ")/" + currentNumber;

                    }

                    else if (equationParts[0][i] == '/' && unknownVar == "")
                    {
                        equationParts[1] = currentNumber + "/(" + equationParts[1] + ")";
                    }
                    else if(equationParts[0][i] == '/')
                    {
                        equationParts[1] = currentNumber + "*(" + equationParts[1] + ")";

                    }



                    currentOperation = equationParts[0][i];

                    currentNumber = "";
                }
                else if (CheckOperator(equationParts[0][i]))
                {
                    currentOperation = equationParts[0][i];
                }
                else
                {
                    unknownVar += equationParts[0][i];
                }



            }

            return new string[] { equationParts[1], unknownVar };
        }
    




        internal static string[] OneUnknownVarSolve(string equation)
        {

            

            string[] equationParts = equation.Split('=');


            char currentOperation = ' ';
            string currentNumber = ""; /*Initialize*/
            string unknownVar = "";

            string equationAfterMovingPlusAndMenusParts = "";

            //int start = 0;

            

            for (int i = 0/*Initialize*/; i < equationParts[0].Length/*Condition*/; i++/*Increment || Decrement*/)
            {
                /*(int)1.1 <-- TypeCasting*/
                if (CheckNumber(equationParts[0][i])/*Condition*/)
                {
                    currentNumber += equationParts[0][i];
                    if (i == equationParts[0].Length - 1)
                    {

                        if (unknownVar != "")
                        {
                            unknownVar = "";
                            currentOperation = equationParts[0][i];
                            continue;
                        }

                        if (currentOperation == ' ' || currentOperation == '+')
                        {
                            equationParts[1] += "-" + currentNumber;
                            Regex regex = new Regex(Regex.Escape("+" + currentNumber));
                            equationAfterMovingPlusAndMenusParts = regex.Replace(equationParts[0], "", 1);
                            

                        }
                        else if (currentOperation == '-')
                        {
                            equationParts[1] += "+" + currentNumber;
                            Regex regex = new Regex(Regex.Escape("-" + currentNumber));
                            equationAfterMovingPlusAndMenusParts = regex.Replace(equationParts[0], "", 1);
                        }


                        currentOperation = equationParts[0][i];
                        currentNumber = "";

                    }
                }
                else if (CheckOperator(equationParts[0][i]))
                {
                    /*if (unknownVar != "")
                    {
                        unknownVar = "";

                        currentOperation = equationParts[0][i];
                        continue;

                    }*/


                    if (equationParts[0][i] == ' ' || equationParts[0][i] == '+')
                    {
                        equationParts[1] += "-" + currentNumber;
                        if (currentOperation == '+')
                        {
                            Regex regex = new Regex(Regex.Escape("+" + currentNumber));
                            equationAfterMovingPlusAndMenusParts = regex.Replace(equationParts[0], "", 1);
                        }
                        else
                        {
                            Regex regex = new Regex(Regex.Escape(currentNumber + "+"));
                            equationAfterMovingPlusAndMenusParts = regex.Replace(equationParts[0], "", 1);
                        }

                    }

                    else if (equationParts[0][i] == '-')
                    {
                        equationParts[1] += "+" + currentNumber;
                        Regex regex = new Regex(Regex.Escape("-" + currentNumber));
                        equationAfterMovingPlusAndMenusParts = regex.Replace(equationParts[0], "", 1);
                    }



                    currentOperation = equationParts[0][i];

                    currentNumber = "";
                }

                else
                {
                    if (i != 0)
                        if (equationParts[0][i - 1] == '-')
                            unknownVar += '-';

                    unknownVar += equationParts[0][i];
                }

                
            }

            //MessageBox.Show(equationParts[0] + " : " + equationParts[1]);

            //equation = OneUnknownVarSolveGD(equationParts);

            //MessageBox.Show(equation[0] + " : " + equation[1]);


            return equation.Contains<char>('*') || equation.Contains<char>('/') ? OneUnknownVarSolveGD(equationAfterMovingPlusAndMenusParts + "=" + equationParts[1]) : new string[] { equationParts[1], unknownVar };
        }

    }
}

