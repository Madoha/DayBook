using DayBook.Domain.Entity;
using DayBook.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Checking the existence of a report with parameters, if the record already exists in the database, creation is not allowed
    /// Checking the user, if a user with UserId is not found, then this user does not exist
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report report, User user);
}
