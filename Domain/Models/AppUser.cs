using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;
        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;
    }


}
