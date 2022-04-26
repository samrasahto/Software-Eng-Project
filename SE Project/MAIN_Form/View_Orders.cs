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
    public partial class View_Orders : Form
    {
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True";
        private bool dateChanged = false; // for search button and date time picker

        public View_Orders()
        {
            InitializeComponent();
        }
        private void viewAll()
        {
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Order_ID, First_Name + ' '+ Last_Name as CustomerName,Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Customer, Orders where Customer.Customer_ID = Orders.Customer_ID; ", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void View_orders_Load(object sender, EventArgs e)
        {
            viewAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {/*
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();


            //retrieving data through data set
            SqlCommand cmd = new SqlCommand("select * from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND   Orders.Order_Date = '" + dateTimePicker1.Value + "' ", con); //creating command here
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data;
            data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            con.Close();*/
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            if (textBox2.Text == "" && textBox3.Text == "" && dateChanged == true)
            {
                SqlCommand cmd6 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND   Orders.Order_Date = '" + dateTimePicker1.Value + "' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd6);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox2.Text == "" && textBox3.Text != "" && dateChanged == false)
            {
                SqlCommand cmd1 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND Customer.First_Name like '" + textBox3.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox2.Text != "" && textBox3.Text == "" && dateChanged == false)
            {
                SqlCommand cmd2 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND orders.order_ID like '" + textBox2.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox2.Text == "" && textBox3.Text != "" && dateChanged == true)
            {
                SqlCommand cmd3 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND   Orders.Order_Date = '" + dateTimePicker1.Value + "' and Customer.First_Name like '" + textBox3.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd3);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox2.Text != "" && textBox3.Text != "" && dateChanged == false)
            {
                SqlCommand cmd4 = new SqlCommand("select * from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID and Orders.Order_ID like '" + textBox2.Text +  "%' and Customer.First_Name like '" + textBox3.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd4);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else if (textBox2.Text != "" && textBox3.Text == "" && dateChanged == true)
            {
                SqlCommand cmd5 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND   Orders.Order_Date = '" + dateTimePicker1.Value + "' and orders.order_ID like '" + textBox2.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd5);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
            else
            {
                SqlCommand cmd7 = new SqlCommand("select Order_ID, First_Name + ' ' + Last_Name as CustomerName, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total from Orders, Customer WHERE Customer.Customer_ID = Orders.Customer_ID AND   Orders.Order_Date = '" + dateTimePicker1.Value + "' and Customer.First_Name like '" + textBox3.Text + "%' and orders.order_ID like '" + textBox2.Text + "%' ", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd7);
                DataTable data;
                data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewAll();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateChanged = true;
        }
    }
}
