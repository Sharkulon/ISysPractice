using AutoMapper;
using DomainLibBack.Organization;
using ISysCoreLibBack.Service.IOrganizationService;
using ISysWebAppBack.ModelsDTO.OrganizationDTO;
using ISysWebAppBack.ModelsDTO.UnitsDTO;
using ISysWebAppBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISysWebAppBack.Controllers.OrganizationController
{
    /// <summary>
    /// API Controller for department model
    /// </summary>
    public class DepartmentController : Controller
    {
        private readonly IDepartment _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<Department> _logger;

        /// <summary>
        /// Public constructor to create a controller 
        /// </summary>
        /// <param name="repository">Interface repository</param>
        /// <param name="mapper">Interface mapper</param>
        /// <param name="logger">Interface logger</param>
        public DepartmentController(IDepartment repository,
            IMapper mapper, ILogger<Department> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All Departments on pages. 
        /// </summary>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Department Dto model</returns>
        [HttpGet("GetDepartments")]
        public IEnumerable<DepartmentDTO> GetDepartments(int? pageNumber) =>
            PagesList<DepartmentDTO>.Create(
                _mapper.Map<List<DepartmentDTO>>(_repository.GetDepartments()),
                pageNumber ?? 1);

        /// <summary>
        /// Get All Departments Async on pages. 
        /// </summary>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Department Dto model</returns>
        [HttpGet("GetDepartmentsAsync")]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsAsync(int? pageNumber) =>
            PagesList<DepartmentDTO>.Create(
            _mapper.Map<List<DepartmentDTO>>(await _repository.GetDepartmentsAsync()),
            pageNumber ?? 1);

        /// <summary>
        /// Get department by code
        /// </summary>
        /// <param name="code">code depatment</param>
        /// <returns>Department Dto model</returns>
        [HttpGet("GetDepartmentByCode/{code}")]
        public DepartmentDTO GetDepartmentByCode(string code) =>
            _mapper.Map<DepartmentDTO>(_repository.GetDepartmentByCode(code));

        /// <summary>
        /// Get department by code Async
        /// </summary>
        /// <param name="code">code department</param>
        /// <returns>Department Dto model</returns>
        [HttpGet("GetDepartmentByCodeAsync/{code}")]
        public async Task<DepartmentDTO> GetDepartmentByCodeAsync(string code) =>
            _mapper.Map<DepartmentDTO>
            (await _repository.GetDepartmentByCodeAsync(code));

        /// <summary>
        ///  Get All Employees of department page by page;
        /// </summary>
        /// <param name="code">code of department</param>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetDepartmentEmployees/{code}/employees")]
        public IEnumerable<EmployeeDTO> GetDepartmentEmployees
            (string code, int? pageNumber) =>
            PagesList<EmployeeDTO>.Create(
            _mapper.Map<List<EmployeeDTO>>(_repository.GetEmployees(code)),
            pageNumber ?? 1);

        /// <summary>
        ///  Get All Employees of department page by page Async;
        /// </summary>
        /// <param name="code">code of department</param>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetDepartmentEmployeesAsync/{code}/employees")]
        public async Task<IEnumerable<EmployeeDTO>> GetDepartmentEmployeesAsync
            (string code, int? pageNumber) =>
            PagesList<EmployeeDTO>.Create(
            _mapper.Map<List<EmployeeDTO>>(await _repository.GetEmployeesAsync(code)),
            pageNumber ?? 1);

        /// <summary>
        /// Create a new department
        /// </summary>
        /// <param name="departmentDto">dto model Department</param>
        /// <returns>status code</returns>
        [HttpPost("CreateDepartment")]
        public IActionResult CreateDepartment(
            [FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method CreateDepartment");

            var department = _mapper.Map<Department>(departmentDto);
            if (!_repository.CreateDepartment(department))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        /// <summary>
        /// Create a new department Async
        /// </summary>
        /// <param name="departmentDto">dto model Department</param>
        /// <returns>status code</returns>
        [HttpPost("CreateDepartmentAsync")]
        public async Task<IActionResult> CreateDepartmentAsync(
            [FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method CreateDepartmentAsync");

            var department = _mapper.Map<Department>(departmentDto);
            if (!await _repository.CreateDepartmentAsync(department))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        /// <summary>
        /// Delete department by subdivisionCode
        /// </summary>
        /// <param name="subdivisionCode">code department</param>
        /// <returns>status code</returns>
        [HttpDelete("DeleteDepartment")]
        public IActionResult DeleteDepartment(
            [FromBody] string subdivisionCode)
        {
            if (!_repository.DepartmentExists(subdivisionCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method DeleteDepartment");

            if (!_repository.DeleteDepartment(subdivisionCode) || !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Delete department by subdivisionCodeAsync
        /// </summary>
        /// <param name="subdivisionCode">code department</param>
        /// <returns>status code</returns>
        [HttpDelete("DeleteDepartmentAsync")]
        public async Task<IActionResult> DeleteDepartmentAsync(
            [FromBody] string subdivisionCode)
        {
            if (!await _repository.DepartmentExistsAsync(subdivisionCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method DeleteDepartmentAsync");

            if (!await _repository.DeleteDepartmentAsync(subdivisionCode) ||
                !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Update Department model
        /// </summary>
        /// <param name="departmentDto">dto model Department</param>
        /// <returns>status code</returns>
        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment(
            [FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method UpdateDepartment");

            var department = _mapper.Map<Department>(departmentDto);
            if (!_repository.UpdateDepartment(department))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        /// <summary>
        /// Update Department model Async
        /// </summary>
        /// <param name="departmentDto">dto model Department</param>
        /// <returns>status code</returns>
        [HttpPut("UpdateDepartmentAsync")]
        public async Task<IActionResult> UpdateDepartmentAsync(
            [FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method UpdateDepartmentAsync");

            var department = _mapper.Map<Department>(departmentDto);
            if (!await _repository.UpdateDepartmentAsync(department))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }
    }
}
