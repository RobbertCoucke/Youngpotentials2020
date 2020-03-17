using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class Students
    {
        public Students()
        {
            Applications = new HashSet<Applications>();
            Favorites = new HashSet<Favorites>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int? UserId { get; set; }

        public AspNetUsers User { get; set; }
        public ICollection<Applications> Applications { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
    }
}
