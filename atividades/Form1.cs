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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void questão1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hoje é quarta - feira!");
        }

        private void questão2(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog(); 
        }

        private void questão3(object sender, EventArgs e)
        {

        }

        private void questão4(object sender, EventArgs e)
        {

        }

        private void questão5(object sender, EventArgs e)
        {

        }

        private void questão6(object sender, EventArgs e)
        {

        }

        private void questão7(object sender, EventArgs e)
        {

        }

        private void questão8(object sender, EventArgs e)
        {

        }
    }
}
