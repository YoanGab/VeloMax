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
    /// Logique d'interaction pour AssociateVeloPiece.xaml
    /// </summary>
    public partial class AssociateVeloPiece : Window
    {
        int IdVelo;
        MySqlConnection Connection;
        public AssociateVeloPiece(int idVelo, MySqlConnection connection)
        {
            InitializeComponent();
            IdVelo = idVelo;
            Connection = connection;
            LoadPieces();
        }

        public void LoadPieces()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT p.id, reference, description, p.prixUnitaire, p.dateIntroduction, p.dateDiscontinuation, t.nom, p.quantite AS stock FROM piece p  " +
                    $"JOIN type t ON p.typeId = t.id " +
                    $"JOIN veloPiece vp ON vp.idPiece = p.id " +
                    $"GROUP BY p.id HAVING SUM(vp.idVelo = {IdVelo}) = 0;", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                AssociationVeloPieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void AssocierVeloPiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = AssociationVeloPieceDataGrid.SelectedItem as DataRowView;
                Int32.TryParse(data[0].ToString(), out int idPiece);
                if (idPiece == 0)
                {
                    throw new Exception();
                }
                int quantite = Convert.ToInt32(quantiteTextBox.Text);
                string request = $"INSERT INTO veloPiece (idVelo, idPiece, quantite) VALUES ({IdVelo}, {idPiece}, {quantite});";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                Connection.Close();
                quantiteTextBox.Text = "1";
            }
            LoadPieces();
        }

        private void TerminerVeloPiece_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            DesassocierVeloPiece dvp = new DesassocierVeloPiece(IdVelo, Connection);
            dvp.ShowDialog();

        }
    }
}
