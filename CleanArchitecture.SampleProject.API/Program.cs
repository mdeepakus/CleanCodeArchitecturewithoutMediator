using Asp.Versioning;
using CleanArchitecture.SampleProject.Api.Services;
using CleanArchitecture.SampleProject.Api.Utility;
using CleanArchitecture.SampleProject.API.Dispatchers;
using CleanArchitecture.SampleProject.Application;
using CleanArchitecture.SampleProject.Application.Contracts;
using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Identity;
using CleanArchitecture.SampleProject.Identity.Models;
using CleanArchitecture.SampleProject.Infrastructure;
using CleanArchitecture.SampleProject.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;


Log.Information("Starting Instanda.SampleService");

//CreateHostBuilder(args).Build().Run();
var config = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json")
   .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    //.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


//builder.Services.AddSwaggerGen();
AddSwagger(builder.Services);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

RegisterQueryHandlers(builder);
RegisterCommands(builder);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await CleanArchitecture.SampleProject.Identity.Seed.UserCreator.SeedAsync(userManager);
        Log.Information("Application Starting");
    }
    catch (Exception ex)
    {
        Log.Warning(ex, "An error occured while starting the application");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture Sample API");
});

app.UseCors("Open");

app.UseAuthorization();

app.MapControllers();

static void RegisterQueryHandlers(WebApplicationBuilder builder)
{
    foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))))
    {
        foreach (var interfaceType in type.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
        {
            builder.Services.AddScoped(interfaceType, type);
        }
    }
}

static void RegisterCommands(WebApplicationBuilder builder)
{
    foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))))
    {
        foreach (var interfaceType in type.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
        {
            builder.Services.AddScoped(interfaceType, type);
        }
    }

    foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))))
    {
        foreach (var interfaceType in type.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
        {
            builder.Services.AddScoped(interfaceType, type);
        }
    }
}

static void AddSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Clean Architecture - Sample API",

        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.CustomSchemaIds(x => x.FullName);
        c.OperationFilter<FileResultContentTypeOperationFilter>();
    });
}
app.Run();
