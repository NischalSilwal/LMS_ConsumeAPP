using LMS_ConsumeAPP.Application.Interface.Repositories.TransactionRepository;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7288/api/Transactions";

        public TransactionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Transaction>>(_baseUrl);
            return response;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Transaction>($"{_baseUrl}/{id}");
            return response;
        }

        public async Task<int> AddTransactionAsync(AddTransactionViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateTransactionAsync(int id, AddTransactionViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
