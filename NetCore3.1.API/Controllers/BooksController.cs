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
    public class BooksController : ControllerBase
    {
        private readonly WebApiDbContext context;

        public BooksController(WebApiDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return context.Books.ToList();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = context.Books.FirstOrDefault(book => book.Id == id);

            if (book == null) return NotFound();

            return book;
        }

        [HttpPost]
        public ActionResult CreateBook([FromBody] Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();

            return new CreatedAtRouteResult("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (book.Id != id) return BadRequest();

            context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Book> DeleteBook(int id)
        {
            var book = context.Books.FirstOrDefault(book => book.Id == id);

            if (book == null) return NotFound();

            context.Books.Remove(book);
            context.SaveChanges();

            return book;
        }
    }
}
