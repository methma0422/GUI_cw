using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddNewTour.xaml
    /// </summary>
    public partial class AddNewTour : UserControl
    {
        private int locationCount = 0;

        public AddNewTour()
        {
            InitializeComponent();
            LoadDataIntoComboBox();
            
        }

        private void LoadDataIntoComboBox()
        {
            LoadGuestData();
            LoadDriverData();
            LoadGuideData();
            LoadVehicleData();
            LoadCurrencyData();
            LoadLocationData(ComBoxLocation);
            LoadAccommodationData(ComboxAccomo);
        }

        DBconnect db = new DBconnect();

        private void LoadGuestData()
        {
            DBconnect db = new DBconnect();
            string query = "SELECT guestId, fullName FROM guest";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComBoxGuest.Items.Add(new ComboBoxItem(reader["fullName"].ToString(), reader["guestId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading guest data: " + ex.Message);
                }
            }
        }

        private void LoadDriverData()
        {
            DBconnect db = new DBconnect();
            string query = "SELECT driverId, fullName FROM driver";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComBoxDriver.Items.Add(new ComboBoxItem(reader["fullName"].ToString(), reader["driverId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading driver data: " + ex.Message);
                }
            }
        }

        private void LoadGuideData()
        {
            DBconnect db = new DBconnect();
            string query = "SELECT guideId, fullName FROM guide";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComBoxGuide.Items.Add(new ComboBoxItem(reader["fullName"].ToString(), reader["guideId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading guide data: " + ex.Message);
                }
            }
        }

        private void LoadVehicleData()
        {
            DBconnect db = new DBconnect();
            string query = "SELECT vehicleId, vehicleType FROM vehicle";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComBoxVehicle.Items.Add(new ComboBoxItem(reader["vehicleType"].ToString(), reader["vehicleId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading vehicle data: " + ex.Message);
                }
            }
        }

        private void LoadCurrencyData()
        {
            DBconnect db = new DBconnect();
            string query = "SELECT currencyId, currency FROM currency";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComBoxCurrency.Items.Add(new ComboBoxItem(reader["currency"].ToString(), reader["currencyId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading currency data: " + ex.Message);
                }
            }
        }

        private void LoadLocationData(ComboBox locationComboBox)
        {
            DBconnect db = new DBconnect();
            string query = "SELECT locationId, locationName FROM location";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                locationComboBox.Items.Add(new ComboBoxItem(reader["locationName"].ToString(), reader["locationId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading location data: " + ex.Message);
                }
            }
        }

        private void LoadAccommodationData(ComboBox accommodationComboBox)
        {
            DBconnect db = new DBconnect();
            string query = "SELECT accomadationId, hotelName FROM accommodation";

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accommodationComboBox.Items.Add(new ComboBoxItem(reader["hotelName"].ToString(), reader["accomadationId"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading accommodation data: " + ex.Message);
                }
            }
        }

        

        

        

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DBconnect db = new DBconnect();

            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();

                    ComboBoxItem item1 = (ComboBoxItem)ComBoxDriver.SelectedItem;
                    string driverId = item1.Value.ToString();

                    ComboBoxItem item2 = (ComboBoxItem)ComBoxGuide.SelectedItem;
                    string guideId = item2.Value.ToString();

                    ComboBoxItem item3 = (ComboBoxItem)ComBoxGuest.SelectedItem;
                    string guestId = item3.Value.ToString();

                    ComboBoxItem item = (ComboBoxItem)ComBoxCurrency.SelectedItem;
                    string currencyId = item.Value.ToString();

                    string noOfMembers = TxtNumberOfTM.Text;

                    ComboBoxItem item4 = (ComboBoxItem)ComBoxVehicle.SelectedItem;
                    string vehicleId = item4.Value.ToString();

                    DateTime startDate = DatePickerStartDate.SelectedDate ?? DateTime.MinValue;
                    DateTime endDate = DatePickerEndingDate.SelectedDate ?? DateTime.MinValue;

                    string status = DetermineTourStatus(startDate, endDate);

                    int createdUserId = Session.UserID.Value;

                    // Calculate the total charge
                    decimal totalCharge = 0;
                    foreach (var child in AddNewTour1.Children)
                    {
                        if (child is TextBox chargeBox && chargeBox.Tag?.ToString() == "Charge")
                        {
                            if (decimal.TryParse(chargeBox.Text, out decimal charge))
                            {
                                totalCharge += charge;
                            }
                        }
                    }

                    string mainTableQuery = @"
                INSERT INTO tour (numberOfTourMembers, startingDate, endingDate, tourStatus, totalTourCharge, currencyId, driverId, guestId, guideId, vehicleId, createdUserId) 
                VALUES (@num, @startingD, @endingD, @status, @totalCharge, @currencyId, @driverId, @guestId, @guideId, @vehicleId, @createdUserId)";

                    using (MySqlCommand mainTableCmd = new MySqlCommand(mainTableQuery, conn))
                    {
                        mainTableCmd.Parameters.AddWithValue("@driverId", driverId);
                        mainTableCmd.Parameters.AddWithValue("@guideId", guideId);
                        mainTableCmd.Parameters.AddWithValue("@guestId", guestId);
                        mainTableCmd.Parameters.AddWithValue("@vehicleId", vehicleId);
                        mainTableCmd.Parameters.AddWithValue("@currencyId", currencyId);
                        mainTableCmd.Parameters.AddWithValue("@num", noOfMembers);
                        mainTableCmd.Parameters.AddWithValue("@startingD", startDate);
                        mainTableCmd.Parameters.AddWithValue("@endingD", endDate);
                        mainTableCmd.Parameters.AddWithValue("@status", status);
                        mainTableCmd.Parameters.AddWithValue("@totalCharge", totalCharge);
                        mainTableCmd.Parameters.AddWithValue("@createdUserId", createdUserId);
                        mainTableCmd.ExecuteNonQuery();
                    }

                    


                    // Retrieve the last inserted ID
                    long mainTableId = 0;
                    string getLastInsertedIdQuery = "SELECT LAST_INSERT_ID()";
                    using (MySqlCommand getIdCmd = new MySqlCommand(getLastInsertedIdQuery, conn))
                    {
                        object result = getIdCmd.ExecuteScalar();
                        mainTableId = Convert.ToInt64(result);
                    }

                    // Insert into 'tour_has_location' table

                    ComboBox locationBox = ComBoxLocation;
                    ComboBox accommodationBox = ComboxAccomo;
                    TextBox chargeTextBoxLocation = TxtCharge;
                    DatePicker datePicker = DateTimePickerLocation;
                    CheckBox accommodationCheck = CheckAccommodation;

                    if (locationBox == null || accommodationBox == null || chargeTextBoxLocation == null || datePicker == null || accommodationCheck == null)
                    {

                        string locationId = locationBox.SelectedValue?.ToString();
                        string accommodationId = accommodationCheck.IsChecked == true ? null : accommodationBox.SelectedValue?.ToString();
                        decimal charge = decimal.TryParse(chargeTextBoxLocation.Text, out decimal parsedCharge) ? parsedCharge : 0;
                        DateTime date = datePicker.SelectedDate ?? DateTime.MinValue;

                        string insertLocationQuery = @"
                        INSERT INTO tour_has_location (tourId, locationId, accommodationId, charge, date, createdUserId) 
                        VALUES (@tourId, @locationId, @accommodationId, @charge, @date, @createdUserId)";

                        using (MySqlCommand insertLocationCmd = new MySqlCommand(insertLocationQuery, conn))
                        {
                            insertLocationCmd.Parameters.AddWithValue("@tourId", mainTableId);
                            insertLocationCmd.Parameters.AddWithValue("@locationId", locationId);
                            insertLocationCmd.Parameters.AddWithValue("@accommodationId", (object)accommodationId ?? DBNull.Value);
                            insertLocationCmd.Parameters.AddWithValue("@charge", charge);
                            insertLocationCmd.Parameters.AddWithValue("@date", date);
                            insertLocationCmd.Parameters.AddWithValue("@createdUserId", createdUserId);
                            insertLocationCmd.ExecuteNonQuery();
                        }
                    }
                 

                    MessageBox.Show("Data saved successfully.");
                    if (this.Parent is ViewTours tourDetails)
                    {
                        tourDetails.LoadData();
                    }
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private string DetermineTourStatus(DateTime startDate, DateTime endDate)
        {
            // Determine current date
            DateTime currentDate = DateTime.Now;

            // Determine tour status based on dates
            if (currentDate < startDate)
            {
                return "upcoming";
            }
            else if (currentDate >= startDate && currentDate <= endDate)
            {
                return "ongoing";
            }
            else
            {
                return "ended";
            }
        }

        private void clearFields()
        {
            ComBoxGuest.Text = string.Empty;
            ComBoxDriver.Text = string.Empty;
            ComBoxGuide.Text = string.Empty;
            TxtNumberOfTM.Clear();
            ComBoxVehicle.Text = string.Empty;
            ComBoxCurrency.Text = string.Empty;
            DatePickerStartDate.Text = string.Empty;
            DatePickerEndingDate.Text = string.Empty;
            ComBoxLocation.Text = string.Empty;
            ComboxAccomo.Text = string.Empty;
            TxtCharge.Clear();
            DateTimePickerLocation.Text = string.Empty;
        }

        

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public ComboBoxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }
            public override string ToString() => Text;
        }
    }
}
