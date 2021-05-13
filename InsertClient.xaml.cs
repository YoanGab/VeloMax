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
    /// Logique d'interaction pour InsertClient.xaml
    /// </summary>
    public partial class InsertClient : Window
    {
        MySqlConnection Connection;
        public InsertClient(MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
        }

        private void insertClientBtn_Click(object sender, RoutedEventArgs e)
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
                string insertRequest = $"INSERT INTO client (estCompagnie, siret, nom, prenom, mail, telephone, rue, ville, codePostal, province, estAbonne) " +
                    $"VALUES ({estEntreprise}, '{siret}', '{nom}', '{prenom}', '{mail}', '{telephone}', '{rue}', '{ville}', '{codePostal}', '{province}', {estAbonne});";
                MySqlCommand cmd = new MySqlCommand(insertRequest, Connection);
                cmd.ExecuteNonQuery();
                int last_id = Convert.ToInt32(cmd.LastInsertedId);
                MessageBox.Show("Data Inserted !");
                Connection.Close();
                if (estAbonne)
                {
                    ClientAbonnement clientAbonnement = new ClientAbonnement(last_id, Connection);
                    clientAbonnement.ShowDialog();
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
