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
    public partial class Chord : Form
    {
        public Chord()
        {
            InitializeComponent();
        }


        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.GetHashCode() == 524296
                || e.KeyChar.GetHashCode() == Keys.Delete.GetHashCode())
                return;
            string mathInfoToAdd = e.KeyChar.ToString();
            e.Handled = true;
            if (Operator.CheckNumber(e.KeyChar))
            {
                if (((TextBox)sender).Text == "0")
                    ((TextBox)sender).Text = "";

                ((TextBox)sender).AppendText(mathInfoToAdd);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double A = 0, R = 0, V = 0, H = 0, C = 0;
            if(txtR.Text != "" && txtA.Text != "" 
                && txtR.Text != "0" && txtA.Text != "0") // AR
            {
                A = double.Parse(txtA.Text);
                R = double.Parse(txtR.Text);
                V = (A * 180) / (Math.PI * R); // Angle formula
                C = Math.Sin(V / 2) * R * 2;
                H = R - Math.Cos(V / 2) * R;

                    
            }
            else if(txtA.Text != "" && txtV.Text != ""
                && txtA.Text != "0" && txtV.Text != "0") // AV
            {
                A = double.Parse(txtA.Text);
                V = double.Parse(txtV.Text);
                R = A * 180 / (V * Math.PI); // Radius formula
                C = Math.Sin(V / 2) * R * 2;
                H = R - Math.Cos(V / 2) * R;
            }
            else if (txtA.Text != "" && txtH.Text != ""
                && txtA.Text != "0" && txtH.Text != "0") // AH
            {
                A = double.Parse(txtA.Text);
                H = double.Parse(txtH.Text);
                V = 1;
                for(float vinkel = 0.1f; ; vinkel += 0.1f)
                {
                    double h = A * 180 / Math.PI * vinkel * (1 - Math.Cos(vinkel));

                    if(h > H - 1 && h < H + 1)
                    {
                        V = vinkel;
                        break;
                    }
                }
                R = A * 180 / (V * Math.PI); // Radius formula
                C = Math.Sin(V / 2) * R * 2;
                V = R - Math.Cos(V / 2) * R;
            }
            else if (txtA.Text != "" && txtH.Text != ""
                && txtA.Text != "0" && txtH.Text != "0") // AC
            {
               /* A = double.Parse(txtA.Text);
                C = double.Parse(txtV.Text);
                V = Math.Sin(V / 2) * R * 2;
                R = A * 180 / (V * Math.PI); // Radius formula
                H = R - Math.Cos(V / 2) * R;*/
            }


            // Show the result
            lblRResult.Text = "R: " + R;
            lblAResult.Text = "A: " + A;
            lblVResult.Text = "V: " + V;
            lblCResult.Text = "C: " + C;
            lblHResult.Text = "H: " + H;
        }
    }
}
