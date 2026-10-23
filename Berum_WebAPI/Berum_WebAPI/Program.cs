using Berum_WebAPI.Helpers;
using Berum_WebAPI.Models;
using Berum_WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using Scalar.AspNetCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Berum_WebAPI.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

// Add services to the container.
QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IOrdenReparacionService, OrdenReparacionService>();
builder.Services.AddScoped<IHistorialEstadoService, HistorialEstadoService>();
builder.Services.AddScoped<ITecnicoService, TecnicoService>();  
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ITipoFallaService, TipoFallaService>();
builder.Services.AddScoped<ITipoEquipoService, TipoEquipoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// --- Configuración JWT ---
var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"]
    ?? throw new InvalidOperationException("Falta configurar Jwt:Key en appsettings.json o user-secrets.");

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
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailGmailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(
        options =>
        options.WithTitle(title: "RepairShop API")
        .WithTheme(ScalarTheme.Saturn)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http));
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
