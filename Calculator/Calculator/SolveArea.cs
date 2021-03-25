using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    class SolveArea : TextBox
    {
        internal SolveArea(int y)
        {
            Location = new Point(0, y);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(130, 223, 238);
            //BackColor = Color.Transparent;

            this.KeyDown += EnterKeyDown;
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.Text = "Ready";
            }
        }
    }
}
