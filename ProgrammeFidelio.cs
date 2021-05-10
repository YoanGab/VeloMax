using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMax
{
    public class ProgrammeFidelio
    {
        public ProgrammeFidelio(int id, string description, float prix, int duree, float rabais)
        {
            Id = id;
            Description = description;
            Prix = prix;
            Duree = duree;
            Rabais = rabais;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public float Prix { get; set; }
        public int Duree { get; set; }
        public float Rabais { get; set; }
    }
}