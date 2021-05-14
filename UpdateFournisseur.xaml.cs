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
    /// Logique d'interaction pour UpdateFournisseur.xaml
    /// </summary>
    public partial class UpdateFournisseur : Window
    {
        int IdFournisseur;
        MySqlConnection Connection;
        public UpdateFournisseur(Fournisseur fournisseur, MySqlConnection connection)
        {
            InitializeComponent();
            IdFournisseur = fournisseur.Id;
            Connection = connection;
            siretTextBox.Text = fournisseur.Siret;
            nomTextBox.Text = fournisseur.Nom;
            nomContactTextBox.Text = fournisseur.NomContact;
            prenomContactTextBox.Text = fournisseur.PrenomContact;
            mailContactTextBox.Text = fournisseur.MailContact;
            rueTextBox.Text = fournisseur.Rue;
            villeTextBox.Text = fournisseur.Ville;
            codePostalTextBox.Text = fournisseur.CodePostal;
            provinceTextBox.Text = fournisseur.Province;
            libelleTextBox.Text = fournisseur.Libelle.ToString();
        }

        private void UpdateFournisseurBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                string siret = siretTextBox.Text;
                string nom = nomTextBox.Text;
                string nomContact = nomContactTextBox.Text;
                string prenomContact = prenomContactTextBox.Text;
                string mailContact = mailContactTextBox.Text;
                string rue = rueTextBox.Text;
                string ville = villeTextBox.Text;
                string codePostal = codePostalTextBox.Text;
                string province = provinceTextBox.Text;
                string libelle = libelleTextBox.Text;

                MySqlCommand cmd = new MySqlCommand($"UPDATE fournisseur SET siret='{siret}', nom='{nom}', nomContact='{nomContact}', prenomContact='{prenomContact}', " +
                    $"mailContact='{mailContact}', rue='{rue}', ville='{ville}', codePostal='{codePostal}', libelle={libelle} WHERE id={IdFournisseur}", Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data updated !");
                Connection.Close();
                this.Close();
                AssociateFournisseurPiece afp = new AssociateFournisseurPiece(IdFournisseur, Connection);
                afp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
                Connection.Close();
            }
        }
    }
}
