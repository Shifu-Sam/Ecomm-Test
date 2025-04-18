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

       
    }
}
