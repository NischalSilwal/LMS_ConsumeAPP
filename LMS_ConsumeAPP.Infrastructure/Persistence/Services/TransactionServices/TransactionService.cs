using LMS_ConsumeAPP.Application.Interface.Repositories.TransactionRepository;
using LMS_ConsumeAPP.Application.Interface.Services.TransactionService;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }

        public async Task<int> AddTransactionAsync(AddTransactionViewModel model)
        {
            return await _transactionRepository.AddTransactionAsync(model);
        }

        public async Task<bool> UpdateTransactionAsync(int id, AddTransactionViewModel model)
        {
            return await _transactionRepository.UpdateTransactionAsync(id, model);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            return await _transactionRepository.DeleteTransactionAsync(id);
        }
    }
}
