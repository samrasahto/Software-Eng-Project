using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace MAIN_Form
{
    public partial class Customer_Dashboard : Form
    {
        public string userName;
   
        public string passingvalue // get username from previous sign in form
        {

            get { return userName; }
            set { userName = value; }
        }

        public Customer_Dashboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e) // update info button clicked
        {
            var f1 = new Update_Customer_info(); // form for adding employee
            f1.passingvalue = label5.Text;
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e) // view previous orders button clicked
        {
            var f1 = new View_Order_history_customer();
            f1.passingvalue = label5.Text;
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e) // view current orders button clicked
        {
            var f1 = new View_Order_Current_Customer();
            f1.passingvalue = label5.Text;
            f1.Show();

        }

        private void button1_Click(object sender, EventArgs e) // top right close button clicked
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Customer_Dashboard_Load(object sender, EventArgs e) // adding username to label
        {
            label5.Text = "" + userName;

        }

        private void button2_Click(object sender, EventArgs e) // place order button clicked
        {
            var f1 = new Place_Order_form();
            f1.passingvalue = label5.Text;
            f1.Show();
        }

        private void button6_Click(object sender, EventArgs e) // sign out button clicked so back to start
        {
            var f1 = new SignIn_or_SignUp(); // direct to start bcz sign out
            this.Hide();
            f1.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
