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
    public partial class Circumference : Form
    {
        public Circumference()
        {
            InitializeComponent();
        }

        private void Circumference_Load(object sender, EventArgs e)
        {

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
            double R = 0;
            double A = 0;

            txtA.Text = txtA.Text == "0"? "" : txtA.Text;
            txtR.Text = txtR.Text == "0"? "" : txtR.Text;

            if(txtA.Text == "" && txtR.Text == "")
            {
                MessageBox.Show("Error #8: \nOne of those feilds must be filled!");
                return;
            }
            else if(txtA.Text != "" && txtR.Text != "")
            {
                MessageBox.Show("Error #9: \nOnly one of those feilds must be filled!");
                return;
            }

            if (txtR.Text != "")
            {
                R = double.Parse(txtR.Text); // Radius
            }
            else
            {
                R = double.Parse(txtA.Text) / (2 * Math.PI);
            }
            if (txtA.Text != "")
            {
                A = double.Parse(txtA.Text); // Arc length
            }
            else
            {
                A = 2 * Math.PI * R;
            }

            lblAResult.Text = lblAResult.Text.Substring(0, 2)
                + Math.Round(A, 2).ToString();

            lblRResult.Text = lblRResult.Text.Substring(0, 2)
                + Math.Round(R, 2).ToString();

        }

    }
}
