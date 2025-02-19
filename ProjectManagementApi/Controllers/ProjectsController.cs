using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectsRepository projectsRepository, IMapper mapper)
        {
            _projectsRepository = projectsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectDto>))]
        public IActionResult GetProjects()
        {
            var projects = _mapper.Map<List<ProjectDto>>(_projectsRepository.GetProjects());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(200, Type = typeof(ProjectDto))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectById(int projectId)
        {
            if (!_projectsRepository.ProjectExists(projectId))
                return NotFound();

            var project = _mapper.Map<ProjectDto>(_projectsRepository.GetProjectById(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProject([FromBody] ProjectDto projectCreate)
        {
            if (projectCreate == null)
                return BadRequest(ModelState);

            if (_projectsRepository.GetProjects()
                .Any(p => p.ProjectName.Trim().ToUpper() == projectCreate.ProjectName.TrimEnd().ToUpper()))
            {
                ModelState.AddModelError("", "Project with this name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectMap = _mapper.Map<Projects>(projectCreate);

            if (!_projectsRepository.CreateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] ProjectDto projectUpdate)
        {
            if (projectUpdate == null)
                return BadRequest(ModelState);

            if (projectId != projectUpdate.ProjectID)
                return BadRequest(ModelState);

            if (!_projectsRepository.ProjectExists(projectId))
                return NotFound(new { message = "Error: Project not found" });

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<Projects>(projectUpdate);

            if (!_projectsRepository.UpdateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating project");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectsRepository.ProjectExists(projectId))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var projectToDelete = _projectsRepository.GetProjectById(projectId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_projectsRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting project");
            }

            return NoContent();
        }
    }
}
