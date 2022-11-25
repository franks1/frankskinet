using Infrastructure.Data;
using Infrastructure.Extensions;
using System.Reflection;
using Api.Middleware;
using Api.Extensions;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.InfrastructureServiceRegistration(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors((option) =>
{
    option.AddPolicy("CorsPolicy",
        (p) => p.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
});

builder.Services.AddSingleton<IConnectionMultiplexer>((a) =>
{
    var conn = ConfigurationOptions
        .Parse(builder.Configuration.GetConnectionString("redis") ?? string.Empty,
            true);
    return ConnectionMultiplexer.Connect(conn);
});

//Swagger
// builder.Services.AddSwaggerGen((gen) =>
// {
//     gen.SwaggerDoc("v1", new OpenApiInfo() { Title = "Skinet Api", Version = "v1" });
//     var securityScheme = new OpenApiSecurityScheme()
//     {
//         Description = "JWT Authentication Bearer",
//         Name = "Authorization",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         Reference = new OpenApiReference()
//         {
//             Type = ReferenceType.SecurityScheme,
//             Id = "Bearer"
//         }
//     };
//
//     gen.AddSecurityDefinition("Bearer", securityScheme);
//     var securityRequirement = new OpenApiSecurityRequirement { { securityScheme, new[] { "Bearer" } } };
//     gen.AddSecurityRequirement(securityRequirement);
// });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
var provider = app.Services.CreateScope().ServiceProvider;
await MigrationHelper.RunMigration(provider);

app.Run();