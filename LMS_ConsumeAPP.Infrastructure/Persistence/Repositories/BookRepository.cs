using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Repositories.Book;

using LMS_ConsumeAPP.Domain.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
            var response = await _client.GetAsync("Book");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Book>>(result);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Book/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookDto>(result);
            }
            return null;
        }

        public async Task<bool> AddBookAsync(BookDto bookDto)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(bookDto.Title), "Title" },
                { new StringContent(bookDto.Genre), "Genre" },
                { new StringContent(bookDto.ISBN), "ISBN" },
                { new StringContent(bookDto.Quantity.ToString()), "Quantity" },
                { new StringContent(string.Join(",", bookDto.Authors)), "Authors" }
            };

            var response = await _client.PostAsync("Book/", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(int id, BookDto bookDto)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(bookDto.Title), "Title" },
                { new StringContent(bookDto.Genre), "Genre" },
                { new StringContent(bookDto.ISBN), "ISBN" },
                { new StringContent(bookDto.Quantity.ToString()), "Quantity" },
                { new StringContent(string.Join(",", bookDto.Authors)), "Authors" }
            };

            var response = await _client.PutAsync($"Book/{id}", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _client.DeleteAsync($"Book/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
