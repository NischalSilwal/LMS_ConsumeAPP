using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Repositories.TransactionRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<int> AddTransactionAsync(AddTransactionViewModel model);
        Task<bool> UpdateTransactionAsync(int id, AddTransactionViewModel model);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
