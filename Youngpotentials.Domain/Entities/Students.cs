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
        public string CvUrl { get; set; }
        public string FirstName { get; set; }
        public int? UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Applications> Applications { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
