using Microsoft.AspNetCore.Identity;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.Identity
{
    public class ApplicationUser : IdentityUser
    { 
        public Customer Customer { get; set; }
    }
}
