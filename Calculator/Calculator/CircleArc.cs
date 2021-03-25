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
    public partial class CircleArc : Form
    {
        public CircleArc()
        {
            InitializeComponent();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.GetHashCode() == 524296 
                || e.KeyChar.GetHashCode() == Keys.Delete.GetHashCode())
                return;
            string mathInfoToAdd = e.KeyChar.ToString();
            e.Handled = true;
            if (Operator.CheckNumber(Char.Parse(mathInfoToAdd)))
            {
                if(((TextBox)sender).Name == "txtV")
                {
                    if(double.Parse(txtV.Text + mathInfoToAdd) > 360)
                    {
                        txtV.Text = "360";
                        return;
                    }
                }
                if (((TextBox)sender).Text == "0")
                    ((TextBox)sender).Text = "";

                ((TextBox)sender).AppendText(mathInfoToAdd);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double R = 0;
            double A = 0;
            double V = 0;
            int counter = 0;
            
            txtA.Text = txtA.Text == "0" ? "" : txtA.Text;
            txtR.Text = txtR.Text == "0" ? "" : txtR.Text;
            txtV.Text = txtV.Text == "0" ? "" : txtV.Text;

            if (txtR.Text != "")
            {
                R = double.Parse(txtR.Text); // Radius
                counter++;
            }
            if (txtA.Text != "")
            {
                A = double.Parse(txtA.Text); // Arc length
                counter++;
            }
            if (txtV.Text != "")
            {
                V = double.Parse(txtV.Text); // Angle
                counter++;
            }

            if(counter < 2)
            {
                MessageBox.Show("Error #3: \nTwo or more fields MUST be filled!");
                return;
            }
            else if(counter >= 3)
            {
                MessageBox.Show("Error #4: \nOnly two fields must be filled!");
                return;
            }

            if((R == 0 && A == 0) || (A == 0 && V == 0) || (R == 0 && V == 0))
            {
                MessageBox.Show("Error #5: \nOne or more input are not vaild!");
                return;
            }

            if(A == 0)
            {
                A = Math.PI * R / 180 * V; // Arc formula
            }
            else if(R == 0)
            {
                R = A * 180 / (V * Math.PI); // Radius formula
            }
            else if(V == 0)
            {
                V = (A * 180) / (Math.PI * R); // Angle formula
            }

            if(V > 360)
            {
                MessageBox.Show(V.ToString());
                MessageBox.Show("Error #6: \nOne or more input are not vaild!");
                return;
            }

            if(A > (2 * Math.PI * R))
            {
                MessageBox.Show("Error #7: \nOne or more input are not vaild!");
                return;
            }


            lblAResult.Text = lblAResult.Text.Substring(0, 2) + " " 
                + Math.Round(A, 2);

            lblRResult.Text = lblRResult.Text.Substring(0, 2) + " " 
                + Math.Round(R, 2);

            lblVResult.Text = lblVResult.Text.Substring(0, 3) + " " 
                + Math.Round(V, 2);
        }
    }
}
