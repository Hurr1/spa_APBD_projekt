using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APDBprojekt.Server.Models
{
    public class ApplicationUserCompany
    {
        public int IdApplicationUserCompany { get; set; }
        public int IdCompany { get; set; }
        public string Id { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ApplicationUser IdUserNavigation { get; set; }

    }
}
