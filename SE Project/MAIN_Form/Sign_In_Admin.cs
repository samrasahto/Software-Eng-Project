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
    public partial class Sign_In_Admin : Form
    {
        public Sign_In_Admin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // if done button clicked
        {
            // some fields are empty
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                string message = "Username or Password is missing!";
                string title = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {

                }
                textBox1.Focus();
                return;
            }
            // hardcode id password
            if (textBox1.Text != "admin" || textBox2.Text != "admin")
            {
                MessageBox.Show("Please enter correct username and password!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }
            else // successful log in
            {
                var f1 = new Admin_Dashboard(); // form for adding employee
                this.Hide();
                f1.Show();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // if checkbox ticked display the password
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Sign_In_Admin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
