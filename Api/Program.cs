using Infrastructure.Data;
using Infrastructure.Extensions;
using System.Reflection;
using Api.Middleware;
using Api.Extensions;
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