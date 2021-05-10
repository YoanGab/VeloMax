using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace VeloMax
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Velomax;UID=root;PASSWORD=root;";
            connection = new MySqlConnection(connectionString);
            LoadPieces(connection);
        }

        public void LoadPieces(MySqlConnection connection)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM piece;", connection);
            ObservableCollection<Piece> pieces = new ObservableCollection<Piece>();
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = -1;
                if (!reader.IsDBNull(0))
                {
                    id = reader.GetInt32(0);
                }

                string reference = "";
                if (!reader.IsDBNull(1))
                {
                    reference = reader.GetString(1);
                }

                string description = "";
                if (!reader.IsDBNull(2))
                {
                    description = reader.GetString(2);
                }

                float prixUnitaire = 0;
                if (!reader.IsDBNull(3))
                {
                    prixUnitaire = reader.GetFloat(3);
                }

                DateTime dateIntroduction = new DateTime();
                if (!reader.IsDBNull(4))
                {
                    dateIntroduction = reader.GetDateTime(4);
                }

                DateTime dateDiscontinuation = new DateTime();
                if (!reader.IsDBNull(5))
                {
                    dateDiscontinuation = reader.GetDateTime(4);
                }

                int typeId = -1;
                if (!reader.IsDBNull(6))
                {
                    typeId = reader.GetInt32(6);
                }

                int quantite = 0;
                if (!reader.IsDBNull(7))
                {
                    quantite = reader.GetInt32(7);
                }

                pieces.Add(new Piece() { Id = id, Reference = reference, Description = description, PrixUnitaire = prixUnitaire, DateIntroduction = dateIntroduction, DateDiscontinuation = dateDiscontinuation, TypeId = typeId, Quantite = quantite });
            }
            connection.Close();
            PiecesDataGrid.ItemsSource = pieces;
        }

        private void InsertPiece_Click(object sender, RoutedEventArgs e)
        {
            InsertPiece insertPiece = new InsertPiece();
            insertPiece.ShowDialog();
        }

        private void UpdatePiece_Click(object sender, RoutedEventArgs e)
        {
            Piece piece = PiecesDataGrid.SelectedItem as Piece;
            UpdatePiece updatePage = new UpdatePiece(piece);
            updatePage.ShowDialog();
            LoadPieces(connection);
        }

        private void DeletePiece_Click(object sender, RoutedEventArgs e)
        {
            int id = (PiecesDataGrid.SelectedItem as Piece).Id;
            connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM piece WHERE id={id}", connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Data not deleted !");
                }
            }
            catch
            {
                MessageBox.Show("Data not deleted !");
            }
            finally
            {
                connection.Close();
            }
            LoadPieces(connection);
        }
    }
}
