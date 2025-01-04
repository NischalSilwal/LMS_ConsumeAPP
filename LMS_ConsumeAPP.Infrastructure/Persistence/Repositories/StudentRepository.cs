using LMS_ConsumeAPP.Application.DTOs.BookDTO;
using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Application.Interface.Repositories.StudentRepository;
using LMS_ConsumeAPP.Domain.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly HttpClient _client;
        private readonly string BaseURL = "https://localhost:7288/api/";

        public StudentRepository(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(BaseURL);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var response = await _client.GetAsync("Student");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Student>>(result);
            }
            return Enumerable.Empty<Student>();
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Student/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StudentDto>(result);
            }
            return null;
        }

        public async Task<bool> AddStudentAsync(AddStudentDto addStudentDto)
        {
            //var form = new MultipartFormDataContent
            //{
            //    { new StringContent(addStudentDto.Name), "Name" },
            //    { new StringContent(addStudentDto.Email), "Email" },
            //    { new StringContent(addStudentDto.ContactNumber), "ContactNumber" },
            //    { new StringContent(addStudentDto.Department), "Department" }
            //};

            //var response = await _client.PostAsync("Student", form);
            //return response.IsSuccessStatusCode;

            var json = JsonConvert.SerializeObject(addStudentDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Student/", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentDto addStudentDto)
        {
            //        var form = new MultipartFormDataContent
            //{
            //    { new StringContent(addStudentDto.Name), "Name" },
            //    { new StringContent(addStudentDto.Email), "Email" },
            //    { new StringContent(addStudentDto.ContactNumber ?? string.Empty), "ContactNumber" },  // Ensure nulls are handled
            //    { new StringContent(addStudentDto.Department ?? string.Empty), "Department" }
            //};

            //        // Assuming the API route is /api/Student/{id}, ensure it is correct
            //        var response = await _client.PutAsync($"api/Student/{id}", form);
            var json = JsonConvert.SerializeObject(addStudentDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"Student/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteStudentAsync(int id)
        {
            var response = await _client.DeleteAsync($"Student/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
