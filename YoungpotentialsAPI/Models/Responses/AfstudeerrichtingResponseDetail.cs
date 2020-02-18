using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Responses
{
    public class AfstudeerrichtingResponseDetail : AfstudeerrichtingResponse
    {
        public IEnumerable<KeuzeResponse> Keuze { get; set; }
    }
}
