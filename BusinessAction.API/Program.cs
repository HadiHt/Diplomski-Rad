using BusinessAction.API.Context;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Repositories;
using BusinessAction.API.Services.Actions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app);

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<UniversityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BusinessIntegration")));

    services.AddHttpClient();

    services.AddScoped<ICourseOfferingRepository, CourseOfferingRepository>();
    services.AddScoped<IMajorRepository, MajorRepository>();
    services.AddScoped<IStudentRepository, StudentRepository>();
    services.AddScoped<IEnrollementRepo, EnrollementRepo>();


    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddTransient<P01Actions>();
    services.AddTransient<P02Actions>();
    services.AddTransient<P03Actions>();



}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
}
