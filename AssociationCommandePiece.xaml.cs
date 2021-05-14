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
    /// Logique d'interaction pour AssociationCommandePiece.xaml
    /// </summary>
    public partial class AssociationCommandePiece : Window
    {
        MySqlConnection Connection;
        int IdCommande;
        public AssociationCommandePiece(int idCommande, MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
            IdCommande = idCommande;
            LoadPieces();
        }

        public void LoadPieces()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT p.*, t.nom FROM piece p JOIN type t ON t.id = p.typeId", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                AssociationCommandePieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void AssocierCommandePiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = AssociationCommandePieceDataGrid.SelectedItem as DataRowView;
                int idPiece = Convert.ToInt32(data[0].ToString());
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                string request = $"INSERT INTO commandePiece (idCommande, idPiece, quantite) VALUES ({IdCommande}, {idPiece}, {quantite});";
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
            }
            LoadPieces();
        }

        private void TerminerCommandePiece_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DesassocierCommandeVelo dcv = new DesassocierCommandeVelo(IdCommande, Connection);
            dcv.ShowDialog();
        }
    }
}
