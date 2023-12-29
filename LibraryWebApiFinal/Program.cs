using DAL.Repositories.RepositoryFactory;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BLL.Services.PersonServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

////Add Auto Mapper
//builder.Services.AddAutoMapper(typeof(Program).Assembly);
// Configure DbContext
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"))
);
// Configure Factory
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

// Configure services
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Auto Mapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(Program));

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
