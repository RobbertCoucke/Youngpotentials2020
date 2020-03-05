using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.Entities;
using Type = Youngpotentials.Domain.Entities.Type;

namespace Youngpotentials.Domain.Models.Requests
{
    public class GetOffersByTypesAndTagsRequest
    {
        public IList<Type> types { get; set; }
        public IList<Studiegebied> ids { get; set; }
    }
}
