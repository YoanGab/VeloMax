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
    /// Logique d'interaction pour AssociationCommandeVelo.xaml
    /// </summary>
    public partial class AssociationCommandeVelo : Window
    {
        int IdCommande;
        MySqlConnection Connection;
        public AssociationCommandeVelo(int idCommande, MySqlConnection connection)
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
                MySqlCommand cmd = new MySqlCommand($"SELECT v.*, g.nom, lp.nom FROM velo v JOIN grandeur g ON g.id = v.grandeurId JOIN ligneProduit lp ON lp.id = v.ligneProduitId", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                AssociationCommandeVeloDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void AssocierCommandeVelo_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = AssociationCommandeVeloDataGrid.SelectedItem as DataRowView;
                if (data == null)
                {
                    MessageBox.Show("Sélectionnez une ligne");
                    return;
                }
                int idVelo = Convert.ToInt32(data[0].ToString());
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                string request = $"INSERT INTO commandeVelo (idCommande, idVelo, quantite) VALUES ({IdCommande}, {idVelo}, {quantite});";
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
            LoadVelos();
        }

        private void TerminerCommandeVelo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AssociationCommandePiece acp = new AssociationCommandePiece(IdCommande, Connection);
            acp.ShowDialog();
        }
    }
}
