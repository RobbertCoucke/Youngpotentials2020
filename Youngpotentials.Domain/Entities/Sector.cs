using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Sector
    {
        public Sector()
        {
            Companies = new HashSet<Companies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Companies> Companies { get; set; }
    }
}
