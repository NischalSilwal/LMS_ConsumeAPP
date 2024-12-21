using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Services.BookServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookCreateDto book);
        Task<bool> UpdateBookAsync(int id, BookUpdateDto book);
        Task<bool> DeleteBookAsync(int id);
    }
}
