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
    public partial class Add_Product : Form
    {
        int value = 0; // for entering dicontinued value
        string idFoodCategory; // food cat id is stored here once found from cat name
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string
        public Add_Product()
        {
            InitializeComponent();
        }
        private void Find_idFoodCategory() // finds id from name and saves to idFoodCategory
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();

            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Category_ID FROM Food_Categories Where Category_Name='" + comboBox1.Text + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idFoodCategory = da.GetValue(0).ToString();
            }
        }
        private void FillCombo() // fill category drop down
        {
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox1.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Category_Name FROM Food_Categories";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Category_Name".ToString()]);
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // add button clicked
        {
            // if even one field is left blank
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please enter missing information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Find_idFoodCategory();
                SqlConnection con = new SqlConnection(sqlcon);
                //Opening a connection
                con.Open();

                //commands are being created
                SqlCommand cmdNonQuery01 = new SqlCommand("INSERT INTO Products (Food_Categories_Category_ID, Product_Name, Units_In_Stock, Discontinued, Unit_Price) VALUES (@Food_Categories_Category_ID, @Product_Name, @Units_In_Stock, @Discontinued, @Unit_Price) ", con);

                // adding parameters
                cmdNonQuery01.Parameters.Add(new SqlParameter("Food_Categories_Category_ID", idFoodCategory));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Product_Name", textBox1.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Units_In_Stock", textBox3.Text));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Discontinued", value)); // intially each product being added will logically not be discontinued
                cmdNonQuery01.Parameters.Add(new SqlParameter("Unit_Price", textBox4.Text));
                cmdNonQuery01.ExecuteNonQuery();

                string message = "New product details have been added!";
                string title = "Success";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }

                con.Close();
            }
        }

        private void Add_Product_Load(object sender, EventArgs e) // fill category drop down on form load
        {
            FillCombo();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
