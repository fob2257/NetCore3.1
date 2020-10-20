using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore3_1.Data.Contexts;
using NetCore3_1.Models.DTOs;
using NetCore3_1.Models.Entities;

namespace NetCore3._1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly WebApiDbContext context;
        private readonly IMapper mapper;

        public BooksController(WebApiDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await context.Books.ToListAsync();

            return mapper.Map<List<BookDTO>>(books);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null) return NotFound();

            return mapper.Map<BookDTO>(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] BookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetBook", new { id = book.Id }, mapper.Map<BookDTO>(book));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);

            if (book.Id != id) return BadRequest();

            context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> DeleteBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null) return NotFound();

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return mapper.Map<BookDTO>(book);
        }
    }
}
