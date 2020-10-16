using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore3_1.Data.Contexts;
using NetCore3_1.Models.Entities;

namespace NetCore3._1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly WebApiDbContext context;

        public AuthorsController(WebApiDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return context.Authors.ToList();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = context.Authors.FirstOrDefault(author => author.Id == id);

            if (author == null) return NotFound();

            return author;
        }

        [HttpPost]
        public ActionResult CreateAuthor([FromBody] Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();

            return new CreatedAtRouteResult("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id, [FromBody] Author author)
        {
            if (author.Id != id) return BadRequest();

            context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Author> DeleteAuthor(int id)
        {
            var author = context.Authors.FirstOrDefault(author => author.Id == id);

            if (author == null) return NotFound();

            context.Authors.Remove(author);
            context.SaveChanges();

            return author;
        }
    }
}
