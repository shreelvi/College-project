using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClassWeb.Models
{
    public class ClassWebContext : DbContext
    {
        public ClassWebContext (DbContextOptions<ClassWebContext> options)
            : base(options)
        {
        }

        public DbSet<ClassWeb.Models.Class> Class { get; set; }
    }
}
