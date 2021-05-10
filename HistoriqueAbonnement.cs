using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class HistoriqueAbonnement
    {
        public HistoriqueAbonnement(int idClient, int idAbonnement)
        {
            IdClient = idClient;
            IdAbonnement = idAbonnement;
        }

        public int IdClient { get; set; }
        public int IdAbonnement { get; set; }
    }
}