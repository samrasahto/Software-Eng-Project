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
    public partial class Update_Customer_info : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True";
        public string idCity;
        public string userName;
        public string idLogin;
        public string idcust;
        public Update_Customer_info()
        {
            InitializeComponent();

        }
        public string passingvalue
        {
            get { return userName; }
            set { userName = value; }
        }
        public void Find_idLogin()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT idLogin FROM Login Where User_Name='" + userName + "'", con); //creating command here
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
            SqlCommand cmd = new SqlCommand("SELECT City_idCity FROM Customer Where Login_idLogin='" + idLogin + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idCity = da.GetValue(0).ToString();
            }
            con.Close();
        }
        public void FillCombo()
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

        private void Update_Customer_info_Load(object sender, EventArgs e)
        {
            FillCombo();
            Find_idLogin();
            Find_idCity();
            DisplayData();

        }

        public void DisplayData()
        {
            Find_idCity();
            Find_idLogin();

            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT City_Name FROM City Where idCity ='" + idCity + "'";
            string city = cmd1.ExecuteScalar().ToString();
            comboBox2.Text = city;
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT First_Name, Last_Name, Contact, Address, Email, City_idCity FROM Customer Where Login_idLogin='" + idLogin + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                textBox5.Text = da.GetValue(0).ToString();
                textBox1.Text = da.GetValue(1).ToString();
                textBox6.Text = da.GetValue(2).ToString();
                textBox7.Text = da.GetValue(3).ToString();
                textBox3.Text = da.GetValue(4).ToString();
            }

            con.Close();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Find_idCity();
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd0 = new SqlCommand("SELECT idCity FROM City Where City_Name='" + comboBox2.Text + "'", con); //creating command here
            SqlDataReader da = cmd0.ExecuteReader();
            while (da.Read())
            {
                idCity = da.GetValue(0).ToString();
            }
            con.Close();
            Find_idLogin();

            if (textBox5.Text != "" && textBox1.Text != "")
            {
                if (textBox6.TextLength != 11)
                {
                    MessageBox.Show("Contact Number should be 11 numbers, try again!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (!textBox3.Text.Contains("@"))
                {
                    MessageBox.Show("Incorrect email format, please try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBox2.Text == "" && textBox4.Text == "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Customer set City_idCity=@City,First_Name=@First_name,Last_Name=@Last_Name,Email=@Email,Contact=@Contact,Address=@Address where Login_idLogin=@Login_idLogin", con);
                    cmd.Parameters.AddWithValue("@Login_idLogin", idLogin);
                    cmd.Parameters.AddWithValue("@City", idCity);
                    cmd.Parameters.AddWithValue("@First_Name", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Contact", textBox6.Text);
                    cmd.Parameters.AddWithValue("@Address", textBox7.Text);
                    cmd.ExecuteNonQuery();
                    string message = "Your details have been updated!";
                    string title = "Success";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else if (textBox2.Text != "" && textBox4.Text != "" && textBox2.Text.Length <8)
                {
                    MessageBox.Show("Password length is less than 8!");
                }

                else if (textBox2.Text == textBox4.Text && textBox2.Text != "" && textBox4.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Customer set City_idCity=@City,First_Name=@First_name,Last_Name=@Last_Name,Email=@Email,Contact=@Contact,Address=@Address where Login_idLogin=@Login_idLogin", con);
                    cmd.Parameters.AddWithValue("@Login_idLogin", idLogin);
                    cmd.Parameters.AddWithValue("@City", idCity);
                    cmd.Parameters.AddWithValue("@First_Name", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Last_Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Contact", textBox6.Text);
                    cmd.Parameters.AddWithValue("@Address", textBox7.Text);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand("update Login set Password_2=@password where idLogin=@Login_idLogin", con);
                    cmd1.Parameters.AddWithValue("@Login_idLogin", idLogin);
                    cmd1.Parameters.AddWithValue("@password", textBox2.Text);
                    cmd1.ExecuteNonQuery();
                    string message = "Your details have been updated!";
                    string title = "Success";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match");
                }

                
                con.Close();
            }
            

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && ch != 8)  // in contact field user can only type numbers or use backspace key
            {
                e.Handled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
