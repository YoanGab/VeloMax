using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class Abonnement
    {
        public Abonnement(int id, int idProgramme, DateTime dateDebut, DateTime dateFin)
        {
            Id = id;
            IdProgramme = idProgramme;
            DateDebut = dateDebut;
            DateFin = dateFin;
        }

        public int Id { get; set; }
        public int IdProgramme { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

    }
}