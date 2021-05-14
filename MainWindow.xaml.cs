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
using System.Globalization;
using Newtonsoft.Json;

namespace VeloMax
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection Connection;
        public MainWindow(MySqlConnection connection)
        {
            InitializeComponent();
            Connection = connection;
            LoadData();
        }

        public void LoadData()
        {
            LoadPieces();
            LoadVelos();
            LoadClients();
            LoadFournisseurs();
            LoadCommandes();
        }

        public void LoadPieces()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT p.id, reference, description, prixUnitaire, dateIntroduction, dateDiscontinuation, typeId, nom AS type, quantite, nbVentes FROM piece p " +
                    "JOIN type t ON t.id = p.typeId;", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                PiecesDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
            Connection.Close();
            }
        }

        private void InsertPiece_Click(object sender, RoutedEventArgs e)
        {
            InsertPiece insertPiece = new InsertPiece(Connection);
            insertPiece.ShowDialog();
            LoadPieces();
        }

        private void UpdatePiece_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = PiecesDataGrid.SelectedItem as DataRowView;
            DateTime dateIntroduction = DateTime.ParseExact(data[4].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            DateTime dateDiscontinuation = new DateTime();
            if (data[5].ToString() != "")
            {
                dateDiscontinuation = DateTime.ParseExact(data[5].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            }
            Piece piece = new Piece(Convert.ToInt32(data[0].ToString()), data[1].ToString(), data[2].ToString(), float.Parse(data[3].ToString()), dateIntroduction, dateDiscontinuation, Convert.ToInt32(data[6].ToString()), Convert.ToInt32(data[8].ToString()));
            UpdatePiece updatePage = new UpdatePiece(piece, Connection);
            updatePage.ShowDialog();
            LoadPieces();
        }

        private void DeletePiece_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = PiecesDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM piece WHERE id={id}", Connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Data not deleted !");
                }
            }
            catch
            {
                MessageBox.Show("Cette pièce ne peut pas être supprimée !");
            }
            finally
            {
                Connection.Close();
            }
            LoadPieces();
        }

        public void LoadVelos()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT v.id, v.nom, prixUnitaire, dateIntroduction, dateDiscontinuation, " +
                    "grandeurId, g.nom AS grandeur, ligneProduitId, lp.nom AS ligneProduit, quantite, nbVentes FROM velo v " +
                    "JOIN grandeur g ON g.id = v.grandeurId JOIN ligneProduit lp ON lp.id = v.ligneProduitId;", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                VelosDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void InsertVelo_Click(object sender, RoutedEventArgs e)
        {
            InsertVelo insertVelo= new InsertVelo(Connection);
            insertVelo.ShowDialog();
            LoadVelos();
        }

        private void UpdateVelo_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = VelosDataGrid.SelectedItem as DataRowView;
            DateTime dateIntroduction = DateTime.ParseExact(data[3].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            DateTime dateDiscontinuation = new DateTime();
            if (data[4].ToString() != "")
            {
                dateDiscontinuation = DateTime.ParseExact(data[4].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            }
            Velo velo = new Velo(Convert.ToInt32(data[0].ToString()), data[1].ToString(), float.Parse(data[2].ToString()), dateIntroduction, dateDiscontinuation, Convert.ToInt32(data[5].ToString()), Convert.ToInt32(data[7].ToString()), Convert.ToInt32(data[9].ToString()));
            UpdateVelo updateVelo = new UpdateVelo(velo, Connection);
            updateVelo.ShowDialog();
            LoadVelos();
        }

        private void DeleteVelo_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = VelosDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM veloPiece WHERE idVelo={id}", Connection);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"DELETE FROM velo WHERE id={id}", Connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Data not deleted !");
                }
            }
            catch
            {
                MessageBox.Show("Cette pièce ne peut pas être supprimée !");
            }
            finally
            {
                Connection.Close();
            }
            LoadVelos();
        }

        public void LoadClients()
        {
            Connection.Open();
            string request = "SELECT c.*, a.*, SUM(prix) AS totalPrix, SUM(cv.quantite) AS nbVelosAchetes, SUM(cp.quantite) AS nbPiecesAchetes, SUM(cv.quantite) + SUM(cp.quantite) AS nbProduitsAchetes FROM client c " +
                "LEFT JOIN abonnement a ON a.Id = c.idAbonnement " +
                "LEFT JOIN commande co ON co.idClient = c.id " +
                "LEFT JOIN commandeVelo cv ON cv.idCommande = co.id " +
                "LEFT JOIN commandePiece cp ON cp.idCommande = co.id " +
                "GROUP BY c.id;";
            MySqlCommand cmd = new MySqlCommand(request, Connection);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            ClientsDataGrid.DataContext = dt.DefaultView;
            Connection.Close();
        }

        private void InsertClient_Click(object sender, RoutedEventArgs e)
        {
            InsertClient insertClient = new InsertClient(Connection);
            insertClient.ShowDialog();
            LoadClients();
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = ClientsDataGrid.SelectedItem as DataRowView;
            int id = Convert.ToInt32(data[0].ToString());
            bool estEntreprise = data[1].ToString() == "True";
            string siret = data[2].ToString();
            string nom = data[3].ToString();
            string prenom = data[4].ToString();
            string mail = data[5].ToString();
            string telephone = data[6].ToString();
            string rue = data[7].ToString();
            string ville = data[8].ToString();
            string codePostal = data[9].ToString();
            string province = data[10].ToString();
            bool estAbonne = data[11].ToString() == "True";
            int idAbonnement;
            Int32.TryParse(data[12].ToString(), out idAbonnement);
            


            Client client = new Client(id, estEntreprise, siret, nom, prenom, mail, telephone, rue, ville, codePostal, province, estAbonne, idAbonnement);
            UpdateClient updateClient = new UpdateClient(client, Connection);
            updateClient.ShowDialog();
            LoadClients();
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = ClientsDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM client WHERE id={id}", Connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Data not deleted !");
                }
            }
            catch
            {
                MessageBox.Show("Cette pièce ne peut pas être supprimée !");
            }
            finally
            {
                Connection.Close();
            }
            LoadVelos();
        }

        public void LoadFournisseurs()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM fournisseur", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                FournisseursDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void InsertFournisseur_Click(object sender, RoutedEventArgs e)
        {
            InsertFournisseur insertFournisseur = new InsertFournisseur(Connection);
            insertFournisseur.ShowDialog();
            LoadFournisseurs();
        }

        private void UpdateFournisseur_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = FournisseursDataGrid.SelectedItem as DataRowView;
            int id = Convert.ToInt32(data[0].ToString());
            string siret = data[1].ToString();
            string nom = data[2].ToString();
            string nomContact = data[3].ToString();
            string prenomContact = data[4].ToString();
            string mailContact = data[5].ToString();
            string rue = data[6].ToString();
            string ville = data[7].ToString();
            string codePostal = data[8].ToString();
            string province = data[9].ToString();
            Int32.TryParse(data[10].ToString(), out int libelle_int);
            Fournisseur fournisseur = new Fournisseur(id, siret, nom, nomContact, prenomContact, mailContact, rue, ville, codePostal, province, libelle_int);
            UpdateFournisseur updateFournisseur = new UpdateFournisseur(fournisseur, Connection);
            updateFournisseur.ShowDialog();
            LoadFournisseurs();
        }

        private void DeleteFournisseur_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = FournisseursDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM fournisseur WHERE id={id}", Connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Data not deleted !");
                }
            }
            catch
            {
                MessageBox.Show("Cette pièce ne peut pas être supprimée !");
            }
            finally
            {
                Connection.Close();
            }
            LoadFournisseurs();
        }

        public void LoadCommandes()
        {
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT c.*, cl.nom AS nomClient, cl.prenom AS prenomClient FROM commande c " +
                    "JOIN client cl ON cl.id = c.idClient;", Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                CommandesDataGrid.DataContext = dt.DefaultView;
            }
            finally
            {
                Connection.Close();
            }
        }

        private void InsertCommande_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = ClientsDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                MessageBox.Show("Sélectionnez un client");
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            InsertCommande insertCommande= new InsertCommande(id, Connection);
            insertCommande.ShowDialog();
            LoadCommandes();
        }

        private void UpdateCommande_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = FournisseursDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            DateTime dateCreation = DateTime.ParseExact(data[1].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            DateTime dateValidation = DateTime.ParseExact(data[2].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            DateTime dateExpedition = DateTime.ParseExact(data[3].ToString(), "dd/MM/yyyy HH:mm:ss", null);
            string rueLivraison = data[4].ToString();
            string villeLivraison = data[5].ToString();
            string codePostalLivraison = data[6].ToString();
            string provinceLivraison = data[7].ToString();
            int idClient = Convert.ToInt32(data[8].ToString());
            float prix = float.Parse(data[9].ToString());
            string statut = data[10].ToString();
            int delai = Convert.ToInt32(data[11].ToString());

            Commande commande = new Commande(id, dateCreation, dateValidation, dateExpedition, rueLivraison, villeLivraison, codePostalLivraison, provinceLivraison, idClient, prix, statut, delai);
            UpdateCommande updateCommande= new UpdateCommande(commande, Connection);
            updateCommande.ShowDialog();
            LoadCommandes();
        }

        private void DeleteCommande_Click(object sender, RoutedEventArgs e)
        {
            DataRowView data = CommandesDataGrid.SelectedItem as DataRowView;
            if (data == null)
            {
                MessageBox.Show("Vous devez selectionner une ligne !");
                return;
            }
            int id = Convert.ToInt32(data[0].ToString());
            Connection.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM commandeVelo WHERE idCommande={id};", Connection);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"DELETE FROM commandePiece WHERE idCommande={id};", Connection);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"DELETE FROM commande WHERE id={id};", Connection);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("No row affected");
                }
            }
            catch
            {
                MessageBox.Show("Cette pièce ne peut pas être supprimée !");
            }
            finally
            {
                Connection.Close();
            }
            LoadCommandes();
        }

        private void AbonnementExportJson_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                string request = "SELECT * FROM historiqueAbonnement h JOIN client c ON c.id = h.idClient JOIN abonnement a ON h.idAbonnement = a.id;";
                MySqlCommand cmd = new MySqlCommand(request, Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt, Formatting.Indented);
                JSONExport.Text = JSONString;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                Connection.Close();
            }
        }

        private void StockExportXML_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            try
            {
                string veloRequest = "SELECT * FROM velo WHERE quantite < 2;";
                string pieceRequest = "SELECT * FROM piece WHERE quantite < 2;";
                MySqlCommand cmd = new MySqlCommand(veloRequest, Connection);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dt.WriteXml(writer, XmlWriteMode.WriteSchema, true);

                cmd = new MySqlCommand(pieceRequest, Connection);
                reader = cmd.ExecuteReader();
                DataTable dtPiece = new DataTable();
                dtPiece.Load(reader);
                dtPiece.WriteXml(writer, XmlWriteMode.WriteSchema, true);
                string dtxml = writer.ToString();
                XMLExport.Text = dtxml;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
