using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WalkApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkApiController : ControllerBase
    {
        
        //Get:https://localhost:7204/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] StudentName = new string[] { "yash", "dog", "Alex", "Ron" };
            return Ok(StudentName);
        }
    }
}
