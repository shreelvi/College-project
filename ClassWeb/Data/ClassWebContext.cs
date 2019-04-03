using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    public class ClassWebContext : DbContext
    {
        public ClassWebContext(DbContextOptions<ClassWebContext> options)
            : base(options)
        {

        }
        public ClassWebContext() {

            }
        public DbSet<ClassWeb.Models.Group> Groups { get; set; }
        public DbSet<ClassWeb.Models.Class> Class { get; set; }

        public DbSet<ClassWeb.Models.Course> Course { get; set; }
    }
}
