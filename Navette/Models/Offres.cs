using System;

namespace Navette.Models
{
    public class Offres
    {
        public int Id { get; set; }
        public int PeriodeAbonnement { get; set; }
        public DateTime HDepart { get; set; }
        public DateTime HArrive { get; set; }
        public string VilleDepart { get; set; }
        public string VilleArrive { get; set; }
        public int NbrVoulu { get; set; }
        public string Desc { get; set; }
        public int Prix { get; set; }
        public bool valable { get; set; }

    }
}