using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCore3._1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private static readonly string[] Summaries = new string[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Summaries;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"Value: {id}";
        }

        [HttpPost()]
        public ActionResult<string> Get([FromBody] string value)
        {
            return $"Value: {value}";
        }
    }
}
