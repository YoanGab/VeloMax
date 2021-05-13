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
    /// Logique d'interaction pour UpdateVelo.xaml
    /// </summary>
    public partial class UpdateVelo : Window
    {
        int Id;
        MySqlConnection Connection;
        public UpdateVelo(Velo velo, MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
            Id = velo.Id;
            nomTextBox.Text = velo.Nom;
            prixTextBox.Text = velo.PrixUnitaire.ToString();
            dateIntroductionTextBox.Text = velo.DateIntroduction.ToString("yyyy-MM-dd");
            if (velo.DateDiscontinuation != new DateTime())
            {
                dateFinDeVenteTextBox.Text = velo.DateDiscontinuation.ToString("yyyy-MM-dd");
            }
            grandeurIdTextBox.Text = velo.GrandeurId.ToString();
            ligneProduitIdTextBox.Text = velo.LigneProduitId.ToString();
            quantiteTextBox.Text = velo.Quantite.ToString();
        }

        private void UpdateVeloBtn_Click(object sender, RoutedEventArgs e)
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
                MySqlCommand cmd = new MySqlCommand($"UPDATE velo SET nom='{nom}', prixUnitaire={prixUnitaire}, dateIntroduction={dateIntroduction}, " +
                    $"dateDiscontinuation={dateDiscontinuation}, grandeurId={grandeurId}, ligneProduitId={ligneProduitId}, quantite={quantite} WHERE id={Id}", Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data updated !");
                Connection.Close();
                this.Close();
                AssociateVeloPiece avp = new AssociateVeloPiece(Id, Connection);
                avp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
                Connection.Close();
            }
        }
    }
}
