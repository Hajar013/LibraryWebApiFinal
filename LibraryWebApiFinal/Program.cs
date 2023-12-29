using DAL.Repositories.RepositoryFactory;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BLL.Services.PersonServices;
using System.Reflection;
using BLL;
using BLL.Services.LibrarianServices;
using BLL.Services.BorrowerServices;

var builder = WebApplication.CreateBuilder(args);

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


//builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddAutoMapper(typeof(Program));
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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Auto Mapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
