using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void BtnAddNewTour_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Visible;
            ViewTours2.Visibility = Visibility.Hidden;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
        }

        private void BtnViewTours_Click(object sender, RoutedEventArgs e)
        {
            ViewTours2.Visibility = Visibility.Visible;
            AddNewTour2.Visibility = Visibility.Hidden;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
        }

        private void BtnGuestDetails_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Hidden;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Visible;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Visible;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            GuestDetails2.LoadData();
        }

        private void BtnDrivers_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Visible;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Visible;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            Driver2.LoadData();
            
        }

        private void BtnGuides_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Visible;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Visible;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            Guide2.LoadData();
        }

        private void BtnVehicles_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Visible;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Visible;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            Vehicles2.LoadData();
        }

        private void BtnCurrencies_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Visible;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Visible;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            Currency2.LoadData();
        }

        private void BtnLocations_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Visible;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Visible;
            AddAccomodation2.Visibility = Visibility.Collapsed;
            Locations2.LoadData();
        }

        private void BtnAccomodation_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Visible;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Visible;
            Accommodation2.LoadData();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult check = MessageBox.Show("Are You sure you want to logout?"
                , "Confirmation Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (check == MessageBoxResult.Yes)
            {
                MainWindow loginForm = new MainWindow();
                loginForm.Show();
                this.Hide();
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour2.Visibility = Visibility.Collapsed;
            ViewTours2.Visibility = Visibility.Collapsed;
            GuestDetails2.Visibility = Visibility.Collapsed;
            Driver2.Visibility = Visibility.Collapsed;
            Guide2.Visibility = Visibility.Collapsed;
            Vehicles2.Visibility = Visibility.Collapsed;
            Currency2.Visibility = Visibility.Collapsed;
            Locations2.Visibility = Visibility.Collapsed;
            Accommodation2.Visibility = Visibility.Collapsed;
            AddGuest2.Visibility = Visibility.Collapsed;
            AddDriver2.Visibility = Visibility.Collapsed;
            AddGuide2.Visibility = Visibility.Collapsed;
            AddVehicle2.Visibility = Visibility.Collapsed;
            AddCurrency2.Visibility = Visibility.Collapsed;
            AddLocation2.Visibility = Visibility.Collapsed;
            AddAccomodation2.Visibility = Visibility.Collapsed;
        }
    }
}
