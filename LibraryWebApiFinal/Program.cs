using DAL.Repositories.RepositoryFactory;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BLL.Services.PersonServices;
using System.Reflection;
using BLL;
using BLL.Services.LibrarianServices;
using BLL.Services.BorrowerServices;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BLL.DTOs;
using System.Security.Claims;
using BLL.Services.AccounterServices;
using BLL.Services.BookServices;
using BLL.Services.TransactionServices;
using BLL.Services.AuthorServices;
using BLL.Services.BookAuthorServices;
using BLL.Services.BillServices;
using BLL.Services.AuthServices;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// configure authorization

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LibrarianPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "librarian");
    });
    options.AddPolicy("BorrowerPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "borrower");
    });
    options.AddPolicy("AccounterPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "accounter");
    });
});

// Add services to the container.

////Add Auto Mapper
builder.Services.AddAutoMapper(typeof(Program), typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
    //config.AddProfile<MappingProfile2>();
    // Add more profiles as needed
});


// Configure DbContext
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"))
);
// Configure Factory
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

// Configure services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ILibrarianService, LibrarianService>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IAccounterService, AccounterService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
builder.Services.AddScoped<IBillService, BillService>();

builder.Services.AddScoped<IAuthService,AuthService>();

//to return person obj when login 
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            // Other JSON serialization options...
        });
///to return person obj when login 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();


app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});





app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();



app.Run();
