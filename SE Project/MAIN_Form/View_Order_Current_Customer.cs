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
    public partial class View_Order_Current_Customer : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string

        public string userName;
        public string idLogin;
        public string idcust;

        public string passingvalue // to get value from previous form
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

        public void Find_Cust_Name()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT First_Name + ' ' + Last_Name, Customer_ID as CustomerName FROM Customer Where Login_idLogin='" + idLogin + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                label1.Text = da.GetValue(0).ToString();
                idcust = da.GetValue(1).ToString();
            }
        }

        private void Load_curr()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();

            //retrieving data through data set
            SqlCommand cmd = new SqlCommand("select * from Orders where Order_Status = 'Not Complete' and Customer_ID='" + idcust + "'", con); //creating command here
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data;
            data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            con.Close();
        }

        public View_Order_Current_Customer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void View_Order_Current_Customer_Load(object sender, EventArgs e)
        {
            Find_idLogin();
            Find_Cust_Name();
            Load_curr();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
