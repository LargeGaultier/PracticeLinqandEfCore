using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Region
    {
        public Region()
        {
            Brasserie = new HashSet<Brasserie>();
        }

        public string NomRegion { get; set; }
        public string NomPays { get; set; }

        public virtual Pays NomPaysNavigation { get; set; }
        public virtual ICollection<Brasserie> Brasserie { get; set; }
    }
}
