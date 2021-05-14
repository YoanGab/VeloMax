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
    /// Logique d'interaction pour InsertFournisseur.xaml
    /// </summary>
    public partial class InsertFournisseur : Window
    {
        MySqlConnection Connection;
        public InsertFournisseur(MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
        }

        private void InsertFournisseurBtn_Click(object sender, RoutedEventArgs e)
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
                
                string insertRequest = $"INSERT INTO fournisseur (siret, nom, nomContact, prenomContact, mailContact, rue, ville, codePostal, province, libelle) " +
                    $"VALUES ('{siret}', '{nom}', '{nomContact}', '{prenomContact}', '{mailContact}', '{rue}', '{ville}', '{codePostal}', '{province}', {libelle});";
                MySqlCommand cmd = new MySqlCommand(insertRequest, Connection);
                cmd.ExecuteNonQuery();
                int last_id = Convert.ToInt32(cmd.LastInsertedId);
                MessageBox.Show("Data Inserted !");
                Connection.Close();
                AssociateFournisseurPiece afp = new AssociateFournisseurPiece(last_id, Connection);
                afp.ShowDialog();
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
