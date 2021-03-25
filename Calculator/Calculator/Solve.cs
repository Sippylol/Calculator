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
    public partial class Solve : Form
    {
        int nextY = 27;
        int distanceToNextY = 26;
        private List<SolveArea> solveAreas = new List<SolveArea>();
        public Solve()
        {
            InitializeComponent();
        }

        private void Solve_DoubleClick(object sender, EventArgs e)
        { 
            SolveArea sa = new SolveArea(nextY);
            solveAreas.Add(sa);
            Controls.Add(sa);
            nextY += distanceToNextY;
        }
    }
}
