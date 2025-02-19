using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApi.Dto;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompaniesRepository companiesRepository, IMapper mapper)
        {
            _companiesRepository = companiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Companies>))]
        public IActionResult GetCompanies()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companiesRepository.GetCompanies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(Companies))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyById(int companyId)
        {
            if (!_companiesRepository.CompanyExists(companyId))
                return NotFound();

            var company = _mapper.Map<CompanyDto>(_companiesRepository.GetCompanyById(companyId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreate)
        {
            if (companyCreate == null)
                return BadRequest(ModelState);

            var company = _companiesRepository.GetCompanies()
                .Where(c => c.CompanyName.Trim().ToUpper() == companyCreate.CompanyName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (company != null)
            {
                ModelState.AddModelError("", "Company already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<Companies>(companyCreate);

            if (!_companiesRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int companyId, [FromBody] CompanyDto companyUpdate)
        {
            if (companyUpdate == null)
                return BadRequest(ModelState);

            if (companyId != companyUpdate.CompanyID)
                return BadRequest(ModelState);

            if (!_companiesRepository.CompanyExists(companyId))
                return BadRequest(new { message = "Error: Invalid Id" });

            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<Companies>(companyUpdate);

            if (!_companiesRepository.UpdateCompany(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companiesRepository.CompanyExists(companyId))
            {
                return BadRequest(new { message = "Error: Invalid Id" });
            }

            var deleteCompany = _companiesRepository.GetCompanyById(companyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_companiesRepository.DeleteCompany(deleteCompany))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
            }

            return NoContent();
        }
    }
}