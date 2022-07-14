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
    public partial class Menu : Form
    {
        public Menu()
        {

            InitializeComponent();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking();
            booking.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Packages packages = new Packages();
            packages.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Show();
        }
    }
}
