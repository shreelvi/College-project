using ClassWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Data
{
    public class FakeDAL
    {
        private static List<Assignment> _Assignments = new List<Assignment>()
        {
            new Assignment(){ID=1,Name="Test",Description="Test",DueDate=DateTime.Now,Grade=98,Feedback="Test"},
            new Assignment(){ID = 2,Name = "INFO_4407_HW1", Description = "Entity Relationship Diagram", DueDate = new DateTime(2019, 2, 26), Feedback ="Not Graded" },
            new Assignment(){ ID = 3,Name = "BA_3312_HW2",Description = "Events Summary", DueDate = new DateTime(2019, 2, 26), Grade = 90, Feedback="Good Work!"  },
            new Assignment(){ ID = 4,Name = "INFO_4482_MVP", Description = "Minimum Viable Product of our Project", DueDate = new DateTime(2019, 4, 26), Feedback="Not Graded" },
            new Assignment(){ ID = 5,Name = "MGT_4460_TakeHomeExam", Description = "FInal Exam", DueDate = new DateTime(2019, 4, 26), Feedback="Not Graded" },
        };
        public static List<Assignment> GetAsignments()
        {
            return _Assignments;
        }

        public static Assignment GetAsignment(int actorID)
        {
            return _Assignments.First(a => a.ID == actorID);
        }


        public static int Add(Assignment a)
        {
            if (a != null)
            {
                a.ID = _Assignments.Max(aa => aa.ID) + 1;
                _Assignments.Add(a);
                return 1;
            }
            return -1;
        }

        public static int Edit(Assignment a)
        {
            Assignment a2Edit = _Assignments.First(aa => aa.ID == a.ID);
            if (a2Edit != null)
            {
                a2Edit.Description = a.Description;
                a2Edit.Name = a.Name;
                return 1;
            }
            return -1;
        }

    }
}
