using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for EditAccommodation.xaml
    /// </summary>
    public partial class EditAccommodation : Window
    {
        public int RowId { get; private set; }
        public string HotelName { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string District { get; private set; }
        public byte[] Image { get; private set; }

        public EditAccommodation(int rowId, string hotelName, string city, string province, string district, byte[] image)
        {
            InitializeComponent();
            RowId = rowId;
            TxtHotelName.Text = hotelName;
            TxtCity.Text = city;
            TxtProvince.Text = province;
            TxtDistrict.Text = district;
            if (image != null)
            {
                imgAccommo.Source = ByteArrayToBitmapImage(image);
            }
        }

        private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
            }
            return bitmap;
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                imgAccommo.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            HotelName = TxtHotelName.Text;
            City = TxtCity.Text;
            Province = TxtProvince.Text;
            District = TxtDistrict.Text;
            if (imgAccommo.Source is BitmapImage bitmapImage)
            {
                Image = BitmapImageToByteArray(bitmapImage);
            }
            DialogResult = true;
        }

        private byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null) return null;

            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
    }
}
