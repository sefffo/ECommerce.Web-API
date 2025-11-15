using Ecommerce.Domain.Models.Identity;
//using Ecommerce.Presistence.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Contexts
{
    public class StoreIdntityDbContext(DbContextOptions<StoreIdntityDbContext> options):IdentityDbContext<ApplicationUser>(options)
    {



        protected override void OnModelCreating(ModelBuilder builder)
        {




            base.OnModelCreating(builder);




            builder.Entity<Address>().ToTable("Adresses");
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("User_Roles");


            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();//we will use JWT
            builder.Ignore<IdentityUserLogin<string>>();


           

        }


    }
}
