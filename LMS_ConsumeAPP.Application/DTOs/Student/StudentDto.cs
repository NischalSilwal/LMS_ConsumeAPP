using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.DTOs.Student
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Department { get; set; }
    }
}
