using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Context
{
    public class ApplicationDataContext : IdentityDbContext<AppUser>
    {
        public ApplicationDataContext()
            : base("EshopConnection")
        { }

        //public System.Data.Entity.DbSet<AppUser> AppUsers { get; set; }
    }
}