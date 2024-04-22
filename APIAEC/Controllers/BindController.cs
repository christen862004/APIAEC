using APIAEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        [HttpGet("{Id}/{Name}/{ManagerName}")]
        //api/Bind/1/weq/eqw
        public IActionResult GetAll([FromRoute] Department dept, int Id, string Name)
        {
            //
            return Ok();
        }

        [HttpPost]
        public IActionResult AddDept([FromBody] int id)
        {
            return Ok();
        }
 
    }
}
