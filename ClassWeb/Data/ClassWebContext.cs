<<<<<<< HEAD
﻿using System;
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
=======
﻿using Microsoft.EntityFrameworkCore;
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
>>>>>>> Elvis
        {
        }

        public DbSet<ClassWeb.Models.Assignment> Assignment { get; set; }
<<<<<<< HEAD
=======
        public DbSet<ClassWeb.Models.LoginModel> LoginModel { get; set; }
        public DbSet<ClassWeb.Models.User> User { get; set; }

>>>>>>> Elvis
    }
}
