using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Vehicles.xaml
    /// </summary>
    public partial class Vehicles : UserControl
    {
        public Vehicles()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                DBconnect db = new DBconnect();
                using (MySqlConnection conn = db.GetConnection())
                {
                    string query = "SELECT vehicleId,imagePath,vehicleType,capacity,vehicleNO FROM vehicle";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridVehicles.ItemsSource = dt.DefaultView;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["vehicleId"]);
                string vehicleType = row["vehicleType"].ToString();
                string capacity = row["capacity"].ToString();
                string vehicleNO = row["vehicleNO"].ToString();
                byte[] image = (byte[])row["imagePath"];

                // Open edit form (assuming it takes these parameters)
                EditVehicle editForm = new EditVehicle(rowId, vehicleType, capacity, vehicleNO, image);
                if (editForm.ShowDialog() == true) 
                {
                    row["vehicleType"] = editForm.VehicleType;
                    row["capacity"] = editForm.Capacity;
                    row["vehicleNO"] = editForm.VehicleNO;
                    row["imagePath"] = editForm.Image;
                    UpdateDatabase(rowId, editForm.VehicleType, editForm.Capacity, editForm.VehicleNO, editForm.Image);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["vehicleId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string vehicleType, string capacity, string vehicleNO, object imagePath)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE vehicle SET vehicleType = @vehicleType, capacity = @capacity, vehicleNO = @vehicleNO, imagePath = @image WHERE vehicleId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@vehicleType", vehicleType);
                    cmd.Parameters.AddWithValue("@capacity", capacity);
                    cmd.Parameters.AddWithValue("@vehicleNO", vehicleNO);
                    cmd.Parameters.AddWithValue("@image", imagePath);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteFromDatabase(int id)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "DELETE FROM vehicle WHERE vehicleId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                MySqlDataAdapter da;
                DataTable dt;
                conn.Open();
                da = new MySqlDataAdapter("SELECT imagePath,vehicleType,capacity,vehicleNO FROM vehicle WHERE vehicleType LIKE @vehicleType", conn);
                da.SelectCommand.Parameters.AddWithValue("@vehicleType", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridVehicles.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddVehicleDetails vehicleDetails = new AddVehicleDetails();
            this.Visibility = Visibility.Collapsed;
            vehicleDetails.Visibility = Visibility.Visible;
        }
    }
}
