using Common.DataModels.DatabaseModels;
using Common.DataModels.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ISSCSDbContext _dbContext;
        public StudentController(ISSCSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("getStudent")]
        public async Task<ActionResult<Student>> GetStudent(CardNumber cardNumber)
        {
            var student =  _dbContext.Students.SingleOrDefault(x => x.CardNumber.Equals(cardNumber.CardIdNumber));
            if (student == null)
                return NotFound("Student with given card number doesn't exist!");
            return Ok(student);
        }

        [HttpPost("createTest")]
        public async Task<ActionResult> CreateTestStudent()
        {
            var cardNumber = "12345";
            var testStudent = new Student()
            {
                FirstName = "test",
                LastName = "test",
                TypeOfStudies = Common.DataModels.Descriptors.TypeOfStudies.Fulltime,
                Department = Common.DataModels.Descriptors.Department.WBMiI,
                Major = Common.DataModels.Descriptors.Major.InformationTechnology,
                Semester = 6,
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
