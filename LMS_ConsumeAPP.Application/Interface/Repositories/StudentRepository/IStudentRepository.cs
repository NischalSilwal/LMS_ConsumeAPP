﻿using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<bool> AddStudentAsync(AddStudentDto addStudentDto);
        Task<bool> UpdateStudentAsync(int id, StudentDto StudentDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
