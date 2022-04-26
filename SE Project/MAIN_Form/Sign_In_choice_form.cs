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
    public partial class Sign_In_choice_form : Form
    {
        public Sign_In_choice_form()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) // sign in as admin buton clicked
        {
            var f3 = new Sign_In_Admin();
            this.Hide();
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e) // sign in as customer button clicked
        {
            var f4 = new Sign_In_Customer();
            this.Hide();
            f4.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
        private void Sign_In_choice_form_Load(object sender, EventArgs e)
        {

        }
    }
}
