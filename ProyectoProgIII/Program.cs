using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Proyecto.Infraestructure.Context;
using Proyecto.Application.IServices;
using Proyecto.Domain.Repositories;
using Proyecto.Application.Services;
using Proyecto.Infraestructure.Repositories;
using Proyecto.Infraestructure.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuracion de Swagger con autenticacion Bearer
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Agrega el token generado al loguearse"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

// Configuracion de conexion DB
var connectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<ProyectoDbContext>(options => options.UseSqlite(connectionString));

// Inyeccion de dependencias
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddScoped<ICommercialInvoiceService, CommercialInvoiceService>();
builder.Services.AddScoped<ICommercialInvoiceRepository, CommercialInvoiceRepository>();

// Configuracion del servicio de autenticacion 
builder.Services.AddScoped<ICustomAuthenticationService, AutenticacionService>();
builder.Services.Configure<AutenticacionServiceOptions>(
    builder.Configuration.GetSection(AutenticacionServiceOptions.AutenticacionService));

// Configuracion de autenticacion JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AutenticacionService:Issuer"],
            ValidAudience = builder.Configuration["AutenticacionService:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AutenticacionService:SecretForKey"])),
            RoleClaimType = "role"
        };
    });

//builder.Services.AddAuthorization(options =>
//
//    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
//    options.AddPolicy("DevPolicy", policy => policy.RequireRole("dev"));
//    options.AddPolicy("ClientPolicy", policy => policy.RequireRole("client"));
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuracion CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173") // Especifica el origen permitido
           .AllowAnyMethod()                     // Permite cualquier metodo HTTP
           .AllowAnyHeader();                    // Permite cualquier header HTTP
});

// autenticacion y autorizacion
app.UseAuthentication(); //  valida el JWT
app.UseAuthorization();  //  valida las políticas de autorizacion

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
