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
    /// Logique d'interaction pour InsertPiece.xaml
    /// </summary>
    public partial class InsertPiece : Window
    {
        public InsertPiece()
        {
            InitializeComponent();
        }

        private void insertPieceBtn_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection;
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Velomax;UID=root;PASSWORD=root;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                string reference = referenceTextBox.Text;
                string description = descriptionTextBox.Text;
                float prixUnitaire = float.Parse(prixTextBox.Text);
                string dateIntroduction= dateIntroductionTextBox.Text != "" ? $"'{dateIntroductionTextBox.Text}'" : $"'{DateTime.Now.ToString("yyyy-MM-dd")}'";
                string dateDiscontinuation = dateFinDeVenteTextBox.Text != "" ? $"'{dateFinDeVenteTextBox.Text}'" : "null";
                int typeId = Convert.ToInt32(typeIdTextBox.Text);
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO piece (reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, typeId, quantite) " +
                    $"VALUES ('{reference}', '{description}', {prixUnitaire}, {dateIntroduction}, {dateDiscontinuation}, {typeId}, {quantite});", connection);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data not Inserted !");
                }                
            }
            catch
            {
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
