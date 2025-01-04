using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Repositories
{
    using LMS_ConsumeAPP.Application.DTOs.BookDTO;
    using LMS_ConsumeAPP.Domain.Model;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;

    public class AuthorRepository : IAuthorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7288/api/Authors";

        public AuthorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AuthorDto>>(_baseUrl);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AuthorDto>($"{_baseUrl}/{id}");
        }

        public async Task<bool> AddAuthorAsync(AuthorDto authorDto)
        {
            var json = JsonConvert.SerializeObject(authorDto);
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, authorDto);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAuthorAsync(int id, AuthorDto authorDto)
        {
            var json = JsonConvert.SerializeObject(authorDto);
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", authorDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
