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
    public partial class Add_new_employee : Form
    {
        string idCity; // after finding city id from city name, id is stored in this variable
        string idManager; // after finding manager id from manager name, id is stored in this variable
        string idJob; // after finding job id from job name, id is stored in this variable
        string idDepartment; // after finding department id from department name, id is stored in this variable
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string
        public Add_new_employee()
        {
            InitializeComponent();
        }
        public void Find_idCity() // finds city id from name and saves in idCity
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT idCity FROM City WHERE City_Name='" + comboBox2.Text + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idCity = da.GetValue(0).ToString();
            }
        }
        public void Find_idJob()  // finds job id from name and saves in idJob
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Job_Id FROM Job WHERE Job_Name='" + comboBox3.Text + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idJob = da.GetValue(0).ToString();
            }
        }

        public void Find_idManager() // finds manager id from name and saves in idManager
        {
            string[] name = comboBox4.Text.Split(' ');            
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Employee_ID FROM Employee Where First_Name='" + name[0] + "' AND Last_Name= '" + name[1] + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idManager = da.GetValue(0).ToString();
            }
        }

        public void Find_idDepartment() // finds department id from name and saves in idDepartment
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Department_ID FROM Departments WHERE Department_Name='"+  comboBox5.Text.Trim() +"'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idDepartment = da.GetValue(0).ToString();
            }
        }

        public void FillCombo() // Fills city drop down
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
        public void FillCombo2() // fills job drop down
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox3.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Job_Name FROM Job";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["Job_Name".ToString()]);
            }
            con.Close();
        }

        public void FillCombo3() // fill manager name drop down
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox4.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT First_Name+' '+Last_Name AS EmployeeName FROM Employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["EmployeeName".ToString()]);
            }
            con.Close();
        }

        public void FillCombo4() // fill department name
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox5.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Department_Name FROM Departments";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox5.Items.Add(dr["Department_Name".ToString()]);
            }
            con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Add_new_employee_Load(object sender, EventArgs e)
        {
            // filling city, job, manager name, department name drop downs
            FillCombo();
            FillCombo2();
            FillCombo3();
            FillCombo4();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // error msg if any field is left
            if (textBox1.Text == "" || comboBox3.Text == "" || textBox3.Text == ""  || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please enter missing information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox6.TextLength != 11)
            {
                MessageBox.Show("Contact Number should be 11 numbers, try again!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (!textBox3.Text.Contains("@"))
            {
                MessageBox.Show("Incorrect email format, please try again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if all fields filled
            else
            {
                Find_idCity();
                Find_idDepartment();
                Find_idJob();
                Find_idManager();

                SqlConnection con = new SqlConnection(sqlcon);
                //Opening a connection
                con.Open();

                //commands are being created
                SqlCommand cmdNonQuery01 = new SqlCommand("INSERT INTO Employee (Job_Id, City_idCity, Manager_ID, Departments_Department_ID, First_Name, Last_Name, Contact, Gender, Address, Email) VALUES (@Job_Id, @City_idCity, @Manager_ID, @Departments_Department_ID, @First_Name, @Last_Name, @Contact, @Gender, @Address, @Email)", con);

                // adding parameters
                cmdNonQuery01.Parameters.Add(new SqlParameter("Job_Id", idJob));
                cmdNonQuery01.Parameters.Add(new SqlParameter("City_idCity", idCity));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Manager_ID", idManager));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Departments_Department_ID", idDepartment));
                cmdNonQuery01.Parameters.Add(new SqlParameter("First_Name", textBox5.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Last_Name", textBox1.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Contact", textBox6.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Address", textBox7.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Email", textBox3.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Gender", comboBox1.Text));
                cmdNonQuery01.ExecuteNonQuery();

                con.Close();

                // success message
                string message = "New employees details have been added!";
                string title = "Success";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) // when the job is changed from the drop down change salary accordingly 
        {
            
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Salary_Per_Hour FROM Job WHERE Job_Name='" + comboBox3.Text.Trim() + "'", con); //creating command here

            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                label5.Text = da.GetValue(0).ToString();
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e) // restiction contact field to numbers
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && ch != 8)  // in contact field user can only type numbers or use backspace key
            {
                e.Handled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
