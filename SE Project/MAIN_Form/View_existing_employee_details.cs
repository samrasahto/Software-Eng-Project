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
    public partial class View_existing_employee_details : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True";
        public View_existing_employee_details()
        {
            InitializeComponent();
        }

        public void FillCombo()
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox1.Items.Clear();
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
                comboBox1.Items.Add(dr["City_Name".ToString()]);
            }
            con.Close();
        }

        private void viewAll()
        {
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, [Address] FROM Employee, City, Job, Departments WHERE Employee.Departments_Department_ID = Departments.Department_ID AND Job.Job_ID = Employee.Job_ID AND Employee.City_idCity = City.idCity; ", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

      
        private void View_existing_employee_details_Load(object sender, EventArgs e)
        {
            FillCombo();
            viewAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            if (textBox1.Text=="" && txtLastName.Text=="" && comboBox1.Text != "")
            {   SqlCommand cmd6 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and  City.City_Name = '" + comboBox1.Text + "'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd6);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox1.Text == "" && txtLastName.Text != "" && comboBox1.Text == "")
            {   SqlCommand cmd1 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.Last_Name like '" + txtLastName.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox1.Text != "" && txtLastName.Text == "" && comboBox1.Text == "")
            {   SqlCommand cmd2 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.First_Name like '" + textBox1.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox1.Text == "" && txtLastName.Text != "" && comboBox1.Text != "")
            {   SqlCommand cmd3 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.Last_Name like '" + txtLastName.Text + "%' and City.City_Name = '" + comboBox1.Text + "'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd3);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox1.Text != "" && txtLastName.Text != "" && comboBox1.Text == "")
            {   SqlCommand cmd4 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.First_Name like '" + textBox1.Text + "%' and Employee.Last_Name like '" + txtLastName.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd4);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox1.Text != "" && txtLastName.Text == "" && comboBox1.Text != "")
            {   SqlCommand cmd5 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.First_Name like '" + textBox1.Text + "%' and City.City_Name = '" + comboBox1.Text + "'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd5);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else
            {
                SqlCommand cmd7 = new SqlCommand("SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, Address from Employee, City, Job, Departments where Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity and Employee.First_Name like '" + textBox1.Text + "%' and Employee.Last_Name like '" + txtLastName.Text + "%' and City.City_Name = '" + comboBox1.Text + "'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd7);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            

            //retrieving data through data set
             //creating command here
            
            con.Close();
        }

 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
