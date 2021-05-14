using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Fournisseur
    {
        public Fournisseur(int id, string siret, string nom, string nomContact, string prenomContact, string mailContact, string rue, string ville, string codePostal, string province, int libelle)
        {
            Id = id;
            Siret = siret;
            Nom = nom;
            NomContact = nomContact;
            PrenomContact = prenomContact;
            MailContact = mailContact;
            Rue = rue;
            Ville = ville;
            CodePostal = codePostal;
            Province = province;
            Libelle = libelle;
        }

        public int Id{ get; set; }
        public string Siret { get; set; }
        public string Nom { get; set; }
        public string NomContact { get; set; }
        public string PrenomContact { get; set; }
        public string MailContact { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Province { get; set; }
        public int Libelle { get; set; }
    }
}