using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCore3_1.API.Helpers;
using NetCore3_1.Data.Contexts;
using NetCore3_1.Models.DTOs;
using NetCore3_1.Models.Entities;

namespace NetCore3._1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly WebApiDbContext context;
        private readonly ILogger<AuthorsController> logger;
        private readonly IMapper mapper;

        public AuthorsController(WebApiDbContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            this.logger = logger;
        }


        [HttpGet]
        [ServiceFilter(typeof(CustomActionFilter))]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            logger.LogInformation("GetAuthors()");
            var authors = await context.Authors.Include(x => x.Books).ToListAsync();

            return mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            logger.LogInformation($"GetAuthor({id})");
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(author => author.Id == id);

            if (author == null)
            {
                logger.LogWarning($"WARNING: Author {id} not found");
                return NotFound();
            }

            return mapper.Map<AuthorDTO>(author);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, mapper.Map<AuthorDTO>(author));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            var author = mapper.Map<Author>(authorDTO);

            if (author.Id != id) return BadRequest();

            context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> DeleteAuthor(int id)
        {
            var author = await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(author => author.Id == id);

            if (author == null) return NotFound();

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return mapper.Map<AuthorDTO>(author);
        }
    }
}
