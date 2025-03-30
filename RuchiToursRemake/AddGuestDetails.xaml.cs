using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for AddGuestDetails.xaml
    /// </summary>
    public partial class AddGuestDetails : UserControl
    {
        public AddGuestDetails()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Example data to be saved
            string data1 = TxtFullName.Text;
            string data2 = TxtContactNo.Text;
            string data3 = TxtEmail.Text;
            string data4 = TxtCountry.Text;
            string data5 = TxtPassportNo.Text;

            if (data1 != "" && data2 != "" && data3 != "" && data4 != "" && data5 != "")
            {
                int createdUserId = Session.UserID.Value;

                DBconnect db = new DBconnect();

                // SQL insert query
                string query = "INSERT INTO guest (fullName, contactNO, email, country, passportNO, createdUserId) VALUES (@data1, @data2, @data3, @data4, @data5, @createdUserId)";

                // Create a new MySQL connection
                using (MySqlConnection conn = db.GetConnection())
                {
                    try
                    {
                        // Open the connection
                        conn.Open();

                        // Create a MySQL command
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            // Add parameters to the command
                            cmd.Parameters.AddWithValue("@data1", data1);
                            cmd.Parameters.AddWithValue("@data2", data2);
                            cmd.Parameters.AddWithValue("@data3", data3);
                            cmd.Parameters.AddWithValue("@data4", data4);
                            cmd.Parameters.AddWithValue("@data5", data5);
                            cmd.Parameters.AddWithValue("@createdUserId", createdUserId);




                            // Execute the command
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Guest details saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Notify the existing GuestDetails instance to refresh data
                                if (this.Parent is GuestDetails guestDetails)
                                {
                                    guestDetails.LoadData();
                                }

                                // Clear fields after successful save
                                ClearTextBoxes();
                            }
                            else
                            {
                                MessageBox.Show("Failed to save guest details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill all required fields");
            }

            
        }

        private void ClearTextBoxes()
        {
            // Clear each text box
            TxtFullName.Clear();
            TxtContactNo.Clear();
            TxtEmail.Clear();
            TxtCountry.Clear();
            TxtPassportNo.Clear();
        }
    }
}
