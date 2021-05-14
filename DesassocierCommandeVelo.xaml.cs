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
    /// Logique d'interaction pour DesassocierCommandeVelo.xaml
    /// </summary>
    public partial class DesassocierCommandeVelo : Window
    {
        int IdCommande;
        MySqlConnection Connection;
        public DesassocierCommandeVelo(int idCommande, MySqlConnection connection)
        {
            InitializeComponent();
            IdCommande = idCommande;
            Connection = connection;
            LoadVelos();
        }

        public void LoadVelos()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT v.*, cv.quantite AS quantitePanier FROM velo v JOIN commandeVelo cv ON cv.idCommande = v.id WHERE idCommande = {IdCommande};", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DesassociationCommandeVeloDataGrid.DataContext = dt.DefaultView;
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }
        }

        private void DesassocierCommandeVelo_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = DesassociationCommandeVeloDataGrid.SelectedItem as DataRowView;
                int idVelo= Convert.ToInt32(data[0].ToString());
                string request = $"DELETE FROM commandeVelo WHERE idCommande = {IdCommande} AND idVelo= {idVelo};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }
            LoadVelos();
        }

        private void TerminerCommandeVelo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DesassocierCommandePiece dcp = new DesassocierCommandePiece(IdCommande, Connection);
            dcp.ShowDialog();
        }
    }
}
