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
    /// Logique d'interaction pour AssociateFournisseurPiece.xaml
    /// </summary>
    public partial class AssociateFournisseurPiece : Window
    {
        int IdFournisseur;
        MySqlConnection Connection;
        public AssociateFournisseurPiece(int idFournisseur, MySqlConnection connection)
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
                MySqlCommand cmd = new MySqlCommand($"SELECT idPiece, reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, p.quantite AS stock, fp.quantite AS stockFournisseur, delai AS delaiFournisseur, t.nom AS type FROM fournisseurPiece fp  " +
                    $"JOIN piece p ON p.id = fp.idPiece " +
                    $"JOIN type t ON p.typeId = t.id " +
                    $"GROUP BY idPiece HAVING SUM(fp.idFournisseur = {IdFournisseur}) = 0; ", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                AssociationFournisseurPieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void AssocierFournisseurPiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = AssociationFournisseurPieceDataGrid.SelectedItem as DataRowView;
                int idPiece = Convert.ToInt32(data[0].ToString());
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                int delai= Convert.ToInt32(delaiTextBox.Text);
                string noProduitFournisseur = noProduitFournisseurTextBox.Text;
                string request = $"INSERT INTO fournisseurPiece (idFournisseur, idPiece, quantite, delai, noProduitFournisseur) VALUES ({IdFournisseur}, {idPiece}, {quantite}, {delai}, {noProduitFournisseur});";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                //MessageBox.Show("Champs non valides");
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                Connection.Close();
                quantiteTextBox.Text = "1";
                delaiTextBox.Text = "0";
            }
            LoadPieces();
        }

        private void TerminerFournisseurPiece_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DesassocierFournisseurPiece dfp = new DesassocierFournisseurPiece(IdFournisseur, Connection);
            dfp.ShowDialog();
        }
    }
}
