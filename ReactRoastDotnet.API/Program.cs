using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReactRoastDotnet.API.Middleware;
using ReactRoastDotnet.API.Services;
using ReactRoastDotnet.Data;
using ReactRoastDotnet.Data.Configurations;
using ReactRoastDotnet.Data.Entities;
using ReactRoastDotnet.Data.Roles;

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
        };
    });
builder.Services.AddAuthorization();
// Scoped for the beginning and end of request.
builder.Services.AddScoped<TokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // TODO: Generate OpenApi xml file.
    // Read generated XML document
    // var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file));
    // Set up Swagger to use a token in our header.
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Put **_ONLY_** your JWT Bearer token on text-box below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "React Roast.NET", Version = "v1" });
});

var app = builder.Build();

// Add global middleware.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await UserConfiguration.Initialize(context, userManager);


    app.UseCors(options =>
    {
        options.AllowAnyHeader().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:5175");
    });
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"); });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();