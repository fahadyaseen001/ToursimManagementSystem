using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ToursimManagementSystem
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();

            LoadDataIntoDataGridView();
        }
        private int BookingID;

        //This method is to load the table data into the data grid view.
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            

            con.Open();

            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Booking";

            MySqlDataReader sdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Load(sdr);

            dataGridView1.DataSource= dt;
        }
        
        //This is to check if there are values provided in the fields to insert/update data.
        private bool IsValid()
        {
            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Hotel Name is required.");
                return false;
            }
           
            else if (dateTimePicker1 == null)
            {
                MessageBox.Show("Booking Date is required.");
                return false;
            }
            else if (dateTimePicker2 == null)
            {
                MessageBox.Show("Checkout Date is required.");
                return false;
            }
            return true;
        }

        //This method is to automatically show the selected row data in the relative textboxes/fields.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BookingID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePicker2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        //This method is to reset the form data fields.
        private void ResetFormData()
        {
            this.BookingID = 0;
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            textBox3.Clear();

            textBox1.Focus();
        }

        //Insert data into the table.
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Booking(BookingDate,HotelName,CheckoutDate) VALUES (@BookingDate ,@HotelName, @CheckoutDate)";
                cmd.Parameters.AddWithValue("@HotelName", textBox1.Text);
                
                cmd.Parameters.AddWithValue("@BookingDate", this.dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@CheckoutDate", this.dateTimePicker2.Text);
                if (textBox2.Text.Trim()!=String.Empty)
                {
                    cmd.CommandText="INSERT INTO Booking(PackageID) VALUES (@PackageID)";
                    cmd.Parameters.AddWithValue("@PackageID", int.Parse(textBox2.Text));
                }
                else
                {
                    cmd.Parameters.AddWithValue("PackageID", 0);
                }
                
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Data is inserted successfully");
            }
            LoadDataIntoDataGridView();
            ResetFormData();
        }
        
        //Update data of the table.
        private void button2_Click(object sender, EventArgs e)
        {

            if (BookingID != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Booking set BookingDate=@BookingDate,HotelName=@HotelName,CheckoutDate=@CheckoutDate WHERE BookingID=@ID";
                cmd.Parameters.AddWithValue("@HotelName", textBox1.Text);
                
                cmd.Parameters.AddWithValue("@BookingDate", this.dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@CheckoutDate", this.dateTimePicker2.Text);
                cmd.Parameters.AddWithValue("@ID",this.BookingID);

                if (textBox2.Text.Trim() != String.Empty)
                {
                    cmd.CommandText = "UPDATE Booking set PackageID=@PackageID WHERE BookingID=@ID";
                    cmd.Parameters.AddWithValue("@PackageID", int.Parse(textBox2.Text));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PackageID", 0);
                }
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Data is updated successfully");

                LoadDataIntoDataGridView();

                ResetFormData();
            }
            else
            {
                MessageBox.Show("Please select a booking to update!","Select Booking");
            }
        }


        //Delete data from the table
        private void button3_Click(object sender, EventArgs e)
        {
            if (BookingID != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Booking WHERE BookingID=@ID";
                cmd.Parameters.AddWithValue("@ID", this.BookingID);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Data is deleted successfully");

                LoadDataIntoDataGridView();

                ResetFormData();
            }
            else
            {
                MessageBox.Show("Please select a booking to delete!", "Select Booking");

            }
            
        }

        //Reset Button
        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormData();
            LoadDataIntoDataGridView();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Trim() != String.Empty)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());


                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "Select * from Booking WHERE HotelName=@HotelName";
                cmd.Parameters.AddWithValue("@HotelName", textBox3.Text);
                
            

                MySqlDataReader sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(sdr);

                if(dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No Record Found!");
                }
            
            }
            else
            {
                MessageBox.Show("Please enter any value to search a record.", "Search Value Required");
            }
        }
    }
}
