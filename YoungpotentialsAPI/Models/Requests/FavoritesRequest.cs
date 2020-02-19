using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Requests
{
    public class FavoritesRequest
    {
        public int UserId { get; set; }
        public int OfferId { get; set; }
    }
}
