using Common.DataModels.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DataModels.DatabaseModels
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public TypeOfStudies TypeOfStudies { get; set; }
        public Department Department { get; set; }
        public Major Major { get; set; }
        public int Semester { get; set; }
        public string CardId { get; set; }
    }
}
