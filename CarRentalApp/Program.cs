using AutoMapper;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using static CarRentalAPI.Infrastructure.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Swagger/OpenAPI konfig�rasyonu
builder.Services.AddEndpointsApiExplorer();

//Swagger konfig�rasyonu
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Rental API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "L�tfen ge�erli bir token girin",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }});
});

//JWT konfig�rasyonu
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

builder.Services.AddAuthorization();

/*using (var context = new CarRentalAPIDbContext())
{
    context.Database.EnsureCreated();
}*/

// Veritaban� ba�lant�s�
builder.Services.AddDbContext<CarRentalAPIDbContext>(opt =>
{
    var connStr = builder.Configuration.GetConnectionString("CarRentalDbConnection");

    opt.UseSqlServer(connStr);
});

// Ba��ml�l�k enjeksiyonu
builder.Services.AddProjectDependencies();

// AutoMapper konfig�rasyonu
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CORS'u konfig�re et
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Build the web application.
var app = builder.Build();

// EnsureCreated() �a�r�s� uygulama yap�land�r�ld�ktan sonra yap�lmal�
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CarRentalAPIDbContext>();
    context.Database.EnsureCreated();
}

// HTTP istek hatt�n� yap�land�r
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS'u kullan
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();