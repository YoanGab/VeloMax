using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Client
    {
        public Client(int id, bool estEntreprise, string siret, string nom, string prenom, string mail, string telephone, string rue, string ville, string codePostal, string province, bool estAbonne, int idAbonnement)
        {
            Id = id;
            EstEntreprise = estEntreprise;
            Siret = siret;
            Nom = nom;
            Prenom = prenom;
            Mail = mail;
            Telephone = telephone;
            Rue = rue;
            Ville = ville;
            CodePostal = codePostal;
            Province = province;
            EstAbonne = estAbonne;
            IdAbonnement = idAbonnement;
        }
        public int Id { get; set; }
        public bool EstEntreprise { get; set; }
        public string Siret { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Province { get; set; }
        public bool EstAbonne { get; set; }
        public int IdAbonnement { get; set; }
    }
}
