using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using ReactRoastDotnet.API.Middleware;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

// Set up Database connection.
// TODO: Change to SQLServer or PostgresSQL for production
// Set up for EF service
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(
    // Specify the database provider
    options => options.UseSqlite(connectionString, x => x.MigrationsAssembly("ReactRoastDotnet.Data")));

// Set up Identity Core.
builder.Services.AddIdentityCore<User>(options => { options.User.RequireUniqueEmail = true; })
    .AddRoles<CustomRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add authentication and authorization
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Add global middleware.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseCors(options =>
    {
        options.AllowAnyHeader().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:3175");
    });
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();