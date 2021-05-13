using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour InsertVelo.xaml
    /// </summary>
    public partial class InsertVelo : Window
    {
        MySqlConnection Connection;
        public InsertVelo(MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
        }

        private void insertVeloBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                string nom = nomTextBox.Text;
                float prixUnitaire = float.Parse(prixTextBox.Text);
                string dateIntroduction = dateIntroductionTextBox.Text != "" ? $"'{dateIntroductionTextBox.Text}'" : $"'{DateTime.Now.ToString("yyyy-MM-dd")}'";
                string dateDiscontinuation = dateFinDeVenteTextBox.Text != "" ? $"'{dateFinDeVenteTextBox.Text}'" : "null";
                int grandeurId = Convert.ToInt32(grandeurIdTextBox.Text);
                int ligneProduitId = Convert.ToInt32(ligneProduitIdTextBox.Text);
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                string insertRequest = $"INSERT INTO velo (nom, prixUnitaire, dateIntroduction, dateDiscontinuation, grandeurId, ligneProduitId, quantite) " +
                    $"VALUES ('{nom}', {prixUnitaire}, {dateIntroduction}, {dateDiscontinuation}, {grandeurId}, {ligneProduitId}, {quantite});";
                MySqlCommand cmd = new MySqlCommand(insertRequest, Connection);
                cmd.ExecuteNonQuery();
                int last_id = Convert.ToInt32(cmd.LastInsertedId);
                MessageBox.Show("Data Inserted !");
                Connection.Close();
                AssociateVeloPiece avp = new AssociateVeloPiece(last_id, Connection);
                avp.ShowDialog();
                this.Close();
            }
            catch
            {
                Connection.Close();
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
            }
        }
    }
}
