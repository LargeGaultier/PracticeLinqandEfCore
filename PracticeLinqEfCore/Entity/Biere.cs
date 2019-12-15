using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Biere
    {
        public string NomMarque { get; set; }
        public string Version { get; set; }
        public int? NumType { get; set; }
        public string CouleurBiere { get; set; }
        public float? TauxAlcool { get; set; }
        public string Caracteristiques { get; set; }
        public DateTime? DateCreation { get; set; }

        public virtual Marque NomMarqueNavigation { get; set; }
        public virtual Type NumTypeNavigation { get; set; }
    }
}
