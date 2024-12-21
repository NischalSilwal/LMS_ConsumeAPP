using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Services.StudentService
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<bool> AddStudentAsync(AddStudentDto addStudentDto);
        Task<bool> UpdateStudentAsync(int id, AddStudentDto addStudentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
