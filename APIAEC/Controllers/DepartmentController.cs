using APIAEC.DTO;
using APIAEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIAEC.Controllers
{
    [Route("api/[controller]")] //api/Department :post
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ITIContext context;

        public DepartmentController(ITIContext context)
        {
            this.context = context;
        }
        //method name the same verb
        [HttpGet]//api/department:get
        public ActionResult<GeneralResponse> GetAllDEpartment()//wrap response
        {
            List<DEpartmentDto> deptList= context.Department
                .Select(d=>
                new DEpartmentDto() {DeptID=d.Id,DeptName=d.Name })
                .ToList();
            GeneralResponse response = new GeneralResponse();
            response.IsPass = true;
            response.Data = deptList;
            //mapping
            return response;
        }

        [HttpGet("emp")]//api/department/emps
        public IActionResult GetAllDEpartmentWithEmp()
        {
            List<DeptWithEmpsDto> depts= 
                context.Department.Include(d => d.Employees).Select(
                    d=>new DeptWithEmpsDto()
                    {
                        DeptName=d.Name,
                        DeptId=d.Id,
                        Employees=d.Employees.Select(e=>e.Name).ToList()
                    }).ToList();
            return Ok(depts);
        }

        //view- content -file -json
        //respons :OK (200) - create (201) -unauthorize(401) - notFound(404)
        //OkREult
        [HttpGet("{id:int}",Name ="GetDeptByIDRoute")]//api/department/1
        public IActionResult GetByID(int id)
        {
            Department dept=context.Department.FirstOrDefault(d=>d.Id==id);
            if(dept==null)
            {
                return NotFound("Invalid Id");
            }
            return Ok(dept);
        }

        [HttpGet("{name:alpha}")]//api/department/sd
        public Department findByName(string name)
        {
            Department dept = context.Department.FirstOrDefault(d => d.Name == name);
            return dept;
        }

        [HttpPost]
        //api/department :POST  {id:1,name:asd}
        public IActionResult AddDept(Department dept)
        {
            if (ModelState.IsValid == true)
            {
                context.Add(dept);
                context.SaveChanges();
                string url = Url.Link("GetDeptByIDRoute", new { id = dept.Id });
                return Created(url, dept);//get byid
            }
            return BadRequest(ModelState);
        }
       
        [HttpPut("{id:int}")]//api/Department/1 :put (id) object (req body)
       // public IActionResult Update([FromRoute]int id,[FromBody]Department deptfromRequest)
        public IActionResult Update(int id,Department deptfromRequest)
        {
            Department deptFromDb = context.Department.FirstOrDefault(d => d.Id == id);
            if(deptFromDb==null)
            {
                return NotFound("id not found");
            }
            
            //if (id == deptfromRequest.Id)
            //{
            deptFromDb.Name = deptfromRequest.Name;
            deptFromDb.ManagerName = deptfromRequest.ManagerName;
            context.SaveChanges();
            return NoContent();//
            //}
        }
        [HttpDelete("{id:int}")]
        public ActionResult<GeneralResponse> Remove(int id)
        {
            GeneralResponse response = new GeneralResponse();
            try
            {
                Department dept = context.Department.FirstOrDefault(d => d.Id == id);
                context.Department.Remove(dept);
                context.SaveChanges();
                response.IsPass = true;
                response.Data = "Object remove";
                //return NoContent();
                return response;
            }catch(Exception ex)
            {
                response.IsPass = false;
                response.Message = ex.Message;
                //return BadRequest(ex.Message);
                return response;
            }
        }
    }
}
