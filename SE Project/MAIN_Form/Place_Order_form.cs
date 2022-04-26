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
    public partial class Place_Order_form : Form
    {   
        string sqlcon = @"Data Source=DESKTOP-U5GCMQE;Initial Catalog=SE Project;Integrated Security=True"; // connection string
        int total = 0; // keeps track of total cost of shopping cart
        string cost; // to save cost of the selected product, aiding in calculation for total
        string idProduct;
        string prodName;
        string quantity; // aiding in calculation for total
        string idLogin;
        string idcust;
        string todayDate; // saves todays date
        string shipDate; // saves expected shipping date
        string expDate; // saves expected date of arrival
        string stockUnit; // to check if product is available 

        public string userName;
        public string passingvalue // brings customer username from previous form
        {
            get { return userName; }
            set { userName = value; }
        }

        public Place_Order_form()
        {
            InitializeComponent();
        }

        private void Find_units_instock(string productName) // stores units in stock of the selected product in stockUnit 
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Units_In_Stock FROM Products WHERE Product_Name='" + productName + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                stockUnit = da.GetValue(0).ToString();
            }
        }

        public void Find_idLogin()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT idLogin FROM Login WHERE User_Name='" + userName + "'", con); //creating command here
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
            SqlCommand cmd = new SqlCommand("SELECT First_Name + ' ' + Last_Name, Customer_ID AS CustomerName FROM Customer Where Login_idLogin='" + idLogin + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                label9.Text = da.GetValue(0).ToString();
                idcust = da.GetValue(1).ToString();
            }
        }

        public void Find_idProduct()
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Product_ID FROM Products WHERE Product_Name='" + prodName.Trim() + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                idProduct = da.GetValue(0).ToString();
            }
        }

        public void Find_date() // getting todys date. cast to avoid time
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT CAST( GETDATE() AS Date )", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                todayDate = da.GetValue(0).ToString();
            }
        }

        public void Find_shipdate() // adding 10 days to todays date for exp shipping date
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT DATEADD(dd, 10, GETDATE())", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                shipDate = da.GetValue(0).ToString();
            }
        }

        public void Find_expdate() // adding 20 days to todays date for exp arrival date
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT DATEADD(dd, 20, GETDATE())", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                expDate = da.GetValue(0).ToString();
            }
        }

        private void ProductPrice(string prodName) // Finding unit price of product name i. passed as a parameter
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            con.Open();
            //retrieving data
            SqlCommand cmd = new SqlCommand("SELECT Unit_Price FROM Products WHERE Product_Name='" + prodName + "'", con); //creating command here
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                cost = da.GetValue(0).ToString();
            }
        }

        private void FillCombo() // fill product drop down
        {
            //Opening a connection
            SqlConnection con = new SqlConnection(sqlcon);
            comboBox2.Items.Clear();
            con.Open();
            //retrieving data
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Product_Name FROM products WHERE Food_Categories_Category_ID=(SELECT Category_ID FROM Food_categories WHERE category_name='" + comboBox1.Text.Trim() + "' AND Discontinued=0)";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Product_Name".ToString()]);
            }
            con.Close();
        }
       
        private void FillCombo2() // fill category drop down
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

        private void Place_Order_form_Load(object sender, EventArgs e)
        {
            FillCombo2(); // filling category dropdown with form load
            // finding login id from username then using it to finde customer name 
            Find_idLogin();
            Find_Cust_Name();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        // if a different category is selected from drop down, product change accordingly
        {
            comboBox2.Items.Clear(); // clearing previous products in drop down if any
            FillCombo();
        }

        private void button1_Click(object sender, EventArgs e) // when place orde button clicked
        {
            // if smth not filled
            if (listBox1.Items.Count == 0 && (comboBox1.Text == "" || comboBox2.Text == "" || numericUpDown1.Value == 0) )
            {
                MessageBox.Show("Please select a product first!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if everything filled
            else
            {
                SqlConnection con = new SqlConnection(sqlcon);
                //Opening a connection
                con.Open();
                
                //commands are being created
                SqlCommand cmdNonQuery01 = new SqlCommand("INSERT INTO Orders (Customer_ID, Order_Date, Shipping_Date, Expected_Date, Order_Status, Total) VALUES  (@Customer_ID, @Order_Date, @Shipping_Date, @Expected_Date, @Order_Status, @Total); Select SCOPE_IDENTITY();", con);

                Find_date();
                Find_shipdate();
                Find_expdate();
                
                // adding parameters
                cmdNonQuery01.Parameters.Add(new SqlParameter("Customer_ID", idcust));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Order_Date", todayDate));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Shipping_Date", shipDate));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Expected_Date", expDate));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Order_Status", "Not Complete"));
                cmdNonQuery01.Parameters.Add(new SqlParameter("Total", label8.Text));
                int idOrder = Convert.ToInt32(cmdNonQuery01.ExecuteScalar()); // executes query and the pk generated is save here in order id

                con.Close();

                // looping over all strings/products in the list box at time of place order click
                foreach (string s in listBox1.Items)
                {
                    string[] subs = s.Split('/');
                    prodName = subs[1].Trim();
                    quantity = subs[2].Trim();
                    Find_idProduct();
                    
                    con.Open();

                    //commands are being created
                    SqlCommand cmdNonQuery = new SqlCommand("INSERT INTO Order_Details (Orders_Order_ID, Products_Product_ID, Quantity) VALUES (@Orders_Order_ID, @Products_Product_ID, @Quantity)", con);
                    // adding parameters
                    cmdNonQuery.Parameters.Add(new SqlParameter("Orders_Order_ID", idOrder.ToString()));
                    cmdNonQuery.Parameters.Add(new SqlParameter("Products_Product_ID", idProduct));
                    cmdNonQuery.Parameters.Add(new SqlParameter("Quantity", quantity));

                    cmdNonQuery.ExecuteNonQuery();
                    con.Close();

                    // finding the stock for each product and minusing according to quantity selected. updating it in database
                    Find_units_instock(prodName);
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET Units_In_Stock = @Units_In_Stock WHERE Product_ID='"+idProduct+"'", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@Units_In_Stock", (int.Parse(stockUnit) - int.Parse(quantity)) );
                    cmd.ExecuteNonQuery();

                }
                // success msg
                string message = "Your Order has been placed!";
                string title = "Success";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // when select button clicked
        {
            Find_units_instock(comboBox2.Text.Trim()); // find units in stock of the product selected
            if (comboBox1.Text == "" || comboBox2.Text == "" || numericUpDown1.Value == 0) // if everything is not filled
                MessageBox.Show("Please enter missing information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((int.Parse(stockUnit) - (int)numericUpDown1.Value) < 0) // if there are not enough units in stocl as desired quantity
            {
                MessageBox.Show(numericUpDown1.Value.ToString() + " units of this product are not available!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                listBox1.Items.Add(comboBox1.Text + " / " + comboBox2.Text + " / " + numericUpDown1.Value.ToString());

                ProductPrice(comboBox2.Text.Trim());
                total += int.Parse(cost) * (int)numericUpDown1.Value;
                label8.Text = total.ToString();

                // resetting
                comboBox1.Text = "";
                comboBox2.Text = "";
                label11.Text = "-";
                numericUpDown1.Value = 0;
            }
        }
        private void button3_Click(object sender, EventArgs e) // delete button clicked
        {
            string[] subs = listBox1.SelectedItem.ToString().Split('/'); // splitting the selected string in listbox
            prodName = subs[1].Trim();
            quantity = subs[2].Trim();
            ProductPrice(prodName);
            total -= int.Parse(cost) * int.Parse(quantity); // revert the total cost
            label8.Text = total.ToString();

            listBox1.Items.Remove(listBox1.SelectedItem); // remove from view

        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // when different product chosen from drop down, find its price and display 
        {
            ProductPrice(comboBox2.Text.Trim());
            label11.Text = cost;

        }
        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
