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
    public partial class View_Products : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True";
        public View_Products()
        {
            InitializeComponent();
        }

        private void viewAll()
        {
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Product_Name, f.Category_Name, Units_In_Stock, Discontinued, Unit_Price FROM Products p, Food_Categories f WHERE f.Category_ID = p.Food_Categories_Category_ID", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();


            //retrieving data through data set
            SqlCommand cmd = new SqlCommand("SELECT Product_Name, f.Category_Name, Units_In_Stock, Discontinued, Unit_Price from Products p, Food_Categories f WHERE f.Category_ID = p.Food_Categories_Category_ID AND Product_Name like '" + textBox1.Text + "%' ", con); //creating command here
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data;
            data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            con.Close();
        }

        private void View_Products_Load(object sender, EventArgs e)
        {
            viewAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
