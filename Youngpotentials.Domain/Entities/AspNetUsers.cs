using System;
using System.Collections.Generic;

namespace Youngpotentials.Domain.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            Companies = new HashSet<Companies>();
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Telephone { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public string Email2 { get; set; }
        public string Code { get; set; }

        public  AspNetRoles Role { get; set; }
        public  ICollection<Companies> Companies { get; set; }
        public  ICollection<Students> Students { get; set; }
    }
}
