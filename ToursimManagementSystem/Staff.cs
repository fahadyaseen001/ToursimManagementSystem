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
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();

            LoadDataIntoDataGridView();
        }
        private int StaffID;

        //This method is to load the table data into the data grid view.
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            

            con.Open();

            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from Staff";

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
                MessageBox.Show("Staff Name is required.");
                return false;
            }
           
            else if (textBox2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Age is required.");
                return false;
            }
            else if (textBox4.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Phone Number is required.");
                return false;
            }
            else if (textBox5.Text.Trim() == String.Empty)
            {
                MessageBox.Show("CNIC is required.");
            }
                return true;
        }

        //This method is to automatically show the selected row data in the relative textboxes/fields.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StaffID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        //This method is to reset the form data fields.
        private void ResetFormData()
        {
            this.StaffID = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();


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
                cmd.CommandText = "INSERT INTO Staff(Name,Age,PhoneNumber,CNIC) VALUES (@Name ,@Age, @PhoneNumber,@CNIC)";
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age", int.Parse(textBox2.Text));
                cmd.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
                cmd.Parameters.AddWithValue("@CNIC", textBox5.Text);

                
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

            if (StaffID != 0)
            {
                if(IsValid())
                {
                    MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                    con.Open();

                    MySqlCommand cmd;
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Staff set Name=@Name,Age=@Age,PhoneNumber=@PhoneNumber,CNIC=@CNIC WHERE StaffID=@ID";
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CNIC", textBox5.Text);

                    cmd.Parameters.AddWithValue("@ID", this.StaffID);


                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Data is updated successfully");

                    
                }
                LoadDataIntoDataGridView();

                ResetFormData();

            }
            else
            {
                MessageBox.Show("Please select a staff to update!","Select Staff");
            }
        }


        //Delete data from the table
        private void button3_Click(object sender, EventArgs e)
        {
            if (StaffID != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();

                MySqlCommand cmd;
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Staff WHERE StaffID=@ID";
                cmd.Parameters.AddWithValue("@ID", this.StaffID);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Data is deleted successfully");

                LoadDataIntoDataGridView();

                ResetFormData();
            }
            else
            {
                MessageBox.Show("Please select a staff to delete!", "Select Staff");

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

                if(radioButton2.Checked)
                {
                    cmd.CommandText = "Select * from Staff WHERE Age=@Age";
                    cmd.Parameters.AddWithValue("@Age", int.Parse(textBox3.Text));
                }
                else if(radioButton3.Checked)
                {
                    cmd.CommandText = "Select * from Staff WHERE PhoneNumber=@PhoneNumber";
                    cmd.Parameters.AddWithValue("@PhoneNumber",textBox3.Text);
                }
                else if(radioButton4.Checked)
                {
                    cmd.CommandText = "Select * from Staff WHERE CNIC=@CNIC";
                    cmd.Parameters.AddWithValue("@CNIC", textBox3.Text);
                }
                else
                {
                    cmd.CommandText = "Select * from Staff WHERE Name=@Name";
                    cmd.Parameters.AddWithValue("@Name", textBox3.Text);
                }    
                
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
