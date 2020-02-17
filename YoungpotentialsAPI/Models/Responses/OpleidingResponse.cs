using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Responses
{
    public class OpleidingResponse
    {
        public string NaamOpleiding { get; set; }
        public string IdStudiegebied { get; set; }
        public StudiegebiedResponse IdStudiegebiedNavigation { get; set; }
    }



}
