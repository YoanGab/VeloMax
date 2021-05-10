using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Velo
    {
        public Velo(int id, float prixUnitaire, DateTime dateIntroduction, DateTime dateDiscontinuation, string grandeurId, string ligneProduitId, int quantite)
        {
            Id = id;
            PrixUnitaire = prixUnitaire;
            DateIntroduction = dateIntroduction;
            DateDiscontinuation = dateDiscontinuation;
            GrandeurId = grandeurId;
            LigneProduitId = ligneProduitId;
            Quantite = quantite;
        }

        public int Id { get; set; }
        public float PrixUnitaire { get; set; }
        public DateTime DateIntroduction { get; set; }
        public DateTime DateDiscontinuation { get; set; }
        public string GrandeurId { get; set; }
        public string LigneProduitId { get; set; }
        public int Quantite { get; set; }
    }
}