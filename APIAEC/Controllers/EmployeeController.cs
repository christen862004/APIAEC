using APIAEC.DTO;
using APIAEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAEC.Controllers
{
    [Route("api/[controller]")]//api/Employee
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIContext context;

        public EmployeeController(ITIContext context)
        {
            this.context = context;
        }
        [HttpGet("{id:int}")]
        //url/api/Employee/1
        public IActionResult Details(int id)
        {
            var emp=  context.Employee.Include(e => e.Department)
                .Where(e=>e.Id==id)
                .Select(e=>new EmployeeWithDEptDto()
                {
                    EmpId=e.Id,
                    EmpName=e.Name,
                    DeptName=e.Department.Name
                }).FirstOrDefault();
            return Ok(emp);
        }
    }
}
