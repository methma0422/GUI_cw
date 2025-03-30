using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for AddVehicleDetails.xaml
    /// </summary>
    public partial class AddVehicleDetails : UserControl
    {
        public AddVehicleDetails()
        {
            InitializeComponent();
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                imgVehicle.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null) return null;

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Example data to be saved
            string data1 = TxtVehicleType.Text;
            string data2 = TxtCapacity.Text;
            string data3 = TxtVRNumber.Text;

            // Retrieve the image from the PictureBox and convert it to a byte array
            byte[] imageData = BitmapImageToByteArray(imgVehicle.Source as BitmapImage);

            if (imageData != null && data1 != "" && data2 != "" && data3 != "")
            {

                int createdUserId = Session.UserID.Value;

                DBconnect db = new DBconnect();

                // SQL insert query
                string query = "INSERT INTO vehicle (vehicleType, capacity, vehicleNO, imagePath, createdUserId) VALUES (@data1, @data2, @data3, @imageData, @createdUserId)";

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
                            cmd.Parameters.Add("@imageData", MySqlDbType.Blob).Value = imageData;
                            cmd.Parameters.AddWithValue("@createdUserId", createdUserId);

                            // Execute the command
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Vehicle details saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Notify the existing GuestDetails instance to refresh data
                                if (this.Parent is Vehicles vehicleDetails)
                                {
                                    vehicleDetails.LoadData();
                                }

                                // Clear fields after successful save
                                ClearTextBoxes();
                                imgVehicle.Source = null;
                            }
                            else
                            {
                                MessageBox.Show("Failed to save vehicle details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may have occurred
                        MessageBox.Show("An error occurred: " + ex.Message);
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
            TxtVehicleType.Clear();
            TxtCapacity.Clear();
            TxtVRNumber.Clear();
        }
    }
}
