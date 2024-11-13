using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using agencia.Database;
using agencia.Interfaces;
using agencia.Repositories;
using agencia.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContextMemory>(options =>
    options.UseInMemoryDatabase("TravelAgencyDb"));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<TravelService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Agência de Viagens API",
        Version = "v1",
        Description = "API para gerenciar clientes, viagens e preferências de uma agência de viagens.",
        Contact = new OpenApiContact
        {
            Name = "Cristian Kreuz Engel",
            Email = "kreuzengelc@gmail.com"
        }
    });
});
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();