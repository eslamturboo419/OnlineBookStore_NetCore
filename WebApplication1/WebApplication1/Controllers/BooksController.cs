using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private readonly OnlineBookCoreDBContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public BooksController(OnlineBookCoreDBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        public async Task<IActionResult> Catalogue()
        {
            var val = await _context.Books.ToListAsync();
            return View(val);
        }

        /// Show Orders From Users
        [HttpGet]
        public IActionResult ShowOrder()
        {
            return View();
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, Book book)
        {
            if (ModelState.IsValid)
            {

                var fileName = string.Empty;
                if (file != null)
                {
                    var upload = Path.Combine(hostingEnvironment.WebRootPath, "Images"); /// to combine wwwRoot and folder

                    fileName =/* Guid.NewGuid().ToString() + */ "_" + file.FileName;
                    var fullPath = Path.Combine(upload, fileName);

                    file.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                book.Imgfile = fileName;



                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (file != null)
                    {
                           var fileName = string.Empty;
                       
                            var upload = Path.Combine(hostingEnvironment.WebRootPath, "Images"); /// to combine wwwRoot and folder

                            fileName = /*Guid.NewGuid().ToString() +*/ "_" + file.FileName;
                            var fullPath = Path.Combine(upload, fileName);

                            file.CopyTo(new FileStream(fullPath, FileMode.Create));
        
                        book.Imgfile = fileName;
                    }


                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
