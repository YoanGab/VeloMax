using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class FournisseurPiece
    {
        public FournisseurPiece(int idFournisseur, int idPiece, int delai, int quantite, int noProduitFournisseur)
        {
            IdFournisseur = idFournisseur;
            IdPiece = idPiece;
            Delai = delai;
            Quantite = quantite;
            NoProduitFournisseur = noProduitFournisseur;
        }

        public int IdFournisseur { get; set; }
        public int IdPiece { get; set; }
        public int Delai { get; set; }
        public int Quantite { get; set; }
        public int NoProduitFournisseur { get; set; }
    }
}