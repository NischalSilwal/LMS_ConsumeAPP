using LMS_ConsumeAPP.Application.DTOs.User;
using LMS_ConsumeAPP.Application.Interface.Services.AuthService;
using LMS_ConsumeAPP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LMS_ConsumeAPP.Infrastructure.Persistence.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync(UserDto userDto)
        {
            // Send the POST request with userDto
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7288/api/Auth/Login", userDto);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content as a plain string (JWT token)
            var token = await response.Content.ReadAsStringAsync();

            return token;  // Return the JWT token directly
        }

    }
}
