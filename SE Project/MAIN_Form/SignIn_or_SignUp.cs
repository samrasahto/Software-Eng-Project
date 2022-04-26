using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAIN_Form
{
    public partial class SignIn_or_SignUp : Form
    {
        public SignIn_or_SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // if sign in button chosen
        {
            var f1 = new Sign_In_choice_form();
            this.Hide();
            f1.Show();

        }

        private void button2_Click_1(object sender, EventArgs e) // if sign up chosen
        {
            var f3 = new Customer_SignUP_form();
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }


            
}
