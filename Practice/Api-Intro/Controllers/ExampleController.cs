using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Intro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<string> students = new List<string>() { "Agshin", "Semed", "Ilham", "Tunzale" };
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId([FromRoute] int id)
        {
            return Ok(id + "- this is ID");
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string searchText)
        {
            return Ok(searchText);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            return Ok(user.Surname + "-" + user.Name);
        }
    }
}
