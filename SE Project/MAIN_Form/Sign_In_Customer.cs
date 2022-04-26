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
    public partial class Sign_In_Customer : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True";
        public Sign_In_Customer()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // if checkbox ticked, display password 
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

        private void button1_Click(object sender, EventArgs e) // done button clicked
        {
            // using stored procedures
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select USER_NAME, Password_2 FROM [Login] Where User_Name ='" + textBox1.Text.Trim() + "' AND Password_2 ='" + textBox2.Text.Trim() + "'", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count == 1) // if the login  details have a match in database
            {
                var f1 = new Customer_Dashboard();
                f1.passingvalue = textBox1.Text;

                this.Hide();
                f1.Show();
            }
            else 
            {
                MessageBox.Show("Please enter correct username and password!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Sign_In_Customer_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
