using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesRepository employeesRepository, IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeesRepository.GetEmployees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeById(int employeeId)
        {
            if (!_employeesRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>(_employeesRepository.GetEmployeeById(employeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeCreate)
        {
            if (employeeCreate == null)
                return BadRequest(ModelState);

            if (_employeesRepository.GetEmployees()
                .Any(e => e.Email.Trim().ToUpper() == employeeCreate.Email.TrimEnd().ToUpper()))
            {
                ModelState.AddModelError("", "Employee with this email already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employees>(employeeCreate);

            if (!_employeesRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto employeeUpdate)
        {
            if (employeeUpdate == null)
                return BadRequest(ModelState);

            if (employeeId != employeeUpdate.EmployeeID)
                return BadRequest(ModelState);

            if (!_employeesRepository.EmployeeExists(employeeId))
                return NotFound(new { message = "Error: Employee not found" });

            if (!ModelState.IsValid)
                return BadRequest();

            var employeeMap = _mapper.Map<Employees>(employeeUpdate);

            if (!_employeesRepository.UpdateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating employee");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeesRepository.EmployeeExists(employeeId))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var employeeToDelete = _employeesRepository.GetEmployeeById(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeesRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
            }

            return NoContent();
        }
    }
}