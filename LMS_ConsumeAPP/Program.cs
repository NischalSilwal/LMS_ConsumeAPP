using LMS_ConsumeAPP.Application.Interface.Repositories.AuthorRepository;
using LMS_ConsumeAPP.Application.Interface.Repositories.Book;
using LMS_ConsumeAPP.Application.Interface.Repositories.StudentRepository;
using LMS_ConsumeAPP.Application.Interface.Repositories.TransactionRepository;
using LMS_ConsumeAPP.Application.Interface.Services.AuthService;
using LMS_ConsumeAPP.Application.Interface.Services.BookServices;
using LMS_ConsumeAPP.Application.Interface.Services.StudentService;
using LMS_ConsumeAPP.Application.Interface.Services.TransactionService;
using LMS_ConsumeAPP.Infrastructure.Persistence.Repositories;
using LMS_ConsumeAPP.Infrastructure.Persistence.Services.AuthService;
using LMS_ConsumeAPP.Infrastructure.Persistence.Services.BookServices;
using LMS_ConsumeAPP.Infrastructure.Persistence.Services.StudentService;
using LMS_ConsumeAPP.Infrastructure.Persistence.Services.TransactionServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient
builder.Services.AddHttpClient();

// Add session services to the container
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register services
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddHttpClient("LMSApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7288/api/"); // Replace with your API's BaseURL
});
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
// Register IHttpContextAccessor to access HttpContext in controllers
builder.Services.AddHttpContextAccessor();

// Add controllers with views (for ModelState support)
builder.Services.AddControllersWithViews();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://myapi.example.com", // Set your valid issuer
        ValidAudience = "https://myapp.example.com", // Set your valid audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("B2E2D9A18A4E54B17FC9AEF65B3B490715B8D36E7F1F11E1238D4A012D0E35D6")) // Set your secret key
    };
});

// Add Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session middleware
app.UseSession();

// Use authentication middleware
app.UseAuthentication(); // This ensures JWT authentication is handled

app.UseAuthorization();

// Map default route for MVC controllers
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}");
});

app.Run();
