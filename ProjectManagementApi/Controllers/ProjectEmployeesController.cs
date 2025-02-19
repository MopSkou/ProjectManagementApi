using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ProjectEmployeesController : Controller
        {
            private readonly IProjectEmployeesRepository _repository;
            private readonly IMapper _mapper;

            public ProjectEmployeesController(IProjectEmployeesRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectEmployeesDto>))]
            public IActionResult GetProjectEmployees()
            {
                var projectEmployees = _mapper.Map<List<ProjectEmployeesDto>>(_repository.GetAll());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(projectEmployees);
            }

            [HttpGet("{projectId}/{employeeId}")]
            [ProducesResponseType(200, Type = typeof(ProjectEmployeesDto))]
            [ProducesResponseType(400)]
            public IActionResult GetProjectEmployee(int projectId, int employeeId)
            {
                if (!_repository.Exists(projectId, employeeId))
                    return NotFound();

                var projectEmployee = _mapper.Map<ProjectEmployeesDto>(_repository.GetById(projectId, employeeId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(projectEmployee);
            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateProjectEmployee([FromBody] ProjectEmployeesDto dto)
            {
                if (dto == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var projectEmployeeMap = _mapper.Map<ProjectEmployees>(dto);

                if (!_repository.Create(projectEmployeeMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }

            [HttpPut("{projectId}/{employeeId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult UpdateProjectEmployee(int projectId, int employeeId, [FromBody] ProjectEmployeesDto projectEmployeeUpdate)
            {
                if (projectEmployeeUpdate == null)
                    return BadRequest(ModelState);

                if (projectId != projectEmployeeUpdate.ProjectID || employeeId != projectEmployeeUpdate.EmployeeID)
                    return BadRequest(ModelState);

                if (!_repository.Exists(projectId, employeeId))
                    return NotFound(new { message = "Error: Record not found" });

                if (!ModelState.IsValid)
                    return BadRequest();

                var projectEmployeeMap = _mapper.Map<ProjectEmployees>(projectEmployeeUpdate);

                if (!_repository.Update(projectEmployeeMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }

            [HttpDelete("{projectId}/{employeeId}")]
            [ProducesResponseType(400)]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public IActionResult DeleteProjectEmployee(int projectId, int employeeId)
            {
                if (!_repository.Exists(projectId, employeeId))
                {
                    return BadRequest(new { message = "Error: Invalid Id" });
                }

                var deleteEntity = _repository.GetById(projectId, employeeId);

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

