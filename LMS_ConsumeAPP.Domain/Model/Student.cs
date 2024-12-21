using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Domain.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Department { get; set; }
    }
}
