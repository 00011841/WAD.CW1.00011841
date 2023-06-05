using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAD.CW1._11841.Interfaces;
using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo _StudentRepo;
        private readonly ITeacherRepo _TeacherRepo;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepo StudentRepo, ITeacherRepo TeacherRepo, IMapper mapper)
        {
            _StudentRepo = StudentRepo;
            _TeacherRepo = TeacherRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Student>))]
        public IActionResult GetAll()
        {
            var Students = _mapper.Map<List<StudentDto>>(_StudentRepo.GetAll());
            if (ModelState.IsValid)
            {
                return Ok(Students);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] int TeacherId, [FromBody] StudentDto StudentCreate)
        {
            if (StudentCreate == null)
                return BadRequest(ModelState);

            var Students = _StudentRepo.GetStudentTrimToUpper(StudentCreate);

            if (Students != null)
            {
                ModelState.AddModelError("", "Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StudentMap = _mapper.Map<Student>(StudentCreate);
            StudentMap.Teacher = _TeacherRepo.GetById(TeacherId);
            var StudentMap2 = _mapper.Map<StudentDto>(StudentMap);

            if (!_StudentRepo.CreateStudent(TeacherId, StudentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{StudentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int StudentId, [FromQuery] int TeacherId,
            [FromBody] StudentDto updateStudent)
        {
            if (updateStudent == null)
                return BadRequest(ModelState);

            if (StudentId != updateStudent.StudentId)
                return BadRequest(ModelState);

            if (!_StudentRepo.IsExist(StudentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var StudentMap = _mapper.Map<Student>(updateStudent);

            if (!_StudentRepo.UpdateStudent(TeacherId, StudentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{StudentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int StudentId)
        {
            if (!_StudentRepo.IsExist(StudentId))
            {
                return NotFound();
            }
            var StudentToDelete = _StudentRepo.GetById(StudentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_StudentRepo.DeleteStudent(StudentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }
            return NoContent();
        }
    }
}
