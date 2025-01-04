using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Application.Interface.Services.StudentService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMS_ConsumeAPP.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentController(IStudentService studentService, IHttpContextAccessor httpContextAccessor)
        {
            _studentService = studentService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentDto studentDTO)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var success = await _studentService.AddStudentAsync(studentDTO);
            if (success)
            {
                ViewBag.msg = "Student added successfully!";
                return RedirectToAction("GetAllStudents");
            }

            ViewBag.msg = "Error adding student!";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var studentDto = await _studentService.GetStudentByIdAsync(id);
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            //var addStudentDto = new AddStudentDto
            //{
            //    Name = studentDto.Name,
            //    Email = studentDto.Email,
            //    ContactNumber = studentDto.ContactNumber,
            //    Department = studentDto.Department
            //};

            return View(studentDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentDto studentDto)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.msg = "Invalid input";
            //    return View(addStudentDto);
            //}
            var json = JsonConvert.SerializeObject(studentDto);
            var success = await _studentService.UpdateStudentAsync(studentDto.StudentId, studentDto);
            if (success)
            {
                ViewBag.msg = "Student updated successfully!";
                return RedirectToAction("GetAllStudents"); // Redirect here after success
            }

            ViewBag.msg = "Error updating student!";
            return View(studentDto);
        }

       
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var success = await _studentService.DeleteStudentAsync(id);
            if (success)
            {
                ViewBag.msg = "Student deleted successfully!";
            }
            else
            {
                ViewBag.msg = "Error deleting student!";
            }

            return RedirectToAction("GetAllStudents");
        }
    }
}