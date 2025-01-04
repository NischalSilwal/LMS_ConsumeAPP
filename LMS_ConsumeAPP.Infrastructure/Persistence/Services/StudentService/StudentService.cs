using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Application.Interface.Repositories.StudentRepository;
using LMS_ConsumeAPP.Application.Interface.Services.StudentService;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<bool> AddStudentAsync(AddStudentDto addStudentDto)
        {
            return await _studentRepository.AddStudentAsync(addStudentDto);
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentDto StudentDto)
        {
            return await _studentRepository.UpdateStudentAsync(id, StudentDto);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

    
    }
}
