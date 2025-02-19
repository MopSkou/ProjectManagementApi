using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoljnostiEmployeeController : Controller
    {
        private readonly IDoljnostiEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public DoljnostiEmployeeController(IDoljnostiEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DoljnostiEmployee>))]
        public IActionResult GetDoljnostiEmployees()
        {
            var doljnostiEmployees = _mapper.Map<List<DoljnostiEmployeeDto>>(_repository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doljnostiEmployees);
        }

        [HttpGet("{employeeId}/{postId}")]
        [ProducesResponseType(200, Type = typeof(DoljnostiEmployee))]
        [ProducesResponseType(400)]
        public IActionResult GetDoljnostiEmployee(int employeeId, int postId)
        {
            if (!_repository.Exists(employeeId, postId))
                return NotFound();

            var doljnostiEmployee = _mapper.Map<DoljnostiEmployeeDto>(_repository.GetById(employeeId, postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(doljnostiEmployee);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDoljnostiEmployee([FromBody] DoljnostiEmployeeDto dto)
        {
            if (dto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doljnostiEmployeeMap = _mapper.Map<DoljnostiEmployee>(dto);

            if (!_repository.Create(doljnostiEmployeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{employeeId}/{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoljnostiEmployee(int employeeId, int postId, [FromBody] DoljnostiEmployeeDto doljnostiEmployeeUpdate)
        {
            if (doljnostiEmployeeUpdate == null)
                return BadRequest(ModelState);

            if (employeeId != doljnostiEmployeeUpdate.EmployeeID || postId != doljnostiEmployeeUpdate.PostID)
                return BadRequest(ModelState);

            if (!_repository.Exists(employeeId, postId))
                return NotFound(new { message = "Error: Record not found" });

            if (!ModelState.IsValid)
                return BadRequest();

            var doljnostiEmployeeMap = _mapper.Map<DoljnostiEmployee>(doljnostiEmployeeUpdate);

            if (!_repository.Update(doljnostiEmployeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{employeeId}/{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDoljnostiEmployee(int employeeId, int postId)
        {
            if (!_repository.Exists(employeeId, postId))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var deleteEntity = _repository.GetById(employeeId, postId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.Delete(deleteEntity))
            {
                ModelState.AddModelError("", "Something went wrong deleting");
            }

            return NoContent();
        }
    }
}
