﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoungpotentialsAPI.Models.Requests
{
    public class Contact
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
