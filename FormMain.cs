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
    public partial class LoginForm : Form
    {
        string connectionString;
        SqlConnection connection;

        public LoginForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["LeaveManagementSystem.Properties.Settings.LeaveManagementConnectionString"].ConnectionString;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
      
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM [dbo].[User] where Name = @Name and Password = @Password;";
            string result = " ";
    
            // reading from database
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        result = String.Concat(result, reader["Name"].ToString(), " : ", reader["Password"].ToString());
                        MessageBox.Show("Login successful!");
                        string usertype = reader["Type"].ToString();
                        string usertype1 = "Employee";

                        if (usertype == usertype1)
                        {
                            
                            Form EmployerSearch = new EmployerSearch();
                            EmployerSearch.Show();

                        }
                   
                    }
                    else
                    {
                        MessageBox.Show("Name and password do not match!");
                    }


                    reader.Close();

                }
           
            }
       
        }

        private void Regbutton_Click(object sender, EventArgs e)
        {
            // opening registration form and hiding login form.

            Form RegForm = new RegForm();
            RegForm.Show();
            
        }

        private void RegButton2_Click(object sender, EventArgs e)
        {
            Form Form1 = new Form1();
            Form1.Show();
        }
    }
}
