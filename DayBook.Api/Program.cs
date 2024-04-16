using DayBook.DAL.DependencyInjection;
using DayBook.Application.DependencyInjection;
using Serilog;
using DayBook.Domain.Settings;
using DayBook.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DayBook.DAL;
using Microsoft.EntityFrameworkCore;
using DayBook.Producer.DependencyInjection;
using DayBook.Consumer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection)); // class JwtSettings будет заполнться данными их секции JWT
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection(nameof(RabbitMqSettings)));

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"));
});

// Connect authentication and authorization
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
    var jwtKey = options.JwtKey;
    var issuer = options.Issuer;
    var audience = options.Audience;
    var authority = options.Authority;
    x.Authority = authority;
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

// Connect Swagger
builder.Services.AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "DayBook.API",
        Description = "This is version 1.0",
        TermsOfService = new Uri("https://youtube.com"),
        Contact = new OpenApiContact()
        {
            Name = "Test contact",
            Url = new Uri("https://google.com")
        }
    });

    options.SwaggerDoc("v2", new OpenApiInfo()
    {
        Version = "v2",
        Title = "DayBook.API",
        Description = "This is version 2.0",
        TermsOfService = new Uri("https://youtube.com"),
        Contact = new OpenApiContact()
        {
            Name = "Test contact",
            Url = new Uri("https://google.com")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // DayBook.API.xml
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccessLayer(configuration);
builder.Services.AddApplication();
builder.Services.AddProducer();
builder.Services.AddConsumer();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "DayBook Swagger v1.0");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "DayBook Swagger v2.0");
        s.RoutePrefix = string.Empty;
    });
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.MapControllers();

app.Run();
