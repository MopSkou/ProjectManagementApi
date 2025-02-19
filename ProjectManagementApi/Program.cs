using Microsoft.EntityFrameworkCore;
using ProjectManagementApi;
using ProjectManagementApi.Data;
using ProjectManagementApi.Repository.Interfaces;
using ProjectManagementApi.Repository;
using ProjectManagementApi.Repository.ProjectManagementApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddScoped<IDoljnostiRepository, DoljnostiRepository>();
builder.Services.AddScoped<IDoljnostiEmployeeRepository, DoljnostiEmployeeRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IProjectEmployeesRepository, ProjectEmployeesRepository>();
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


