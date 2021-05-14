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
    /// Logique d'interaction pour DesassocierFournisseurPiece.xaml
    /// </summary>
    public partial class DesassocierFournisseurPiece : Window
    {
        int IdFournisseur;
        MySqlConnection Connection;
        public DesassocierFournisseurPiece(int idFournisseur, MySqlConnection connection)
        {
            InitializeComponent();
            IdFournisseur = idFournisseur;
            Connection = connection;
            LoadPieces();
        }

        public void LoadPieces()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT p.id, reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, t.nom, p.quantite AS stock, fp.quantite AS stockFournisseur, delai AS delaiFournisseur FROM piece p " +
                    $"JOIN type t ON p.typeId = t.id " +
                    $"JOIN fournisseurPiece fp ON fp.idPiece = p.id WHERE fp.idFournisseur = {IdFournisseur};", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DesassociationFournisseurPieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void DesassocierFournisseurPiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = DesassociationFournisseurPieceDataGrid.SelectedItem as DataRowView;
                int idPiece = Convert.ToInt32(data[0].ToString());
                string request = $"DELETE FROM fournisseurPiece WHERE idFournisseur = {IdFournisseur} AND idPiece= {idPiece};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            LoadPieces();
        }

        private void TerminerFournisseurPiece_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
