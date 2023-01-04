using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WebApiPrac1.Repository;

namespace WebApiPrac1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //Show All Data
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
        }

        //Find Data Using Id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Employee>> GetEmployees(int Id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployees(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
        }

        // Create Employee or Add Data
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var Created = await _employeeRepository.AddEmployees(employee);
                return CreatedAtAction(nameof(GetEmployees), new { Id = Created.Id }, Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
            return Ok();
        }

        // Edit Employee Data or Edit Data
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Employee>> EditEmployee(int Id, Employee employee)
        {
            try
            {
                if (Id != employee.Id)
                {
                    return BadRequest("Mismathed Id");
                }
                var EmployeeUpdate = await _employeeRepository.GetEmployees(Id);
                if (EmployeeUpdate == null)
                {
                    return NotFound($"Employee Id ={Id} not found");
                }
                return await _employeeRepository.UpdateEmployees(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
        }

        //Delete Data
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int Id)
        {
            try
            {
                var EmployeeDelete = await _employeeRepository.GetEmployees(Id);
                if (EmployeeDelete == null)
                {
                    return NotFound($"Employee Id ={Id} not found");
                }
                return await _employeeRepository.DeleteEmployees(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
        }

        //Search Data
        [HttpGet("{SearchName}/{City}")]
        public async Task<ActionResult<Employee>> SearchEmployee([FromRoute] String SearchName,string City)
        {
            try
            {
                var SearchResult = await _employeeRepository.SearchEmployee(SearchName);
                SearchResult = SearchResult.Where(x => x.City == City);
                if (SearchResult.Any())
                {
                    return Ok(SearchResult);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in retrieving data form Database");
            }
        }

    }
}
