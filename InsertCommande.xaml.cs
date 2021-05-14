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
    /// Logique d'interaction pour InsertCommande.xaml
    /// </summary>
    public partial class InsertCommande : Window
    {
        int IdClient;
        MySqlConnection Connection;
        public InsertCommande(int idClient, MySqlConnection connection)
        {
            InitializeComponent();
            IdClient = idClient;
            Connection = connection;
        }

        private void InsertCommandeBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                string dateCommande = dateCommandeTextBox.Text != "" ? $"'{dateCommandeTextBox.Text}'" : $"'{DateTime.Now.ToString("yyyy-MM-dd")}'";
                string dateValidation = dateValidationTextBox.Text != "" ? $"'{dateValidationTextBox.Text}'" : "null";
                string dateExpedition = dateExpeditionTextBox.Text != "" ? $"'{dateExpeditionTextBox.Text}'" : "null";
                string rue = rueLivraisonTextBox.Text;
                string ville = villeLivraisonTextBox.Text;
                string codePostal = codePostalLivraisonTextBox.Text;
                string province = provinceLivraisonTextBox.Text;
                string statut = statutTextBox.Text;


                string insertRequest = $"INSERT INTO commande (dateCreation, dateValidation, dateExpedition, rueLivraison, villeLivraison, codePostalLivraison, provinceLivraison, statut, idClient, prix) " +
                    $"VALUES ({dateCommande}, {dateValidation}, {dateExpedition}, '{rue}', '{ville}', '{codePostal}', '{province}', '{statut}', {IdClient}, 0);";
                MySqlCommand cmd = new MySqlCommand(insertRequest, Connection);
                cmd.ExecuteNonQuery();
                int last_id = Convert.ToInt32(cmd.LastInsertedId);
                MessageBox.Show("Data Inserted !");
                Connection.Close();
                AssociationCommandeVelo acv = new AssociationCommandeVelo(last_id, Connection);
                acv.ShowDialog();
                this.Close();
            }
            catch (Exception exc)
            {
                Connection.Close();
                MessageBox.Show(exc.ToString());
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
            }
        }
    }
}
