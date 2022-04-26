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
    public partial class Customer_SignUP_form : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string

        string idLogin;
        string idCity;
        bool is_match; 
        public Customer_SignUP_form()
        {
            InitializeComponent();
        }

        private bool check_username() // check for repition of username since it cannot be repeated
        {
            SqlConnection con = new SqlConnection(sqlcon);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Login WHERE User_Name='" + textBox8.Text.Trim() + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1) // atleast one matching username found
            { return true; }
            else 
            { return false; }
        }

        public void Find_idLogin()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT idLogin FROM Login Where User_Name='" + textBox8.Text + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idLogin = da.GetValue(0).ToString();
            }
        }

        public void Find_idCity()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT idCity FROM City Where City_Name='" + comboBox2.Text + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idCity = da.GetValue(0).ToString();
            }
        }

        public void FillCombo() // fill city dropdown 
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox2.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT City_Name FROM City";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["City_Name".ToString()]);
            }
            con.Close();
        }
        private void Customer_SignUP_form_Load(object sender, EventArgs e)
        {
            FillCombo(); // fill city drop down upon loading
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // done button clicked
        {
            // finding login and city id when done clicked
            Find_idCity(); 
            Find_idLogin();
            is_match = check_username();
            // if even one field unfilled
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please enter missing information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if passwords are not matching
            else if (textBox6.Text != textBox7.Text)
            {
                MessageBox.Show("The passwords are not matching!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;
            }
            // if password length less than 8
            else if (textBox6.TextLength < 8)
            {
                MessageBox.Show("Password should be of 8 or more characters!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if the username entered already exists
            else if (is_match==true)
            {
                MessageBox.Show("This username is already taken! Try another one.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox4.TextLength != 11)
            {
                MessageBox.Show("Contact Number should be 11 numbers, try again!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (!textBox3.Text.Contains("@"))
            {
                MessageBox.Show("Incorrect email format, please try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if no issues
            else
            {
                //Opening a connection
                SqlConnection con = new SqlConnection(sqlcon);
                con.Open();

                //commands are being created
                SqlCommand cmdNonQuery = new SqlCommand("INSERT INTO Login (User_Name, Password_2) VALUES (@User_Name,@Password_2)", con);
                // adding parameters
                cmdNonQuery.Parameters.Add(new SqlParameter("User_Name", textBox8.Text));
                cmdNonQuery.Parameters.Add(new SqlParameter("Password_2", textBox7.Text));
                cmdNonQuery.ExecuteNonQuery();

                con.Close();
                Find_idCity();
                Find_idLogin();

                //Opening a connection
                con.Open();

                SqlCommand cmdNonQuery01 = new SqlCommand("INSERT INTO Customer (Login_idLogin, City_idCity, First_Name, Last_Name, Contact, Address, Email, Gender) VALUES  (@Login_idLogin, @City_idCity, @First_Name, @Last_Name, @Contact, @Address, @Email, @Gender); Select SCOPE_IDENTITY();", con);

                // adding parameters
                cmdNonQuery01.Parameters.Add(new SqlParameter("Login_idLogin", idLogin));
                cmdNonQuery01.Parameters.Add(new SqlParameter("City_idCity", idCity));
                cmdNonQuery01.Parameters.Add(new SqlParameter("First_Name", textBox1.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Last_Name", textBox2.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Contact", textBox4.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Address", textBox5.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Email", textBox3.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Gender", comboBox1.SelectedItem));
                cmdNonQuery01.ExecuteNonQuery();

                textBox8.Clear();
                textBox7.Clear();

                con.Close();

                // success msg
                string message = "Your details have been added. Try signing into your account!";
                string title = "Success";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && ch != 8)  // in contact field user can only type numbers or use backspace key
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // if checkbox ticked display the password
        {
            if (checkBox1.Checked == true)
            {
                textBox6.UseSystemPasswordChar = false;
                textBox7.UseSystemPasswordChar = false;
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
                textBox7.UseSystemPasswordChar = true;
            }
        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
