using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Brasserie
    {
        public Brasserie()
        {
            Marque = new HashSet<Marque>();
        }

        public string CodeBrasserie { get; set; }
        public string NomBrasserie { get; set; }
        public string Ville { get; set; }
        public string NomRegion { get; set; }

        public virtual Region NomRegionNavigation { get; set; }
        public virtual ICollection<Marque> Marque { get; set; }
    }
}
