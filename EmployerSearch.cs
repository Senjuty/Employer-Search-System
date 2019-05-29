using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace LeaveManagementSystem
{
    public partial class EmployerSearch : Form
    {
        string connectionString;
        SqlConnection connection;
        public EmployerSearch()
        {
            InitializeComponent();
            JobComboBox.Items.Add("Information Technology");
            JobComboBox.Items.Add("Finance");
            JobComboBox.Items.Add("Social Service");
            JobComboBox.Items.Add("Media");
            JobComboBox.Items.Add("Healthcare");
            JobComboBox.Items.Add("Education");
            JobComboBox.Items.Add("Administration");
            CityComboBox.Items.Add("Darmstadt");
            CityComboBox.Items.Add("Frankfurt");
            CityComboBox.Items.Add("Mainz");
            CityComboBox.Items.Add("Wiesbaden");
            CityComboBox.Items.Add("Hanau");
            CityComboBox.Items.Add("Düsseldorf");
            CityComboBox.Items.Add("Köln");
            connectionString = ConfigurationManager.ConnectionStrings["LeaveManagementSystem.Properties.Settings.LeaveManagementConnectionString"].ConnectionString;
        }

        private void EmployerSearch_Load(object sender, EventArgs e)
        {

        }

        private void SearchButtton_Click(object sender, EventArgs e)
        {
            string query = "SELECT Name FROM [dbo].[User] where City = @City and JobCategory = @Job;";
            string result = "";

            // reading from database
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@City", CityComboBox.Text);
                    cmd.Parameters.AddWithValue("@Job", JobComboBox.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
             
                    while (reader.Read())
                    {

                        result = String.Concat(result, reader["Name"].ToString(),"\n");
                 
                    }
                    
                    MessageBox.Show(result);


                    reader.Close();

                }

            }
        }
    }
}
