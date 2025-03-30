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

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for EditGuest.xaml
    /// </summary>
    public partial class EditGuest : Window
    {
        public EditGuest(int rowId, string fullName, string contactNo, string email, string country, string passportNo)
        {
            InitializeComponent();
            RowId = rowId;
            TxtFullName.Text = fullName;
            TxtContactNo.Text = contactNo;
            TxtEmail.Text = email;
            TxtCountry.Text = country;
            TxtPassportNo.Text = passportNo;
        }

        public int RowId { get; private set; }
        public string FullName { get; private set; }
        public string ContactNo { get; private set; }
        public string Email { get; private set; }
        public string Country { get; private set; }
        public string PassportNo { get; private set; }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            FullName = TxtFullName.Text;
            ContactNo = TxtContactNo.Text;
            Email = TxtEmail.Text;
            Country = TxtCountry.Text;
            PassportNo = TxtPassportNo.Text;
            DialogResult = true; 
        }
    }
}
