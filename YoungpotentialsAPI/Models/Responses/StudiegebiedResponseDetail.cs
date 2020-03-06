using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.Models.Responses;

namespace YoungpotentialsAPI.Models.Responses
{
    public class StudiegebiedResponseDetail : StudiegebiedResponse
    {
        public IEnumerable<OpleidingResponseDetail> Opleiding { get; set; }
    }
}
