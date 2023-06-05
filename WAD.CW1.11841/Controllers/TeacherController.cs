using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAD.CW1._11841.Interfaces;
using WAD.CW1._11841.Models;

namespace WAD.CW1._11841.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepo _TeacherRepo;
        private readonly IMapper _mapper;
        public TeacherController(ITeacherRepo TeacherRepo, IMapper mapper)
        {
            _TeacherRepo = TeacherRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Teacher>))]
        public IActionResult GetAll()
        {
            var Teachers = _mapper.Map<List<TeacherDto>>(_TeacherRepo.GetAll());
            if (ModelState.IsValid)
            {
                return Ok(Teachers);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] int TeacherId, [FromBody] TeacherDto TeacherCreate)
        {
            if (TeacherCreate == null)
                return BadRequest(ModelState);

            var Teachers = _TeacherRepo.GetTeacherTrimToUpper(TeacherCreate);

            if (Teachers != null)
            {
                ModelState.AddModelError("", "Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var TeacherMap = _mapper.Map<Teacher>(TeacherCreate);


            if (!_TeacherRepo.CreateTeacher(TeacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{TeacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(int TeacherId,
            [FromBody] TeacherDto updateTeacher)
        {
            if (updateTeacher == null)
                return BadRequest(ModelState);

            if (TeacherId != updateTeacher.TeacherId)
                return BadRequest(ModelState);

            if (!_TeacherRepo.IsExist(TeacherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var TeacherMap = _mapper.Map<Teacher>(updateTeacher);

            if (!_TeacherRepo.UpdateTeacher(TeacherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{TeacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacher(int TeacherId)
        {
            if (!_TeacherRepo.IsExist(TeacherId))
            {
                return NotFound();
            }
            var TeacherToDelete = _TeacherRepo.GetById(TeacherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_TeacherRepo.DeleteTeacher(TeacherToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }

    }
}
