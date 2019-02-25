using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Data
{
    public class ClassWebContext: DbContext
    {
        public ClassWebContext(DbContextOptions<ClassWebContext> options)
           : base(options)
        {
        }

        public DbSet<ClassWeb.Models.Assignment> Assignment { get; set; }
    }
}
