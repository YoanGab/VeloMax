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
    /// Logique d'interaction pour ClientAbonnement.xaml
    /// </summary>
    public partial class ClientAbonnement : Window
    {
        int IdClient;
        MySqlConnection Connection;
        public ClientAbonnement(int idClient, MySqlConnection connection)
        {
            InitializeComponent();
            IdClient = idClient;
            Connection = connection;
            LoadAbonnements();
        }

        public void LoadAbonnements()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM programmeFidelio;", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                AbonnementsDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void AssocierClientAbonnement_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = AbonnementsDataGrid.SelectedItem as DataRowView;
                if (data == null)
                {
                    this.Close();
                    Connection.Close();
                    return;
                }
                int idProgramme = Convert.ToInt32(data[0].ToString());
                int duration = Convert.ToInt32(data[3].ToString());
                string request = $"INSERT INTO abonnement (idProgramme, dateDebut, dateFin) VALUES ({idProgramme}, '{DateTime.Now.ToString("yyyy-MM-dd")}', '{DateTime.Now.AddYears(duration).ToString("yyyy-MM-dd")}');";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
                int last_id = Convert.ToInt32(cmd.LastInsertedId);
                request = $"UPDATE client SET idAbonnement = {last_id} WHERE id = {IdClient};";
                cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
                this.Close();
            }
            catch
            {
                MessageBox.Show("An error occured...");
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
