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

    public partial class Admin_Dashboard : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string

        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // view employee details button clicked
        {
            var f1 = new View_existing_employee_details(); // form for adding employee
            f1.Show();
        }


        private void button5_Click(object sender, EventArgs e) // view orders button clicked
        {
            var f1 = new View_Orders(); // form for adding employee
            f1.Show();
        }

        private void button6_Click(object sender, EventArgs e) // add new product button clicked
        {
            var f1 = new Add_new_employee(); // form for adding employee
            f1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // top right clocse button clicked
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e) //  view product button clicked
        {
            var f1 = new View_Products(); // form for viewing products
            f1.Show();
        }

        private void button8_Click(object sender, EventArgs e) // add product button clicked
        {
            var f1 = new Add_Product(); // form for adding products
            f1.Show();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            // displat orders placed this month, with form load
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Orders Where DATEPART(year, GETDATE())= DATEPART(year, Order_Date) and  DATEPART(month, GETDATE())=DATEPART(month, Order_Date)", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                label7.Text = da.GetValue(0).ToString();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e) // sign out button clicked so back to start
        {
            var f1 = new SignIn_or_SignUp(); // direct to start bcz sign out
            this.Hide();
            f1.Show();
        }

   
        private void button2_Click_1(object sender, EventArgs e)
        {
            var f1 = new View_existing_employee_details(); // form for adding products
            f1.Show();
        }
    }
}
