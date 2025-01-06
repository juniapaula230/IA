using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atividdes
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void num1(object sender, EventArgs e)
        {

        }

        private void num2(object sender, EventArgs e)
        {

        }

        private void escrevenum1(object sender, EventArgs e)
        {

        }

        private void escrevenum2(object sender, EventArgs e)
        {

        }

        private void escrevenum3(object sender, EventArgs e)
        {

        }

        private void calcular(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(txtNum1.Text);
            double b = Convert.ToDouble(txtNum2.Text);
            double c = Convert.ToDouble(txtNum3.Text);

            double r = (a + b) * c;
            MessageBox.Show("O resultado de (" + a+ "+" + b + ") "+" *" + c + " é: " + r.ToString());
        }
    }
}
