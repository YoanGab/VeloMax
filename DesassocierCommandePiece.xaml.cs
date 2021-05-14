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
    /// Logique d'interaction pour DesassocierCommandePiece.xaml
    /// </summary>
    public partial class DesassocierCommandePiece : Window
    {
        int IdCommande;
        MySqlConnection Connection;
        public DesassocierCommandePiece(int idCommande, MySqlConnection connection)
        {
            InitializeComponent();
            IdCommande = idCommande;
            Connection = connection;
            LoadPieces();
        }

        public void LoadPieces()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT p.id, reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, t.nom, p.quantite AS stock, cp.quantite AS quantitePanier FROM piece p " +
                    $"JOIN type t ON p.typeId = t.id " +
                    $"JOIN commandePiece cp ON cp.idPiece = p.id WHERE cp.idCommande = {IdCommande};", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DesassociationCommandePieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void DesassocierCommandePiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = DesassociationCommandePieceDataGrid.SelectedItem as DataRowView;
                int idPiece = Convert.ToInt32(data[0].ToString());
                string request = $"DELETE FROM commandePiece WHERE idCommande = {IdCommande} AND idPiece= {idPiece};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            LoadPieces();
        }

        private void TerminerCommandePiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                float prixCommande = 0;
                string request = $"SELECT SUM(cv.quantite*v.prixUnitaire + cp.quantite*p.prixUnitaire) FROM commande c " +
                    $"JOIN commandePiece cp ON c.id = cp.idCommande " +
                    $"JOIN commandeVelo cv ON c.id = cv.idCommande " +
                    $"JOIN piece p ON cp.idPiece = p.id " +
                    $"JOIN velo v ON cv.idVelo = v.id " +
                    $"WHERE c.id = {IdCommande}; ";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    prixCommande = reader.GetFloat(0);
                }
                cmd.Dispose();
                reader.Close();
                MessageBox.Show($"La commande a un total de {prixCommande}€");
                request = $"UPDATE commande SET prix = {prixCommande} WHERE id = {IdCommande};";
                cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                Connection.Close();
            }
            this.Close();
        }
    }
}
