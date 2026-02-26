using NewsPortal.Application.Services;
using NewsPortal.Domain.Interfaces;
using NewsPortal.Infrastructure.Context;
using NewsPortal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using NewsPortal.API.Middleware;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

// Add Controllers & Swagger first
builder.Services.AddControllers();  
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "News Portal API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token. Example: \"Bearer abcdef12345\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

        // 🔥 This line is CRUCIAL if your token uses a role claim with a schema URI
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    };

});

// Add Authorization
builder.Services.AddAuthorization();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Dependency Injection
builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddSingleton<EncryptionHelper>();

builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<LanguageService>();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");     

// Development-only middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "News Portal v1");
//    });
//}

app.UseSwagger();
app.UseSwaggerUI();

// Enable HTTPS and Routing
app.UseHttpsRedirection();
app.UseRouting();

// 🔥 Order is important: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Custom middleware (Error logger)
app.UseMiddleware<ErrorLoggingMiddleware>();

// Map controller routes
app.MapControllers();

app.Run();
