using ClassWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Data
//<summary>
//code by: Elvis
//Initialize DB with test data
//Reference: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.2
//</summary>
{
    public static class DbInitializer
    {
        public static void Initialize(FakeDAL context)
        {
            context.Database.EnsureCreated();

            //Look for any Assignments 
            if (context.Assignment.Any())
            {
                return; //DB has been seeded
            }

            var assignments = new Assignment[]
            {
                new Assignment{ID=1,Name="Test",Description="Test",DueDate=DateTime.Now,Grade=98,Feedback="Test"},
                new Assignment{ID = 2,Name = "INFO_4407_HW1", Description = "Entity Relationship Diagram", DueDate = new DateTime(2019, 2, 26), Feedback ="Not Graded" },
                new Assignment{ID = 3,Name = "BA_3312_HW2",Description = "Events Summary", DueDate = new DateTime(2019, 2, 26), Grade = 90, Feedback="Good Work!"  },
                new Assignment{ID = 4,Name = "INFO_4482_MVP", Description = "Minimum Viable Product of our Project", DueDate = new DateTime(2019, 4, 26), Feedback="Not Graded" },
                new Assignment{ID = 5,Name = "MGT_4460_TakeHomeExam", Description = "FInal Exam", DueDate = new DateTime(2019, 4, 26), Feedback="Not Graded" },
            };

            foreach (Assignment a in assignments)
            {
                context.Assignment.Add(a);

            }

            context.SaveChanges();
        }
    }
}
