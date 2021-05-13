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
    /// Logique d'interaction pour DesassocierVeloPiece.xaml
    /// </summary>
    public partial class DesassocierVeloPiece : Window
    {
        int IdVelo;
        MySqlConnection Connection;
        public DesassocierVeloPiece(int idVelo, MySqlConnection connection)
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
                MySqlCommand cmd = new MySqlCommand($"SELECT p.id, reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, t.nom, p.quantite AS stock, vp.quantite AS quantiteRequired FROM piece p " +
                    $"JOIN type t ON p.typeId = t.id " +
                    $"JOIN veloPiece vp ON vp.idPiece = p.id WHERE vp.idVelo = {IdVelo};", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DesassociationVeloPieceDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void DesassocierVeloPiece_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                DataRowView data = DesassociationVeloPieceDataGrid.SelectedItem as DataRowView;
                int idPiece = Convert.ToInt32(data[0].ToString());
                string request = $"DELETE FROM veloPiece WHERE idVelo = {IdVelo} AND idPiece= {idPiece};";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            LoadPieces();
        }

        private void TerminerVeloPiece_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
