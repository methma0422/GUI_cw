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
using System.Xml.Linq;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for EditVehicle.xaml
    /// </summary>
    public partial class EditVehicle : Window
    {
        public int RowId { get; private set; }
        public string VehicleType { get; private set; }
        public string Capacity { get; private set; }
        public string VehicleNO { get; private set; }
        public byte[] Image { get; private set; }

        public EditVehicle(int rowId, string vehicleType, string capacity, string vehicleNO, byte[] image)
        {
            InitializeComponent();
            RowId = rowId;
            TxtVehicleType.Text = vehicleType;
            TxtCapacity.Text = capacity;
            TxtVRNumber.Text = vehicleNO;
            if (image != null)
            {
                imgVehicle.Source = ByteArrayToBitmapImage(image);
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
                imgVehicle.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            VehicleType = TxtVehicleType.Text;
            Capacity = TxtCapacity.Text;
            VehicleNO = TxtVRNumber.Text;
            if (imgVehicle.Source is BitmapImage bitmapImage)
            {
                Image = BitmapImageToByteArray(bitmapImage);
            }
            DialogResult = true;
        }
    }
}
