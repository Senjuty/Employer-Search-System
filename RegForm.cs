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
    public partial class RegForm : Form
    {
        string connectionString;
        SqlConnection connection;
        public RegForm()
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            int agecheck = 0;
            int flag = 1;


            if ((NameTextBox.Text == "") || (AgeTextBox.Text == "") || (CityComboBox.Text == "")
                || (PasswordTextBox.Text == ""))
            {
                MessageBox.Show("Please fill up the empty fields!");
                flag = 0;
        
            }
            else if (!Int32.TryParse(AgeTextBox.Text, out agecheck))
            {
                agecheck = -1;
                MessageBox.Show("Please enter a valid age!");
                flag = 0;

            }
            else if (Int32.TryParse(AgeTextBox.Text, out agecheck))
            {
                if (agecheck > 75 || agecheck < 18)
                {
                    MessageBox.Show("Age of the user should be between 18 and 75!");
                    flag = 0;

                }
            }
            else if (NameTextBox.Text.Length > 50)
            {
                MessageBox.Show("Maximum name length is 50!");
                flag = 0;

            }

            else if (PasswordTextBox.Text.Length > 10)
            {
                MessageBox.Show("Maximum password length is 10!");
                flag = 0;

            }

            if (flag == 1)
            {
                // inserting into database

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [dbo].[User] (Name, Type, Password, Age, City, JobCategory) VALUES (@Name, @Type, @Password, @Age, @City, @Job);";

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                        cmd.Parameters.AddWithValue("@Type", "Employer");
                        cmd.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                        Int32.TryParse(AgeTextBox.Text, out agecheck);
                        cmd.Parameters.AddWithValue("@Age", agecheck);
                        cmd.Parameters.AddWithValue("@City", CityComboBox.Text);
                        cmd.Parameters.AddWithValue("@Job", JobComboBox.Text);
                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            this.Close();
                            MessageBox.Show("User Registered!");

                        }

                        catch (SqlException se)
                        {
                            MessageBox.Show(se.ToString());
                        }

                    }
                }
            }

        }

        private void JobComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
