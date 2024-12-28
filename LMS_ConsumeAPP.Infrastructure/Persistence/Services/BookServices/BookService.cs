using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.Interface.Services.BookServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        // Constructor with HttpClient injection and BaseAddress set to the API endpoint
        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7288/api/"); // Set the base URL for API requests
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<BookDto>>("Books");
                return response ?? new List<BookDto>();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching books", ex);
            }
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Books/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var book = await response.Content.ReadFromJsonAsync<BookDto>();
                    return book;
                }
                else
                {
                    throw new Exception($"Failed to retrieve book with ID {id}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching book with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> AddBookAsync(BookCreateDto bookCreateDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Books", bookCreateDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<int>();
                    return result;
                }

                throw new Exception("Failed to add new book. Please try again.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding a new book: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateBookAsync(int id, BookUpdateDto bookDto)
        {
           // id = 2;
            string authorIdsString = string.Join(",", bookDto.AuthorIds);
            var form = new MultipartFormDataContent
            {
                { new StringContent(bookDto.BookId.ToString()), "BookId" },
                { new StringContent(bookDto.Title), "Title" },
                { new StringContent(bookDto.Genre), "Genre" },
                { new StringContent(bookDto.ISBN), "ISBN" },
                { new StringContent(bookDto.Quantity.ToString()), "Quantity" },
                { new StringContent(authorIdsString), "AuthorIds" }
            };

            

            var response = await _httpClient.PutAsync($"Books/{id}", form);
            return response.IsSuccessStatusCode;
        //    try
        //    {
               
        //        // Payload object to send in the PUT request
        //        var payload = new
        //        {
        //            BookId = id,
        //            Title = bookDto.Title,
        //            Genre = bookDto.Genre,
        //            ISBN = bookDto.ISBN,
        //            Quantity = bookDto.Quantity,
        //            AuthorIds = bookDto.AuthorIds
        //        };

        //        var response = await _httpClient.PutAsJsonAsync($"Books/{id}", payload);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var errorDetails = await response.Content.ReadAsStringAsync();
        //            throw new Exception($"Failed to update book. Status Code: {response.StatusCode}. Details: {errorDetails}");
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"An error occurred while updating the book with ID {id}: {ex.Message}", ex);
        //    }
        }


        public async Task<bool> DeleteBookAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Books/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the book with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
