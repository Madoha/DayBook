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

    InternalServerError = 10
}
