using Alasson.Interfaces;
using Alasson.Models;
using Alasson.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var corsConfig = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsConfig, policy =>
    {
        policy.WithOrigins("*");
        policy.WithMethods("*");
        policy.WithHeaders("*");
    });
});


//parámetro: clase EmployeeService y que la estructura es la de la interfaz IES
builder.Services.AddScoped<IEmployeesService,EmployeesService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("api");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsConfig);

app.UseAuthorization();

app.MapControllers();

app.Run();
