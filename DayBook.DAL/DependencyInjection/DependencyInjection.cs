using DayBook.DAL.Interceptors;
using DayBook.DAL.Repositories;
using DayBook.Domain.Entity;
using DayBook.Domain.Interfaces.Databases;
using DayBook.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DayBook.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSql");

        services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        service.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
        service.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
        service.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
        service.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
    }

}
