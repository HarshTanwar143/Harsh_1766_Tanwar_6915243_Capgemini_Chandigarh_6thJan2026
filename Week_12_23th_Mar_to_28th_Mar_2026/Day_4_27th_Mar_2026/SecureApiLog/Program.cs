using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using log4net;
using log4net.Config;
using System.Reflection;
using SecureApiLog.Middleware;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var builder = WebApplication.CreateBuilder(args);

const string SecretKey = "SUPER_SECRET_KEY_FOR_JWT_AUTH_12345678";

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var log = LogManager.GetLogger("JwtMiddleware");
                log.Warn($"Token authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var log = LogManager.GetLogger("JwtMiddleware");
                var user = context.Principal?.Identity?.Name ?? "unknown";
                log.Info($"Valid token used by: {user}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                var log = LogManager.GetLogger("JwtMiddleware");
                log.Warn($"Unauthorized access to: {context.Request.Path}");
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                var log = LogManager.GetLogger("JwtMiddleware");
                log.Warn($"Forbidden access to: {context.Request.Path}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Secure API with JWT + Log4net",
        Version = "v1",
        Description = "Case Study 2: JWT Auth + Log4net Security Logging"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token here."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Secure API v1");
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
