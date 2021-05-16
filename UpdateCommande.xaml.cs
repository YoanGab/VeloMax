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
    /// Logique d'interaction pour UpdateCommande.xaml
    /// </summary>
    public partial class UpdateCommande : Window
    {
        MySqlConnection Connection;
        int IdCommande;
        public UpdateCommande(Commande commande, MySqlConnection connection)
        {
            InitializeComponent();
            IdCommande = commande.Id;
            Connection = connection;
            dateCommandeTextBox.Text = commande.DateCreation.ToString("yyyy-MM-dd");
            if (commande.DateExpedition != new DateTime())
            {
                dateExpeditionTextBox.Text = commande.DateExpedition.ToString("yyyy-MM-dd");
            }
            if (commande.DateValidation != new DateTime())
            {
                dateValidationTextBox.Text = commande.DateValidation.ToString("yyyy-MM-dd");
            }
            rueLivraisonTextBox.Text = commande.RueLivraison;
            villeLivraisonTextBox.Text = commande.VilleLivraison;
            codePostalLivraisonTextBox.Text = commande.CodePostalLivraison;
            provinceLivraisonTextBox.Text = commande.ProvinceLivraison;
            statutTextBox.Text = commande.Statut;

        }

        private void UpdateCommandeBtn_Click(object sender, RoutedEventArgs e)
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

                string request = $"UPDATE commande SET dateCreation = {dateCommande}, dateValidation={dateValidation}, dateExpedition={dateExpedition}, " +
                    $"rueLivraison='{rue}', villeLivraison='{ville}', codePostalLivraison='{codePostal}', provinceLivraison='{province}', statut='{statut}' " +
                    $"WHERE id = {IdCommande};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
                Connection.Close();
                AssociationCommandeVelo acv = new AssociationCommandeVelo(IdCommande, Connection);
                acv.ShowDialog();
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
                Connection.Close();
            }
        }
    }
}
