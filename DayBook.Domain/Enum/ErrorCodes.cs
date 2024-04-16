using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.Domain.Enum;

public enum ErrorCodes
{
    // 1-10 report error codes
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,

    // 11-20 user error codes
    UserNotFound = 11,
    UserAlreadyExists = 12,
    UserUnauthorizedAccess = 13,
    UserAlreadyHasThisRole = 14,

    PasswordNotEqualPasswordConfirm = 21,
    PasswordIsIncorrect = 22,

    RoleAlreadyExists = 31,
    RoleNotFound = 32,

    InternalServerError = 10
}
