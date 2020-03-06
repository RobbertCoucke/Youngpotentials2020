using System;
using System.Collections.Generic;
using System.Text;

namespace Youngpotentials.Domain.Models.Responses
{
    public class FavoriteResponse
    {

        public int Id { get; set; }

        public OfferResponse Vacature { get; set; }
    }
}
