using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for Driver.xaml
    /// </summary>
    public partial class Driver : UserControl
    {
        

        public Driver()
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
                    string query = "SELECT driverId, fullName, contactNO, address, imagePath FROM driver";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridDriver.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["driverId"]);
                string fullName = row["fullName"].ToString();
                string contactNo = row["contactNO"].ToString();
                string address = row["address"].ToString();
                byte[] image = (byte[])row["imagePath"];

                // Open edit form (assuming it takes these parameters)
                EditDriver editForm = new EditDriver(rowId, fullName, contactNo, address, image);
                if (editForm.ShowDialog() == true) // Use WPF's ShowDialog()
                {
                    row["fullName"] = editForm.FullName;
                    row["contactNO"] = editForm.ContactNo;
                    row["address"] = editForm.Address;
                    row["imagePath"] = editForm.Image;
                    UpdateDatabase(rowId, editForm.FullName, editForm.ContactNo, editForm.Address, editForm.Image);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["driverId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string fullName, string contactNo, string address, object imagePath)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE driver SET fullName = @fullName, contactNO = @contactNo, address = @address, imagePath = @image WHERE driverId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@contactNo", contactNo);
                    cmd.Parameters.AddWithValue("@address", address);
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
                string query = "DELETE FROM driver WHERE driverId = @Id";
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
                da = new MySqlDataAdapter("SELECT imagePath,fullName,contactNO,address FROM driver WHERE fullName LIKE @fullName", conn);
                da.SelectCommand.Parameters.AddWithValue("@fullName", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridDriver.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddDriverDetails driverDetails = new AddDriverDetails();
            this.Visibility = Visibility.Collapsed;
            driverDetails.Visibility = Visibility.Visible;
        }
    }
}
