using LMS_ConsumeAPP.Application.DTOs.AuthorDTO;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(int id);
        Task<bool> AddAuthorAsync(AuthorDto authorDto);
        Task<bool> UpdateAuthorAsync(int id, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
