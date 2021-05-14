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
    /// Logique d'interaction pour UpdateCommande.xaml
    /// </summary>
    public partial class UpdateCommande : Window
    {
        MySqlConnection Connection;
        int IdCommande;
        public UpdateCommande(Commande commande, MySqlConnection connection)
        {
            InitializeComponent();
            IdCommande = commande.Id;
            Connection = connection;
        }
    }
}
