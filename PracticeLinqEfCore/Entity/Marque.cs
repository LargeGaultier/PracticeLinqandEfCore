using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Marque
    {
        public Marque()
        {
            Biere = new HashSet<Biere>();
        }

        public string NomMarque { get; set; }
        public string CodeBrasserie { get; set; }

        public virtual Brasserie CodeBrasserieNavigation { get; set; }
        public virtual ICollection<Biere> Biere { get; set; }
    }
}
