using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Input;

namespace MohsenCalculator
{
    public partial class Main : Form
    {
        readonly string initialDirectory = "Desktop";
        internal static int unsavedTabsCount = 0;
        internal static List<Tab> tabs = new List<Tab>();


        public Main()
        {
            InitializeComponent();

            // Add the controling functions to the writing area
            richTextBoxWriteArea.KeyDown += WritingAreaEventsMethods.RichTextBoxWriteArea_KeyDown;
            richTextBoxWriteArea.KeyPress += WritingAreaEventsMethods.RichTextBoxWriteArea_KeyPress;

            

            // Show the shortcuts

        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Change the size of the writing area
            ChangeWritingAreaSize();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            // Change the size of the writing area
            ChangeWritingAreaSize();
        }



        private void ChangeWritingAreaSize()
        {
            richTextBoxWriteArea.Width = Width;
            richTextBoxWriteArea.Height = Height - richTextBoxWriteArea.Location.Y;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WritingAreaEventsMethods.path = string.Empty;
            richTextBoxWriteArea.Clear();
            tabs.Add(new Tab(WritingAreaEventsMethods.path, "new tab (" + unsavedTabsCount++ + ")", panelNav, richTextBoxWriteArea));
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.RestoreDirectory = true;

                // Import the information if the user chose a file
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the chosen file
                    WritingAreaEventsMethods.path = openFileDialog.FileName;
                    bool fileIsOpen = tabs.Any(tab => tab.path == WritingAreaEventsMethods.path); // Check if there is any tab that has the path of the file that are going to be opened
                    if(!fileIsOpen)
                        tabs.Add(new Tab(WritingAreaEventsMethods.path, openFileDialog.SafeFileName, panelNav, richTextBoxWriteArea));
                }

            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the user has chose a path to save the file in
            if(WritingAreaEventsMethods.path == "")
            {
                SaveAsToolStripMenuItem_Click(sender, e);
                return;
            }
            SaveFileWithMathematicalInfo(WritingAreaEventsMethods.path);
            /*using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(richTextBoxWriteArea.Text);
                writer.Close();
                WritingAreaEventsMethods.currentTab.closeBtn.Text = "X";
            }*/
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.Title = "Save File";
                saveFileDialog.ShowDialog();

