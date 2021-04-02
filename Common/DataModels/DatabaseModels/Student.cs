using Common.DataModels.Descriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DataModels.DatabaseModels
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeOfStudies TypeOfStudies { get; set; }
        public Department Department { get; set; }
        public Major Major { get; set; }
        public int DegreeLevel { get; set; }
        public int Semester { get; set; }
        public string Group { get; set; }
        [Required]
        public string CardNumber { get; set; }
    }
}
