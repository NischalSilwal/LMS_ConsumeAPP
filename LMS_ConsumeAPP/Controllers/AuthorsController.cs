using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                BookIds = new List<int>() // Ensure this is initialized
            };
            return View(authorDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(AuthorDto authorDto, string formInputBookIds)
        {
            //  var json = JsonConvert.SerializeObject(authorDto);

            try
            {
                // Parse book IDs
                if (!string.IsNullOrWhiteSpace(formInputBookIds))
                {
                    authorDto.BookIds = formInputBookIds
                                        .Split(',')
                                        .Select(id => int.TryParse(id, out int result) ? result : throw new FormatException())
                                        .ToList();
                }
                else
                {
                    authorDto.BookIds = new List<int>();
                }
                var json = JsonConvert.SerializeObject(authorDto);
                // Add author via repository
                var success = await _authorRepository.AddAuthorAsync(authorDto);
                if (success)
                {
                    ViewBag.SuccessMessage = "Author created successfully!";
                    return RedirectToAction("IndexAuthor");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create author. Please try again.";
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                ModelState.AddModelError("", "An error occurred while creating the author.");
            }


            return View(authorDto);
        }

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
        public async Task<IActionResult> EditAuthor(AuthorDto authorDto, string formInputBookIds)
        {
            try
            {
                // Parse the comma-separated book IDs from user input
                if (!string.IsNullOrWhiteSpace(formInputBookIds))
                {
                    authorDto.BookIds = formInputBookIds.Split(',').Select(int.Parse).ToList();
                }
                else
                {
                    authorDto.BookIds = new List<int>();
                }

                var success = await _authorRepository.UpdateAuthorAsync(authorDto.AuthorId, authorDto);
                if (success)
                {
                    ViewBag.msg = "Author updated successfully!";
                    return RedirectToAction("IndexAuthor");
                }

                ViewBag.msg = "Error updating Author!";
                return View(authorDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while updating the author.");
                return View(authorDto);
            }
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
