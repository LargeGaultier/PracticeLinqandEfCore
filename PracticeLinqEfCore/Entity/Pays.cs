using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Pays
    {
        public Pays()
        {
            Region = new HashSet<Region>();
        }

        public string NomPays { get; set; }
        public double? Consommation { get; set; }
        public double? Production { get; set; }

        public virtual ICollection<Region> Region { get; set; }
    }
}
