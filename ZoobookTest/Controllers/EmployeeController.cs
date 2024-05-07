using BLL.IServices;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels;
using System.Net;

namespace ZoobookTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeVM>>> Get()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeVM>> Get(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

 
        [HttpPost]
        public async Task<ActionResult<EmployeeVM>> Post([FromBody] EmployeeVM EmployeeVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var addedEmployeeId = await _employeeService.AddAsync(EmployeeVM);
            if (addedEmployeeId > 0)
                return Ok(new { result = addedEmployeeId, message = "Employeee added.", HttpStatusCode.OK });
            else
                return Ok(new { result = addedEmployeeId, message = "Request Failed.", HttpStatusCode.InternalServerError });

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeVM>> Put(int id, [FromBody] EmployeeVM EmployeeVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updatedEmployeeId = await _employeeService.UpdateAsync(id, EmployeeVM);
            if (updatedEmployeeId == 0)
            {
                return Ok(new { result = updatedEmployeeId, message = "Employee not found.", HttpStatusCode.NotFound });
            }
            return Ok(new { result = updatedEmployeeId, message = "Employeee udpated.", HttpStatusCode.OK });
        }

      
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
