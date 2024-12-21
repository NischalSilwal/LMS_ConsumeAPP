using LMS_ConsumeAPP.Application.Interface.Services.TransactionService;
using LMS_ConsumeAPP.Domain.Model;
using LMS_ConsumeAPP.Infrastructure.Persistence.Services.TransactionServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ConsumeAPP.Controllers
{
    
        public class TransactionsController : Controller
        {
            private readonly ITransactionService _transactionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsController(ITransactionService transactionService, IHttpContextAccessor httpContextAccessor)
        {
            _transactionService = transactionService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> GetAllTransaction()
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var transactions = await _transactionService.GetAllTransactionsAsync();
                return View(transactions);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(AddTransactionViewModel model)
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
                {
                    await _transactionService.AddTransactionAsync(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }

            public async Task<IActionResult> Edit(int id)
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                var model = new AddTransactionViewModel
                {
                    StudentId = transaction.StudentId,
                    UserId = transaction.UserId,
                    BookId = transaction.BookId,
                    TransactionType = transaction.TransactionType,
                    Date = transaction.Date
                };

                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, AddTransactionViewModel model)
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
                {
                    var success = await _transactionService.UpdateTransactionAsync(id, model);
                    if (success)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }

            public async Task<IActionResult> Delete(int id)
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return View(transaction);
            }

            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            await _transactionService.DeleteTransactionAsync(id);
                return RedirectToAction("Index");
            }
        }
    
}
