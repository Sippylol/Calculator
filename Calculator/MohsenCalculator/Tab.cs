using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MohsenCalculator
{
    class Tab : Panel
    {
        internal string path = "";
        internal string fileText = "";
        internal List<int> mathLines = new List<int>(); // This will remember the lines that are mathematics
        internal Dictionary<string, string> variables = new Dictionary<string, string>();
        internal CloseBtn closeBtn;
        internal Panel container;
        internal Label fileName = new Label();
        internal RichTextBox writingArea;

        

        internal Tab(string path, string fileName, Panel container, RichTextBox writingArea)
        {

            // Add Pi to the dictionary
            variables.Add("pi", Math.PI.ToString());

            this.fileName.Text = fileName;
            this.container = container;
            this.path = path;
            this.writingArea = writingArea; // Writing area

            

            closeBtn = new CloseBtn(this); // Close button

            // Set the layout of this tab
            Width = 100;
            Height = container.Height;
            BackColor = Color.DarkGray;
            BorderStyle = BorderStyle.FixedSingle;

            // Set the elements that is part of this container
            Controls.Add(this.fileName);
            Controls[0].Left = 0;
            this.fileName.AutoSize = false;
            this.fileName.Height = Height;
            this.fileName.TextAlign = ContentAlignment.MiddleCenter;
            this.fileName.Width = 70;

            
            this.Left = container.Controls.Count * 100;

            // Change the color of the current opened tab back to its default color
            if (WritingAreaEventsMethods.currentTab != null)
                WritingAreaEventsMethods.currentTab.fileName.BackColor = Color.DarkGray;


            WritingAreaEventsMethods.mathLines = mathLines; // Set the mathlines of this file as the main mathlines
            WritingAreaEventsMethods.path = path; // Set the path of this area the be the main path so when the user change it's contain the changes will be apllied to this file.
            WritingAreaEventsMethods.currentTab = this; // Will be used to change the color of the tab
            this.fileName.BackColor = Color.Gray; // Show that this is the opened tab

            
            

            this.fileName.MouseClick += OpenTab;


            // Read and display the text information if it was opened
            if (path != "")
                using (StreamReader reader = new StreamReader(path))
                {
                    fileText = reader.ReadToEnd();
                    string[] fileInfo = fileText.Split('\n');

                    if (fileInfo.Length > 1)
                    {

                        string[] fileMathLines = fileInfo[0].Split(',');

                        // Get the mathmatical lines in this file, if they exist
                        for (int i = 0; i < fileMathLines.Length; i++)
                        {
                            int currentLine;
                            if (int.TryParse(fileMathLines[i], out currentLine))
                            {
                                mathLines.Add(currentLine);
                            }
                        }

                        // Display the file info in the writing area --> Main class/form
                        string fileInfoAfterExtractingMathLines = "";
                        for (int i = 1; i < fileInfo.Length; i++)
                        {
                            fileInfoAfterExtractingMathLines += fileInfo[i] + "\n";
                        }

                        writingArea.Text = fileInfoAfterExtractingMathLines;

                        // Change the background color of the mathematical lines
                        WritingAreaEventsMethods.ChangeTheBackColorOfTheMathmaticalLines(writingArea, 0);
                        // Display the info and mark the mathematical lines
                        //WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea, fileInfoAfterExtractingMathLines);

                    }
                    else
                        writingArea.Text = fileText;
                }


            container.Controls.Add(this); // Add this tab to the tabs
        }


        internal void OpenTab(object sender, MouseEventArgs e)
        {
            if (WritingAreaEventsMethods.path == path && WritingAreaEventsMethods.path != "")
                return;

            if(WritingAreaEventsMethods.currentTab != null)
                WritingAreaEventsMethods.currentTab.fileName.BackColor = Color.DarkGray; // Change the last opened tab back to its original color

            WritingAreaEventsMethods.mathLines = mathLines; // Set the mathlines of this file as the main mathlines
            WritingAreaEventsMethods.path = path; // Set the path of this area the be the main path so when the user change it's contain the changes will be apllied to this file.
            WritingAreaEventsMethods.currentTab = this;
            fileName.BackColor = Color.Gray;
            writingArea.Text = fileText;

            WritingAreaEventsMethods.AddCharToMathlineAtCharIndex(writingArea, "");
        }
    }

    // The close button of the tab
    class CloseBtn : Button
    {
        readonly Tab parentTab;

        internal CloseBtn(Tab parent)
        {
            parentTab = parent;
            Width = 30;
            Height = 30;
            Text = "X";
            BackColor = Color.Red;

            Click += CloseThisTab;

            parentTab.Controls.Add(this);
            Left = 70;
            Top = (int)((parentTab.Height / 2f) - Height);
            Dock = DockStyle.Right;
        }

        internal void CloseThisTab(Object sender, EventArgs e)
        {

            if (Text == "X")
            {
                CloseTab();
            }
            else
            {
                // Make sure that the user know that he didn't saved the changes
                Confirm confirmCloseTab = new Confirm($"You are trying to close the file {parentTab.fileName}\nwithout saving the last changes\nAre you sure you want to close this file?");

                confirmCloseTab.ShowDialog();

                if (confirmCloseTab.DialogResult == DialogResult.OK)
                {
                    CloseTab();
                }
            }
        }


        private void CloseTab()
        {
            parentTab.container.Controls.Remove(parentTab);

            Main.tabs.Remove(parentTab);

            WritingAreaEventsMethods.currentTab = null;
            WritingAreaEventsMethods.mathLines = new List<int>();
            WritingAreaEventsMethods.path = "";

            if (parentTab.container.Controls.Count < 1)
            {
                parentTab.writingArea.Clear();
                return;
            }

            // Relocate the tabs
            for (int i = 0; i < parentTab.container.Controls.Count; i++)
            {
                parentTab.container.Controls[i].Left = i * parentTab.Width;
            }

            Tab lastTab = (Tab)parentTab.container.Controls[parentTab.container.Controls.Count - 1];
            lastTab.OpenTab(null, null);
        }
    }
}
