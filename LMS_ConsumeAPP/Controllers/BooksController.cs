using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository;
using LMS_ConsumeAPP.Application.Interface.Services.BookServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorRepository _authorRepository;

    public BooksController(IBookService bookService, IAuthorRepository authorRepository)
    {
        _bookService = bookService;
        _authorRepository = authorRepository;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBooksAsync();
        return View(books);
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // GET: Books/Create
    public async Task<IActionResult> Create()
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        ViewBag.AuthorList = new SelectList(authors, "AuthorId", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateDto bookCreateDto)
    {
        if (ModelState.IsValid)
        {
            // Ensure AuthorIds is not null or empty
            if (bookCreateDto.AuthorIds == null || !bookCreateDto.AuthorIds.Any())
            {
                ModelState.AddModelError("", "At least one author must be selected.");
                var authors_ = await _authorRepository.GetAllAuthorsAsync();
                ViewBag.AuthorList = new SelectList(authors_, "AuthorId", "Name");
                return View(bookCreateDto);
            }

            var bookId = await _bookService.AddBookAsync(bookCreateDto);
            return RedirectToAction(nameof(Details), new { id = bookId });
        }

        var authors = await _authorRepository.GetAllAuthorsAsync();
        ViewBag.AuthorList = new SelectList(authors, "AuthorId", "Name");
        return View(bookCreateDto);
    }


    // GET: Books/Edit/5
    public async Task<IActionResult> Edit(int id)
    
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        // Manually map book's authors to author IDs
        var authorIds = book.Authors.Select(a => a.AuthorId).ToList();

        var bookUpdateDto = new BookUpdateDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Genre = book.Genre,
            ISBN = book.ISBN,
            Quantity = book.Quantity,
            AuthorIds = authorIds
        };

        var authors = await _authorRepository.GetAllAuthorsAsync();
        ViewBag.AuthorList = new SelectList(authors, "AuthorId", "Name", bookUpdateDto.AuthorIds);
        return View(bookUpdateDto);
    }

    // POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BookUpdateDto bookUpdateDto)
    {
       

            var success = await _bookService.UpdateBookAsync(id, bookUpdateDto);
            if (success)
            {
                ViewBag.msg = "Book updated successfully!";
                return RedirectToAction("Index");
            }
       
        return NotFound();
    }

    // GET: Books/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _bookService.DeleteBookAsync(id);
        if (success)
        {
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }
}
