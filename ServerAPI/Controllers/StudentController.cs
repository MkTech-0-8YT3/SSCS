using AutoMapper;
using Common.DataModels.DatabaseModels;
using Common.DataModels.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ISSCSDbContext _dbContext;
        private readonly IMapper _mapper;
        public StudentController(ISSCSDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost("getStudent")]
        public async Task<ActionResult<StudentNameDto>> GetStudent(CardNumberDto cardNumber)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.CardNumber.Equals(cardNumber.CardIdNumber));
            if (student == null)
                return NotFound("Student with given card number doesn't exist!");
            return Ok(new StudentNameDto() { FirstName = student.FirstName });
        }

        [HttpPost("getSchedule")]
        public async Task<ActionResult<List<ClassEventDto>>> GetCurrentWeekSchedule(CardNumberDto cardNumber)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.CardNumber.Equals(cardNumber.CardIdNumber));
            var schedule = _dbContext.Schedules.Include(x=>x.Classes).FirstOrDefault(x => x.Department.Equals(student.Department) &&
            x.Major.Equals(student.Major) &&
            x.Semester.Equals(student.Semester));
            schedule.Classes.Sort((x,y) => DateTime.Compare(x.StartDate, y.StartDate));

            if (student == null)
                return NotFound("Student with given card number doesn't exist!");
            return Ok(_mapper.Map<List<ClassEventDto>>(schedule.GetClassesForCurrentWeek()));
        }

        [HttpPost("createTest")]
        public async Task<ActionResult> CreateTestStudent()
        {
            var cardNumber = "12345";
            var testStudent = new Student()
            {
                FirstName = "Example First Name",
                LastName = "Example Last Name",
                TypeOfStudies = Common.DataModels.Descriptors.TypeOfStudies.Extramural,
                Department = Common.DataModels.Descriptors.Department.WBMiI,
                Major = Common.DataModels.Descriptors.Major.Inf,
                DegreeLevel = 1,
                Semester = 6,
                Group = "gr1",
                CardNumber = cardNumber
            };
            _dbContext.Students.Add(testStudent);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("deleteTest")]
        public async Task<ActionResult> DeleteTestStudent()
        {
            var student = _dbContext.Students.FirstOrDefault();
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
