using AutoMapper;
using Common.DataModels.DatabaseModels;
using Common.DataModels.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
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
        public async Task<ActionResult<Student>> GetStudent(CardNumberDto cardNumber)
        {
            var student =  _dbContext.Students.SingleOrDefault(x => x.CardNumber.Equals(cardNumber.CardIdNumber));
            if (student == null)
                return NotFound("Student with given card number doesn't exist!");
            return Ok(student);
        }

        [HttpPost("getSchedule")]
        public async Task<ActionResult<Student>> GetCurrentWeekSchedule(CardNumberDto cardNumber)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.CardNumber.Equals(cardNumber.CardIdNumber));
            
            var schedule = _dbContext.Schedules.Include(x=>x.Classes).SingleOrDefault(x => x.Department.Equals(student.Department) &&
            x.Major.Equals(student.Major) &&
            x.Semester.Equals(student.Semester));

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
                FirstName = "test",
                LastName = "test",
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
            var student = _dbContext.Students.SingleOrDefault();
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
