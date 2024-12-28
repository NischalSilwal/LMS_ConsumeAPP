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

        public async Task<IActionResult> IndexAuthor()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync(); // Ensure this returns a collection
            return View(authors); // authors should be IEnumerable<AuthorDto> or List<AuthorDto>
        }


        public async Task<IActionResult> DetailsAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return View(author);
        }

        // Create GET
        public IActionResult CreateAuthor()
        {
            var authorDto = new AuthorDto
            {
                BookIds = new List<int>()
            };
            return View(authorDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
        {
            try
            {
                // Initialize BookIds if null
                authorDto.BookIds ??= new List<int>();

                // Remove BookIds validation errors since it's optional
                ModelState.Remove("BookIds");

                if (ModelState.IsValid)
                {
                    var authorId = await _authorRepository.AddAuthorAsync(authorDto);
                    return RedirectToAction(nameof(IndexAuthor));
                }
                return View(authorDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while creating the author.");
                return View(authorDto);
            }
        }
        //// Create POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _authorRepository.AddAuthorAsync(authorDto);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(authorDto);
        //}

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuthor(int id, AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = await _authorRepository.UpdateAuthorAsync(id, authorDto);
            if (success)
            {
                ViewBag.msg = "Author updated successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.msg = "Error updating Author!";
            return View(authorDto);
           
        }


      
        // Delete GET
        public async Task<IActionResult> DeleteAuthor(int id)
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
