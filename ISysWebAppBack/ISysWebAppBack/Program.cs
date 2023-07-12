using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ISysCoreLibBack.Service.IProjectsService;
using ISysCoreLibBack.Service.IOrganizationService;
using ISysCoreLibBack.Service.IUnitService;
using ISysCoreLibBack.Service.IUtilsService;
using ISysCoreLibBack.Repos.OrganizationRepos;
using ISysCoreLibBack.Repos.ProjectsRepos;
using ISysCoreLibBack.Repos.UnitsRepos;
using ISysCoreLibBack.Repos.UtilsRepos;
using ISysDataBaseBack.DBContext;
using ISysWebAppBack.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

configuration.GetSection(Config.Project).Bind(new Config());

builder.Services.AddDbContext<DataBaseContext>(optionsBuilder
    => optionsBuilder.UseNpgsql(Config.ConnectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddScoped<IDepartment, DepartmentRepos>()
    .AddScoped<IEmployeeService, EmployeeRepos>()
    .AddScoped<IProjectService, ProjectRepos>()
    .AddScoped<IEmployeeProjectService, EmployeeProjectRepos>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation()
    .AddValidatorsFromAssembly(typeof(DepartmentRepos).Assembly)
    .AddValidatorsFromAssembly(typeof(EmployeeRepos).Assembly)
    .AddValidatorsFromAssembly(typeof(ProjectRepos).Assembly)
    .AddValidatorsFromAssembly(typeof(EmployeeProjectRepos).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<DataBaseContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
