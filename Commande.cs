using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Commande
    {
        public Commande(int id, DateTime dateCreation, DateTime dateValidation, DateTime dateExpedition, string rueLivraison, string villeLivraison, string codePostalLivraison, string provinceLivraison, int idClient, float prix, string statut, int delai)
        {
            Id = id;
            DateCreation = dateCreation;
            DateValidation = dateValidation;
            DateExpedition = dateExpedition;
            RueLivraison = rueLivraison;
            VilleLivraison = villeLivraison;
            CodePostalLivraison = codePostalLivraison;
            ProvinceLivraison = provinceLivraison;
            IdClient = idClient;
            Prix = prix;
            Statut = statut;
            Delai = delai;
        }

        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateValidation { get; set; }
        public DateTime DateExpedition { get; set; }
        public string RueLivraison { get; set; }
        public string VilleLivraison { get; set; }
        public string CodePostalLivraison { get; set; }
        public string ProvinceLivraison { get; set; }
        public int IdClient { get; set; }
        public float Prix { get; set; }
        public string Statut { get; set; }
        public int Delai { get; set; }
    }
}