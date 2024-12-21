using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ConsumeAPP.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync(); // Ensure this returns a collection
            return View(authors); // authors should be IEnumerable<AuthorDto> or List<AuthorDto>
        }


        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorDto authorDto)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.AddAuthorAsync(authorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(authorDto);
        }

        //// Edit GET
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var author = await _authorRepository.GetAuthorByIdAsync(id);
        //    if (author == null)
        //        return NotFound();

        //    // Pass the author data and set IsEdit flag to true
        //    ViewBag.IsEdit = true;
        //    ViewBag.Author = author;
        //    return View(author);
        //}

        //// Edit POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, AuthorDto authorDto)
        //{
        //    if (id != authorDto.AuthorId || !ModelState.IsValid)
        //        return BadRequest();

        //    await _authorRepository.UpdateAuthorAsync(id, authorDto);
        //    return RedirectToAction(nameof(Index));
        //}
        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            // Pass the author data and set IsEdit flag to true
            ViewBag.IsEdit = true;
            ViewBag.Author = author;
            return View(author);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorDto authorDto)
        {
            if (id != authorDto.AuthorId || !ModelState.IsValid)
                return BadRequest();

            await _authorRepository.UpdateAuthorAsync(id, authorDto);
            return RedirectToAction(nameof(Index));
        }

        // Delete GET
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
