using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for EditCurrency.xaml
    /// </summary>
    public partial class EditCurrency : Window
    {
        public int RowId { get; private set; }
        public string Country { get; private set; }
        public string Currency { get; private set; }
        public string InLKR { get; private set; }

        public EditCurrency(int rowId, string country, string currency, string inLKR)
        {
            InitializeComponent();
            RowId = rowId;
            TxtCountry.Text = country;
            TxtCurrency.Text = currency;
            TxtInLKR.Text = inLKR;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Country = TxtCountry.Text;
            Currency = TxtCurrency.Text;
            InLKR = TxtInLKR.Text;
            DialogResult = true;
        }
    }
}
