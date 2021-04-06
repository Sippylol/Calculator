using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MohsenCalculator
{
    internal static class WritingAreaEventsMethods
    {

        internal static List<int> mathLines = new List<int>();
        internal static string path;
        internal static Tab currentTab;

        internal static string ReplaceVarKeyWithItsValue(string equation)
        {
            
                
            while (true)
            {
                bool noMoreVars = true;
                for (int varIndex = 0; varIndex < currentTab.variables.Count; varIndex++)
                {
                    // Check if the equation contains this variable
                    if (equation.Contains(currentTab.variables.ElementAt(varIndex).Key))
                    {
                        noMoreVars = false;
                        // Exchange the var name with var value to calculate it later
                        equation = equation.Replace(currentTab.variables.ElementAt(varIndex).Key, "(" + currentTab.variables.ElementAt(varIndex).Value + ")");
                    }
                }
                
                // if noMoreVars is true, that means the for loop didn't find any variablesKey match in a whole run
                if (noMoreVars)
                    break;
            }

            return equation;
        }


        internal static void RichTextBoxWriteArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {

                ConvertToNormalOrMathLine((RichTextBox)sender);
                //writingArea.ForeColor = Color.SkyBlue;




            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Chgeck if this line is mathematical and contains an variables
                RichTextBox writingArea = (RichTextBox)sender;
                int currentLine = writingArea.GetLineFromCharIndex(writingArea.SelectionStart);
                //int firstCharOfLineIndex = writingArea.GetFirstCharIndexOfCurrentLine();

                if (writingArea.Lines.Length == 0)
                    return;

                if (writingArea.Lines[currentLine].Contains(":="))
                {
                    if (writingArea.Lines[currentLine].Contains(';'))
                    {
                        string[] varsInfo = writingArea.Lines[currentLine].Split(';');

                        // Make a for loop to extract the variables from the current line
                        for(int currentVarIndex = 0; currentVarIndex < varsInfo.Length; currentVarIndex++)
                        {
                            if (varsInfo[currentVarIndex] == "")
                                continue;

                            // Replace the := mark with a line, to make the work easier and the user of split funktion posiible
                            string[] varInfo = varsInfo[currentVarIndex].Replace(":=", "\n").Replace(" ", "").Split('\n');
                            if (varInfo.Length == 2)
                            {
                                // Check if the variable already exist
                                if (currentTab.variables.ContainsKey(varInfo[0]))
                                    currentTab.variables.Remove(varInfo[0]);

                                currentTab.variables.Add(varInfo[0], varInfo[1]); // Add the var info to the variables' dicitionary in this tab
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show(writingArea.Lines[currentLine]);
                        // Replace the := mark with a line, to make the work easier and the user of split funktion posiible
                        string[] varInfo = writingArea.Lines[currentLine].Replace(":=", "\n").Replace(" ", "").Split('\n');
                        if (varInfo.Length == 2)
                        {
                            // Check if the variable already exist
                            if (currentTab.variables.ContainsKey(varInfo[0]))
                                currentTab.variables.Remove(varInfo[0]);

                            currentTab.variables.Add(varInfo[0], varInfo[1]);
                            //MessageBox.Show("Variable was added: " + currentTab.variables.ElementAt(currentTab.variables.Count-1).Key);
                        }
                    }
                    
                }
                

                writingArea.SelectionBackColor = Color.White;
                //MessageBox.Show(e.KeyCode.ToString());
            }

            
        }

        internal static void ConvertToNormalOrMathLine(RichTextBox writingArea)
        {
            int currentLine = writingArea.GetLineFromCharIndex(writingArea.SelectionStart);
            int firstCharOfLineIndex = writingArea.GetFirstCharIndexOfCurrentLine();
            if (currentLine > 0)
                writingArea.Select(firstCharOfLineIndex, writingArea.Lines[currentLine].Length);

            // Check if this line is added to the mathmatics lines
            if (!mathLines.Contains(currentLine))
            {

                writingArea.SelectionBackColor = Color.SkyBlue;
                mathLines.Add(currentLine);
            }
            else
            {
                mathLines.Remove(currentLine);
                writingArea.SelectionBackColor = Color.White;
            }
        }

        internal static void RichTextBoxWriteArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            RichTextBox writingArea = (RichTextBox)sender;
            if (currentTab != null)
            {
                currentTab.closeBtn.Text = "O";
                currentTab.fileText = currentTab.writingArea.Text;
            }

            // Check if the pressed button is a normal key, check if there is mathematical lines and the current line that the user is writing on is mathematical. 
            // Check if the pressed key is not the Delete key (<-) and not the real delete key
            if(mathLines != null && mathLines.Contains(writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())) && e.KeyChar.GetHashCode() != 524296)
            {
                e.Handled = true;

                //writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine()) - 1] = e.KeyChar.ToString();
                //writingArea.AppendText(e.KeyChar.ToString());
                //MessageBox.Show(writingArea.SelectionStart.ToString());


                AddCharToMathlineAtCharIndex(writingArea, e.KeyChar.ToString());
                //Intestning.AddChar(writingArea, e.KeyChar.ToString());
            }

        }

        static internal void AddCharToMathlineAtCharIndex(RichTextBox writingArea, string charactersToAdd)
        {
            string[] writingAreaCopy = (string[])writingArea.Lines.Clone();

            if (writingAreaCopy.Length == 0) return; // Make sure there is a line to write on, in this richtextbox

            writingAreaCopy[writingArea.GetLineFromCharIndex(writingArea.GetFirstCharIndexOfCurrentLine())] += charactersToAdd;

            int positionOfCurrentChar = writingArea.SelectionStart + charactersToAdd.Length;

            writingArea.Lines = writingAreaCopy;

            ChangeTheBackColorOfTheMathmaticalLines(writingArea, positionOfCurrentChar);

            writingArea.SelectionLength = 0;
        }


        static internal void ChangeTheBackColorOfTheMathmaticalLines(RichTextBox writingArea, int positionOfCurrentChar)
        {
            if (mathLines == null)
                return;

            for (int i = 0; i < mathLines.Count; i++)
            {
                int currentLine = mathLines[i];
                int firstCharOfLineIndex = writingArea.GetFirstCharIndexFromLine(currentLine);
                if (currentLine > 0)
                    writingArea.Select(firstCharOfLineIndex, writingArea.Lines[currentLine].Length);

                writingArea.SelectionBackColor = Color.SkyBlue;


            }

            writingArea.SelectionStart = positionOfCurrentChar;
        }
    }
}
