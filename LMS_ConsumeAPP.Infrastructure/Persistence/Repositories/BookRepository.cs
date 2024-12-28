using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.Book;
using LMS_ConsumeAPP.Domain.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly HttpClient _client;
        private readonly string BaseURL = "https://localhost:7288/api/";

        public BookRepository(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(BaseURL);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var response = await _client.GetAsync("Books");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Book>>(result);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Books/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookDto>(result);
            }
            return null;
        }

        public async Task<bool> AddBookAsync(BookDto bookDto)
        {
            var json = JsonConvert.SerializeObject(bookDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Books/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(int id, BookDto bookDto)
        {
            var json = JsonConvert.SerializeObject(bookDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"Books/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _client.DeleteAsync($"Books/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
