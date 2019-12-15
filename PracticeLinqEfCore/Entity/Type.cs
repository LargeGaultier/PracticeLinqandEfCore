using System;
using System.Collections.Generic;

namespace PracticeLinqEfCore.Entity
{
    public partial class Type
    {
        public Type()
        {
            Biere = new HashSet<Biere>();
        }

        public int NroType { get; set; }
        public string NomType { get; set; }
        public string Description { get; set; }
        public string Fermentation { get; set; }
        public string Commentaire { get; set; }

        public virtual ICollection<Biere> Biere { get; set; }
    }
}
