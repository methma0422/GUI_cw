using MySql.Data.MySqlClient;
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
    /// Interaction logic for AddCurrencyDetails.xaml
    /// </summary>
    public partial class AddCurrencyDetails : UserControl
    {
        public AddCurrencyDetails()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Example data to be saved
            string data1 = TxtCountry.Text;
            string data2 = TxtCurrency.Text;
            string data3 = TxtInLKR.Text;

            if (data1 != "" && data2 != "" && data3 != "")
            {
                int createdUserId = Session.UserID.Value;

                DBconnect db = new DBconnect();

                // SQL insert query
                string query = "INSERT INTO currency (country, currency, inLKR, createdUserId) VALUES (@data1, @data2, @data3, @createdUserId)";

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
                            cmd.Parameters.AddWithValue("@createdUserId", createdUserId);




                            // Execute the command
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Currency details saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Notify the existing GuestDetails instance to refresh data
                                if (this.Parent is Currency currencyDetails)
                                {
                                    currencyDetails.LoadData();
                                }

                                // Clear fields after successful save
                                ClearTextBoxes();
                            }
                            else
                            {
                                MessageBox.Show("Failed to save Currency details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            TxtCountry.Clear();
            TxtCurrency.Clear();
            TxtInLKR.Clear();
        }
    }
}