                // Check whether the selected path is vaild or not
                if(saveFileDialog.FileName != "")
                {
                    // Set the path to be the chose path
                    WritingAreaEventsMethods.path = saveFileDialog.FileName;

                    SaveFileWithMathematicalInfo(WritingAreaEventsMethods.path);

                }
            }
                
        }

        private void SaveFileWithMathematicalInfo(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                string mathLines = "";

                int[] mathLinesArray = new int[WritingAreaEventsMethods.mathLines.Count];

                WritingAreaEventsMethods.mathLines.CopyTo(mathLinesArray);

                // Add the mathlines to the beginning of the file
                for (int i = 0; i < mathLinesArray.Length; i++)
                {
                    if (i == 0)
                        mathLines += mathLinesArray[i];
                    else
                        mathLines += "," + mathLinesArray[i];
                }
                // Add new line
                mathLines += "\n";

                // Rewrite or create and write the file
                writer.Write(mathLines + richTextBoxWriteArea.Text);
                writer.Close();

                // Set the new information to this tab
                WritingAreaEventsMethods.currentTab.path = path;
                WritingAreaEventsMethods.currentTab.fileName.Text = Path.GetFileName(path);
                WritingAreaEventsMethods.currentTab.closeBtn.Text = "X";

                // Reopen the curren tab to reset the tab information
                WritingAreaEventsMethods.currentTab.OpenTab(null, null);

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) // Close the application
        {
            if (Application.MessageLoop)
            {
                Application.Exit();
            }
        }

        private void TxtboxWriteArea_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                RichTextBox writingArea = (RichTextBox)sender;

                if (writingArea.Lines.Length != 0) // Get the current line the user is on
                { }
                    //MessageBox.Show(writingArea.Lines[writingArea.GetLineFromCharIndex(writingArea.SelectionStart)]);
            }
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (WritingAreaEventsMethods.currentTab == null)
            {
                MessageBox.Show("You are not able to calculate without having an open tab");
                return;
            }

            string result = "";

            int currentLineIndex = richTextBoxWriteArea.GetLineFromCharIndex(richTextBoxWriteArea.SelectionStart); // The index of the current selected line by the user in the writing area

            // Check if the current line is mathematical line and check if there is any lines in the writing area
            if (richTextBoxWriteArea.Lines.Length == 0 || !WritingAreaEventsMethods.mathLines.Contains(currentLineIndex) || richTextBoxWriteArea.Lines[currentLineIndex] == "")
                return;

            string equation = richTextBoxWriteArea.Lines[currentLineIndex]; // Extract the equation to calculate

            // Check if this line is already calculated
            if (equation.Contains<char>('='))
                return;

            // Exchange the variables in this mathematical line with their value
            equation = WritingAreaEventsMethods.ReplaceVarKeyWithItsValue(equation);

            try
            {

                if (equation.Contains<char>('('))
                    result = Operator.CheckBrackets(equation);
                else if (equation.Contains<char>('*')
                    || equation.Contains<char>('/')
                    || equation.Contains<char>('%'))
                    result = Operator.CheckMDM(equation);
                else if (equation.Contains<char>('-')
                    || equation.Contains<char>('+'))
                    result = Operator.CheckPM(equation);

                WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(richTextBoxWriteArea, " = " + result);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //MessageBox.Show(result);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Before closing the prgoram, check if there is any unsaved change
            if(tabs.Any(tab => tab.closeBtn.Text == "O"))
            {
                Confirm confrimClosing = new Confirm("Your are about to close the program without saving all the changes you made\nAre you sure you want to close the program anyway?");

                confrimClosing.ShowDialog(); // Show the confirm closing form to the user

                if (confrimClosing.DialogResult == DialogResult.Cancel)
                    e.Cancel = true; // If the user don't want to to close the program, stop the closing 
            }
            

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WritingAreaEventsMethods.currentTab != null)
                WritingAreaEventsMethods.currentTab.closeBtn.CloseThisTab(null, null);
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WritingAreaEventsMethods.currentTab == null)
            {
                MessageBox.Show("You are not able to calculate without having an open tab");
                return;
            }

            int currentLineIndex = richTextBoxWriteArea.GetLineFromCharIndex(richTextBoxWriteArea.SelectionStart); // The index of the current selected line by the user in the writing area

            // Check if the current line is mathematical line and check if there is any lines in the writing area
            if (richTextBoxWriteArea.Lines.Length == 0 || !WritingAreaEventsMethods.mathLines.Contains(currentLineIndex) || richTextBoxWriteArea.Lines[currentLineIndex] == "")
                return;

            string equation = richTextBoxWriteArea.Lines[currentLineIndex]; // Extract the equation to calculate

            if (!equation.Contains<char>('='))
                return;

            // Exchange the variables in this mathematical line with their value
            equation = WritingAreaEventsMethods.ReplaceVarKeyWithItsValue(equation);

            string[] equationParts = equation.Contains<char>('+') || equation.Contains<char>('-') ? Operator.OneUnknownVarSolve(equation) : Operator.OneUnknownVarSolveGD(equation);

            equationParts[0] = equationParts[0].Replace("-+", "-").Replace("+-", "-");

            try
            {

                if (equationParts[0].Contains<char>('('))
                    equationParts[0] = Operator.CheckBrackets(equationParts[0]);
                else if (equationParts[0].Contains<char>('*')
                    || equationParts[0].Contains<char>('/')
                    || equationParts[0].Contains<char>('%'))
                    equationParts[0] = Operator.CheckMDM(equationParts[0]);
                else if (equationParts[0].Contains<char>('-')
                    || equationParts[0].Contains<char>('+'))
                    equationParts[0] = Operator.CheckPM(equationParts[0]);

                if (equationParts[1] == "")
                    return;

                WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(richTextBoxWriteArea, " : " + equationParts[1] + " = " + equationParts[0]);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void mathenaticalLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WritingAreaEventsMethods.ConvertToNormalOrMathLine(richTextBoxWriteArea);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }
    }
}
