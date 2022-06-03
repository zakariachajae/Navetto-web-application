using System;

namespace Navette.Models
{
    public class Demande
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NomDemandeur { get; set; }
        public int PeriodeAbonnement { get; set; }
        public string VilleDepart { get; set; }
        public string VilleArrive { get; set; }
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrive { get; set; }

        public int nbrPersonneInteresse = 1;

    }
}