using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCore3_1.API.Helpers;
using NetCore3_1.Data.Contexts;
using NetCore3_1.Models.Entities;

namespace NetCore3._1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly WebApiDbContext context;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(WebApiDbContext context, ILogger<AuthorsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }


        [HttpGet]
        [ServiceFilter(typeof(CustomActionFilter))]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            logger.LogInformation("GetAuthors()");
            return await context.Authors.Include(x => x.Books).ToListAsync();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            logger.LogInformation($"GetAuthor({id})");
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(author => author.Id == id);

            if (author == null)
            {
                logger.LogWarning($"WARNING: Author {id} not found");
                return NotFound();
            }

            return author;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody] Author author)
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (author.Id != id) return BadRequest();

            context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return NotFound();

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return author;
        }
    }
}
