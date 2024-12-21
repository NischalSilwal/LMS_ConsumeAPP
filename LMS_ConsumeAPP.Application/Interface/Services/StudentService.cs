using LMS_ConsumeAPP.Application.DTOs.Student;
using LMS_ConsumeAPP.Domain.Model;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Application.Interface.Services
{
    public class StudentRepositorys
    {
        private readonly string _baseUrl = "https://localhost:7288/api/Student";
        private readonly HttpClient _httpClient;

        public StudentRepositorys(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Student>>(json);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Student>(json);
        }

        public async Task<int> AddStudentAsync(Student student)
        {
            var json = JsonSerializer.Serialize(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
            return int.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            var json = JsonSerializer.Serialize(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
