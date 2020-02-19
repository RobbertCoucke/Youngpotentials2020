using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Responses
{
    public class OpleidingResponseDetail : OpleidingResponse
    {
        public IEnumerable<AfstudeerrichtingResponseDetail> Afstudeerrichting { get; set; }
    }
}
