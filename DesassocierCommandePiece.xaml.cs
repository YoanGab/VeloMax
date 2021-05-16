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
                string request = $"SELECT SUM(cv.quantite*v.prixUnitaire + cp.quantite*p.prixUnitaire) FROM commande c " +
                    $"JOIN commandePiece cp ON c.id = cp.idCommande " +
                    $"JOIN commandeVelo cv ON c.id = cv.idCommande " +
                    $"JOIN piece p ON cp.idPiece = p.id " +
                    $"JOIN velo v ON cv.idVelo = v.id " +
                    $"WHERE c.id = {IdCommande}; ";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                float prixCommande = float.Parse(cmd.ExecuteScalar().ToString());
                MessageBox.Show($"La commande a un total de {prixCommande}€");
                request = $"UPDATE commande SET prix = {prixCommande} WHERE id = {IdCommande};";
                cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();

                request = $"SELECT p.reference, p.quantite  FROM commandePiece cp " +
                    $"JOIN piece p ON cp.idPiece = p.id " +
                    $"WHERE idCommande = {IdCommande};";
                cmd = new MySqlCommand(request, Connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                string reference;
                int quantite;
                
                while (reader.Read())
                {
                    reference = reader.GetString(0);
                    quantite = reader.GetInt32(1);  

                    if (quantite <= 2)
                    {
                        MessageBox.Show($"ATTENTION !\nLa pièce {reference} a un stock de {quantite}");
                    }
                }
                reader.Dispose();

                /*
                 * Délai Livraison
                 */
                request = $"SELECT idPiece, cp.quantite - p.quantite AS ToBuy FROM commandePiece cp " +
                    $"JOIN piece p ON cp.idPiece = p.id " +
                    $"WHERE idCommande = {IdCommande}; ";
                cmd = new MySqlCommand(request, Connection);
                reader = cmd.ExecuteReader();

                List<int> idPieces = new List<int>();
                List<int> quantitiesToBuy = new List<int>();
                int quantity;

                while (reader.Read())
                {
                    quantity = reader.GetInt32(1);
                    if (quantity > 0 )
                    {
                        idPieces.Add(reader.GetInt32(0));
                        quantitiesToBuy.Add(quantity);
                    }
                }
                reader.Dispose();

                int delai = -1;
                int temp = -1;
                for (int i = 0; i < idPieces.Count(); i++)
                {
                    request =$"SELECT MIN(delai) FROM fournisseurPiece WHERE (idPiece = {idPieces[i]} AND quantite >= {quantitiesToBuy[i]});";
                    cmd = new MySqlCommand(request, Connection);
                    try
                    {
                        temp = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch
                    {
                        MessageBox.Show($"Impossible de définir un délai pour votre commande");
                        request = $"UPDATE commande SET delai = {-1} WHERE id = {IdCommande};";
                        cmd = new MySqlCommand(request, Connection);
                        cmd.ExecuteNonQuery();
                        return;
                    }

                    if (delai < 0 | delai < temp)
                    {
                        delai = temp;
                    }
                }
                MessageBox.Show($"Le délai de votre livraison est de {delai} jours");
                request = $"UPDATE commande SET delai = {delai} WHERE id = {IdCommande};";
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
