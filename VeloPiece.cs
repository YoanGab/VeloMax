using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class VeloPiece
    {
        public VeloPiece(int idVelo, int idPiece, int quantite)
        {
            IdVelo = idVelo;
            IdPiece = idPiece;
            Quantite = quantite;
        }

        public int IdVelo { get; set; }
        public int IdPiece { get; set; }
        public int Quantite { get; set; }
    }
}