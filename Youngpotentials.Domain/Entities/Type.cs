using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Type
    {
        public Type()
        {
            Offers = new HashSet<Offers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Offers> Offers { get; set; }
    }
}
