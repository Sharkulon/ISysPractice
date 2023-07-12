using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ISysWebAppBack.ModelsDTO.OrganizationDTO;
using ISysWebAppBack.ModelsDTO.UnitsDTO;
using ISysWebAppBack.Services;
using DomainLibBack.Organization;
using ISysCoreLibBack.Service.IOrganizationService;
using DomainLibBack.Utils;
using ISysCoreLibBack.Service.IUtilsService;
using ISysWebAppBack.ModelsDTO.UtilsDTO;

namespace ISysWebAppBack.Controllers.UtilsCtontroller
{
    /// <summary>
    /// API Controller for EmployeeProject model
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : Controller
    {
        private readonly IEmployeeProjectService _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeProject> _logger;

        /// <summary>
        /// Public constructor to create a controller 
        /// </summary>
        /// <param name="repository">Interface repository</param>
        /// <param name="mapper">Interface mapper</param>
        /// <param name="logger">Interface logger</param>
        public EmployeeProjectController(IEmployeeProjectService repository,
            IMapper mapper, ILogger<EmployeeProject> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Add Employee In Project
        /// </summary>
        /// <param name="employeeProjectDto">dto model EmployeeProject</param>
        /// <returns>status code</returns>
        [HttpPost("AddEmployeeInProject")]
        public IActionResult AddEmployeeInProject(
            [FromBody] EmployeeProjectDTO employeeProjectDto)
        {
            if (employeeProjectDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method AddEmployeeInProject");

            if (!_repository.AddEmployeeInProject(
                employeeProjectDto.ProjectCode, employeeProjectDto.EmployeeCode))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }

        /// <summary>
        /// Add Employee In Project Async
        /// </summary>
        /// <param name="employeeProjectDto">dto model EmployeeProject</param>
        /// <returns>status code</returns>
        [HttpPost("AddEmployeeInProjectAsync")]
        public async Task<IActionResult> AddEmployeeInProjectAsync(
            [FromBody] EmployeeProjectDTO employeeProjectDto)
        {
            if (employeeProjectDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method AddEmployeeInProjectAsync");

            if (!await _repository.AddEmployeeInProjectAsync(
                employeeProjectDto.ProjectCode, employeeProjectDto.EmployeeCode))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }

        /// <summary>
        /// Remove Employee From Project
        /// </summary>
        /// <param name="employeeProjectCodeDto">employee project dto</param>
        /// <returns>status code</returns>
        [HttpDelete("RemoveEmployeeFromProject")]
        public IActionResult RemoveEmployeeFromProject(
            [FromBody] EmployeeProjectDTO employeeProjectCodeDto)
        {
            if (!_repository.ParticipatesInProject(
                employeeProjectCodeDto.ProjectCode, employeeProjectCodeDto.EmployeeCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method RemoveEmployeeFromProject");

            if (!_repository.RemoveEmployeeFromProject(
                employeeProjectCodeDto.ProjectCode, employeeProjectCodeDto.EmployeeCode) ||
                !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Remove Employee From Project Async
        /// </summary>
        /// <param name="employeeProjectCodeDto">employee project dto</param>
        /// <returns>status code</returns>
        [HttpDelete("RemoveEmployeeFromProjectAsync")]
        public async Task<IActionResult> RemoveEmployeeFromProjectAsync(
            [FromBody] EmployeeProjectDTO employeeProjectCodeDto)
        {
            if (!await _repository.ParticipatesInProjectAsync(
                employeeProjectCodeDto.ProjectCode, employeeProjectCodeDto.EmployeeCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method RemoveEmployeeFromProjectAsync");

            if (!await _repository.RemoveEmployeeFromProjectAsync(
                employeeProjectCodeDto.ProjectCode, employeeProjectCodeDto.EmployeeCode) ||
                !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }


    }
}
