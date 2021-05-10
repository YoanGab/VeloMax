using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class CommandePiece
    {
        public CommandePiece(int idCommande, int idPiece, int quantite)
        {
            IdCommande = idCommande;
            IdPiece = idPiece;
            Quantite = quantite;
        }

        public int IdCommande { get; set; }
        public int IdPiece { get; set; }
        public int Quantite { get; set; }
    }
}
