using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System.Reflection;
using Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Api.Errors;
using Microsoft.OpenApi.Models;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.InfrastructureServiceRegistration(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors((option)=>{

    option.AddPolicy("CorsPolicy",(p)=>p.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7047"));

});
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");



app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
var provider = app.Services.CreateScope().ServiceProvider;
await MigrationHelper.RunMigration(provider);

app.Run();