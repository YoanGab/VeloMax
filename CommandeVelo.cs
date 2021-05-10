using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class CommandeVelo
    {
        public CommandeVelo(int idCommande, int idVelo, int quantite)
        {
            IdCommande = idCommande;
            IdVelo = idVelo;
            Quantite = quantite;
        }

        public int IdCommande { get; set; }
        public int IdVelo { get; set; }
        public int Quantite { get; set; }
    }
}