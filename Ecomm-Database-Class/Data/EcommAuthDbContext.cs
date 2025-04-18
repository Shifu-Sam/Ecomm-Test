using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Data
{
    public class EcommAuthDbContext : IdentityDbContext
    {
        public EcommAuthDbContext(DbContextOptions<EcommAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminUserId = "d60b1b08-6b2b-4601-a20c-81db0320f1fa";
            var customerUserId = "da72a912-b9e3-4336-8cb8-ca2d4d3cad54";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminUserId,
                    ConcurrencyStamp = adminUserId,
                    Name = "adminUser",
                    NormalizedName = "adminUser".ToUpper()
                },
                new IdentityRole
                {
                    Id = customerUserId,
                    ConcurrencyStamp = customerUserId,
                    Name = "customerUser",
                    NormalizedName = "customerUser".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }


    }
}
