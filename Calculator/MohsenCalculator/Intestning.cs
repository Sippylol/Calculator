using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MohsenCalculator
{
    internal static class Intestning
    {
        internal static string AddBrackets(string valueToCheck)
        {
            string bracket = "";
            string currentChar;

            // Check if the valueToCheck is empty
            if (valueToCheck == "")
                return "(";

            currentChar = valueToCheck.Substring(valueToCheck.Length - 1, 1);
            // Check if the last char is open brackets
            if (currentChar == "(")
            {
                return ""; // Return
            }



            // Count open brackets
            int bracketsCount = 0;
            for (int i = 0; i < valueToCheck.Length; i++)
            {
                currentChar = valueToCheck.Substring(i, 1);

                if (currentChar == "(")
                    bracketsCount++;

                if (currentChar == ")")
                    bracketsCount--;

            }

            currentChar = valueToCheck.Substring(valueToCheck.Length - 1, 1);

            // Check if the last char is close bracket
            if (currentChar == ")")
            {
                if (bracketsCount > 0)
                    return ")";
                else
                    return "*("; // Return
            }

            // Check if the last char is operator
            if (Operator.CheckOperator(char.Parse(currentChar)))
            {
                // Check if the equation contains more than one char
                if (valueToCheck.Length > 1)
                {
                    // Check if the last operator is minus
                    if (currentChar == "-")
                    {
                        currentChar = valueToCheck.Substring(valueToCheck.Length - 2, 1);
                        // Check if there is a number before this operator
                        if (Operator.CheckNumber(char.Parse(currentChar)))
                        {
                            return "(";
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else return "(";

                }

            }

            // Check if the equation contains open bracket
            if (valueToCheck.Contains<char>('('))
            {
                // Extract the equation inside the last bracket (backwards)
                for (int i = valueToCheck.Length - 1; i >= 0; i--)
                {
                    currentChar = valueToCheck.Substring(i, 1);


                    // Break when finding bracket
                    if (currentChar == "(" || currentChar == ")")
                        break;

                    // Add the last finded char as the first char inside the string
                    bracket = currentChar + bracket;
                }
            }
            else
            {
                bracket = valueToCheck;
            }

            string lastChar = bracket.Substring(bracket.Length - 1, 1);
            //MessageBox.Show(bracket + " : " + bracketsCount);
            // Check if the last char is number
            if (Operator.CheckNumber(char.Parse(lastChar)))
            {
                // check if this is negative number
                if (bracket.Substring(0, 1) == "-")
                {
                    return ")";
                }
                else
                {
                    bool checkOperator = false;
                    // Check if the equation after the '(' contains operators
                    for (int i = 0; i < bracket.Length; i++)
                    {
                        currentChar = bracket.Substring(i, 1);
                        checkOperator = Operator.CheckOperator(char.Parse(currentChar));

                        if (checkOperator)
                        {
                            if (bracketsCount > 0)
                                return ")";
                            else
                                return "*(";
                        }
                    }
                    if (!checkOperator)
                        return "*(";
                }
            }
            else
            {
                return "*(";
            }

            return bracket;
        }

        internal static void AddChar(RichTextBox writingArea, string mathInfoToAdd)
        {
            string lastNum = Operator.GetLastNumber(writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())]);

            bool validation;
            if (mathInfoToAdd == "(-")
            {
                if (writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Length > 0)
                {
                    if (writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Substring(writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Length - 1, 1) == ")")
                        WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea,"*(-");
                }
                else
                {
                    WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea, "(-");
                }

                return;
            }
            else
            {
                validation = Operator.CheckNumber(char.Parse(mathInfoToAdd))
                || Operator.CheckOperator(char.Parse(mathInfoToAdd));
            }

            if (validation)
            {

                if (Operator.CheckNumber(char.Parse(mathInfoToAdd))
                    && lastNum != "" && !lastNum.Contains<char>('.'))
                {
                    if (float.Parse(lastNum) == 0 && writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Length != 0)
                    {
                        WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea,
                            writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Substring(0
                            , writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())].Length - 1));
                    }
                }
                else
                {
                    if (mathInfoToAdd == "." && lastNum == "")
                    {
                        mathInfoToAdd = "0.";
                    }
                    else if (mathInfoToAdd == "." && lastNum.Contains(".")) mathInfoToAdd = "";

                }

                WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea, mathInfoToAdd);

                //MessageBox.Show(writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())] + " : " + mathInfoToAdd);

            }
            else MessageBox.Show("Error #1: Something went wrong! --> unvaild character");
        }
    }
}
