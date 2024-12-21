using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Repositories.Book
{
    public interface IBookRepository
    {
        Task<IEnumerable<LMS_ConsumeAPP.Domain.Model.Book>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<bool> AddBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
