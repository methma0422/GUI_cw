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
    /// Interaction logic for ViewTours.xaml
    /// </summary>
    public partial class ViewTours : UserControl
    {
        public ViewTours()
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
                    string query = @"
                        SELECT 
                            t.tourId as Tour_Id,
                            g.fullName as guestName,
                            d.fullName as driverName,
                            gui.fullName as guideName,
                            v.vehicleType as vehicleType,
                            cu.currency as currency,
                            t.numberOfTourMembers as Number_of_Members, 
                            t.startingDate, 
                            t.endingDate, 
                            t.tourStatus, 
                            IFNULL(SUM(thl.charge), 0) as totalTourCharge,
                            GROUP_CONCAT(DISTINCT l.locationName ORDER BY l.locationName SEPARATOR ', ') as Location_Names,
                            GROUP_CONCAT(DISTINCT a.hotelName ORDER BY a.hotelName SEPARATOR ', ') as Hotel_Names,
                            MIN(thl.date) as First_Date, 
                            MAX(thl.date) as Last_Date
                        FROM 
                            tour t
                        LEFT JOIN 
                            tour_has_location thl ON t.tourId = thl.tourId
                        LEFT JOIN 
                            location l ON thl.locationId = l.locationId
                        LEFT JOIN 
                            accommodation a ON thl.accommodationId = a.accomadationId
                        LEFT JOIN
                            guest g ON t.guestId = g.guestId
                        LEFT JOIN
                            driver d ON t.driverId = d.driverId
                        LEFT JOIN
                            guide gui ON t.guideId = gui.guideId
                        LEFT JOIN
                            vehicle v ON t.vehicleId = v.vehicleId
                        LEFT JOIN 
                            currency cu ON t.currencyId = cu.currencyId
                        GROUP BY 
                            t.tourId";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataGridViewTours.ItemsSource = dt.DefaultView;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DataRowView row)
            {
                int rowId = Convert.ToInt32(row["Tour_Id"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteFromDatabase(rowId);
                    LoadData(); // Refresh DataGrid after deletion
                }
            }
        }

        private void DeleteFromDatabase(int id)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                string query = "DELETE FROM tour WHERE tourId = @tourId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tourId", id);
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
                string query = @"
            SELECT 
                t.tourId as Tour_Id,
                g.fullName as guestName,
                d.fullName as driverName,
                gui.fullName as guideName,
                v.vehicleType as vehicleType,
                cu.currency as currency,
                t.numberOfTourMembers as Number_of_Members, 
                t.startingDate, 
                t.endingDate, 
                t.tourStatus, 
                SUM(thl.charge) as totalTourCharge,
                GROUP_CONCAT(DISTINCT l.locationName ORDER BY l.locationName SEPARATOR ', ') as Location_Names,
                GROUP_CONCAT(DISTINCT a.hotelName ORDER BY a.hotelName SEPARATOR ', ') as Hotel_Names,
                MIN(thl.date) as First_Date, 
                MAX(thl.date) as Last_Date
            FROM 
                tour t
            LEFT JOIN 
                tour_has_location thl ON t.tourId = thl.tourId
            LEFT JOIN 
                location l ON thl.locationId = l.locationId
            LEFT JOIN 
                accommodation a ON thl.accommodationId = a.accomadationId
            LEFT JOIN
                guest g ON t.guestId = g.guestId
            LEFT JOIN
                driver d ON t.driverId = d.driverId
            LEFT JOIN
                guide gui ON t.guideId = gui.guideId
            LEFT JOIN
                vehicle v ON t.vehicleId = v.vehicleId
            LEFT JOIN 
                currency cu ON t.currencyId = cu.currencyId
            WHERE 
                g.fullName LIKE @guestName
            GROUP BY 
                t.tourId";

                da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@guestName", "%" + TxtSearch.Text + "%");
                dt = new DataTable();
                da.Fill(dt);
                DataGridViewTours.ItemsSource = dt.DefaultView;
            }
        }

        
    }
}
