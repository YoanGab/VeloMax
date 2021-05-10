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

namespace VeloMax
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string connectionString = $"SERVER=localhost;PORT=3306;DATABASE=Velomax;UID={username};PASSWORD={password};";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();                
            }
            catch
            {
                MessageBox.Show("Username or password is incorrect.");
                txtUsername.Text = "";
                txtPassword.Password = "";
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
