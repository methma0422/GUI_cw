using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for GuestDetails.xaml
    /// </summary>
    public partial class GuestDetails : UserControl
    {
        

        public GuestDetails()
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
                    string query = "SELECT guestId, fullName, contactNO, email, country, passportNO FROM guest";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataGridGuestDetails.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["guestId"]);
                string fullName = row["fullName"].ToString();
                string contactNo = row["contactNO"].ToString();
                string email = row["email"].ToString();
                string country = row["country"].ToString();
                string passportNo = row["passportNO"].ToString();

                // Open edit form (assuming it takes these parameters)
                EditGuest editForm = new EditGuest(rowId, fullName, contactNo, email, country, passportNo);
                if (editForm.ShowDialog() == true) // Use WPF's ShowDialog()
                {
                    row["fullName"] = editForm.FullName;
                    row["contactNO"] = editForm.ContactNo;
                    row["email"] = editForm.Email;
                    row["country"] = editForm.Country;
                    row["passportNO"] = editForm.PassportNo;
                    UpdateDatabase(rowId, editForm.FullName, editForm.ContactNo, editForm.Email, editForm.Country, editForm.PassportNo);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["guestId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string fullName, string contactNo, string email, string country, string passportNo)
        {
            DBconnect db = new DBconnect();
            
            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE guest SET fullName = @fullName, contactNO = @contactNo, email = @Email, country = @Country, passportNO = @PassportNo WHERE guestId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@contactNo", contactNo);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@PassportNo", passportNo);
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
                string query = "DELETE FROM guest WHERE guestId = @Id";
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

            using (MySqlConnection conn = db.GetConnection( ))
            {
                MySqlDataAdapter da;
                DataTable dt;
                conn.Open();
                da = new MySqlDataAdapter("SELECT guestId, fullName, contactNO, email, country, passportNO FROM guest WHERE fullName LIKE @fullName", conn);
                da.SelectCommand.Parameters.AddWithValue("@fullName", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridGuestDetails.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddGuestDetails guestDetails = new AddGuestDetails();
            this.Visibility = Visibility.Collapsed;
            guestDetails.Visibility = Visibility.Visible;
        }

        
    }
}
