using AutoMapper;
using DomainLibBack.Units;
using ISysCoreLibBack.Service.IUnitService;
using ISysWebAppBack.ModelsDTO.UnitsDTO;
using ISysWebAppBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISysWebAppBack.Controllers.UnitsContollers
{
    /// <summary>
    /// API Controller for employee model
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<Employee> _logger;

        /// <summary>
        /// Public constructor to create a controller 
        /// </summary>
        /// <param name="repository">Interface repository</param>
        /// <param name="mapper">Interface mapper</param>
        /// <param name="logger">Interface logger</param>
        public EmployeeController(IEmployeeService repository,
            IMapper mapper, ILogger<Employee> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get All Employee on pages. 
        /// </summary>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetEmployees")]
        public IEnumerable<EmployeeDTO> GetEmployees(int? pageNumber) =>
        PagesList<EmployeeDTO>.Create(
                _mapper.Map<List<EmployeeDTO>>(_repository.GetEmployees()),
                pageNumber ?? 1);


        /// <summary>
        /// Get All Employees Async on pages. 
        /// </summary>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetEmployeesAsync")]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(int? pageNumber) =>
        PagesList<EmployeeDTO>.Create(
            _mapper.Map<List<EmployeeDTO>>(await _repository.GetEmployeesAsync()),
            pageNumber ?? 1);

        /// <summary>
        /// Get employee by code
        /// </summary>
        /// <param name="code">code employee</param>
        /// <returns>Employee Dto model</returns>
        [HttpGet("GetEmployeesByCode/{code}")]
        public EmployeeDTO GetEmployeesByCode(int code) =>
            _mapper.Map<EmployeeDTO>(_repository.GetEmployeeByCode(code));

        /// <summary>
        /// Get employee by code Async
        /// </summary>
        /// <param name="code">code employee</param>
        /// <returns>Employee Dto model</returns>
        [HttpGet("GetEmployeesByCodeAsync/{code}")]
        public async Task<EmployeeDTO> GetEmployeesByCodeAsync(int code) =>
            _mapper.Map<EmployeeDTO>
            (await _repository.GetEmployeeByCodeAsync(code));

        /// <summary>
        /// Get all subordinate employees
        /// </summary>
        /// <param name="code">code employee</param>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetSubEmployees/{code}/subordinates")]
        public IEnumerable<EmployeeDTO> GetSubEmployees(int code, int? pageNumber) =>
        PagesList<EmployeeDTO>.Create(
                _mapper.Map<List<EmployeeDTO>>(_repository.GetSubEmployees(code)),
                pageNumber ?? 1);

        /// <summary>
        /// Get all subordinate employees Async
        /// </summary>
        /// <param name="code">code employee</param>
        /// <param name="pageNumber">Page number, by default 1</param>
        /// <returns>List Employee Dto model</returns>
        [HttpGet("GetSubEmployeesAsync/{code}/subordinates")]
        public async Task<IEnumerable<EmployeeDTO>> GetSubEmployeesAsync
        (int code, int? pageNumber) =>
            PagesList<EmployeeDTO>.Create(
                _mapper.Map<List<EmployeeDTO>>
                (await _repository.GetSubEmployeesAsync(code)),
                pageNumber ?? 1);

        /// <summary>
        /// Create a new Employee
        /// </summary>
        /// <param name="employeeDto">dto model Employee</param>
        /// <returns>status code</returns>
        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(
            [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method CreateEmployee");

            var employee = _mapper.Map<Employee>(employeeDto);

            var department = _repository
                .GetIdDepartment(employeeDto.DepartmentCode);

            employee.Department = department;
            employee.IdDepartment = department.Id;

            if (!_repository.CreateEmployee(employee))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        /// <summary>
        /// Create a new employee Async
        /// </summary>
        /// <param name="employeeDto">dto model Employee</param>
        /// <returns>status code</returns>
        [HttpPost("CreateEmployeeAsync")]
        public async Task<IActionResult> CreateEmployeeAsync(
            [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method CreateEmployeeAsync");

            var employee = _mapper.Map<Employee>(employeeDto);

            var department = _repository
                .GetIdDepartment(employeeDto.DepartmentCode);

            employee.Department = department;
            employee.IdDepartment = department.Id;

            if (!await _repository.CreateEmployeeAsync(employee))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        /// <summary>
        /// Delete employee by code
        /// </summary>
        /// <param name="employeeCode">code employee</param>
        /// <returns>status code</returns>
        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(
            [FromBody] int employeeCode)
        {
            if (!_repository.EmployeeExists(employeeCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method DeleteEmployee");

            if (!_repository.DeleteEmployee(employeeCode) || !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Delete employee by code Async
        /// </summary>
        /// <param name="employeeCode">code employee</param>
        /// <returns>status code</returns>
        [HttpDelete("DeleteEmployeeAsync")]
        public async Task<IActionResult> DeleteEmployeeAsync(
            [FromBody] int employeeCode)
        {
            if (!await _repository.EmployeeExistsAsync(employeeCode))
                return NotFound();

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method DeleteEmployeeAsync");

            if (!await _repository.DeleteEmployeeAsync(employeeCode) ||
                !ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Something went wrong deleting institution");
                return BadRequest(ModelState);
            }

            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Update Employee model
        /// </summary>
        /// <param name="employeeDto">dto model employee</param>
        /// <returns>status code</returns>
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(
            [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method UpdateEmployee");

            var employee = _mapper.Map<Employee>(employeeDto);
            if (!_repository.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        /// <summary>
        /// Update Employee model Async
        /// </summary>
        /// <param name="employeeDto">dto model Employee</param>
        /// <returns>status code</returns>
        [HttpPut("UpdateEmployeeAsync")]
        public async Task<IActionResult> UpdateEmployeeAsync(
            [FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            _logger.LogInformation($"ModelState {ModelState}, " +
                $"method UpdateEmployeeAsync");

            var employee = _mapper.Map<Employee>(employeeDto);
            if (!await _repository.UpdateEmployeeAsync(employee))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }
    }
}
