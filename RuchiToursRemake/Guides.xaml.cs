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
    /// Interaction logic for Guides.xaml
    /// </summary>
    public partial class Guides : UserControl
    {
        public Guides()
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
                    string query = "SELECT guideId,imagePath,fullName,contactNO,address FROM guide";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridGuideDetails.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["guideId"]);
                string fullName = row["fullName"].ToString();
                string contactNo = row["contactNO"].ToString();
                string address = row["address"].ToString();
                byte[] image = (byte[])row["imagePath"];

                // Open edit form (assuming it takes these parameters)
                EditGuide editForm = new EditGuide(rowId, fullName, contactNo, address, image);
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
                int rowId = Convert.ToInt32(row["guideId"]);

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
                string query = "UPDATE guide SET fullName = @fullName, contactNO = @contactNo, address = @address, imagePath = @image WHERE guideId = @Id";
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
                string query = "DELETE FROM guide WHERE guideId = @Id";
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
                da = new MySqlDataAdapter("SELECT imagePath,fullName,contactNO,address FROM guide WHERE fullName LIKE @fullName", conn);
                da.SelectCommand.Parameters.AddWithValue("@fullName", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridGuideDetails.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddGuideDetails guideDetails = new AddGuideDetails();
            this.Visibility = Visibility.Collapsed;
            guideDetails.Visibility = Visibility.Visible;
        }
    }
}
