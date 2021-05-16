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
    /// Logique d'interaction pour DetailsCommande.xaml
    /// </summary>
    public partial class DetailsCommande : Window
    {
        MySqlConnection Connection;
        int Id;
        public DetailsCommande(int id, MySqlConnection connection)
        {
            InitializeComponent();
            Id = id;
            Connection = connection;
            LoadDetails();
        }

        public void LoadDetails()
        {
            Connection.Open();
            try
            {
                string request = $"SELECT cp.quantite, p.*, cv.quantite, v.* FROM commande c  " +
                    $"JOIN commandePiece cp ON c.id = cp.idCommande " +
                    $"JOIN piece p ON cp.idPiece = p.id " +
                    $"JOIN commandeVelo cv ON c.id = cv.idCommande " +
                    $"JOIN velo v ON cv.idVelo = v.id " +
                    $"WHERE c.id = {Id};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DetailsCommandeDataGrid.DataContext= dt.DefaultView;
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
