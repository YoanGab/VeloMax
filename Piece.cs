using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Piece
    {
        public Piece(int id, string reference, string description, float prixUnitaire, DateTime dateIntroduction, DateTime dateDiscontinuation, int typeId, int quantite)
        {
            Id = id;
            Reference = reference;
            Description = description;
            PrixUnitaire = prixUnitaire;
            DateIntroduction = dateIntroduction;
            DateDiscontinuation = dateDiscontinuation;
            TypeId = typeId;
            Quantite = quantite;
        }
        public Piece()
        {

        }
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public float PrixUnitaire { get; set; }
        public DateTime DateIntroduction { get; set; }
        public DateTime DateDiscontinuation { get; set; }
        public int TypeId { get; set; }
        public int Quantite { get; set; }
    }
}