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
    /// Interaction logic for HotelAndAccommodation.xaml
    /// </summary>
    public partial class HotelAndAccommodation : UserControl
    {
        public HotelAndAccommodation()
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
                    string query = "SELECT accomadationId,imagePath,hotelName,city,province,district FROM accommodation";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridAccommodation.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["accomadationId"]);
                string hotelName = row["hotelName"].ToString();
                string city = row["city"].ToString();
                string province = row["province"].ToString();
                string district = row["district"].ToString();
                byte[] image = (byte[])row["imagePath"];

                // Open edit form (assuming it takes these parameters)
                EditAccommodation editForm = new EditAccommodation(rowId, hotelName, city, province, district, image);
                if (editForm.ShowDialog() == true) // Use WPF's ShowDialog()
                {
                    row["hotelName"] = editForm.HotelName;
                    row["city"] = editForm.City;
                    row["province"] = editForm.Province;
                    row["district"] = editForm.District;
                    row["imagePath"] = editForm.Image;
                    UpdateDatabase(rowId, editForm.HotelName, editForm.City, editForm.Province, editForm.District, editForm.Image);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["accomadationId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string hotelName, string city, string province, string district, object imagePath)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE accommodation SET hotelName = @hotelName, city = @city, province = @province, district = @district,imagePath = @image WHERE accomadationId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hotelName", hotelName);
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
                string query = "DELETE FROM accommodation WHERE accomadationId = @Id";
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
                da = new MySqlDataAdapter("SELECT imagePath,hotelName,city,province,district FROM accommodation WHERE HotelName LIKE @hotelName", conn);
                da.SelectCommand.Parameters.AddWithValue("@hotelName", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridAccommodation.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAccommodationDetails accommodationDetails = new AddAccommodationDetails();
            this.Visibility = Visibility.Collapsed;
            accommodationDetails.Visibility = Visibility.Visible;
        }
    }
}
