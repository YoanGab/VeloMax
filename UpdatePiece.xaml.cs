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
    /// Logique d'interaction pour UpdatePiece.xaml
    /// </summary>
    public partial class UpdatePiece : Window
    {
        int Id;
        MySqlConnection Connection;
        public UpdatePiece(Piece piece, MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
            Id = piece.Id;
            referenceTextBox.Text = piece.Reference;
            descriptionTextBox.Text = piece.Description;
            prixTextBox.Text = piece.PrixUnitaire.ToString();
            dateIntroductionTextBox.Text = piece.DateIntroduction.ToString("yyyy-MM-dd");
            if (piece.DateDiscontinuation != new DateTime())
            {
                dateFinDeVenteTextBox.Text = piece.DateDiscontinuation.ToString("yyyy-MM-dd");
            }
            typeIdTextBox.Text = piece.TypeId.ToString();
            quantiteTextBox.Text = piece.Quantite.ToString();
        }

        private void UpdatePieceBtn_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try {
                string reference = referenceTextBox.Text;
                string description = descriptionTextBox.Text;
                float prixUnitaire = float.Parse(prixTextBox.Text);
                string dateIntroduction = dateIntroductionTextBox.Text != "" ? $"'{dateIntroductionTextBox.Text}'" : $"'{DateTime.Now.ToString("yyyy-MM-dd")}'";
                string dateDiscontinuation = dateFinDeVenteTextBox.Text != "" ? $"'{dateFinDeVenteTextBox.Text}'" : "null";
                int typeId = Convert.ToInt32(typeIdTextBox.Text);
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                MySqlCommand cmd = new MySqlCommand($"UPDATE piece SET reference='{reference}', description='{description}', prixUnitaire={prixUnitaire}, dateIntroduction={dateIntroduction}, " +
                    $"dateDiscontinuation={dateDiscontinuation}, typeId={typeId}, quantite={quantite} WHERE id={Id}", Connection);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data updated !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Data not updated !");
                }
            }
            catch
            {
                MessageBox.Show("Les Champs renseignes ne sont pas valides");
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
