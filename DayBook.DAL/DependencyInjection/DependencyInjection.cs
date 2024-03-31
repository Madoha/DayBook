using DayBook.DAL.Interceptors;
using DayBook.DAL.Repositories;
using DayBook.Domain.Entity;
using DayBook.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayBook.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQL");

        services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection service)
    {
        service.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        service.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
    }

}
