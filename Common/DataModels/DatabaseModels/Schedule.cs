using Common.DataModels.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.DataModels.DatabaseModels
{
    public class Schedule
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public Major Major { get; set; }
        public TypeOfStudies TypeOfStudies { get; set; }
        public int DegreeLevel { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        public virtual List<ClassEvent> Classes { get; set; }

        public List<ClassEvent> GetClassesForCurrentWeek()
        {
            DateTime currentDay = DateTime.Today;
            DateTime firstDayOfWeek = currentDay.AddDays(1 - currentDay.DayOfWeek.GetHashCode()); // Testing: DateTime.Parse("03-03-2021");//
            DateTime lastDayOfWeek = currentDay.AddDays(7 - currentDay.DayOfWeek.GetHashCode()); // Testing: DateTime.Parse("03-09-2021");//
            return Classes.Where(x => x.StartDate >= firstDayOfWeek && x.EndDate <= lastDayOfWeek).ToList();
        }
    }
}
