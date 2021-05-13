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
    /// Logique d'interaction pour UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Window
    {
        int IdClient;
        MySqlConnection Connection;
        public UpdateClient(Client client, MySqlConnection connection)
        {
            InitializeComponent();
            IdClient = client.Id;
            Connection = connection;
            entrepriseCheckBox.IsChecked = client.EstEntreprise;
            siretTextBox.Text = client.Siret;
            nomTextBox.Text = client.Nom;
            prenomTextBox.Text = client.Prenom;
            mailTextBox.Text = client.Mail;
            telephoneTextBox.Text = client.Telephone;
            rueTextBox.Text = client.Rue;
            villeTextBox.Text = client.Ville;
            codePostalTextBox.Text = client.CodePostal;
            provinceTextBox.Text = client.Province;
            estAbonneCheckBox.IsChecked = client.EstAbonne;
        }

        private void updateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                bool estEntreprise = entrepriseCheckBox.IsChecked == true;
                string siret = siretTextBox.Text;
                string nom = nomTextBox.Text;
                string prenom = prenomTextBox.Text;
                string mail = mailTextBox.Text;
                string telephone = telephoneTextBox.Text;
                string rue = rueTextBox.Text;
                string ville = villeTextBox.Text;
                string codePostal = codePostalTextBox.Text;
                string province = provinceTextBox.Text;
                bool estAbonne = estAbonneCheckBox.IsChecked == true;
                string request = $"UPDATE client SET estCompagnie={estEntreprise}, siret='{siret}', nom='{nom}', prenom='{prenom}', mail='{mail}', telephone='{telephone}', " +
                    $"rue='{rue}', ville='{ville}', codePostal='{codePostal}', province='{province}', estAbonne={estAbonne} WHERE id={IdClient};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Updated !");
                if (estAbonne)
                {
                    Connection.Close();
                    ClientAbonnement clientAbonnement = new ClientAbonnement(IdClient, Connection);
                    clientAbonnement.ShowDialog();
                }
                else
                {
                    request = $"UPDATE client SET idAbonnement = NULL WHERE id = {IdClient};";
                    cmd = new MySqlCommand(request, Connection);
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
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
