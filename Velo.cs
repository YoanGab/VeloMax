using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Velo
    {
        public Velo(int id, string nom, float prixUnitaire, DateTime dateIntroduction, DateTime dateDiscontinuation, int grandeurId, int ligneProduitId, int quantite)
        {
            Id = id;
            Nom = nom;
            PrixUnitaire = prixUnitaire;
            DateIntroduction = dateIntroduction;
            DateDiscontinuation = dateDiscontinuation;
            GrandeurId = grandeurId;
            LigneProduitId = ligneProduitId;
            Quantite = quantite;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public float PrixUnitaire { get; set; }
        public DateTime DateIntroduction { get; set; }
        public DateTime DateDiscontinuation { get; set; }
        public int GrandeurId { get; set; }
        public int LigneProduitId { get; set; }
        public int Quantite { get; set; }
    }
}