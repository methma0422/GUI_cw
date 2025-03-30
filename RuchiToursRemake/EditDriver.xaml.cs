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
    /// Interaction logic for EditDriver.xaml
    /// </summary>
    public partial class EditDriver : Window
    {
        public int RowId { get; private set; }
        public string FullName { get; private set; }
        public string ContactNo { get; private set; }
        public string Address { get; private set; }
        public byte[] Image { get; private set; }


        public EditDriver(int rowId, string fullName, string contactNo, string address, byte[] image)
        {
            InitializeComponent();
            RowId = rowId;
            TxtName.Text = fullName;
            TxtContactNo.Text = contactNo;
            TxtAddress.Text = address;
            if (image != null)
            {
                imgDriver.Source = ByteArrayToBitmapImage(image);
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
                imgDriver.Source = new BitmapImage(new Uri(openFileDialog.FileName));
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
            FullName = TxtName.Text;
            ContactNo = TxtContactNo.Text;
            Address = TxtAddress.Text;
            if (imgDriver.Source is BitmapImage bitmapImage)
            {
                Image = BitmapImageToByteArray(bitmapImage);
            }
            DialogResult = true;
        }
    }
}
