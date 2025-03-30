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
using System.Windows.Shapes;

namespace RuchiToursRemake
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        

        private void TxtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(TxtUsername.Text == ""|| TxtPassword.Text == "")
            {
                MessageBox.Show("Please fill all the blank fields", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DBconnect db = new DBconnect();

                try
                {
                    db.OpenConnection();
                    using (MySqlConnection conn = db.GetConnection())
                    {
                        string checkUserName = "select * from user where username = '" + TxtUsername.Text.Trim() + "'";

                        using (MySqlCommand checkuser = new MySqlCommand(checkUserName, conn))
                        {
                            MySqlDataAdapter adapter = new MySqlDataAdapter(checkuser);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(TxtUsername.Text + "is already exist", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                string insertData = "UPDATE user SET username = @newUsername, password = @newPassword WHERE userid = 1";

                                using (MySqlCommand cmd = new MySqlCommand(insertData, conn))
                                {
                                    cmd.Parameters.AddWithValue("@newUsername", TxtUsername.Text.Trim());
                                    cmd.Parameters.AddWithValue("@newPassword", TxtPassword.Text.Trim());
                                    

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Registered Successfully", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);

                                    MainWindow login = new MainWindow();
                                    login.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting database: " + ex, "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }
    }
}
