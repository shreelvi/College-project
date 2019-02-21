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
            new Assignment(){ID=1,Title="Test",Description="Test",DueDate=DateTime.Now,Grade=98,Feedback="Test"},
            new Assignment(){ID = 2,Title = "Freak", Description = "Monkey" },
            new Assignment(){ ID = 3,Title = "Slayer",Description = "Doom",  },
            new Assignment(){ ID = 4,Title = "McStabberson", Description = "Stabby",  },
            new Assignment(){ ID = 5,Title = "Doe", Description = "Jane",  },
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
                a2Edit.Title = a.Title;
                return 1;
            }
            return -1;
        }

    }
}
