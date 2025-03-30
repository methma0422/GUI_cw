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
    /// Interaction logic for Currency.xaml
    /// </summary>
    public partial class Currency : UserControl
    {
        public Currency()
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
                    string query = "SELECT currencyId,country,currency,inLKR FROM currency";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        DataGridCurrency.ItemsSource = dt.DefaultView;
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
                int rowId = Convert.ToInt32(row["currencyId"]);
                string country = row["country"].ToString();
                string currency = row["currency"].ToString();
                string inLKR = row["inLKR"].ToString();

                // Open edit form (assuming it takes these parameters)
                EditCurrency editForm = new EditCurrency(rowId, country, currency, inLKR);
                if (editForm.ShowDialog() == true) // Use WPF's ShowDialog()
                {
                    row["country"] = editForm.Country;
                    row["currency"] = editForm.Currency;
                    row["inLKR"] = editForm.InLKR;

                    UpdateDatabase(rowId, editForm.Country, editForm.Currency, editForm.InLKR);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["currencyId"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }


        private void UpdateDatabase(int id, string country, string currency, string inLKR)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE currency SET country = @country, currency = @currency, inLKR = @inLKR WHERE currencyId = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@currency", currency);
                    cmd.Parameters.AddWithValue("@inLKR", inLKR);
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
                string query = "DELETE FROM currency WHERE currencyId = @Id";
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
                da = new MySqlDataAdapter("SELECT country,currency,inLKR FROM currency WHERE Country LIKE @country", conn);
                da.SelectCommand.Parameters.AddWithValue("@country", this.TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridCurrency.ItemsSource = dt.DefaultView;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCurrencyDetails currencyDetails = new AddCurrencyDetails();
            this.Visibility = Visibility.Collapsed;
            currencyDetails.Visibility = Visibility.Visible;
        }
    }
}
