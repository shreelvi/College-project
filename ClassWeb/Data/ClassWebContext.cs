using ClassWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Data
{
    //<summary>
    //The database context class relates the database with the model
    //Reference: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.2
    //</summary>
    public class ClassWebContext: DbContext
    {
        public ClassWebContext(DbContextOptions<ClassWebContext> options)
           : base(options)
        {
        }

        public DbSet<Assignment> Assignment { get; set; }

        public DbSet<ClassWeb.Models.CourseSemester> CourseSemester { get; set; }

        public DbSet<ClassWeb.Models.Course> Course { get; set; }

        public DbSet<ClassWeb.Models.Semester> Semester { get; set; }

        public DbSet<ClassWeb.Models.Year> Year { get; set; }
    }
}
