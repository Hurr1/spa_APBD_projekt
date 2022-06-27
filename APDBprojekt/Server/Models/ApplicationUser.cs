﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Shared.Models;

namespace APDBprojekt.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserCompany> Companies { get; set; }
    }
}
