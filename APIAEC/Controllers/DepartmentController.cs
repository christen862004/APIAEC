using APIAEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllDEpartment()//wrap response
        {
            List<Department> deptList= context.Department.ToList();
            return Ok(deptList);
        }
        //view- content -file -json
        //respons :OK (200) - create (201) -unauthorize(401) - notFound(404)
        //OkREult











        [HttpGet]
        [Route("{id:int}")]//api/department/1
        public IActionResult GetByID(int id)
        {
            Department dept=context.Department.FirstOrDefault(d=>d.Id==id);
            if(dept==null)
            {
                return NotFound("Invalid Id");
            }
            return Ok(dept);
        }

        [HttpGet]//api/department/sd
        [Route("{name:alpha}")]//api/departmet/sd
        public Department findByName(string name)
        {
            Department dept = context.Department.FirstOrDefault(d => d.Name == name);
            return dept;
        }

        [HttpPost]
        //api/department :POST  {id:1,name:asd}
        public IActionResult AddDept(Department dept)
        {
            context.Add(dept);
            context.SaveChanges();
            return Created($"http://localhost:5091/api/department/{dept.Id}", dept);//get byid
        }
       
        [HttpPut]//api/Department/1 :put (id) object (req body)
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Remove(int id)
        {
            try
            {
                Department dept = context.Department.FirstOrDefault(d => d.Id == id);
                context.Department.Remove(dept);
                context.SaveChanges();
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
