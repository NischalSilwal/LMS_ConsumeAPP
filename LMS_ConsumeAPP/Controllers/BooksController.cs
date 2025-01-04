using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.Book;
using LMS_ConsumeAPP.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            var json = JsonConvert.SerializeObject(bookDto);
            if (ModelState.IsValid)
            {
                

                // Call the repository method to add the book (this will use the stored procedure)
                var success = await _bookRepository.AddBookAsync(bookDto);

                if (success)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to the book list after successful add
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add the book.");
                    return View(bookDto);
                }
            }

            return View(bookDto);
        }

        // GET: Books/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookDto bookDto)
        {
            //if (id != bookDto.BookId)
            //{
            //    return NotFound();
            //}
            var json = JsonConvert.SerializeObject(bookDto);



            var isUpdated = await _bookRepository.UpdateBookAsync(bookDto.BookId, bookDto);
                if (isUpdated)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "An error occurred while updating the book.");
            
            return View(bookDto);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _bookRepository.DeleteBookAsync(id);
            if (isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

       
    }
}
