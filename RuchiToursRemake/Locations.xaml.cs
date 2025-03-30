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
    /// Interaction logic for Locations.xaml
    /// </summary>
    public partial class Locations : UserControl
    {
        public Locations()
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
                    string query = "SELECT locationId,imagePath,locationName,city,province,district FROM location";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridLocation.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["locationId"]);
                string locationName = row["locationName"].ToString();
                string city = row["city"].ToString();
                string province = row["province"].ToString();
                string district = row["district"].ToString();
                byte[] image = (byte[])row["imagePath"];

                // Open edit form (assuming it takes these parameters)
                EditLocation editForm = new EditLocation(rowId, locationName, city, province, district, image);
                if (editForm.ShowDialog() == true) // Use WPF's ShowDialog()
                {
                    row["locationName"] = editForm.LocationName;
                    row["city"] = editForm.City;
                    row["province"] = editForm.Province;
                    row["district"] = editForm.District;
                    row["imagePath"] = editForm.Image;
                    UpdateDatabase(rowId, editForm.LocationName, editForm.City, editForm.Province, editForm.District, editForm.Image);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["locationId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string locationName, string city, string province, string district, object imagePath)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE location SET locationName = @locationName, city = @city, province = @province, district = @district,imagePath = @image WHERE locationId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@locationName", locationName);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@province", province);
                    cmd.Parameters.AddWithValue("@district", district);
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
                string query = "DELETE FROM location WHERE locationId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLocaitonDetails locationDetails = new AddLocaitonDetails();
            this.Visibility = Visibility.Collapsed;
            locationDetails.Visibility = Visibility.Visible;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                MySqlDataAdapter da;
                DataTable dt;
                conn.Open();
                da = new MySqlDataAdapter("SELECT imagePath,locationName,city,province,district FROM location WHERE locationName LIKE @locationName", conn);
                da.SelectCommand.Parameters.AddWithValue("@locationName", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridLocation.ItemsSource = dt.DefaultView;
            }
        }
    }
}
