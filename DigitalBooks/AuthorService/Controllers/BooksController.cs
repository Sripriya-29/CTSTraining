﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorService.Models;
using Microsoft.AspNetCore.Authorization;


namespace AuthorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly DigitalBooksContext _context;

        public BooksController(DigitalBooksContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        [HttpGet]
        [Route("GetauthorBook")]
        public async Task<ActionResult<IEnumerable<Book>>> GetauthorBook(string uid)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Where(x => x.UserId.ToString() == uid).ToListAsync();
            if (book == null)
            {
                return NotFound();
            }
            //else
            //{
            //    foreach (var item in book)
            //    {
            //        item.User.Books = null;
            //    }
            //}
            return book;
        }
        [HttpPut]
        [Route("ubook")]
        public async Task<IActionResult> ubook(int id, string content)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            if (!BookExists(id))
            {
                return NotFound();
            }



            try
            {
                Book b = await _context.Books.Where(x => x.BookId == id).SingleOrDefaultAsync();



                if (b != null)
                {
                    b.Content = content;
                    _context.Entry(b).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return CreatedAtAction("GetBook", new { id = b.BookId }, b);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }



            return NoContent();
        }
        [HttpPut]
        [Route("Deactivebook")]
        public async Task<IActionResult> Deactivebook(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            if (!BookExists(id))
            {
                return NotFound();
            }



            try
            {
                Book b = await _context.Books.Where(x => x.BookId == id).SingleOrDefaultAsync();

                if (b != null)
                {
                    b.Active = !b.Active;
                    _context.Entry(b).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return CreatedAtAction("GetBook", new { id = b.BookId }, b);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }



            return NoContent();
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

         //   _context.Entry(book).State = EntityState.Modified;

            try
            {
               var DbBook= _context.Books.FirstOrDefault(x => x.BookId.Equals(id));  
               
                DbBook.BookName = book.BookName;    
                DbBook.Price = book.Price;
                DbBook.Publisher = book.Publisher;
                DbBook.Content=book.Content;    
                DbBook.Active=book.Active;  

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
       
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            bool IsExist = _context.Books.Any(x => x.BookName.Equals(book.BookName));
         
          if (_context.Books == null || IsExist == true)
          {
              return Problem("Entity set 'DigitalBooksContext.Books'  is null.");
          }

           
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
