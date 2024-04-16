using DayBook.Domain.Entity;
using DayBook.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DayBook.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    IBaseRepository<User> Users { get; set; }
    IBaseRepository<Role> Roles { get; set; }
    IBaseRepository<UserRole> UserRoles { get; set; }
}
